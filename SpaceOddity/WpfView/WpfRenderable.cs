using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewInterface;

namespace WpfView
{
    public class WpfRenderable : IRenderable
    {
        private IFrameworkElementWrapper wrapper;
        private Canvas canvasParent;
        private BuilderWorldObjectState[] states;

        public IAction LeftClickAction { private get; set; }
        public IAction RightClickAction { private get; set; }

        public WpfRenderable(IFrameworkElementWrapper wrapper, Canvas canvasParent, BuilderWorldObjectState[] states)
        {
            this.wrapper = wrapper;
            this.canvasParent = canvasParent;
            this.states = states;
            wrapper.Element.MouseLeftButtonDown += ElementMouseLeftButtonDown;
            wrapper.Element.MouseRightButtonDown += ElementMouseRightButtonDown;
        }

        private void ElementMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LeftClickAction.Execute();
            e.Handled = true;
        }

        private void ElementMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            RightClickAction.Execute();
            e.Handled = true;
        }

        public void Update(Vector2 position, Vector2 rotation, Vector2 scale)
        {
            var topLeftPosition = position - scale * 0.5;
            Canvas.SetTop(wrapper.Element, topLeftPosition.Y);
            Canvas.SetLeft(wrapper.Element, topLeftPosition.X);
            wrapper.Element.Height = scale.Y;
            wrapper.Element.Width = scale.X;
        }

        public void Delete()
        {
            canvasParent.Children.Remove(wrapper.Element);
        }

        public void SetState(int state)
        {
            wrapper.Fill = states[state].Fill;
            wrapper.Border = states[state].Border;
        }
    }
}

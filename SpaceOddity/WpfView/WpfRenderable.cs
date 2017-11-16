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
        private FrameworkElement element;
        private Canvas canvasParent;

        public IAction LeftClickAction { private get; set; }
        public IAction RightClickAction { private get; set; }

        public WpfRenderable(FrameworkElement element, Canvas canvasParent)
        {
            this.element = element;
            this.canvasParent = canvasParent;
            element.MouseLeftButtonDown += ElementMouseLeftButtonDown;
            element.MouseRightButtonDown += ElementMouseRightButtonDown;
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
            Canvas.SetTop(element, topLeftPosition.Y);
            Canvas.SetLeft(element, topLeftPosition.X);
            element.Height = scale.Y;
            element.Width = scale.X;
        }

        public void Delete()
        {
            canvasParent.Children.Remove(element);
        }
    }
}

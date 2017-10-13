using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using ViewInterface;

namespace WpfView
{
    public class WpfWorldObject : IWorldObject
    {
        private FrameworkElement element;

        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                UpdateElement();
            }
        }

        private Vector2 scale;
        public Vector2 Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                UpdateElement();
            }
        }

        private IAction leftClickAction;
        public IAction LeftClickAction 
        {
            get
            {
                return leftClickAction;
            }
            set
            {
                leftClickAction = value;
            }
        }

        private void UpdateElement()
        {
            var topLeftPosition = position - scale * 0.5;
            Canvas.SetTop(element, topLeftPosition.Y);
            Canvas.SetLeft(element, topLeftPosition.X);
            element.Height = scale.Y;
            element.Width = scale.X;
        }

        public WpfWorldObject(FrameworkElement element, Vector2 position, Vector2 scale, IAction leftClickAction)
        {
            this.element = element;
            Position = position;
            Scale = scale;
            LeftClickAction = leftClickAction;

            element.MouseLeftButtonDown += ElementMouseLeftButtonDown;
        }

        private void ElementMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LeftClickAction.Execute();
        }
    }
}

using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using ViewInterface;

namespace WpfView
{
    public class WpfWorldObject : IWorldObject
    {
        private Canvas elementCanvas;
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
        public Vector2 Rotation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
            }
        }

        public IAction LeftClickAction { get; set; }
        public IAction RightClickAction { get; set; }

        private void UpdateElement()
        {
            var topLeftPosition = position - scale * 0.5;
            Canvas.SetTop(element, topLeftPosition.Y);
            Canvas.SetLeft(element, topLeftPosition.X);
            element.Height = scale.Y;
            element.Width = scale.X;
        }

        public WpfWorldObject(FrameworkElement element, Canvas elementCanvas, Vector2 position, Vector2 scale, 
            IAction leftClickAction, IAction rightClickAction)
        {
            this.element = element;
            this.elementCanvas = elementCanvas;
            Position = position;
            Scale = scale;
            LeftClickAction = leftClickAction;
            RightClickAction = rightClickAction;

            element.MouseLeftButtonDown += ElementMouseLeftButtonDown;
            element.MouseRightButtonDown += ElementMouseRightButtonDown;
        }

        private void ElementMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LeftClickAction.Execute();
        }

        private void ElementMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            RightClickAction.Execute();
        }

        public void Delete()
        {
            elementCanvas.Children.Remove(element);
        }
    }
}

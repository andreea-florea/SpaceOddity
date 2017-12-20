using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ViewInterface;

namespace WpfView
{
    public class WpfCurveRenderable : IRenderable
    {
        private Line line;
        private ICurve curve;
        private Canvas canvasParent;
        private BuilderWorldObjectState[] states;

        public IAction LeftClickAction { private get; set; }

        public IAction RightClickAction { private get; set; }

        public WpfCurveRenderable(Line line, ICurve curve, Canvas canvasParent, BuilderWorldObjectState[] states)
        {
            this.line = line;
            this.curve = curve;
            this.canvasParent = canvasParent;
            this.states = states;
            line.MouseLeftButtonDown += ElementMouseLeftButtonDown;
            line.MouseRightButtonDown += ElementMouseRightButtonDown;
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
            Canvas.SetTop(line, position.Y);
            Canvas.SetLeft(line, position.X);

            var halfScale = scale * 0.5;
            var startPoint = curve.GetPoint(0).Multiply(halfScale);
            var endPoint = curve.GetPoint(1).Multiply(halfScale);

            line.X1 = startPoint.X;
            line.Y1 = startPoint.Y;
            line.X2 = endPoint.X;
            line.Y2 = endPoint.Y;
        }

        public void Delete()
        {
            canvasParent.Children.Remove(line);
        }

        public void SetState(int state)
        {
            line.Fill = new SolidColorBrush(states[state].Fill.GetColor());
            line.Stroke = new SolidColorBrush(states[state].Border.GetColor());
        }
    }
}

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
        private IList<Line> lines;
        private ICurve curve;
        private Canvas canvasParent;
        private BuilderWorldObjectState[] states;

        public IAction LeftClickAction { private get; set; }

        public IAction RightClickAction { private get; set; }

        public WpfCurveRenderable(IList<Line> lines, ICurve curve, Canvas canvasParent, BuilderWorldObjectState[] states)
        {
            this.lines = lines;
            this.curve = curve;
            this.canvasParent = canvasParent;
            this.states = states;
            AssignLineControl();
        }

        private void AssignLineControl()
        {
            foreach (var line in lines)
            {
                line.MouseLeftButtonDown += ElementMouseLeftButtonDown;
                line.MouseRightButtonDown += ElementMouseRightButtonDown;
            }
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
            var deltaTime = 1.0 / lines.Count;
            var lineTime = 0.0;

            foreach (var line in lines)
            {
                Canvas.SetTop(line, position.Y);
                Canvas.SetLeft(line, position.X);

                var halfScale = scale * 0.5;
                var startPoint = curve.GetPoint(lineTime).Multiply(halfScale);
                lineTime += deltaTime;
                var endPoint = curve.GetPoint(lineTime).Multiply(halfScale);

                SetLineCoordinates(line, startPoint, endPoint);
            }
        }

        private void SetLineCoordinates(Line line, Vector2 startPoint, Vector2 endPoint)
        {
            line.X1 = startPoint.X;
            line.Y1 = startPoint.Y;
            line.X2 = endPoint.X;
            line.Y2 = endPoint.Y;
        }

        public void Delete()
        {
            foreach (var line in lines)
            {
                canvasParent.Children.Remove(line);
            }
        }

        public void SetState(int state)
        {
            foreach (var line in lines)
            {
                line.Fill = new SolidColorBrush(states[state].Fill.GetColor());
                line.Stroke = new SolidColorBrush(states[state].Border.GetColor());
            }
        }
    }
}

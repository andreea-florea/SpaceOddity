using ConstructedGame;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;

using WpfView;

namespace WpfGameView
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var tileStates = new BuilderWorldObjectState[1];
            tileStates[0] = new BuilderWorldObjectState(new ColorVector(0.8, 0.9, 0.9), new ColorVector(0.5, 0.6, 0.6));
            var frameworkElementFactory = new RectangleFrameworkElementFactory(1);
            var tileObjectFactory = new WpfRenderableFactory(mainCanvas, frameworkElementFactory, tileStates);

            var blockStates = new BuilderWorldObjectState[1];
            blockStates[0] = new BuilderWorldObjectState(new ColorVector(0.1, 0.8, 0.9), new ColorVector(0.1, 0.8, 0.9));
            var frameworkBlockFactory = new RectangleFrameworkElementFactory(2);
            var blockObjectFactory = new WpfRenderableFactory(mainCanvas, frameworkBlockFactory, blockStates);

            var shipComponentStates = new BuilderWorldObjectState[1];
            shipComponentStates[0] = new BuilderWorldObjectState(new ColorVector(1.0, 0.9, 0.0), new ColorVector(1.0, 0.9, 0.0));
            var frameworkShipComponentFactory = new GridParentFrameworkElementFactory(
                new CircleFrameworkElementFactory(5), new Vector2(20, 20), 5);
            var shipComponentObjectFactory = 
                new WpfRenderableFactory(mainCanvas, frameworkShipComponentFactory, shipComponentStates);

            var emptyShipComponentStates = new BuilderWorldObjectState[1];
            emptyShipComponentStates[0] = new BuilderWorldObjectState(new ColorVector(0.1, 0.9, 1.0), new ColorVector(0.1, 0.9, 1.0));
            var frameworkEmptyShipComponentFactory = new GridParentFrameworkElementFactory(
                new CircleFrameworkElementFactory(5), new Vector2(20, 20), 5);
            var emptyShipComponentsFactory = 
                new WpfRenderableFactory(mainCanvas, frameworkShipComponentFactory, emptyShipComponentStates);

            var pipeLinkStates = new BuilderWorldObjectState[2];
            pipeLinkStates[0] = new BuilderWorldObjectState(new ColorVector(0.15, 0.55, 0.9), new ColorVector(0.15, 0.55, 0.9));
            pipeLinkStates[1] = new BuilderWorldObjectState(new ColorVector(1.0, 0.5, 0.0), new ColorVector(1.0, 0.5, 0.0));
            var frameworkPipeLinkFactory = new FixedSizeFrameworkElementFactory(
                new RectangleFrameworkElementFactory(4), new Vector2(10, 10), 4);
            var pipeLinkObjectFactory = new WpfRenderableFactory(mainCanvas, frameworkPipeLinkFactory, pipeLinkStates);

            var pipeStates = new BuilderWorldObjectState[1];
            pipeStates[0] = new BuilderWorldObjectState(new ColorVector(0.8, 0.9, 1.0), new ColorVector(0.8, 0.9, 1.0));
            var pipeObjectFactory = new WpfCurveRenderableFactory(mainCanvas, pipeStates, 3, 8);

            var rectangle = new MarginRectangleSection(new Vector2(20, 20), 
                new FullRectangleSection(new Geometry.Rectangle(
                        new Vector2(0, 0), new Vector2(mainCanvas.ActualWidth, mainCanvas.ActualHeight))));

            var blueprintBuilderView = new GameViewFactory();
            blueprintBuilderView.CreateBlueprintBuilderView(
                tileObjectFactory, 
                blockObjectFactory, 
                shipComponentObjectFactory, 
                emptyShipComponentsFactory,
                pipeLinkObjectFactory, 
                pipeObjectFactory,
                rectangle);
        }

    }
}

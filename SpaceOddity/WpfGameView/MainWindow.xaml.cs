using Game;
using Game.Interfaces;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewInterface;
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
            var frameworkElementFactory = new RectangleFrameworkElementFactory(Brushes.LightGray, Brushes.Gray);
            var tileObjectFactory = new WpfWorldObjectFactory(mainCanvas, frameworkElementFactory);

            var frameworkBlockFactory = new RectangleFrameworkElementFactory(Brushes.Blue, Brushes.LightBlue);
            var blockObjectFactory = new WpfWorldObjectFactory(mainCanvas, frameworkBlockFactory);

            CreateBlueprintBuilderView(tileObjectFactory, blockObjectFactory);
        }

        private void CreateBlueprintBuilderView(IWorldObjectFactory tileObjectFactory, IWorldObjectFactory blockObjectFactory)
        {
            var observableBlueprintBuilder = CreateBlueprintBuilder();

            var fittingRectangle = CreateViewRectangle(observableBlueprintBuilder,
                new Geometry.Rectangle(new Vector2(0, 0), new Vector2(mainCanvas.ActualWidth, mainCanvas.ActualHeight)));

            var controller = new BlueprintBuilderController();

            var blueprintViewModelFactory =
                new BlueprintBuilderViewModelFactory(tileObjectFactory, blockObjectFactory, controller);
            blueprintViewModelFactory.CreateViewModel(observableBlueprintBuilder, fittingRectangle);
        }

        private AspectRatioRectangleSection CreateViewRectangle(
            IObservableBlueprintBuilder observableBlueprintBuilder, Geometry.Rectangle containingRectangle)
        {
            return new AspectRatioRectangleSection(
                new Vector2(observableBlueprintBuilder.Width, observableBlueprintBuilder.Height),
                new MarginRectangleSection(new Vector2(20, 20), new FullRectangleSection(containingRectangle)));
        }

        private IObservableBlueprintBuilder CreateBlueprintBuilder()
        {
            var width = 10;
            var height = 10;

            var blueprint = new IBlock[height, width];
            var blockFactory = new BlockFactory(1);
            var blueprintBuilder = new BlueprintBuilder(blueprint, blockFactory);
            return new ObservableBlueprintBuilder(blueprintBuilder);
        }
    }
}

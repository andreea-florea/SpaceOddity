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
using ViewModel;
using WpfView;

namespace WpfGameView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var width = 10;
            var height = 10;

            var blueprint = new IBlock[height, width];
            var blockFactory = new BlockFactory(1);
            var blueprintBuilder = new BlueprintBuilder(blueprint, blockFactory);
            var observableBlueprintBuilder = new ObservableBlueprintBuilder(blueprintBuilder);

            var fittingRectangle = new AspectRatioRectangleSection(
                new Vector2(width, height), new MarginRectangleSection(new Vector2(10, 10),
                    new FullRectangleSection(
                        new Geometry.Rectangle(new Vector2(0, 0), 
                            new Vector2(mainCanvas.ActualWidth, mainCanvas.ActualHeight)))));

            var frameworkElementFactory = new RectangleFrameworkElementFactory(Brushes.LightGray, Brushes.Gray);
            var tileObjectFactory = new WpfWorldObjectFactory(mainCanvas, frameworkElementFactory);

            var frameworkBlockFactory = new RectangleFrameworkElementFactory(Brushes.Blue, Brushes.LightBlue);
            var blockObjectFactory = new WpfWorldObjectFactory(mainCanvas, frameworkBlockFactory);

            var controller = new BlueprintBuilderController();

            var blueprintViewModelFactory = new BlueprintBuilderViewModelFactory(tileObjectFactory, blockObjectFactory, controller);
            blueprintViewModelFactory.CreateViewModel(observableBlueprintBuilder, fittingRectangle);
        }
    }
}

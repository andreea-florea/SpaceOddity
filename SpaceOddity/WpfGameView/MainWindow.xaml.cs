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
using ViewModel.Fancy;

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

            var rectangle = new MarginRectangleSection(new Vector2(20, 20), 
                new FullRectangleSection(new Geometry.Rectangle(
                        new Vector2(0, 0), new Vector2(mainCanvas.ActualWidth, mainCanvas.ActualHeight))));

            var blueprintBuilderView = new GameViewFactory();
            blueprintBuilderView.CreateBlueprintBuilderView(tileObjectFactory, blockObjectFactory, rectangle);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Torpedo.View.single_view
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        public Game()
        {
            InitializeComponent();
        }

        private void grid_pressed(object sender, MouseButtonEventArgs e)
        {

            Grid asd = (Grid)sender;
            msg(asd.Name.ToString());

        }


        private void msg(string szoveg)
        {

            MessageBox.Show(szoveg, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

        private void btnStartOver_Click(object sender, RoutedEventArgs e)
        {

        }


        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }
    }

 
}

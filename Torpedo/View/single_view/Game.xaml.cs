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

        Grid[] placed_ships_on_grids,my_grids;
        
        public Game(Grid[] fields)
        {
            placed_ships_on_grids = fields;
            InitializeComponent();
            set_my_grids();
        }

        private void set_my_grids() {
            my_grids = new Grid[] { gridA1, gridA2, gridA3, gridA4, gridA5, gridA6, gridA7, gridA8, gridA9, gridA10,
                                    gridB1, gridB2, gridB3, gridB4, gridB5, gridB6, gridB7, gridB8, gridB9, gridB10,
                                    gridC1, gridC2, gridC3, gridC4, gridC5, gridC6, gridC7, gridC8, gridC9, gridC10,
                                    gridD1, gridD2, gridD3, gridD4, gridD5, gridD6, gridD7, gridD8, gridD9, gridD10,
                                    gridE1, gridE2, gridE3, gridE4, gridE5, gridE6, gridE7, gridE8, gridE9, gridE10,
                                    gridF1, gridF2, gridF3, gridF4, gridF5, gridF6, gridF7, gridF8, gridF9, gridF10,
                                    gridG1, gridG2, gridG3, gridG4, gridG5, gridG6, gridG7, gridG8, gridG9, gridG10,
                                    gridH1, gridH2, gridH3, gridH4, gridH5, gridH6, gridH7, gridH8, gridH9, gridH10,
                                    gridI1, gridI2, gridI3, gridI4, gridI5, gridI6, gridI7, gridI8, gridI9, gridI10,
                                    gridJ1, gridJ2, gridJ3, gridJ4, gridJ5, gridJ6, gridJ7, gridJ8, gridJ9, gridJ10};
            int index = 0;
            foreach (Grid p in placed_ships_on_grids) {
                if (p.Tag.ToString() != "empty")
                {
                    my_grids[index].Tag = "ship";
                    my_grids[index].Background = Brushes.Green;
                }
                else
                {
                    my_grids[index].Tag = "empty";
                }
                index++;
            }
        
        
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

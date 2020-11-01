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
using Torpedo.Modell.Single_modell;

namespace Torpedo.View.single_view
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {

        Grid[] ai_grids,my_grids;
        Gamemodell game;
        
        public Game(ref Grid[] fields)
        {
            InitializeComponent();
            set_my_grids(fields);
            set_ai_grids();
            game = new Gamemodell(ref ai_grids,this);
            
        }
        private void set_ai_grids()
        {
            ai_grids = new Grid[] { A1, A2, A3, A4, A5, A6, A7, A8, A9, A10,
                                    B1, B2, B3, B4, B5, B6, B7, B8, B9, B10,
                                    C1, C2, C3, C4, C5, C6, C7, C8, C9, C10,
                                    D1, D2, D3, D4, D5, D6, D7, D8, D9, D10,
                                    E1, E2, E3, E4, E5, E6, E7, E8, E9, E10,
                                    F1, F2, F3, F4, F5, F6, F7, F8, F9, F10,
                                    G1, G2, G3, G4, G5, G6, G7, G8, G9, G10,
                                    H1, H2, H3, H4, H5, H6, H7, H8, H9, H10,
                                    I1, I2, I3, I4, I5, I6, I7, I8, I9, I10,
                                    J1, J2, J3, J4, J5, J6, J7, J8, J9, J10};
        }
            private void set_my_grids(Grid[] placed_ships_on_grids) {
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

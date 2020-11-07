using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Torpedo.View.single_view
{
    /// <summary>
    /// Interaction logic for Winner.xaml
    /// </summary>
    public partial class Loser : Window
    {
        Game game;
        public Loser(Game game)
        {
            this.game = game;
            InitializeComponent();

        }

        private void Yes_Button_Click(object sender, RoutedEventArgs e)
        {
            Ship_Placement ship_Placement = new Ship_Placement(game.username);
            game.Close();
            ship_Placement.Show();
            this.Close();
        }

        private void No_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            game.Close();
            mainwindow.Show();
            this.Close();

        }
    }
}

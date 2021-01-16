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

namespace Torpedo.View.pvp_view
{
    /// <summary>
    /// Interaction logic for PvpWinner.xaml
    /// </summary>
    public partial class PvpWinner : Window
    {
        PvpGame pvpgame;
        string winner;

        public PvpWinner(PvpGame pvpgame, string winnerName)
        {
            this.pvpgame = pvpgame;
            this.winner = winnerName;
            InitializeComponent();
            winnerNameTB.Text = winner + "!";
        }

        private void MenuButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            pvpgame.Close();
            mainwindow.Show();
            this.Close();

        }
    }
}

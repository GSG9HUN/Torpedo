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
using Torpedo.View.pvp_view;

namespace Torpedo
{
    /// <summary>
    /// Interaction logic for MultiPlayer.xaml
    /// </summary>
    public partial class MultiPlayer : Window
    {
        public MultiPlayer()
        {
            InitializeComponent();
        }

        private void StartClick(object sender, RoutedEventArgs e)
        {
            String player1Name = player1NameTB.Text;
            String player2Name = player2NameTB.Text;

            if (player1Name == "" || player2Name == "")
            {
                msg("Be kell írni egy nevet mindenkinek!");
            }
            else
            {
                Pvp1ShipPlacement pvp1ShipPlacement = new Pvp1ShipPlacement(player1Name, player2Name);
                this.Close();
                pvp1ShipPlacement.Show();
            }
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            this.Close();
            mainwindow.Show();
        }
        private void msg(string szoveg)
        {

            MessageBox.Show(szoveg, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

        }
    }
}

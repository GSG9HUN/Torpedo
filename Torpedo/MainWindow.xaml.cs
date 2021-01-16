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
using Torpedo.View.single_view;

namespace Torpedo
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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void Single_Click(object sender, RoutedEventArgs e)
        {
            Single_player single_player = new Single_player();
            this.Close();
            single_player.Show();
        }

        private void PvpClick(object sender, RoutedEventArgs e)
        {
            MultiPlayer multiPlayer = new MultiPlayer();
            this.Close();
            multiPlayer.Show();
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            Result result = new Result();
            this.Close();
            result.Show();
        }
    }
}

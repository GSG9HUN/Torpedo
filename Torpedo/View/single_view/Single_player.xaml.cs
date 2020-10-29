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
using Torpedo.View.single_view;

namespace Torpedo
{

    
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Single_player : Window
    {

        String _username{ set; get; }
       
    public Single_player()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            String name = username.Text;
            if (name == "") {
                MessageBox.Show("You must enter a name", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Ship_Placement ship_placement = new Ship_Placement();
                _username = name;
                this.Close();
                ship_placement.Show();
            }
        

        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow=new MainWindow();
            this.Close();
            mainwindow.Show();
        }
    }
}

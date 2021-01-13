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
using Torpedo.Modell.Single_modell;

namespace Torpedo.View.single_view
{
    /// <summary>
    /// Interaction logic for Result.xaml
    /// </summary>
    public partial class Result : Window
    {
        public Result()
        {
            InitializeComponent();
            if (FileWriter.list_adatok.Count==0)
            {
                FileWriter.ReadFromJSON();
            }  

            foreach(Datas p in FileWriter.list_adatok){
                listbox_winner.Items.Add(p.player_name);
                listbox_loser.Items.Add(p.player_name2);
               
            }
          
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            this.Close();
            mainwindow.Show();
        }
    }
}

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
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 
    
    public partial class Ship_Placement : Window
    {


        String irany { get; set; }
        String selected_ship { get; set; }

        Path lastship;
        Polygon lastarrow;

        SolidColorBrush unselected = new SolidColorBrush(Colors.Black);
        SolidColorBrush selected = new SolidColorBrush(Colors.Green);

        public Ship_Placement()
        {
            InitializeComponent();
            irany = "fel";
        }

        private void ship_MouseLeftButtonDown(object sender, MouseButtonEventArgs args) {

            Path myship = (Path)sender;

            if (lastship != null && lastship != myship) {

                lastship.Stroke = unselected;
            }

            lastship = myship;
            
            switch (myship.Name) {
                case "destroyer": selected_ship = "destroyer"; myship.Stroke = selected; break;
                case "cruiser": selected_ship = "cruiser"; myship.Stroke = selected; break;
                case "submarine": selected_ship = "submarine"; myship.Stroke = selected; break;
                case "battleship":selected_ship = "battleship"; myship.Stroke = selected; break;
                case "carrier": selected_ship = "carrier"; myship.Stroke = selected;  break;
            }






        }
        private void orientationMouseDown(object sender, MouseButtonEventArgs args)
        {
            if (args.LeftButton == Mouse.LeftButton)
            {
                
                Polygon mypoly = (Polygon)sender;


                if (lastarrow != null && lastarrow != mypoly)
                {

                    lastarrow.Stroke = unselected;
                }

                lastarrow = mypoly;

                switch (mypoly.Name) {

                    case "jobb": irany = "jobb";
                        mypoly.Stroke = selected;
                        break;
                    case "bal": irany = "bal";
                        mypoly.Stroke = selected;
                        break;
                    case "fel": irany = "fel";
                        mypoly.Stroke = selected;
                        break;
                    case "le": irany = "le";
                        mypoly.Stroke = selected;
                        break;
                

                }




            }
            
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 
    public partial class Ship_Placement : Window
    {
        bool ballra_mehet = true;
        bool jobbra_mehet = true;
        bool felfele_mehet = true;
        bool lefele_mehet = true;
        String irany { get; set; }
        String selected_ship { get; set; }
        Path[] ships;
        Path lastship;
        Polygon lastarrow;
        Dictionary<string, int> placed_ship = new Dictionary<string, int>();

        SolidColorBrush unselected = new SolidColorBrush(Colors.Black);
        SolidColorBrush selected = new SolidColorBrush(Colors.Green);
        Grid[] fields;

        public Ship_Placement()
        {
            InitializeComponent();
            irany = "fel";
            fields = new Grid[]
            {
                A1, A2, A3, A4, A5, A6, A7, A8, A9, A10,
                B1, B2, B3, B4, B5, B6, B7, B8, B9, B10,
                C1, C2, C3, C4, C5, C6, C7, C8, C9, C10,
                D1, D2, D3, D4, D5, D6, D7, D8, D9, D10,
                E1, E2, E3, E4, E5, E6, E7, E8, E9, E10,
                F1, F2, F3, F4, F5, F6, F7, F8, F9, F10,
                G1, G2, G3, G4, G5, G6, G7, G8, G9, G10,
                H1, H2, H3, H4, H5, H6, H7, H8, H9, H10,
                I1, I2, I3, I4, I5, I6, I7, I8, I9, I10,
                J1, J2, J3, J4, J5, J6, J7, J8, J9, J10
            };

            ships = new Path[] {cruiser, submarine, battleship, destroyer, carrier};

            set_ships();
            set_Tags();
        }

        private void set_ships()
        {
            foreach (var ship in ships) {
                placed_ship.Add(ship.Name.ToString(), 0);
            }
        }

        private void set_Tags()
        {
            foreach (Grid field in fields)
            {
                field.Tag = "empty";
                field.Background = new SolidColorBrush(Colors.Black);
            }
        }

        private void ship_MouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            Path myship = (Path) sender;

            if (lastship != null && lastship != myship)
            {
                lastship.Stroke = unselected;
            }

            lastship = myship;

            switch (myship.Name)
            {
                case "destroyer":
                    selected_ship = "destroyer";
                    myship.Stroke = selected;
                    break;
                case "cruiser":
                    selected_ship = "cruiser";
                    myship.Stroke = selected;
                    break;
                case "submarine":
                    selected_ship = "submarine";
                    myship.Stroke = selected;
                    break;
                case "battleship":
                    selected_ship = "battleship";
                    myship.Stroke = selected;
                    break;
                case "carrier":
                    selected_ship = "carrier";
                    myship.Stroke = selected;
                    break;
            }
        }

        private void orientationMouseDown(object sender, MouseButtonEventArgs args)
        {
            if (args.LeftButton == Mouse.LeftButton)
            {
                Polygon mypoly = (Polygon) sender;


                if (lastarrow != null && lastarrow != mypoly)
                {
                    lastarrow.Stroke = unselected;
                }

                lastarrow = mypoly;

                switch (mypoly.Name)
                {
                    case "jobb":
                        irany = "jobb";
                        mypoly.Stroke = selected;
                        break;
                    case "bal":
                        irany = "bal";
                        mypoly.Stroke = selected;
                        break;
                    case "fel":
                        irany = "fel";
                        mypoly.Stroke = selected;
                        break;
                    case "le":
                        irany = "le";
                        mypoly.Stroke = selected;
                        break;
                }
            }
        }


        private void Reset_clicked(object sender, RoutedEventArgs e)
        {
            if (lastarrow != null)
                lastarrow.Stroke = unselected;
            if (lastship != null)
                lastship.Stroke = unselected;

            foreach (var ship in ships)
            {
                ship.IsEnabled = true;
                ship.Stroke = unselected;
                ship.Opacity = 1;
            }

            set_Tags();
            selected_ship = null;
        }

        private int check_ship(string name) {

            switch (name)
            {
                case "destroyer":
                   return 2;

                case "cruiser":
                   return 3;

                case "submarine":
                    return 3;

                case "battleship":
                    return 4;

                case "carrier":
                    return 5;
            }

            return 0;
        }


        private int set_index(Grid clicked_grid) {
            var value = 0;
            foreach (Grid field in fields)
            {
                if (field.Name == clicked_grid.Name)
                {
                    break;
                }

                value++;
            }

            return value;

        }

        private void call_my_function(string irany, int index, int size) {
            switch (irany)
            {
                case "jobb":
                    Jobbra(index, size);
                    break;
                case "bal":
                    Ballra(index, size);
                    break;
                case "fel":
                    Felfele(index, size);
                    break;
                case "le":
                    Lefele(index, size);
                    break;
            }

        }
        private void grid_pressed(object sender, MouseButtonEventArgs e)
        {
            Grid clicked_grid = (Grid) sender;
            if (selected_ship == null)
            {
                MessageBox.Show("Válassz ki egy hajót");
                return;
            }


            int size = check_ship(selected_ship);
            int index = set_index(clicked_grid);

            call_my_function(irany,index,size);
        }


        private bool check_if_i_can_go_left(int honnan, int meddig) {
            if (honnan % 10 - meddig >= 0)
            {
                for (int i = honnan; i > honnan - meddig; i--)
                {
                    if (fields[i].Tag.ToString() == "ship")
                    {
                        return false;
                        
                    }
                }
            }
            else
            {
                return false;
            }
            return true;

        }

        private bool check_if_i_can_go_right(int honnan, int meddig) {
            if (honnan % 10 + meddig <= 9)
            {
                for (int i = honnan; i < honnan + meddig; i++)
                {
                    if (fields[i].Tag.ToString() == "ship")
                    {
                        return false;
                        break;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private void i_go_left(int honnan,int meddig) {
            for (int i = honnan; i > honnan - meddig; i--)
            {
                fields[i].Background = new SolidColorBrush(Colors.Green);
                fields[i].Tag = "ship";
            }

            foreach (var ship in ships)
            {
                if (ship.Name.ToString() == selected_ship)
                {
                    ship.IsEnabled = false;
                    ship.Opacity = 0.5;
                    if (lastship != null)
                        lastship.Stroke = unselected;
                    lastship = null;
                    selected_ship = null;
                    break;
                }
            }

        }

        private void i_go_right(int honnan, int meddig) {

            for (int i = honnan; i < honnan + meddig; i++)
            {
                fields[i].Background = new SolidColorBrush(Colors.Green);
                fields[i].Tag = "ship";
            }

            foreach (var ship in ships)
            {
                if (ship.Name.ToString() == selected_ship)
                {
                    ship.IsEnabled = false;
                    ship.Opacity = 0.5;
                    if (lastship != null)
                        lastship.Stroke = unselected;
                    lastship = null;
                    selected_ship = null;
                    break;
                }
            }

        }


        private void Ballra(int honnan, int meddig)
        {
            jobbra_mehet = check_if_i_can_go_right(honnan,meddig);
            ballra_mehet = check_if_i_can_go_left(honnan, meddig);
            
            if (ballra_mehet)
            {
                i_go_left(honnan, meddig);
            }
            else if (jobbra_mehet)
            {
                i_go_right(honnan,meddig);

            }

            else
            {
                MessageBox.Show("Ide nem helyezhető hajó");
            }
        }

        private void Jobbra(int honnan, int meddig)
        {
            jobbra_mehet = check_if_i_can_go_right(honnan,meddig);
            ballra_mehet = check_if_i_can_go_left(honnan, meddig);

            if (jobbra_mehet)
                {
                i_go_right(honnan, meddig);

                }
                else if (ballra_mehet)
                {
                i_go_left(honnan, meddig);
                    
                }
                else
                {
                    MessageBox.Show("Ide nem helyezhető hajó");
                }
            }
        

        private void Felfele(int honnan, int meddig)
        {

             felfele_mehet = true;
             lefele_mehet = true;

            if ((int)honnan / 10 - meddig >= 0)
            {

                for (int i = honnan; (int)i / 10 > (int)honnan / 10 - meddig; i -= 10)
                {
                    if (fields[i].Tag.ToString() == "ship")
                    {
                        felfele_mehet = false;
                        break;
                    }
                }

            }
            else {

                felfele_mehet = false;
            }
            if ((int)honnan / 10 + meddig <= 9)
            {

                for (int i = honnan; (int)i / 10 < (int)honnan / 10 + meddig; i += 10)
                {
                    if (fields[i].Tag.ToString() == "ship")
                    {
                        lefele_mehet = false;
                        break;
                    }
                }

            }
            else {
                lefele_mehet = false;
            }


            if (felfele_mehet)
            {
                for (int i = honnan; (int)i/10 > (int)honnan / 10 - meddig; i -= 10)
                {
                    fields[i].Background = new SolidColorBrush(Colors.Green);
                    fields[i].Tag = "ship";
                }


                foreach (var ship in ships)
                {
                    if (ship.Name.ToString() == selected_ship)
                    {
                        if (lastship != null) 
                        lastship.Stroke = unselected;
                        lastship = null;
                        ship.IsEnabled = false;
                        ship.Opacity = 0.5;
                        selected_ship = null;
                        break;
                    }
                }

                
            }
            else if (lefele_mehet)
            {
                for (int i = honnan; (int)i/10 < (int)honnan / 10 + meddig; i += 10)
                {
                    fields[i].Background = new SolidColorBrush(Colors.Green);
                    fields[i].Tag = "ship";
                }
                
                foreach (var ship in ships)
                {
                    if (ship.Name.ToString() == selected_ship)
                    {
                        if (lastship != null) 
                        lastship.Stroke = unselected;
                        lastship = null;
                        ship.IsEnabled = false;
                        ship.Opacity = 0.5;
                        selected_ship = null;
                        break;
                    }
                }

                

            }
            else
            {
                MessageBox.Show("Ide nem helyezhető hajó");
            }
        }

        private void Lefele(int honnan, int meddig)
        {

            felfele_mehet = true;
            lefele_mehet = true;

            if ((int)honnan / 10 + meddig <= 9)
            {

                for (int i = honnan; (int)i / 10 < (int)honnan / 10 + meddig; i += 10)
                {
                    if (fields[i].Tag.ToString() == "ship")
                    {
                        lefele_mehet = false;
                        break;
                    }
                }

            }
            else
            {
                lefele_mehet = false;
            }

            if ((int)honnan / 10 - meddig >= 0)
            {

                for (int i = honnan; (int)i / 10 > (int)honnan / 10 - meddig; i -= 10)
                {
                    if (fields[i].Tag.ToString() == "ship")
                    {
                        felfele_mehet = false;
                        break;
                    }
                }

            }
            else
            {

                felfele_mehet = false;
            }



            if (lefele_mehet)
            {

                for (int i = honnan; (int)i / 10 < (int)honnan / 10 + meddig; i += 10)
                {
                    fields[i].Background = new SolidColorBrush(Colors.Green);
                    fields[i].Tag = "ship";
                }

                foreach (var ship in ships)
                {
                    if (ship.Name.ToString() == selected_ship)
                    {
                        if (lastship != null)
                            lastship.Stroke = unselected;
                        lastship = null;
                        ship.IsEnabled = false;
                        ship.Opacity = 0.5;
                        selected_ship = null;
                        break;
                    }
                }

            }
            else if (felfele_mehet)
            {

                {
                    for (int i = honnan; (int)i / 10 > (int)honnan / 10 - meddig; i -= 10)
                    {
                        fields[i].Background = new SolidColorBrush(Colors.Green);
                        fields[i].Tag = "ship";
                    }


                    foreach (var ship in ships)
                    {
                        if (ship.Name.ToString() == selected_ship)
                        {
                            if (lastship != null)
                                lastship.Stroke = unselected;
                            lastship = null;
                            ship.IsEnabled = false;
                            ship.Opacity = 0.5;
                            selected_ship = null;
                            break;
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Ide nem helyezhető hajó");
            }


        }

        private void Random_button(object sender, RoutedEventArgs e)
        {
            foreach (var ship in ships) {
                if (placed_ship[ship.Name.ToString()] == 0) {
                    placed_ship[ship.Name.ToString()] = 1;
                    selected_ship = ship.Name;


                }
            
            }
        }
    }
}
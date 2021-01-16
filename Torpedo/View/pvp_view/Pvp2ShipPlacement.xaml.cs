using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using System.Xml.Serialization;
using Torpedo.Modell.Multi_modell;

namespace Torpedo.View.pvp_view
{
    /// <summary>
    /// Interaction logic for Pvp2ShipPlacement.xaml
    /// </summary>
    public partial class Pvp2ShipPlacement : Window
    {
        bool ballra_mehet = true;
        bool jobbra_mehet = true;
        bool felfele_mehet = true;
        bool lefele_mehet = true;
        int manual_counter = 0;
        bool contains = false;
        String irany { get; set; }
        String selected_ship { get; set; }
        List<Ship> ships_class = new List<Ship>();
        List<Ship> player1Ships = new List<Ship>();
        //public Ship[] ships_class;
        Path[] ships;
        Polygon[] arrows;
        Path lastship;
        Polygon lastarrow;
        Dictionary<string, int> placed_ship = new Dictionary<string, int>();
        String name, player1Name;
        SolidColorBrush unselected = new SolidColorBrush(Colors.Black);
        SolidColorBrush selected = new SolidColorBrush(Colors.Green);
        Grid[] fields, player1Fields;

        public Pvp2ShipPlacement(ref Grid[] player1Fields, List<Ship> player1Ships, string player1Name, string player2Name)
        {
            this.name = player2Name;
            this.player1Name = player1Name;
            this.player1Fields = player1Fields;
            this.player1Ships = player1Ships;
            InitializeComponent();
            string tb = player2Name + "'s ship placement";
            Who.Text = tb;
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

            ships = new Path[] { cruiser, submarine, battleship, destroyer, carrier };
            arrows = new Polygon[] { fel, le, bal, jobb };

            set_ships();
            set_Tags();
        }

        private void set_ships()
        {
            foreach (var ship in ships)
            {
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
            Path myship = (Path)sender;

            if (lastship != null && lastship != myship)
            {
                lastship.Stroke = unselected;
            }

            lastship = myship;
            selected_ship = set_selected_ship(myship);
        }

        private string set_selected_ship(Path myship)
        {
            switch (myship.Name)
            {
                case "destroyer":
                    myship.Stroke = selected;
                    return "destroyer";
                case "cruiser":
                    myship.Stroke = selected;
                    return "cruiser";
                case "submarine":
                    myship.Stroke = selected;
                    return "submarine";
                case "battleship":
                    myship.Stroke = selected;
                    return "battleship";
                case "carrier":
                    myship.Stroke = selected;
                    return "carrier";
            }
            return "";
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
                irany = Set_irany(mypoly);
            }
        }
        private string Set_irany(Polygon mypoly)
        {
            switch (mypoly.Name)
            {
                case "jobb":
                    mypoly.Stroke = selected;
                    return "jobb";
                case "bal":
                    mypoly.Stroke = selected;
                    return "bal";
                case "fel":
                    mypoly.Stroke = selected;
                    return "fel";
                case "le":
                    mypoly.Stroke = selected;
                    return "le";
            }
            return "";
        }

        private void set_default_ships()
        {
            foreach (var ship in ships)
            {
                ship.IsEnabled = true;
                ship.Stroke = unselected;
                ship.Opacity = 1;
            }
        }

        private void set_default_arrows()
        {
            foreach (var arrow in arrows)
            {
                arrow.Stroke = unselected;
            }
        }


        private void Reset_clicked(object sender, RoutedEventArgs e)
        {
            if (lastarrow != null)
                lastarrow.Stroke = unselected;
            if (lastship != null)
                lastship.Stroke = unselected;

            set_dictionary_default();
            set_default_ships();
            set_default_arrows();
            set_Tags();
            selected_ship = null;
            lastarrow = null;
            ships_class = new List<Ship>();
            manual_counter = 0;
        }

        public int check_ship(string name)
        {
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


        private int set_index(Grid clicked_grid)
        {
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

        private void call_my_function(string irany, int index, int size)
        {
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

        private void msg(string szoveg)
        {
            MessageBox.Show(szoveg, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public bool check_if_i_can_go_left(int honnan, int meddig)
        {
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

        public bool check_if_i_can_go_right(int honnan, int meddig)
        {
            if (honnan % 10 + meddig <= 9)
            {
                for (int i = honnan; i < honnan + meddig; i++)
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

        public void i_go_left(int honnan, int meddig)
        {
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

        public void i_go_right(int honnan, int meddig)
        {
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
            jobbra_mehet = check_if_i_can_go_right(honnan, meddig);
            ballra_mehet = check_if_i_can_go_left(honnan, meddig);

            if (ballra_mehet)
            {
                foreach (Ship p in ships_class)
                {
                    if (p.shipName == selected_ship)
                        contains = true;
                }
                if (!contains)
                {
                    ships_class.Add(new Ship(selected_ship, honnan, ship_ending("bal", honnan, check_ship(selected_ship)), "bal", false));
                }

                i_go_left(honnan, meddig);
                manual_counter++;

            }
            else if (jobbra_mehet)
            {
                foreach (Ship p in ships_class)
                {
                    if (p.shipName == selected_ship)
                        contains = true;
                }
                if (!contains)
                {
                    ships_class.Add(new Ship(selected_ship, honnan, ship_ending("jobb", honnan, check_ship(selected_ship)), "jobb", false));
                }
                i_go_right(honnan, meddig);

                manual_counter++;
            }

            else
            {
                msg("Ide nem helyezhető hajó");
            }
        }

        private void Jobbra(int honnan, int meddig)
        {
            jobbra_mehet = check_if_i_can_go_right(honnan, meddig);
            ballra_mehet = check_if_i_can_go_left(honnan, meddig);

            if (jobbra_mehet)
            {
                foreach (Ship p in ships_class)
                {
                    if (p.shipName == selected_ship)
                        contains = true;
                }
                if (!contains)
                {
                    ships_class.Add(new Ship(selected_ship, honnan, ship_ending("jobb", honnan, check_ship(selected_ship)), "jobb", false));
                }

                i_go_right(honnan, meddig);
                manual_counter++;

            }
            else if (ballra_mehet)
            {
                foreach (Ship p in ships_class)
                {
                    if (p.shipName == selected_ship)
                        contains = true;
                }
                if (!contains)
                {
                    ships_class.Add(new Ship(selected_ship, honnan, ship_ending("bal", honnan, check_ship(selected_ship)), "bal", false));
                }
                i_go_left(honnan, meddig);
                manual_counter++;
            }
            else
            {
                msg("Ide nem helyezhető hajó");
            }
        }



        public bool check_if_i_can_go_up(int honnan, int meddig)
        {
            if ((int)honnan / 10 - meddig >= 0)
            {
                for (int i = honnan; (int)i / 10 > (int)honnan / 10 - meddig; i -= 10)
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

        public bool check_if_i_can_go_down(int honnan, int meddig)
        {
            if ((int)honnan / 10 + meddig <= 9)
            {
                for (int i = honnan; (int)i / 10 < (int)honnan / 10 + meddig; i += 10)
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

        public void i_go_up(int honnan, int meddig)
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

        public void i_go_down(int honnan, int meddig)
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


        private void Felfele(int honnan, int meddig)
        {
            felfele_mehet = check_if_i_can_go_up(honnan, meddig);
            lefele_mehet = check_if_i_can_go_down(honnan, meddig);

            if (felfele_mehet)
            {
                foreach (Ship p in ships_class)
                {
                    if (p.shipName == selected_ship)
                        contains = true;
                }
                if (!contains)
                {
                    ships_class.Add(new Ship(selected_ship, honnan, ship_ending(irany, honnan, check_ship(selected_ship)), "fel", false));
                }

                i_go_up(honnan, meddig);
                manual_counter++;
            }
            else if (lefele_mehet)
            {
                foreach (Ship p in ships_class)
                {
                    if (p.shipName == selected_ship)
                        contains = true;
                }
                if (!contains)
                {
                    ships_class.Add(new Ship(selected_ship, honnan, ship_ending("le", honnan, check_ship(selected_ship)), "le", false));
                }
                i_go_down(honnan, meddig);
                manual_counter++;
            }
            else
            {
                msg("Ide nem helyezhető hajó");
            }
        }

        private void Lefele(int honnan, int meddig)
        {
            felfele_mehet = check_if_i_can_go_up(honnan, meddig);
            lefele_mehet = check_if_i_can_go_down(honnan, meddig);
            if (lefele_mehet)
            {
                foreach (Ship p in ships_class)
                {
                    if (p.shipName == selected_ship)
                        contains = true;
                }
                if (!contains)
                {
                    ships_class.Add(new Ship(selected_ship, honnan, ship_ending("le", honnan, check_ship(selected_ship)), "le", false));
                }
                i_go_down(honnan, meddig);
                manual_counter++;
            }
            else if (felfele_mehet)
            {
                foreach (Ship p in ships_class)
                {
                    if (p.shipName == selected_ship)
                        contains = true;
                }
                if (!contains)
                {
                    ships_class.Add(new Ship(selected_ship, honnan, ship_ending("le", honnan, check_ship(selected_ship)), "le", false));
                }

                manual_counter++;
                i_go_up(honnan, meddig);
            }
            else
            {
                msg("Ide nem helyezhető hajó");
            }
        }

        private int ship_ending(String irany, int kezdet, int hossz)
        {
            switch (irany)
            {
                case "fel": return kezdet - ((hossz - 1) * 10);
                case "le": return kezdet + ((hossz - 1) * 10);
                case "bal": return kezdet - (hossz - 1);
                case "jobb": return kezdet + (hossz - 1);
            }
            return 0;
        }

        private void grid_pressed(object sender, MouseButtonEventArgs e)
        {
            Grid clicked_grid = (Grid)sender;
            if (selected_ship == null)
            {
                msg("Válassz ki egy hajót");
                return;
            }
            int size = check_ship(selected_ship);
            int index = set_index(clicked_grid);
            call_my_function(irany, index, size);
        }


        private void Random_button(object sender, RoutedEventArgs e)
        {
            Reset_clicked(sender, e);
            foreach (var ship in ships)
            {
                set_default_arrows();
                if (placed_ship[ship.Name.ToString()] == 0)
                {
                    selected_ship = ship.Name;
                    irany = set_random_irany(get_random_number(1, 4));
                    lastarrow = get_poly(irany);
                    while (placed_ship[ship.Name.ToString()] != 1)
                    {
                        int random_grid = get_random_number(0, 99);
                        if (get_can_i_go_to_irany(irany, random_grid, check_ship(selected_ship)))
                        {
                            ships_class.Add(new Ship(selected_ship, random_grid, ship_ending(irany, random_grid, check_ship(selected_ship)), irany, false));
                            call_my_function(irany, random_grid, check_ship(selected_ship));
                            placed_ship[ship.Name.ToString()] = 1;
                        }
                    }
                }
            }
        }

        private Polygon get_poly(string irany)
        {
            switch (irany)
            {
                case "fel": return arrows[0];
                case "le": return arrows[1];
                case "jobb": return arrows[3];
                case "bal": return arrows[2];
            }
            return arrows[0];
        }

        private bool get_can_i_go_to_irany(string irany, int honnan, int meddig)
        {
            switch (irany)
            {
                case "fel": return check_if_i_can_go_up(honnan, meddig);
                case "le": return check_if_i_can_go_down(honnan, meddig);
                case "jobb": return check_if_i_can_go_right(honnan, meddig);
                case "bal": return check_if_i_can_go_left(honnan, meddig);
            }
            return false;
        }


        private string set_random_irany(int random_number)
        {
            return get_random_irany(random_number);
        }

        private string get_random_irany(int number)
        {
            switch (number)
            {
                case 1:
                    arrows[3].Stroke = selected;
                    return "jobb";
                case 2:
                    arrows[2].Stroke = selected;
                    return "bal";
                case 3:
                    arrows[0].Stroke = selected;
                    return "fel";
                case 4:
                    arrows[1].Stroke = selected;
                    return "le";
            }
            return "";
        }

        private void set_dictionary_default()
        {
            foreach (var ship in ships)
            {
                placed_ship[ship.Name.ToString()] = 0;
            }
        }

        private int get_random_number(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max + 1);
        }

        private void NextButton(object sender, RoutedEventArgs e)
        {
            if (manual_counter != 5)
            {
                msg("Minden hajót el kell helyezni");
            }
            else
            {
                PvpGame pvpGame = new PvpGame(ref player1Fields, ref fields, player1Ships, ships_class, player1Name, name);
                this.Close();
                pvpGame.Show();
            }
        }
    }
}


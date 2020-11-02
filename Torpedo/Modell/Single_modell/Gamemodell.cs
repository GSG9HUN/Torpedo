using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;
using Torpedo.View.single_view;

namespace Torpedo.Modell.Single_modell
{
    class Gamemodell : check_if,i_go
    {
        private enum Irany { fel, le, bal, jobb }
        Dictionary<String, int> placed_ships;
        Dictionary<String, bool> i_have_to_pick;
        int ship_sank_counter = 0;

        String[] ships;
        SolidColorBrush miss = new SolidColorBrush(Colors.Gray);
        SolidColorBrush direct_hit = new SolidColorBrush(Colors.Red);
        bool last_tip_was_hit = false;
        int last_tip_position = 0, temp_last_tip_pos=0;
        Grid[] AI_grids { get; set; }
           Grid[] my_grids;
        Ship[] player_ships;
        public Ship[] ai_ships { get; set; }
        public Path[] en_flotam;
        public Gamemodell(ref Grid[] AI_grids, ref Grid[] my_grids, Ship[] player_ships, ref Path[] en_flotam)
        {
            this.AI_grids = AI_grids;
            this.my_grids = my_grids;
            this.en_flotam = en_flotam;
            ai_ships = new Ship[5];
            this.player_ships = player_ships;

            set_Dictionary();
            set_random_dict();
            set_ai_grids_tags();
            Randomize_for_Ai();

        }

        private void set_random_dict()
        {
            i_have_to_pick = new Dictionary<string, bool>();
            while (i_have_to_pick.Count != 4) {
                bool have = false;
                string p = get_random_irany(get_ranom_number(1,5));
                foreach (KeyValuePair<string, bool> entry in i_have_to_pick)
                {
                    if (entry.Key == p) {
                        have = true;
                    }
                    if (have) {
                        break;
                    }
                  
                }

                if (!have) {
                    i_have_to_pick.Add(p,true);
                
                }

            }
        }

        private int get_ranom_number(int min,int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max);
           
        }

        private void set_ai_grids_tags()
        {

            foreach (Grid p in AI_grids)
            {
                p.Tag = "empty";
            }

        }

        private void set_Dictionary()
        {
            ships = new string[5];
            ships[0] = "destroyer";
            ships[1] = "cruiser";
            ships[2] = "submarine";
            ships[3] = "battleship";
            ships[4] = "carrier";

            placed_ships = new Dictionary<string, int>();

            foreach (String p in ships)
            {
                placed_ships[p] = 0;

            }
        }

        private string get_random_irany(int number) {

            switch (number) {
                case 1: return "fel";
                case 2: return "le";
                case 3: return "jobb";
                case 4: return "bal";
                default: return "";


            }
        
        }


        private void function_caller(String irany, int honnan, int meddig, ref bool i_can_go_up, ref bool i_can_go_down, ref bool i_can_go_left,ref bool i_can_go_right) {
            switch (irany) {
                case "fel": i_can_go_up=  check_if_i_can_go_up(honnan,meddig); break;
                
                case "le": i_can_go_down=   check_if_i_can_go_down(honnan,meddig); break;
                
                case "jobb": i_can_go_right= check_if_i_can_go_right(honnan,meddig); break;
                
                case "bal": i_can_go_left=  check_if_i_can_go_left(honnan,meddig); break;
                
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
        public void Randomize_for_Ai()
        {
            int size;
            Random mezo = new Random();
            bool i_can_go_left = false;
            bool i_can_go_right = false;
            bool i_can_go_up = false;
            bool i_can_go_down = false;
            string irany;
            int honnan;
            int counter = 0;

            foreach (string s in ships) {
                size = check_ship(s);
                i_can_go_left = false;
                i_can_go_right = false;
                i_can_go_up = false;
                i_can_go_down = false;


                do {
                    honnan = mezo.Next(0,100);
                    irany = get_random_irany(mezo.Next(1, 5));

                    function_caller(irany, honnan, size, ref i_can_go_up, ref i_can_go_down, ref i_can_go_left, ref i_can_go_right);


                } while (!i_can_go_left && !i_can_go_right && !i_can_go_up && !i_can_go_down);

                if (i_can_go_left)
                {
                    i_go_left(honnan,size);
                    ai_ships[counter] = new Ship(s,irany, honnan, ship_ending(irany, honnan,size),false);

                }
                else if (i_can_go_right)
                {
                    i_go_right(honnan,size);
                    ai_ships[counter] = new Ship(s, irany, honnan, ship_ending(irany, honnan, size), false);
                }
                else if (i_can_go_up)
                {
                    i_go_up(honnan, size);
                    ai_ships[counter] = new Ship(s, irany, honnan, ship_ending(irany, honnan, size), false);
                }
                else if(i_can_go_down)
                {
                    i_go_down(honnan, size);
                    ai_ships[counter] = new Ship(s, irany, honnan, ship_ending(irany, honnan, size), false);
                }



                counter++;
            }
        }

        private void ship_sank() {

            foreach (Ship p in player_ships)
            {

                if (!p.elsulyedt)
                {
                    if (check_if_the_ship_sank(p.irany, p.kezdet, p.veg))
                    {
                        p.elsulyedt = true;
                        ship_sank_counter++;
                        for (int i = 0; i < 5; i++)
                        {

                            if (en_flotam[i].Name.ToString() == p.nev)
                            {
                                en_flotam[i].Stroke = Brushes.Red;
                                en_flotam[i].Opacity = 0.5;
                                en_flotam[i].IsEnabled = false;

                                msg(p.nev + "elsülyedt");
                                last_tip_was_hit = false;
                                if (ship_sank_counter == 5)
                                {
                                    msg("Sajnos Vesztettél!");

                                    //ha vesztett meghív egy kis ablakot
                                }
                            }
                        }
                    }
                }

            }

        }

        private bool checker(string irany) {

            switch (irany) {
                case "fel": return check_if_i_have_grid_above(last_tip_position);
                case "le": return check_if_i_have_grid_under(last_tip_position);
                case "bal": return check_if_i_have_grid_next_left(last_tip_position);
                case "jobb": return check_if_i_have_grid_next_right(last_tip_position);

            }

            return false;
        }

        private bool check_if_i_have_grid_next_left(int last_tip)
        {
            if (last_tip % 10 == 0)
            {
                return false;
            }
           temp_last_tip_pos--;
            return true;
        }

        private bool check_if_i_have_grid_next_right(int last_tip)
        {
            if (last_tip % 10 == 9) {
                return false;
            }
            temp_last_tip_pos++;
            return true;
        }

        private bool check_if_i_have_grid_under(int last_tip)
        {
            if (last_tip > 89)
            {
                return false;
            }
            temp_last_tip_pos += 10;
            return true;
        }

        private bool check_if_i_have_grid_above(int last_tip)
        {
            if (last_tip <= 9)
            {
                return false;
            }
                temp_last_tip_pos -= 10;
                return true;
            
            
        }

        internal void ai_tip(Game.AI_delegate hit_delegate, Game.AI_delegate miss_delegate)
        {
            Random random = new Random();
            int tip = random.Next(0, 100);
            bool empty_tip = false;
            int false_counter = 0;

            foreach (KeyValuePair<string, bool> iterator in i_have_to_pick.ToArray()) {
                if (!i_have_to_pick[iterator.Key]) {
                    false_counter++;
                }
            
            }

            if (false_counter == 4) {
                last_tip_was_hit = false;
                set_default();
            }



                if (last_tip_was_hit)
            {   
                

                foreach(KeyValuePair<string,bool> iterator in i_have_to_pick.ToArray())
                {
                               
                    if (iterator.Value) {
                        temp_last_tip_pos = last_tip_position;
                        if (checker(iterator.Key))
                        {

                            if (can_i_click_there(temp_last_tip_pos))
                            {
                                i_have_to_pick[iterator.Key] = false;
                                checker_tip(temp_last_tip_pos, ref empty_tip, miss_delegate, hit_delegate);

                                break;
                            }

                        }
                        else {
                            i_have_to_pick[iterator.Key] = false;
                        }
                    }
                
                
                }
          

            }
            else {

                do
                {
                    tip = random.Next(0, 100);
                    checker_tip(tip,ref empty_tip ,miss_delegate,hit_delegate);

                } while (empty_tip == false);
            }

        }

        private void set_default() {

            foreach (KeyValuePair<string, bool> it in i_have_to_pick.ToArray())
            {
                i_have_to_pick[it.Key] = true;
            }


            }

        private bool can_i_click_there(int last_tip_position)
        {
           
            if (my_grids[last_tip_position].Tag.ToString() == "Clicked")
                return false;
            return true;
        }

        private void checker_tip(int tip, ref bool empty_tip, Game.AI_delegate miss_delegate, Game.AI_delegate hit_delegate)
        {
            if (my_grids[tip].Tag.ToString() == "empty")
            {

                my_grids[tip].Background = miss;
                miss_delegate();
                my_grids[tip].Tag = "Clicked";
                empty_tip = true;
            }
            else if (my_grids[tip].Tag.ToString() == "ship")
            {
                my_grids[tip].Background = direct_hit;
                my_grids[tip].Tag = "Clicked";
                last_tip_was_hit = true;
                hit_delegate();
                empty_tip = true;
                last_tip_position = tip;
                ship_sank();
                set_default();
            }

        }

        bool fel(int honnan, int meddig)
        {

            for (int i = honnan; i >= meddig; i -= 10)
            {
                if (my_grids[i].Tag.ToString() == "ship")
                {
                    return false;
                }

            }
            return true;
        }
        bool le(int honnan, int meddig)
        {

            for (int i = honnan; i <= meddig; i += 10)
            {
                if (my_grids[i].Tag.ToString() == "ship")
                {
                    return false;
                }

            }
            return true;
        }

        bool bal(int honnan, int meddig)
        {

            for (int i = honnan; i >= meddig; i--)
            {
                if (my_grids[i].Tag.ToString() == "ship")
                {
                    return false;
                }

            }
            return true;
        }
        bool jobb(int honnan, int meddig)
        {

            for (int i = honnan; i <= meddig; i++)
            {
                if (my_grids[i].Tag.ToString() == "ship")
                {
                    return false;
                }

            }
            return true;
        }

        private bool check_if_the_ship_sank(string irany, int honnan, int meddig)
        {
            switch (irany)
            {
                case "fel": return fel(honnan, meddig);
                case "le": return le(honnan, meddig);
                case "jobb": return jobb(honnan, meddig);
                case "bal": return bal(honnan, meddig);

            }


            return false;
        }

        private void msg(string v)
        {
            MessageBox.Show(v,"Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public bool check_if_heres_ship( ref Grid clicked_grid)
        {

            if (clicked_grid.Tag.ToString() == "ship")
            {
                clicked_grid.Background = direct_hit;
                clicked_grid.Tag = "Clicked";
                return true;
                
            }
            else {

                clicked_grid.Background = miss;
                clicked_grid.Tag = "Clicked";
                return false;

            }
        }

        public int check_ship(String ship) {

            switch (ship) {

                case "destroyer": return 2;

                case "cruiser": return 3;

                case "submarine": return 3;

                case "battleship": return 4;

                case "carrier": return 5;

                default: return 0;
            }
        }

        public bool check_if_i_can_go_left(int honnan, int meddig)
        {
            if (honnan % 10 - meddig >= 0)
            {
                for (int i = honnan; i > honnan - meddig; i--)
                {
                    if (AI_grids[i].Tag.ToString() == "ship")
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
                    if (AI_grids[i].Tag.ToString() == "ship")
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

        public bool check_if_i_can_go_up(int honnan, int meddig)
        {

            if ((int)honnan / 10 - meddig >= 0)
            {

                for (int i = honnan; (int)i / 10 > (int)honnan / 10 - meddig; i -= 10)
                {
                    if (AI_grids[i].Tag.ToString() == "ship")
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
                    if (AI_grids[i].Tag.ToString() == "ship")
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
                AI_grids[i].Background = new SolidColorBrush(Colors.Green);
                AI_grids[i].Tag = "ship";
            }

        }

        public void i_go_right(int honnan, int meddig)
        {

            for (int i = honnan; i < honnan + meddig; i++)
            {
                AI_grids[i].Background = new SolidColorBrush(Colors.Green);
                AI_grids[i].Tag = "ship";
            }


        }


        public void i_go_up(int honnan, int meddig)
        {
            for (int i = honnan; (int)i / 10 > (int)honnan / 10 - meddig; i -= 10)
            {
                AI_grids[i].Background = new SolidColorBrush(Colors.Green);
                AI_grids[i].Tag = "ship";
            }


         

        }

        public void i_go_down(int honnan, int meddig)
        {

            for (int i = honnan; (int)i / 10 < (int)honnan / 10 + meddig; i += 10)
            {
                AI_grids[i].Background = new SolidColorBrush(Colors.Green);
                AI_grids[i].Tag = "ship";
            }

        }
    }
}

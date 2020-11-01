using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace Torpedo.Modell.Single_modell
{
    class Gamemodell
    {
        private enum Irany { fel, le, bal, jobb }
        Dictionary<String, int> placed_ships;
        String[] ships;
        SolidColorBrush miss = new SolidColorBrush(Colors.Black);
        SolidColorBrush direct_hit = new SolidColorBrush(Colors.Green);
        Grid[] AI_grids;
        public Gamemodell(ref Grid[] AI_grids, View.single_view.Game game)
        {
            this.AI_grids = AI_grids;
            set_Dictionary();
            set_ai_grids_tags();
            Randomize_for_Ai();

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
                }
                else if (i_can_go_right)
                {
                    i_go_right(honnan,size);
                }
                else if (i_can_go_up)
                {
                    i_go_up(honnan, size);
                }
                else if(i_can_go_down)
                {
                    i_go_down(honnan, size);
                }

            
            
            
            }
        }

        private int check_ship(String ship) {

            switch (ship) {

                case "destroyer": return 2;

                case "cruiser": return 3;

                case "submarine": return 3;

                case "battleship": return 4;

                case "carrier": return 5;

                default: return 0;
            }
        }

        private bool check_if_i_can_go_left(int honnan, int meddig)
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

        private bool check_if_i_can_go_right(int honnan, int meddig)
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

        private bool check_if_i_can_go_up(int honnan, int meddig)
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

        private bool check_if_i_can_go_down(int honnan, int meddig)
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


        private void i_go_left(int honnan, int meddig)
        {
            for (int i = honnan; i > honnan - meddig; i--)
            {
                AI_grids[i].Background = new SolidColorBrush(Colors.Green);
                AI_grids[i].Tag = "ship";
            }

        }

        private void i_go_right(int honnan, int meddig)
        {

            for (int i = honnan; i < honnan + meddig; i++)
            {
                AI_grids[i].Background = new SolidColorBrush(Colors.Green);
                AI_grids[i].Tag = "ship";
            }


        }


        private void i_go_up(int honnan, int meddig)
        {
            for (int i = honnan; (int)i / 10 > (int)honnan / 10 - meddig; i -= 10)
            {
                AI_grids[i].Background = new SolidColorBrush(Colors.Green);
                AI_grids[i].Tag = "ship";
            }


         

        }

        private void i_go_down(int honnan, int meddig)
        {

            for (int i = honnan; (int)i / 10 < (int)honnan / 10 + meddig; i += 10)
            {
                AI_grids[i].Background = new SolidColorBrush(Colors.Green);
                AI_grids[i].Tag = "ship";
            }

        }
    }
}

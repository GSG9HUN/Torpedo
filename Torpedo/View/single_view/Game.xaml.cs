using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Torpedo.Modell.Single_modell;

namespace Torpedo.View.single_view
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {

        Grid[] ai_grids;
        Grid[]my_grids;
        Gamemodell game;
        List<Ship> player_ships;
        Path[] en_flotam, gep_flotaja;
        int ship_sank_counter = 0;
        bool timer_started = false;
        static TextBlock  timer;
        static TextBlock ai_miss_tb, ai_hit_tb;
        static DispatcherTimer atimer;

        public bool first_key_pressed { get; set; }
        public String username { get; set; }


        public delegate void AI_delegate();
        AI_delegate miss = set_AI_miss_textblock;
        AI_delegate hit = set_AI_hit_textblock;
        Random random = new Random();

        public Game(ref Grid[] fields, List<Ship> ships_class,String name)
        {
            player_ships = ships_class;
            first_key_pressed = false;
            InitializeComponent();
            username = name;
            set_my_grids(fields);
            set_ai_grids();
            set_flottak();
            game = new Gamemodell(ref ai_grids, ref my_grids, player_ships,ref en_flotam,this);
            set_labels();
            if (FileWriter.list_adatok == null)
            {
                FileWriter.ReadFromJSON();
            }
         

            if (random.Next(0, 2) == 1)
            {
                game.ai_tip(hit, miss, username);
            }
            

        }


        private void set_player_miss_textblock() {

            int p = Int32.Parse(player_miss.Text);
            p++;
            player_miss.Text = p.ToString();
        
        }

        private void set_player_hit_textblock()
        {
            int p = Int32.Parse(player_hit.Text);
            p++;
            player_hit.Text = p.ToString();
        }

        private static void SetTimer()
        {

            atimer = new DispatcherTimer();
            atimer.Interval = TimeSpan.FromSeconds(1);
            atimer.IsEnabled = true;
            atimer.Tick += set_timert_textblock;
        }

        private static void set_timert_textblock(object sender, EventArgs e)
        {
          
                int counter = Int32.Parse(timer.Text);
                counter++;
                timer.Text = counter.ToString();
           
        }

        private void set_labels()
        {
            ai_hit_tb = ai_hit;
            ai_miss_tb = ai_miss;
            timer = Timer_counter;
            
        }

        private void set_flottak()
        {
            en_flotam = new Path[]  { cruiser, submarine, battleship, destroyer, carrier };
            gep_flotaja = new Path[] { cruiser1, submarine1, battleship1, destroyer1, carrier1 };

           

        }

        private void set_ai_grids()
        {
            ai_grids = new Grid[] { A1, A2, A3, A4, A5, A6, A7, A8, A9, A10,
                                    B1, B2, B3, B4, B5, B6, B7, B8, B9, B10,
                                    C1, C2, C3, C4, C5, C6, C7, C8, C9, C10,
                                    D1, D2, D3, D4, D5, D6, D7, D8, D9, D10,
                                    E1, E2, E3, E4, E5, E6, E7, E8, E9, E10,
                                    F1, F2, F3, F4, F5, F6, F7, F8, F9, F10,
                                    G1, G2, G3, G4, G5, G6, G7, G8, G9, G10,
                                    H1, H2, H3, H4, H5, H6, H7, H8, H9, H10,
                                    I1, I2, I3, I4, I5, I6, I7, I8, I9, I10,
                                    J1, J2, J3, J4, J5, J6, J7, J8, J9, J10};
        }
        private void set_my_grids(Grid[] placed_ships_on_grids) {
            my_grids = new Grid[] { gridA1, gridA2, gridA3, gridA4, gridA5, gridA6, gridA7, gridA8, gridA9, gridA10,
                                    gridB1, gridB2, gridB3, gridB4, gridB5, gridB6, gridB7, gridB8, gridB9, gridB10,
                                    gridC1, gridC2, gridC3, gridC4, gridC5, gridC6, gridC7, gridC8, gridC9, gridC10,
                                    gridD1, gridD2, gridD3, gridD4, gridD5, gridD6, gridD7, gridD8, gridD9, gridD10,
                                    gridE1, gridE2, gridE3, gridE4, gridE5, gridE6, gridE7, gridE8, gridE9, gridE10,
                                    gridF1, gridF2, gridF3, gridF4, gridF5, gridF6, gridF7, gridF8, gridF9, gridF10,
                                    gridG1, gridG2, gridG3, gridG4, gridG5, gridG6, gridG7, gridG8, gridG9, gridG10,
                                    gridH1, gridH2, gridH3, gridH4, gridH5, gridH6, gridH7, gridH8, gridH9, gridH10,
                                    gridI1, gridI2, gridI3, gridI4, gridI5, gridI6, gridI7, gridI8, gridI9, gridI10,
                                    gridJ1, gridJ2, gridJ3, gridJ4, gridJ5, gridJ6, gridJ7, gridJ8, gridJ9, gridJ10};
            int index = 0;
            foreach (Grid p in placed_ships_on_grids) {
                if (p.Tag.ToString() != "empty")
                {
                    my_grids[index].Tag = "ship";
                    my_grids[index].Background = Brushes.Green;
                }
                else
                {
                    my_grids[index].Tag = "empty";
                }
                index++;
            }


        }

        bool fel(int honnan,int meddig) {

            for(int i=honnan; i >= meddig; i -= 10)
            {
                if (ai_grids[i].Tag.ToString()=="ship") {
                    return false;                
                }

            }
            return true;
        }
        bool le(int honnan, int meddig)
        {

            for (int i = honnan; i <= meddig; i += 10)
            {
                if (ai_grids[i].Tag.ToString() == "ship")
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
                if (ai_grids[i].Tag.ToString() == "ship")
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
                if (ai_grids[i].Tag.ToString() == "ship")
                {
                    return false;
                }

            }
            return true;
        }


        public bool check_if_the_ship_sank(string irany, int honnan, int meddig){

            switch (irany) {
                case "fel":  return fel(honnan,meddig);
                case "le":   return le(honnan,meddig);
                case "jobb": return jobb(honnan,meddig);
                case "bal":  return bal(honnan,meddig);
            
            }


            return false;
        }

        private void grid_pressed(object sender, MouseButtonEventArgs e)
        {

            if (!timer_started) {
                SetTimer();
                timer_started = !timer_started;
            }

            Grid clicked_grid = (Grid)sender;
            
            if (clicked_grid.Tag.ToString() != "Clicked")
            {
                if (game.check_if_heres_ship(ref clicked_grid))
                {

                    set_player_hit_textblock();

                    foreach (Ship p in game.ai_ships)
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
                                        gep_flotaja[i].Stroke = Brushes.Red;
                                        gep_flotaja[i].Opacity = 0.5;
                                        gep_flotaja[i].IsEnabled = false;

                                        msg(p.nev + " elsülyedt");
                                        if (ship_sank_counter == 5)
                                        {
                                            atimer.Stop();
                                            msg("Gratulálok nyertél!");
                                            Datas adatok = new Datas(username,"Győzőtt","AI","Vesztett");
                                            FileWriter.WriteToJSON(adatok);
                                            new Winner(this).Show();
                                            grids_disable();
                                            return;
                                        }
                                    }
                                }

                            }


                        }


                    }


                }
                else {
                    set_player_miss_textblock();
                
                }

                game.ai_tip(hit,miss,username);
            }
            else {
                msg("Ide már klikkeltél egyszer");
            
            }
           

        }

        public void popup_loser_window()
        {
            Loser loser = new Loser(this);
            loser.Show();
        }


        private void grids_disable() {
            foreach (Grid q in ai_grids)
            {
                q.IsEnabled = false;
            }
        }

        public  static void set_AI_miss_textblock()
        {

            int p = Int32.Parse(ai_miss_tb.Text);
            p++;
            ai_miss_tb.Text = p.ToString();

        }

        public static void set_AI_hit_textblock()
        {
            int p = Int32.Parse(ai_hit_tb.Text);
            p++;
            ai_hit_tb.Text = p.ToString();
        }


        private void Cheating(object sender, KeyEventArgs e)
        {

            if (e.Key.ToString()==Key.LeftCtrl.ToString())
            {
                first_key_pressed = true;
                return;
            }
            if (first_key_pressed) {
                first_key_pressed = false;
                if (e.Key.ToString()==Key.C.ToString()) {
                    foreach (Grid p in ai_grids)
                        if (p.Tag.ToString() == "ship") {
                            p.Background = Brushes.Green;
                        }
                }
            
            }

        }

        private void msg(string szoveg)
        {

            MessageBox.Show(szoveg, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

    }

 
}

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
using Torpedo.Modell.Multi_modell;

namespace Torpedo.View.pvp_view
{
    public partial class PvpGame : Window
    {
        Grid[] player1Grids, player2Grids;
        Grid[] player1EnemyView, player2EnemyView;
        PvpGamemodell game;
        List<Ship> player1Ships, player2Ships;
        Path[] player1Fleet, player2Fleet;
        int shipSankCounterByPlayer1 = 0, shipSankCounterByPlayer2 = 0;
        int currentPlayer = 1;
        bool isTimerStarted = false;
        static TextBlock timer;
        //static TextBlock player1_miss_tb, player1_hit_tb, player2_miss_tb, player2_hit_tb;
        static DispatcherTimer atimer;
        public String player1Name { get; set; }
        public String player2Name { get; set; }

        public PvpGame(ref Grid[] player1Fields, ref Grid[] player2Fields, List<Ship> player1Ships, List<Ship> player2Ships, String player1Name, String player2Name)
        {
            player1Ships = player1Ships;
            player2Ships = player2Ships;
            InitializeComponent();
            setPlayer1Grids(player1Fields);
            //setPlayer2Grids(player2Fields);
            //setPlayer1EnemyView();
            setPlayer2EnemyView();
            player1Name = player1Name;
            player2Name = player2Name;
            setFleets();
            game = new PvpGamemodell(ref player1Grids, ref player2Grids, player1Ships, player2Ships, ref player1Fleet, ref player2Fleet, this);
            setLabels();
            setPlayer1MovesTB();
            FileWriter.ReadFromJSON();
        }

        private static void SetTimer()
        {
            atimer = new DispatcherTimer();
            atimer.Interval = TimeSpan.FromSeconds(1);
            atimer.IsEnabled = true;
            atimer.Tick += setTimertTextblock;
        }

        private static void setTimertTextblock(object sender, EventArgs e)
        {
            int counter = Int32.Parse(timer.Text);
            counter++;
            timer.Text = counter.ToString();
        }

        private void setLabels()
        {
            timer = Timer_counter;
        }

        private void setFleets()
        {
            player1Fleet = new Path[] { cruiser1, submarine1, battleship1, destroyer1, carrier1 };
            player2Fleet = new Path[] { cruiser2, submarine2, battleship2, destroyer2, carrier2 };
        }

        private void setPlayer1Grids(Grid[] placedPlayer1ShipsOnGrids)
        {
            player1Grids = new Grid[] { gridA1, gridA2, gridA3, gridA4, gridA5, gridA6, gridA7, gridA8, gridA9, gridA10,
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
            foreach (Grid p in placedPlayer1ShipsOnGrids)
            {
                if (p.Tag.ToString() != "empty")
                {
                    player1Grids[index].Tag = "ship";
                    player1Grids[index].Background = Brushes.Green;
                }
                else
                {
                    player1Grids[index].Tag = "empty";
                }
                index++;
            }
        }

        private void setPlayer2Grids(Grid[] placedPlayer2ShipsOnGrids)
        {
            player2Grids = new Grid[] { gridA1, gridA2, gridA3, gridA4, gridA5, gridA6, gridA7, gridA8, gridA9, gridA10,
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
            foreach (Grid p in placedPlayer2ShipsOnGrids)
            {
                if (p.Tag.ToString() != "empty")
                {
                    player2Grids[index].Tag = "ship";
                    player2Grids[index].Background = Brushes.Green;
                }
                else
                {
                    player2Grids[index].Tag = "empty";
                }
                index++;
            }
        }

        private void setPlayer1EnemyView()
        {
            player1EnemyView = new Grid[] { A1, A2, A3, A4, A5, A6, A7, A8, A9, A10,
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

        private void setPlayer2EnemyView()
        {
            player2EnemyView = new Grid[] { A1, A2, A3, A4, A5, A6, A7, A8, A9, A10,
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



        bool fel(int honnan, int meddig)
        {
            if (currentPlayer == 1)
            {
                for (int i = honnan; i >= meddig; i -= 10)
                {
                    if (player1EnemyView[i].Tag.ToString() == "ship")
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                for (int i = honnan; i >= meddig; i -= 10)
                {
                    if (player2EnemyView[i].Tag.ToString() == "ship")
                    {
                        return false;
                    }
                }
                return true;
            }
            
        }

        bool le(int honnan, int meddig)
        {
            if (currentPlayer == 1)
            {
                for (int i = honnan; i <= meddig; i += 10)
                {
                    if (player1EnemyView[i].Tag.ToString() == "ship")
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                for (int i = honnan; i <= meddig; i += 10)
                {
                    if (player2EnemyView[i].Tag.ToString() == "ship")
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        bool bal(int honnan, int meddig)
        {
            if (currentPlayer == 1)
            {
                for (int i = honnan; i >= meddig; i--)
                {
                    if (player1EnemyView[i].Tag.ToString() == "ship")
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                for (int i = honnan; i >= meddig; i--)
                {
                    if (player2EnemyView[i].Tag.ToString() == "ship")
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        bool jobb(int honnan, int meddig)
        {
            if (currentPlayer == 1)
            {
                for (int i = honnan; i <= meddig; i++)
                {
                    if (player1EnemyView[i].Tag.ToString() == "ship")
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                for (int i = honnan; i <= meddig; i++)
                {
                    if (player2EnemyView[i].Tag.ToString() == "ship")
                    {
                        return false;
                    }
                }
                return true;
            }
        }


        public bool checkIfTheShipSank(string irany, int honnan, int meddig)
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


        private void grid_pressed(object sender, MouseButtonEventArgs e)
        {
            if (!isTimerStarted)
            {
                SetTimer();
                isTimerStarted = !isTimerStarted;
            }

            Grid clicked_grid = (Grid)sender;

            if(currentPlayer == 1)
            {
                if (clicked_grid.Tag.ToString() != "Clicked")
                {
                    if (game.checkIfHeresShip(ref clicked_grid))
                    {
                        setPlayer1HitTB();
                        foreach (Ship p in player2Ships)    //bruh
                        {
                            if (!p.isDestroyed)
                            {
                                if (checkIfTheShipSank(p.shipAlign, p.shipStart, p.shipEnd))
                                {
                                    p.isDestroyed = true;
                                    shipSankCounterByPlayer1++;
                                    for (int i = 0; i < 5; i++)
                                    {
                                        if (player1Fleet[i].Name.ToString() == p.shipName)
                                        {
                                            player2Fleet[i].Stroke = Brushes.Red;
                                            player2Fleet[i].Opacity = 0.5;
                                            player2Fleet[i].IsEnabled = false;

                                            msg(p.shipName + "elsülyedt");
                                            if (shipSankCounterByPlayer1 == 5)
                                            {
                                                atimer.Stop();
                                                msg(player1Name + " nyert!");
                                                //Datas adatok = new Datas(username, "Győzőtt");
                                                //FileWriter.WriteToJSON(adatok);
                                                new PvpWinner(this, player1Name).Show();
                                                gridsDisable();
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        currentPlayer = 2;
                    }
                    else
                    {
                        setPlayer1MissTB();
                    }
                    currentPlayer = 2;
                }
                else
                {
                    msg("Ide már klikkeltél egyszer!");
                }
            }
            else
            {
                ///////////////////////////////////////////////////////////////////////////////
            }
        }


        private void gridsDisable()
        {
            if (currentPlayer == 1)
            {
                foreach (Grid q in player1EnemyView)
                {
                    q.IsEnabled = false;
                }
            }
            else
            {
                foreach (Grid q in player2EnemyView)
                {
                    q.IsEnabled = false;
                }
            }
        }

        private void setPlayer1HitTB()
        {
            int p = Int32.Parse(player1Hit.Text);
            p++;
            player1Hit.Text = p.ToString();
        }

        private void setPlayer1MissTB()
        {
            int p = Int32.Parse(player1Miss.Text);
            p++;
            player1Miss.Text = p.ToString();
        }

        private void setPlayer2HitTB()
        {
            int p = Int32.Parse(player2Hit.Text);
            p++;
            player2Hit.Text = p.ToString();
        }

        private void setPlayer2MissTB()
        {
            int p = Int32.Parse(player2Miss.Text);
            p++;
            player2Miss.Text = p.ToString();
        }

        private void setPlayer1MovesTB()
        {
            playerMoves.Text = player1Name;
        }

        private void setPlayer2MovesTB()
        {
            playerMoves.Text = player2Name;
        }

        private void msg(string szoveg)
        {

            MessageBox.Show(szoveg, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

        }
    }

 
}

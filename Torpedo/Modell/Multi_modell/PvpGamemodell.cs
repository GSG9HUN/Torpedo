using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Torpedo.View.pvp_view;

namespace Torpedo.Modell.Multi_modell
{
    class PvpGamemodell
    {
        private enum shipAlign { fel, le, bal, jobb}
        Dictionary<String, int> placedShips;
        int shipSankCounterByPlayer1 = 0, shipSankCounterByPlayer2 = 0;
        String[] ships;
        SolidColorBrush miss = new SolidColorBrush(Colors.Gray);
        SolidColorBrush hit = new SolidColorBrush(Colors.Red);
        Grid[] player1Grids, player2Grids;
        List<Ship> player1Ships, player2Ships;
        PvpGame pvpGame;
        public Ship[] player2_Ships;
        public Ship[] player1_Ships;
        public Path[] player1Fleet, player2Fleet;

        public PvpGamemodell(ref Grid[] player1Grids, ref Grid[] player2Grids, List<Ship> player1Ships, List<Ship> player2Ships, ref Path[] player1Fleet, ref Path[] player2Fleet, PvpGame pvpGame)
        {
            this.player1Grids = player1Grids;
            this.player2Grids = player2Grids;
            this.player1Ships = player1Ships;
            this.player2Ships = player2Ships;
            this.player1Fleet = player1Fleet;
            this.player2Fleet = player2Fleet;
            this.pvpGame = pvpGame;
            set_Dictionary();
        }

        private void set_Dictionary()
        {
            ships = new string[5];
            ships[0] = "destroyer";
            ships[1] = "cruiser";
            ships[2] = "submarine";
            ships[3] = "battleship";
            ships[4] = "carrier";
            placedShips = new Dictionary<string, int>();

            foreach (String p in ships)
            {
                placedShips[p] = 0;
            }
        }

        public bool checkIfHeresShip(ref Grid clicked_grid)
        {
            if (clicked_grid.Tag.ToString() == "ship")
            {
                clicked_grid.Background = hit;
                clicked_grid.Tag = "Clicked";
                return true;
            }
            else
            {
                clicked_grid.Background = miss;
                clicked_grid.Tag = "Clicked";
                return false;
            }
        }
    }
}

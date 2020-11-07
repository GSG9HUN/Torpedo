using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Modell.Single_modell
{
    class Datas
    {
        String player1_name;
        String player2_name;
        String steps;
        String message;
        public Datas(string player1_name,string message,string player2_name,string steps) {
            this.player1_name = player1_name;
            this.player2_name = player2_name;
            this.steps = steps;
            this.message = message;
        }
    }
}

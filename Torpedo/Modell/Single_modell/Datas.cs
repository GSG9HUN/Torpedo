using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Modell.Single_modell
{
    class Datas
    {
        public String player_name;
        public String player_name2;
        public String message;
        public String message2;
        public Datas(string player_name,string message, string player_name2, string message2) {
            this.player_name = player_name;
            this.message = message;
            this.player_name2 = player_name2;
            this.message2 = message2;
        }
    }
}

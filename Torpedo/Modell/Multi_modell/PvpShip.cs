using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Modell.Multi_modell
{
    public class PvpShip
    {
        public String shipName { get; set; }

        public int shipStart { get; set; }

        public int shipEnd { get; set; }

        public String shipAlign { get; set; }

        public bool isDestroyed { get; set; }
        
        public PvpShip(String shipName, int shipStart, int shipEnd, String shipAlign, bool isDestroyed)
        {
            this.shipName = shipName;
            this.shipStart = shipStart;
            this.shipEnd = shipEnd;
            this.shipAlign = shipAlign;
            this.isDestroyed = isDestroyed;

        }
    }
}

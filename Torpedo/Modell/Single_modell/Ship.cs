using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Modell.Single_modell
{
    public class Ship
    {
        public String irany  { get; set; }

        public String nev { get; set; }
        
        public bool elsulyedt { get; set; }
        public int kezdet { get; set; }
        public int veg { get; set; }
        public Ship(String nev,String irany, int kezdet, int veg,bool elsulyedt)
        {
            this.nev = nev;
            this.irany = irany;
            this.kezdet = kezdet;
            this.veg = veg;
            this.elsulyedt = elsulyedt;

        }
    }
}

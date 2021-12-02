using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
     public class APIModel
    {
        public string userName { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public int handsPlayed { get; set; }
        
        public int blackjacks { get; set; }
        public int dealerBlackjacks { get; set; }
        public int busts { get; set; }

        public APIModel() { }

        public override string ToString()
        {
            return base.ToString();
        }




    }
}

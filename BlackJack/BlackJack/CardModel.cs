using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
     public class CardModel
    {
        public string userID { get; set; }
        public int wins { get; set; }
        public int winOnHit { get; set; }
        public int winOnStand { get; set; }
        public int loseOnStand { get; set; }
        public int losses { get; set; }
        public int handsPlayed { get; set; }
        public int blackjacks { get; set; }
        public int busts { get; set; }
        public int pushes { get; set; }

        public CardModel() { }

        public override string ToString()
        {
            return $"userID: {userID} \n Wins: {wins}\n Losses: {losses}\n";
        }




    }
}

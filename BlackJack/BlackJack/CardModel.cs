using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
     public class CardModel
    {
        public string userID { get; set; }
        public int wins { get; set; }
        
        public int losses { get; set; }
        public int handsPlayed { get; set; }
       
        public int busts { get; set; }
        public int pushes { get; set; }

        public CardModel() { }

        public override string ToString()
        {
            return $"userID: {userID} \n Wins: {wins}\n Losses: {losses}\n Hands Played: {handsPlayed}\n Busts: {busts}\n Pushes: {pushes}";
        }




    }
}

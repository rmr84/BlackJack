using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    class GameManager
    {
        public static readonly GameManager manager = new GameManager();
        public List<CardModel> list = new List<CardModel>();


        private GameManager() { }

        public static GameManager GetInstance()
        {
            return manager;
        }
    }
}

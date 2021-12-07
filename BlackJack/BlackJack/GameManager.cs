using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    class GameManager
    {
        public static readonly GameManager manager = new GameManager();
        public List<APIModel> list = new List<APIModel>();


        private GameManager() { }

        public static GameManager GetInstance()
        {
            return manager;
        }
    }
}

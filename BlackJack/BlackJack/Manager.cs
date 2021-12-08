using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BlackJack
{
    class Manager
    {
        public static readonly Manager manager = new Manager();
        public List<CardModel> list = new List<CardModel>();
        

        private Manager() { }

        public static Manager GetInstance()
        {
            return manager;
        }

        public List<CardModel> GetItems()
        {
            return list;
        }

        public ObservableCollection<CardModel> GetObsList()
        {
            return new ObservableCollection<CardModel>();
        }
    }
}

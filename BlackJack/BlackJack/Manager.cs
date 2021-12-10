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
        CardModel c = new CardModel();

        private Manager() { }

        public static Manager GetInstance()
        {
            return manager;
        }

        public void Add(CardModel cardItem)
        {
            list.Add(cardItem);
        }

        public bool Remove(CardModel cardItem)
        {
            list.Remove(cardItem);
            return true;
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

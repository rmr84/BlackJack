using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BlackJack
{
    class Manager
    {
        public static readonly Manager manager = new Manager();
        public List<APIModel> list = new List<APIModel>();
        

        private Manager() { }

        public static Manager GetInstance()
        {
            return manager;
        }

        public List<APIModel> GetItems()
        {
            return list;
        }

        public ObservableCollection<APIModel> GetObsList()
        {
            return new ObservableCollection<APIModel>();
        }
    }
}

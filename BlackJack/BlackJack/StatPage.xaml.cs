using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlackJack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatPage : ContentPage
    {
        private Manager manager = Manager.GetInstance();
        private bool user = true;
       // private int index;
        public StatPage(bool user)
        {
            InitializeComponent();
            this.user = user;
            
            var var1 = manager.GetObsList();
            //WelcomeUser.Text = var1.userName; // fix this 
        }

        private void Reset_Clicked(object sender, EventArgs e)
        {

        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new EditUser());
        }
    }
}
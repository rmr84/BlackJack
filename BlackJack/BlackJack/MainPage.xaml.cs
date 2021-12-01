using System;
using System.Linq;
using Xamarin.Forms;

using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
        }

       

            private async void NewGame_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage());
    
        }

        private async void ViewStats_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatPage());
        }

        private async void Tutorial_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TutorialPage());
        }
    }
}

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
        private Manager manager = Manager.GetInstance();
        private int deckC;
        public MainPage()
        {
            
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

        }

            private async void NewGame_Clicked(object sender, EventArgs e)
            {
            var check = CheckInput();
            if (!String.IsNullOrEmpty(check))
            {
                await DisplayAlert("Error", check, "Try again");
                return;
            }

                await Navigation.PushAsync(new GamePage());
            }
            
        private async void ViewStats_Clicked(object sender, EventArgs e)
        {
            var check = CheckInput();
            if (!String.IsNullOrEmpty(check))
            {
                await DisplayAlert("Error", check, "Try again");
                return;
            }

            await Navigation.PushAsync(new StatPage(true));
              
        }

        private async void Tutorial_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TutorialPage());
        }

        private String CheckInput()
        {
            StringBuilder b = new StringBuilder("");
            if (String.IsNullOrEmpty(userID.Text))
            {
                return "Please enter a userID first!";

            } else
            {
                return b.ToString();
            }
        }
    }
}

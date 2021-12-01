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

        //need to add public ICommand ClickCommand => new Command<string>((url) =>
        //{
        // Device.OpenUri(new System.Uri(url));
// }); somewhere??? 

       

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

using System;
using System.Linq;
using Xamarin.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace BlackJack
{
    public partial class MainPage : ContentPage
    {

        private Manager manager = Manager.GetInstance();
        
        public static readonly HttpClient client = new HttpClient();
        CardModel card = new CardModel();

        public MainPage()
        {
            InitializeComponent();

        }

        private async void NewGame_Clicked(object sender, EventArgs e)
        {
            var check = CheckInput();
            if (!String.IsNullOrEmpty(check))
            {
                await DisplayAlert("Error", check, "Try again");
                return;
                
            }
            var i = await Post(card);
            if (i == null)
            {
                i = card;
            }
            manager.Add(card);
            
            Console.WriteLine(card);
            

            await Navigation.PushAsync(new GamePage(card.userID));
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

            }
            else
            {
                return b.ToString();

            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri("https://responsibleplay.pa.gov/get-gambling-addiction-help/"));
        }

        // post 
        public async Task<CardModel> Post(CardModel userId)
        {

            userId.userID = userID.Text;
            var uri = new Uri("https://blackjackmobileapp.azurewebsites.net/User");
            String json = JsonConvert.SerializeObject(userId);
            StringContent strContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = new HttpResponseMessage();
            response = await client.PostAsync(uri, strContent);
            CardModel card = null;

            if (response.IsSuccessStatusCode)
            {
                
                var content = await response.Content.ReadAsStringAsync();
                card = JsonConvert.DeserializeObject<CardModel>(content);
                
            }
            return card;

        }

        public static async Task<CardModel> Put(CardModel item)
        {

            var var1 = Manager.GetInstance().list.Find(x => x.userID == item.userID);



            var uri = new Uri("https://blackjackmobileapp.azurewebsites.net/User/" + item.userID + "/");
           

            String json = JsonConvert.SerializeObject(var1);
            StringContent strContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Put;
            request.RequestUri = uri;
            request.Content = strContent;

            HttpResponseMessage response = await MainPage.client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();
                int statusCode = JsonConvert.DeserializeObject<int>(content);
            }
            return item;
        }



    }
}


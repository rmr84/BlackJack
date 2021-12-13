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
        bool showtext;
   

        public MainPage(bool onappear)
        {
            InitializeComponent();
            showtext = onappear;
           
        }

        protected override void OnAppearing()
        {
            if (showtext == true)
            {
                userID.Text = " ";
            }
            base.OnAppearing();
            
        }

        private async void NewGame_Clicked(object sender, EventArgs e)
        {
            ActivityIndicator i = new ActivityIndicator();

            var check = CheckInput();
            if (!String.IsNullOrEmpty(check))
            {
                await DisplayAlert("Error", check, "Try again");
                return;

            }

            var x = await StatPage.RefreshDataAsync(card);
           if ((x != null) && (x.userID == card.userID)) {
                card.userID = x.userID;
            }
    
             else
            {
                var y = await Post(card);
                card.userID = y.userID;
            }

            i.Color = Color.Red;
            IsBusy = true;
            i.IsEnabled = true;
            i.IsRunning = true;
            i.IsVisible = true;
       
            
            await Navigation.PushAsync(new GamePage(card.userID));
            i.IsEnabled = false;
            i.IsRunning = false;
            i.IsVisible = false;
            IsBusy = false;
        }

        private async void ViewStats_Clicked(object sender, EventArgs e)
        {
            try
            {
                var check = CheckInput();
                if (!String.IsNullOrEmpty(check))
                {
                    await DisplayAlert("Error", check, "Try again");
                    return;
                }

                await Navigation.PushAsync(new StatPage(true, card.userID));

            }
            catch (Exception ex)
            {
                Console.WriteLine("This is an alert that you are being alerted!");
            }
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

            //var var1 = Manager.GetInstance().list.Find(x => x.userID == item.userID);



            var uri = new Uri("https://blackjackmobileapp.azurewebsites.net/User/" + item.userID + "/");
           

            String json = JsonConvert.SerializeObject(item);
            StringContent strContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Put;
            request.RequestUri = uri;
            request.Content = strContent;

            HttpResponseMessage response = await client.SendAsync(request);
            CardModel card = null;
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    card = JsonConvert.DeserializeObject<CardModel>(content);
                    
                }
            }catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return card;
            
        }



    }
}


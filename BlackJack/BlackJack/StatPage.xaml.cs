using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlackJack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatPage : ContentPage
       

    {
        private string userID;
        public static readonly HttpClient client = new HttpClient();
        private Manager manager = Manager.GetInstance();
        CardModel card = new CardModel();

        


        public StatPage(bool isUser, String c)
        {
            card.userID = c;
            InitializeComponent();
            
            
        }

        protected override async void OnAppearing()
        {
            
            base.OnAppearing();
            try
            {
                if (card.userID == null)
                {
                    
                    IfEmpty.IsVisible = true;
                    User.IsVisible = false;
                    Wins.IsVisible = false;
                    Losses.IsVisible = false;
                    HandsPlayed.IsVisible = false;
                    Busts.IsVisible = false;
                    Pushes.IsVisible = false;
                    DeleteUser.IsVisible = false;


                }
                else
                {

                    var output = await RefreshDataAsync(card);
                    User.Text = "User: " + output.userID;
                    Wins.Text = "Wins: " + output.wins;
                    Losses.Text = "Losses: " + output.losses;
                    HandsPlayed.Text = "Hands Played: " + output.handsPlayed;
                    Busts.Text = "Busts: " + output.busts;
                    Pushes.Text = "Pushes: " + output.pushes;
                }
            }
            catch (Exception e)
            {
              
                    IfEmpty.IsVisible = true;
                    User.IsVisible = false;
                    Wins.IsVisible = false;
                    Losses.IsVisible = false;
                    HandsPlayed.IsVisible = false;
                    Busts.IsVisible = false;
                    Pushes.IsVisible = false;
                    DeleteUser.IsVisible = false;

                    Console.WriteLine("this is the warner exception");
            }
            
        }
           
        //get 

         public static async Task<CardModel> RefreshDataAsync(CardModel c)
        {
         var uri = new Uri("https://blackjackmobileapp.azurewebsites.net/User/" + c.userID);
         HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
         CardModel Item = null;
         if (response.IsSuccessStatusCode)
         {
         var content = await response.Content.ReadAsStringAsync();
          Item = JsonConvert.DeserializeObject<CardModel>(content);
        }
        return Item;
        }

        private async void DeleteUser_Clicked(object sender, EventArgs e)
        {
            await Delete(card);

            await Navigation.PopAsync(true);
        }

        public async Task Delete(CardModel user)
        {

            var uri = new Uri("https://blackjackmobileapp.azurewebsites.net/User/" + user.userID);
            String json = JsonConvert.SerializeObject(user);
            StringContent strContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = HttpMethod.Delete;
            request.RequestUri = uri;
            request.Content = strContent;
            CardModel card1 = null;
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                card1 = JsonConvert.DeserializeObject<CardModel>(content);

            }
        }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlackJack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUser : ContentPage
    {
        CardModel c = new CardModel();
        public static readonly HttpClient client = new HttpClient();
        private Manager manager = Manager.GetInstance();
        private int index;
        private bool put = false;
        ActivityIndicator Indicator = new ActivityIndicator();
        public EditUser(bool reset, int index, bool put)
        {
            InitializeComponent();
            this.index = index;
            this.put = put;

            Reset.IsVisible = reset;

            if (reset && index >= 0)
            {
                var var1 = manager.GetObsList()[index];
                UserId.Text = var1.userID;
            }

        }

        private void Reset_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;
            ActivityIndicator indicator = new ActivityIndicator();
            indicator.IsRunning = true;
            indicator.IsVisible = true;
            //await Reset(c)
            indicator.IsRunning = false;
            indicator.IsEnabled = false;
        }


        private async void Save_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;
            Indicator.IsEnabled = true;
            Indicator.IsRunning = true;
            Indicator.IsVisible = true;


        }



        //public async Task Put(CardModel cardItem) {
        // {
        // var var1 = manager.GetObsList()[index];

        //var uri = new Uri("https://.....");
        //CardModel put = new CardModel();

        //String json = JsonConvert.SerializeObjet(var1);
        //StringContent strContent = new StringContent(json, Encoding.UTF8, "application/json");

        //HttpRequestMessage request = new HttpRequestMessage();
        //request.Method = HttpMethod.Put;
        //request.RequestUri = uri; 
        //request.Content = strContent;

        //HttpResponseMessage response = await client.SendAsync(request); 
        // if (response.IsSuccessStatusCode)
        // {
        // IsBusy = true; 
        // var content = await response.Content.ReadAsStringAsync();
        // int statusCode = JsonConvert.DeserializeObject<int>(content);


    }




}


  
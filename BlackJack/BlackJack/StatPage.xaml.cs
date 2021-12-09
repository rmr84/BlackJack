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
        public static readonly HttpClient client = new HttpClient();
        private Manager manager = Manager.GetInstance();
        CardModel c = new CardModel();
       
        private int index;

       
        public StatPage(bool reset)
        {
            InitializeComponent();
            
            Reset.IsVisible = reset;
            
            
            if (reset && index >= 0)
            {
                var var1 = manager.GetObsList()[index];
                WelcomeUser.Text = var1.userID;

            }

            if (!Reset.IsVisible)
            {
                //await Post(c)
            }
        }

        private async void Reset_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;
            ActivityIndicator indicator = new ActivityIndicator();
            indicator.IsRunning = true;
            indicator.IsVisible = true;
            //await Reset(c)

        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new EditUser());
        }

        private void Upload_Clicked(object sender, EventArgs e)
        {

        }

        //get all

        // public async Task<List<APIModel>> RefreshDataAsync()
        // {
        // var uri = new Uri(https://.....);
        // HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
        //List<APiModel> Items = null;
        // if (response.IsSuccessStatusCode)
        // {
        //var content = await response.Content.ReadAsStringAsync();
        // Items = JsonConvert.DeserializeObject<List<APIModel>>(content);
        //}
        //return Items;
        //}

        //public async Task Reset(CardModel user)
        // {
        // var var1 = manager.getObsList()[index];
        //var uri = new Uri("https://.....);
        // String json = JsonConvert.SerializeObject(var1);
        //StringContent strContent = new StringContent(json, Endocing.UTF8, "application/json);

        //HttpRequestMessage request = new HttpRequestMessage();
        //request.Method = HttpMethod.Delete;
        //request.RequestUri = uri;
        //request.Content = strContent;

        //HttpResponseMessage response = await client.SendAsync(request);
        //if (response.IsSuccessStatusCode)
        // {
        // var content = await response.Content.ReadAsStringAsync();
        // int statuscode = JsonConvert.DeserializeObject<int>(content);

        // }



    }
}
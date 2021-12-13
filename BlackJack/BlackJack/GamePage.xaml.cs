using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xamarin.Forms;

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for Freestyle.xaml
    /// </summary>
    public partial class GamePage : ContentPage
    {
        CardModel c = new CardModel();
        private Game blackjackInstance;
        private string userID;
        private FlexLayout[] views;
        private Label[] totals;
        private Button[] buttons;
        

        public GamePage(String userID)
        {
            
            InitializeComponent();
            buttons = new Button[] { PlayButton, HitButton, StandButton };
            totals = new Label[] { DealerTotal, PlayerTotal };
            views = new FlexLayout[] { DealerView, PlayerView };
            this.userID = userID;
            
           
        }

        private async void PlayButton_Clicked(object sender, EventArgs e)
        {
            
            PlayerView.Children.Clear();
            DealerView.Children.Clear();
            blackjackInstance = new Game(c);
            System.Diagnostics.Debug.WriteLine("play button clicked");
            PlayButton.IsEnabled = false;
            PlayerView.IsVisible = true;
            DealerView.IsVisible = true;
            
            await blackjackInstance.Play(views, totals, buttons, userID);
            HitButton.IsEnabled = true;
            StandButton.IsEnabled = true;
            
        }
     
        private async void HitButton_Clicked(object sender, EventArgs e)
        {
            await blackjackInstance.Hit(views, totals, buttons);
            
        }

        private async void StandButton_Clicked(object sender, EventArgs e)
        {
            HitButton.IsEnabled = false;
            StandButton.IsEnabled = false;
            await blackjackInstance.Stand(views, totals, buttons);
        }

        

     
        }
    }

       



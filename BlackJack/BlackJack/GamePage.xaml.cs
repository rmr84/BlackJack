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

        private Game blackjackInstance; 
        private StackLayout[] notifications;
        private FlexLayout[] views;
        private Label[] totals;
        private Button[] buttons;
        

        public GamePage()
        {
            
            InitializeComponent();
            buttons = new Button[] { PlayButton, HitButton, StandButton };
            totals = new Label[] { DealerTotal, PlayerTotal };
            views = new FlexLayout[] { DealerView, PlayerView };
            
           
        }

        private async void PlayButton_Clicked(object sender, EventArgs e)
        {
            
            PlayerView.Children.Clear();
            DealerView.Children.Clear();
            blackjackInstance = new Game();
            System.Diagnostics.Debug.WriteLine("play button clicked");
            PlayButton.IsEnabled = false;
            PlayerView.IsVisible = true;
            DealerView.IsVisible = true;
            

            await blackjackInstance.Play(views, totals, buttons);
            HitButton.IsEnabled = true;
            StandButton.IsEnabled = true;
            
            

        }
     
        private async void HitButton_Clicked(object sender, EventArgs e)
        {
            await blackjackInstance.Hit(views, totals, buttons);
            
        }

        private async void StandButton_Clicked(object sender, EventArgs e)
        {
            await blackjackInstance.Stand(views, totals, buttons);
            HitButton.IsEnabled = false;
            StandButton.IsEnabled = false;
        }

     
        }
    }

       



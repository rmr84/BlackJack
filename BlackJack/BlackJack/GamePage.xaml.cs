using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlackJack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        private GameConstants.GameOperations instance;
        private StackLayout[] notifications;
        private FlexLayout[] views;
        private Label[] totals;
        private Button[] buttons;
        private int deckCount;
        

        public GamePage()
        {
            InitializeComponent();

            views = new FlexLayout[] { DealerView, PlayerView };
            totals = new Label[] { DealerTotal, PlayerTotal, Win, Lost, Push };
            notifications = new StackLayout[] { DealersTurnNotification, PushNotification, BustNotification, LostNotification, DealerBlackjackNotification, WinNotification, BlackjackNotification };
            buttons = new Button[] { PlayButton, HitButton, StandButton };
        }

        private async void HitButton_Clicked(object sender, EventArgs e)
        {
            await instance.Hit(views, totals, notifications, buttons);
        }

        private async void StandButton_Clicked(object sender, EventArgs e)
        {
            await instance.Stand(views, totals, notifications, buttons);
        }

        private async void PlayButton_Clicked(object sender, EventArgs e)
        {
            PlayButton.IsEnabled = false;
            PlayButton.Text = "Play";
            await instance.Play(views, totals, notifications, buttons);

        }
    }

    
}
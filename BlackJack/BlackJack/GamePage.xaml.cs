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
        private StackLayout[] notifications;
        private Label[] totals;
        private Button[] buttons;
        private int deckCount;
        

        public GamePage()
        {
            InitializeComponent();
           // this.deckCount = deckCount;

            notifications = new StackLayout[] { DealersTurnNotification, PushNotification, BustNotification, LostNotification, DealerBlackjackNotification, WinNotification, BlackjackNotification };
        }

        private void HitButton_Clicked(object sender, EventArgs e)
        {

        }

        private void StandButton_Clicked(object sender, EventArgs e)
        {

        }

        private void PlayButton_Clicked(object sender, EventArgs e)
        {

        }
    }

    
}
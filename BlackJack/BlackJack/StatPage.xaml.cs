﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlackJack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatPage : ContentPage
    {
        public StatPage()
        {
            InitializeComponent();
        }

        private void Reset_Clicked(object sender, EventArgs e)
        {

        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new EditUser());
        }
    }
}
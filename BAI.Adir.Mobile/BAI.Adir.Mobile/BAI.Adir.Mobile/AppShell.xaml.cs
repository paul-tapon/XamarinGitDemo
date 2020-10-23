using System;
using System.Collections.Generic;
using BAI.Adir.Mobile.ViewModels;
using BAI.Adir.Mobile.Views;
using BAI.Adir.Mobile.Views.Species;
using Xamarin.Forms;

namespace BAI.Adir.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(SpeciesDetails), typeof(SpeciesDetails));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}

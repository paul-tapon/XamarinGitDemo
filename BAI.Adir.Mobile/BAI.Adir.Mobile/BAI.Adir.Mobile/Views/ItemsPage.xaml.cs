using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using BAI.Adir.Mobile.Models;
using BAI.Adir.Mobile.Views;
using BAI.Adir.Mobile.ViewModels;

namespace BAI.Adir.Mobile.Views
{
    public partial class ItemsPage : ContentPage
    {
        public ItemsPage()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }
    }
}
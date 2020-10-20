using System.ComponentModel;
using Xamarin.Forms;
using BAI.Adir.Mobile.ViewModels;

namespace BAI.Adir.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BAI.Adir.Mobile.Services;
using BAI.Adir.Mobile.Views;

namespace BAI.Adir.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

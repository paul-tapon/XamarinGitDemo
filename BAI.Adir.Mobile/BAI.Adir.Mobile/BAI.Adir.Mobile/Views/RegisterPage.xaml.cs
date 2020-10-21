using BAI.Adir.Mobile.Models;
using BAI.Adir.Mobile.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BAI.Adir.Mobile.Views
{
    public partial class RegisterPage : ContentPage
    {
        string apiUrl = "https://webapidemo-ls8.conveyor.cloud/api/";
        public AppUser appUser { get; set; }
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel();
        }

        private async Task buttonSave_Clicked(object sender, EventArgs e)
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest("appUsers", DataFormat.Json);
            //var context = new AppUser();

            OnPropertyChanged("IsBusy");
            try
            {
                request.AddJsonBody(appUser);
                request.Method = Method.POST;

                var response = await client.ExecuteAsync(request);
                var productData = JsonConvert.DeserializeObject<AppUser>(response.Content);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    //await UploadPhoto(productData.ProductID);
                    //await DisplayAlert("System Message", response.Content, "OK");
                    await DisplayAlert("Success", response.Content, "OK");
                    //await Shell.Current.GoToAsync("//LoginPage");
                }
                else
                {
                    await DisplayAlert("Error", response.Content, "NOT OK");
                    //await Shell.Current.GoToAsync("//LoginPage");
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged("IsBusy");
            }
        }
    }
}
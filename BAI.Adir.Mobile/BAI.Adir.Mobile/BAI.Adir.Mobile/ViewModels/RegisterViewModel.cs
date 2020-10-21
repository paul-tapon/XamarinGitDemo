using BAI.Adir.Mobile.Models;
using BAI.Adir.Mobile.Views;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xamarin.Forms;

namespace BAI.Adir.Mobile.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        //public Command RegisterCommand { get; }
        public Command CancelCommand { get; }
        public Command SaveCommand { get; }
        //public AppUser AppUser { get; set; }
        ////API
        //string apiUrl = "https://webapidemo-ls8.conveyor.cloud/api/";

        public RegisterViewModel()
        {
            CancelCommand = new Command(OnCancel);
            //RegisterCommand = new Command(OnRegisterClicked);
            SaveCommand = new Command(OnSaveClicked);
        }
        //private async void OnRegisterClicked(object obj)
        //{
        //    await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        //}
        private async void OnSaveClicked(object obj)
        {
            //var client = new RestClient(apiUrl);
            //var request = new RestRequest("appUsers", DataFormat.Json);
            ////var context = new AppUser();

            //OnPropertyChanged("IsBusy");
            //try
            //{
                
            //    request.AddJsonBody(AppUser);
            //    request.Method = Method.POST;

            //    var response = await client.ExecuteAsync(request);
            //    var productData = JsonConvert.DeserializeObject<AppUser>(response.Content);

            //    if (response.StatusCode == HttpStatusCode.Created)
            //    {
            //        //await UploadPhoto(productData.ProductID);
            //        //await DisplayAlert("System Message", response.Content, "OK");

            //        await Shell.Current.GoToAsync("//LoginPage");
            //    }
            //    else
            //    {
            //        await Shell.Current.GoToAsync("//LoginPage");
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
            //    IsBusy = false;
            //    OnPropertyChanged("IsBusy");
            //}
        }
        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}

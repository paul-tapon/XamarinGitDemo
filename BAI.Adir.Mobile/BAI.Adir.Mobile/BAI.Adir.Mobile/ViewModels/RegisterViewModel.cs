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
        public RegisterDto RegisterDto { get; set; }
        ////API
        //string Settings.AdirApiUrl = "https://webapidemo-ls8.conveyor.cloud/api/";

        public RegisterViewModel()
        {
            CancelCommand = new Command(OnCancel);
            //RegisterCommand = new Command(OnRegisterClicked);
            SaveCommand = new Command(OnSaveClicked);
            RegisterDto = new RegisterDto();
        }
        //private async void OnRegisterClicked(object obj)
        //{
        //    await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        //}        
        private async void OnSaveClicked(object obj)
        {
            //await DisplayAlert("Error", "API not success", "OK");

            var client = new RestClient(Settings.AdirApiUrl);
            var request = new RestRequest("RegisterAppUser", DataFormat.Json);
            //var context = new AppUser();

            //OnPropertyChanged("IsBusy");
            try
            {
                //request.AddQueryParameter
                request.AddJsonBody(RegisterDto);
                request.Method = Method.POST;

                var response = await client.ExecuteAsync(request);
                //var productData = JsonConvert.DeserializeObject<AppUser>(response.Content);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    //await UploadPhoto(productData.ProductID);
                    //await DisplayAlert("System Message", response.Content, "OK");
                    //await DisplayAlert("Success", "Please check your email for verification", "OK");
                    //await Shell.Current.GoToAsync("//LoginPage");
                    //isSavingForm(_isSaving);
                }
                else
                {
                    //await DisplayAlert("Error", "API not success", "OK");
                    //await Shell.Current.GoToAsync("//LoginPage");
                    //isSavingForm(_isSaving);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //IsBusy = false;
                //OnPropertyChanged("IsBusy");
            }
        }
        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}

using BAI.Adir.Mobile.Models;
using BAI.Adir.Mobile.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BAI.Adir.Mobile.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterDto registerDto { get; set; }
        bool _isSaving { get; set; }
        RegisterViewModel viewModel = null;

        public RegisterPage()
        {
            InitializeComponent();
            viewModel = new RegisterViewModel();
            BindingContext = viewModel;
            _isSaving = true;
            ValidateSave();
        }

        private void isSavingForm(bool issaving)
        {
            buttonSave.IsEnabled = issaving;

            _ = issaving == true ? buttonSave.Text = "Save" : buttonSave.Text = "Saving...";
            entryEmail.IsEnabled = issaving;
            entryFirstName.IsEnabled = issaving;
            entryLastName.IsEnabled = issaving;
            entryMiddleName.IsEnabled = issaving;
            entryPassword.IsEnabled = issaving;
            entryRepeatPassword.IsEnabled = issaving;
            entryUsername.IsEnabled = issaving;
            OnPropertyChanged("_isSaving");

        }
        public class Validator
        {
            public static bool EmailIsValid(string emailaddress)
            {
                try
                {
                    MailAddress mail = new MailAddress(emailaddress);
                    return true;
                }
                catch (FormatException)
                {
                    return false;
                }

            }
        }
        private void ValidateSave()
        {
            if (!String.IsNullOrWhiteSpace(entryFirstName.Text)
                && !String.IsNullOrWhiteSpace(entryMiddleName.Text)
                && !String.IsNullOrWhiteSpace(entryLastName.Text)
                && !String.IsNullOrWhiteSpace(entryUsername.Text)
                && !String.IsNullOrWhiteSpace(entryPassword.Text)
                && !String.IsNullOrWhiteSpace(entryRepeatPassword.Text)
                && !String.IsNullOrWhiteSpace(entryEmail.Text))
            {
                buttonSave.IsEnabled = true;
            }
            else
            {
                buttonSave.IsEnabled = false;
            }
        }
        protected bool checkEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                if (Validator.EmailIsValid(email))
                {
                    return true;
                }
            }
            return false;
        }

        private async void buttonSave_Clicked(object sender, EventArgs e)
        {
            //isSavingForm(!_isSaving);

            //if (!checkEmail(entryEmail.Text))
            //{
            //    await DisplayAlert("EMAIL", "Email not in correct format", "OK");
            //    //await Shell.Current.GoToAsync("//LoginPage");
            //    isSavingForm(_isSaving);
            //    return;
            //}
            //var client = new RestClient(Settings.AdirApiUrl);
            //var request = new RestRequest("appUsers", DataFormat.Json);
            //var context = new AppUser();

            ////OnPropertyChanged("IsBusy");
            //try
            //{
            //    context.FirstName = entryFirstName.Text;
            //    context.MiddleName = entryMiddleName.Text;
            //    context.LastName = entryLastName.Text;
            //    context.Username = entryUsername.Text;
            //    context.PasswordHash = entryPassword.Text;
            //    context.Email = entryEmail.Text;

            //    //request.AddQueryParameter
            //    request.AddJsonBody(context);
            //    request.Method = Method.POST;

            //    var response = await client.ExecuteAsync(request);
            //    //var productData = JsonConvert.DeserializeObject<AppUser>(response.Content);

            //    if (response.StatusCode == HttpStatusCode.Created)
            //    {
            //        //await UploadPhoto(productData.ProductID);
            //        //await DisplayAlert("System Message", response.Content, "OK");
            //        await DisplayAlert("Success", "Please check your email for verification", "OK");
            //        //await Shell.Current.GoToAsync("//LoginPage");
            //        isSavingForm(_isSaving);
            //    }
            //    else
            //    {
            //        await DisplayAlert("Error", "API not success", "OK");
            //        //await Shell.Current.GoToAsync("//LoginPage");
            //        isSavingForm(_isSaving);
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
            //    //IsBusy = false;
            //    //OnPropertyChanged("IsBusy");
            //}
        }
        private async void EntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if(e.NewTextValue !=null)
            {
                var entry = sender as Entry;
                ValidateSave();
            }
        }
    }
}
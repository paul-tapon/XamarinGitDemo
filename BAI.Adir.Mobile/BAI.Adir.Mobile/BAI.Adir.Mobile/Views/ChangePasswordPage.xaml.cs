using BAI.Adir.Mobile.Models;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(AppUserId), nameof(AppUserId))]
    public partial class ChangePasswordPage : ContentPage
    {


        string apiUrl = "http://192.168.1.235:45457/api/";
        public ChangePassword ChangePassword { get; set; }
        //public UserProfile(LoginResult loginResult)
        private int _appUserId = 0;
        public string OldPassword { get; set; }
        //private string _FullName = "";
        public string AppUserId
        {
            set
            {
                _appUserId = int.Parse(value);

                //getUserProfile(_appUserId);
            }
            //get { return _appUserId.ToString(); }

        }

        //public string FullName
        //{
        //    set
        //    {
        //        _FullName = value;
        //    }
        //}
        public ChangePasswordPage()
        {
            InitializeComponent();

           

        }

        

        private async void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            Clearall();
            await Shell.Current.GoToAsync($"//{nameof(UserProfile)}?AppUserId={_appUserId}");
        }

        private async void ButtonSaveChanges_Clicked(object sender, EventArgs e)
        {
           
            if (!String.IsNullOrEmpty(TextRepeatPassword.Text) && !String.IsNullOrEmpty(TextOldPassword.Text) && !String.IsNullOrEmpty(TextNewPassword.Text))
                {
                if (TextNewPassword.Text == TextRepeatPassword.Text)
                {
                    try
                    {


                        var client = new RestClient(apiUrl);
                        var request = new RestRequest("user/" + _appUserId, DataFormat.Json);

                        var changepass = new ChangePassword();
                        changepass.AppUserId = _appUserId;
                        changepass.NewPassword = TextNewPassword.Text;
                        changepass.OldPassword = TextOldPassword.Text;

                        request.Method = Method.PUT; //HttpPUT IHttpActionResult Put
                        request.AddJsonBody(changepass);

                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            await DisplayAlert("Information", "Password Successfully Updated.", "Ok");
                            Clearall();
                            await Shell.Current.GoToAsync($"//{nameof(UserProfile)}?AppUserId={_appUserId}");
                        }
                        else
                        {
                            await DisplayAlert("Warning", "An error occured while updating password.", "Ok");
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Warning", "may mali sa saving mo.", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Warning", "Passwords doesn't match.", "Ok");
                }
            }
           
            else
            {
                await DisplayAlert("Warning","Password fields are blank.", "Ok");
            }
        }

        public void Clearall()
        {
            TextNewPassword.Text = null;
            TextRepeatPassword.Text = null;
            TextOldPassword.Text = null;
        }
        
           
        }
}
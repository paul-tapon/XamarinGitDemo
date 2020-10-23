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
        public LoginResult LoginResult { get; set; }
        //public UserProfile(LoginResult loginResult)
        private int _appUserId = 0;
        public string OldPassword { get; set; }
        //private string _FullName = "";
        public string AppUserId
        {
            set
            {
                _appUserId = int.Parse(value);

                getUserProfile(_appUserId);
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

        public async void getUserProfile(int id)
        {

            var client = new RestClient(apiUrl);
            var request = new RestRequest("user/"+_appUserId, DataFormat.Json);

            //var login = new Login();

            //login.Username = TextUsername.Text;
            //login.Password = TextPassword.Text;
            //request.AddUrlSegment("id", id);
            request.Method = Method.GET; //HttpPost
            //request.AddJsonBody(login);

            var response = await client.ExecuteAsync(request);
            var responseData = JsonConvert.DeserializeObject<LoginResult>(response.Content);


            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                
                OldPassword = responseData.PasswordHash;



            }
            else
            {
                //await DisplayAlert("Error", _appUserID.ToString(),"OK");
                await DisplayAlert("Error", "No user", "Ok");
            }
        }

        private async void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(UserProfile)}?AppUserId={_appUserId}");
        }

        private async void ButtonSaveChanges_Clicked(object sender, EventArgs e)
        {
            //getOldPassword();
            

            //var client = new RestClient(apiUrl);
            //var request = new RestRequest("user/{id}", DataFormat.Json);

            ////var LoginResult = new LoginResult();


            //request.AddUrlSegment("id", LoginResult.appuserid);
            //request.Method = Method.PUT;
            //request.AddJsonBody(LoginResult);


            //var response = await client.ExecuteAsync(request);
            //var responseData = JsonConvert.DeserializeObject<LoginResult>(response.Content);

            //responseData.PasswordHash = TextNewPassword.Text;

            //if (response.StatusCode == HttpStatusCode.NoContent)
            //{
            //    await DisplayAlert("Display Message", "password updated.", "Ok");

            //}
            //else
            //{
            //    await DisplayAlert("Warning", "An error occured while changing password.", "Ok");
            //}
            if (TextRepeatPassword.Text.Equals(TextNewPassword.Text) && TextOldPassword.Text == OldPassword.ToString())
                {
                var client = new RestClient(apiUrl);
                var request = new RestRequest("user/" + _appUserId, DataFormat.Json);

                var loginResult = new LoginResult();

                loginResult.PasswordHash = TextNewPassword.Text;

                request.Method = Method.PUT; //HttpPUT IHttpActionResult Put
                request.AddJsonBody(loginResult);

                var response = await client.ExecuteAsync(request);

                 if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    await DisplayAlert("Information", "Password Successfully Updated.", "Ok");
                    TextNewPassword.Text = null;
                    TextRepeatPassword.Text = null;
                    await Shell.Current.GoToAsync($"//{nameof(UserProfile)}?AppUserId={_appUserId}");
                }
                else
                {
                    await DisplayAlert("Warning", "An error occured while updating password.", "Ok");
                }
            }
            else if (string.IsNullOrEmpty(TextOldPassword.Text) || string.IsNullOrEmpty(TextNewPassword.Text) || string.IsNullOrEmpty(TextRepeatPassword.Text))
            {

                await DisplayAlert("Warning", "Empty Fields, palagyan naman.", "Ok");
            }
            else
            {
                await DisplayAlert("Warning","Passwords don't match or old password is mali.", "Ok");
            }
        }

        
            //public string AppUserId
            //{
            //    set
            //    {
            //        _appUserId = int.Parse(value);

            //        getUserPassword(_appUserId);
            //    }
            //    //get { return _appUserId.ToString(); }

            //}
            //public async void getUserPassword(int id)
            //{
            //    var client = new RestClient(apiUrl);
            //    var request = new RestRequest("user/{id}", DataFormat.Json);

            //    request.AddUrlSegment("id", id);
            //    request.Method = Method.PUT; 
            //    //request.AddJsonBody(login);

            //    var response = await client.ExecuteAsync(request);
            //    var responseData = JsonConvert.DeserializeObject<LoginResult>(response.Content);


            //    if (response.StatusCode == System.Net.HttpStatusCode.Created)
            //    {

            //        //LabelFullName.Text = responseData.fullname;
            //        responseData.PasswordHash = TextNewPassword.Text;


            //    }
            //    else
            //    {
            //        await DisplayAlert("Error", "No user", "Ok");
            //    }
            //}
        }
}
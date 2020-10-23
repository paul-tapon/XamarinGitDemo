using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAI.Adir.Mobile.Models;
using BAI.Adir.Mobile.ViewModels;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RestSharp;
using Newtonsoft.Json;

namespace BAI.Adir.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(AppUserId), nameof(AppUserId))]
    //[QueryProperty(nameof(FullName), nameof(FullName))]
    

    public partial class UserProfile : ContentPage

    {
        string apiUrl = "http://192.168.1.235:45457/api/";
        public LoginResult LoginResult { get; set; }
        //public UserProfile(LoginResult loginResult)
        private int _appUserId = 0;
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

        public UserProfile()
        {
            InitializeComponent();

            LoginResult = LoginResult;
            
            BindingContext = this;
            //ExecuteUserInfoCommand()
            //getUserProfile(_appUserId);
        }


        public async void getUserProfile(int id)
        {

            var client = new RestClient(apiUrl);
            var request = new RestRequest("user/{id}", DataFormat.Json);

            //var login = new Login();

            //login.Username = TextUsername.Text;
            //login.Password = TextPassword.Text;
            request.AddUrlSegment("id", id);
            request.Method = Method.GET; //HttpPost
            //request.AddJsonBody(login);

            var response = await client.ExecuteAsync(request);
           var responseData = JsonConvert.DeserializeObject<LoginResult>(response.Content);
           

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                LabelFullName.Text = responseData.fullname;
                LabelEmailAddress.Text = responseData.Email;
                //await DisplayAlert("Information", response.Content, "OK");


            }
            else
            {
                //await DisplayAlert("Error", _appUserID.ToString(),"OK");
                await DisplayAlert("Error", "No user", "Ok");
            }
        }

        private async void ButtonChangePassword_Clicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("ChangePasswordPage");
           
            await Shell.Current.GoToAsync($"//{nameof(ChangePasswordPage)}?AppUserId={_appUserId}");
        }

        //public async Task ExecuteUserInfoCommand()

        //{
        //    try
        //    {

        //        //code to call API
        //        var client = new RestClient(apiUrl);
        //        var request = new RestRequest("User/"+_appUserId.ToString(), DataFormat.Json);
        //        request.Method = Method.GET;

        //        var response = await client.ExecuteAsync(request);

        //        //Newtonsoft
        //        var responseData = JsonConvert.DeserializeObject<List<LoginResult>>(response.Content);

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //    }
        //}
    }
}
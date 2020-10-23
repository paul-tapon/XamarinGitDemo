using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAI.Adir.Mobile.Models;
using BAI.Adir.Mobile.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BAI.Adir.Mobile.Views
{
    public partial class LoginPage : ContentPage
    {
        public UserProfile userProfile { get; set; }
        string apiUrl = "http://192.168.1.235:45457/api/";
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest("security", DataFormat.Json);

            var login = new Login();

            login.Username = TextUsername.Text;
            login.Password = TextPassword.Text;

            request.Method = Method.POST; //HttpPost
            request.AddJsonBody(login);

            var response = await client.ExecuteAsync(request);
            var responseData = JsonConvert.DeserializeObject<LoginResult>(response.Content);
           
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {


                //await Shell.Current.GoToAsync("UserProfile");

                //****responseData
                //await Shell.Current.GoToAsync($"//{new UserProfile(responseData)}");

                //userProfile = new UserProfile(responseData);
                //UserProfile UserProfile = userProfile;
                //await Shell.Current.GoToAsync($"userProfile");
                TextUsername.Text = null;
                TextPassword.Text = null;
                await Shell.Current.GoToAsync($"//{nameof(UserProfile)}?AppUserId={responseData.appuserid}"); //,?FullName={responseData.fullname
                //await Shell.Current.GoToAsync($"//{nameof(UserProfile)}?AppUserId=87687");


                //await DisplayAlert("Information", "Invalid Credentials.", "Ok");
                //await Navigation.PushModalAsync(new NavigationPage(new UserProfile(this)));

            }
            else
            {
                await DisplayAlert("Information", "Invalid Credentials.", "Ok");
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
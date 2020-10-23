using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BAI.Adir.Mobile.Models;
using RestSharp;
using Newtonsoft.Json;
using System.Net;

namespace BAI.Adir.Mobile.Views.Species
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpeciesBrowser : ContentPage
    {
        public ObservableCollection<Models.Species> Species { get; set; }
        public SpeciesBrowser()
        {
            InitializeComponent();
            BindingContext = this;
            Species = new ObservableCollection<Models.Species>();

            SpeciesListView.RefreshCommand = new Command(() =>
            {
                ExecuteLoadSpeciesCommand();
                SpeciesListView.IsRefreshing = false;

            });

            ExecuteLoadSpeciesCommand();

        }

        public async void ExecuteLoadSpeciesCommand()
        {
            OnPropertyChanged("IsBusy");
            try
            {

                var client = new RestClient(Settings.AdirApiUrl);
                var request = new RestRequest("species", DataFormat.Json);
                request.Method = Method.GET;

                var response = await client.ExecuteAsync(request);
                var responseData = JsonConvert.DeserializeObject<List<Models.Species>>(response.Content);
                RefreshListView(responseData);

            }
            catch (Exception ex)
            {
            }

            finally
            {
                IsBusy = false;
                OnPropertyChanged("IsBusy");
                SpeciesListView.IsRefreshing = false;
            }

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Species.Clear();
            var client = new RestClient(Settings.AdirApiUrl);
            var request = new RestRequest("species/filter?value=" + WebUtility.UrlEncode(txtQuery.Text), DataFormat.Json);
            request.Method = Method.GET;

            var response = await client.ExecuteAsync(request);
            var responseData = JsonConvert.DeserializeObject<List<Models.Species>>(response.Content);

            RefreshListView(responseData);
        }

        private void RefreshListView(List<Models.Species> responseData)
        {
            Species.Clear();

            foreach (var species in responseData)
            {
                Species.Add(species);
            }
            OnPropertyChanged("Species");
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
             Species.Clear();

            txtQuery.Text = null;
            txtQuery.Focus();

            var client = new RestClient(Settings.AdirApiUrl);
            var request = new RestRequest("species/filter?value=", DataFormat.Json);
            request.Method = Method.GET;

            var response = await client.ExecuteAsync(request);
            var responseData = JsonConvert.DeserializeObject<List<Models.Species>>(response.Content);

            RefreshListView(responseData);
        }

        public async void SpeciesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var species = e.Item as Models.Species;
            
            await Shell.Current.GoToAsync("SpeciesDetails?SpeciesId=" + species.SpeciesId);

        }

    }
    }


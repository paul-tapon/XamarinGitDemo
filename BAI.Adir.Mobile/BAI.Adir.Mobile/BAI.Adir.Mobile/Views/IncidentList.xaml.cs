using BAI.Adir.Mobile.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Region = BAI.Adir.Mobile.Models.Region;

namespace BAI.Adir.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncidentList : ContentPage
    {
        public ObservableCollection<DiseaseIncident> DiseaseIncidents { get; set; }


        public ObservableCollection<Region> Regions { get; set; }
        public ObservableCollection<Province> Provinces { get; set; }

        public ObservableCollection<Municipality> Municipalities { get; set; }

        public ObservableCollection<Models.Species> Speciess { get; set; }

       
        public IncidentList()
        {
            InitializeComponent();
            BindingContext = this;
            DiseaseIncidents = new ObservableCollection<DiseaseIncident>();
            ExecuteLoadRegionCommand();
            ExecuteLoadSpeciesCommand();

            SummaryIncident.RefreshCommand = new Command(() =>
            {
                
                Region.SelectedIndex = -1;
                Province.SelectedIndex = -1;
                Municipality.SelectedIndex = -1;
                SpeciesKind.SelectedIndex = -1;
                SummaryIncident.IsRefreshing = false;
                ExecuteLoadDiseaseIncidentCommand();
            });
            ExecuteLoadDiseaseIncidentCommand();

        }

        public async Task ExecuteLoadDiseaseIncidentCommand()
        {
            //if (IsBusy) return;
            //IsBusy = true;
            try
            {
                DiseaseIncidents.Clear();
                var client = new RestClient(Settings.AdirApiUrl);
                var request = new RestRequest("DiseaseIncident", DataFormat.Json);
                request.Method = Method.GET;

                var response = await client.ExecuteAsync(request);
                var responseData = JsonConvert.DeserializeObject<List<DiseaseIncident>>(response.Content);
                foreach (var diseaseIncident in responseData)
                {
                    DiseaseIncidents.Add(diseaseIncident);
                }
                OnPropertyChanged("DiseaseIncidents");
            }
            catch
            {

            }
            finally
            {

            }
        }
        private async void filter()
        {
            var client = new RestClient(Settings.AdirApiUrl);
            
            

            if (Region.SelectedItem == null & Province.SelectedItem == null & Municipality.SelectedItem == null & SpeciesKind.SelectedItem == null)
            {
                ExecuteLoadDiseaseIncidentCommand();
            }
          else
            {
               

                var region = Region.SelectedItem as Region;
                var province = Province.SelectedItem as Province;
                var mun = Municipality.SelectedItem as Municipality;
                var species = SpeciesKind.SelectedItem as Models.Species;
                int regionid;
                int provinceid;
                int munid;
                int speciesid;
                if (Region.SelectedItem == null)
                {
                    regionid = 0;
                }
                else
                {
                    regionid = region.RegionId;
                }
                if (Province.SelectedItem == null)
                {
                    provinceid = 0;
                }
                else
                {
                    provinceid = province.ProvinceId;
                }
                if(Municipality.SelectedItem == null)
                {
                    munid = 0;
                }
                else
                {
                    munid = mun.MunicipalityId;
                }
                if (SpeciesKind.SelectedItem == null)
                {
                    speciesid = 0;
                }
                else
                {
                    speciesid = species.SpeciesId;
                }
                var request = new RestRequest("DiseaseIncident/filter?region=" + WebUtility.UrlEncode(regionid.ToString()), DataFormat.Json);
                request.AddParameter("province", provinceid);
                request.AddParameter("municipality", munid);
                request.AddParameter("barangay", 0);
                request.AddParameter("species", speciesid);
                // request.AddParameter("description", txtQuery.Text);
                request.AddParameter("sortfield", "region");
                request.AddParameter("sortDir", "asc");
                request.Method = Method.GET;

                var response = await client.ExecuteAsync(request);
                var responseData = JsonConvert.DeserializeObject<List<DiseaseIncident>>(response.Content);
                DiseaseIncidents.Clear();
                foreach (var incidentList in responseData)
                {
                    DiseaseIncidents.Add(incidentList);
                  
                }
                OnPropertyChanged("DiseaseIncidents");
               
            }

        }
        private void btnSearch_Clicked(object sender, EventArgs e)
        {
            filter();
        }

        private void Back_Clicked(object sender, EventArgs e)
        {

        }
        //region load
        public async Task ExecuteLoadRegionCommand()
        {
            //if (IsBusy) return;
            //IsBusy = true;
            try
            {
                
                var client = new RestClient(Settings.AdirApiUrl);
                var request = new RestRequest("Regions", DataFormat.Json);
                request.Method = Method.GET;

                var response = await client.ExecuteAsync(request);
                var responseData = JsonConvert.DeserializeObject<List<Region>>(response.Content);
                Region.ItemsSource = responseData;
                /*foreach (var region in responseData)
                {
                    Regions.Add(region);
                }*/
                OnPropertyChanged("Regions");
            }
            catch
            {

            }
            finally
            {

            }
        }
        //province load


        private async void Region_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var region = Region.SelectedItem as Region;
                var client = new RestClient(Settings.AdirApiUrl);
                //IsBusy = true;
                var request = new RestRequest("provinces/GetProvinceByRegionId?id=" + WebUtility.UrlEncode(region.RegionId.ToString()), DataFormat.Json);



                request.Method = Method.GET;

                var response = await client.ExecuteAsync(request);
                var responseData = JsonConvert.DeserializeObject<List<Province>>(response.Content);
                Province.ItemsSource = responseData;
                /* foreach (var incidentList in responseData)
                 {
                     DiseaseIncidents.Add(incidentList);
                 }*/
                OnPropertyChanged("Provinces");
                
            }
            catch
            {
                
            }


        }

        private async void Province_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var province = Province.SelectedItem as Province;
                var client = new RestClient(Settings.AdirApiUrl);
                //IsBusy = true;
                var request = new RestRequest("Municipalities/GetMunicipalitiesByProvinceId?id=" + WebUtility.UrlEncode(province.ProvinceId.ToString()), DataFormat.Json);



                request.Method = Method.GET;

                var response = await client.ExecuteAsync(request);
                var responseData = JsonConvert.DeserializeObject<List<Municipality>>(response.Content);
                Municipality.ItemsSource = responseData;
                /* foreach (var incidentList in responseData)
                 {
                     DiseaseIncidents.Add(incidentList);
                 }*/
                OnPropertyChanged("Municipalities");
            }
            catch
            {

            }
        }

        private void Municipality_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public async Task ExecuteLoadSpeciesCommand()
        {
            //if (IsBusy) return;
            //IsBusy = true;
            try
            {

                var client = new RestClient(Settings.AdirApiUrl);
                var request = new RestRequest("Species", DataFormat.Json);
                request.Method = Method.GET;

                var response = await client.ExecuteAsync(request);
                var responseData = JsonConvert.DeserializeObject<List<Models.Species>>(response.Content);
                SpeciesKind.ItemsSource = responseData;
                /*foreach (var region in responseData)
                {
                    Regions.Add(region);
                }*/
                OnPropertyChanged("Speciess");
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void btnExport_Clicked(object sender, EventArgs e)
        {
            
               
            
        }

        public async void SummaryIncident_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var incidentlist = e.Item as Models.DiseaseIncident;
           //await Shell.Current.GoToAsync("IncidentListView?DiseaseIncidentId=" + incidentlist.DiseaseIncidentId);

            await Navigation.PushAsync(new IncidentListView(incidentlist));

        }

        private void btnReset_Clicked(object sender, EventArgs e)
        {
            Region.SelectedIndex = -1;
            Province.SelectedIndex = -1;
            Municipality.SelectedIndex = -1;
            SpeciesKind.SelectedIndex = -1;
            SummaryIncident.IsRefreshing = false;
            ExecuteLoadDiseaseIncidentCommand();

        }
    }
}
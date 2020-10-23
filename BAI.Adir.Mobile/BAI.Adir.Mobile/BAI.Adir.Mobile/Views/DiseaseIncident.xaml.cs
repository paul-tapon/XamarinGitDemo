using BAI.Adir.Mobile.Models;
using BAI.Adir.Mobile.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Region = BAI.Adir.Mobile.Models.Region;

namespace BAI.Adir.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class DiseaseIncident : ContentPage
    {
        string apiURL = "http://10.0.2.2:45455/api/";

        App _mainpage = null;

        byte[] byteArray = null;
        public ObservableCollection<Region> Regions { get; set; }
        public ObservableCollection<Province> Provinces { get; set; }
        public ObservableCollection<Municipalities> Municipalities { get; set; }
        public ObservableCollection<Barangay> Barangays { get; set; }

        public ObservableCollection<SpeciesDTO> Species { get; set; }

        // public ObservableCollection<Species> Provinces { get; set; }
        

        // public DiseaseIncident Incident { get; set; }

        //public DiseaseIncident(App mainPage)
        //{
        //    InitializeComponent();
        //    _mainpage = mainPage;
        //    BindingContext = this;

        //}

        public DiseaseIncident()
        {
            //_mainpage = mainPage;
            
            DiseaseIncidentViewModel DiseaseBinding = new DiseaseIncidentViewModel();
            BindingContext = DiseaseBinding;

            InitializeComponent();
            ExecuteLoadRegionCommand();
            LoadSpecies();


        }

        private void Generate_Clicked(object sender, EventArgs e)
        {

        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            var client = new RestClient(apiURL);
            var request = new RestRequest("diseaseincidents", (Method)DataFormat.Json);

            var diseaseincident = new DiseaseIncidentDTO();
            var DiseaseIncidentViewModel = new DiseaseIncidentViewModel();

            diseaseincident.ReportedByFirstname = FirstName.Text;
            diseaseincident.ReportedByMiddleName = MiddleName.Text;
            diseaseincident.ReportedByLastname = LastName.Text;
            diseaseincident.ReportedBySuffixName = ExtensionName.Text;
            diseaseincident.ContactNumber = ContactNumber.Text;
            diseaseincident.ContactPerson = ContactPerson.Text;
            diseaseincident.SymptomsStartDate = Symptoms.Date;
            diseaseincident.DeceasedDate = AnimalDied.Date;
            diseaseincident.NumberOfDeaths = Convert.ToInt32(Deaths.Text);
            diseaseincident.NumberOfInfected = Convert.ToInt32(Infected.Text);
            diseaseincident.Population = Convert.ToInt32(Population.Text);
            diseaseincident.Symptoms = WhatSymptoms.Text;
            diseaseincident.Notes = Remarks.Text;

            var brgy = BarangaySelect.SelectedItem as Barangay;

            diseaseincident.BarangayId = (int)brgy.BarangayId;
            diseaseincident.Longitude = Convert.ToDouble(Longitude.Text);
            diseaseincident.Latitude = Convert.ToDouble(Longitude.Text);


            // diseaseincident.SpeciesId = DiseaseIncidentViewModel.SelectedSpecies.SpeciesId;

            var spcs = BarangaySelect.SelectedItem as Barangay;

            diseaseincident.SpeciesId = (int)spcs.BarangayId;

            diseaseincident.IsActive = true;
            diseaseincident.CreatedByAppUserId = 1;
            diseaseincident.CreatedOn = DateTime.Now;
            request.Method = Method.POST;
            request.AddJsonBody(diseaseincident);
            var response = await client.ExecuteAsync(request);

            var productData = JsonConvert.DeserializeObject<DiseaseIncidentDTO>(response.Content);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
              //  await UploadPhoto(diseaseincident.DiseaseIncidentId);
                await DisplayAlert("Information", "Product successfully added", "OK");
               // await Navigation.PopModalAsync();

               // await _mainpage.ExecuteLoadProductsCommand();
            }
            else
            {
                await DisplayAlert("Warning", "An Error occured while creating", "OK");
            }
        }


        public async Task ExecuteLoadRegionCommand()
        {
            var client = new RestClient(apiURL);
            var request = new RestRequest("regions", (Method)DataFormat.Json);
            request.Method = Method.GET;
            var response = await client.ExecuteAsync(request);
            var responseData = JsonConvert.DeserializeObject<List<Region>>(response.Content);
            RegionSelect.ItemsSource = responseData;
            //Regions = responseData;
            OnPropertyChanged("Regions");
        }

        public async Task LoadSpecies()
        {
            try
            {
                var client = new RestClient(apiURL);
                var request = new RestRequest("species", (Method)DataFormat.Json);
                request.Method = Method.GET;
                var response = await client.ExecuteAsync(request);
                var responseData = JsonConvert.DeserializeObject<List<SpeciesDTO>>(response.Content);
                SpeciesSelect.ItemsSource = responseData;
                OnPropertyChanged("Species");
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }


      

     
        private void Species_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void PhotoBrowse_Clicked(object sender, EventArgs e)
        {

        }

        private async void ProvinceSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var province = ProvinceSelect.SelectedItem as Province;
                var client = new RestClient(apiURL);
                //IsBusy = true;
                var request = new RestRequest("Municipalities/GetMunicipalitiesByProvinceId?id=" + WebUtility.UrlEncode(province.ProvinceId.ToString()), DataFormat.Json);



                request.Method = Method.GET;

                var response = await client.ExecuteAsync(request);
                var responseData = JsonConvert.DeserializeObject<List<Municipalities>>(response.Content);
                MunicipalitySelect.ItemsSource = responseData;
                /* foreach (var incidentList in responseData)
                 {
                     DiseaseIncidents.Add(incidentList);
                 }*/
                OnPropertyChanged("Municipalities");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void MunicipalitySelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var Municipalities = MunicipalitySelect.SelectedItem as Municipalities;
                var client = new RestClient(apiURL);
                //IsBusy = true;
                var request = new RestRequest("Barangays/GetBarangaysByMunicipalitiesId?id=" + WebUtility.UrlEncode(Municipalities.MunicipalityId.ToString()), DataFormat.Json);
                request.Method = Method.GET;

                var response = await client.ExecuteAsync(request);
                var responseData = JsonConvert.DeserializeObject<List<Barangay>>(response.Content);
                BarangaySelect.ItemsSource = responseData;
                /* foreach (var incidentList in responseData)
                 {
                     DiseaseIncidents.Add(incidentList);
                 }*/
                OnPropertyChanged("Barangays");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BarangaySelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private async void RegionSelect_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                var region = RegionSelect.SelectedItem as Region;
                var client = new RestClient(apiURL);
                //IsBusy = true;
                var request = new RestRequest("provinces/GetProvinceByRegionId?id=" + WebUtility.UrlEncode(region.RegionId.ToString()), DataFormat.Json);

                request.Method = Method.GET;
                var response = await client.ExecuteAsync(request);
                var responseData = JsonConvert.DeserializeObject<List<Province>>(response.Content);
                ProvinceSelect.ItemsSource = responseData;
                /* foreach (var incidentList in responseData)
                 {
                     DiseaseIncidents.Add(incidentList);
                 }*/
                OnPropertyChanged("Provinces");

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
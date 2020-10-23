using BAI.Adir.Mobile.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BAI.Adir.Mobile.Views.Species
{
    [QueryProperty(nameof(SpeciesId), nameof(SpeciesId))]
    public partial class SpeciesDetails : ContentPage
    {
        public static string animalspecies { get; set; }
        public static string Type { get; set; }
        public static string Common { get; set; }

        public bool RStatus { get; set; }


        private int _speciesId = 0;

        public bool FieldsAreReadOnly { get; set; }

         public Models.Species Species { get; set; }
        SpeciesBrowser _speciesbrowser = null;

        public string SpeciesId
        {
            set
            {
                _speciesId = int.Parse(value);
               
                GetSpeciesDetails();
                animalspecies = Species.Name;
                Type = Species.Type;
                Common = Species.CommonDiseases;
                RStatus = Species.IsActive;
            }
        }
        public SpeciesDetails()
        {
            InitializeComponent();
            BindingContext = this;
            FieldsAreReadOnly = true;
            OnPropertyChanged("FieldsAreReadOnly");


        }

        private void GetSpeciesDetails()
        {
            var client = new RestClient(Settings.AdirApiUrl);
            var request = new RestRequest("Species/{id}");
            request.AddUrlSegment("id", _speciesId.ToString());

            var response = client.Execute(request);

            var responseData = JsonConvert.DeserializeObject<Models.Species>(response.Content);

            Species = responseData;
            OnPropertyChanged("Species");
        }

        private async void ButtonEdit_Clicked(object sender, EventArgs e)
        {
            FieldsAreReadOnly = false;
            OnPropertyChanged("FieldsAreReadOnly");
            TextName.Focus();
            ButtonEdit.IsVisible = false;
            ButtonSave.IsVisible = true;
            ButtonCancel.IsVisible = true;
            Status.IsEnabled = true;
            Createdby.IsVisible = false;
            Createdon.IsVisible = false;
            Modifiedby.IsVisible = false;
            Modifiedon.IsVisible = false;
            LCreatedby.IsVisible = false;
            LCreatedon.IsVisible = false;
            LModifiedby.IsVisible = false;
            LModifiedon.IsVisible = false;
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            if (animalspecies == TextName.Text & Type == TextType.Text & Common == TextDisease.Text) /*& RStatus == Status.Value*/
            {
                await DisplayAlert("Information", "No change recorded.", "OK");
            }
            else
            {

                var client = new RestClient(Settings.AdirApiUrl);
                var request = new RestRequest("Species/{id}");

                request.AddUrlSegment("id", _speciesId.ToString());
                request.Method = Method.PUT;
               
                request.AddJsonBody(Species);

                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    await DisplayAlert("Information", "Product successfully updated.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
            }



        }

        private async void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

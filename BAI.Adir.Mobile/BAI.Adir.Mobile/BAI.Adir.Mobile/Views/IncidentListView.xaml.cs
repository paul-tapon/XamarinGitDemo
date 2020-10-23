using BAI.Adir.Mobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace BAI.Adir.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncidentListView : ContentPage
    {
        //[QueryProperty(nameof(), nameof(DiseaseIncidentId))]
        string apiURL = "http://10.0.2.2:45456/api";

 

       
        public Models.DiseaseIncident DiseaseIncident { get; set; }
        public IncidentListView(Models.DiseaseIncident diseaseIncident)
        {
            InitializeComponent();
            DiseaseIncident = diseaseIncident;
            FullName.Text = DiseaseIncident.ReportedByFirstname + " " + DiseaseIncident.ReportedByMiddleName + " " + DiseaseIncident.ReportedByLastname + " " + DiseaseIncident.ReportedBySuffixName;
            FullAddress.Text = DiseaseIncident.FullAddress;
            BindingContext = this;

        }
    }
}
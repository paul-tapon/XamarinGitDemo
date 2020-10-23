using BAI.Adir.Mobile.Models;
using BAI.Adir.Mobile.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BAI.Adir.Mobile.ViewModels
{
    class DiseaseIncidentViewModel
    {
        private List<SpeciesDTO> SpeciesList;

        private readonly APIServices _apiservice = new APIServices();
        private int SpeciesId;
        private string Name;
        private SpeciesDTO _selectedSpecies;

        public int speciesId
        {
            get 
            {
                return SpeciesId;
            }

            set 
            {
                SpeciesId = value;
                OnPropertyChange();
            }
        }

        public string SpeciesName
        {
            get
            {
                return Name;
            }

            set
            {
                Name = value;
                OnPropertyChange();
            }
        }


        public List<SpeciesDTO> SpeciesPublicList
        {
            get
            {
                return SpeciesList;
            }
            set
            {
                SpeciesList = value;
                OnPropertyChange();
            }
        }
        public SpeciesDTO SelectedSpecies
        {
            get
            {
                return _selectedSpecies;
            }
            set
            {
                _selectedSpecies = value;
                OnPropertyChange();
            }
        }

        public DiseaseIncidentViewModel()
        {
            Task.Run(async () =>
            {
                SpeciesPublicList = await _apiservice.GetSpecies();
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChange([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

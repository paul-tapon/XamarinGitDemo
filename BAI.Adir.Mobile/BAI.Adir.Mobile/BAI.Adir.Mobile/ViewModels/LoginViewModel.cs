﻿using BAI.Adir.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xamarin.Forms;
using Newtonsoft.Json;

using RestSharp;
using BAI.Adir.Mobile.Models;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace BAI.Adir.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            

            //
            

        }
    }
}

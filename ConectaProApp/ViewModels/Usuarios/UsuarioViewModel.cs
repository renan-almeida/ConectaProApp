﻿using ConectaProApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel 
    {
        private UsuarioServices uService;
        public ICommand LoginPageCommand { get; set; }

        public UsuarioViewModel()
        {
            uService = new UsuarioServices();
            InitializeCommands();
        }

        public void InitializeCommands()
        {
            LoginPageCommand = new Command(async () => await LoginPage());
        }

        public async Task LoginPage()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Usuario.LoginView());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                    ("Ops!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
                
            }
        }
    }
}

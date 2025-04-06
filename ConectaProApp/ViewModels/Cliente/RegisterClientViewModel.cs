using ConectaProApp.Services;
using ConectaProApp.Services.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Cliente
{

    class RegisterClientViewModel : BaseViewModel
    {
        private ClienteService eService;
        public ICommand EtapaDoisRegisterClientCommand { get; set; }
        public ICommand EtapaTresRegisterClientCommand { get; set; }
        public ICommand EtapaQuatroRegisterClientCommand { get; set; }

        public RegisterClientViewModel()
        {
            eService = new ClienteService();
            InitializeCommands();

            Segmentos = new List<string>
        {
            "Tecnologia",
            "Alimentação",
            "Serviços Gerais",
            "Saúde",
            "Educação"
        };
      }
        

        public void InitializeCommands()
        {
            EtapaDoisRegisterClientCommand = new Command(async () => await EtapaDois());
            EtapaTresRegisterClientCommand = new Command(async () => await EtapaTres());
            EtapaQuatroRegisterClientCommand = new Command(async () => await EtapaQuatro());
        }

        public async Task EtapaDois()
        {
            try
            {
                var proximaPagina = new View.Cliente.RegisterClientTwo
                {
                    BindingContext = this
                };


                await Application.Current.MainPage.Navigation.PushAsync(proximaPagina);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                       ("Opss!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }

        }

        public async Task EtapaTres()
        {
            try
            {
                var proximaPagina = new View.Cliente.RegisterClientTree
                {
                    BindingContext = this
                };


                await Application.Current.MainPage.Navigation.PushAsync(proximaPagina);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                       ("Opss!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }

        }

        public async Task EtapaQuatro()
        {
            try
            {
                var proximaPagina = new View.Cliente.RegisterClientFinal
                {
                    BindingContext = this
                };


                await Application.Current.MainPage.Navigation.PushAsync(proximaPagina);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                       ("Opss!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }

        }


        private string nome;
        public string Nome
        {
            get => nome;
            set
            {
                nome = value;
                OnPropertyChanged();
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }

        }

        private string segmento;
        public string Segmento
        {
            get => segmento;
            set
            {
                segmento = value;
                OnPropertyChanged();
            }
        }

        private List<string> segmentos;
        public List<string> Segmentos
        {
            get => segmentos;
            set
            {
                segmentos = value;
                OnPropertyChanged();
            }
        }


    }
}

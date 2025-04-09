using ConectaProApp.Services.Prestador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Prestador
{
    public class RegisterPrestadorViewModel : BaseViewModel
    {
        private PrestadorService pService;

        public ICommand EtapaDoisRegisterPrestadorCommand { get; set; }
        public ICommand EtapaTresRegisterPrestadorCommand { get; set; }
        public ICommand EtapaQuatroRegisterPrestadorCommand { get; set; }
        

        public RegisterPrestadorViewModel()
        {
            pService = new PrestadorService();
            InitializeCommands();
           
        }

        private string nomePrestador;
        public string NomePrestador 
        { 
            get => nomePrestador;
            set
            {
                nomePrestador = value;
                OnPropertyChanged();            }
            }

        private string emailPrestador;
        public string EmailPrestador
        {
            get => emailPrestador;
            set
            {
                emailPrestador = value;
                OnPropertyChanged();
            }
        }

        private string cpfPrestador;
        public string CpfPrestador
        {
            get => cpfPrestador;
            set
            {
                cpfPrestador = value;
                OnPropertyChanged();
            }
        }



        private List<string> habilidades;
        public List<string> Habilidades
        {
            get => habilidades;
            set
            {
                habilidades = value;
                OnPropertyChanged();
            }
        }

        private List<string> especializacao;
        public List<string> Especializacao
        {
            get => especializacao;
            set
            {
                especializacao = value;
                OnPropertyChanged();
            }
        }

        private string descPrestador;
        public string DescPrestador
        {
            get => descPrestador;
            set
            {
                descPrestador = value;
                OnPropertyChanged();
            }
        }

        private string telefonePrestador;
        public string TelefonePrestador
        {
            get => telefonePrestador;
            set
            {
                telefonePrestador = MascararTelefone(RemoverNaoNumericos(value));
                OnPropertyChanged();
            }
        }

        private string MascararTelefone(string input)
        {
            if (input.Length > 11)
                input = input.Substring(0, 11);

            if (input.Length == 11) // celular
                return Convert.ToUInt64(input).ToString(@"\(00\) 00000\-0000");

            else if (input.Length == 10) // fixo
                return Convert.ToUInt64(input).ToString(@"\(00\) 0000\-0000");
            else
                return input;
        }

        private string cepPrestador;
        public string CepPrestador
        {
            get => cepPrestador;
            set
            {
                cepPrestador = MascararCep(RemoverNaoNumericos(value));
                OnPropertyChanged();
            }
        }

        private string MascararCep(string input)
        {
            input = new string(input.Where(char.IsDigit).ToArray());

            if (input.Length > 8)
                input = input.Substring(0, 8);

            if (ulong.TryParse(input, out ulong cepNumerico))
                return cepNumerico.ToString(@"00000\-000");

            return input;
        }

        private int nroResidencia;
        public int NroResidencia
        {
            get => nroResidencia;
            set
            {
                nroResidencia = value;
                OnPropertyChanged();
            }
        }

        private string senhaPrestador;
        public string SenhaPrestador
        {
            get => senhaPrestador;
            set
            {
                senhaPrestador = value;
                OnPropertyChanged();
            }
        }

        private string confirmacaoSenhaPrestador;
        public string ConfirmacaoSenhaPrestador
        {
            get => confirmacaoSenhaPrestador;
            set
            {
                confirmacaoSenhaPrestador = value;
                OnPropertyChanged();
            }
        }

        


        public void InitializeCommands()
        {
            EtapaDoisRegisterPrestadorCommand = new Command(async () => EtapaDois());
            EtapaTresRegisterPrestadorCommand = new Command(async () => EtapaTres());
            EtapaQuatroRegisterPrestadorCommand = new Command(async () => EtapaQuatro());
        }

        public async Task EtapaDois()
        {
            try
            {
                var proximaPagina = new View.Prestador.RegisterPrestadorTwo
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
                var proximaPagina = new View.Prestador.RegisterPrestadortree
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
                var proximaPagina = new View.Prestador.RegisterPrestadorFinal
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

        private string RemoverNaoNumericos(string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }

    }
}

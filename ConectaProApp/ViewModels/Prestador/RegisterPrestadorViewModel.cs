using ConectaProApp.Models;
using ConectaProApp.Services.Prestador;
using ConectaProApp.Services.Validações;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Prestador
{
    public class RegisterPrestadorViewModel : BaseViewModel
    {
        private PrestadorService pService;
        private CepService _cepService;
        
        public ICommand EtapaDoisRegisterPrestadorCommand { get; set; }
        public ICommand EtapaTresRegisterPrestadorCommand { get; set; }
        public ICommand EtapaQuatroRegisterPrestadorCommand { get; set; }
        public ICommand CriarContaPrestadorCommand { get; set; }
        

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

        // Validação de Email
        private bool EmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        private string cpfPrestador;
        public string CpfPrestador
        {
            get => cpfPrestador;
            set
            {
                cpfPrestador = MascararCpf(RemoverNaoNumericos(value));
                OnPropertyChanged();
            }
        }

        private string  MascararCpf(string input)
        {
            // Removendo qualquer caractere que não seja número
            input = new string(input.Where(char.IsDigit).ToArray());

            // Aqui garantimos que não passe de 11 digitos
            if (input.Length > 11)
                input = input.Substring(0, 11);
            // Enquanto o usuario nao digite todos os 11 caracteres, garantimos que apareça os caracteres que ele já digitou.
            if (input.Length < 11)
                return input;

            // Convertendo do tipo string para númerico (Int)
            // O trecho .ToString(...) é onde realizamos a mascara do cpf, ou seja como será
            // exibido para o usuario.
            return Convert.ToUInt64(input).ToString(@"000\.000\.000\-00");


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

        // Validação de telefone
        public bool ValidarTelefone(string telefone)
        {
            return Regex.IsMatch(telefone, @"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$");
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

            if (input.Length < 8)
                return input;

            return Convert.ToUInt64(input).ToString(@"00000\-000");

          
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
            CriarContaPrestadorCommand = new Command(async () => FinalizarCadastro());
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

        public async Task FinalizarCadastro()
        {
            try
            {
                if (!ValidarCampos(out string mensagemErro))
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", mensagemErro, "Ok");
                    return;
                }

                if (!EmailValido(EmailPrestador))
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Email inválido", "Ok");
                    return;
                }

                

                var cepValido = await _cepService.ValidarCepAsync(CepPrestador);

                if (!cepValido)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Cep inválido", "Ok");
                    return;
                }

                if (TelefonePrestador.Length < 10 || !ValidarTelefone(TelefonePrestador))
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Telefone inválido", "Ok");
                }

                var novoPrestador = new Models.Prestador
                {
                    Nome = this.NomePrestador,
                    Email = this.EmailPrestador,
                    Cpf = this.CpfPrestador,
                    Habilidades = this.Habilidades,
                    Especialização = this.Especializacao,
                   // Segmento = this.SegmentoPrestador
                    DescPrestador = this.DescPrestador,
                    Telefone = this.TelefonePrestador,
                    Cep = this.CepPrestador,
                    Nro = this.NroResidencia,
                    Senha = this.SenhaPrestador,
                    TipoUsuario = Models.Enuns.TipoUsuarioEnum.PRESTADOR
                };

                var prestadorRegistrado = await pService.PostRegistrarUsuarioAsync(novoPrestador);

                if (prestadorRegistrado != null && prestadorRegistrado.IdUsuario > 0)
                {
                    await Application.Current.MainPage.DisplayAlert($"Bem Vindo {NomePrestador}", "Cadastro realizado com sucesso, vamos trilhar essa carreira de prestador juntos!", "Ok");

                    Application.Current.MainPage = new NavigationPage(new View.Prestador.HomePrestador());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possível realizar o cadastro.", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"Falha ao cadastrar: {ex.Message}", "Ok");
            }
        }


        public bool ValidarCampos(out string mensagemErro)
        {
            if (string.IsNullOrWhiteSpace(NomePrestador) ||
                    string.IsNullOrWhiteSpace(EmailPrestador) ||
                    string.IsNullOrWhiteSpace(CpfPrestador) ||
                    string.IsNullOrWhiteSpace(CepPrestador) ||
                    string.IsNullOrWhiteSpace(TelefonePrestador) ||
                    NroResidencia == 0 ||
                    string.IsNullOrWhiteSpace(SenhaPrestador) ||
                    string.IsNullOrWhiteSpace(ConfirmacaoSenhaPrestador))
            {
                mensagemErro = "Por favor, preencha todos os campos.";
                return false;
            }
            if (SenhaPrestador != ConfirmacaoSenhaPrestador)
            {
                mensagemErro = "A senha e a confirmação não coincidem";
                return false;
            }

            mensagemErro = string.Empty;
            return true;
        }
        

        private string RemoverNaoNumericos(string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }

    }
}

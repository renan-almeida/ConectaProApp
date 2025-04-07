
using ConectaProApp.Models;
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

    public class RegisterClientViewModel : BaseViewModel
    {
        private ClienteService eService;
        public ICommand EtapaDoisRegisterClientCommand { get; set; }
        public ICommand EtapaTresRegisterClientCommand { get; set; }
        public ICommand EtapaQuatroRegisterClientCommand { get; set; }
        public ICommand CriarContaPrestadorCommand { get; set; }

        private string nomeCliente;
        public string NomeCliente
        {
            get => nomeCliente;
            set
            {
                nomeCliente = value;
                OnPropertyChanged();
            }
        }

        private string emailCliente;
        public string EmailCliente
        {
            get => emailCliente;
            set
            {
                emailCliente = value;
                OnPropertyChanged();
            }
        }

        private string nomeFantasia;
        public string NomeFantasia
        {
            get => nomeFantasia;
            set
            {
                nomeFantasia = value;
                OnPropertyChanged();
            }
        }

        private string cnpj;
        public string Cnpj
        {
            get => cnpj;
            set
            {
                cnpj = MascararCnpj(RemoverNaoNumericos(value));
                OnPropertyChanged();
            }
        }

        private string MascararCnpj(string input)
        {
            if (input.Length > 14) input.Substring(0, 14);
            return Convert.ToUInt64(input).ToString(@"00\.000\.000\/0000\-00");
        }

        private string telefoneCliente;
        public string TelefoneCliente
        {
            get => telefoneCliente;
            set
            {
                telefoneCliente = MascararTelefone(RemoverNaoNumericos(value));
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

        private string cepCliente;
        public string CepCliente
        {
            get => cepCliente;
            set
            {
                cepCliente = MascararCep(RemoverNaoNumericos(value));
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

        private int nroEndCliente;
        public int NroEndCliente
        {
            get => nroEndCliente;
            set
            {
                nroEndCliente = value;
                OnPropertyChanged();
            }
        }

        private string senhaCliente;
        public string SenhaCliente
        {
            get => senhaCliente;
            set
            {
                senhaCliente = value;
                OnPropertyChanged();
            }
        }

        private string confimacaoSenhaCliente;
        public string ConfirmacaoSenhaCliente
        {
            get => confimacaoSenhaCliente;
            set
            {
                confimacaoSenhaCliente = value;
                OnPropertyChanged();
            }
        }

        



        public RegisterClientViewModel()
        {
            eService = new ClienteService();
            InitializeCommands();
        }
       
        public void InitializeCommands()
        {
            EtapaDoisRegisterClientCommand = new Command(async () => await EtapaDois());
            EtapaTresRegisterClientCommand = new Command(async () => await EtapaTres());
            EtapaQuatroRegisterClientCommand = new Command(async () => await EtapaQuatro());
            CriarContaPrestadorCommand = new Command(async () => await FinalizarCadastro());
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

        public async Task FinalizarCadastro()
        {
            try
            {
                if (!ValidarCampos(out string mensagemErro))
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", mensagemErro, "Ok");
                    return;
                }

                var novoCliente = new EmpresaCliente
                {
                    Nome = this.NomeCliente,
                    Email = this.EmailCliente,
                    NomeFantasia = this.NomeFantasia,
                    Cnpj = this.Cnpj,
                    Telefone = this.TelefoneCliente,
                    Cep = this.CepCliente,
                    Nro = this.NroEndCliente,
                    Senha = this.SenhaCliente,
                    TipoUsuario = Models.Enuns.TipoUsuarioEnum.EMPRESA
                };

                var clienteRegistrado = await eService.PostRegistrarUsuarioAsync(novoCliente);

                if (clienteRegistrado != null && clienteRegistrado.IdUsuario > 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Cadastro realizado com sucesso!", "Ok");
                    
                    Application.Current.MainPage = new NavigationPage(new View.Usuario.LoginView());
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
            if (string.IsNullOrWhiteSpace(NomeCliente) ||
                    string.IsNullOrWhiteSpace(EmailCliente) ||
                    string.IsNullOrWhiteSpace(NomeFantasia) ||
                    string.IsNullOrWhiteSpace(Cnpj) ||
                    string.IsNullOrWhiteSpace(CepCliente) ||
                    string.IsNullOrWhiteSpace(TelefoneCliente) ||
                    NroEndCliente == 0 ||
                    string.IsNullOrWhiteSpace(SenhaCliente) ||
                    string.IsNullOrWhiteSpace(ConfirmacaoSenhaCliente))
            {
                mensagemErro = "Por favor, preencha todos os campos.";
                return false;
            }
            if (SenhaCliente != ConfirmacaoSenhaCliente)
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


using ConectaProApp.Models;
using ConectaProApp.Services;
using ConectaProApp.Services.Cliente;
using ConectaProApp.Services.Validações;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Cliente
{

    public class RegisterClientViewModel : BaseViewModel
    {
        private ClienteService eService;
        private readonly CepService _cepService;
        private readonly CnpjService _cnpjService;

        public ICommand EtapaDoisRegisterClientCommand { get; set; }
        public ICommand EtapaTresRegisterClientCommand { get; set; }
        public ICommand EtapaQuatroRegisterClientCommand { get; set; }
        public ICommand CriarContaClienteCommand { get; set; }

        public RegisterClientViewModel()
        {
            eService = new ClienteService();
            _cnpjService = new CnpjService();
            _cepService = new CepService();
            InitializeCommands();

        }

        // spin de carregamento quando o prestador clicar em criar a conta.
        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

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
        
        // Validação de Email
        private bool EmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
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
            
            input = new string(input.Where(char.IsDigit).ToArray());

            // Limita a 14 dígitos de outra forma -> []
            if (input.Length > 14)
                input = input[..14]; 

            // Aplica a máscara apenas se tiver os 14 dígitos
            if (input.Length == 14 && ulong.TryParse(input, out ulong cnpjNumerico))
                return cnpjNumerico.ToString(@"00\.000\.000\/0000\-00");

            return input; // Retorna o número parcial enquanto digita
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

            if (input.Length < 8)
                return input;

            return Convert.ToUInt64(input).ToString(@"00000\-000");


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
       
        public void InitializeCommands()
        {
            EtapaDoisRegisterClientCommand = new Command(async () => await EtapaDois());
            EtapaTresRegisterClientCommand = new Command(async () => await EtapaTres());
            EtapaQuatroRegisterClientCommand = new Command(async () => await EtapaQuatro());
            CriarContaClienteCommand = new Command(async () => await FinalizarCadastro());
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
                isBusy = true;

                if (!ValidarCampos(out string mensagemErro))
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", mensagemErro, "Ok");
                    return;
                }

                if (!EmailValido(EmailCliente))
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Email inválido", "Ok");
                    return;
                }

                var cnpjValido = await _cnpjService.ValidarCnpjAsync(Cnpj);

                if(!cnpjValido)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Cnpj inválido", "Ok");
                    return;
                }

                var cepValido = await _cepService.ValidarCepAsync(CepCliente);

                if(!cepValido)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Cep inválido", "Ok");
                    return;
                }

                if(TelefoneCliente.Length < 10 || !ValidarTelefone(TelefoneCliente))
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Telefone inválido", "Ok");
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

                var clienteRegistrado = await eService.PostRegistrarClienteAsync(novoCliente);

                if (clienteRegistrado != null && clienteRegistrado.IdUsuario > 0)
                {
                    await Application.Current.MainPage.DisplayAlert
                        ($"Bem Vindo {NomeCliente}", "Cadastro realizado com sucesso," +
                        " juntos vamos encontrar soluções rapidas e simples" +
                        " para o seu negócio!", "Ok");

                    Preferences.Set("UsuarioLogado", true);
                    Preferences.Set("TipoUsuario", "CLIENTE");

                    Application.Current.MainPage = new AppShell();
                    await Shell.Current.GoToAsync("//cliente");
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
            finally
            {
                isBusy = false;
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

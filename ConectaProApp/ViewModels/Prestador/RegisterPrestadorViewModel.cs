using ConectaProApp.Models;
using ConectaProApp.Models.Enuns;
using ConectaProApp.Services.Prestador;
using ConectaProApp.Services.Validações;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

namespace ConectaProApp.ViewModels.Prestador
{
    public class RegisterPrestadorViewModel : BaseViewModel
    {
        private PrestadorService pService;
        private CepService _cepService;
        private CpfService _cpfService;
        
        public ICommand EtapaDoisRegisterPrestadorCommand { get; set; }
        public ICommand EtapaTresRegisterPrestadorCommand { get; set; }
        public ICommand EtapaQuatroRegisterPrestadorCommand { get; set; }
        public ICommand CriarContaPrestadorCommand { get; set; }
        public ICommand AdicionarHabilidadeCommand { get; set; }
        public ICommand RemoverHabilidadeCommand { get; set; }
        public ICommand AdicionarEspecializacaoCommand { get; set; }
        public ICommand RemoverEspecializacaoCommand { get; set; }

     
        public RegisterPrestadorViewModel()
        {
            pService = new PrestadorService();
            InitializeCommands();
            CarregarSegmentos();
            Ufs = [.. Enum.GetNames(typeof(UfEnum))];
            TiposPlano = [.. Enum.GetNames(typeof(TipoPlanoEnum))];
            TiposDisponibilidade = [.. Enum.GetNames(typeof(StatusDisponibilidadeEnum))];
           
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

        private DateTime dtNascimento;
        public DateTime DtNascimento
        {
            get => dtNascimento;
            set
            {
                dtNascimento = value;
                OnPropertyChanged();
            }
        }
        
        // Verificando se o Prestador é maior de idade.
        public bool MaiorDeIdade(DateTime dataNascimento)
        {
            // Essa variavel esta calcula a diferença de anos entre o ano atual e o ano de nascimento
            var idade = DateTime.Today.Year - dataNascimento.Year;

            // Ajustamos a idade se a pessoa ainda não fez aniversário esse ano.
            if (dataNascimento.Date > DateTime.Today.AddYears(-idade)) idade--;

            // retornamos verdadeiro caso a idade seja maior ou igual a 18.
            return idade >= 18;
        }

        public List<string> Habilidades { get; set; } = new();

        private string habilidadeAtual;
        public string HabilidadeAtual
        {
            get => habilidadeAtual;
            set
            {
                habilidadeAtual = value;
                OnPropertyChanged();
            }
        }

        private void AdicionarHabilidade()
        {
            if (string.IsNullOrWhiteSpace(HabilidadeAtual)) { 
                Application.Current.MainPage.DisplayAlert("Erro", "Por favor insira uma habilidade", "Ok");
                 return;
            }

            if (Habilidades.Count >= 3)
            {
                Application.Current.MainPage.DisplayAlert("Limite atingido", "Você só pode adicionar até 3 habilidades.", "Ok");
                return;
            }

            if (Habilidades.Contains(HabilidadeAtual.Trim()))
            {
                Application.Current.MainPage.DisplayAlert("Duplicado", "Essa habilidade já foi adicionada", "Ok");
                return;
            }
           
                Habilidades.Add(HabilidadeAtual.Trim());
                HabilidadeAtual = string.Empty; // Limpa o Entry
        }

        private void RemoverHabilidade(string habilidade)
        {
            if (Habilidades.Contains(habilidade))
            {
                Habilidades.Remove(habilidade);
            }
        }



        public List<string> Especializacoes { get; set; } = new();

        private string especializacaoAtual;
        public string EspecializacaoAtual
        {
            get => especializacaoAtual;
            set
            {
                especializacaoAtual = value;
                OnPropertyChanged();
            }
        }

        private void AdicionarEspecializacao()
        {
            if (string.IsNullOrWhiteSpace(EspecializacaoAtual))
            {
                Application.Current.MainPage.DisplayAlert("Erro", "Por favor insira uma especialização", "Ok");
                return;
            }
                
            if (Habilidades.Contains(HabilidadeAtual.Trim()))
            {
                Application.Current.MainPage.DisplayAlert("Duplicado", "Essa habilidade já foi adicionada.", "Ok");
                return;
            }
            Especializacoes.Add(EspecializacaoAtual.Trim());
            EspecializacaoAtual = string.Empty;


        }

        private void RemoverEspecializacao(string especializacao)
        {
            if(Especializacoes.Contains(especializacao))
            {
                Especializacoes.Remove(especializacao);
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

        private string complemento;
        public string Complemento
        {
            get => complemento;
            set
            {
                complemento = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Ufs { get; set; }

        private string ufSelecionada;
        public string UfSelecionada
        {
            get => ufSelecionada;
            set
            {
                ufSelecionada = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> TiposPlano { get; set; }

        private string planoSelecionado;
        public string PlanoSelecionado
        {
            get => planoSelecionado;
            set
            {
                planoSelecionado = value;
                OnPropertyChanged();

            }
        }

        public ObservableCollection<string> TiposDisponibilidade { get; set; }
        private string disponibilidadeSelecionada;
        public string DisponibilidadeSelecionada
        {
            get => disponibilidadeSelecionada;
            set
            {
                disponibilidadeSelecionada = value;
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

        public List<TipoSegmentoEnum> SegmentosSelecionados { get; set; } = new();
        public ObservableCollection<SegmentoSelecionado> Segmentos { get; set; } = new();

        // Carregar seguimentos vindos da Enum (TipoSegmentoEnum)
        private void CarregarSegmentos()
        {
            foreach (var segmento in Enum.GetValues(typeof(TipoSegmentoEnum)).Cast<TipoSegmentoEnum>())
            {
                Segmentos.Add(new SegmentoSelecionado
                {
                    Segmento = segmento,
                    Nome = ObterDescricao(segmento),
                    Selecionado = false
                });
            }
        }

        // Carregar os segmentos em tela para o prestador. 
        private string ObterDescricao(TipoSegmentoEnum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
                return attribute?.Description ?? value.ToString();
        }

        public List<TipoSegmentoEnum> ObterSegmentosSelecionados()
        {
            return Segmentos.Where(s => s.Selecionado).Select(s => s.Segmento).ToList();
        }


        public void InitializeCommands()
        {
            EtapaDoisRegisterPrestadorCommand = new Command(async () => EtapaDois());
            EtapaTresRegisterPrestadorCommand = new Command(async () => EtapaTres());
            EtapaQuatroRegisterPrestadorCommand = new Command(async () => EtapaQuatro());
            CriarContaPrestadorCommand = new Command(async () => FinalizarCadastro());
            AdicionarHabilidadeCommand = new Command(AdicionarHabilidade);
            RemoverHabilidadeCommand = new Command<string>(RemoverHabilidade);
            AdicionarEspecializacaoCommand = new Command(AdicionarEspecializacao);
            RemoverEspecializacaoCommand = new Command<string>(RemoverEspecializacao);
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
                // Spin começa a girar
                IsBusy = true; 

                if (!ValidarCampos(out string mensagemErro))
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", mensagemErro, "Ok");
                    return;
                }

                var cpfLimpo = Regex.Replace(CpfPrestador, "[^0-9]", "");
                var telefoneLimpo = Regex.Replace(TelefonePrestador, "[^0-9]", "");


                Console.WriteLine($"📌 CPF limpado: {CpfPrestador}");
                Console.WriteLine($"📌 Telefone limpado: {TelefonePrestador}");


                if (!EmailValido(EmailPrestador))
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Email inválido", "Ok");
                    return;
                }

                if (cpfLimpo.Length != 11)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "CPF deve conter 11 dígitos numéricos.", "OK");
                    return;
                }

                if (telefoneLimpo.Length < 10 || telefoneLimpo.Length > 11)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Telefone inválido.", "OK");
                    return;
                }


                if (!MaiorDeIdade(DtNascimento))
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "É necessário ser maior de 18 anos para se cadastrar.", "Ok");
                    return;
                }

                SegmentosSelecionados = ObterSegmentosSelecionados();

                // convertendo a string selecionada no picker para uma enum
                Enum.TryParse(UfSelecionada, out UfEnum ufEnum);
                Enum.TryParse(PlanoSelecionado, out TipoPlanoEnum planoEnum);
                Enum.TryParse
                    (DisponibilidadeSelecionada, out StatusDisponibilidadeEnum disponibilidadeEnum);


                var novoPrestador = new PrestadorCreateDTO
                {
                    Nome = this.NomePrestador,
                    Email = this.EmailPrestador,
                    Cpf = cpfLimpo,
                    Habilidades = this.Habilidades,
                    Especialidades = this.Especializacoes,
                    Segmentos = SegmentosSelecionados
                        .Select(segEnum => (int)segEnum)
                        .ToList(),
                    DescPrestador = this.DescPrestador,
                    Telefone = telefoneLimpo, 
                        Cep = this.CepPrestador,
                        Numero = this.NroResidencia,
                        Complemento = this.Complemento,
                        Uf = ufEnum,
                    IdPlano = new Plano
                    {
                        IdPlano = (long)planoEnum
                    },
                    StatusDisponibilidade = disponibilidadeEnum,
                    Senha = this.SenhaPrestador,
                };

                var prestadorRegistrado = await pService.PostRegistrarPrestadorAsync(novoPrestador);

                if (prestadorRegistrado != null)
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Sucesso", "Cadastro concluído! Agora faça login para continuar.", "OK");

                    await Application.Current.MainPage.Navigation.PushAsync(new View.Usuario.LoginView());

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
                // Spin para de girar após o fim do try catch
                IsBusy = false;
            }
        }


        public bool ValidarCampos(out string mensagemErro)
        {
            if (string.IsNullOrWhiteSpace(NomePrestador) ||
                    string.IsNullOrWhiteSpace(EmailPrestador) ||
                    string.IsNullOrWhiteSpace(CpfPrestador) ||
                    string.IsNullOrWhiteSpace(CepPrestador) ||
                    string.IsNullOrWhiteSpace(TelefonePrestador) ||
                    NroResidencia == 0 
                    || string.IsNullOrWhiteSpace(UfSelecionada) ||
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

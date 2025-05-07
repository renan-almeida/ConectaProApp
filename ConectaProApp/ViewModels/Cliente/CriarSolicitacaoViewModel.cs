

using ConectaProApp.Models.Enuns;
using ConectaProApp.Services.Servico;
using ConectaProApp.View.Cliente.CriarSolicitacaoViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Cliente
{
    public class CriarSolicitacaoViewModel: BaseViewModel
    {
        private ServicoService sService;
        public ICommand EtapaDoisCommand { get; set; }
        public ICommand EtapaFinalCommand { get; set; }

        public CriarSolicitacaoViewModel()
        {
            sService = new ServicoService();
            InitializeCommands();
            Segmentos = [.. Enum.GetNames(typeof(TipoSegmentoEnum))];
            NiveisUrgencia = [.. Enum.GetNames(typeof(NvlUrgenciaEnum))];
            FormasPagto = [.. Enum.GetNames(typeof(FormaPagtoEnum))];
        }

        private string titulo;
        public string Titulo
        {
            get => titulo;
            set
            {
                titulo = value;
                OnPropertyChanged();
            }
        }

        private string descricao;
        public string Descricao
        {
            get => descricao;
            set
            {
                descricao = value;
                OnPropertyChanged();
            }
        }

        private string especiailidade;
        public string Especialidade
        {
            get => especiailidade;
            set
            {
                especiailidade = value;
                OnPropertyChanged();            }
        }

        public ObservableCollection<string> Segmentos { get; set; }
        private string segmentoSelecionado;
        public string SegmentoSelecionado
        {
            get => segmentoSelecionado;
            set
            {
                segmentoSelecionado = value;
                OnPropertyChanged();
            }
        }

        private string logradouro;
        public string Logradouro
        {
            get => logradouro;
            set
            {
                logradouro = value;
                OnPropertyChanged();
            }
        }

        private int nro;
        public int Nro
        {
            get => nro;
            set
            {
                nro = value;
                OnPropertyChanged();
            }
        }

        private string cep;
        public string Cep
        {
            get => cep;
            set
            {
                cep = value;
                OnPropertyChanged();
            }
        }

        private decimal valorProposto;
        public decimal ValorProposto
        {
            get => valorProposto;
            set
            {
                valorProposto = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> NiveisUrgencia { get; set; }
        private string nvlUrgencia;
        public string NvlUrgencia
        {
            get => nvlUrgencia;
            set
            {
                nvlUrgencia = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> FormasPagto { get; set; }
        private string formaPagto;
        public string FormaPagto
        {
            get => formaPagto;
            set
            {
                formaPagto = value;
                OnPropertyChanged();
            }
        }

        private byte[] fotoSevicoBytes;
        public byte[] FotoServicoBytes
        {
            get => fotoSevicoBytes;
            set
            {
                fotoSevicoBytes = value;
                OnPropertyChanged();
            }
        }

        private void InitializeCommands()
        {
            EtapaDoisCommand = new Command(async() => await EtapaDois());
            EtapaFinalCommand = new Command(async() =>  await EtapaFinal());
        }

        public async Task EtapaDois()
        {
            try
            {
                var proximaPagina = new CriarSolicitacaoClientTwo
                {
                    BindingContext = this
                };


                await Application.Current.MainPage.Navigation.PushAsync(proximaPagina);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                       ("Erro!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }
        }

        public async Task EtapaFinal()
        {
            try
            {
                var proximaPagina = new CriarSolicitacaoClientFinal
                {
                    BindingContext = this
                };


                await Application.Current.MainPage.Navigation.PushAsync(proximaPagina);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                       ("Erro!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }
        }

        private void ValidarCampos()
        {

        }
    }
}

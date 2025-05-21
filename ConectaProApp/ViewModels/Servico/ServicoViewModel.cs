using ConectaProApp.View.Prestador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ServicoModel = ConectaProApp.Models.Servico; // Alias para a classe Servico

namespace ConectaProApp.ViewModels.Servico
{
    public class ServicoViewModel : BaseViewModel
    {
        private readonly ServicoModel servico; // Usando o alias ServicoModel

        public ServicoViewModel(ServicoModel servico)
        {
            this.servico = servico;
            VerMaisCommand = new Command(() => MostrarDescricao = !MostrarDescricao);
            CandidatarCommand = new Command((async () => CriarOrcamento()));
        }

        public string Nome => servico.Titulo;
        public string StServico => servico.SituacaoServico.ToString();
        public string ValorServico => servico.ValorContratacao.ToString("C");
        public string NvlUrgenciaEnum => servico.NvlUrgenciaEnum.ToString();
        public string NomeSegmento => servico.IdSegmento?.DescSegmento; // Corrigido para DescSegmento
        public string FormaPagamento => servico.FormaPagamento.ToString();
        public string Descricao => servico.Descricao;
        public string Especialidade => servico.IdPrestador?.Especialidades?.FirstOrDefault(); // Ajustado para pegar a primeira especialidade
        public string Logradouro => servico.Logradouro;
        public string Nro => servico.NroEmpresa.ToString();
        public string FotoServico => servico.FotoServico;
        public string NomeFantasia => servico.IdCliente.NomeFantasia;
        public string FotoPerfilEmpresa => servico.IdCliente?.CaminhoFoto;

        public ImageSource ImagemFundoHome
        {
            get
            {
                if (string.IsNullOrEmpty(FotoServico))
                    return ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(FotoServico)));

                if (string.IsNullOrEmpty(FotoPerfilEmpresa))
                    return ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(FotoPerfilEmpresa)));

                return "empresasemfoto.png";

            }
        }

        private bool mostrarDescricao;
        public bool MostrarDescricao
        {
            get => mostrarDescricao;
            set
            {
                if (mostrarDescricao != value)
                {
                    mostrarDescricao = value;
                    OnPropertyChanged(nameof(MostrarDescricao));
                }
            }
        }

        public ICommand VerMaisCommand { get; }
        public ICommand CandidatarCommand { get;}

        private async Task CriarOrcamento()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CandidatarPrestador());
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message + "Não foi possivel se candidatar", "OK");
            }
        }

      

    }
}
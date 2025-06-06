using ConectaProApp.Models;
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
           // CandidatarCommand = new Command((async () => CriarOrcamento()));
        }

        public string Nome => servico.TituloSolicitacao;
        public string StServico => servico.StatusSolicitacao.ToString();
        public string ValorServico => servico.ValorProposto.ToString("C");
        public string NvlUrgenciaEnum => string.IsNullOrEmpty(servico.NvlUrgencia) ? 
            "URGÊNCIA NÃO DEFINIDA" : servico.NvlUrgencia;
        public string NomeSegmento => servico.TipoCategoria?.ToString() ?? "SEM CATEGORIA"; // Corrigido para DescSegmento
        public string FormaPagamento => servico.FormaPagto?.ToString() ?? "SEM FORMA DE PAGAMENTO";
        public string Descricao => servico.DescSolicitacao;
        // public string Especialidade => servico.IdPrestador?.Especialidades?.FirstOrDefault(); // Ajustado para pegar a primeira especialidade
        // public string Logradouro => servico.Logradouro;
        // public string Nro => servico.NroEmpresa.ToString();
        public string NomeFantasia => servico.EmpresaClienteResumoDTO.NomeFantasia;
        public string FotoPerfilEmpresa => string.IsNullOrEmpty(servico?.EmpresaClienteResumoDTO.CaminhoFoto) ? "empresasemfoto.png" : (servico.EmpresaClienteResumoDTO.CaminhoFoto);

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
        public ICommand CandidatarCommand { get; }

        /*private async Task CriarOrcamento()
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
        */


    }
}
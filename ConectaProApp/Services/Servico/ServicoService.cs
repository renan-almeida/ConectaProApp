
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;
using ConectaProApp.Converters;
using ConectaProApp.Models;
using ConectaProApp.Models.Enuns;
using Newtonsoft.Json;
using ServicoModel = ConectaProApp.Models.Servico;

namespace ConectaProApp.Services.Servico
{
    public class ServicoService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "https://conectapro-api.azurewebsites.net";
        private static readonly HttpClient client = new HttpClient();
        private readonly ApiService _apiService;

        public ServicoService()
        {
            _request = new Request();
            _apiService = new ApiService();
        }

        // Tela de busca de prestador
        public async Task<List<ServicoModel>> BuscarServicoAsync(string termo)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                System.Diagnostics.Debug.WriteLine($"Token: {token}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                const string buscaServicoEndpoint = "/busca-solicitacoes";
                termo = Uri.EscapeDataString(termo);

                var response = await client.GetAsync($"{apiUrlBase}{buscaServicoEndpoint}?termo={termo}");
                System.Diagnostics.Debug.WriteLine("Requisição para URL: " + response);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ServicoModel>>(json);
                }
                else
                {
                    // Caso a requisição falhe, capturamos o código de status e a mensagem de erro
                    var statusCode = response.StatusCode;
                    var errorMessage = await response.Content.ReadAsStringAsync();

                    System.Diagnostics.Debug.WriteLine($"Erro na requisição. Status: {statusCode}, Mensagem: {errorMessage}");
                    // Logando para diagnóstico

                    // Exibindo a mensagem de erro para o usuário
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current.MainPage.DisplayAlert("Erro", $"Erro ao buscar serviços: {errorMessage}", "OK");
                    });
                }
            }
            catch (Exception ex)
            {
                // Captura de exceção e log do erro
                Debug.WriteLine($"Erro ao buscar serviços: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                await Application.Current.MainPage.DisplayAlert("Erro", "Ocorreu um erro inesperado. Tente novamente.", "OK");
            }

            return new List<ServicoModel>();
        }
      

        public async Task<List<ServicoModel>> BuscarServicoPorCategoriaAsync(string categoria)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                System.Diagnostics.Debug.WriteLine($"Token: {token}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                const string buscaServicoEndpoint = "/busca-solicitacoes";

                //busca-solicitacoes/uf=${}&termo=${}

                categoria = Uri.EscapeDataString(categoria);

                var response = await client.GetAsync($"{apiUrlBase}{buscaServicoEndpoint}?categoria={categoria}");
                System.Diagnostics.Debug.WriteLine("Requisição para URL: " + $"{apiUrlBase}{buscaServicoEndpoint}?categoria={categoria}");

                System.Diagnostics.Debug.WriteLine("Resposta da API: " + response);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("JSON da API:\n" + json);
                    return JsonConvert.DeserializeObject<List<ServicoModel>>(json);
                }
                else
                {
                    Debug.WriteLine($"Erro na requisição. StatusCode: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro ao buscar serviço: " + ex.Message);
            }

            return new List<ServicoModel>();
        }

        // Exibe ao prestador apenas servicos com base na sua UF
        public async Task<List<ServicoHomeDTO>> BuscarSolictacaoPorUfAsync(string uf)
        {
            try
            {
                // Verificar se o token existe
                var token = await SecureStorage.GetAsync("token");
                if (string.IsNullOrEmpty(token))
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Token de autenticação inválido ou expirado.", "OK");
                    return new List<ServicoHomeDTO>(); // Retorna uma lista vazia se o token não for válido
                }
                System.Diagnostics.Debug.WriteLine($"Token: {token}");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Verificar se o parâmetro "uf" está válido
                if (string.IsNullOrEmpty(uf))
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "UF não pode ser vazio.", "OK");
                    return new List<ServicoHomeDTO>(); // Retorna uma lista vazia se "uf" não for válido
                }

                const string urlComplementar = "/busca-solicitacoes";
                uf = Uri.EscapeDataString(uf);

                var requestUrl = $"{apiUrlBase}{urlComplementar}?uf={uf}"; // Adicionando o parâmetro "uf" à URL
                System.Diagnostics.Debug.WriteLine("Requisição para URL: " + requestUrl); // Logando a URL para diagnóstico

                var response = await client.GetAsync(requestUrl);

                // Verificando o código de status da resposta
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ServicoHomeDTO>>(json);
                }
                else
                {
                    // Caso a requisição falhe, capturamos o código de status e a mensagem de erro
                    var statusCode = response.StatusCode;
                    var errorMessage = await response.Content.ReadAsStringAsync();

                    System.Diagnostics.Debug.WriteLine($"Erro na requisição. Status: {statusCode}, Mensagem: {errorMessage}");
                    // Logando para diagnóstico

                    // Exibindo a mensagem de erro para o usuário
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current.MainPage.DisplayAlert("Erro", $"Erro ao buscar serviços: {errorMessage}", "OK");
                    });
                }
            }
            catch (Exception ex)
            {
                // Captura de exceção e log do erro
                Debug.WriteLine($"Erro ao buscar serviços: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                await Application.Current.MainPage.DisplayAlert("Erro", "Ocorreu um erro inesperado. Tente novamente.", "OK");
            }

            // Retorna uma lista vazia caso haja falha
            return new List<ServicoHomeDTO>();
        }


        // Exibe Empresa apenas prestadores correspondentes a sua UF

        public async Task<List<PrestadorResponseBuscaDTO>> BuscarPrestadorUfAsync(string uf)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                const string urlComplementar = "/busca-prestadores";
                uf = Uri.EscapeDataString(uf);
                var response = await client.GetAsync($"{apiUrlBase}{urlComplementar}?uf={uf}");
                System.Diagnostics.Debug.WriteLine("resposta: " + response); // Logando a URL para diagnóstico

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Json: " + json);
                    return JsonConvert.DeserializeObject<List<PrestadorResponseBuscaDTO>>(json);
                }
            }
            catch (Exception ex)
            {
                // Captura de exceção e log do erro
                Debug.WriteLine($"Erro ao buscar serviços: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            }

            return new List<PrestadorResponseBuscaDTO>();
        }



        public async Task<ServicoCreateDTO> PostRegistrarServicoAsync(ServicoCreateDTO s)
        {
            var token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string urlComplementar = "/solicitacao/registro";

            var servicoRegistrado = await PostAsyncFlexToken<ServicoCreateDTO, ServicoCreateDTO>(
                apiUrlBase + urlComplementar, s, token);

            return servicoRegistrado;
        }

        public async Task<PropostaCreateDTO> EnviarPropostaAsync(PropostaCreateDTO proposta)
        {
            var token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Debug.WriteLine("Token: " + token);

            const string url = "/servico/proposta-direta";

            var response = await PostAsyncFlexToken<PropostaCreateDTO, PropostaCreateDTO>
                (apiUrlBase + url, proposta, token);

            Debug.WriteLine("Resposta: " + response);

            return response;
        }


        public async Task AceitarPropostaAsync(int idServicoAceito, int idSolicitacao)
        {
            var token = await SecureStorage.GetAsync("token");


            // 1. Atualizar status da proposta aceita
            var contentAceita = new
            {
                statusOrcamento = "ACEITO"
            };

            var responseAceita = await _request.PutAsync(
                $"{apiUrlBase}/servicos/{idServicoAceito}/status", contentAceita, token);

            if (!responseAceita.IsSuccessStatusCode)
                throw new Exception("Erro ao aceitar proposta");

            // 2. Buscar outras propostas da mesma solicitação
            var propostas = await BuscarPropostasPorSolicitacaoAsync(idSolicitacao);

            foreach (var proposta in propostas.Where(p => p.IdSolicitacao != idServicoAceito))
            {
                var contentRecusa = new
                {
                    statusOrcamento = "RECUSADO"
                };

                var responseRecusa = await _request.PutAsync(
                    $"{apiUrlBase}/servicos/{proposta.IdSolicitacao}/status", contentRecusa, token);

                if (!responseRecusa.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro ao recusar proposta {proposta.IdSolicitacao}");
                }
            }
        }

        public async Task<List<ServicoModel>> BuscarPropostasPorSolicitacaoAsync(int idSolicitacao)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{apiUrlBase}/servicos/propostas?solicitacaoId={idSolicitacao}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ServicoModel>>(json);
                }
                else
                {
                    Console.WriteLine($"Erro ao buscar propostas: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar propostas: " + ex.Message);
            }

            return new List<ServicoModel>();
        }

        public async Task<PropostaCreateDTO> BuscarSolicitacaoPorIdAsync(int id)
        {

            var token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            const string buscaServicoEndpoint = "/solicitacao/";
            var resposta = await client.GetAsync($"{apiUrlBase}{buscaServicoEndpoint}{id}");
            Debug.WriteLine("resposta da API: " + resposta);

            if (resposta.IsSuccessStatusCode)
            {
                var json = await resposta.Content.ReadAsStringAsync();
                Debug.WriteLine("Json devolvido: " + json);
                return JsonConvert.DeserializeObject<PropostaCreateDTO>(json);
            }

            return null;
        }



        public async Task<ServicoDTO> PagamentoAsync(int idServico)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                Debug.WriteLine($"🔵 Token obtido: {token}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Buscar o serviço atual para validar o status
                var responseBusca = await client.GetAsync($"{apiUrlBase}/servico/{idServico}");

                if (!responseBusca.IsSuccessStatusCode)
                    throw new Exception("Erro ao buscar o serviço para validação de status.");

                var jsonBusca = await responseBusca.Content.ReadAsStringAsync();
                var servicoAtual = JsonConvert.DeserializeObject<ServicoDTO>(jsonBusca, new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter>
                    {
                    new DateTimeFromStringNewtonsoftConverter("dd/MM/yyyy - HH:mm"),
                    new DateTimeNullableFromStringNewtonsoftConverter("dd/MM/yyyy - HH:mm"),
                    new DateOnlyFromStringNewtonsoftConverter("dd/MM/yyyy"),
                    new DecimalFromStringNewtonsoftConverter()
                    },
                    Culture = new CultureInfo("pt-BR")
                });

                // Verifica se o status atual permite pagamento
                if (servicoAtual.SituacaoServico != StatusServicoEnum.PENDENTE_PAGTO)
                    throw new Exception("Este serviço não está com status pendente de pagamento.");

                // Realizar o pagamento
                var contentPago = new
                {
                    SituacaoServico = StatusServicoEnum.PENDENTE_PAGTO,
                    DataPagamento = DateTime.Now.ToString("dd/MM/yyyy - HH:mm"),
                };

                var responsePago = await _request.PutAsync($"{apiUrlBase}/servico/{idServico}/pagar", contentPago, token);

                if (responsePago.IsSuccessStatusCode)
                {
                    var json = await responsePago.Content.ReadAsStringAsync();
                    Debug.WriteLine("Json atualizado: " + json);

                    var servicoAtualizado = JsonConvert.DeserializeObject<ServicoDTO>(json, new JsonSerializerSettings
                    {
                        Converters = new List<JsonConverter>
                        {
                            new DateTimeFromStringNewtonsoftConverter("dd/MM/yyyy - HH:mm"),
                            new DateTimeNullableFromStringNewtonsoftConverter("dd/MM/yyyy - HH:mm"),
                            new DateOnlyFromStringNewtonsoftConverter("dd/MM/yyyy"),
                            new DecimalFromStringNewtonsoftConverter()
                        },
                        Culture = new CultureInfo("pt-BR")
                    });

                    Debug.WriteLine("Servico atualizado: " + servicoAtualizado);

                    return servicoAtualizado;
                }

                throw new Exception("Erro ao processar o pagamento. Verifique os dados e tente novamente.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro ao pagar serviço: " + ex.Message);
                throw new Exception("Erro ao pagar serviço. Tente novamente mais tarde.");
            }
        }

    }
}

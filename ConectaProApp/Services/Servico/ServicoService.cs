
using Azure;
using ConectaProApp.Converters;
using ConectaProApp.Models;
using ConectaProApp.Models.Enuns;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
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
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Debug.WriteLine("Token: " + token);


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



        public async Task<ServicoDTO> PagamentoAsync(int IdServico)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                Debug.WriteLine($"🔵 Token obtido: {token}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Buscar o serviço atual para validar o status
                var responseBusca = await client.GetAsync($"{apiUrlBase}/servico/{IdServico}");

                if (!responseBusca.IsSuccessStatusCode)
                    throw new Exception("Erro ao buscar o serviço para validação de status.");

                var jsonBusca = await responseBusca.Content.ReadAsStringAsync();
                Debug.WriteLine("Json: " + jsonBusca);
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
                Debug.WriteLine("servico: " + servicoAtual);

                // Verifica se o status atual permite pagamento
                if (servicoAtual.SituacaoServico != StatusServicoEnum.PENDENTE_PAGTO)
                    throw new Exception("Este serviço não está com status pendente de pagamento.");

                // Realizar o pagamento
                var contentPago = new
                {
                    SituacaoServico = StatusServicoEnum.PENDENTE_PAGTO,
                    DataPagamento = DateTime.Now.ToString("dd/MM/yyyy - HH:mm"),
                };
                // Serializar o conteúdo em JSON//
                var jsonContent = JsonConvert.SerializeObject(contentPago);

                // Criar o HttpContent (StringContent) com o JSON
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var responsePago = await client.PutAsync($"{apiUrlBase}/servico/{IdServico}/pagar", content);

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

        //Somente para prestador de serviço
        public async Task<ServicoDTO> IniciarServicoAsync(int IdServico)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                Debug.WriteLine($"🔵 Token obtido: {token}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Buscar o serviço atual para validar o status
                var responseBusca = await client.GetAsync($"{apiUrlBase}/servico/{IdServico}");

                if (!responseBusca.IsSuccessStatusCode)
                    throw new Exception("Erro ao buscar o serviço para validação de status.");

                var jsonBusca = await responseBusca.Content.ReadAsStringAsync();
                Debug.WriteLine("Json: " + jsonBusca);
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
                Debug.WriteLine("servico: " + servicoAtual);

                // Verifica se o status atual permite INICIAR
                if (servicoAtual.SituacaoServico != StatusServicoEnum.PENDENTE_INICIO)
                    throw new Exception("Este serviço não está com status aceitável.");

                // Realizar o pagamento
                var contentInicio = new
                {
                    SituacaoServico = StatusServicoEnum.EM_EXECUCAO,
                    DataInicio = DateTime.Now.ToString("dd/MM/yyyy - HH:mm"),
                };
                // Serializar o conteúdo em JSON//
                var jsonContent = JsonConvert.SerializeObject(contentInicio);

                // Criar o HttpContent (StringContent) com o JSON
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{apiUrlBase}/servico/{IdServico}/iniciar", content);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
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

                throw new Exception("Erro ao iniciar. Verifique os dados e tente novamente.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro ao inciar serviço: " + ex.Message);
                throw new Exception("Erro ao iniciar serviço. Tente novamente mais tarde.");
            }
        }

        //Somente para prestador de serviço
        public async Task<ServicoDTO> FinalizarServicoAsync(int IdServico)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                Debug.WriteLine($"🔵 Token obtido: {token}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Buscar o serviço atual para validar o status
                var responseBusca = await client.GetAsync($"{apiUrlBase}/servico/{IdServico}");

                if (!responseBusca.IsSuccessStatusCode)
                    throw new Exception("Erro ao buscar o serviço para validação de status.");

                var jsonBusca = await responseBusca.Content.ReadAsStringAsync();
                Debug.WriteLine("Json: " + jsonBusca);
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
                Debug.WriteLine("servico: " + servicoAtual);

                // Verifica se o status atual permite FINALIZACAO
                if (servicoAtual.SituacaoServico != StatusServicoEnum.EM_EXECUCAO)
                    throw new Exception("Este serviço não está com status aceitável.");

                // Realizar o pagamento
                var contentInicio = new
                {
                    SituacaoServico = StatusServicoEnum.PENDENTE_CONFIRMAR_FINALIZACAO,
                    DataFinalizacao = DateTime.Now.ToString("dd/MM/yyyy - HH:mm"),
                };
                // Serializar o conteúdo em JSON//
                var jsonContent = JsonConvert.SerializeObject(contentInicio);

                // Criar o HttpContent (StringContent) com o JSON
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{apiUrlBase}/servico/{IdServico}/finalizar", content);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
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

                throw new Exception("Erro ao finalizar. Verifique os dados e tente novamente.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro ao finaizar serviço: " + ex.Message);
                throw new Exception("Erro ao finalizar serviço. Tente novamente mais tarde.");
            }
        }

        //Somente para cliente
        public async Task<ServicoDTO> ConfirmarFinalizacaoServicoAsync(int IdServico)
        {
            try
            {

                var token = await SecureStorage.GetAsync("token");
                Debug.WriteLine($"🔵 Token obtido: {token}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Buscar o serviço atual para validar o status
                var responseBusca = await client.GetAsync($"{apiUrlBase}/servico/{IdServico}");

                if (!responseBusca.IsSuccessStatusCode)
                    throw new Exception("Erro ao buscar o serviço para validação de status.");

                var jsonBusca = await responseBusca.Content.ReadAsStringAsync();
                Debug.WriteLine("Json: " + jsonBusca);
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
                Debug.WriteLine("servico: " + servicoAtual);

                // Verifica se o status atual permite FINALIZACAO
                if (servicoAtual.SituacaoServico != StatusServicoEnum.FINALIZADO)
                    throw new Exception("Este serviço não está com status aceitável.");

                // Realizar o pagamento
                var contentInicio = new
                {
                    SituacaoServico = StatusServicoEnum.FINALIZADO,
                    DataFinalizacao = DateTime.Now.ToString("dd/MM/yyyy - HH:mm"),
                };
                // Serializar o conteúdo em JSON//
                var jsonContent = JsonConvert.SerializeObject(contentInicio);

                // Criar o HttpContent (StringContent) com o JSON
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{apiUrlBase}/servico/{IdServico}/finalizar", content);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
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

                throw new Exception("Erro ao confirmar finalização. Verifique os dados e tente novamente.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro ao confirmar finalização: " + ex.Message);
                throw new Exception("Erro ao confirmar finalização. Tente novamente mais tarde.");
            }
        }

        //não está funcionando mas é importante para o prestador
        public async Task<ServicoDTO> AceitarServicoAsync(int idServico)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = new { SituacaoServico = StatusServicoEnum.PENDENTE_INICIO };
                var jsonContent = JsonConvert.SerializeObject(content);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{apiUrlBase}/servico/{idServico}/aceitar", httpContent);

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Erro ao aceitar serviço.");

                var json = await response.Content.ReadAsStringAsync();

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

                return servicoAtualizado;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao aceitar serviço: " + ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;

namespace WCFWebClient.Dominio.Dominio
{
    public class InvocadorDeServico
    {
        public object ObjetoParametro { get; set; }

        public Resultado Retorno { get; set; }

        public Resultado InvoqueMetodo(string nomeMetodo, object[] parametro, string nomeInterface, string endpointServico)
        {
            var assembly = new ExtratorDeInformacaoDeAssembly("WCFTestClient.Dominio");

            var tipoInterface = assembly.RetornaTipoInterface(nomeInterface);

            var classe = assembly.RetornaClassesValidas(tipoInterface);

            var servico = Activator.CreateInstance(classe.FirstOrDefault());
           
            string endpointTratado;

            var nomeInterfaceSemVersao = nomeInterface.Split('_')[0];

            if (endpointServico == "")
            {
                endpointTratado = "http://" + System.Environment.MachineName + ":1008/lg.com.br/svc/" + nomeInterfaceSemVersao;
            }
            else
            {
                endpointTratado = "http://" + endpointServico + ":1008/lg.com.br/svc/" + nomeInterfaceSemVersao;
            }
            //Address

            var endpoint = servico.GetType().GetProperty("Endpoint").GetValue(servico, null);

            endpoint.GetType().GetProperty("Address").SetValue(endpoint, new EndpointAddress(new Uri(endpointTratado), addressHeaders: null), null);

            var tipoParametro = assembly.RetornaTipoDoParametroDoMetodo(classe.FirstOrDefault(), nomeMetodo);

            object autenticador = assembly.RetornaObjetoAutenticador(nomeInterface, nomeMetodo);

            var tipoAutenticador = assembly.RetornaTipoAutenticador(nomeInterface, nomeMetodo);

            var tipoToken = autenticador.GetType().GetProperty("TokenUsuario").PropertyType.FullName.ToString();

            object token = assembly.CriaObjeto(tipoToken);

            token.GetType().GetProperty("Usuario").SetValue(token, "1", null);

            token.GetType().GetProperty("Senha").SetValue(token, "FPW", null);

            autenticador.GetType().GetProperty("TokenUsuario").SetValue(autenticador, token, null);
            try
            {
                servico.GetType().GetMethod("Open").Invoke(servico, null);

                object[] parametros = (object[])Array.CreateInstance(typeof(object), (parametro.Length + 2));

                parametros[0] = null;

                parametros[1] = Convert.ChangeType(autenticador, tipoAutenticador);

                for (int i = 0; i < parametro.Length; i++)
                {
                    parametros[i + 2] = Convert.ChangeType(parametro[i], tipoParametro[i]);
                }

                object resultado = servico.GetType().GetMethod(nomeMetodo).Invoke(servico, parametros);

                if (resultado != null)
                {
                    Type tipoRetorno = resultado.GetType();

                    this.Retorno = new Resultado((tipoRetorno.ToString()).Replace("WCFTestClient.Dominio.", ""));

                    if (tipoRetorno.IsArray || tipoRetorno.GetInterfaces().Contains(typeof(IList<>)))
                    {
                        if (resultado != null)
                        {
                            foreach (object item in (Array)resultado)
                            {
                                AdicionaPropriedadeEValorDoObjetoComoMensagem(item);
                                this.Retorno.AdicionaMensagem("\n");
                            }
                        }
                    }
                    else
                    {
                        AdicionaPropriedadeEValorDoObjetoComoMensagem(resultado);
                    }
                }
                else
                {
                    this.Retorno = new Resultado("Sucesso!");

                    this.Retorno.AdicionaMensagem("O Serviço foi executado com sucesso e o retorno é null!");
                }
            }
            catch (Exception e)
            {
                Type exceptionType = e.InnerException.GetType();

                if (exceptionType.IsGenericType && exceptionType.GetGenericTypeDefinition() == typeof(FaultException<>))
                {
                    PropertyInfo property = exceptionType.GetProperty("Detail");
                    object propertyValue = property.GetValue(e.InnerException, null);

                    if (propertyValue.GetType().Name == "Inconsistencia")
                    {
                        this.Retorno = new Resultado("Inconsistência");

                        var mensagem = propertyValue.GetType().GetProperty("Mensagem").GetValue(propertyValue, null);

                        this.Retorno.AdicionaMensagem((string)mensagem);
                    }
                    else
                    {
                        if (propertyValue.GetType().Name == "ListaDeInconsistencias")
                        {
                            this.Retorno = new Resultado("Lista de Inconsistências");

                            var inconsistencias = propertyValue.GetType().GetProperty("Inconsistencias").GetValue(propertyValue, null);

                            foreach (object item in (Array)inconsistencias)
                            {
                                var mensagem = item.GetType().GetProperty("Mensagem").GetValue(item, null);

                                this.Retorno.AdicionaMensagem((string)mensagem);
                            }
                        }
                        else
                        {
                            if (propertyValue.GetType().Name == "FalhaInternaDeExecucao")
                            {
                                this.Retorno = new Resultado("Falha Interna De Execucao");

                                this.Retorno.AdicionaMensagem(e.InnerException.Message);
                            }
                            else
                            {
                                if (propertyValue.GetType().Name == "FalhaDePermissao")
                                {
                                    this.Retorno = new Resultado("Falha De Permissao");

                                    this.Retorno.AdicionaMensagem(e.InnerException.Message);
                                }
                            }
                        }
                    }
                }
            }

            servico.GetType().GetMethod("Close").Invoke(servico, null);

            if (this.Retorno == null)
            {
                this.Retorno = new Resultado("Falha ao iniciar o Serviço");

                this.Retorno.AdicionaMensagem("Houve um problema ao tentar iniciar o serviço solicitado, por favor verifique se as configurações estão corretas e se o serviço está ativo.");
            }

            return this.Retorno;
        }

        public void AdicionaPropriedadeEValorDoObjetoComoMensagem(object item)
        {
            var propriedades = item.GetType().GetProperties();

            this.Retorno.AdicionaMensagem(item.GetType().Name + ": <ul>");

            foreach (PropertyInfo propriedade in propriedades)
            {
                if (propriedade.PropertyType.IsPrimitive
                    || propriedade.PropertyType == typeof(string)
                    || propriedade.PropertyType == typeof(DateTime)
                    || (propriedade.PropertyType.IsGenericType && propriedade.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    var valorPropriedade = item.GetType().GetProperty(propriedade.Name).GetValue(item, null);

                    if (valorPropriedade == null)
                    {
                        valorPropriedade = "null";
                    }

                    string mensagem = propriedade.Name + ": " + valorPropriedade.ToString() + ".";

                    this.Retorno.AdicionaMensagem(mensagem);
                }
                else
                {
                    if (propriedade.PropertyType.Name != "ExtensionDataObject")
                    {
                        this.Retorno.AdicionaMensagem(propriedade.Name + ": <ul>");

                        if (propriedade.PropertyType.IsArray || propriedade.PropertyType.GetInterfaces().Contains(typeof(IList<>)))
                        {
                            var lista = item.GetType().GetProperty(propriedade.Name).GetValue(item, null);

                            if (lista != null)
                            {
                                foreach (var itemDaLista in (Array)lista)
                                {
                                    AdicionaPropriedadeEValorDoObjetoComoMensagem(itemDaLista);
                                }
                            } else {
                                this.Retorno.AdicionaMensagem("null");
                            }
                        }
                        else
                        {
                            var valor = item.GetType().GetProperty(propriedade.Name).GetValue(item, null);

                            if (valor == null) {
                                this.Retorno.AdicionaMensagem(String.Concat("null"));
                            } else {
                                AdicionaPropriedadeEValorDoObjetoComoMensagem(valor);
                            }
                        }

                        this.Retorno.AdicionaMensagem("</ul>");
                    }
                }
            }
            this.Retorno.AdicionaMensagem("</ul>");
        }
    }
}

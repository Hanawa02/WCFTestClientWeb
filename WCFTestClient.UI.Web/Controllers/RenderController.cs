using System;
using System.Collections.Generic;
using System.Web.Mvc;
//using WCFWebClient.Dominio.Contratos.Service;
using WCFWebClient.Aplicacao.Servico;
using WCFWebClient.Dominio.Dominio;


namespace WCFWebClient.UI.Web.Controllers
{
    public class RenderController : Controller
    {
        public ActionResult Index(string nomeInterface, string nomeMetodo)
        {
            var service = new ServiceApp();

            var serviceAssembly = new ServiceExtratorDeInformacaoDeAssembly();

            var serviceCampoObjeto = new ServiceCampoObjeto();

            var serviceData = new ServiceData();

            var objetosDosParametros = serviceAssembly.RetornaObjetosDoParametroDoMetodo("WCFTestClient.Dominio", nomeInterface, nomeMetodo);

            var campoObjeto = service.CriaCampoObjeto(objetosDosParametros, nomeMetodo);

            ViewBag.NomeMetodo = nomeMetodo;

            ViewBag.NomeInterface = nomeInterface;

            ViewBag.ListaPerfis = serviceData.RetornaListaDePerfis(nomeMetodo, nomeInterface);

            return View(campoObjeto);
        }

        public PartialViewResult IndexPartialView(Type tipo)
        {
            var service = new ServiceApp();

            return PartialView(service.GetAllByObject(tipo));
        }

        [HttpPost]
        public JsonResult InvocarServico(FormCollection parametros)
        {
            var service = new ServiceApp();

            var serviceCampoObjeto = new ServiceCampoObjeto();

            var serviceAssembly = new ServiceExtratorDeInformacaoDeAssembly();

            var nomeMetodo = parametros["nome-metodo"];

            var nomeInterface = parametros["nome-interface"];

            var endpointServico = parametros["endpoint-servico"];

            var objetos = serviceAssembly.RetornaObjetos("WCFTestClient.Dominio", nomeInterface, nomeMetodo);

            var propriedades = serviceAssembly.RetornaObjetosDoParametroDoMetodo("WCFTestClient.Dominio", nomeInterface, nomeMetodo);

            foreach (var nomePropriedade in parametros.AllKeys)
            {
                if (nomePropriedade != "nome-metodo"
                    && nomePropriedade != "nome-interface"
                    && nomePropriedade != "descricao-perfil"
                    && nomePropriedade != "perfil"
                    && nomePropriedade != "endpoint-servico")
                {
                    var valor = parametros[nomePropriedade];

                    if (valor != "")
                    {
                        for (int i = 0; i < objetos.Length; i++)
                        {
                            var nomeObjeto = propriedades[i].Name;

                            string inicioParametro;

                            if (nomeObjeto.Length > nomePropriedade.Length)
                            {
                                inicioParametro = "";
                            }
                            else
                            {
                                inicioParametro = nomePropriedade.Substring(0, nomeObjeto.Length);
                            }

                            if (nomeObjeto == inicioParametro)
                            {
                                service.DefinePropriedade(ref objetos[i], nomePropriedade, valor);

                                break;
                            }
                        }
                    }
                }
            }

            var invocadorDeServico = new ServiceInvocadorDeServico();

            var resultado = invocadorDeServico.InvoqueMetodo(nomeMetodo, objetos, nomeInterface, endpointServico);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SalvarPerfil(FormCollection parametros)
        {
            var servico = new ServiceData();

            var nomeMetodo = parametros["nome-metodo"];

            var nomeInterface = parametros["nome-interface"];

            var descricao = parametros["descricao-perfil"];

            var listaParametros = new Dictionary<string, string>();

            foreach (var nomePropriedade in parametros.AllKeys)
            {
                if (nomePropriedade != "nome-metodo"
                    && nomePropriedade != "nome-interface"
                    && nomePropriedade != "descricao-perfil"
                    && nomePropriedade != "perfil"
                    && nomePropriedade != "endpoint-servico")
                {
                    var valor = parametros[nomePropriedade];

                    if (valor != "")
                    {
                        listaParametros.Add(nomePropriedade, valor);
                    }
                }
            }

            var perfil = servico.SalvaPerfil(descricao, nomeMetodo, nomeInterface, listaParametros);

            var resultado = new Resultado("");

            if (perfil == true)
            {
                resultado.TipoDoReultado = "Sucesso!";

                resultado.AdicionaMensagem("O perfil foi salvo com sucesso!");
            }
            else
            {
                resultado.TipoDoReultado = "Erro Desconhecido!";

                resultado.AdicionaMensagem("Não foi possível salvar o perfil!");
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeletarPerfil(int perfilId)
        {
            var servico = new ServiceData();

            var perfil = servico.DeletaPerfil(perfilId);

            var resultado = new Resultado("");

            if (perfil == true)
            {
                resultado.TipoDoReultado = "Sucesso!";

                resultado.AdicionaMensagem("O perfil foi deletado com sucesso!");
            }
            else
            {
                resultado.TipoDoReultado = "Erro Desconhecido!";

                resultado.AdicionaMensagem("Não foi possível deletar o perfil!");
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Catalogo()
        {
            var serviceAssembly = new ServiceExtratorDeInformacaoDeAssembly();

            var listaServicos = serviceAssembly.RetornaInterfacesValidas("WCFTestClient.Dominio");

            return View(listaServicos);
        }

        public JsonResult SelecionaServico(string nomeInterface)
        {
            var serviceAssembly = new ServiceExtratorDeInformacaoDeAssembly();

            var interfaceSelecionada = serviceAssembly.RetornaTipoInterface("WCFTestClient.Dominio", nomeInterface);

            var listaMetodos = serviceAssembly.RetornaMetodosString("WCFTestClient.Dominio", interfaceSelecionada);

            return Json(listaMetodos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SelecionaPerfil(string idPerfil, string nomeInterface, string nomeMetodo)
        {
            if (idPerfil != "")
            {
                var service = new ServiceData();

                var listaParametros = service.RetornaDictionaryParametrosDoPerfil(Int32.Parse(idPerfil));

                return Json(listaParametros, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AtualizaListaDePerfil(string nomeMetodo, string nomeInterface)
        {
            var serviceData = new ServiceData();

            var listaDePerfil = serviceData.RetornaListaDePerfis(nomeMetodo, nomeInterface);

            return Json(listaDePerfil, JsonRequestBehavior.AllowGet);
        }
    }
}

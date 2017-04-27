using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;

namespace WCFWebClient.Dominio.Dominio
{
    /* Tipos Trabalhados Especialmente
         *  - Primitivos (int, double, byte, sbyte, uint, boolean, char, single)
         *  - String
         *  - DateTime
         *  - List (IList)
         *  - Dictionary (IDictionary)
         *  - Nullable
         */

    public class ReflectionUtil
    {
        private Type TipoObjeto;

        public ReflectionUtil(Type tipoObjeto)
        {
            TipoObjeto = tipoObjeto;
        }

        public CampoObjeto CriaCampoObjeto(ParameterInfo[] objetos, string nomeMetodo)
        {
            var campoObjetoRetorno = new CampoObjeto(typeof(object), nomeMetodo, "CampoObjetoParametroGeral");

            campoObjetoRetorno.Lista = new List<CampoObjeto>();

            foreach (var parametro in objetos)
            {
                if (parametro.ParameterType.IsPrimitive || parametro.ParameterType == typeof(string) || parametro.ParameterType == typeof(DateTime))
                {
                    CampoObjeto campoObjeto = new CampoObjeto(parametro.ParameterType, parametro.Name, "");

                    campoObjeto.CaminhoGenealogico = parametro.Name;

                    campoObjetoRetorno.Lista.Add(campoObjeto);
                }
                else
                {
                    if (parametro.ParameterType.IsGenericType && parametro.ParameterType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        Type tipoInternoNullable = Nullable.GetUnderlyingType(parametro.ParameterType);

                        CampoObjeto campoObjeto = new CampoObjeto(tipoInternoNullable, tipoInternoNullable.Name, "");

                        campoObjeto.CaminhoGenealogico = tipoInternoNullable.Name;

                        campoObjetoRetorno.Lista.Add(campoObjeto);
                    }
                    else
                    {
                        var campoObjetoParametro = new CampoObjeto(parametro.ParameterType, parametro.Name, "");

                        campoObjetoParametro.CaminhoGenealogico = parametro.Name;

                        var service = new ReflectionUtil(parametro.ParameterType);

                        campoObjetoParametro.Lista = service.RetornaPropriedades();

                        campoObjetoRetorno.Lista.Add(campoObjetoParametro);
                    }
                }
            }

            return campoObjetoRetorno;
        }

        public List<CampoObjeto> RetornaPropriedades()
        {
            if (this.TipoObjeto.IsGenericType && this.TipoObjeto.GetInterfaces().Contains(typeof(IList)) || this.TipoObjeto.IsArray)
            {
                Type tipoItemLista;

                if (this.TipoObjeto.IsArray)
                {
                    tipoItemLista = this.TipoObjeto.GetElementType();
                }
                else
                {
                    tipoItemLista = this.TipoObjeto.GetGenericArguments()[0];
                }

                var campoObjetoDoTipoDaLista = new CampoObjeto(tipoItemLista, tipoItemLista.Name, this.TipoObjeto.Name.ToString() + "|Item");

                if (!tipoItemLista.IsPrimitive && tipoItemLista != typeof(string) && tipoItemLista != typeof(DateTime))
                {
                    var reflection = new ReflectionUtil(tipoItemLista);

                    campoObjetoDoTipoDaLista.Lista = reflection.RetornaPropriedades();
                }
                var listaRetorno = new List<CampoObjeto>();

                listaRetorno.Add(campoObjetoDoTipoDaLista);

                return listaRetorno;
            }
            else
            {
                return this.RetornaPropriedadesGenerico(this.TipoObjeto, this.TipoObjeto.Name);
            }
        }

        public List<CampoObjeto> RetornaPropriedadesGenerico(Type tipo, string caminhoGenealogicoPai)
        {
            List<CampoObjeto> Lista = new List<CampoObjeto>();

            var propriedades = tipo.GetProperties();

            foreach (var propriedade in propriedades)
            {
                if (propriedade.PropertyType.Name != "ExtensionDataObject")
                {
                    if (propriedade.PropertyType.IsPrimitive || propriedade.PropertyType == typeof(string) || propriedade.PropertyType == typeof(DateTime))
                    {
                        // Trecho Referente a criação de um Objeto dos tipos Primitivos, String e DateTime 
                        Lista.Add(new CampoObjeto(propriedade.PropertyType, propriedade.Name, caminhoGenealogicoPai));
                        // Fim do trecho Referente a criação de um Objeto dos tipos Primitivos, String e DateTime
                    }
                    else
                    {

                        if (propriedade.PropertyType.IsGenericType && propriedade.PropertyType.GetInterfaces().Contains(typeof(IList)) || propriedade.PropertyType.IsArray)
                        {
                            // Trecho Referente a criação de um Objeto do tipo List
                            var campoObjetoLista = new CampoObjeto(propriedade.PropertyType, propriedade.Name, caminhoGenealogicoPai);

                            var listaCampoObjeto = new List<CampoObjeto>();

                            Type tipoItemLista;

                            if (propriedade.PropertyType.IsArray)
                            {
                                tipoItemLista = propriedade.PropertyType.GetElementType();
                            }
                            else
                            {
                                tipoItemLista = propriedade.PropertyType.GetGenericArguments()[0];
                            }

                            var campoObjetoDoTipoDaLista = new CampoObjeto(tipoItemLista, tipoItemLista.Name, campoObjetoLista.CaminhoGenealogico + "|Item");

                            if (tipoItemLista.IsPrimitive || tipoItemLista == typeof(string) || tipoItemLista == typeof(DateTime))
                            {
                                listaCampoObjeto.Add(campoObjetoDoTipoDaLista);
                            }
                            else
                            {
                                campoObjetoDoTipoDaLista.Lista = (List<CampoObjeto>)RetornaPropriedadesGenerico(tipoItemLista, campoObjetoDoTipoDaLista.CaminhoGenealogico);

                                listaCampoObjeto.Add(campoObjetoDoTipoDaLista);
                            }

                            campoObjetoLista.Lista = listaCampoObjeto;

                            Lista.Add(campoObjetoLista);
                            // Fim do trecho Referente a criação de um Objeto do tipo List
                        }
                        else
                        {
                            if (propriedade.PropertyType.IsGenericType && propriedade.PropertyType.GetInterfaces().Contains(typeof(IDictionary)))
                            {
                                // Trecho Referente a criação de um Objeto do tipo Dictionary
                                var campoObjetoDictionary = new CampoObjeto(propriedade.PropertyType, propriedade.Name, caminhoGenealogicoPai);

                                //Lista do campoObjetoDictionary
                                var ListaKeyValue = new List<CampoObjeto>();

                                //Inicio do trecho do Campo Objeto refernte a(s) Key(s)
                                var KeysType = propriedade.PropertyType.GetGenericArguments()[0];

                                var campoObjetoKeys = new CampoObjeto(KeysType, "Key", campoObjetoDictionary.CaminhoGenealogico + "|Item");

                                if (!KeysType.IsPrimitive && KeysType != typeof(string) && KeysType != typeof(DateTime))
                                {
                                    campoObjetoKeys.Lista = RetornaPropriedadesGenerico(KeysType, campoObjetoDictionary.CaminhoGenealogico);
                                }

                                ListaKeyValue.Add(campoObjetoKeys);
                                //Fim do trecho do Campo Objeto refernte a(s) Key(s)

                                //Inicio do trecho do Campo Objeto refernte ao(s) Value(s)
                                var ValuesType = propriedade.PropertyType.GetGenericArguments()[1];

                                var campoObjetoValues = new CampoObjeto(ValuesType, "Value", campoObjetoDictionary.CaminhoGenealogico + "|Item");

                                if (!ValuesType.IsPrimitive && ValuesType != typeof(string) && ValuesType != typeof(DateTime))
                                {
                                    campoObjetoValues.Lista = RetornaPropriedadesGenerico(ValuesType, campoObjetoDictionary.CaminhoGenealogico);
                                }

                                ListaKeyValue.Add(campoObjetoValues);

                                //Fim do trecho do Campo Objeto refernte ao(s) Value(s)

                                campoObjetoDictionary.Lista = ListaKeyValue;

                                Lista.Add(campoObjetoDictionary);
                                // Fim do trecho Referente a criação de um Objeto do tipo Dictionary
                            }
                            else
                            {
                                if (propriedade.PropertyType.IsGenericType && propriedade.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                {
                                    Type tipoInternoNullable = Nullable.GetUnderlyingType(propriedade.PropertyType);
                                    Lista.Add(new CampoObjeto(tipoInternoNullable, propriedade.Name, caminhoGenealogicoPai));
                                }
                                else
                                {
                                    // Trecho Referente a criação de um Objeto de todos os outros tipos não manipulados especificamente.
                                    var campoObjetoFilho = new CampoObjeto(propriedade.PropertyType, propriedade.Name, caminhoGenealogicoPai);

                                    campoObjetoFilho.Lista = RetornaPropriedadesGenerico(propriedade.PropertyType, campoObjetoFilho.CaminhoGenealogico);

                                    Lista.Add(campoObjetoFilho);
                                    // Fim do trecho Referente a criação de um Objeto de todos os outros tipos não manipulados especificamente.
                                }
                            }
                        }
                    }
                }
            }
            return Lista;
        }
        /// <summary>
        /// Define a propriedade de acordo com o valor indicado pelo usuário.
        /// </summary>
        /// <param name="instanciaTipoComplexo">Instancia que contém a propriedade</param>
        /// <param name="nomePropriedade">Nome da propriedade</param>
        /// <param name="valor">Valor a ser atribuido a propriedade</param>        
        public void DefinePropriedade(ref object instanciaTipoComplexo, string nomePropriedade, object valor)
        {

            var dadosObjeto = nomePropriedade.Split('|');

            if (dadosObjeto.Length < 2)
            {
                instanciaTipoComplexo = Convert.ChangeType(valor, instanciaTipoComplexo.GetType());
            }
            else
            {

                if (propertySimples(nomePropriedade))
                {
                    // Trecho que preenche um objeto dos tipos Nullable de primitivos
                    if (instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).PropertyType.IsGenericType
                        && instanciaTipoComplexo
                            .GetType()
                            .GetProperty(dadosObjeto[1])
                            .PropertyType
                            .GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        Type tipoInternoNullable = Nullable.GetUnderlyingType(instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).PropertyType);
                        // Trecho que preenche um objeto Enum
                        if (tipoInternoNullable.IsEnum)
                        {
                            var valorEnum = Enum.Parse(tipoInternoNullable, valor.ToString());

                            instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).SetValue(instanciaTipoComplexo, valorEnum, index: null);
                            // Fim  do trecho que preenche um objeto Enum
                        }
                        else
                        {
                            instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).SetValue(instanciaTipoComplexo, Convert.ChangeType(valor,
                                                                                                tipoInternoNullable),
                                                                                                index: null);
                        }
                        // Fim do trecho que preenche um objeto dos tipos Nullable de primitivos
                    }
                    else
                    {
                        // Trecho que preenche um objeto Enum
                        if (instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).PropertyType.IsEnum)
                        {
                            var valorEnum = Enum.Parse(instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).PropertyType, valor.ToString());

                            instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).SetValue(instanciaTipoComplexo, valorEnum, index: null);
                            // Fim  do trecho que preenche um objeto Enum
                        }
                        else
                        {
                            // Trecho que preenche um objeto dos tipos Primitivos, String, DateTime
                            instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).SetValue(instanciaTipoComplexo, Convert.ChangeType(valor,
                                                                                                            instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).PropertyType),
                                                                                                            index: null);
                            // Fim do trecho que preenche um objeto dos tipos Primitivos, String e DateTime
                        }
                    }
                    return;
                }
                else
                {
                    // Trecho que preenche um objeto do tipo Lista
                    if (instanciaTipoComplexo.GetType().GetInterfaces().Contains(typeof(IList)) && !instanciaTipoComplexo.GetType().IsArray)
                    {
                        Type tipoLista = instanciaTipoComplexo.GetType().GetProperty("Item").PropertyType;

                        if (tipoLista.IsPrimitive || tipoLista == typeof(string) || tipoLista == typeof(DateTime))
                        {
                            object[] parametros = {Convert.ChangeType(valor,
                                                                instanciaTipoComplexo
                                                                .GetType()
                                                                .GetProperty("Item")
                                                                .PropertyType)};

                            instanciaTipoComplexo.GetType().GetMethod("Add").Invoke(instanciaTipoComplexo, parametros);
                        }
                        else
                        {
                            var stringPosicao = dadosObjeto[1].Replace("Item", "");

                            var posicao = 0;

                            if (stringPosicao != "")
                            {
                                posicao = int.Parse(stringPosicao);
                            }

                            var contador = (int)instanciaTipoComplexo.GetType().GetProperty("Count").GetValue(instanciaTipoComplexo, null);

                            if (contador != (posicao + 1) || contador == 0)
                            {
                                var novoObjeto = Activator.CreateInstance(instanciaTipoComplexo.GetType().GetProperty("Item").PropertyType.Assembly.FullName,
                                                                        instanciaTipoComplexo.GetType().GetProperty("Item").PropertyType.FullName);

                                object[] parametros = { ((ObjectHandle)novoObjeto).Unwrap() };

                                instanciaTipoComplexo.GetType().GetMethod("Add").Invoke(instanciaTipoComplexo, parametros);
                            }

                            var valorProperty = instanciaTipoComplexo.GetType().GetProperty("Item").GetValue(instanciaTipoComplexo, new object[] { posicao });

                            var nomePropriedadeSemItem = nomePropriedade.Substring(nomePropriedade.IndexOf("|") + 1);

                            DefinePropriedade(ref valorProperty, nomePropriedadeSemItem.Substring(nomePropriedadeSemItem.IndexOf("|") + 1), valor);
                        }
                        // Fim do trecho que preenche um objeto do tipo Lista
                    }
                    else
                    {
                        if (instanciaTipoComplexo.GetType().IsArray)
                        {
                            var stringPosicao = dadosObjeto[1].Replace("Item", "");

                            int posicao = 0;

                            if (stringPosicao != "")
                            {
                                posicao = int.Parse(stringPosicao);
                            }

                            object[] parametrosGetValue = { (Int32)(posicao) };

                            object[] parametroContador = { 0 };

                            int contador = (int)instanciaTipoComplexo.GetType().GetMethod("GetLength").Invoke(instanciaTipoComplexo, parametroContador);

                            if (contador < (posicao + 1))
                            {
                                var arrayTemporario = Array.CreateInstance(instanciaTipoComplexo.GetType().GetElementType(), (posicao + 1));

                                Array.ConstrainedCopy((Array)instanciaTipoComplexo, 0, arrayTemporario, 0, contador);

                                instanciaTipoComplexo = Array.CreateInstance(instanciaTipoComplexo.GetType().GetElementType(), (posicao + 1));

                                arrayTemporario.CopyTo((Array)instanciaTipoComplexo, (Int32)0);

                            }

                            var valorProperty = instanciaTipoComplexo.GetType().GetMethod("GetValue", new[] { typeof(Int32) }).Invoke(instanciaTipoComplexo, parametrosGetValue);

                            if (contador != (posicao + 1) || contador == 1 && valorProperty == null)
                            {
                                var novoObjeto = Activator.CreateInstance(instanciaTipoComplexo.GetType().GetElementType().Assembly.FullName,
                                                                        instanciaTipoComplexo.GetType().GetElementType().FullName);

                                object[] parametros = { ((ObjectHandle)novoObjeto).Unwrap(), (Int32)(posicao) };

                                instanciaTipoComplexo.GetType().GetMethod("SetValue", new[] { typeof(object), typeof(Int32) }).Invoke(instanciaTipoComplexo, parametros);
                            }

                            valorProperty = instanciaTipoComplexo.GetType().GetMethod("GetValue", new[] { typeof(Int32) }).Invoke(instanciaTipoComplexo, parametrosGetValue);

                            var nomePropriedadeSemItem = nomePropriedade.Substring(nomePropriedade.IndexOf("|") + 1);

                            DefinePropriedade(ref valorProperty, nomePropriedadeSemItem.Substring(nomePropriedadeSemItem.IndexOf("|") + 1), valor);
                        }
                        else
                        {

                            if (instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).PropertyType.IsArray)
                            {
                                Type tipoArray = instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).PropertyType.GetElementType();

                                var valorArray = instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).GetValue(instanciaTipoComplexo, null);

                                if (valorArray == null)
                                {
                                    valorArray = Array.CreateInstance(instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).PropertyType.GetElementType(), 1);
                                }
                                /*else
                                {
                                    var stringPosicao = dadosObjeto[1].Replace("Item", "");

                                    int posicao = 0;

                                    if (stringPosicao != "")
                                    {
                                        posicao = int.Parse(stringPosicao);
                                    }
                                
                                    int contador = (int)instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).PropertyType.GetMethod("GetLength").Invoke(instanciaTipoComplexo, new object[] {0});

                                    if (contador < (posicao + 1))
                                    {
                                        var arrayTemporario = instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).GetValue(instanciaTipoComplexo, null);

                                        var arrayRedimensionado = Array.CreateInstance(instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).PropertyType, (posicao + 1));

                                    }

                                    
                                }*/

                                DefinePropriedade(ref valorArray, nomePropriedade.Substring(nomePropriedade.IndexOf("|") + 1), valor);

                                instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).SetValue(instanciaTipoComplexo, valorArray, null);
                            }
                            else
                            {
                                // Trecho que preenche um objeto de todos os outros tipos não manipulados especificamente.
                                var valorProperty = instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).GetValue(instanciaTipoComplexo, index: null);

                                if (valorProperty == null)
                                {
                                    valorProperty = Activator.CreateInstance(instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).PropertyType.Assembly.FullName,
                                                                                instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).PropertyType.FullName);

                                    instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).SetValue(instanciaTipoComplexo, ((ObjectHandle)valorProperty).Unwrap(), index: null);

                                }

                                valorProperty = instanciaTipoComplexo.GetType().GetProperty(dadosObjeto[1]).GetValue(instanciaTipoComplexo, index: null);

                                DefinePropriedade(ref valorProperty, nomePropriedade.Substring(nomePropriedade.IndexOf("|") + 1), valor);
                                // Fim do trecho que preenche um objeto de todos os outros tipos não manipulados especificamente.
                            }
                        }
                    }
                }
            }
        }

        private bool propertySimples(string nomePropriedade)
        {
            return nomePropriedade.Split('|').Count() == 2;
        }
    }
}

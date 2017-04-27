using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WCFWebClient.Dominio.Dominio
{
    [Serializable]
    public class CampoObjeto
    {
        public Type Tipo { get; set; }

        public string  Nome { get; set; }

        public List<CampoObjeto> Lista { get; set; }

        public string CaminhoGenealogico { get; set; }

        public string Valor { get; set; }

        public CampoObjeto(Type tipo, string  nome, string caminhoGenealogicoPai)
        {
            this.Tipo = tipo;

            this.Nome = nome;

            this.CaminhoGenealogico = caminhoGenealogicoPai + "|" + nome;

            this.Valor = "";
        }

        public CampoObjeto(Type tipo, string nome, List<CampoObjeto> lista, string caminhoGenealogico, string valor)
        {
            this.Tipo = tipo;

            this.Nome = nome;

            this.Lista = lista;

            this.CaminhoGenealogico = caminhoGenealogico;

            this.Valor = valor;
        }

        public void InsereValor(string caminhoGenealogico, string valor)
        {
            if (caminhoGenealogico == this.CaminhoGenealogico)
            {
                this.Valor = valor;
                return;
            }
            else
            {
                if (this.Lista != null)
                {
                    foreach (CampoObjeto campoObjeto in this.Lista)
                    {
                        campoObjeto.InsereValor(caminhoGenealogico, valor);
                    }
                }
            }
        }
    }
}

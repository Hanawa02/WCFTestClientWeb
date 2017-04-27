using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFTestClient.Dominio.Dominio
{
    public class ObjetoEspectro
    {
        private Object Objeto { get; set; }

        public ObjetoEspectro(string nomeDaClasse)
        {
        }
        
        public void DefinaValorPropriedade(string nomeDaPropriedade, object valor)
        {
            //Objeto.GetType().GetProperty().SetValue(Objeto, valor);
        }

        public bool EhTipoComplexo(string nomeDaPropriedade)
        {
            return false;
        }

    }
}

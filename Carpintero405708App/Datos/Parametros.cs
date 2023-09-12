using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpintero405708App.Datos
{
    internal class Parametros
    {
        public string Nombre { get; set; }
        public object Valor { get; set; }

        public Parametros(string nombre, object valor)
        {
            this.Nombre = nombre;
            this.Valor = valor;
        }
    }
}

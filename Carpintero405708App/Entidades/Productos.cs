using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpintero405708App.Entidades
{
    internal class Productos
    {
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public bool Activo { get; set; }

        public Productos()
        {
            ProductoID = 0;
            Nombre = string.Empty;
            Precio = 0;
            Activo = true;
        }

        public Productos(int productoID, string nombre, double precio)
        {
            ProductoID = productoID;
            Nombre = nombre;
            Precio = precio;
            
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}

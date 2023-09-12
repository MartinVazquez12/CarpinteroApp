using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpintero405708App.Entidades
{
    internal class DetallesPresupuesto
    {
        public Productos Producto { get; set; }
        public int Cantidad { get; set; }

        public DetallesPresupuesto(Productos producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
        }

        public double CalcularSubtotal()
        {
            return Cantidad*Producto.Precio;
        }

    }

}

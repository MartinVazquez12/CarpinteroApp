using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carpintero405708App.Entidades
{
    internal class Presupuesto
    {
        public int PresupuestoNro { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public double costoMO { get; set; }
        public double Descuento { get; set; }
        public DateTime FechaBaja { get; set; }
        public List<DetallesPresupuesto> Detalles { get; set; }

        public Presupuesto() 
        {
            Detalles = new List<DetallesPresupuesto>();
        }

        public void AgregarDetalle(DetallesPresupuesto detalle)
        {
            Detalles.Add(detalle);
        }

        public void QuitarDetalle(int posicion)
        {
            Detalles.RemoveAt(posicion);
        }

        public double CalcularTotal()
        {
            double total = 0;
            //for (int i = 0; i < Detalles.Count; i++)
            //{
            //    total += Detalles[i].CalcularSubtotal();

            //}
            foreach (DetallesPresupuesto det in Detalles)
            {
                total += det.CalcularSubtotal();
            }
            return total;
        }
    }
}

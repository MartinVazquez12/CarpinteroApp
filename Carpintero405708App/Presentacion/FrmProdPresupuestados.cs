using Carpintero405708App.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carpintero405708App.Presentacion
{
    public partial class FrmProdPresupuestados : Form
    {
        public FrmProdPresupuestados()
        {
            InitializeComponent();
        }

        private void FrmProdPresupuestados_Load(object sender, EventArgs e)
        {
            dtpFechaDesde.Value = DateTime.Today.AddDays(-30);
            dtpFechaHasta.Value = DateTime.Today;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            List<Parametros> lista = new List<Parametros>();

            lista.Add(new Parametros("@fecha_desde", dtpFechaDesde.Value));
            lista.Add(new Parametros("@fecha_hasta", dtpFechaHasta.Value));

            DataTable tabla = new AccesoDatos().Consultar("SP_REPORTE_PRODUCTOS", lista);
            this.dTProductosBindingSource.DataSource = tabla;

            this.reportViewer1.RefreshReport();
        }
    }
}

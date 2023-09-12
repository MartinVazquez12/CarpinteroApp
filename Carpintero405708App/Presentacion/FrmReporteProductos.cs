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
using System.Data.SqlClient;

namespace Carpintero405708App.Presentacion
{
    public partial class FrmReporteProductos : Form
    {
        public FrmReporteProductos()
        {
            InitializeComponent();
        }

        private void FrmReporteProductos_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dataSetCarpinteria.T_PRODUCTOS' Puede moverla o quitarla según sea necesario.
            //this.t_PRODUCTOSTableAdapter.Fill(this.dataSetCarpinteria.T_PRODUCTOS);

            DataTable tabla = new AccesoDatos().ConsultarBD("SP_CONSULTAR_PRODUCTOS"); //Creo un datatable para traer los productos
            this.tPRODUCTOSBindingSource.DataSource = tabla; //Cargamos el source con la tabla

            this.reportViewer1.RefreshReport();
        }
    }
}

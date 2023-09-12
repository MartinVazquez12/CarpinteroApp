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
    public partial class FrmConsultarPresupuesto : Form
    {       
        public FrmConsultarPresupuesto()
        {
            InitializeComponent();
        }

        private void FrmConsultarPresupuesto_Load(object sender, EventArgs e)
        {
            dtpFecIniConsulta.Value = DateTime.Now.AddDays(-7);
            dtpFecFinConsulta.Value = DateTime.Now;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //Validar Campos de carga
            
            //Lanzo la consulta
            List<Parametros> lista = new List<Parametros>();
            lista.Add(new Parametros("@fecha_desde", dtpFecIniConsulta.Value.ToString("yyyyMMdd")));
            lista.Add(new Parametros("@fecha_hasta", dtpFecFinConsulta.Value.ToString("yyyyMMdd")));
            lista.Add(new Parametros("@cliente", txtCliente.Text));

            DataTable tabla = new AccesoDatos().Consultar("SP_CONSULTAR_PRESUPUESTO", lista); //A mi tabla le digo que traiga un acceso a datos con el metodo
            dgvConsulta.DataSource = tabla;

            //MODIFICAR
            foreach (DataRow fila in tabla.Rows)
            {
                dgvConsulta.Rows.Add(new object[] { fila["Presupuesto_nro"].ToString(),
                                                    fila["Fecha"].ToString(),
                                                    fila["Cliente"].ToString(),
                                                    fila["Total"].ToString()});
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {


        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {


        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta por salir", "Salir", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                this.Close();
        }

        private void dgvConsulta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvConsulta.CurrentCell.ColumnIndex == 4)
            {
               int nro = int.Parse(dgvConsulta.CurrentRow.Cells["ColNro"].Value.ToString());
               FrmDetallePresupuesto detalle = new FrmDetallePresupuesto();
                detalle.ShowDialog();
            }
        }

    }
}

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
using Carpintero405708App.Entidades;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Carpintero405708App.Datos;

namespace Carpintero405708App.Presentacion
{
    public partial class FrmNuevoPresupuesto : Form
    {
        Presupuesto nuevo = null;
        AccesoDatos gestor= null;

        public FrmNuevoPresupuesto()
        {
            InitializeComponent();
            nuevo = new Presupuesto();
            gestor = new AccesoDatos();
        }

        private void FrmNuevoPresupuesto_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString(); //Para solo mostrar DMY, sin hora exacta
            txtFecha.Enabled = false;
            txtCliente.Text = "Consumidor Final";
            txtDescuento.Text = "0";
            txtCantidad.Text = "1";
            txtSubtotal.Enabled = false;
            txtTotal.Enabled = false;

            lblPresupuestoNro.Text = lblPresupuestoNro.Text + " " + gestor.ProximoPresupuesto().ToString();
            CargarCombo(cboProducto, "SP_CONSULTAR_PRODUCTOS");

        }

        private void CargarCombo(ComboBox combo, string nombreSP)
        {
            DataTable tabla = gestor.ConsultarBD(nombreSP);

            combo.DataSource = tabla;
            combo.ValueMember = tabla.Columns[0].ToString();
            combo.DisplayMember= tabla.Columns[1].ToString();
            combo.DropDownStyle = ComboBoxStyle.DropDownList;


        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

                                                   //VALIDO DATOS//
            if(cboProducto.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un Producto","Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboProducto.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out _)) //Para revisar si es null, empty o si es numerico
            {
                MessageBox.Show("Ingrese una Cantidad Valida", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCantidad.Focus();
                return;
            }

            foreach (DataGridViewRow fila in dgvDetalle.Rows) //Por cada Row de mi grilla busco una "fila" de tipo DatGrViRow, veo si es repetida
            {
                if (fila.Cells["ColProducto"].Value.ToString() == cboProducto.Text) //El valor de la celda ColProducto, veo que no se repita
                {
                    MessageBox.Show("Este producto ya esta presupuestado", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboProducto.Focus();
                    return;
                }
            }

                                       //CREO OBJETOS PARA MOSTRARLOS EN MI GRILLA//

            DataRowView item = (DataRowView)cboProducto.SelectedItem; //Mi item seleccionado del cbo lo hago una fila de la grilla


                                                    //CREO UN PRODUCTO//
            int nro = Convert.ToInt32(item.Row.ItemArray[0]); //Saco del item seleccionado la columna 0 como int
            string nom = item.Row.ItemArray[1].ToString(); //Ahora la columna 1
            double pre = Convert.ToDouble(item.Row.ItemArray[2]); // Y la otra columna, la 2

            Productos prod = new Productos(nro, nom, pre); //Ahora creo un producto con los parametros antes asignados

            int cant = Convert.ToInt32(txtCantidad.Text); //La cantidad la pongo en un int

                                                    //CREO UN DETALLE//

            DetallesPresupuesto detalle = new DetallesPresupuesto(prod, cant); //Ahora uso DetPresup y uso el prod y la cant
            nuevo.AgregarDetalle(detalle); //Agrego el detalle a su presupuesto
            dgvDetalle.Rows.Add(new object[] { detalle.Producto.ProductoID, detalle.Producto.Nombre,//Muestro el detalle en la grilla
                                               detalle.Producto.Precio, detalle.Cantidad}); //creando un objeto detalle

            CalcularTotales();
        }
                                                //METODO PARA CALCULAR TOTAL Y SUBTOTAL
        private void CalcularTotales()
        {
            txtSubtotal.Text = nuevo.CalcularTotal().ToString(); //Utilizo los metodos de mis clases, para mostrar las sumas de todos los detalles
            if(txtDescuento.Text!= string.Empty)
            {
                double desc = nuevo.CalcularTotal() * Convert.ToDouble(txtDescuento.Text) / 100; //Hago el descuento en una variable
                txtTotal.Text = (nuevo.CalcularTotal() - desc).ToString(); //Total - Descuento
            }
            else txtTotal.Text = txtSubtotal.Text;

        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalle.CurrentCell.ColumnIndex == 4)
            {
                nuevo.QuitarDetalle(dgvDetalle.CurrentRow.Index);
                dgvDetalle.Rows.RemoveAt(dgvDetalle.CurrentRow.Index);

                CalcularTotales();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Validar
            if(txtCliente.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar un Cliente", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCliente.Focus();
                return;
            }
            if (dgvDetalle.Rows.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos un Detalle", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dgvDetalle.Focus();
                return;
            }
            //Confirmar(Grabar)
            GrabarPresupuesto();


        }

        private void GrabarPresupuesto()
        {
            nuevo.Fecha = Convert.ToDateTime(txtFecha.Text);
            nuevo.Cliente = txtCliente.Text;
            nuevo.Descuento = Convert.ToDouble(txtDescuento.Text);

            if (gestor.ConfirmarPresupuesto(nuevo))
            {
                MessageBox.Show("Se cargo el Presupuesto correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose(); //Libera la ventana y vuelve al anterior
            }
            else MessageBox.Show("Hubo un error al cargar su Presupuesto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cancelar la Carga y Salir", "CANCELAR", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                this.Dispose();
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cboProducto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}

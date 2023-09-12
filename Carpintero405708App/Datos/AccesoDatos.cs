using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Carpintero405708App.Entidades;

namespace Carpintero405708App.Datos
{
    internal class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;

        public AccesoDatos()
        {
            //conexion = new SqlConnection(@"Data Source=172.16.10.196;Initial Catalog=Carpinteria_2023;User ID=alumno1w1;Password=alumno1w1");
            conexion = new SqlConnection(@"Data Source=PCI5;Initial Catalog=carpinteria405708;Integrated Security=True");
        }

        public void Conectar()
        {
            conexion.Open();
            comando = new SqlCommand();
            comando.Connection = conexion;
        }

        public void Desconectar()
        {
            conexion.Close();
        }

        public int ProximoPresupuesto()
        { 
            Conectar();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "SP_PROXIMO_ID"; //Llamo al procedimiento

            //SqlParameter parametro = new SqlParameter("@next", SqlDbType.Int); //Podemos utilizar parametros para el nombre y tipo
            SqlParameter parametro = new SqlParameter(); //Creo un parametro para guardar el valor que trae el SP
            parametro.ParameterName = "@next"; //Miro los parametros del SP y los creo aca
            parametro.SqlDbType = SqlDbType.Int; //Le pongo su tipo de datos
            parametro.Direction = ParameterDirection.Output; //Le digo que es un parametro de salida, por defecto es Input
            comando.Parameters.Add(parametro); //Agrego el nuevo parametro generado
            comando.ExecuteNonQuery();

            Desconectar();
            return (int)parametro.Value; //Muestro el label con el parametro
        }

        public DataTable ConsultarBD(string nombreSP)
        {
            DataTable tabla= new DataTable();
            Conectar();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = nombreSP;

            tabla.Load(comando.ExecuteReader());

            Desconectar();
            return tabla;
        }

        public DataTable Consultar(string nombreSP, List<Parametros> param)
        {
            DataTable tabla = new DataTable();
            Conectar();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = nombreSP;
            comando.Parameters.Clear();

            foreach (Parametros p in param)
            {
                comando.Parameters.AddWithValue(p.Nombre, p.Valor);
            }

            tabla.Load(comando.ExecuteReader());

            Desconectar();
            return tabla;
        }

        public bool ConfirmarPresupuesto(Presupuesto oPresupuesto)
        {
            bool resultado = true;

            SqlTransaction t = null; //Creamos una transaccion

            try
            { //Hacemos un try para toda la transaccion

                Conectar();
                t = conexion.BeginTransaction(); //Conexion abierta bajo transaccion
                comando.Transaction = t; //Infiere en mi comando
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "SP_INSERTAR_MAESTRO";

                //PARAMETROS DE ENTRADA DE PRIMER SP
                comando.Parameters.AddWithValue("@cliente", oPresupuesto.Cliente);
                comando.Parameters.AddWithValue("@dto", oPresupuesto.Descuento);
                comando.Parameters.AddWithValue("@total", oPresupuesto.CalcularTotal());

                //PARAMETROS DE SALIDA DE PRIMER SP
                SqlParameter parametro = new SqlParameter();
                parametro.ParameterName = "@presupuesto_nro";
                parametro.SqlDbType = SqlDbType.Int;
                parametro.Direction = ParameterDirection.Output;
                comando.Parameters.Add(parametro);
                comando.ExecuteNonQuery();

                //PREPARAMOS OTRO SP PARA EJECUTAR CON DETALLE

                int presupuestoNro = (int)parametro.Value;
                int detalleNro = 1;
                SqlCommand cmdDetalle;

                foreach (DetallesPresupuesto dp in oPresupuesto.Detalles) //Por cada detalle dp en la lista de detalles del objeto de presupuesto
                {
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", conexion, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;

                    cmdDetalle.Parameters.AddWithValue("@presupuesto_nro", presupuestoNro);
                    cmdDetalle.Parameters.AddWithValue("@detalle", detalleNro);
                    cmdDetalle.Parameters.AddWithValue("@id_producto", dp.Producto.ProductoID);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", dp.Cantidad);
                    cmdDetalle.ExecuteNonQuery();

                    detalleNro++;

                }

                t.Commit(); //Confirmamos la transaccion, si todo anda bien
            }
            catch
            {
                if (t != null) t.Rollback(); //Si esto no anduvo bien, hacemos un rollback(reinicia)
                resultado = false;
            }
            finally 
            {
                if (conexion != null && conexion.State == ConnectionState.Open) //si la conexion no es null y esta abierta, hacemos close
                    Desconectar();
            }
            

            return resultado;
        }
    }
}

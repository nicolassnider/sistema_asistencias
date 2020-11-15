using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAsistencias.Logica;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace SistemaAsistencias.Datos
{
    public class DCargo
    {
        public bool InsertarCargo(LCargo parametros)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlCommand cmd = new SqlCommand("InsertarCargo", CONEXIONMAESTRA.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Cargo", parametros.Cargo);
                cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorHora);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            finally
            {
                CONEXIONMAESTRA.close();
            }

        }

        public bool EditarCargo(LCargo parametros)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlCommand cmd = new SqlCommand("EditarCargo", CONEXIONMAESTRA.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", parametros.Id);
                cmd.Parameters.AddWithValue("@Cargo", parametros.Cargo);
                cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorHora);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            finally
            {
                CONEXIONMAESTRA.close();
            }

        }
        public bool EliminarCargo(LCargo parametros)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlCommand cmd = new SqlCommand("EliminarCargo", CONEXIONMAESTRA.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", parametros.Id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            finally
            {
                CONEXIONMAESTRA.close();
            }

        }        
        
        public void BuscarCargos(ref DataTable dt, string buscador)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlDataAdapter da = new SqlDataAdapter("BuscarCargos", CONEXIONMAESTRA.connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@buscador", buscador);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }

            finally
            {
                CONEXIONMAESTRA.close();
            }

        }
    }
}


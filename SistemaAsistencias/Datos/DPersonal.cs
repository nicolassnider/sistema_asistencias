using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SistemaAsistencias.Logica;
using System.Windows.Forms;

namespace SistemaAsistencias.Datos
{
    public class DPersonal
    {
        public bool InsertarPersonal(LPersonal parametros)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlCommand cmd = new SqlCommand("InsertarPersonal", CONEXIONMAESTRA.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
                cmd.Parameters.AddWithValue("@Identificacion", parametros.Identificacion);
                cmd.Parameters.AddWithValue("@Pais", parametros.Pais);
                cmd.Parameters.AddWithValue("@id_cargo", parametros.IdCargo);
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

        public bool EditarPersonal(LPersonal parametros)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlCommand cmd = new SqlCommand("EditarPersonal", CONEXIONMAESTRA.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", parametros.Id);
                cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
                cmd.Parameters.AddWithValue("@Identificacion", parametros.Identificacion);
                cmd.Parameters.AddWithValue("@Pais", parametros.Pais);
                cmd.Parameters.AddWithValue("@id_cargo", parametros.IdCargo);
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
        public bool EliminarPersonal(LPersonal parametros)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlCommand cmd = new SqlCommand("EliminarPersonal", CONEXIONMAESTRA.connection);
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
        public void MostrarPersonal(ref DataTable dt, int desde, int hasta )
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlDataAdapter da = new SqlDataAdapter("MostrarPersonal", CONEXIONMAESTRA.connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@desde", desde);
                da.SelectCommand.Parameters.AddWithValue("@hasta", hasta);
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
        public void BuscarPersonal(ref DataTable dt, int desde, int hasta,string buscador)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlDataAdapter da = new SqlDataAdapter("BuscarPersonal", CONEXIONMAESTRA.connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@desde", desde);
                da.SelectCommand.Parameters.AddWithValue("@hasta", hasta);
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

        public bool RestaurarPersonal(LPersonal parametros)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlCommand cmd = new SqlCommand("RestaurarPersonal", CONEXIONMAESTRA.connection);
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

        public void ContarPersonal(ref int Contador)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlCommand cmd = new SqlCommand("Select Count(Id) from Personal",CONEXIONMAESTRA.connection);
                Contador = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception e)
            {

                Contador = 0;
            }
            finally
            {
                CONEXIONMAESTRA.close();
            }
        }
    }
}

using SistemaAsistencias.Logica;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaAsistencias.Datos
{
    class DAsistencia
    {

        public void BuscarAsistenciaId(ref DataTable dt, int idPersonal)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlDataAdapter da = new SqlDataAdapter("BuscarAsistenciaId", CONEXIONMAESTRA.connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@id_personal", idPersonal);                
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
        public bool InsertarAsistencia(LAsistencia parametros)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlCommand cmd = new SqlCommand("InsertarAsistencia", CONEXIONMAESTRA.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_personal", parametros.idPersonal);
                cmd.Parameters.AddWithValue("@fechaEntrada", parametros.fechaEntrada);
                cmd.Parameters.AddWithValue("@fechaSalida", parametros.fechaSalida);
                cmd.Parameters.AddWithValue("@estado", parametros.estado);
                cmd.Parameters.AddWithValue("@horas", parametros.horas);
                cmd.Parameters.AddWithValue("@observacion", parametros.observacion);
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

        public bool ConfirmarSalida(LAsistencia parametros)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlCommand cmd = new SqlCommand("ConfirmarSalida", CONEXIONMAESTRA.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_personal", parametros.idPersonal);
                cmd.Parameters.AddWithValue("@fechaSalida", parametros.fechaSalida);
                cmd.Parameters.AddWithValue("@horas", parametros.horas);
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

        public void MostrarAsistenciasDiarias(ref DataTable dt, DateTime desde, DateTime hasta, int semana)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_asistencias_diarias", CONEXIONMAESTRA.connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@desde", desde);
                da.SelectCommand.Parameters.AddWithValue("@hasta", hasta);
                da.SelectCommand.Parameters.AddWithValue("@semana", semana);
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

using SistemaAsistencias.Logica;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaAsistencias.Datos
{
    public class DPermit
    {
        public bool InsertarPermit(LPermit parametros)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlCommand cmd = new SqlCommand("InsertarPermisos", CONEXIONMAESTRA.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idModulo", parametros.IdModulo);
                cmd.Parameters.AddWithValue("@idUsuario", parametros.IdUsuario);                
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

        public void MostrarPermits(ref DataTable dt, LPermit parametros)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlDataAdapter da = new SqlDataAdapter("MostrarPermit", CONEXIONMAESTRA.connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idUsuario", parametros.IdUsuario);

                da.Fill(dt);

                CONEXIONMAESTRA.close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        public bool EliminarPermits(LPermit parametros)
        {
            try
            {
                CONEXIONMAESTRA.open();
                SqlCommand cmd = new SqlCommand("EliminarPermit", CONEXIONMAESTRA.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", parametros.IdUsuario);
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
    }
}

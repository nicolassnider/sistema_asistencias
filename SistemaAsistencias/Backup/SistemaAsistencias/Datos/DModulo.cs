using SistemaAsistencias.Logica;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaAsistencias.Datos
{
    class DModulo
    {
        public void MostrarModulos(ref DataTable dt)
        {
			try
			{
				CONEXIONMAESTRA.open();
				SqlDataAdapter da = new SqlDataAdapter("Select * from Modulo", CONEXIONMAESTRA.connection);
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

		public bool InsertarModulos(LModulo parametros)
		{
			try
			{
				CONEXIONMAESTRA.open();
				SqlCommand cmd = new SqlCommand("InsertarModulos", CONEXIONMAESTRA.connection);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Modulo", parametros.Modulo);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return true;
			}
			finally
			{
				CONEXIONMAESTRA.close();
			}
		}
	}
}

using SistemaAsistencias.Logica;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace SistemaAsistencias.Datos
{
	public class DCopiaBD
	{
		public bool InsertarCopiasBd()
		{
			try
			{
				CONEXIONMAESTRA.open();
				SqlCommand cmd = new SqlCommand("InsertarCopiaBd", CONEXIONMAESTRA.connection);
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
		public void MostrarRuta(ref string ruta)
		{
			try
			{
				CONEXIONMAESTRA.open();
				SqlCommand da = new SqlCommand("select Ruta from CopiaBd", CONEXIONMAESTRA.connection);
				ruta = Convert.ToString(da.ExecuteScalar());
				CONEXIONMAESTRA.close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace);
			}
		}
		public bool EditarCopiaBd(LCopiaBD parametros)
		{
			try
			{
				CONEXIONMAESTRA.open();
				SqlCommand cmd = new SqlCommand("EditarCopiaBd", CONEXIONMAESTRA.connection);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@ruta", parametros.ruta);
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

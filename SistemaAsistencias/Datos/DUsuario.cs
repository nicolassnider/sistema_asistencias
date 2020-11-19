using SistemaAsistencias.Logica;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaAsistencias.Datos
{
    class DUsuario
    {
		public bool InsertarUsuarios(LUsuario parametros)
		{
			try
			{
				CONEXIONMAESTRA.open();
				SqlCommand cmd = new SqlCommand("InsertarUsuario", CONEXIONMAESTRA.connection);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@nombresApellidos", parametros.nombresApellidos);
				cmd.Parameters.AddWithValue("@login", parametros.login);
				cmd.Parameters.AddWithValue("@password", parametros.password);
				cmd.Parameters.AddWithValue("@icono", parametros.icono);
				cmd.Parameters.AddWithValue("@estado", "ACTIVO");
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

		public void MostrarUsuarios(ref DataTable dt)
		{
			try
			{
				CONEXIONMAESTRA.open();
				SqlDataAdapter da = new SqlDataAdapter("Select * from Usuario", CONEXIONMAESTRA.connection);
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
		public void ObtenerIdUsuario(ref int IdUsuario, string Login)
		{
			try
			{
				CONEXIONMAESTRA.open();
				SqlCommand cmd = new SqlCommand("ObtenerIdUsuario", CONEXIONMAESTRA.connection);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Login", Login);
				IdUsuario = Convert.ToInt32(cmd.ExecuteScalar());
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
		public void VerificarUsuarios(ref string Indicador)
		{
			try
			{
				int Iduser;
				CONEXIONMAESTRA.open();
				SqlCommand da = new SqlCommand("Select IdUsuario From Usuario", CONEXIONMAESTRA.connection);
				Iduser = Convert.ToInt32(da.ExecuteScalar());
				CONEXIONMAESTRA.close();
				Indicador = "Correcto";
			}
			catch (Exception)
			{

				Indicador = "Incorrecto";
			}
		}
		public void validarUsuario(LUsuario parametros, ref int id)
		{
			try
			{
				CONEXIONMAESTRA.open();
				SqlCommand cmd = new SqlCommand("validar_usuario", CONEXIONMAESTRA.connection);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@password", parametros.password);
				cmd.Parameters.AddWithValue("@login", parametros.login);
				id = Convert.ToInt32(cmd.ExecuteScalar());


			}
			catch (Exception ex)
			{
				id = 0;

			}
			finally
			{
				CONEXIONMAESTRA.close();
			}
		}
	}
}

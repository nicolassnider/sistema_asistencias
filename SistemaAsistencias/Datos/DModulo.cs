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
                SqlDataAdapter da = new SqlDataAdapter("Select * from Modulo",CONEXIONMAESTRA.connection);
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

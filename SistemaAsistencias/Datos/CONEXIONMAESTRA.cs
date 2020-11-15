using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SistemaAsistencias.Datos
{
    class CONEXIONMAESTRA
    {
        public static string conString = @"Data source=SOULIT; Initial Catalog=SistemaAsistencias; Integrated Security=true";
        public static SqlConnection connection = new SqlConnection(conString);

        public static void open()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

        }

        public static void close()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close(); 
            }
        }
    }
}

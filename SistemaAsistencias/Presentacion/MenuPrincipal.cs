using SistemaAsistencias.Datos;
using SistemaAsistencias.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAsistencias.Presentacion
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }
        public int idUsuario;
        public string login;
        public string baseDeDatos = "SistemaAsistencias";
        public string servidor = @".\SQLEXPRESS";
        public string ruta;
        
        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            panelBienvenida.Dock = DockStyle.Fill;
            ValidarPermisos();
            lblLogin.Text = login;
        }

        private void ValidarPermisos()
        {
            DataTable dt = new DataTable();
            DPermit funcion = new DPermit();
            LPermit parametros = new LPermit();
            parametros.IdUsuario = idUsuario;

            funcion.MostrarPermits(ref dt, parametros);
            btnConsultas.Enabled = false;
            btnPersonal.Enabled = false;
            btnRegistro.Enabled = false;
            btnUsuarios.Enabled = false;

            btnRestaurar.Enabled = false;
            btnRespaldo.Enabled = false;

            foreach (DataRow rowPermiso in dt.Rows)
            {
                string modulo = Convert.ToString(rowPermiso["Modulo"]);
                switch (modulo)
                {
                    case "PrePlanillas":
                        btnConsultas.Enabled = true;
                        break;
                    case "Usuarios":
                        btnUsuarios.Enabled = true;
                        btnRegistro.Enabled = true;
                        break;
                    case "Personal":
                        btnPersonal.Enabled = true;
                        break;
                    case "Respaldos":
                        btnRespaldo.Enabled = true;
                        btnRestaurar.Enabled = true;
                        break;
                    default:
                        break;
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                string modulo = Convert.ToString(row["Modulo"]);
                if (true)
                {

                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel25_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            panelPadre.Controls.Clear();
            UserControlPersonal userControlPersonal = new UserControlPersonal();
            userControlPersonal.Dock = DockStyle.Fill;
            panelPadre.Controls.Add(userControlPersonal);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            panelPadre.Controls.Clear();
            UserControlUsuario userControlUsuario = new UserControlUsuario();
            userControlUsuario.Dock = DockStyle.Fill;
            panelPadre.Controls.Add(userControlUsuario);
        }

        private void btnConsultas_Click(object sender, EventArgs e)
        {
            panelPadre.Controls.Clear();
            UserControlPrePlanilla userControlPrePlanilla = new UserControlPrePlanilla();
            userControlPrePlanilla.Dock = DockStyle.Fill;
            panelPadre.Controls.Add(userControlPrePlanilla);
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            Dispose();
            FormTomarAsistencia frm = new FormTomarAsistencia();
            frm.ShowDialog();
        }

        private void btnRespaldo_Click(object sender, EventArgs e)
        {
            panelPadre.Controls.Clear();
            CopiaBD control = new CopiaBD();
            control.Dock = DockStyle.Fill;
            panelPadre.Controls.Add(control);
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            RestaurarBDExpress();
        }

        private void RestaurarBDExpress()
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Backup" + baseDeDatos + "|*.bak";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de Backup";
            if (dlg.ShowDialog()==DialogResult.OK)
            {
                ruta = Path.GetFullPath(dlg.FileName);
                DialogResult pregunta = MessageBox.Show("¿Restaurar base de datos?", "Base de datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (pregunta==DialogResult.Yes)
                {
                    SqlConnection connection = new SqlConnection("Server=" + servidor + ";database=master; integrated security=yes");
                    try
                    {
                        
                        connection.Open();
                        string Proceso = "EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'" + baseDeDatos + "' USE [master] ALTER DATABASE [" + baseDeDatos + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE DROP DATABASE [" + baseDeDatos + "] RESTORE DATABASE " + baseDeDatos + " FROM DISK = N'" + ruta + "' WITH FILE = 1, NOUNLOAD, REPLACE, STATS = 10";
                        SqlCommand borrarRestaurar = new SqlCommand(Proceso, connection);
                        borrarRestaurar.ExecuteNonQuery();
                        MessageBox.Show("Base de datos restaurada satisfactoriamente. Reiniciar aplicación", "Restauración de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Dispose();
                    }
                    catch (Exception ex)
                    {

                    }

                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }

                    }


                }
            }
        }

        private void RestaurarBDNoExpress()
        {
            servidor = ".";
            SqlConnection connection = new SqlConnection("Server=" + servidor + ";database=master; integrated security=yes");
            try
            {
                connection.Open();
                string Proceso = "EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'" + baseDeDatos + "' USE [master] ALTER DATABASE [" + baseDeDatos + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE DROP DATABASE [" + baseDeDatos + "] RESTORE DATABASE " + baseDeDatos + " FROM DISK = N'" + ruta + "' WITH FILE = 1, NOUNLOAD, REPLACE, STATS = 10";
                SqlCommand BorraRestaura = new SqlCommand(Proceso, connection);
                BorraRestaura.ExecuteNonQuery();
                MessageBox.Show("La base de datos ha sido restaurada satisfactoriamente! Vuelve a Iniciar El Aplicativo", "Restauración de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dispose();


            }
            catch (Exception)
            {

            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

            }
        }
    }
}

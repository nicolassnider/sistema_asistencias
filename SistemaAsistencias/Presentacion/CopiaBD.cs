using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaAsistencias.Datos;
using System.Threading;
using System.IO;
using System.Data.SqlClient;
using SistemaAsistencias.Logica;

namespace SistemaAsistencias.Presentacion
{
    public partial class CopiaBD : UserControl
    {
        public CopiaBD()
        {
            InitializeComponent();
        }
        string ruta;
        string txtSoftware = "SistemaAsistencias";
        string baseDeDatos = "SistemaAsistencias";
        private Thread hilo;
        private bool end = false;

        private void CopiaBD_Load(object sender, EventArgs e)
        {
            MostrarRuta();
        }

        private void MostrarRuta()
        {
            DCopiaBD funcion = new DCopiaBD();
            funcion.MostrarRuta(ref ruta);
            txtRuta.Text = ruta;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            GenerarCopia();
        }

        private void GenerarCopia()
        {
            if (!string.IsNullOrEmpty(txtRuta.Text))
            {
                hilo =new Thread(new ThreadStart(Ejecucion));
                hilo.Start();
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Seleccione la ruta", "Ruta inexistente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRuta.Focus();
            }
        }
        private void Ejecucion()
        {
            string myFolder = "Copia_de_Seguridad_de_" + txtSoftware;
            if (!Directory.Exists(txtRuta.Text+myFolder))
            {
                Directory.CreateDirectory(txtRuta.Text + myFolder);
            }            

            string path = txtRuta.Text + myFolder;

            string subFolder = path + @"\Respaldo_al_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day+ "_" + 
                DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;

            try
            {
                Directory.CreateDirectory(Path.Combine(path, subFolder));
            }
            catch (Exception)
            {

                throw;
            }

            try
            {
                string vNombreRespaldo = baseDeDatos + ".bak";
                CONEXIONMAESTRA.open();
                string comando = $"BACKUP DATABASE {baseDeDatos} TO DISK = '{subFolder}\\{vNombreRespaldo}'";
                SqlCommand cmd = new SqlCommand(comando, CONEXIONMAESTRA.connection);
                cmd.ExecuteNonQuery();
                end = true;
            }
            catch (Exception ex)
            {
                end = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void ObtenerRuta()
        {
            if (folderBrowserDialog1.ShowDialog()==DialogResult.OK)
            {
                txtRuta.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ObtenerRuta();
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            ObtenerRuta();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (end)
            {
                timer1.Stop();
                EditarRespaldos();
            }
        }

        private void EditarRespaldos()
        {
            LCopiaBD parametros = new LCopiaBD();
            DCopiaBD funcion = new DCopiaBD();
            parametros.ruta = txtRuta.Text;

            if (funcion.EditarCopiaBd(parametros))
            {
                MessageBox.Show("Archivo creado", "Creación de copias", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        
    }
}

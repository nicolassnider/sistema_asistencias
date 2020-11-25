using SistemaAsistencias.Datos;
using SistemaAsistencias.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            panelBienvenida.Dock = DockStyle.Fill;
            ValidarPermisos();
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
    }
}

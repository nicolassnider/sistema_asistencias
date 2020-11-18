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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            panelBienvenida.Dock = DockStyle.Fill; 
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

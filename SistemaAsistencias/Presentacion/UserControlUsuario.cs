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

namespace SistemaAsistencias.Presentacion
{
    public partial class UserControlUsuario : UserControl
    {
        public UserControlUsuario()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Limpiar();
            HabilitarPaneles();
            MostrarModulos();
        }

        private void Limpiar()
        {
            txtNombre.Clear();
            txtUsuario.Clear();
            txtPassword.Clear();
        }

        private void HabilitarPaneles()
        {
            panelRegistro.Visible = true;
            lblAnuncioIcono.Visible = true;
            panelIconos.Visible = true;
            panelRegistro.Dock = DockStyle.Fill;
            panelRegistro.BringToFront();
            btnGuardar.Visible = true;
            btnActualizar.Visible = false;

        }

        
        private void MostrarModulos()
        {
            DModulo funcion = new DModulo();
            DataTable dt = new DataTable();
            funcion.MostrarModulos(ref dt);
            dataListadoUsuario.DataSource = dt;

        }
    }
}

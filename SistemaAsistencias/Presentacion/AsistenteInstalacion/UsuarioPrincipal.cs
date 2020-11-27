using SistemaAsistencias.Datos;
using SistemaAsistencias.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAsistencias.Presentacion.AsistenteInstalacion
{
    public partial class UsuarioPrincipal : Form
    {
        public UsuarioPrincipal()
        {
            InitializeComponent();
        }

        int idUsuario;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtnombre.Text))
            {
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    if (txtPassword.Text==txtConfirmPassword.Text)
                    {
                        InsertarUsuarioDefault();
                    }
                    else
                    {
                        MessageBox.Show("Contraseñas no coinciden", "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Ingresar contraseña", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("Ingresar Nombre", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void InsertarCopiaBD()
        {
            DCopiaBD funcion = new DCopiaBD();
            funcion.InsertarCopiasBd();
        }

        private void InsertarUsuarioDefault()
        {
            LUsuario parametros = new LUsuario();
            DUsuario funcion = new DUsuario();

            parametros.nombresApellidos = txtnombre.Text;
            parametros.login = TXTUSUARIO.Text;
            parametros.password = txtPassword.Text;
            MemoryStream ms = new MemoryStream();
            Icono.Image.Save(ms, Icono.Image.RawFormat);
            parametros.icono = ms.GetBuffer();
            if (funcion.InsertarUsuarios(parametros))
            {
                InsertarCopiaBD();
                InsertarModulos();
                OBtenerIdUsuario();
                InsertarPermisos();

            }
        }

        private void InsertarPermisos()
        {
            DataTable dt = new DataTable();
            DModulo funcionModulo = new DModulo();
            funcionModulo.MostrarModulos(ref dt);
            foreach(DataRow row in dt.Rows)
            {
                int idModulo = Convert.ToInt32(row["IdModulo"]);
                LPermit parametros = new LPermit();
                DPermit funcion = new DPermit();
                parametros.IdModulo = idModulo;
                parametros.IdUsuario = idUsuario;
                if (funcion.InsertarPermit(parametros))
                {
                    MessageBox.Show("Usuario administrador generado. Deberá ingresar con : " + TXTUSUARIO.Text + 
                        " y password : " + txtPassword.Text, "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dispose();
                    Login frm = new Login();
                    frm.ShowDialog();
                }
            }
        }

        private void OBtenerIdUsuario()
        {
            DUsuario funcion = new DUsuario();
            funcion.ObtenerIdUsuario(ref idUsuario, TXTUSUARIO.Text);

        }

        private void InsertarModulos()
        {
            LModulo parametros = new LModulo();
            DModulo funcion = new DModulo();
            var modulos = new List<string> { "Usuarios", "Respaldos", "Personal", "PrePlanillas" };

            foreach (var modulo in modulos)
            {
                parametros.Modulo = modulo;
                funcion.InsertarModulos(parametros);

            }

        }
    }
}

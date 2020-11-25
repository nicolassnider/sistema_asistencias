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
using SistemaAsistencias.Logica;
using System.IO;

namespace SistemaAsistencias.Presentacion
{
    public partial class UserControlUsuario : UserControl
    {
        public UserControlUsuario()
        {
            InitializeComponent();
        }

        int idUsuario;
        string estado;
        string login;

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
            txtUsuario.Enabled = true;
            dataListadoModulo.Enabled = true;
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
            dataListadoModulo.DataSource = dt;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                if (!string.IsNullOrEmpty(txtUsuario.Text))
                {
                    if (!string.IsNullOrEmpty(txtPassword.Text))
                    {
                        if (!lblAnuncioIcono.Visible)
                        {
                            InsertarUsuario();
                        }
                        else
                        {
                            MessageBox.Show("Seleccione un ícono");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingrese Contraseña");
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese Usuario");
                }
            }
            else
            {
                MessageBox.Show("Ingrese Nombre");
            }
        }

        private void InsertarUsuario()
        {
            LUsuario parametros = new LUsuario();
            DUsuario funcion = new DUsuario();
            parametros.nombresApellidos = txtNombre.Text;
            parametros.login = txtUsuario.Text;
            parametros.password = txtPassword.Text;
            MemoryStream ms = new MemoryStream();
            icono.Image.Save(ms, icono.Image.RawFormat);
            parametros.icono = ms.GetBuffer();
            if (funcion.InsertarUsuarios(parametros))
            {
                ObtenerIdUsuario();
                InsertarPermit();
            }
            
        }

        private void ObtenerIdUsuario()
        {
            DUsuario funcion = new DUsuario();
            funcion.ObtenerIdUsuario(ref idUsuario, txtUsuario.Text);
        }

        private void InsertarPermit()
        {
            foreach (DataGridViewRow row in dataListadoModulo.Rows)
            {
                int idModulo = Convert.ToInt32(row.Cells["idModulo"].Value);
                bool marcado = Convert.ToBoolean(row.Cells["Marcar"].Value);
                if (marcado)
                {
                    LPermit parametros = new LPermit();
                    DPermit funcion = new DPermit();
                    parametros.IdUsuario = idUsuario;
                    if (funcion.InsertarPermit(parametros))
                    {
                        LPermit parametrosPermit = new LPermit();
                        DPermit funcionPermit = new DPermit();
                        parametrosPermit.IdModulo = idModulo;
                        parametrosPermit.IdUsuario = idUsuario;
                        funcionPermit.InsertarPermit(parametrosPermit);
                    }
                }
            }
            MostrarUsuarios();
            panelRegistro.Visible = false;
        }
        private void MostrarUsuarios()
        {
            DataTable dt = new DataTable();
            DUsuario funcion = new DUsuario();
            funcion.MostrarUsuarios(ref dt);
            dataListadoUsuario.DataSource = dt;
            dataListadoUsuario.Columns[2].Visible = false;
            dataListadoUsuario.Columns[5].Visible = false;
            
        }

        private void designDtv()
        {
            Bases.DesignDtv(ref dataListadoUsuario);
            Bases.DesignDtvEliminar(ref dataListadoUsuario);
        }

        private void lblAnuncioIcono_Click(object sender, EventArgs e)
        {
            panelIconos.Visible = true;
            panelIconos.Dock = DockStyle.Fill;
            panelIconos.BringToFront();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            icono.Image = pictureBox2.Image;
            lblAnuncioIcono.Visible = false;
            OcultarPanelIconos();
        }
        
        private void OcultarPanelIconos()
        {
            panelIconos.Visible = false;
            icono.Visible = true;
        }

        private void btnAgregarIconoPC_Click(object sender, EventArgs e)
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Imagenes|*.jpg;*.png";
            dlg.FilterIndex = 2;
            dlg.Title= "Cargador de Imágenes";
            if (dlg.ShowDialog()==DialogResult.OK)
            {
                icono.BackgroundImage = null;
                icono.Image = new Bitmap(dlg.FileName);
                lblAnuncioIcono.Visible = false;
                OcultarPanelIconos();
            }
        }

        private void icono_Click(object sender, EventArgs e)
        {
            panelIconos.Visible = true;
            panelIconos.Dock = DockStyle.Fill;
            panelIconos.BringToFront();
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            icono.Image = pictureBox3.Image;
            lblAnuncioIcono.Visible = false;
            OcultarPanelIconos();
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            icono.Image = pictureBox4.Image;
            lblAnuncioIcono.Visible = false;
            OcultarPanelIconos();
        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            icono.Image = pictureBox5.Image;
            lblAnuncioIcono.Visible = false;
            OcultarPanelIconos();
        }

        private void pictureBox6_Click_1(object sender, EventArgs e)
        {
            icono.Image = pictureBox6.Image;
            lblAnuncioIcono.Visible = false;
            OcultarPanelIconos();
        }

        private void pictureBox7_Click_1(object sender, EventArgs e)
        {
            icono.Image = pictureBox7.Image;
            lblAnuncioIcono.Visible = false;
            OcultarPanelIconos();
        }

        private void pictureBox8_Click_1(object sender, EventArgs e)
        {
            icono.Image = pictureBox8.Image;
            lblAnuncioIcono.Visible = false;
            OcultarPanelIconos();
        }

        private void pictureBox9_Click_1(object sender, EventArgs e)
        {
            icono.Image = pictureBox9.Image;
            lblAnuncioIcono.Visible = false;
            OcultarPanelIconos();
        }

        private void UserControlUsuario_Load(object sender, EventArgs e)
        {
            MostrarUsuarios();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if(char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void dataListadoUsuario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListadoUsuario.Columns["Editar"].Index)
            {
                ObtenerEstado();
                if (estado == "INACTIVO")
                {
                    DialogResult resultado = MessageBox.Show("Este Usuario se Elimino. ¿Desea Volver a Habilitarlo?", "Restauracion de registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (resultado == DialogResult.OK)
                    {
                        RestaurarUsuario();
                    }
                }
                else
                {
                    ObtenerDatos();
                }

            }
            if (e.ColumnIndex == dataListadoUsuario.Columns["Eliminar"].Index)
            {
                DialogResult resultado = MessageBox.Show("¿Realmente desea eliminar este Registro?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (resultado == DialogResult.OK)
                {
                    CapturarIdUsuario();
                    EliminarUsuarios();
                }

            }
        }

        private void ObtenerEstado()
        {
            estado = dataListadoUsuario.SelectedCells[7].Value.ToString();
        }

        private void RestaurarUsuario()
        {
            CapturarIdUsuario();
            LUsuario parametros = new LUsuario();
            DUsuario funcion = new DUsuario();
            parametros.idUsuario = idUsuario;
            if (funcion.RestaurarUsuario(parametros) == true)
            {
                MostrarUsuarios();
            }
        }

        private void CapturarIdUsuario()
        {
            idUsuario = Convert.ToInt32(dataListadoUsuario.SelectedCells[2].Value);
            login = dataListadoUsuario.SelectedCells[4].Value.ToString();


        }
        private void EliminarUsuarios()
        {
            LUsuario parametros = new LUsuario();
            DUsuario funcion = new DUsuario();
            parametros.idUsuario = idUsuario;
            parametros.login = login;
            if (funcion.EliminarUsuarios(parametros) == true)
            {
                MostrarUsuarios();
            }
        }

        private void ObtenerDatos()
        {
            CapturarIdUsuario();
            txtNombre.Text = dataListadoUsuario.SelectedCells[3].Value.ToString();
            txtUsuario.Text = dataListadoUsuario.SelectedCells[4].Value.ToString();
            if (txtUsuario.Text == "admin")
            {
                txtUsuario.Enabled = false;
                dataListadoModulo.Enabled = false;
            }
            else
            {
                txtUsuario.Enabled = true;
                dataListadoModulo.Enabled = true;
            }
            txtPassword.Text = dataListadoUsuario.SelectedCells[5].Value.ToString();

            icono.BackgroundImage = null;
            byte[] b = (byte[])(dataListadoUsuario.SelectedCells[6].Value);
            MemoryStream ms = new MemoryStream(b);
            icono.Image = Image.FromStream(ms);
            panelRegistro.Visible = true;
            panelRegistro.Dock = DockStyle.Fill;
            lblAnuncioIcono.Visible = false;
            btnActualizar.Visible = true;
            btnGuardar.Visible = false;
            MostrarModulos();
            MostrarPermisos();
        }

        private void MostrarPermisos()
        {
            DataTable dt = new DataTable();
            DPermit funcion = new DPermit();
            LPermit parametros = new LPermit();
            parametros.IdUsuario = idUsuario;
            funcion.MostrarPermits(ref dt, parametros);
            foreach (DataRow rowPermisos in dt.Rows)
            {
                int idmoduloPermisos = Convert.ToInt32(rowPermisos["IdModulo"]);
                foreach (DataGridViewRow rowModulos in dataListadoModulo.Rows)
                {
                    int Idmodulo = Convert.ToInt32(rowModulos.Cells["IdModulo"].Value);
                    if (idmoduloPermisos == Idmodulo)
                    {
                        rowModulos.Cells[0].Value = true;
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaAsistencias.Logica;
using SistemaAsistencias.Datos;


namespace SistemaAsistencias.Presentacion
{
    public partial class UserControlPersonal : UserControl
    {
        public UserControlPersonal()
        {
            InitializeComponent();
        }

        int idCargo = 0;
        int desde = 1;
        int hasta = 10;
        int contador = 0;
        int idPersonal = 0;

        private int itemsPorPagina = 10;
        string estado   = "";
        int totalPaginas = 0;

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            LocalizarDtvCargos();
            panelCargos.Visible = false;
            panelPaginado.Visible = false;
            panelRegistros.Visible = true;
            panelRegistros.Dock = DockStyle.Fill;
            btnGuardarCambiosPersonal.Visible = false;
            Limpiar();
        }

        private void LocalizarDtvCargos()
        {
            dataListadoCargos.Location = new Point(panel16.Location.X, panel16.Location.Y + panel16.Size.Height);
            dataListadoCargos.Size = new Size(panel16.Size.Width, 141);
            dataListadoCargos.Visible = true;
            lblSueldoPorHora.Visible = false;
            panelGuardarPersonal.Visible = false;
        }

        private void InsertarPersonal()
        {
            LPersonal parametros = new LPersonal();
            DPersonal funcion = new DPersonal();
            parametros.Nombres = txtNombres.Text;
            parametros.Identificacion = txtIdentificacion.Text;
            parametros.Pais = comboBoxPais.Text;
            parametros.IdCargo = idCargo;
            parametros.SueldoPorHora = Convert.ToDouble(txtSueldoPorHora.Text);
            if (funcion.InsertarPersonal(parametros))
            {
                ReiniciarPaginado();
                MostrarPersonal();
                panelRegistros.Visible = false;
            }
        }

        private void InsertarCargo()
        {
            if (!string.IsNullOrEmpty(txtCargoG.Text))
            {
                if (!string.IsNullOrEmpty(txtSueldoPorHoraG.Text))
                {
                    LCargo parametros = new LCargo();
                    DCargo funcion = new DCargo();
                    parametros.Cargo = txtCargoG.Text;
                    parametros.SueldoPorHora = Convert.ToDouble(txtSueldoPorHoraG.Text);

                    if (funcion.InsertarCargo(parametros))
                    {
                        txtCargo.Clear();
                        BuscarCargos();
                        panelCargos.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Agregue el Sueldo por Hora", "Falta el Sueldo por Hora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Agregue el cargo", "Falta el cargo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void BuscarCargos()
        {
            DataTable dt = new DataTable();
            DCargo funcion = new DCargo();

            funcion.BuscarCargos(ref dt, txtCargo.Text);
            dataListadoCargos.DataSource = dt;
            string a = "";

        }

        private void Limpiar()
        {
            txtNombres.Clear();
            txtIdentificacion.Clear();
            txtCargo.Clear();
            txtSueldoPorHora.Clear();
            BuscarCargos();

        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            panelRegistros.Visible = false;
            panelPaginado.Visible = true;
        }

        private void txtCargo_TextChanged(object sender, EventArgs e)
        {
            BuscarCargos();
        }

        private void btnAgregarCargo_Click(object sender, EventArgs e)
        {
            panelCargos.Visible = true;
            panelCargos.Dock = DockStyle.Fill;
            panelCargos.BringToFront();
            btnGuardarC.Visible = true;
            btnGuardarCambiosC.Visible = false;
            txtCargoG.Clear();
            txtSueldoPorHoraG.Clear();
        }

        private void btnGuardarC_Click(object sender, EventArgs e)
        {
            InsertarCargo();
        }

        private void txtSueldoPorHoraG_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldoPorHoraG, e);
        }

        private void txtSueldoPorHoraG_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldoPorHoraG, e);

        }

        private void dataListadoCargos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListadoCargos.Columns["EditCargo"].Index)
            {
                ObtenerCargosEditar();
            }
            else
            {
                ObtenerCargo();
            }
        }
        
        private void ObtenerCargo()
        {
            idCargo = Convert.ToInt32(dataListadoCargos.SelectedCells[1].Value);
            txtCargo.Text = Convert.ToString(dataListadoCargos.SelectedCells[2].Value);
            txtSueldoPorHora.Text = Convert.ToString(dataListadoCargos.SelectedCells[3].Value);
            dataListadoCargos.Visible = false;
            panelGuardarPersonal.Visible = true;
            lblSueldoPorHora.Visible = true;
        }

        private void ObtenerCargosEditar()
        {
            idCargo = Convert.ToInt32(dataListadoCargos.SelectedCells[1].Value);
            txtCargoG.Text = Convert.ToString(dataListadoCargos.SelectedCells[2].Value);
            txtSueldoPorHoraG.Text = Convert.ToString(dataListadoCargos.SelectedCells[3].Value);
            btnGuardarCambiosC.Visible = false;
            btnGuardarC.Visible = true;
            txtCargoG.Focus();
            txtCargoG.SelectAll();
            panelCargos.Visible = true;
            panelCargos.Dock = DockStyle.Fill;
            panelCargos.BringToFront();
        }

        private void btnVolverPersonal_Click(object sender, EventArgs e)
        {
            panelCargos.Visible = false;
        }

        private void btnGuardarCambiosC_Click(object sender, EventArgs e)
        {
            EditarCargo();
        }

        private void EditarCargo()
        {
            LCargo parametros = new LCargo();
            DCargo funcion = new DCargo();

            parametros.Id = idCargo;
            parametros.Cargo = txtCargoG.Text;
            parametros.SueldoPorHora = Convert.ToDouble(txtSueldoPorHoraG.Text);

            if (funcion.EditarCargo(parametros))
            {
                txtCargo.Clear();
                BuscarCargos();
                panelCargos.Visible = false;
            }
        }

        private void btnGuardarPersonal_Click(object sender, EventArgs e)
        {
            
        }

        private void MostrarPersonal()
        {
            DataTable dt = new DataTable();
            DPersonal funcion = new DPersonal();
            funcion.MostrarPersonal(ref dt, desde, hasta);
            dataListadoPersonal.DataSource = dt;
            designDtvPersonal();
        }

        private void designDtvPersonal()
        {
            Bases.DesignDtv(ref dataListadoPersonal);
            Bases.DesignDtvEliminar(ref dataListadoPersonal);
            panelPaginado.Visible = true;
            dataListadoPersonal.Columns[2].Visible = false;
            dataListadoPersonal.Columns[7].Visible = false;

        }

        private void UserControlPersonal_Load(object sender, EventArgs e)
        {
            MostrarPersonal();
            ReiniciarPaginado();
        }

        private void btnGuardarPersonal_Click_1(object sender, EventArgs e)
        {
            InsertarPersonal();
        }

        
        
        private void EliminarPersonal()
        {
            idPersonal = Convert.ToInt32(dataListadoPersonal.SelectedCells[2].Value);
            LPersonal parametros = new LPersonal();
            DPersonal funcion = new DPersonal();
            parametros.Id = idPersonal;
            if (funcion.EliminarPersonal(parametros))
            {
                MostrarPersonal();
            }
        }

        private void EditarPersonal()
        {
            idPersonal = Convert.ToInt32(dataListadoPersonal.SelectedCells[2].Value);
            LPersonal parametros = new LPersonal();
            DPersonal funcion = new DPersonal();
            parametros.Id = idPersonal;
            parametros.Nombres = Convert.ToString(txtNombres.Text);
            parametros.Identificacion = Convert.ToString(txtIdentificacion.Text);
            parametros.Pais = Convert.ToString(comboBoxPais.Text);
            parametros.SueldoPorHora = Convert.ToDouble(txtSueldoPorHora.Text);
            parametros.IdCargo = idCargo ;
            if (funcion.EditarPersonal(parametros))
            {
                MostrarPersonal();
                panelRegistros.Visible = false;
            }
        }

        private void ObtenerDatosPersonal()
        {
            idPersonal = Convert.ToInt32(dataListadoPersonal.SelectedCells[2].Value);
            estado = Convert.ToString(dataListadoPersonal.SelectedCells[8].Value);
            if (estado.Equals("INACTIVO"))
            {
                RestaurarPersonal();
            }
            else
            {
                LocalizarDtvCargos();
                txtNombres.Text = Convert.ToString(dataListadoPersonal.SelectedCells[3].Value);
                txtIdentificacion.Text = Convert.ToString(dataListadoPersonal.SelectedCells[4].Value);
                comboBoxPais.Text = Convert.ToString(dataListadoPersonal.SelectedCells[10].Value);
                txtCargo.Text = Convert.ToString(dataListadoPersonal.SelectedCells[6].Value);
                idCargo = Convert.ToInt32(dataListadoPersonal.SelectedCells[7].Value);
                txtSueldoPorHora.Text = Convert.ToString(dataListadoPersonal.SelectedCells[5].Value);
                panelPaginado.Visible = false;
                panelRegistros.Visible = true;
                panelRegistros.Dock = DockStyle.Fill;
                dataListadoCargos.Visible = false;
                lblSueldoPorHora.Visible = true;
                panelGuardarPersonal.Visible = true;
                btnGuardarPersonal.Visible = false;
                btnGuardarCambiosPersonal.Visible = true;
                panelCargos.Visible = false;


            }

        }

        private void RestaurarPersonal()
        {
            DialogResult result = MessageBox.Show("¿Reactivar personal?", "Eliminar Registro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result.Equals(DialogResult.OK))
            {
                ActivarPersonal();
            }
        }

        private void ActivarPersonal()
        {
            LPersonal parametros = new LPersonal();
            DPersonal funcion = new DPersonal();
            parametros.Id = idPersonal;
            if (funcion.RestaurarPersonal(parametros))
            {
                MostrarPersonal();
            }
        }

        private void dataListadoPersonal_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListadoPersonal.Columns["Eliminar"].Index)
            {
                DialogResult result = MessageBox.Show("¿Desactivar personal?", "Modificar registro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result.Equals(DialogResult.OK))
                {
                    EliminarPersonal();
                }
            }
            if (e.ColumnIndex == dataListadoPersonal.Columns["Editar"].Index)
            {
                ObtenerDatosPersonal();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            designDtvPersonal();
            timer1.Stop();
        }

        private void btnGuardarCambiosPersonal_Click(object sender, EventArgs e)
        {
            EditarPersonal();
        }

        private void btnPagSg_Click(object sender, EventArgs e)
        {
            desde += itemsPorPagina;
            hasta += itemsPorPagina;
            MostrarPersonal();
            ContarPersonal();
            if (contador > hasta)
            {
                btnPagSg.Visible = true;
                btnPagAt.Visible = true;    
            }
            else
            {
                btnPagSg.Visible = false;
                btnPagAt.Visible = true;
            }
            Paginar();
        }

        private void Paginar()
        {
            try
            {
                lblPagina.Text = Convert.ToString((desde / itemsPorPagina)+1);
                lblTotalPag.Text = Convert.ToString(Math.Floor( Convert.ToSingle(contador) / itemsPorPagina));
                totalPaginas = Convert.ToInt32(lblTotalPag.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ContarPersonal()
        {
            DPersonal funcion = new DPersonal();
            funcion.ContarPersonal(ref contador);
        }

        private void ReiniciarPaginado()
        {
            ContarPersonal();
            if (contador>hasta)
            {
                btnPagSg.Visible = true;
                btnPagAt.Visible = true;
                btnPagPrimera.Visible = true;
                btnPagUltima.Visible = true;
            }
            else
            {
                {
                    btnPagSg.Visible = false;
                    btnPagAt.Visible = false;
                    btnPagPrimera.Visible = false;
                    btnPagUltima.Visible = false;
                }
            }
            Paginar();
        }

        private void btnPagAt_Click(object sender, EventArgs e)
        {
            if ((desde - itemsPorPagina) > 0) {
                desde -= itemsPorPagina;
                hasta -= itemsPorPagina;
            }
            
            MostrarPersonal();
            ContarPersonal();
            if (contador > hasta)
            {
                btnPagSg.Visible = true;
                btnPagAt.Visible = true;
            }
            else
            {
                btnPagSg.Visible = false;
                btnPagAt.Visible = true;
            }
            if (desde==1)
            {
                ReiniciarPaginado();
            }
            Paginar();
        }

        private void btnPagUltima_Click(object sender, EventArgs e)
        {
            hasta = totalPaginas * itemsPorPagina;
            desde = hasta - (itemsPorPagina - 1);
            MostrarPersonal();
            ContarPersonal();
        }

        private void btnPagPrimera_Click(object sender, EventArgs e)
        {
            ReiniciarPaginado();
            MostrarPersonal();
        }

        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {
            BuscarPersonal();
        }

        private void BuscarPersonal()
        {
            DPersonal funcion = new DPersonal();
            DataTable dt = new DataTable();
            funcion.BuscarPersonal(ref dt, desde, hasta, txtBuscador.Text);
            dataListadoPersonal.DataSource = dt;
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            ReiniciarPaginado();
            MostrarPersonal();
        }
    }
}

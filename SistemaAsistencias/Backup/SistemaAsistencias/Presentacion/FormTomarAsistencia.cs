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
    public partial class FormTomarAsistencia : Form
    {
        public FormTomarAsistencia()
        {
            InitializeComponent();
        }

        string identificacion = "";
        int idPersonal = 0;
        int contador = 0;
        DateTime fechaReg = new DateTime();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void timerHora_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
            lblFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textIdentificacion_TextChanged(object sender, EventArgs e)
        {
            BuscarPersonalIdentidad();
            if (identificacion.Equals(textIdentificacion.Text))
            {
                BuscarAsistenciaId();
                if (contador==0)
                {
                    DialogResult result = MessageBox.Show("¿Agregar una Observacion?", "Observaciones", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.OK)
                    {
                        panelObservacion.Visible = true;
                        panelObservacion.Location = new Point(panel1.Location.X,panel1.Location.X);
                        panelObservacion.Size = new Size(panel1.Width, panel1.Height);
                        panelObservacion.BringToFront();
                        txtObservacion.Clear();
                        txtObservacion.Focus();
                            
                    }
                    else
                    {
                        InsertarAsistencia();
                    }
                }
                else
                {
                    ConfirmarSalida();
                }
            }
        }

        private void InsertarAsistencia()
        {
            if (string.IsNullOrEmpty(txtObservacion.Text)) txtObservacion.Text = "-";

            LAsistencia parametros = new LAsistencia();
            DAsistencia funcion = new DAsistencia();

            parametros.idPersonal = idPersonal;
            parametros.fechaEntrada = DateTime.Now;
            parametros.fechaSalida = DateTime.Now;
            parametros.estado = "ENTRADA";
            parametros.horas = 0;
            parametros.observacion = txtObservacion.Text;

            if (funcion.InsertarAsistencia(parametros))
            {
                txtAviso.Text = "Entrada registrada";
                textIdentificacion.Clear();
                textIdentificacion.Focus();
                panelObservacion.Visible = false;
            }
        }

        private void ConfirmarSalida()
        {
            LAsistencia parametros = new LAsistencia();
            DAsistencia funcion = new DAsistencia();

            parametros.idPersonal = idPersonal;
            parametros.fechaSalida = DateTime.Now;
            parametros.horas = Bases.DateDiff(Bases.DateInterval.Hour,fechaReg,DateTime.Now);

            if (funcion.ConfirmarSalida(parametros))
            {
                txtAviso.Text = "SALIDA REGISTRADA";
                textIdentificacion.Clear();
                textIdentificacion.Focus();
            }
        }

        private void BuscarAsistenciaId()
        {
            DataTable dt = new DataTable();
            DAsistencia funcion = new DAsistencia();

            funcion.BuscarAsistenciaId(ref dt, idPersonal);
            contador = dt.Rows.Count;

            if (contador>0)
            {
                fechaReg = Convert.ToDateTime(dt.Rows[0]["FechaEntrada"]);
            }
        }

        private void BuscarPersonalIdentidad()
        {
            DataTable dt = new DataTable();
            DPersonal funcion = new DPersonal();
            funcion.BuscarPersonalIdentidad(ref dt,textIdentificacion.Text);

            if(dt.Rows.Count > 0)
            {
                identificacion = Convert.ToString(dt.Rows[0]["Identificacion"]);
                idPersonal = Convert.ToInt32(dt.Rows[0]["Id"]);
                txtNombre.Text= Convert.ToString(dt.Rows[0]["Nombres"]);
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            InsertarAsistencia();
        }
    }
}

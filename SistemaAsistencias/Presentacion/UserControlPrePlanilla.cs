using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaAsistencias.Presentacion.Reportes;
using SistemaAsistencias.Datos;
using System.Globalization;

namespace SistemaAsistencias.Presentacion
{
    public partial class UserControlPrePlanilla : UserControl
    {
        public UserControlPrePlanilla()
        {
            InitializeComponent();
        }
        int semana;

        private void UserControlPrePlanilla_Load(object sender, EventArgs e)
        {
            CalcularNumeroSemana();
        }

        private void ReporteAsistenciasDiarias()
        {
            ReportAsistencias rpt = new ReportAsistencias();
            DataTable dt = new DataTable();

            DAsistencia funcion = new DAsistencia();
            funcion.MostrarAsistenciasDiarias(ref dt, txtDesde.Value, txtHasta.Value, semana);
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
            reportViewer1.Report = rpt;
            reportViewer1.RefreshReport();
        }

        private void CalcularNumeroSemana()
        {
            DateTime v2 = txtHasta.Value;
            lblSemana.Text = Convert.ToString(CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(v2, CalendarWeekRule.FirstDay, v2.DayOfWeek));
            semana = Convert.ToInt32(CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(v2, CalendarWeekRule.FirstDay, v2.DayOfWeek));
        }

        private void txtDesde_ValueChanged(object sender, EventArgs e)
        {
            CalcularNumeroSemana();
            ReporteAsistenciasDiarias();
        }

        private void txtHasta_ValueChanged(object sender, EventArgs e)
        {
            CalcularNumeroSemana();
            ReporteAsistenciasDiarias();
        }
    }
}

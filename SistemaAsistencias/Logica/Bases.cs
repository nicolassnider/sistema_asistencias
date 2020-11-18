using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SistemaAsistencias.Logica
{
    class Bases
    {
        public static void DesignDtv(ref DataGridView Listado)
        {
            Listado.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.AllCells;
            Listado.BackgroundColor = Color.FromArgb(29,29,29);
            Listado.EnableHeadersVisualStyles = false;
            Listado.BorderStyle = BorderStyle.None;
            Listado.CellBorderStyle = DataGridViewCellBorderStyle.None;
            Listado.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            Listado.RowHeadersVisible = false;

            DataGridViewCellStyle cabecera = new DataGridViewCellStyle();
            cabecera.BackColor = Color.FromArgb(29, 29, 29);
            cabecera.ForeColor = Color.White;
            cabecera.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            Listado.ColumnHeadersDefaultCellStyle = cabecera;

        }

        public static void DesignDtvEliminar(ref DataGridView Listado)
        {
            foreach (DataGridViewRow row in Listado.Rows)
            {
                string estado = "";
                estado = Convert.ToString(row.Cells["Estado"].Value);

                if (estado.Equals("INACTIVO"))
                {
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 10,FontStyle.Strikeout|FontStyle.Bold);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(255,128,128);
                }
            }
            

        }

        public static object Decimales(TextBox CajaTexto, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '.') || (e.KeyChar == ','))
            {
                e.KeyChar = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];

            }
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == '.' && (~CajaTexto.Text.IndexOf(".")) != 0)
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '.' && (~CajaTexto.Text.IndexOf(",")) != 0)
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '.')
            {
                e.Handled = false;
            }
            else if (e.KeyChar == ',')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            return null;
        }

        public enum DateInterval
        {
            Day,
            DayOfYear,
            Hour,
            Minute,
            Month,
            Quarter,
            Second,
            Weekday,
            WeekOfYear,
            Year
        }

        public static long DateDiff(DateInterval intervalType, DateTime fechaEntrada, DateTime fechaSalida)
        {
            switch (intervalType)
            {
                case DateInterval.Day:
                case DateInterval.DayOfYear:
                    TimeSpan spanForDays = fechaSalida - fechaEntrada;
                    return (long)spanForDays.TotalDays;
                case DateInterval.Hour:
                    TimeSpan spanForHours = fechaSalida - fechaEntrada;
                    return (long)spanForHours.TotalHours;
                case DateInterval.Minute:
                    TimeSpan spanForMinutes = fechaSalida - fechaEntrada;
                    return (long)spanForMinutes.TotalMinutes;
                case DateInterval.Month:
                    return ((fechaSalida.Year - fechaEntrada.Year) * 12) + (fechaSalida.Month - fechaEntrada.Month);
                case DateInterval.Quarter:
                    long fechaEntradaQuarter = (long)Math.Ceiling(fechaEntrada.Month / 3.0);
                    long fechaSalidaQuarter = (long)Math.Ceiling(fechaSalida.Month / 3.0);
                    return (4 * (fechaSalida.Year - fechaEntrada.Year)) + fechaSalidaQuarter - fechaEntradaQuarter;
                case DateInterval.Second:
                    TimeSpan spanForSeconds = fechaSalida - fechaEntrada;
                    return (long)spanForSeconds.TotalSeconds;
                case DateInterval.Weekday:
                    TimeSpan spanForWeekdays = fechaSalida - fechaEntrada;
                    return (long)(spanForWeekdays.TotalDays / 7.0);
                case DateInterval.WeekOfYear:
                    DateTime fechaEntradaModified = fechaEntrada;
                    DateTime fechaSalidaModified = fechaSalida;
                    while (fechaSalidaModified.DayOfWeek != DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                    {
                        fechaSalidaModified = fechaSalidaModified.AddDays(-1);
                    }
                    while (fechaEntradaModified.DayOfWeek != DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                    {
                        fechaEntradaModified = fechaEntradaModified.AddDays(-1);
                    }
                    TimeSpan spanForWeekOfYear = fechaSalidaModified - fechaEntradaModified;
                    return (long)(spanForWeekOfYear.TotalDays / 7.0);
                case DateInterval.Year:
                    return fechaSalida.Year - fechaEntrada.Year;
                default:
                    return 0;
            }
        }
    }
}

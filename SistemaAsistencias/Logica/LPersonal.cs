using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAsistencias.Logica
{
    public class LPersonal
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Identificacion { get; set; }
        public string Pais { get; set; }
        public int IdCargo { get; set; }
        public double SueldoPorHora { get; set; }
        public string Estado { get; set; }
        public string Codigo { get; set; }
    }
}

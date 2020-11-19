using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAsistencias.Logica
{
    class LUsuario
    {
        public int idUsuario { get; set; }
        public string nombresApellidos { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public byte[] icono { get; set; }
        public string estado { get; set; }
    }
}

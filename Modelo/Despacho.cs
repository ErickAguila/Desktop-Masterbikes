using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Despacho
    {
        public int id_despacho { get; set; }
        public string direccion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public int id_tipoDespacho { get; set; }
        public int id_estadoDespacho { get; set; }
        public int id_usuario { get; set; }

    }
}

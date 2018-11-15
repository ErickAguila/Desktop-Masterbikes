using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string rut { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public int id_sexo { get; set; }
        public int edad { get; set; }
        public string direccion { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int id_comuna { get; set; }
        public int id_tipoUsuario { get; set; }
    }
}

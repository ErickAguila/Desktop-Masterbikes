using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Bicicleta
    {
        public int id_bicicleta { get; set; }
        public int id_marca { get; set; }
        public int id_modelo { get; set; }
        public int id_tipoBicicleta { get; set; }
        public int precio { get; set; }
        public bool enpromocion { get; set; }
        public string imagen { get; set; }
    }
}

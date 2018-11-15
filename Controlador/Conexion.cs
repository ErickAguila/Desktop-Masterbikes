using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;

namespace Controlador
{
    public class Conexion
    {
        public static OracleConnection conexion;

        public Conexion()
        {
            if (conexion==null)
            {
                //CONEXION PC - ERICK
                //conexion = new OracleConnection("DATA SOURCE = localhost:1521 / xe; USER ID = admin; PASSWORD=admin");
                //CONEXION NOTEBOOK - ERICK
                conexion = new OracleConnection("DATA SOURCE = localhost:1521 / xe; USER ID = masterbikes2; PASSWORD=admin");
            }
        }

        public OracleConnection ObtenerConexion()
        {
            return conexion;
        }
    }
}

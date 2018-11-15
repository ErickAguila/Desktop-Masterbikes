using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Referencias
using Modelo;
using System.Data;
using System.Data.OracleClient;

namespace Controlador
{
    public class DaoProveedor
    {
        private OracleConnection conn;
        public static Conexion conexion = new Conexion();

        public DaoProveedor()
        {
            conn = conexion.ObtenerConexion();
        }

        public bool AgregarProveedor(Modelo.Proveedor pro)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_PROVEEDOR.SP_GUARDAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_NOMBRE", OracleType.VarChar)).Value = pro.nombre;
                cmd.Parameters.Add(new OracleParameter("P_ID_USUARIO", OracleType.Number)).Value = pro.id_usuario;

                conn.Close();
                conn.Open();
                int resuesta = cmd.ExecuteNonQuery();
                if (resuesta == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public bool ModificarProveedor(Modelo.Proveedor pro)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_PROVEEDOR.SP_MODIFICAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_NOMBRE", OracleType.VarChar)).Value = pro.nombre;
                cmd.Parameters.Add(new OracleParameter("P_ID_USUARIO", OracleType.Number)).Value = pro.id_usuario;
                cmd.Parameters.Add(new OracleParameter("P_ID_PROVEEDOR", OracleType.Number)).Value = pro.id_proveedor;

                conn.Close();
                conn.Open();
                int respuesta = cmd.ExecuteNonQuery();
                if (respuesta == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        //Metodo que valida si el usuario que se decea AGREGAR O MODIFICAR si existe
        public bool ExisteProveedor(int id)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_PROVEEDOR.SP_EXISTEPROVEEDOR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_ID_PROVEEDOR", OracleType.VarChar)).Value = id;
                OracleParameter op = new OracleParameter("PCURSOR", OracleType.Cursor);
                op.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(op);
                conn.Open();
                cmd.ExecuteNonQuery();
                if (op.Value != null)
                {
                    OracleDataReader odr = (OracleDataReader)op.Value;

                    if (odr.HasRows)
                    {
                        while (odr.Read())
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                //return false;
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
               

        public bool EmilinarProveedor(int id)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_PROVEEDOR.SP_ELIMINAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_ID_PROVEEDOR", OracleType.VarChar)).Value = id;

                conn.Close();
                conn.Open();
                int respuesta = cmd.ExecuteNonQuery();
                if (respuesta == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataSet ListarProveedores()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_PROVEEDOR.SP_LISTAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("PCURSOR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                OracleDataAdapter dr = new OracleDataAdapter(cmd);
                DataSet dt = new DataSet();
                List<Proveedor> listado = new List<Proveedor>();
                dr.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        

    }
}

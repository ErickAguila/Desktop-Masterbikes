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
    public class DaoDespacho
    {
        private OracleConnection conn;
        private static Conexion conexion = new Conexion();

        public DaoDespacho()
        {
            conn = conexion.ObtenerConexion();
        }

        //Agregar despacho
        public bool GuardarDespacho(Modelo.Despacho despa)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_DESPACHO.SP_GUARDAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_DIRECCION", OracleType.VarChar)).Value = despa.direccion;
                cmd.Parameters.Add(new OracleParameter("P_FECHACREACION", OracleType.DateTime)).Value = despa.fechaCreacion;
                cmd.Parameters.Add(new OracleParameter("P_ID_TIPODESPACHO", OracleType.Number)).Value = despa.id_tipoDespacho;
                cmd.Parameters.Add(new OracleParameter("P_ID_ESTADODESPACHO", OracleType.Number)).Value = despa.id_estadoDespacho;                
                cmd.Parameters.Add(new OracleParameter("P_ID_USUARIO", OracleType.Number)).Value = despa.id_usuario;

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
                conn.Close();
                return false;
            }
        }

        public bool ModificarDespacho(Modelo.Despacho despa)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_DESPACHO.SP_MODIFICAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_DIRECCION", OracleType.VarChar)).Value = despa.direccion;
                cmd.Parameters.Add(new OracleParameter("P_FECHACREACION", OracleType.DateTime)).Value = despa.fechaCreacion;
                cmd.Parameters.Add(new OracleParameter("P_ID_TIPODESPACHO", OracleType.Number)).Value = despa.id_tipoDespacho;
                cmd.Parameters.Add(new OracleParameter("P_ID_ESTADODESPACHO", OracleType.Number)).Value = despa.id_estadoDespacho;
                cmd.Parameters.Add(new OracleParameter("P_ID_USUARIO", OracleType.Number)).Value = despa.id_usuario;
                cmd.Parameters.Add(new OracleParameter("P_ID_DESPACHO", OracleType.Number)).Value = despa.id_usuario;

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
                conn.Close();
                return false;
            }
        }

        public bool ExisteDespacho(int id)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_DESPACHO.SP_EXISTEDESPACHO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_ID_DESPACHO", OracleType.Number)).Value = id;
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
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        //Listar todos los despachos
        public DataSet ListarDespacho()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_DESPACHO.SP_LISTAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("PCURSOR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                OracleDataAdapter dr = new OracleDataAdapter(cmd);
                DataSet dt = new DataSet();
                List<Despacho> listado = new List<Despacho>();
                dr.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                conn.Close();
                return null;
            }
        }
        
        public DataSet ListarIdUsuario()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_USUARIO.SP_LISTARIDUSUARIO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("PCURSOR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                OracleDataAdapter dr = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                List<Usuario> listado = new List<Usuario>();
                dr.Fill(ds);
                return ds;
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

        public DataSet ListarTipoDespacho()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_TIPODESPACHO.SP_LISTAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("PCURSOR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                OracleDataAdapter dr = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                List<TipoDespacho> listado = new List<TipoDespacho>();
                dr.Fill(ds);
                return ds;
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


        public DataSet ListarEstadoDespacho()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_ESTADODESPACHO.SP_LISTAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("PCURSOR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                OracleDataAdapter dr = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                List<EstadoDespacho> listado = new List<EstadoDespacho>();
                dr.Fill(ds);
                return ds;
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

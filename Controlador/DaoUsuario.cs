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
    public class DaoUsuario
    {
        private OracleConnection conn;
        public static Conexion conexion = new Conexion();

        public DaoUsuario()
        {
            conn = conexion.ObtenerConexion();
        }
        
        public bool AgregarUsuario(Modelo.Usuario user)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_USUARIO.SP_GUARDAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_RUT", OracleType.VarChar)).Value = user.rut;
                cmd.Parameters.Add(new OracleParameter("P_NOMBRE", OracleType.VarChar)).Value = user.nombre;
                cmd.Parameters.Add(new OracleParameter("P_APELLIDO", OracleType.VarChar)).Value = user.apellido;
                cmd.Parameters.Add(new OracleParameter("P_EMAIL", OracleType.VarChar)).Value = user.email;
                cmd.Parameters.Add(new OracleParameter("P_EDAD", OracleType.VarChar)).Value = user.edad;
                cmd.Parameters.Add(new OracleParameter("P_DIRECCION", OracleType.VarChar)).Value = user.direccion;
                cmd.Parameters.Add(new OracleParameter("P_USERNAME", OracleType.VarChar)).Value = user.username;
                cmd.Parameters.Add(new OracleParameter("P_PASSWORD", OracleType.VarChar)).Value = user.password;
                cmd.Parameters.Add(new OracleParameter("P_ID_COMUNA", OracleType.Number)).Value = user.id_comuna;
                cmd.Parameters.Add(new OracleParameter("P_ID_TIPOUSUARIO", OracleType.Number)).Value = user.id_tipoUsuario;
                cmd.Parameters.Add(new OracleParameter("P_ID_SEXO", OracleType.VarChar)).Value = user.id_sexo;
                
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
            finally {
                conn.Close();
            }
        }

        public bool ModificarUsuario(Modelo.Usuario user)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_USUARIO.SP_MODIFICAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_RUT", OracleType.VarChar)).Value = user.rut;
                cmd.Parameters.Add(new OracleParameter("P_NOMBRE", OracleType.VarChar)).Value = user.nombre;
                cmd.Parameters.Add(new OracleParameter("P_APELLIDO", OracleType.VarChar)).Value = user.apellido;
                cmd.Parameters.Add(new OracleParameter("P_EMAIL", OracleType.VarChar)).Value = user.email;
                cmd.Parameters.Add(new OracleParameter("P_EDAD", OracleType.VarChar)).Value = user.edad;
                cmd.Parameters.Add(new OracleParameter("P_DIRECCION", OracleType.VarChar)).Value = user.direccion;
                cmd.Parameters.Add(new OracleParameter("P_USERNAME", OracleType.VarChar)).Value = user.username;
                cmd.Parameters.Add(new OracleParameter("P_PASSWORD", OracleType.VarChar)).Value = user.password;
                cmd.Parameters.Add(new OracleParameter("P_ID_COMUNA", OracleType.Number)).Value = user.id_comuna;
                cmd.Parameters.Add(new OracleParameter("P_ID_TIPOUSUARIO", OracleType.Number)).Value = user.id_tipoUsuario;
                cmd.Parameters.Add(new OracleParameter("P_ID_SEXO", OracleType.VarChar)).Value = user.id_sexo;

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
            finally {
                conn.Close();
            }
        }

        //Metodo que valida si el usuario que se decea AGREGAR O MODIFICAR si existe
        public bool ExisteUsuario(string rut)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_USUARIO.SP_EXISTEUSUARIO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_RUT", OracleType.VarChar)).Value = rut;
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
            finally {
                conn.Close();
            }
        }

        //Metodo buscar usuario para el inicio de sesion
        public bool BuscarUsuario(string user, string pass)
        {
            
            try
            {
                DataTable pico = new DataTable();
         
                OracleCommand cmd = new OracleCommand();

                cmd.Connection = conn;
                cmd.CommandText = "PKG_USUARIO.SP_LOGIN";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_USERNAME", OracleType.VarChar)).Value = user;
                cmd.Parameters.Add(new OracleParameter("P_PASSWORD", OracleType.VarChar)).Value = pass;

                OracleParameter op = new OracleParameter("PCURSOR", OracleType.Cursor);
                op.Direction = ParameterDirection.Output;

                //cmd.Parameters.Add(new OracleParameter("PCURSOR", OracleType.Cursor)).Direction = ParameterDirection.Output;

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
            finally{
                conn.Close();
            }
        }

        public bool EmilinarUsuario(string rut)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_USUARIO.SP_ELIMINAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_RUT", OracleType.VarChar)).Value = rut;

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
            finally {
                conn.Close();
            }
        }

        public DataSet ListarUsuario()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_USUARIO.SP_LISTAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("PCURSOR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                OracleDataAdapter dr = new OracleDataAdapter(cmd);
                DataSet dt = new DataSet();
                List<Usuario> listado = new List<Usuario>();
                dr.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //return null;
            }
            finally {
                conn.Close();
            }
        }

        public DataSet ListarTipoUsuario()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_TIPOUSUARIO.SP_LISTAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("PCURSOR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                OracleDataAdapter dr = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                List<TipoUsuario> listado = new List<TipoUsuario>();
                dr.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally {
                conn.Close();
            }
        }

        public DataSet ListarComuna()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_COMUNA.SP_LISTAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("PCURSOR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                OracleDataAdapter dr = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                List<Comuna> listado = new List<Comuna>();
                dr.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                //return null;
                throw new Exception(ex.Message);
            }
            finally {
                conn.Close();
            }
        }

        public DataSet ListarSexo()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_SEXO.SP_LISTAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("PCURSOR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                OracleDataAdapter dr = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                List<Sexo> listado = new List<Sexo>();
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

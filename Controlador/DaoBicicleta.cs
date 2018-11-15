using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Referencia
using Modelo;
using System.Data;
using System.Data.OracleClient;

namespace Controlador
{
    public class DaoBicicleta
    {
        private OracleConnection conn;
        public static Conexion conexion = new Conexion();

        public DaoBicicleta()
        {
            conn = conexion.ObtenerConexion();
        }

        //Agregar Bicicleta
        public bool AgregarBicicleta(Modelo.Bicicleta bici)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_BICICLETA.SP_GUARDAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_ID_MARCA", OracleType.Number)).Value = bici.id_marca;
                cmd.Parameters.Add(new OracleParameter("P_ID_MODELO", OracleType.Number)).Value = bici.id_modelo;
                cmd.Parameters.Add(new OracleParameter("P_ID_TIPOBICICLETA", OracleType.Number)).Value = bici.id_tipoBicicleta;
                cmd.Parameters.Add(new OracleParameter("P_PRECIO", OracleType.Number)).Value = bici.precio;
                cmd.Parameters.Add(new OracleParameter("P_ENPROMOCION", OracleType.Char)).Value = bici.id_tipoBicicleta;
                cmd.Parameters.Add(new OracleParameter("P_IMAGEN", OracleType.VarChar)).Value = bici.imagen;

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

        public bool ModificarBicicleta(Modelo.Bicicleta bici)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_BICICLETA.SP_MODIFICAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_ID_MARCA", OracleType.Number)).Value = bici.id_marca;
                cmd.Parameters.Add(new OracleParameter("P_ID_MODELO", OracleType.Number)).Value = bici.id_modelo;
                cmd.Parameters.Add(new OracleParameter("P_ID_TIPOBICICLETA", OracleType.Number)).Value = bici.id_tipoBicicleta;
                cmd.Parameters.Add(new OracleParameter("P_PRECIO", OracleType.Number)).Value = bici.precio;
                cmd.Parameters.Add(new OracleParameter("P_ENPROMOCION", OracleType.Char)).Value = bici.id_tipoBicicleta;
                cmd.Parameters.Add(new OracleParameter("P_IMAGEN", OracleType.VarChar)).Value = bici.imagen;
                cmd.Parameters.Add(new OracleParameter("P_ID_BICICLETA", OracleType.Number)).Value = bici.id_bicicleta;
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

        public bool ExisteBicicleta(int id)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_BICICLETA.SP_EXISTEBICICLETA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_ID_BICICLETA", OracleType.Number)).Value = id;
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
            finally {
                conn.Close();
            }
        }

        public bool EliminarBicicleta(int id)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_BICICLETA.SP_ELIMINAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("P_ID_BICICLETA", OracleType.Number)).Value = id;
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

        //Listar bicicleta
        public DataSet ListarBicicleta()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_BICICLETA.SP_LISTAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("PCURSOR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                OracleDataAdapter dr = new OracleDataAdapter(cmd);
                DataSet dt = new DataSet();
                List<Bicicleta> listado = new List<Bicicleta>();
                dr.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally {
                conn.Close();
            }
        }

        public DataSet ListarTipoBicicleta()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_TIPOBICICLETA.SP_LISTAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("PCURSOR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                OracleDataAdapter dr = new OracleDataAdapter(cmd);
                DataSet dt = new DataSet();
                List<TipoBicicleta> listado = new List<TipoBicicleta>();
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

        public DataSet ListarMarca()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_MARCA.SP_LISTAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("PCURSOR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                OracleDataAdapter dr = new OracleDataAdapter(cmd);
                DataSet dt = new DataSet();
                List<Marca> listado = new List<Marca>();
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

        public DataSet ListarModelo()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "PKG_MODELO.SP_LISTAR";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("PCURSOR", OracleType.Cursor)).Direction = ParameterDirection.Output;
                OracleDataAdapter dr = new OracleDataAdapter(cmd);
                DataSet dt = new DataSet();
                List<Modelo.Modelo> listado = new List<Modelo.Modelo>();//Se hace referencia a la clase
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

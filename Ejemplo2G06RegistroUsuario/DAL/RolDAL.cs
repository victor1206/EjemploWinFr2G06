using Ejemplo2G06RegistroUsuario.Conexion;
using Ejemplo2G06RegistroUsuario.Modelos;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejemplo2G06RegistroUsuario.DAL
{
    public class RolDAL
    {
        public static DataTable ObtenerRoles()
        {
            //ComunDB comun = new ComunDB();//Instancia
            using (SqlConnection conn = ComunDB.ObtenerConexion())
            {
                string query = "Select IdRol, NombreRol From Roles Order By IdRol Desc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                return dt;
            }
        }

        public static int Guardar(Rol pRol)
        { 
            int result = 0;
            using (SqlConnection conn = ComunDB.ObtenerConexion())
            {
                string sql = "Insert Into Roles (NombreRol) values(@NombreRol)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NombreRol", pRol.NombreRol);
                result = cmd.ExecuteNonQuery();
            }
            return result;
        }

        public static int Modificar(Rol pRol)
        { 
            int resultado = 0;
            using (SqlConnection conn = ComunDB.ObtenerConexion())
            {
                string sql = "Update Roles set NombreRol = @NombreRol Where IdRol = @IdRol";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@NombreRol", pRol.NombreRol);
                command.Parameters.AddWithValue("@IdRol", pRol.IdRol);
                resultado = command.ExecuteNonQuery();
            }
            return resultado;
        }

        public static int Eliminar(Rol pRol)
        {
            int resultado = 0;
            using (SqlConnection conn = ComunDB.ObtenerConexion())
            {
                string sql = "Delete From Roles Where IdRol = @IdRol";
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@IdRol", pRol.IdRol);
                resultado = command.ExecuteNonQuery();
            }

            return resultado;
        }

        public static DataTable BuscarRoles(Rol pRol)
        {
            using (SqlConnection conn = ComunDB.ObtenerConexion())
            {
                string sql = "Select IdRol, NombreRol From Roles";
                if (pRol.NombreRol.Trim() != "")
                {
                    sql += " Where NombreRol Like @NombreRol";
                    sql += " Order by IdRol Desc";
                }
                else
                    sql += " Order by IdRol Desc";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (pRol.NombreRol.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@NombreRol", "%" + pRol.NombreRol + "%");
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
        }


    }
}

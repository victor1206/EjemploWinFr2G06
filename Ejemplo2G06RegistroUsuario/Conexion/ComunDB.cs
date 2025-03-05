using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejemplo2G06RegistroUsuario.Conexion
{
    public class ComunDB
    {
        private static string cadenaConexion = "Data Source=VictorDuran;Initial Catalog=MiBaseDeDatos;Integrated Security=True;";

        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conn = new SqlConnection(cadenaConexion);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la conexion: " + ex.Message);
            }
            return conn;
        }
    }
}

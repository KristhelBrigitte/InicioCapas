using CapaEntidades;
using System;
using System.Data.SqlClient;
using System.Data;

namespace CapaAccesoDatos
{
    public class ADLibro
    {
        private string cadConexion;
        private string mensaje;
        public string Mensaje { get; }


        public ADLibro()
        {
            cadConexion = string.Empty;
            mensaje = string.Empty;
        }
        
        public ADLibro(string cad)//Preguntar
        {
            cadConexion = cad;
            mensaje = string.Empty;
        }
        
        public bool libroRepetido(ELibro libro)
        {
            bool result=false;
            string sentencia;
            sentencia = $"Select 1 From Libro Where titulo='{libro.Titulo}' and claveAutor ='{libro.ClaveAutor}'";
            SqlCommand comandoSql = new SqlCommand();
            SqlConnection conexionSQL = new SqlConnection(cadConexion);
            SqlDataReader datos;

            comandoSql.Connection = conexionSQL;
            comandoSql.CommandText = sentencia;

            try
            {
                conexionSQL.Open();
                datos=comandoSql.ExecuteReader();
                if (datos.HasRows) //result=datos.HasRows
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                conexionSQL.Close();
               
            }
            catch (Exception)
            {
                throw new Exception("Se ha producido un error realizando la consulta");
            }
            finally
            {
                comandoSql.Dispose();
                conexionSQL.Dispose();
            }
            return result;
        }

        public bool claveLibroRepetida(string clave)
        {
            bool result = false;
            object ObEscalar;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexion = new SqlConnection(cadConexion);
            //SqlDataReader datos;

            comando.CommandText = "select 1 from Libro Where claveLibro=@claveLibro";
            comando.Parameters.AddWithValue("@claveLibro", clave);
            comando.Connection = conexion;

            try
            {
                conexion.Open();
                ObEscalar = comando.ExecuteScalar();
                if (ObEscalar != null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

                conexion.Close();
               

            }
            catch (Exception)
            {
                throw new Exception("Error de conexión");
            }
            finally
            {
                comando.Dispose();
                conexion.Dispose();
            }

            return result;
        }

        public DataSet listarTodos()
        {
            throw new NotImplementedException();
        }

        //READER LEER DEVUELVE DATOS

        public int insertar(ELibro libro)
        {
            int result = -1;
            string sentencia = "insert into Libro(claveLibro,titulo," +
            "claveAutor,claveCategoria)" +
            " values(@claveLibro,@titulo,@claveAutor,@claveCategoria)" //Con arrobas, al no interpolar
            ;

            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            comando.Parameters.AddWithValue("@claveLibro", libro.ClaveLibro);
            comando.Parameters.AddWithValue("@titulo", libro.Titulo);
            comando.Parameters.AddWithValue("@claveAutor", libro.ClaveAutor);
            comando.Parameters.AddWithValue("@claveCategoria",
            libro.Categoria.ClaveCategoria);

            try
            {
                conexion.Open();
                result = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw new Exception("Error");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }
            return result;

        }
        
        public DataSet listarTodos(string condicion)
        {
            DataSet setLibros = new DataSet();
            string sentencia = "Select claveLibro,titulo,claveAutor,ClaveCategoria from Libro";

            if (!string.IsNullOrEmpty(condicion))
                sentencia = string.Format("{0} where{1}",sentencia,condicion);

            SqlConnection conexion = new SqlConnection(cadConexion);// adapter para llenar dataset y dataview, y llevarlos a la base
            SqlDataAdapter adaptador;// no se instancia de una vez, solo donde se usa  PARA EL DATASET NO SE USA COMANDO

            try
            {
                adaptador = new SqlDataAdapter(sentencia,conexion);
                adaptador.Fill(setLibros);
                adaptador.Dispose();
            }
            catch (Exception)
            {
               
                throw new Exception("");
            }
            finally
            {
                conexion.Dispose();
            }
           
            //sentencia=${sentencia}where{condicion};
            return setLibros;
        }
    }
}

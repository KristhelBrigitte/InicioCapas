using System;
using System.Collections.Generic;
using System.Text;
using CapaEntidades;
using CapaAccesoDatos;
using System.Data;

namespace CapaLogicaDeNegocio
{
    public class LNLibro
    {
        private string mensaje;
        private string cadConexion;

        public LNLibro()
        {
            mensaje = string.Empty;
            cadConexion = string.Empty;
        }

        public LNLibro(string cadena)
        {
            mensaje = string.Empty;
            cadConexion = cadena;
        }


        public bool libroRepetido(ELibro libro)
        {
            bool result = false;
            ADLibro adLibro = new ADLibro(cadConexion);

            try
            {
                result = adLibro.libroRepetido(libro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool claveLibroRepetida(string clave)
        {
            bool result = false;
            ADLibro adLibro = new ADLibro(cadConexion);

            try
            {
                result = adLibro.claveLibroRepetida(clave);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public int insertar(ELibro libro)
        {
            int result;
            ADLibro adLibro = new ADLibro(cadConexion);
            try
            {
                result = adLibro.insertar(libro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }

        public DataSet listarTodos(string condicion)
        {
            DataSet setLibros;
            ADLibro adLibro = new ADLibro(cadConexion);
            try
            {
                setLibros = adLibro.listarTodos(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return setLibros;

        }
    }

}

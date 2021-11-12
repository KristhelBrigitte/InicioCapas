using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidades;
using CapaLogicaDeNegocio;

namespace InicioCapas
{
    public partial class frmLibros : Form
    {
        ECategoria categoria = new ECategoria("C0001","Comic");
        public frmLibros()
        {
            InitializeComponent();
        }
        #region Metodos

        private void limpirTextos()
        {
            txtClaveAutor.Clear();
            txtTituloLibro.Clear();
            txtClaveCategoria.Text = categoria.ClaveCategoria;
            txtClaveLibro.Clear();
            txtClaveLibro.Focus();
        }


        private void llenarDGV(string condicion="")
        {
            LNLibro ln= new LNLibro(Config.cadConexion);
            DataSet ds;
            try
            {
                ds = ln.listarTodos(condicion);
                //ds= ln.listarTodos("titulo like %amor%");

                dgvLibros.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
     
                 mensajeError(ex);
            }
            dgvLibros.Columns[0].HeaderText = "Clave de libro";
            dgvLibros.Columns[1].HeaderText = "Titulo";
            dgvLibros.Columns[2].HeaderText = "Clave Autor"; 
            dgvLibros.Columns[3].HeaderText = "Clave Categoria";
            dgvLibros.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }


        private void mensajeError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        #endregion




        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpirTextos();
        }

      
        private bool textosLlenos()
        {
            string msj = " ";
            bool result =false;
            if (string.IsNullOrEmpty(txtClaveLibro.Text))
            {
                msj = "Debe agregar una clave de libro";
                txtClaveLibro.Focus();

            }else if (string.IsNullOrEmpty(txtTituloLibro.Text))
            {
                msj = "Debe escribir un titulo";
                txtTituloLibro.Focus();
            }
            else if (string.IsNullOrEmpty(txtClaveCategoria.Text))
            {
                msj = "Debe escribir la clave de la categoría";
                txtClaveCategoria.Focus();

            }
            else if(string.IsNullOrEmpty(txtClaveAutor.Text))
            {
                msj = "Debe escribir la clave del autor";
                txtClaveAutor.Focus();
            }
            else
            {
                result= true;
            }
            if (!result)
            {
                MessageBox.Show(msj, "Atención", MessageBoxButtons.OK);
            }
            return result;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ELibro libro;
            LNLibro lnl = new LNLibro(Config.cadConexion);//Creo el objeto y paso la cadena de consturctor
         //   LNCategoria cat = new LNCategoria(Config.cadConexion);
            LNAutor at = new LNAutor(Config.cadConexion);
            if (textosLlenos())
            {
                libro = new ELibro(txtClaveLibro.Text, txtTituloLibro.Text, txtClaveAutor.Text, categoria, false);

                try
                {
                    //TODO: Agregar acceso a capa de lógica
                    if (!lnl.libroRepetido(libro))
                    {
                        if (!lnl.claveLibroRepetida(libro.ClaveLibro))
                        {
                         //   if (cat.claveCategoriaExiste(libro.Categoria.ClaveCategoria))
                         //   {
                                if (at.claveAutorExiste(libro.ClaveAutor)){

                                    if (lnl.insertar(libro) > 0)
                                    {
                                        MessageBox.Show("Guardado con éxito");

                                    }
                                    else
                                    {
                                        MessageBox.Show("Error, no se logró insertar!");
                                    }
                                }
                            else
                                {
                                    MessageBox.Show("La clave del autor es incorrecta");
                                }
                              
                          //  }
                          //  else
                          //  {
                                MessageBox.Show("La clave de categoria es incorrecta");
                        //   }
                            //TODO Agregar acceso a capa de lógica
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error el libro es repetido");
                        txtClaveAutor.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtTituloLibro.Focus();
                }
            }
        }

        private void frmLibros_Load(object sender, EventArgs e)
        {
            llenarDGV();
        }
    }
}

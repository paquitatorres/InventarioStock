using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConexionDB;
using Dominio;
using System.IO;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace Inventario_de_Articulos_de_Tienda
{
    public partial class Form2 : Form
    {
       private Articulo articulo = null;

        private OpenFileDialog archivo = null;

        public Form2()
        {
            InitializeComponent();
            Text = "Agregar articulo";
        }

        //sobreescribo para que pueda recibir un articulo por parametro para luego "modificarlo"
        public Form2(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;

            //Cambio nombre de la ventana 
            Text = "Modificar articulo";
        }




        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
          //Articulo nuevoArticulo=new Articulo();

            ArticulosNegocio negocio=new ArticulosNegocio();
            
            try 
            {   if(articulo==null)
                 articulo= new Articulo();

                articulo.Nombre= txtBoxNombre.Text;
                articulo.UrlImagen=txtBoxImagen.Text;

                double precio;
                if (!double.TryParse(txtBoxPrecio.Text, out precio))
                {
                    MessageBox.Show("Por favor, ingrese un precio válido.");
                    return; // Salir del método si el precio no es válido
                }

                articulo.Precio = precio; 


                // articulo.Precio=double.Parse(txtBoxPrecio.Text);


                articulo.Codigo=txtBoxCodigo.Text;
                articulo.Descripcion=txtBoxDescripcion.Text;
                articulo.Categoria=(Categoria)cboBoxCategoria.SelectedItem;
                articulo.Marca=(Marca)cboBoxMarca.SelectedItem;

               
                if(articulo.Id!=0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado Exitosamente");
                }
               else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado Exitosamente");
                }

                // quiero que guarde solo si la subo

                if (archivo!= null && !( txtBoxImagen.Text.ToUpper().Contains("HTTP")))
                    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName);

                Close();            
            } 
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString()); 
            }    
        }


        private void Form2_Load(object sender, EventArgs e)
        {

            AccesoCategoria listaCategoria = new AccesoCategoria();

            cboBoxCategoria.DataSource = listaCategoria.listarCategoria();
            cboBoxCategoria.DisplayMember = "Descripcion";//lo que muestro
            cboBoxCategoria.ValueMember = "Id";//lo interno


            AccesoMarca listaMarca = new AccesoMarca();

            cboBoxMarca.DataSource=listaMarca.listarMarca();
            cboBoxMarca.DisplayMember = "Descripcion";
            cboBoxMarca.ValueMember="Id";

            try
            {
                if (articulo != null)
                {
                    txtBoxNombre.Text = articulo.Nombre;

                    txtBoxImagen.Text = articulo.UrlImagen;
                    //arreglo para que se precargue la imagen seleccionada en el visor del Formulario Modificar
                    cargarPreImagen(articulo.UrlImagen);

                    txtBoxPrecio.Text = articulo.Precio.ToString();

                    txtBoxCodigo.Text = articulo.Codigo;

                    txtBoxDescripcion.Text = articulo.Descripcion;

                    cboBoxCategoria.SelectedValue = articulo.Categoria.Id;

                    cboBoxMarca.SelectedValue= articulo.Marca.Id;

                }
            }
             catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            }

        }

        private void txtBoxImagen_Leave(object sender, EventArgs e)
        {
            cargarPreImagen(txtBoxImagen.Text); 
        }

        private void cargarPreImagen(string imagen)
        {

            try
            {
                pBoxPreMuestra.Load(imagen);

            }
            catch (Exception ex)
            {
                pBoxPreMuestra.Load("https://img.freepik.com/free-vector/illustration-gallery-icon_53876-27002.jpg");

            }


        }

        private void btnSubirImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog archivo = new OpenFileDialog();

            archivo.Filter = "jpg|*.jpg";
            
            if(archivo.ShowDialog() == DialogResult.OK) 
            {
                txtBoxImagen.Text = archivo.FileName;
                cargarPreImagen(archivo.FileName);


                //para guardar imagen  
               // File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"]+archivo.SafeFileName);
            
                    
             }


        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using ConexionDB;

namespace Inventario_de_Articulos_de_Tienda
{
    public partial class Form1 : Form
    {

        private List<Articulo> listaArticulos;

        
        public Form1()
        {
            InitializeComponent();
            Text = "Inventario Articulos";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
        }


        private void cargar()
        {

            ArticulosNegocio negocio = new ArticulosNegocio();


            try
            {  listaArticulos = negocio.Listar();
                
              // dgvLista.DataSource = negocio.Listar();
               
                dgvLista.DataSource = listaArticulos;

                ocultarColumnas();

                cargarImagen(listaArticulos[0].UrlImagen);

            } 

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvLista_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLista.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvLista.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlImagen);
            }
           
        }


        private void cargarImagen(string imagen)
        {
            try
            {
                pBoxImagen.Load(imagen);

            }
            catch (Exception ex)
            {
                pBoxImagen.Load("https://img.freepik.com/free-vector/illustration-gallery-icon_53876-27002.jpg");
               
            }


        }

        private void btnAgregar1_Click(object sender, EventArgs e)
        {
            Form2 alta= new Form2();
            alta.ShowDialog();

            //actualizar despues de agregar Articulo
            cargar();
            
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvLista.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un artículo para modificar.");
                return;
            }

            // Intentar obtener el artículo seleccionado
            Articulo seleccionado = dgvLista.CurrentRow.DataBoundItem as Articulo;
            if (seleccionado == null)
            {
                MessageBox.Show("El artículo seleccionado no es válido.");
                return;
            }

            // Crear el formulario para modificar el artículo
            Form2 modificar = new Form2(seleccionado);
            modificar.ShowDialog();

            // Actualizar la lista una vez modificado
            cargar();



        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticulosNegocio negocio = new ArticulosNegocio();

            Articulo seleccionado;

            try
            {
                DialogResult respuesta = MessageBox.Show("¿Queres eliminar este articulo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
               

                if(respuesta==DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvLista.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    cargar(); 


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }



        private void filtrar()
        {
            List<Articulo> listaFiltrada;
            string filtro = txtBoxBuscar.Text;


            if (filtro != "")
            {
                if (filtro.Length >= 3)
                {
                    listaFiltrada = listaArticulos.FindAll(x => 
                    x.Categoria.ToString().ToUpper().Contains(filtro.ToUpper())||
                    x.Marca.ToString().ToUpper().Contains(filtro.ToUpper())||
                    x.Nombre.ToUpper().Contains(filtro.ToUpper()) ||
                    x.Codigo.ToUpper().Contains(filtro.ToUpper()));
                }
                else { listaFiltrada=listaArticulos; }  

            }

            else
            {
                listaFiltrada = listaArticulos; // Si el filtro está vacío, mostrar todos los artículos.
            }

            dgvLista.DataSource = null;

            dgvLista.DataSource= listaFiltrada;
            
        }


        private void ocultarColumnas()
        {
            dgvLista.Columns["UrlImagen"].Visible = false;
            dgvLista.Columns["Id"].Visible = false;


        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            filtrar();
            ocultarColumnas();
        }

        private void txtBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            filtrar();
            ocultarColumnas();
        
        }






    }
}

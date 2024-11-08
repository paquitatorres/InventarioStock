using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data.SqlClient;

namespace ConexionDB
{
    public class ArticulosNegocio
    {

        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();

            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server =.\\SQLEXPRESS;  Initial Catalog=CATALOGO_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT A.codigo, A. Nombre, A.Descripcion, A. ImagenUrl, A.Precio, M.Descripcion Marca, C.Descripcion Categoria,A.IdMarca,A.IdCategoria, A.Id  FROM ARTICULOS A , MARCAS M, CATEGORIAS C WHERE A.IdCategoria=C.Id AND A.IdMarca=M.Id;";
                comando.Connection = conexion;

                conexion.Open();

                lector = comando.ExecuteReader();

                while (lector.Read())
                {


                    Articulo art = new Articulo();

                    art.Id = (int)lector["Id"];

                    art.Codigo = (string)lector["Codigo"];
                    art.Nombre = (string)lector["Nombre"];

                    art.Marca = new Marca();
                    art.Marca.Descripcion = (string)lector["Marca"];
                    art.Marca.Id = (int)lector["IdMarca"];


                    art.Categoria = new Categoria();
                    art.Categoria.Descripcion = (string)lector["Categoria"];
                    art.Categoria.Id = (int)lector["IdCategoria"];


                    if (!(lector["Descripcion"] is DBNull))
                    {
                        art.Descripcion = (string)lector["Descripcion"];
                    }

                    if (!(lector["ImagenUrl"] is DBNull))
                    {
                        art.UrlImagen = (string)lector["ImagenUrl"];
                    }

                    art.Precio = (double)Math.Round((lector.GetDecimal(4)), 2);


                    lista.Add(art);

                }


                conexion.Close();
                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }




        }


        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("INSERT INTO ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) " + "VALUES(@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)");

                // Establecer los parámetros de la consulta
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@ImagenUrl", nuevo.UrlImagen);
                datos.setearParametro("@Precio", nuevo.Precio);

                // Ejecutar la acción
                datos.ejecutarAccion();

            }
            catch (Exception)
            {

                throw;
            }

            finally { datos.cerrarConexion(); }
        }

        public void modificar(Articulo artMod)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("update ARTICULOS set Nombre=@Nombre,Codigo=@Codigo,Descripcion=@Descripcion,IdMarca=@IdMarca,IdCategoria=@IdCategoria,ImagenUrl=@UrlImagen,Precio=@Precio where Id=@Id;");

                datos.setearParametro("@Nombre", artMod.Nombre);
                datos.setearParametro("@Codigo", artMod.Codigo);
                datos.setearParametro("@Descripcion", artMod.Descripcion);
                datos.setearParametro("@IdMarca", artMod.Marca.Id);
                datos.setearParametro("@IdCategoria", artMod.Categoria.Id);
                datos.setearParametro("@UrlImagen", artMod.UrlImagen);
                datos.setearParametro("@Precio", artMod.Precio);
                datos.setearParametro("@Id", artMod.Id);

                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                datos.cerrarConexion();

            }




            }


        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("DELETE from Articulos WHERE id=@id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();


            }
            catch (Exception ex)
            {
                throw ex;
            }













        }


    }
}

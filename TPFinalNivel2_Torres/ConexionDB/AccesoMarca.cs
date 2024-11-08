using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionDB
{
    public class AccesoMarca
    {

        public List<Marca> listarMarca()
        {
            List<Marca>lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, Descripcion from MARCAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca auxi = new Marca();

                    auxi.Id = (int)datos.Lector["Id"];
                    auxi.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(auxi);
                }

                return lista;
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







    }
}

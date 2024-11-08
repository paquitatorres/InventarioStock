using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        
        public int Id {  get; set; }

        public string Nombre { get; set; }

        public Categoria Categoria { get; set; }

        public Marca Marca { get; set; }

        public string UrlImagen { get; set; }

        public double Precio {  get; set; }

        [DisplayName("Código")]
        public string Codigo {  get; set; }

        
        [DisplayName("Descripción del producto")]
        public string Descripcion { get; set; }

    }
}

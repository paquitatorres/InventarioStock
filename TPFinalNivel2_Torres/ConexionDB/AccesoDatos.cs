﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConexionDB
{
    public class AccesoDatos
    {
        //centralizo las acciones necesarias de la base de datos 

        private SqlConnection conexion;
        private SqlCommand comando; 
        private SqlDataReader lector;
        


        public SqlDataReader Lector { get { return lector; } }

        public AccesoDatos() 
        {
            conexion = new SqlConnection("server =.\\SQLEXPRESS;  Initial Catalog=CATALOGO_DB; Integrated security=true");
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;

        }

        public void ejecutarLectura()
        {
           comando.Connection = conexion;

            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ejecutarAccion()
        { comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery(); 
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }



        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }









    }
}

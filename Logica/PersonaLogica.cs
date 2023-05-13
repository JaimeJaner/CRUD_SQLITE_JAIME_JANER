using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//En la configuración, situamos la cadena para tenerla a la mano.
using System.Configuration;
//Instanciamos la clase modelo.
using CRUD_SQLITE_JAIME_JANER.Modelo;
using System.Data.SQLite;


namespace CRUD_SQLITE_JAIME_JANER.Logica
{
    public class PersonaLogica
    {
        //Buscamos la cadena que la almacenamos en el Managger Configuration con nombre de "Cadena." Donde está el directorio de ubicacion de nuestro archivo DB.
        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        //Patron de diseño Singletón: Nos permite hacer una instancia única de nuestra clase.

        private static PersonaLogica _instancia = null;
        //Constructor
        public PersonaLogica()
        {

        }
        //Instancia unica a la clase PersonaLogica
        public static PersonaLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new PersonaLogica();
                }
                return _instancia;
            }
        }

        public bool Guardar(Persona obj)
        {
         
            //Lanzamos un try-catch en caso de errores.
            try
            {
                bool respuesta = true;

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    conexion.Open();
                    string Comando = "Insert Into tbl_Persona (Nombre, Apellido, Telefono) values (@nombre,@apellido,@telefono)";
                    //Creamos un comando que recibe una cadena y una instrucción de conexión a la BD.
                    SQLiteCommand cmd = new SQLiteCommand(Comando, conexion);
                    //Creamos los parámetros para ejecutar el comando, dándole valor al contenido del string. 
                    //Se reemplaza por nombre, que está en la clase de Personas, etc.

                    cmd.Parameters.Add(new SQLiteParameter("@nombre", obj.Nombre));
                    cmd.Parameters.Add(new SQLiteParameter("@apellido", obj.Apellido));
                    cmd.Parameters.Add(new SQLiteParameter("@telefono", obj.Telefono));
                    cmd.CommandType = System.Data.CommandType.Text;

                    //Verificamos si hubo o no inserción de la información a la BD.
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        respuesta = false;
                    }
                }
                return respuesta;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        //Este método, crea una lista de objetos del tipo persona con los atributos para luego leerlos en el dataGridView.
        public List<Persona> Listar()
        {
            //Lanzamos un try-catch en caso de errores.
            try
            {
                List<Persona> oLista = new List<Persona>();

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    conexion.Open();
                    string Comando = "Select * from tbl_Persona";
                    //Creamos un comando que recibe una cadena y una instrucción de conexión a la BD.
                    SQLiteCommand cmd = new SQLiteCommand(Comando, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    //Con el Using, no hace falta cerrar la base de datos, 
                    //Al ejecutarse, el código "muere"
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        //Listamos el contenido de nuestra base de datos en la Lista Persona.
                        while (dr.Read())
                        {
                            oLista.Add(new Persona()
                            {
                                Id = int.Parse(dr["Id"].ToString()),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                            });
                        }
                    }

                }
                return oLista;
            }
            catch (Exception ex)
            {

                throw ex;
            }    
        }

        public bool Editar(Persona obj)
        {
            //Lanzamos un try-catch en caso de errores.
            try
            {
                bool respuesta = true;

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    conexion.Open();
                    //Comando de actualización de datos según el ID.
                    string Comando = "Update tbl_Persona set Nombre = @nombre,Apellido = @apellido,Telefono = @telefono where Id = @Id";
                    //Creamos un comando que recibe una cadena y una instrucción de conexión a la BD.
                    SQLiteCommand cmd = new SQLiteCommand(Comando, conexion);
                    //Creamos los parámetros para ejecutar el comando, dándole valor al contenido del string. 
                    //Se reemplaza por nombre, que está en la clase de Personas, etc.

                    cmd.Parameters.Add(new SQLiteParameter("@nombre", obj.Nombre));
                    cmd.Parameters.Add(new SQLiteParameter("@Id", obj.Id));
                    cmd.Parameters.Add(new SQLiteParameter("@apellido", obj.Apellido));
                    cmd.Parameters.Add(new SQLiteParameter("@telefono", obj.Telefono));
                    cmd.CommandType = System.Data.CommandType.Text;

                    //Verificamos si hubo o no Edición de la información a la BD.

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        respuesta = false;
                    }
                }
                return respuesta;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Eliminar(Persona obj)
        {
            //Lanzamos un try-catch en caso de errores.
            try
            {

                bool respuesta = true;

                using (SQLiteConnection conexion = new SQLiteConnection(cadena))
                {
                    conexion.Open();
                    //Comando de actualización de datos según el ID.
                    string Comando = "Delete from tbl_Persona where Id = @Id";
                    //Creamos un comando que recibe una cadena y una instrucción de conexión a la BD.
                    SQLiteCommand cmd = new SQLiteCommand(Comando, conexion);
                    //Creamos los parámetros para ejecutar el comando, dándole valor al contenido del string. 
                    //Se reemplaza por nombre, que está en la clase de Personas, etc.

                    cmd.Parameters.Add(new SQLiteParameter("@Id", obj.Id));
                    cmd.CommandType = System.Data.CommandType.Text;

                    //Verificamos si hubo o no eliminación de la información a la BD.
                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        respuesta = false;
                    }
                }
                return respuesta;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

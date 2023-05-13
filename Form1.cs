using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Importamos nuestras clases.
using CRUD_SQLITE_JAIME_JANER.Modelo;
using CRUD_SQLITE_JAIME_JANER.Logica;

namespace CRUD_SQLITE_JAIME_JANER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            if(txt_Nombre.Text=="" || txt_Apellido.Text=="" || txt_Telefono.Text=="")
            {
                MessageBox.Show("Por favor, rellene los campos de Nombre, Apellido y Teléfono.");
            }
            else
            {
                //Indicamos los atributos qque va a tener el objeto de la clase persona que estamos creando.
                Persona objeto = new Persona()
                {
                    Nombre = txt_Nombre.Text,
                    Apellido = txt_Apellido.Text,
                    Telefono = txt_Telefono.Text
                };
                //Enviamos nuestro objeto al método guardar de la clase PersonaLogica y recibimos
                //Una respuesta en caso de ser efectiva o no el método.
                bool respuesta = PersonaLogica.Instancia.Guardar(objeto);

                if (respuesta)
                {
                    mostrar_Personas();
                }

                Limpiar();

            }
            
        }

        //Método que limpia el DataGridView y lo rellena nuevamente. - Actualiza - 
        public void mostrar_Personas()
        {
            dgv_Personas.DataSource = null;
            dgv_Personas.DataSource = PersonaLogica.Instancia.Listar();
        }

        //Al cargar el form, inmediatamente actualiza el DGV, mostrando los elementos que tenemos listados.
        private void Form1_Load(object sender, EventArgs e)
        {
            mostrar_Personas();
        }

        //Metodo para limpiar las cajas de texto.
        public void Limpiar()
        {
            txt_Id.Clear();
            txt_Apellido.Clear();
            txt_Nombre.Clear();
            txt_Telefono.Clear();
            txt_Nombre.Focus();
        }

        private void btn_Editar_Click(object sender, EventArgs e)
        {

            if (txt_Id.Text=="")
            {
                MessageBox.Show("Por favor seleccione el ID a editar.");
            }
            else
            {

                //Indicamos los atributos qque va a tener el objeto de la clase persona que estamos creando.
                Persona objeto = new Persona()
                {
                    Id = int.Parse(txt_Id.Text),
                    Nombre = txt_Nombre.Text,
                    Apellido = txt_Apellido.Text,
                    Telefono = txt_Telefono.Text
                };
                //Enviamos nuestro objeto al método guardar de la clase PersonaLogica y recibimos
                //Una respuesta en caso de ser efectiva o no el método.
                bool respuesta = PersonaLogica.Instancia.Editar(objeto);

                if (respuesta)
                {
                    mostrar_Personas();
                    Limpiar();
                }
            }
 
        }

        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (txt_Id.Text == "")
            {
                MessageBox.Show("Por favor seleccione el ID a eliminar.");
            }
            else
            {
                //Indicamos los atributos qque va a tener el objeto de la clase persona que estamos creando.
                Persona objeto = new Persona()
                {
                    Id = int.Parse(txt_Id.Text),
                };
                //Enviamos nuestro objeto al método guardar de la clase PersonaLogica y recibimos
                //Una respuesta en caso de ser efectiva o no el método.
                bool respuesta = PersonaLogica.Instancia.Eliminar(objeto);

                if (respuesta)
                {
                    mostrar_Personas();
                    Limpiar();
                }
            }
            

        }

        //Este m{etodo, restrige los caracteres del tipo letra en los textbox de tipo numerico.
        private void txt_Telefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar<= 255))
            {
                e.Handled = true;
                return;
            }

        }

        //Este m{etodo, restrige los caracteres del tipo letra en los textbox de tipo numerico.
        private void txt_Id_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                e.Handled = true;
                return;
            }
        }

        //Este m{etodo, restrige los caracteres del tipo numérico en los TextBox del tipo Nombre, Apellido.
        private void txt_Nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                e.Handled = true;
                return;
            }
        }

        //Este m{etodo, restrige los caracteres del tipo numérico en los TextBox del tipo Nombre, Apellido.
        private void txt_Apellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                e.Handled = true;
                return;
            }
        }
    }
}

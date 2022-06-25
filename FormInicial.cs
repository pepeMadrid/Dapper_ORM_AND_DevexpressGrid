using DevExpress.XtraGrid;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormInicial : Form
    {
        private DapperBD dapper;
        private List<Entidades.Trabajador> empleados;
        private int mostrandoTrabajador;
        public FormInicial()
        {
            InitializeComponent();

            //Boton derecho en la solucion del proyecto > Administrar paquetes nuguet
            //Añadir paquete Dapper y Microsoft.Data.SqlClient
            //creamos conexion con BD
            dapper = new DapperBD();
            refreshDatos();
        }
        private void refreshDatos()
        {
            empleados = dapper.Select("empleados");
            gridDevExpress.DataSource = empleados;
            mostrandoTrabajador = 0;
            txtDatosMostrar(mostrandoTrabajador);
        }

        private void txtDatosMostrar(int posicion)
        {
            txtID.Text = empleados[posicion].id.ToString();
            txtNombre.Text = empleados[posicion].nombre;
            txtTelefono.Text = empleados[posicion].telefono;
            txtDNI.Text = empleados[posicion].dni;
        }

        private void buttonSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                txtDatosMostrar(++mostrandoTrabajador);
            }
            catch (Exception ex)
            {
                mostrandoTrabajador = 0;
                txtDatosMostrar(mostrandoTrabajador);
            }
        }

        private void buttonAtras_Click(object sender, EventArgs e)
        {
            try
            {
                txtDatosMostrar(--mostrandoTrabajador);
            }
            catch (Exception ex)
            {
                mostrandoTrabajador = empleados.Count - 1;
                txtDatosMostrar(mostrandoTrabajador);
            }
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Equals("N"))
            {//Insertando
                dapper.InsertTrabajador(txtNombre.Text, txtTelefono.Text, txtDNI.Text,txtID.Text);
            }
            else
            {//Update
                dapper.InsertTrabajador(txtNombre.Text, txtTelefono.Text, txtDNI.Text, txtID.Text);
            }
            refreshDatos();
        }

        private void buttonNuevo_Click(object sender, EventArgs e)
        {
            txtID.Text = "N";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtDNI.Text = "";
        }
    }
}

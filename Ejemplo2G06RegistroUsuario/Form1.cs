using Ejemplo2G06RegistroUsuario.DAL;
using Ejemplo2G06RegistroUsuario.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejemplo2G06RegistroUsuario
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CargarRoles();
            DesActivarBotones();
        }

        private void CargarRoles()
        {
            dgDatos.DataSource = RolDAL.ObtenerRoles();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Rol rol = new Rol();
            if (txtNombre.Text.Trim() != "")
            {
                rol.NombreRol = txtNombre.Text;
                if (RolDAL.Guardar(rol) != 0)
                {
                    CargarRoles();
                    limpiarControlesGuardar();
                }
                else
                    MessageBox.Show("Error interno al registrar el Rol", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("El nombre del rol es obligatorio", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
        }

        private void limpiarControlesGuardar()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
        }

        private void dgDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            { 
                DataGridViewRow filaSeleccionada = dgDatos.Rows[e.RowIndex];
                if (filaSeleccionada.Cells["IdRol"].Value != null
                    && filaSeleccionada.Cells["NombreRol"].Value != null)
                { 
                    var idRol = filaSeleccionada.Cells["IdRol"].Value.ToString();
                    var nombre = filaSeleccionada.Cells["NombreRol"].Value.ToString();

                    txtCodigo.Text = idRol;
                    txtNombre.Text = nombre;

                    ActivarBotones();
                }
            }
        }

        private void ActivarBotones()
        { 
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
            btnEliminar.Enabled = true;
        }

        private void DesActivarBotones()
        {
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
            btnEliminar.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Rol rol = new Rol();
            rol.IdRol = int.Parse(txtCodigo.Text);
            rol.NombreRol = txtNombre.Text;
            if (rol.IdRol != 0 && rol.NombreRol.Trim() != "")
            {
                    RolDAL.Modificar(rol);
                    CargarRoles();
                    DesActivarBotones();
                    limpiarControlesGuardar();
            }
            else
                MessageBox.Show("Debe seleccionar un registro del grid", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Rol rol = new Rol { 
                               IdRol = int.Parse(txtCodigo.Text), 
                               NombreRol = txtNombre.Text
                              };
            if (rol.IdRol != 0 && rol.NombreRol.Trim() != "")
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar?",
                                                      "Confirmacion", MessageBoxButtons.YesNo
                                                      , MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    RolDAL.Eliminar(rol);
                    CargarRoles();
                    DesActivarBotones();
                    limpiarControlesGuardar();
                }
            }
            else
                MessageBox.Show("Debe seleccionar un registro del grid", "Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Rol rol = new Rol { NombreRol = txtBuscar.Text};
            dgDatos.DataSource = RolDAL.BuscarRoles(rol);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

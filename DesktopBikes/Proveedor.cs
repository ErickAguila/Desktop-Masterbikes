using Controlador;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopBikes
{
    public partial class Proveedor : Form
    {
        public Proveedor()
        {
            InitializeComponent();
            ListarProveedor();
        }

        public void ListarProveedor()
        {
            DaoProveedor dao = new DaoProveedor();
            gvProveedor.DataSource = dao.ListarProveedores().Tables[0];
            this.gvProveedor.Columns[0].ReadOnly = true;
            this.gvProveedor.Columns[1].ReadOnly = true;
            this.gvProveedor.Columns[2].ReadOnly = true;
            gvProveedor.Refresh();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                DaoProveedor dao = new DaoProveedor();
                Modelo.Proveedor pro = new Modelo.Proveedor();
                int id = 0;
                if (txtIdProveedor.Text != "")
                {
                    id = Convert.ToInt32(txtIdProveedor.Text);
                }
                else
                {
                    id = 0;
                }
                    
                pro.nombre = txtNombre.Text;
                pro.id_usuario = Convert.ToInt32(txtIdAdministrador.Text);

                bool existe = dao.ExisteProveedor(id);
                if (existe)
                {
                    //Actaulizamos
                    bool resp = dao.ModificarProveedor(pro);
                    if (resp)
                    {
                        MessageBox.Show("Proveedor Actualizado", "Mensaje", MessageBoxButtons.OK);
                        ListarProveedor();
                    }
                    else
                    {
                        MessageBox.Show("Proveedor no Actualizado", "Mensaje", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    //Agregamos
                    bool resp = dao.AgregarProveedor(pro);
                    if (resp)
                    {
                        MessageBox.Show("Proveedor Agregado", "Mensaje", MessageBoxButtons.OK);
                        ListarProveedor();
                    }
                    else
                    {
                        MessageBox.Show("Proveedor no Agregado", "Mensaje", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (gvProveedor.SelectedRows.Count > 0)
            {
                txtIdProveedor.Text = gvProveedor.CurrentRow.Cells["ID_PROVEEDOR"].Value.ToString();
                txtNombre.Text = gvProveedor.CurrentRow.Cells["NOMBRE"].Value.ToString();
                txtIdAdministrador.Text = gvProveedor.CurrentRow.Cells["ID_USUARIO"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DaoProveedor dao = new DaoProveedor();
                int id = Convert.ToInt32(txtIdProveedor.Text);
                bool resp = dao.EmilinarProveedor(id);
                if (resp)
                {
                    MessageBox.Show("Proveedor eliminado", "Mensaje", MessageBoxButtons.OK);
                    ListarProveedor();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtIdProveedor.Text = "";
            txtNombre.Text = "";
        }
    }
}

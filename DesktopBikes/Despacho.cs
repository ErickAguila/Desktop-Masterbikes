using System;
using Controlador;
using Modelo;
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
    public partial class Despacho : Form
    {
        public Despacho()
        {
            InitializeComponent();
            ListarIdUsuario();
            ListarTipoDespacho();
            ListarEstadoDespacho();
            ListarDespacho();            
        }

        public void ListarEstadoDespacho()
        {
            DaoDespacho dao = new DaoDespacho();
            cboEstado.DataSource = dao.ListarEstadoDespacho().Tables[0];
            cboEstado.DisplayMember = "DESCRIPCION";
            cboEstado.ValueMember = "ID_ESTADODESPACHO";
        }

        public void ListarTipoDespacho()
        {
            DaoDespacho dao = new DaoDespacho();
            cboTipoDespacho.DataSource = dao.ListarTipoDespacho().Tables[0];
            cboTipoDespacho.DisplayMember = "NOMBRE";
            cboTipoDespacho.ValueMember = "ID_TIPODESPACHO";
        }

        public void ListarIdUsuario()
        {
            DaoDespacho dao = new DaoDespacho();
            cboIdUsuario.DataSource = dao.ListarIdUsuario().Tables[0];
            cboIdUsuario.DisplayMember = "ID_USUARIO";
            cboIdUsuario.ValueMember = "ID_USUARIO";
        }

        public void ListarDespacho()
        {
            DaoDespacho dao = new DaoDespacho();
            gvDespacho.DataSource = dao.ListarDespacho().Tables[0];
            this.gvDespacho.Columns[0].ReadOnly = true;
            this.gvDespacho.Columns[1].ReadOnly = true;
            this.gvDespacho.Columns[2].ReadOnly = true;
            this.gvDespacho.Columns[3].ReadOnly = true;
            this.gvDespacho.Columns[4].ReadOnly = true;
            this.gvDespacho.Columns[5].ReadOnly = true;
            gvDespacho.Refresh();
        }

        private void Despacho_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                DaoDespacho dao = new DaoDespacho();
                Modelo.Despacho despa = new Modelo.Despacho();

                
                int id = 0;

                if (txtIdDespacho.Text != "")
                {
                    id = Convert.ToInt32(txtIdDespacho.Text);
                    despa.id_despacho = id;
                }
                else
                {
                    id = 0;
                }


                despa.id_usuario = Convert.ToInt32(cboIdUsuario.SelectedIndex.ToString())+1;
                despa.direccion = txtDireccion.Text;
                despa.fechaCreacion = Convert.ToDateTime(dtFechaCreacion.Text);
                despa.id_tipoDespacho = Convert.ToInt32(cboTipoDespacho.SelectedIndex.ToString())+1;
                despa.id_estadoDespacho = Convert.ToInt32(cboEstado.SelectedIndex.ToString())+1;

                bool existe = dao.ExisteDespacho(id);
                if (existe)
                {
                    //Modifica el despacho
                    bool resp = dao.ModificarDespacho(despa);
                    if (resp)
                    {
                        MessageBox.Show("Despacho Actualizado", "Mensaje", MessageBoxButtons.OK);
                        ListarDespacho();
                    }
                    else
                    {
                        MessageBox.Show("Despacho no Actualizado", "Mensaje", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    //Agrega un nuevo despacho
                    bool resp = dao.GuardarDespacho(despa);
                    if (resp)
                    {
                        MessageBox.Show("Despacho creado", "Mensaje", MessageBoxButtons.OK);
                        ListarDespacho();
                    }
                    else
                    {
                        MessageBox.Show("Despacho no creado", "Mensaje", MessageBoxButtons.OK);
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (gvDespacho.SelectedRows.Count > 0)
            {
                txtIdDespacho.Text = gvDespacho.CurrentRow.Cells["ID_DESPACHO"].Value.ToString();
                txtDireccion.Text = gvDespacho.CurrentRow.Cells["DIRECCION"].Value.ToString();
                dtFechaCreacion.Text = gvDespacho.CurrentRow.Cells["FECHAENTREGA"].Value.ToString();
                cboTipoDespacho.Text = gvDespacho.CurrentRow.Cells["TIPO_DESPACHO"].Value.ToString();
                cboEstado.Text = gvDespacho.CurrentRow.Cells["ESTADO_DESPACHO"].Value.ToString();
                cboIdUsuario.Text = gvDespacho.CurrentRow.Cells["ID_USUARIO"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtIdDespacho.Text = "";
            txtDireccion.Text = "";
            dtFechaCreacion.Text = DateTime.Now.ToString();
            cboTipoDespacho.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;
            cboIdUsuario.SelectedIndex = 0;
        }
    }
}

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
    public partial class Bicicleta : Form
    {
        public Bicicleta()
        {
            InitializeComponent();
            ListarTipoBicicleta();
            ListarMarca();
            ListarModelo();
            ListarBicicletas();

            cboEnPromocion.Items.Add("Si");
            cboEnPromocion.Items.Add("No");
            cboEnPromocion.SelectedIndex = 0;
        }

        public void ListarModelo()
        {
            DaoBicicleta dao = new DaoBicicleta();
            cboModelo.DataSource = dao.ListarModelo().Tables[0];
            cboModelo.DisplayMember = "NOMBRE";
            cboModelo.ValueMember = "ID_MODELO";
        }

        public void ListarMarca()
        {
            DaoBicicleta dao = new DaoBicicleta();
            cboMarca.DataSource = dao.ListarMarca().Tables[0];
            cboMarca.DisplayMember = "Nombre";
            cboMarca.ValueMember = "ID_MARCA";
        }

        public void ListarTipoBicicleta()
        {
            DaoBicicleta dao = new DaoBicicleta();
            cboTipoBicicleta.DataSource = dao.ListarTipoBicicleta().Tables[0];
            cboTipoBicicleta.DisplayMember = "NOMBRE";
            cboTipoBicicleta.ValueMember = "ID_TIPOBICICLETA";
        }

        public void ListarBicicletas()
        {
            DaoBicicleta dao = new DaoBicicleta();
            gvBicicleta.DataSource = dao.ListarBicicleta().Tables[0];
            this.gvBicicleta.Columns[0].ReadOnly = true;
            this.gvBicicleta.Columns[1].ReadOnly = true;
            this.gvBicicleta.Columns[2].ReadOnly = true;
            this.gvBicicleta.Columns[3].ReadOnly = true;
            this.gvBicicleta.Columns[4].ReadOnly = true;
            this.gvBicicleta.Columns[5].ReadOnly = true;
            this.gvBicicleta.Columns[6].ReadOnly = true;
            gvBicicleta.Refresh();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                DaoBicicleta dao = new DaoBicicleta();
                Modelo.Bicicleta bici = new Modelo.Bicicleta();

                int id = 0;

                if (txtIdBicicleta.Text != "")
                {
                    id = Convert.ToInt32(txtIdBicicleta.Text);
                    bici.id_bicicleta = id;
                }
                else {
                    id = 0;
                }

                bici.id_marca = Convert.ToInt32(cboMarca.SelectedIndex.ToString())+1;
                bici.id_modelo = Convert.ToInt32(cboModelo.SelectedIndex.ToString())+1;
                bici.id_tipoBicicleta = Convert.ToInt32(cboTipoBicicleta.SelectedIndex.ToString())+1;
                bici.precio = Convert.ToInt32(txtPrecio.Text);
                bici.imagen = txtImagen.Text;

                if (cboEnPromocion.SelectedIndex.ToString() == "0")
                {
                    bici.enpromocion = true;
                }
                else
                {
                    bici.enpromocion = false;
                }
                bool existe = dao.ExisteBicicleta(id);
                if (existe)
                {
                    bool resp = dao.ModificarBicicleta(bici);
                    if (resp)
                    {
                        MessageBox.Show("Bicicleta Modificada", "Mensaje", MessageBoxButtons.OK);
                        ListarBicicletas();
                    }
                    else
                    {
                        MessageBox.Show("Bicicleta  NO Actaulizada", "Mensaje", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    bool resp = dao.AgregarBicicleta(bici);
                    if (resp)
                    {
                        MessageBox.Show("Bicicleta Registrada", "Mensaje", MessageBoxButtons.OK);
                        ListarBicicletas();
                    }
                    else
                    {
                        MessageBox.Show("Bicicleta  NO Registrada", "Mensaje", MessageBoxButtons.OK);
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
            if (gvBicicleta.SelectedRows.Count > 0)
            {
                txtIdBicicleta.Text = gvBicicleta.CurrentRow.Cells["ID_BICICLETA"].Value.ToString();
                cboTipoBicicleta.Text = gvBicicleta.CurrentRow.Cells["TIPO_BICICLETA"].Value.ToString();
                cboMarca.Text = gvBicicleta.CurrentRow.Cells["MARCA"].Value.ToString();
                cboModelo.Text = gvBicicleta.CurrentRow.Cells["MODELO"].Value.ToString();
                txtPrecio.Text = gvBicicleta.CurrentRow.Cells["PRECIO"].Value.ToString();
                cboEnPromocion.Text = gvBicicleta.CurrentRow.Cells["ENPROMOCION"].Value.ToString();
                txtImagen.Text = gvBicicleta.CurrentRow.Cells["IMAGEN"].Value.ToString();
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
                DaoBicicleta dao = new DaoBicicleta();
                int id = Convert.ToInt32(txtIdBicicleta.Text);
                bool resp = dao.EliminarBicicleta(id);
                if (resp)
                {
                    MessageBox.Show("Bicicleta eliminado", "Mensaje", MessageBoxButtons.OK);
                    ListarBicicletas();
                }
                else {
                    MessageBox.Show("Bicicleta No eliminada", "Mensaje", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboTipoBicicleta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtIdBicicleta.Text="";
            cboMarca.SelectedIndex = 0;
            cboModelo.SelectedIndex = 0;
            cboTipoBicicleta.SelectedIndex = 0;
            txtPrecio.Text = "";
            cboEnPromocion.SelectedIndex = 0;
            txtImagen.Text = "";
        }
    }
}

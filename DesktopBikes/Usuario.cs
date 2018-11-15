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
    public partial class Usuario : Form
    {
        public Usuario()
        {
            InitializeComponent();
            ListarUsuario();

            ListaTipoUsuario();
            ListarComuna();
            ListarSexo();
        }

        public void ListarSexo()
        {
            DaoUsuario dao = new DaoUsuario();
            cboSexo.DataSource = dao.ListarSexo().Tables[0];
            cboSexo.DisplayMember = "NOMBRE";
            cboSexo.ValueMember = "ID_SEXO";
        }

        public void ListarComuna()
        {
            DaoUsuario dao = new DaoUsuario();
            cboComuna.DataSource = dao.ListarComuna().Tables[0];
            cboComuna.DisplayMember = "NOMBRE";
            cboComuna.ValueMember = "ID_COMUNA";
        }

        public void ListaTipoUsuario()
        {
            DaoUsuario dao = new DaoUsuario();
            cboTipoUsuario.DataSource = dao.ListarTipoUsuario().Tables[0];
            cboTipoUsuario.DisplayMember = "NOMBRE";
            cboTipoUsuario.ValueMember = "ID_TIPOUSUARIO";
        }
                

        public void ListarUsuario()
        {
            DaoUsuario dao = new DaoUsuario();
            List<Modelo.Usuario> usuario = new List<Modelo.Usuario>();

            //usuario = dao.ListarUsuario();
            gvUsuario.DataSource = dao.ListarUsuario().Tables[0];
            this.gvUsuario.Columns[0].ReadOnly = true;
            this.gvUsuario.Columns[1].ReadOnly = true;
            this.gvUsuario.Columns[2].ReadOnly = true;
            this.gvUsuario.Columns[3].ReadOnly = true;
            this.gvUsuario.Columns[4].ReadOnly = true;
            this.gvUsuario.Columns[5].ReadOnly = true;
            this.gvUsuario.Columns[6].ReadOnly = true;
            this.gvUsuario.Columns[7].ReadOnly = true;
            this.gvUsuario.Columns[8].ReadOnly = true;
            this.gvUsuario.Columns[9].ReadOnly = true;
            this.gvUsuario.Columns[10].ReadOnly = true;
            gvUsuario.Refresh();
        
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //btnAgregar
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DaoUsuario dao = new DaoUsuario();
                Modelo.Usuario user = new Modelo.Usuario();

                string rut = txtRut.Text;

                user.rut = txtRut.Text;
                user.nombre = txtNombre.Text;
                user.apellido = txtApellido.Text;
                user.email = txtEmail.Text;
                user.id_sexo = Convert.ToInt32(cboSexo.SelectedIndex.ToString())+1;
                user.edad = Convert.ToInt32(txtEdad.Text);
                user.direccion = txtDireccion.Text;
                user.username = txtUsername.Text;
                user.password = txtPassword.Text;
                user.id_comuna = Convert.ToInt32(cboComuna.SelectedIndex.ToString())+1;
                user.id_tipoUsuario = Convert.ToInt32(cboTipoUsuario.SelectedIndex.ToString())+1;

                bool existe = dao.ExisteUsuario(rut);
                if (existe)
                {
                    //Se actualiza el dato registrado
                    bool resp = dao.ModificarUsuario(user);
                    if (resp)
                    {
                        MessageBox.Show("Usuario Actualizada", "Mensaje", MessageBoxButtons.OK);
                        ListarUsuario();
                    }
                    else
                    {
                        MessageBox.Show("Usuario no Actualizado", "Mensaje", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    //Se crea un nuevo Objeto
                    bool resp = dao.AgregarUsuario(user);
                    if (resp)
                    {
                        MessageBox.Show("Usuario Registrado", "Mensaje", MessageBoxButtons.OK);
                        ListarUsuario();
                    }
                    else
                    {
                        MessageBox.Show("Usuario no registrado", "Mensaje", MessageBoxButtons.OK);
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
            if (gvUsuario.SelectedRows.Count > 0)
            {
                txtRut.Text = gvUsuario.CurrentRow.Cells["RUT"].Value.ToString();
                txtNombre.Text = gvUsuario.CurrentRow.Cells["NOMBRE"].Value.ToString();
                txtApellido.Text = gvUsuario.CurrentRow.Cells["APELLIDO"].Value.ToString();
                txtEmail.Text = gvUsuario.CurrentRow.Cells["EMAIL"].Value.ToString();
                txtEdad.Text = gvUsuario.CurrentRow.Cells["EDAD"].Value.ToString();
                txtDireccion.Text = gvUsuario.CurrentRow.Cells["DIRECCION"].Value.ToString();
                txtUsername.Text = gvUsuario.CurrentRow.Cells["USERNAME"].Value.ToString();
                txtPassword.Text = gvUsuario.CurrentRow.Cells["PASSWORD"].Value.ToString();
                cboComuna.Text = gvUsuario.CurrentRow.Cells["COMUNA"].Value.ToString();
                cboTipoUsuario.Text = gvUsuario.CurrentRow.Cells["TIPO_USUARIO"].Value.ToString();
                cboSexo.Text = gvUsuario.CurrentRow.Cells["SEXO"].Value.ToString();
            }
            else {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DaoUsuario dao = new DaoUsuario();
                string rut = txtRut.Text;
                bool resp = dao.EmilinarUsuario(rut);
                if (resp)
                {
                    MessageBox.Show("Usuario eliminado", "Mensaje", MessageBoxButtons.OK);
                    ListarUsuario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Usuario_Load(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtRut.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            cboSexo.SelectedIndex = 0;
            txtEdad.Text = "";
            txtDireccion.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            cboComuna.SelectedIndex = 0;
            cboTipoUsuario.SelectedIndex = 0;
        }
    }
}

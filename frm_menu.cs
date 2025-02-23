using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT_Movie_2025
{
    public partial class frm_menu: Form
    {
        public frm_menu()
        {
            InitializeComponent();
        }

        private void btn_adduser_Click(object sender, EventArgs e)
        {
            adduser add = new adduser();
            add.Show();
            this.Hide();

        }

        private void btn_return_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void btn_rent_Click(object sender, EventArgs e)
        {
            frm_rent rent = new frm_rent();
            rent.Show();
            this.Hide();
        }

        private void btn_addmember_Click(object sender, EventArgs e)
        {
            frm_member mem = new frm_member();
            mem.Show();
            this.Hide();
        }

        private void btn_addtype_Click(object sender, EventArgs e)
        {
            frm_movie_type type = new frm_movie_type();
            type.Show();
            this.Hide();

        }

        private void btn_addmovie_Click(object sender, EventArgs e)
        {
            frm_movie MV = new frm_movie();
            MV.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login f = new Login(); //ເຊື່ອມລິ້ງໄປຫນ້າອື່ນ
            f.ShowDialog();
            this.Hide();
        }
    }
}

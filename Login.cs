using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT_Movie_2025
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("ກະລຸນາຕື່ມຂໍ້ມູນໃສ່ໃຫ້ຄົບຖ້ວນ");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Select();
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("ກະລຸນາຕື່ມຂໍ້ມູນໃສ່ໃຫ້ຄົບຖ້ວນ");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Select();
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection("Data Source=NONG; Initial Catalog=NT_Movie_2025; Integrated Security=SSPI;");
                    SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_user WHERE user_email=@user_email and user_pass=@user_pass", con);
                    cmd.Parameters.AddWithValue("@user_email", textBox1.Text);
                    cmd.Parameters.AddWithValue("@user_pass", textBox2.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0) {
                        MessageBox.Show("ຍິນດີຕ້ອນຮັບເຂົ້າສູ່ລະບົບ");
                        frm_menu f = new frm_menu(); //ເຊື່ອມລິ້ງໄປຫນ້າອື່ນ
                        f.ShowDialog();
                        this.Hide();
                        


                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox1.Select();

                    }
                    else
                    {
                        MessageBox.Show("ຊື່ຜູ້ໃຊ້ ຫຼື ລະຫັດຜ່ານບໍ່ຖືກຕ້ອງ");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox1.Select();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}

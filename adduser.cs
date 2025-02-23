using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NT_Movie_2025
{
    public partial class adduser : Form
    {
        public adduser()
        {
            InitializeComponent();
            Auto_id();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-DG7MDRT; Initial Catalog=NT_Movie_2025; Integrated Security=SSPI;");
        SqlCommand cmd;
        SqlDataAdapter da;
        string sql;

        public void Auto_id()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                sql = "SELECT MAX(user_id) FROM [tbl_user]";
                cmd = new SqlCommand(sql, con);
                var result = cmd.ExecuteScalar();

                if (result == DBNull.Value || result == null || string.IsNullOrEmpty(result.ToString()))
                {
                    textBox1.Text = "A-000001";
                }
                else
                {
                    string lastId = result.ToString();

                    // ตรวจสอบว่ามีความยาวมากพอหรือไม่
                    if (lastId.Length >= 8 && lastId.StartsWith("A-"))
                    {
                        int inval = int.Parse(lastId.Substring(2, 6)) + 1;
                        textBox1.Text = string.Format("A-{0:000000}", inval);
                    }
                    else
                    {
                        textBox1.Text = "A-000001"; // ตั้งค่าเริ่มต้นหากรูปแบบไม่ถูกต้อง
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in Auto_id: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                sql = "SELECT user_id AS 'ລະຫັດ', user_email AS 'ອີເມວ', user_pass AS 'ລະຫັດຜ່ານ' FROM [tbl_user] ORDER BY user_id ASC";
                cmd = new SqlCommand(sql, con);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in btn_show_Click: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                btn_save.Enabled = false;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                sql = "INSERT INTO [tbl_user] (user_id,user_email, user_pass) VALUES (@user_id, @user_email, @user_pass)";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@user_id", textBox1.Text);
                cmd.Parameters.AddWithValue("@user_email", textBox2.Text);
                cmd.Parameters.AddWithValue("@user_pass", textBox3.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("ບັນທຶກຂໍໍມູນສຳເລັດ");
                Auto_id();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear(); // Clear password field
                btn_save.Enabled = true;
                btn_show_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in btn_save_Click: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                sql = "UPDATE [tbl_user] SET user_email=@user_email, user_pass=@user_pass WHERE user_id=@user_id";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@user_id", textBox1.Text);
                cmd.Parameters.AddWithValue("@user_email", textBox2.Text);
                cmd.Parameters.AddWithValue("@user_pass", textBox3.Text); // Update password as well
                cmd.ExecuteNonQuery();

                MessageBox.Show("ແກ້ໄຂຂໍ້ມູນສຳເລັດ");
                Auto_id();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear(); // Clear password field
                btn_save.Enabled = true;
                btn_show_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in btn_update_Click: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                sql = "DELETE FROM [tbl_user] WHERE user_id=@user_id";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@user_id", textBox1.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("ລົບຂໍໍມູນສຳເລັດ");
                Auto_id();
                btn_save.Enabled = true;
                btn_show_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in btn_delete_Click: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                sql = "SELECT user_id AS 'ລະຫັດ',user_email AS 'ອີເມວ', user_pass AS 'ລະຫັດຜ່ານ' FROM" +
                    " [tbl_user] WHERE user_id LIKE @search OR user_email LIKE @search OR user_pass LIKE @search";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@search", "%" + txt_search.Text + "%");
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in btn_search_Click: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            btn_search_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_menu f = new frm_menu(); //ເຊື່ອມລິ້ງໄປຫນ້າອື່ນ
            f.ShowDialog();
            this.Hide();
        }
    }
}

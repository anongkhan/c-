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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NT_Movie_2025
{
    public partial class frm_movie_type : Form
    {
        public frm_movie_type()
        {
            InitializeComponent();
            Auto_id();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-DG7MDRT; Initial Catalog=NT_Movie_2025; Integrated Security=SSPI;");
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        string sql;
        public void Auto_id()
        {
            try
            {
                con.Open();
                string sql = "SELECT MAX(Movie_type_id) FROM tbl_movie_type";
                SqlCommand cmd = new SqlCommand(sql, con);
                var maxid = cmd.ExecuteScalar();

                int newId = 1; // Default starting ID if no records exist

                if (maxid != DBNull.Value && maxid != null)
                {
                    newId = Convert.ToInt32(maxid) + 1;
                }

                textBox1.Text = newId.ToString();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btn_show_Click(object sender, EventArgs e)
        {
            con.Open();


            sql = "SELECT Movie_type_id as 'ລະຫັດປະເພດໜັງ', Movie_name_type as 'ຊື່ປະເພດໜັງ' " +
                  " FROM tbl_movie_type ORDER BY Movie_type_id ASC";

            cmd = new SqlCommand(sql, con);
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
            textBox4.Select();

        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            sql = "INSERT INTO tbl_movie_type (Movie_type_id, Movie_name_type) VALUES ('" + textBox1.Text + "',N'" + textBox4.Text + "')";
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("ບັນທຶກຂໍ້ມູນສຳເລັດ");
            con.Close();
            Auto_id();
            textBox4.Text = "";
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            con.Open();
            sql = "UPDATE tbl_movie_type SET " +
                "Movie_name_type=@nametype " +
                "WHERE Movie_type_id='" + textBox1.Text + "'";
            cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@typeid", textBox1.Text);
            cmd.Parameters.AddWithValue("@nametype", textBox4.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("ແກ້ໄຂຂໍ້ມູນສຳເລັດ");
            con.Close();
            Auto_id();
            textBox4.Text = "";
            //  btn_save.Enabled = true;
            textBox4.Select();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            con.Open();
            sql = "DELETE tbl_movie_type WHERE Movie_type_id='" + textBox1.Text + "'";
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();


            con.Close();
            Auto_id();
            //btn_save.Enabled = true;
            textBox4.Select();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            con.Open();
            sql = "SELECT  " +
                " Movie_type_id as 'ລະຫັດປະເພດໜັງ',Movie_name_type as 'ຊື່ປະເພດໜັງ'" +
                " FROM tbl_movie_type WHERE Movie_type_id LIKE N'%" + txt_search.Text + "%'";
            cmd = new SqlCommand(sql, con);
            da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();


                //btn_save.Enabled = false;
                textBox4.Select();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_menu f = new frm_menu(); //ເຊື່ອມລິ້ງໄປຫນ້າອື່ນ
            f.ShowDialog();
            this.Hide();
        }
    }
}

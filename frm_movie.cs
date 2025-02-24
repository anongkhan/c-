using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace NT_Movie_2025
{
    public partial class frm_movie : Form
    {
        public frm_movie()
        {
            InitializeComponent();
            Auto_id();
            load_id();

        }
        SqlConnection con = new SqlConnection("Data Source=NONG; Initial Catalog=NT_Movie_2025; Integrated Security=SSPI;");
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        string sql;
        public void Auto_id()
        {
            try
            {
                con.Open();
                string sql = "SELECT MAX(Movie_id) FROM tbl_movie";
                SqlCommand cmd = new SqlCommand(sql, con);
                var maxid = cmd.ExecuteScalar();

                int newId = 1; // Default starting ID if no records exist

                if (maxid != DBNull.Value && maxid != null)
                {
                    newId = Convert.ToInt32(maxid) + 1;
                }

                textBox2.Text = newId.ToString();

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


            sql = "SELECT Movie_id as 'ລະຫັດ', Movie_name as 'ຊື່ຫນັງ'," +
                  " Movie_type_id as 'ລະຫັດປະເພດ', Movie_name_type as 'ຊື່ປະເພດ' " +
                  " FROM tbl_movie ORDER BY Movie_id ASC";

            cmd = new SqlCommand(sql, con);
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
            textBox1.Select();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                comboBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

                btn_save.Enabled = false;
                textBox1.Select();
            }

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            sql = "INSERT INTO tbl_movie (movie_id, movie_name, movie_type_id, movie_name_type) VALUES ('" + textBox2.Text + "',N'" + textBox1.Text + "',N'" + comboBox2.Text + "','" + textBox4.Text + "')";
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("ບັນທຶກຂໍ້ມູນສຳເລັດ");
            con.Close();
            Auto_id();
            textBox1.Text = "";
            comboBox2.Text = "";
            textBox4.Text = "";
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            con.Open();
            sql = "UPDATE tbl_movie SET Movie_name=@movie_name, " +
                "Movie_type_id=@typeid,Movie_name_type=@nametype " +
                "WHERE Movie_id='" + textBox2.Text + "'";
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@movie_id", textBox1.Text);
            cmd.Parameters.AddWithValue("@movie_name", textBox1.Text);
            cmd.Parameters.AddWithValue("@typeid", comboBox2.Text);
            cmd.Parameters.AddWithValue("@nametype", textBox4.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("ແກ້ໄຂຂໍ້ມູນສຳເລັດ");
            con.Close();
            Auto_id();
            textBox1.Text = "";
            comboBox2.Text = "";
            textBox4.Text = "";
            btn_save.Enabled = true;
            textBox1.Select();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            con.Open();
            sql = "DELETE tbl_movie WHERE Movie_id='" + textBox2.Text + "'";
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();


            con.Close();
            Auto_id();
            btn_save.Enabled = true;
            textBox1.Select();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            con.Open();
            sql = "SELECT Movie_id as 'ລະຫັດ', Movie_name as 'ຊື່ຫນັງ', " +
                " Movie_type_id as 'ລະຫັດຫນັງ',Movie_name_type as 'ຊື່ປະເພດ'" +
                " FROM tbl_movie WHERE Movie_name LIKE N'%"
                + txt_search.Text + "%' or Movie_name_type LIKE N'%" + txt_search.Text + "%'";
            cmd = new SqlCommand(sql, con);
            da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            sql = "SELECT Movie_id as 'ລະຫັດ', Movie_name as 'ຊື່ຫນັງ', " +
                " Movie_type_id as 'ລະຫັດຫນັງ',Movie_name_type as 'ຊື່ປະເພດ'" +
                " FROM tbl_movie WHERE Movie_name LIKE N'%"
                + txt_search.Text + "%' or Movie_name_type LIKE N'%" + txt_search.Text + "%'";
            cmd = new SqlCommand(sql, con);
            da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }
        public void load_id()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            sql = "SELECT * FROM tbl_movie_type";
            cmd = new SqlCommand(sql, con);
            da = new SqlDataAdapter(sql, con);
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "Movie_type_id";
            comboBox2.ValueMember = "Movie_type_id";
            con.Close();
        }
        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd = new SqlCommand("SELECT * FROM tbl_movie_type WHERE Movie_type_id='" + comboBox2.Text + "'", con);
            cmd.ExecuteNonQuery();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string name = (string)dr["Movie_name_type"].ToString();
                textBox4.Text = name;

            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_menu f = new frm_menu(); //ເຊື່ອມລິ້ງໄປຫນ້າອື່ນ
            f.ShowDialog();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Show();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {

        }
    }

}


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT_Movie_2025
{
    public partial class frm_rent : Form
    {
        int total, price, amount;

        public frm_rent()
        {
            InitializeComponent();
            Auto_id();
            LoadMemberIDs();
            LoadMovieIDs();
            loadData();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-DG7MDRT; Initial Catalog=NT_Movie_2025; Integrated Security=SSPI;");
        SqlCommand cmd;
        SqlDataAdapter da;
        string sql;

        public void Auto_id()
        {
            try
            {
                con.Open();
                sql = "SELECT MAX(Rent_id) FROM tbl_rent";
                cmd = new SqlCommand(sql, con);
                var maxid = cmd.ExecuteScalar()?.ToString();
                con.Close();

                if (string.IsNullOrEmpty(maxid))
                {
                    textBox1.Text = "U-000001";
                }
                else
                {
                    if (int.TryParse(maxid.Substring(2), out int inval))
                    {
                        inval++;
                        textBox1.Text = $"U-{inval:D6}";
                    }
                    else
                    {
                        MessageBox.Show("Error: Invalid Rent_id format in database.");
                        textBox1.Text = "U-000001";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Auto_id: " + ex.Message);
            }
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            loadData();
        }

        public void loadData()
        {
            con.Open();
            sql = "SELECT Rent_no, Rent_id, Mem_id, Mem_name, Movie_id, Movie_name, Movie_name_type, Rent_date, Return_date, Rent_Price, Rent_Amount, Rent_Total FROM tbl_rent ORDER BY Rent_id DESC";
            cmd = new SqlCommand(sql, con);
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            textBox2.Select();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                sql = "INSERT INTO tbl_rent (Rent_id, Mem_id, Mem_name, Movie_id, Movie_name, Movie_name_type, Rent_date, Return_date, Rent_Price, Rent_Amount, Rent_Total) " +
                      "VALUES (@RentID, @MemID, @MemName, @MovieID, @MovieName, @MovieNametype, @RentDate, @ReturnDate, @RentPrice, @RentAmount, @RentTotal)";

                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@RentID", textBox1.Text);
                cmd.Parameters.AddWithValue("@MemID", comboBox1.Text);
                cmd.Parameters.AddWithValue("@MemName", textBox2.Text);
                cmd.Parameters.AddWithValue("@MovieID", comboBox2.Text);
                cmd.Parameters.AddWithValue("@MovieName", textBox3.Text);
                cmd.Parameters.AddWithValue("@MovieNametype", textBox4.Text);
                cmd.Parameters.AddWithValue("@RentDate", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@ReturnDate", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@RentPrice", decimal.Parse(comboBox3.Text));
                cmd.Parameters.AddWithValue("@RentAmount", int.Parse(comboBox4.Text));
                cmd.Parameters.AddWithValue("@RentTotal", textBox5.Text); // Use parameter for Rent_Total

                cmd.ExecuteNonQuery();
                MessageBox.Show("Insert successful!");

                // Clear input fields
                comboBox1.Text = "";
                textBox2.Text = "";
                comboBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                textBox5.Text = "";

                con.Close();
                Auto_id();
                loadData(); // Refresh DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting record: " + ex.Message);
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                sql = "UPDATE tbl_rent SET Mem_id=@MemID, Mem_name=@MemName, Movie_id=@MovieID, Movie_name=@MovieName, Movie_name_type=@MovieNametype, Rent_date=@RentDate, Return_date=@ReturnDate, Rent_Price=@RentPrice, Rent_Amount=@RentAmount, Rent_Total=@RentTotal WHERE Rent_id=@RentID";

                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@RentID", textBox1.Text);
                cmd.Parameters.AddWithValue("@MemID", comboBox1.Text);
                cmd.Parameters.AddWithValue("@MemName", textBox2.Text);
                cmd.Parameters.AddWithValue("@MovieID", comboBox2.Text);
                cmd.Parameters.AddWithValue("@MovieName", textBox3.Text);
                cmd.Parameters.AddWithValue("@MovieNametype", textBox4.Text);
                cmd.Parameters.AddWithValue("@RentDate", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@ReturnDate", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@RentPrice", decimal.Parse(comboBox3.Text));
                cmd.Parameters.AddWithValue("@RentAmount", int.Parse(comboBox4.Text));
                cmd.Parameters.AddWithValue("@RentTotal", textBox5.Text); // Use parameter for Rent_Total

                cmd.ExecuteNonQuery();
                MessageBox.Show("Update successful!");

                con.Close();
                Auto_id();
                loadData(); // Refresh DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating record: " + ex.Message);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                sql = "DELETE FROM tbl_rent WHERE Rent_id=@RentID";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@RentID", textBox1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete successful!");
                con.Close();
                loadData(); // Refresh DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                sql = "SELECT * FROM tbl_rent WHERE Mem_id LIKE @Search OR Mem_name LIKE @Search OR Movie_id LIKE @Search OR Movie_name LIKE @Search";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Search", "%" + txt_search.Text.Trim() + "%");

                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching records: " + ex.Message);
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            loadData(); // Optionally reload data or filter based on search text
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                comboBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                comboBox3.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                comboBox4.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                btn_save.Enabled = false;
                comboBox1.Select();
            }
        }

        private void LoadMemberIDs()
        {
            try
            {
                con.Open();
                sql = "SELECT Mem_id FROM tbl_member";
                cmd = new SqlCommand(sql, con);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                comboBox1.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    comboBox1.Items.Add(row["Mem_id"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading member IDs: " + ex.Message);
            }
        }

        private void mem_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;

            try
            {
                con.Open();
                sql = "SELECT Mem_name FROM tbl_member WHERE Mem_id = @Mem_id";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Mem_id", comboBox1.SelectedItem.ToString());
                var result = cmd.ExecuteScalar();
                con.Close();

                if (result != null)
                {
                    textBox2.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show("Member not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving member info: " + ex.Message);
            }
        }

        private void LoadMovieIDs()
        {
            try
            {
                con.Open();
                sql = "SELECT Movie_id FROM tbl_movie";
                cmd = new SqlCommand(sql, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                con.Close();

                comboBox2.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    comboBox2.Items.Add(row["Movie_id"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading movie IDs: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_menu f = new frm_menu(); // Link to another form
            f.ShowDialog();
            this.Hide();
        }

        private void movie_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null) return;

            try
            {
                con.Open();
                sql = "SELECT Movie_name, Movie_name_type FROM tbl_movie WHERE Movie_id = @MovieID";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@MovieID", comboBox2.SelectedItem.ToString());

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox3.Text = reader["Movie_name"].ToString();
                    textBox4.Text = reader["Movie_name_type"].ToString();
                }
                else
                {
                    MessageBox.Show("Movie not found.");
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving movie info: " + ex.Message);
            }
        }

        private void rent_amount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(comboBox3.Text, out price) && int.TryParse(comboBox4.Text, out amount))
            {
                total = price * amount;
                textBox5.Text = total.ToString();
            }
        }
    }
}

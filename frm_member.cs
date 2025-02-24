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
    public partial class frm_member : Form
    {
        public frm_member()
        {
            InitializeComponent();
            LoadData();
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
                if (con.State == ConnectionState.Closed) con.Open();

                sql = "SELECT MAX(Mem_id) FROM tbl_member";
                cmd = new SqlCommand(sql, con);
                var maxid = cmd.ExecuteScalar()?.ToString();

                if (string.IsNullOrEmpty(maxid))
                {
                    textBox1.Text = "U-202501";
                }
                else
                {
                    if (maxid.Length >= 8)
                    {
                        int inval = int.Parse(maxid.Substring(2, 6));
                        inval++;
                        textBox1.Text = String.Format("U-{0:000000}", inval);
                    }
                    else
                    {
                        textBox1.Text = "U-202501";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดใน Auto_id: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                con.Open();
                sql = "SELECT * FROM tbl_member ORDER BY Mem_id DESC";
                da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                btn_save.Enabled = true;
                Auto_id();

                // ล้างฟิลด์ข้อมูล
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูล: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void ClearInputFields()
        {
            textBox2.Text = "";
            textBox5.Text = "";
            textBox4.Text = "";
            comboBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index != -1)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

                // ตรวจสอบว่าค่าที่ดึงมาเป็น DBNull หรือ null หรือไม่
                if (dataGridView1.CurrentRow.Cells[2].Value != DBNull.Value && dataGridView1.CurrentRow.Cells[2].Value != null)
                {
                    try
                    {
                        dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[2].Value);
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show("เกิดข้อผิดพลาดในรูปแบบวันที่: " + ex.Message);
                        dateTimePicker1.Value = DateTime.Now; // ตั้งค่าวันที่เป็นวันที่ปัจจุบันเป็นทางเลือก
                    }
                }
                else
                {
                    dateTimePicker1.Value = DateTime.Now; // ตั้งค่าวันที่เป็นวันที่ปัจจุบันถ้าค่าคือ NULL
                }

                textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                comboBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();

                btn_save.Enabled = false;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                sql = "INSERT INTO tbl_member (Mem_id, Mem_name, Mem_date, Mem_village, Mem_district, Mem_province, Mem_tel, Mem_email) " +
                      "VALUES (@MemID, @MemName, @MemDate, @MemVillage, @MemDistrict, @MemProvince, @MemTel, @MemEmail)";

                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@MemID", textBox1.Text);
                cmd.Parameters.AddWithValue("@MemName", textBox2.Text);
                cmd.Parameters.AddWithValue("@MemDate", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@MemVillage", textBox3.Text);
                cmd.Parameters.AddWithValue("@MemDistrict", comboBox2.Text);
                cmd.Parameters.AddWithValue("@MemProvince", comboBox1.Text);

                // ตรวจสอบว่าเบอร์โทรศัพท์ (Mem_tel) ไม่ว่าง
                string memTel = textBox4.Text.Trim();
                if (string.IsNullOrEmpty(memTel))
                {
                    MessageBox.Show("กรุณากรอกหมายเลขโทรศัพท์ให้ถูกต้อง");
                    return;
                }
                cmd.Parameters.AddWithValue("@MemTel", memTel);
                cmd.Parameters.AddWithValue("@MemEmail", textBox5.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("บันทึกข้อมูลสำเร็จ!");

                Auto_id();
                LoadData();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("ข้อผิดพลาดของ SQL: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการบันทึกข้อมูล: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                sql = "UPDATE tbl_member SET Mem_name=@MemName, Mem_date=@MemDate, Mem_village=@MemVillage, " +
                      "Mem_district=@MemDistrict, Mem_province=@MemProvince, Mem_tel=@MemTel, Mem_email=@MemEmail " +
                      "WHERE Mem_id=@MemID";

                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@MemID", textBox1.Text);
                cmd.Parameters.AddWithValue("@MemName", textBox2.Text);
                cmd.Parameters.AddWithValue("@MemDate", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@MemVillage", textBox3.Text);
                cmd.Parameters.AddWithValue("@MemDistrict", comboBox2.Text);
                cmd.Parameters.AddWithValue("@MemProvince", comboBox1.Text);
                cmd.Parameters.AddWithValue("@MemTel", textBox4.Text);
                cmd.Parameters.AddWithValue("@MemEmail", textBox5.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("อัปเดตสำเร็จ!");

                Auto_id();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการอัปเดตข้อมูล: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                sql = "DELETE FROM tbl_member WHERE Mem_id=@MemID";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@MemID", textBox1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("ลบข้อมูลสำเร็จ!");

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการลบข้อมูล: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();

                // Correct SQL statement, including MemDate
                string sql = "SELECT * FROM tbl_member " +
                             "WHERE Mem_id LIKE @Search OR Mem_name LIKE @Search " +
                             "OR Mem_tel LIKE @Search OR Mem_email LIKE @Search";

                cmd = new SqlCommand(sql, con);
                // Execute query
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการค้นหาข้อมูล: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            LoadData(); // Optionally reload data or filter based on search text
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            frm_menu f = new frm_menu();
            f.ShowDialog();
            this.Hide();
        }
    }
}

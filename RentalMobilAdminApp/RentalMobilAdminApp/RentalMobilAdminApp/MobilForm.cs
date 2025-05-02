using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;


namespace RentalMobilAdminApp
{
    public partial class MobilForm : Form
    {
        private string connectionString = "server=localhost;user=root;password=;database=rentalmobil;";
        private int selectedMobilId = -1;

        public MobilForm()
        {
            InitializeComponent();
            LoadData();
            dataGridView1.CellClick += dataGridView1_CellClick;

            button7.Click += button7_Click; // Tambah
            button8.Click += button8_Click; // Ubah
            button9.Click += button9_Click; // Hapus
        }

        private void LoadData()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, merk, tipe, harga_sewa, status FROM mobil";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat load data: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selectedMobilId = Convert.ToInt32(row.Cells["id"].Value);

                textBox1.Text = row.Cells["merk"].Value.ToString();
                textBox3.Text = row.Cells["tipe"].Value.ToString();
                textBox2.Text = row.Cells["harga_sewa"].Value.ToString();
                textBox4.Text = row.Cells["status"].Value.ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO mobil (merk, tipe, harga_sewa, status) VALUES (@merek, @tipe, @harga_sewa, @status)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@merek", textBox1.Text);
                    cmd.Parameters.AddWithValue("@tipe", textBox3.Text);
                    cmd.Parameters.AddWithValue("@harga_sewa", textBox2.Text);
                    cmd.Parameters.AddWithValue("@status", textBox4.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data mobil berhasil ditambahkan.");
                    LoadData();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat tambah data: " + ex.Message);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (selectedMobilId == -1)
            {
                MessageBox.Show("Pilih mobil yang ingin diubah.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE mobil SET merk=@merek, tipe=@tipe, harga_sewa=@harga_sewa, status=@status WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@merek", textBox1.Text);
                    cmd.Parameters.AddWithValue("@tipe", textBox3.Text);
                    cmd.Parameters.AddWithValue("@harga_sewa", textBox2.Text);
                    cmd.Parameters.AddWithValue("@status", textBox4.Text);
                    cmd.Parameters.AddWithValue("@id", selectedMobilId);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data mobil berhasil diubah.");
                    LoadData();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat ubah data: " + ex.Message);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (selectedMobilId == -1)
            {
                MessageBox.Show("Pilih mobil yang ingin dihapus.");
                return;
            }

            DialogResult result = MessageBox.Show("Yakin ingin menghapus mobil ini?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM mobil WHERE id=@id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", selectedMobilId);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data mobil berhasil dihapus.");
                        LoadData();
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saat hapus data: " + ex.Message);
                    }
                }
            }
        }

        private void ClearForm()
        {
            textBox1.Text = "Merek";
            textBox3.Text = "Tipe";
            textBox2.Text = "Harga Sewa";
            textBox4.Text = "Status";
            selectedMobilId = -1;
        }
    }
}

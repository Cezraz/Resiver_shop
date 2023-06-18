using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using MySql.Data.MySqlClient;

namespace kyrsa4
{
    public partial class AdminMenu : Form
    {
        public string queryString = @"SELECT * FROM  resiver";
        public static string connect = Main.str_connect.ToString();
        public MySqlConnection con = new MySqlConnection(connect);
        public DataTable pdt = new DataTable();
        
        public AdminMenu()
        {
            InitializeComponent();
           DataTable dt = new DataTable();
           MySqlCommand com = new MySqlCommand(queryString, con);
            try
            {
                con.Open();

                dt.Clear();
                using (MySqlDataReader dr = com.ExecuteReader())
                {

                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dataGridView1.DataSource = dt;
            pdt = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            queryString = @"SELECT * FROM "+textBox1.Text+";";
           DataTable dt = new DataTable();
            MySqlCommand com = new MySqlCommand(queryString, con);
            try
            {

                
                using (MySqlDataReader dr = com.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dataGridView1.DataSource = dt;
            pdt = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt1 = pdt;
            MySqlDataAdapter da = new MySqlDataAdapter(queryString,con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(da);
            try
            {
                da.UpdateCommand = builder.GetUpdateCommand();
                da.InsertCommand = builder.GetInsertCommand();
                da.DeleteCommand = builder.GetDeleteCommand();
                da.Update(dt1);
                MessageBox.Show("Изменения сохранены!", "success");
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void AdminMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Hide();
            main.Show();
        }
    }
}

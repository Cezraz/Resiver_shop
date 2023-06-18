using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.Odbc;
using MySql.Data.MySqlClient;


namespace kyrsa4
{
    public partial class SelectForm : Form
    {
        public string queryString = @"SELECT * FROM  interface";
        public static string connect = Main.str_connect.ToString();
        public MySqlConnection con = new MySqlConnection(connect);
      

        public SelectForm()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            DataTable dt = new DataTable();
            MySqlCommand com = new MySqlCommand(queryString, con);
            try
            {
                con.Open();
               
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
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
            queryString = @"SELECT * FROM  interface";
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

        }

        private void SelectForm_Load(object sender, EventArgs e)
        {
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
            queryString = @"SELECT * FROM  resiver";
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

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            
            queryString = @"SELECT * FROM  customer";
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0) { MessageBox.Show("Таблица не выбрана"); return; }
            string cm_ts = comboBox1.SelectedItem.ToString();
            int x;
            
                switch(cm_ts)
                {
                    case "Интерфейсы":
                        queryString = @"SELECT * FROM  interface;";
                        break;
                    case "Ресиверы":
                        queryString = @"SELECT * FROM  resiver;";
                        break;
                    case "Заказчики":
                        queryString = @"SELECT * FROM  customer;";
                        break;
                    
                }
            x = queryString.Length - 1;
            if (textBox2.Text != "")
            {
                queryString= queryString.Substring(0,x)+ " where " + textBox2.Text + ";"; 
            }
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuForm mf = new MenuForm();
            this.Hide();
            mf.Show();
        }

        private void SelectForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            Application.Exit();
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

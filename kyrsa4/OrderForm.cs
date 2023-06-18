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
    public partial class OrderForm : Form
    {
        public string queryString = @"SELECT * FROM  resiver";
        public static string connect = Main.str_connect.ToString();
        public MySqlConnection con = new MySqlConnection(connect);
        public OrderForm()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
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


            queryString = @"SELECT * FROM  customer";
            MySqlCommand com1 = new MySqlCommand(queryString, con);
            try
            {
                //con.Open();


                using (MySqlDataReader dr = com1.ExecuteReader())
                {

                    if (dr.HasRows)
                    {
                        dt1.Load(dr);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dataGridView2.DataSource = dt1;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "") { MessageBox.Show("Некорректные данные"); return; }
            if(comboBox1.SelectedIndex < 0) { MessageBox.Show("Не выбран тип работ"); return; }
            queryString = "insert into orders(idcustomer,idresiver,tip_rabot,orderdate,stoimost) " +
                   "values( " + Convert.ToInt32(textBox1.Text) + "," + Convert.ToInt32(textBox2.Text) + ", '"+comboBox1.SelectedItem+"', curdate(), calcul_stoim(" + Convert.ToInt32(textBox2.Text) +")); ";
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
                MessageBox.Show("Заказ оформлен!", "Успех");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuForm mf = new MenuForm();
            this.Hide();
            mf.Show();
        }

        private void OrderForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            Application.Exit();

        }
    }
}

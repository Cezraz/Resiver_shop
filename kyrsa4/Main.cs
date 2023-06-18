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
    public partial class Main : Form
    {
        public string log;
        public string pass;
        public static string connect;

        public static string str_connect { get { return connect; } set { connect = value; } }






        public Main()
        {
            
            InitializeComponent();
            
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            log = textBox3.Text;
            pass=textBox1.Text;
             connect = "server = localhost; user id =" + log + "; password =" + pass +
                "; database = resiver_shop; persistsecurityinfo=True; CharacterSet = utf8; AllowPublicKeyRetrieval=True";
            str_connect = connect;
            MySqlConnection myConnection = new MySqlConnection(connect);


                try
                {
                    myConnection.Open();
                    MessageBox.Show("Привествую, " + textBox3.Text + "!");
                if (log != "root")
                {
                    MenuForm mf = new MenuForm();
                    this.Hide();
                    mf.Show();
                }
                else
                {
                    AdminMenu am = new AdminMenu();
                    this.Hide();
                    am.Show();
                }
                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kyrsa4
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            SelectForm sel = new SelectForm();
            sel.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            OrderForm order = new OrderForm();
            order.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void MenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            Application.Exit();

        }

        private void MenuForm_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Hide();
            main.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectSQL
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
           
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.FromArgb(55, 187, 77);

        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.ForeColor = Color.White;

        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.FromArgb(55, 187, 77);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentLogin std = new StudentLogin();
            std.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Instractors_home ins = new Instractors_home();
            ins.Show();
        }
    }
}

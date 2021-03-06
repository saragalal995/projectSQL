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
    public partial class Instractors_home : Form
    {
       
        public Instractors_home()
        {
            InitializeComponent();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginManger lm = new LoginManger();
            lm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginInstrctour li = new LoginInstrctour();
            li.Show();
        }

        private void Instractors_home_Load(object sender, EventArgs e)
        {

        }
    }
}

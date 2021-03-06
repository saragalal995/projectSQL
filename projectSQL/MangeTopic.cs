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
    public partial class MangeTopic : Form
    {
        public MangeTopic()
        {
            InitializeComponent();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            InstructorOperation instructor = new InstructorOperation(1);
            instructor.Show();
        }
    }
}

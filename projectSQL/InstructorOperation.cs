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
    public partial class InstructorOperation : Form
    {
        public InstructorOperation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            MangeStudents ms = new MangeStudents();
            ms.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            MangeTopic mt = new MangeTopic();
            mt.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            MangeExam Me = new MangeExam();
            Me.Show();
        }
    }
}

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
    public partial class MangeExam : Form
    {
        public MangeExam()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            InstructorOperation ist = new InstructorOperation();
            ist.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            CreateExame ce = new CreateExame();
            ce.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            MangeCourseQuestions Mcq = new MangeCourseQuestions();
            Mcq.Show();

        }
    }
}

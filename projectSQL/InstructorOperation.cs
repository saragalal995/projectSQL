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
        private int inst;
        public InstructorOperation(int id)
        {
            InitializeComponent();
            this.inst = id;
            Online_Exame exam = new Online_Exame();
            var student = (from s in exam.Instractors
                           where s.Ins_id == id
                           select s).First();
            label2.Text = student.Ins_id.ToString();
            label4.Text = student.Ins_fname + ' ' + student.Ins_lname;
            label5.Text = student.Dept_id.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            MangeStudents ms = new MangeStudents(inst);
            ms.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            MangeTopic mt = new MangeTopic(inst);
            mt.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            MangeExam Me = new MangeExam(inst);
            Me.Show();
        }

        private void InstructorOperation_Load(object sender, EventArgs e)
        {

        }
    }
}

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
    
    public partial class StudentDashbord : Form
    {
        private int id;
        public StudentDashbord(int id)
        {
            InitializeComponent();
            this.id = id;
            Oline_Exam1 exam = new Oline_Exam1();
            var student = (from s in exam.Students
                           where s.St_id == id
                           select s).First();
            label2.Text = student.St_id.ToString();
            label3.Text = student.St_fname + ' ' + student.St_lname;
            label5.Text = student.Address;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Course_List crs = new Course_List();
            crs.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            ExamDashborde ed = new ExamDashborde(id);
            ed.Show();
        }
    }
}

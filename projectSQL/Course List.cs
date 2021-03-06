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
    public partial class Course_List : Form
    {
        private int stdId;
        public Course_List(int id)
        {
            this.stdId = id;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            StudentDashbord sd = new StudentDashbord(stdId);
            sd.Show();
        }

        private void Course_List_Load(object sender, EventArgs e)
        {
            Online_Exame exam = new Online_Exame();
            var grade = exam.Examresults(stdId);
            dataGridView1.DataSource = grade;
        }
    }
}

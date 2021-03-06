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
    public partial class ExamDashborde : Form
    {
        private int stdID;
        private int ExamID = -1;
        public ExamDashborde(int id)
        {
            InitializeComponent();
            stdID =id ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            StudentDashbord sd = new StudentDashbord(stdID);
            sd.Show();
        }

        private void LoadCourses()
        {
            Online_Exame exam = new Online_Exame();
            var std = (from s in exam.Students where s.St_id == stdID select s).First();
            var course = exam.getCoursesByDeptID(std.Dept_id).ToList();
           
            
                foreach (var item in course)
                {
                string cour = item.C_id +","+ item.C_name;
                    listBox1.Items.Add(cour);
                }
            
        }

        private void ExamDashborde_Load(object sender, EventArgs e)
        {
            LoadCourses();
            StartButtonStatus();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(listBox1.SelectedItem.ToString().Split(',')[0]);
            loadEaxam(id);
        }

        private void loadEaxam(int cId)
        {
            Online_Exame exam = new Online_Exame();
           
            var course = from c in exam.courses where c.C_id == cId select c;

            var exames = from ex in exam.Exams
                         where ex.C_id == cId
                         select ex;
            if (exames.Count() == 0)
            {
                flowLayoutPanel1.Controls.Clear();
                comboBox1.Items.Clear();
                comboBox1.Text = string.Empty;
                Label nolabel = new Label();
                nolabel.Text = "There Is No Exams Available";
                nolabel.Size = new Size(350, 30);
                nolabel.ForeColor = Color.FromArgb(55,187,77);
                nolabel.Font = new Font("Microsoft Sans Serif", 18);
                flowLayoutPanel1.Controls.Add(nolabel);
                ExamID = -1;
                StartButtonStatus();
            }
            else
            {
                flowLayoutPanel1.Controls.Clear();
                comboBox1.Items.Clear();
                foreach (var item in exames)
                {
                    string exams = $"{item.Ex_id}, {item.Ex_Des}";
                    comboBox1.Items.Add(exams);
                }
            }



        }

       private void StartButtonStatus()
        {
            if (ExamID == -1)
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(comboBox1.SelectedItem.ToString().Split(',')[0]);
            ExamID = id;
      
            StartButtonStatus();
   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Take_Exame te = new Take_Exame(ExamID, stdID);
            te.Show();

        }
    }
}

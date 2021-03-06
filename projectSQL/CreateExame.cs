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
    public partial class CreateExame : Form
    {
        private int inst;
        public CreateExame(int idInst)
        {
            InitializeComponent();
            this.inst = idInst;
        }

        private void button4_Click(object sender, EventArgs e)
        {
                this.Close();
            MangeExam meo = new MangeExam(inst);
            meo.Show();
        }

        private void CreateExame_Load(object sender, EventArgs e)
        {
            loadCourses();
        }
        private void loadCourses()
        {
            comboBox2.Items.Clear();
            Online_Exame exam = new Online_Exame();
            var cour = exam.getInsCourses(inst);
            foreach (var item in cour)
            {
                comboBox2.Items.Add($"{item.C_id} , {item.C_name}");
            }

        }

        private void loadExam(int id)
        {
            Online_Exame exam = new Online_Exame();
            var exames = from ex in exam.Exams
                         where ex.C_id == id
                         select ex;
            dataGridView1.DataSource = exames.ToList();
            comboBox1.Items.Clear();
            foreach (var item in exames)
            {
                string exams = $"{item.Ex_id}, {item.Ex_Des}";
                comboBox1.Items.Add(exams);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int crId = int.Parse(comboBox2.SelectedItem.ToString().Split(',')[0]);
                loadExam(crId);
            }
            catch (Exception)
            {

                MessageBox.Show("Invalid Inputs", "Warnung");
            }
        }

        private void examDetails(int exId)
        {
            Online_Exame exam = new Online_Exame();
            var data =( from e in exam.Exams where e.Ex_id == exId select e).First();
            textBox1.Text =  data.Ex_Des;
            numericUpDown1.Value = int.Parse(data.T_F.ToString());
            numericUpDown2.Value = int.Parse(data.MC.ToString());
            numericUpDown3.Value = int.Parse(data.Duration.ToString());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int exid = int.Parse(comboBox1.SelectedItem.ToString().Split(',')[0]);
                examDetails(exid);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Inputs Exam ID", "Warnung");
            }
        }

        private bool isDataValid()
        {
            if (
                textBox1.Text == "" ||
                numericUpDown1.Value == 0 ||
                numericUpDown2.Value == 0 ||
                numericUpDown3.Value == 0
                )
            {
                return false;
            }
            return true;
        }

        private bool isNumberOfMcqOutOfRange(int crsId, int numOfMcQ)
        {
            Online_Exame online = new Online_Exame();
            var mcqList = from mcq in online.Questions
                          where mcq.Crs_id == crsId && mcq.Type == "mcq"
                          select mcq;

          
            if (numOfMcQ > mcqList.Count())
            {
    
                return true;
            }

            return false;
        }


        private bool isNumberOfTFRange(int crsId, int numOfTF)
        {
            Online_Exame online = new Online_Exame();
            var mcqList = from mcq in online.Questions
                          where mcq.Crs_id == crsId && mcq.Type == "T/F"
                          select mcq;


            if (numOfTF > mcqList.Count())
            {
                return true;
            }

            return false;
        }

        //add
        private void button1_Click(object sender, EventArgs e)
        {
            int crsId = 0;
            Online_Exame ent = new Online_Exame();
          
              


            if (comboBox2.SelectedItem != null)
            {
                crsId = int.Parse(comboBox2.SelectedItem.ToString().Split(',')[0]);
            }
            else
            {
                MessageBox.Show("invalid Course ID entred", "Waring");
                return;

            }

        
            

            if (!isDataValid())
            {
                MessageBox.Show("There is Data Is Missing", "Waring");
                return;
            }

            string desc = textBox1.Text;
            int nMcq = (int)numericUpDown2.Value;

            if (isNumberOfMcqOutOfRange(crsId, nMcq))
            {
                MessageBox.Show($"The Number {nMcq} is out of Range\nplease go and insert more Question to Continue", "Waring from MCQ");
                return;
            }

            int nTF = (int)numericUpDown1.Value;
            if (isNumberOfTFRange(crsId, nTF))
            {
                MessageBox.Show("The Number Is out of Range\nplease go and insert more Question to Continue", "Waring From TF");
                return;
            }


            int duration = (int)numericUpDown3.Value;
            Exam exam = new Exam()
            {
                C_id = crsId,
                Ex_Des = desc,
                MC = nMcq,
                T_F = nTF,
                Duration = duration,

            };
            comboBox1.Text = comboBox2.Text =textBox1.Text=string.Empty;
            numericUpDown1.Value = numericUpDown2.Value = numericUpDown3.Value = 0;

            ent.Exams.Add(exam);
            ent.SaveChanges();
            loadCourses();
            loadExam(crsId);

        }
        //update
        private void button2_Click(object sender, EventArgs e)
        {
            int examId = 0;
            int crsId = 0;
            Online_Exame ent = new Online_Exame();

            if (comboBox2.SelectedItem != null)
            {
                crsId = int.Parse(comboBox2.SelectedItem.ToString().Split(',')[0]);
            }
            else
            {
                MessageBox.Show("invalid Course ID entred", "Waring");
                return;
            }

            if (comboBox1.SelectedItem != null)
            {
                examId = int.Parse(comboBox1.SelectedItem.ToString().Split(',')[0]);
            }
            else
            {
                MessageBox.Show("There Is No Exam Selected");
                return;
            }

            if (!isDataValid())
            {
                MessageBox.Show("There is Data Is Missing", "Waring");
                return;
            }

            string desc = textBox1.Text;
            int nMcq = (int)numericUpDown2.Value;

            if (isNumberOfMcqOutOfRange(crsId, nMcq))
            {
                MessageBox.Show("The Number Is out of Range\nplease go and insert more Question to Continue", "Waring");
                return;
            }

            int nTF = (int)numericUpDown1.Value;
            if (isNumberOfTFRange(crsId, nTF))
            {
                MessageBox.Show("The Number Is out of Range\nplease go and insert more Question to Continue", "Waring");
                return;
            }


            int duration = (int)numericUpDown3.Value;
            if (duration < 10)
            {
                MessageBox.Show("Mim Duration is 10 Min");
                return;
            }

            var exam = (from ex in ent.Exams
                        where ex.Ex_id == examId
                        select ex).First();

            exam.Ex_Des = desc;
            exam.Duration = duration;
            exam.MC = nMcq;
            exam.T_F = nTF;
            comboBox1.Text = comboBox2.Text = textBox1.Text = string.Empty;
            numericUpDown1.Value = numericUpDown2.Value = numericUpDown3.Value = 0;

            ent.SaveChanges();
            loadCourses();
            loadExam(crsId);
        }
        //delete
        private void button3_Click(object sender, EventArgs e)
        {
            int examId = 0;
            if (comboBox1.SelectedItem != null)
            {
                examId = int.Parse(comboBox1.SelectedItem.ToString().Split(',')[0]);
            }
            else
            {
                MessageBox.Show("There Is No Exam Selected");
                return;
            }

            Online_Exame ent = new Online_Exame();
            var exam = (from ex in ent.Exams
                        where ex.Ex_id == examId
                        select ex).First();
            comboBox1.Text = comboBox2.Text = textBox1.Text = string.Empty;
            numericUpDown1.Value = numericUpDown2.Value = numericUpDown3.Value = 0;

            int crsId = (int)exam.C_id;
            ent.Exams.Remove(exam);
            ent.SaveChanges();
            loadCourses();
            loadExam(crsId);
        }
    }
    }


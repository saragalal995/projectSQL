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
         
    public partial class MangeCourseQuestions : Form
    {
        private int inst;
        public MangeCourseQuestions(int idInstrac)
        {
            InitializeComponent();
            this.inst = idInstrac;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            MangeExam me = new MangeExam(inst);
            me.Show();
        }

        private void MangeCourseQuestions_Load(object sender, EventArgs e)
        {
            LoadCourses();
        }

        private void LoadCourses()
        {
            Online_Exame exam = new Online_Exame();
            var std = (from s in exam.Instractors where s.Ins_id == inst select s).First();
            var course = exam.getCoursesByDeptID(std.Dept_id).ToList();


            foreach (var item in course)
            {
                string cour = item.C_id + "," + item.C_name;
                comboBox1.Items.Add(cour);
            }

        }

        private void loadQuestions(int crsID)
        {
            Online_Exame ent = new Online_Exame();

            var quesList = from q in ent.Questions
                           where q.Crs_id== crsID
                           select q;

            dataGridView1.DataSource = quesList.ToList();
            comboBox2.Items.Clear();
            foreach (var item in quesList)
            {
                string ques = $"{item.Quest_id}, {item.Type}";
                comboBox2.Items.Add(ques);
            }
        }

        private void questionInfo(int qid)
        {
            Online_Exame ent = new Online_Exame();
            // select Question by ID
            var quest = (from q in ent.Questions
                         where q.Quest_id == qid
                         select q).First();

            // Show Quest Info
            textBox1.Text = quest.Qustion;
            
            numericUpDown1.Value = (int)quest.Grade;
            textBox2.Text =quest.CorectAnswer.ToString();


            // Check Type Of Quest To select Display Method
            if (quest.Type.ToLower() == "t/f")
            {
                // case 1: TF
                // don't need ans group
                Answers.Hide();
                
            }
            else
            {
                // case 2: MCQ
                // show Ans GroupBox
                
                TextBox[] ch = { ans1, ans2, ans3, ans4 };
                Answers.Show();
                // Mapping

                var choicess = ( from c in ent.Choices
                              where c.Quset_id == qid
                              select c).ToList();

                for (int i = 0; i < choicess.Count; i++)
                {
                    ch[i].Text = choicess[i].choices;
                }

              
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int crsId = int.Parse(comboBox1.SelectedItem.ToString().Split(',')[0]);
                loadQuestions(crsId);
            }
            catch (Exception)
            {
                MessageBox.Show("invalid Course ID entred", "Waring");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                int quesID = int.Parse(comboBox2.SelectedItem.ToString().Split(',')[0]);
                questionInfo(quesID);
            }
            else
            {
                MessageBox.Show("Please Select Questions", "Waring");
            }
        }



        private bool isQuestDataValid()
        {
            if (comboBox3.SelectedItem == null ||
                textBox1.Text == string.Empty ||
                textBox2.Text == string.Empty)
            {
                return false;
            }
            return true;
        }

        private bool isChoiceDataValid()
        {
            if (
                ans1.Text == string.Empty ||
                ans2.Text == string.Empty ||
                ans3.Text == string.Empty ||
                ans4.Text == string.Empty
                )
            {
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int crsID = 0;
            if (comboBox1.SelectedItem != null)
            {
                crsID = int.Parse(comboBox1.SelectedItem.ToString().Split(',')[0]);
            }
            else
            {
                MessageBox.Show("Please select course id", "Waring");
                return;
            }

            if (!isQuestDataValid())
            {
                MessageBox.Show("Please Enter Valid Data", "Waring");
                return;
            }

            string qbody = textBox1.Text;
            string qtype = comboBox3.SelectedItem.ToString();
            int qdeg = (int)numericUpDown1.Value;
            int modelAns = int.Parse(textBox2.Text);
            Online_Exame ent = new Online_Exame();
            var last = ent.Newquestion(modelAns, qtype, qdeg, qbody,crsID).First();
          
            ////////////////////////////////////////////////////////////////////
            if (last.Type.ToLower() == "mcq")
            {
                insertChoicesForQuestById(last.Quest_id);
            }



            LoadCourses();
            loadQuestions(crsID);
            textBox1.Text = textBox2.Text = string.Empty;
            numericUpDown1.Value = 1;
            comboBox3.Text = string.Empty;

        }
        private void insertChoicesForQuestById(int questID)
        {
            Online_Exame ent = new Online_Exame();
            
            if (!isChoiceDataValid())
            {
                MessageBox.Show("Please Enter All Data Rrequired", "Waring");
                return;
            }

            TextBox[] ch = { ans1, ans2, ans3, ans4 };
            for (int i = 0; i < ch.Length; i++)
            {
                Choice chois = new Choice()
                {
                    ch_id = i,
                    Quset_id = questID,
                    choices = ch[i].Text
                };
                ent.Choices.Add(chois);
            }

            ent.SaveChanges();
            ans1.Text = ans2.Text = ans3.Text = ans4.Text = string.Empty;
        }

        private void viewAnswerGroup()
        {
            if (comboBox3.SelectedItem == null || comboBox3.SelectedIndex == 1)
            {
                Answers.Hide();
            }
            else if (comboBox3.SelectedIndex == 0)
            {
                Answers.Show();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewAnswerGroup();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int questID = 0;
            int crsID = 0;
            if (comboBox2.SelectedItem != null)
            {
                questID = int.Parse(comboBox2.SelectedItem.ToString().Split(',')[0]);
            }
            else
            {
                MessageBox.Show("Please select quest id", "Waring");
            }

            if (comboBox1.SelectedItem != null)
            {
                crsID = int.Parse(comboBox1.SelectedItem.ToString().Split(',')[0]);
            }
            else
            {
                MessageBox.Show("Please select course id", "Waring");
                return;
            }

            if (!isQuestDataValid())
            {
                MessageBox.Show("Please Enter Valid Data", "Waring");
                return;
            }

            string qbody = textBox1.Text.Trim();
            int qdeg = (int)numericUpDown1.Value;
            int modelAns =int.Parse(textBox2.Text.Trim());

            Online_Exame ent = new Online_Exame();

            var quest = (from q in ent.Questions
                         where q.Quest_id == questID
                         select q).First();

            quest.Qustion = qbody;
            quest.CorectAnswer = modelAns;
            quest.Grade = qdeg;

            if (quest.Type.ToLower().Trim() == "mcq")
            {
                updateChoice(questID);
            }

            ent.SaveChanges();
            LoadCourses();
            loadQuestions(crsID);
            questionInfo(questID);
            textBox1.Text = textBox2.Text = string.Empty;
            ans1.Text = ans2.Text = ans3.Text = ans4.Text = string.Empty;
            numericUpDown1.Value = 1;
            comboBox3.Text = string.Empty;
        }


        private void updateChoice(int QuestID)
        {
            Online_Exame ent = new Online_Exame();
            if (!isChoiceDataValid())
            {
                MessageBox.Show("Please Enter All Data Rrequired", "Waring");
                return;
            }

            var chList = (from c in ent.Choices where c.Quset_id == QuestID select c).ToList();

            chList[0].choices = ans1.Text;
            chList[1].choices = ans2.Text;
            chList[2].choices = ans3.Text;
            chList[3].choices = ans4.Text;

         
            ent.SaveChanges();
            ans1.Text = ans2.Text = ans3.Text = ans4.Text = string.Empty;
        }



        private void deleteQuestion()
        {
            int questId = 0;
            if (comboBox2.SelectedItem != null)
            {
                questId = int.Parse(comboBox2.Text.ToString().Split(',')[0]);
            }
            else
            {
                MessageBox.Show("Please select course id", "Waring");
                return;
            }

            Online_Exame ent = new Online_Exame();
            var ques = (from q in ent.Questions
                        where q.Quest_id == questId
                        select q).First();
            int courseId = (int)ques.Crs_id;
            try
            {
                ent.deleteQuestion(questId);
            }
            catch (Exception)
            {


            }


            LoadCourses();
            loadQuestions(courseId);
            textBox1.Text = textBox2.Text = string.Empty;
            numericUpDown1.Value = 1;
            comboBox3.Text = string.Empty;
            ans1.Text = ans2.Text = ans3.Text = ans4.Text = string.Empty;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            deleteQuestion();
        }
    }
}

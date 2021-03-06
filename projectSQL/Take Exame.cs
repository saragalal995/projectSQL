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
    public partial class Take_Exame : Form

    {
        private int examID;
        private int studentID;
        int currentIndex = 0;
        List<getExam_Question_Result> list;
        public Dictionary<int, string> StudentAnswers;

        public Take_Exame(int examId, int stdid)
        {
            InitializeComponent();
            this.examID = examId;
            this.studentID = stdid;
        }




        private void LoadExam()
        {
            Online_Exame exams = new Online_Exame();
            var examQuest = exams.getExam_Question(examID);
            list = examQuest.ToList();
            StudentAnswers = new Dictionary<int, string>(list.Count);

            // show Exam details
            var examInfo = (from e in exams.Exams where e.Ex_id == examID select e).First();
            label1.Text = examInfo.Ex_Des.Trim();
            label2.Text = examInfo.Duration.ToString()+"M";


       
        }

        private void loadQuestion(int index)
        {
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            Online_Exame ex = new Online_Exame();
            currentIndex = index;
            ButtonsStates();
            flowLayoutPanel2.Controls.Clear();
            getExam_Question_Result q = list[index];
            labelQuestion.Text = $"{index + 1}) {q.Qustion}";
            NumQuest.Text = $"{index + 1} / {list.Count}";
          
            var choices = from c in ex.Choices

                          where c.Quset_id == q.Quest_Id
                          select c;

            foreach (var item in choices)
            {
                RadioButton radio = new RadioButton();
                radio.CheckedChanged += new System.EventHandler(getAnswer);
                string answer = item.ch_id + ") " + item.choices;
                radio.Text = answer;
                radio.Size = new Size(200, 20);
           
                flowLayoutPanel2.Controls.Add(radio);
            }
        }

        private void getAnswer(object sender, EventArgs e)
        {
            // 1 - get Values Of Answer
            string answ = (sender as RadioButton).Text.Split(')')[0];
            // 2 - Get Question ID
            int QuestID = list[currentIndex].Quest_Id;
       
            if (existInAnswerSheet(QuestID))
            {
                StudentAnswers[QuestID] = answ;
                return;
            }

            StudentAnswers.Add(QuestID, answ);
            generateFinishBtn();
           
        }


        private bool existInAnswerSheet(int id)
        {
            foreach (var item in StudentAnswers)
            {
                if (item.Key == id)
                    return true;
            }

            return false;
        }


        private void ButtonsStates()
        {

            if (currentIndex == 0)
            {
                prev.Enabled = false;
            }
            else if (currentIndex == list.Count - 1)
            {
                next.Enabled = false;
            }
            else
            {
                prev.Enabled = true;
                next.Enabled = true;
            }
        }

        private void next_Click(object sender, EventArgs e)
        {
            currentIndex++;
            loadQuestion(currentIndex);
        }

        private void prev_Click(object sender, EventArgs e)
        {
            currentIndex--;
            loadQuestion(currentIndex);
        }

        private void Take_Exame_Load(object sender, EventArgs e)
        {
            LoadExam();
            loadQuestion(currentIndex);
            generateFinishBtn();
        }

        private void generateFinishBtn()
        {
            if (StudentAnswers.Count== list.Count)
            {
                //Button finish = new Button();
                //finish.Size = new Size(177, 38);
                //finish.BackColor = Color.FromArgb(55, 187, 77);
                //finish.Font = new Font("Tahoma", 12);
                //finish.ForeColor = Color.FromArgb(255, 255, 255);
                //finish.Text = "Finish";
                //finish.FlatStyle = FlatStyle.Flat;
                //flowLayoutPanel1.Controls.Add(finish);

                
                finish.Enabled = true;
                finish.Show();

            }
            else
            {

                finish.Enabled = false;
                finish.Hide();
            }
        }

        private void finish_Click(object sender, EventArgs e)
        {

            Online_Exame ex = new Online_Exame();
            IfStdExameExestBefor();
            foreach (var item in StudentAnswers)
            {
                //var msg = ex.NewStudentExam(studentID, item.Key, examID, 0, item.Value);
                Student_Exam se = new Student_Exam()
                {
                    St_id = studentID,
                    Ex_id = examID,
                    Q_id = item.Key,
                    Result = 0,
                    St_answer = item.Value
                };
                ex.Student_Exam.Add(se);
    
                ex.SaveChanges();
            }
            ex.ExamCorrection(studentID, examID);
           var res= ex.ExamRes(studentID, examID).First();
            MessageBox.Show($"your grade {res}");
            StudentDashbord std = new StudentDashbord(studentID);
            this.Close();
            std.Show();
        }

        private void IfStdExameExestBefor()
        {
            try
            {
                Online_Exame ex = new Online_Exame();
              var msg= ex.deleteStudentExam(studentID, examID);
                ex.SaveChanges();

            }
            catch (Exception)
            {

                return;
            }
        }
    }
}

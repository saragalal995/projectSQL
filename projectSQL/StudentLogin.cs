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
    public partial class StudentLogin : Form
    {
        public StudentLogin()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isValidated())
            {
                MessageBox.Show("Please Enter The Data");
                return;
            }

            int id = int.Parse(textBox1.Text);
            string email = textBox2.Text;
            Login(id, email);
            


          
        }
        //check textbox  if user not enter data
        private bool isValidated() {

            if (textBox1.Text==string.Empty || textBox2.Text==string.Empty)
            {
                return false;
            }
            return true;
        
        }

        private void Login(int id,string email)
        {
            try
            {
                Online_Exame exam = new Online_Exame();
                var std = (from s in exam.Students where s.St_id == id & s.Email==email select s).First();
                StudentDashbord Student = new StudentDashbord(std.St_id);
                Student.Show();
                this.Close();

            }
            catch (Exception)
            {

                MessageBox.Show("Not Autho");
            }
        }

    }
}

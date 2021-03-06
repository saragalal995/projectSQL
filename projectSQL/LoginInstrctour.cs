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
    public partial class LoginInstrctour : Form
    {
        public LoginInstrctour()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isValidated())
            {
                MessageBox.Show("Please Enter The Data");
                return;
            }

            int id = int.Parse(textBox1.Text);
            string name = textBox2.Text;
            Login(id, name);


        }

        //check textbox  if user not enter data
        private bool isValidated()
        {

            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
            {
                return false;
            }
            return true;

        }

        private void Login(int id, string name)
        {
            try
            {
                Online_Exame exam = new Online_Exame();
                var instrac = (from s in exam.Instractors where s.Ins_id == id & s.Ins_fname==name select s).First();
            
           InstructorOperation instractor = new InstructorOperation(instrac.Ins_id);
                instractor.Show();
                this.Close();

            }
            catch (Exception)
            {

                MessageBox.Show("Not Autho");
            }
        }

    }
}

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
    public partial class LoginManger : Form
    {
        public LoginManger()
        {
            InitializeComponent();
        }
        public static int inputId = 0;

        //--login buuton 
        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter your Id");
            }
            else
            {
                try
                {
                    inputId = int.Parse(textBox1.Text);
                    Online_Exame ent = new Online_Exame();
                    var item = (from d in ent.Departments
                                where d.Dept_manger == inputId
                                select d).First();

                    this.Close();
                    MangerHome mn = new MangerHome();
                    mn.Show();

                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Incorrect ID");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Incorrect ID");
                }

            }

        }
    }
}

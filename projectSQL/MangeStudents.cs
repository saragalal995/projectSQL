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
    public partial class MangeStudents : Form
    {
        public MangeStudents()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            InstructorOperation io = new InstructorOperation(1);
            io.Show();
        }

        private void MangeStudents_Load(object sender, EventArgs e)
        {

        }
    }
}

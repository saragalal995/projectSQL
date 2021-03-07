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
    public partial class MangeInstructor : Form
    {
        public MangeInstructor()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            MangerHome mh = new MangerHome();
            mh.Show();
        }

        //function to load Dept ids
        private void LoadDepartments()
        {
            comboBox1.Items.Clear();
            Online_Exame ent = new Online_Exame();
            var deptids = from v in ent.Departments
                          select v.Dept_id;
            foreach (var i in deptids)
            {
                comboBox1.Items.Add(i);

            }
        }

        //function to load instructor ids
        private void Loadinstructor()
        {
            dataGridView1.DataSource = null;
            comboBox2.Items.Clear();
            Online_Exame ent = new Online_Exame();
            var insids = from v in ent.Instractors
                         select v.Ins_id;
            foreach (var i in insids)
            {
                comboBox2.Items.Add(i);
            }

        }

        //on form load fill combo boxes
        private void MangeInstructor_Load(object sender, EventArgs e)
        {
            LoadDepartments();
            Loadinstructor();
        }




        //insert new instructor
        private void Add_button(object sender, EventArgs e)
        {
            try
            {
                using (Online_Exame ent = new Online_Exame())
                {
                    //ent.NewInstructor(textBox1.Text, textBox2.Text, int.Parse(comboBox1.Text));
                    Instractor ins = new Instractor();
                    ins.Dept_id = int.Parse(comboBox1.Text);
                    ins.Ins_fname = textBox1.Text;
                    ins.Ins_lname = textBox2.Text;
                    ent.Instractors.Add(ins);
                    ent.SaveChanges();
                    Loadinstructor();
                    MessageBox.Show("Added Successfully");
                    comboBox1.Text = comboBox2.Text = textBox1.Text = textBox2.Text = string.Empty;
                  

                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please complete the information");
            }

        }

        //update instructor
        private void update_button(object sender, EventArgs e)
        {
            try
            {
                string fname = textBox1.Text;
                string lname = textBox2.Text;
                int insId = (int)comboBox2.SelectedItem;
                int deptId = (int)comboBox1.SelectedItem;
                using (Online_Exame ent = new Online_Exame())
                {
                    ent.UpdateInstractor(insId, fname, lname, deptId);
                    comboBox1.Text = comboBox2.Text = textBox1.Text = textBox2.Text = string.Empty;
                    ent.SaveChanges();
                    Loadinstructor();
                }
                MessageBox.Show("Updated Successfully");
               
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Not complete Form");
            }
        }

        //delete instructor
        private void delete_button(object sender, EventArgs e)
        {
            try
            {
                int insId = (int)comboBox2.SelectedItem;
                using (Online_Exame ent = new Online_Exame())
                {
                    
                    // ent.deleteInstructor(insId);
                    int ins_id = int.Parse(comboBox2.Text);
                    Instractor ins = ent.Instractors.Find(ins_id);
                    ent.Instractors.Remove(ins);
                    ent.SaveChanges();
                   
                    Loadinstructor();
                    comboBox1.Text = comboBox2.Text = textBox1.Text = textBox2.Text = string.Empty;
                }
                MessageBox.Show("Deleted Successfully");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Incorrect ID");
            }
        }

        // display department instructors in grid view
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int deptId = (int)comboBox1.SelectedItem;
            using (Online_Exame ent = new Online_Exame())
            {
                dataGridView1.DataSource = ent.Get_instructor_by_deptId(deptId);
            }

        }

        // display choosen instructor information
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int insId = (int)comboBox2.SelectedItem;
            Online_Exame ent = new Online_Exame();
            var item = (from d in ent.Instractors
                        where d.Ins_id == insId
                        select d).First();
            textBox1.Text = item.Ins_fname;
            textBox2.Text = item.Ins_lname;
            comboBox1.SelectedItem = item.Dept_id;
        }
    }
}

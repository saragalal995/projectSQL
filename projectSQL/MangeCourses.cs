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
    public partial class MangeCourses : Form
    {
        public MangeCourses()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            MangerHome mn = new MangerHome();
            mn.Show();
        }

        
        private void LoadCourses()
        {
            comboBox1.Items.Clear();
            Online_Exame ent = new Online_Exame();
            var crsid = from v in ent.courses
                        select v.C_id;
            foreach (var i in crsid)
            {
                comboBox1.Items.Add(i);
                
            }

        }

        //----- on form load fill combo boxes  
        private void MangeCourses_Load(object sender, EventArgs e)
        {
            Online_Exame ent = new Online_Exame();
            LoadCourses();
            var insnames = from v in ent.Instractors
                           select v.Ins_fname + " " + v.Ins_lname;
            foreach (var i in insnames)
            {
                comboBox3.Items.Add(i);
            }

        }

        //--- display choosen course information
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Instructors.Items.Clear();
                int crsId = (int)comboBox1.SelectedItem;
                Online_Exame ent = new Online_Exame();
                var item = (from d in ent.courses
                            where d.C_id == crsId
                            select d).First();
                textBox1.Text = item.C_name;

                //get instructors of the course
                var items = ent.get_course_instructors(crsId);
                foreach (var i in items)
                {
                    Instructors.Items.Add(i);
                }

            }
            catch (InvalidOperationException)
            {
                Instructors.Items.Add("No instructor");
            }
        }


        //--- add course
        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = string.Empty;
            try
            {
                using (Online_Exame ent = new Online_Exame())
                {
                    ent.InsertNewCourse(textBox1.Text);
                    ent.SaveChanges();

                    MessageBox.Show("Added Successfully");
                    Courses.Items.Clear();
                    Instructors.Items.Clear();
                    label7.Text = comboBox1.Text = comboBox3.Text = textBox1.Text = string.Empty;

                    
                    LoadCourses();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please complete the information");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Invalid Information");
            }
        }

        //--- update course
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string crsname = textBox1.Text;

                int crsid = (int)comboBox1.SelectedItem;
                using (Online_Exame ent = new Online_Exame())
                {
                    ent.UpdateCourse(crsid, crsname);
                    ent.SaveChanges();

                }
                MessageBox.Show("Updated Successfully");
                Courses.Items.Clear();
                Instructors.Items.Clear();
                label7.Text = comboBox1.Text = comboBox3.Text = textBox1.Text = string.Empty;

                LoadCourses();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Not complete Form");
            }
        }


        //---- delete courses
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int crsid = (int)comboBox1.SelectedItem;
                using (Online_Exame ent = new Online_Exame())
                {
                    ent.deleteCourse(crsid);
                    ent.SaveChanges();

                }
                MessageBox.Show("Deleted Successfully");
                Courses.Items.Clear();
                Instructors.Items.Clear();
                label7.Text = comboBox1.Text = comboBox3.Text = textBox1.Text = string.Empty;

                LoadCourses();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Incorrect ID");
            }
        }



        //--- Display instructor coursrs in list
        private void listBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            Courses.Items.Clear();
            Online_Exame ent = new Online_Exame();

            //--get instractor from combo box
            string insName = Instructors.SelectedItem.ToString();
            var insId = (int)(ent.Get_insid_by_inname(insName)).First();

            label7.Text = "of" + " " + insName;
            //--get courses of same instructor
            var items = ent.get_instructor_course(insId);
            foreach (var i in items)
            {
                Courses.Items.Add(i);
            }
        }

        //add instructor to choosen course 
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int crsId = int.Parse(comboBox1.Text);
                using (Online_Exame ent = new Online_Exame())
                {
                    //--get instractor from combo box
                    string insName = comboBox3.Text.ToString();
                    var insId = (int)(ent.Get_insid_by_inname(insName)).First();

                    //--add instractor to course 
                    ent.Add_Course_instractor(crsId, insId);

                    MessageBox.Show("Added Successfully");
                    Courses.Items.Clear();
                    Instructors.Items.Clear();
                    label7.Text = comboBox1.Text = comboBox3.Text = textBox1.Text = string.Empty;


                    ent.SaveChanges();
                }
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException)
            {
                MessageBox.Show("This instructor already exist");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Please Choose Instructor Name");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please Choose Instructor Name");
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Please Choose Course ID");
            }
        }


        //delete instructor to choosen course 
        private void button6_Click(object sender, EventArgs e)
        {

            try
            {
                int crsId = int.Parse(comboBox1.Text);
                using (Online_Exame ent = new Online_Exame())
                {

                    //--get instractor from combo box
                    string insName = Instructors.SelectedItem.ToString();
                    var insId = (int)(ent.Get_insid_by_inname(insName)).First();

                    ent.Delete_course_instractor(crsId, insId);
                    MessageBox.Show("Deleted Successfully");
                    Courses.Items.Clear();
                    Instructors.Items.Clear();
                    label7.Text = comboBox1.Text = comboBox3.Text = textBox1.Text = string.Empty;

                    ent.SaveChanges();
                }
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException)
            {
                MessageBox.Show("This instructor already exist");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Please Choose Instructor Name");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please Choose Instructor Name");
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Please Choose Course ID");
            }
        }
    }
}

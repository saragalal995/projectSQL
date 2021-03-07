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
        int ins_id = 0;
        public MangeStudents(int id)
        {
            InitializeComponent();
            this.ins_id = id;
        }

        public void load()
        {
            using (Online_Exame ent = new Online_Exame())
            {
                var ins = ent.Instractors.Find(ins_id);
                int Ins_dept = (int)ins.Dept_id;

                comboBox1.Text = txtFname.Text = txtLname.Text = txtEmail.Text = txtage.Text = txtaddress.Text = string.Empty;
                comboBox1.Items.Clear();
                var students = from S in ent.Students
                               where S.Dept_id == Ins_dept
                               select S;
                foreach (var d in students)
                {
                    comboBox1.Items.Add(d.St_id);
                }
            }
        }
        public void loadDataGrid()
        {
            using (Online_Exame ent = new Online_Exame())
            {
                var ins = ent.Instractors.Find(ins_id);
                int Ins_dept = (int)ins.Dept_id;

                var students = from S in ent.Students
                               where S.Dept_id == Ins_dept
                               select new
                               {
                                   S.St_id,
                                   S.St_fname,
                                   S.St_lname,
                                   S.Address,
                                   S.Age,
                                   S.Email,
                                   S.Dept_id
                               };

                dataGridView1.DataSource = students.ToList();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            InstructorOperation io = new InstructorOperation(1);
            io.Show();
        }

        // Add
        private void Add_Click(object sender, EventArgs e)
        {
            if (txtFname.Text == "")
            {
                MessageBox.Show("Please enter your Fname");
            }
            else if (txtLname.Text == "")
            {
                MessageBox.Show("Please enter your Lname");
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Please enter your Email");
            }
            else if (txtage.Text == "")
            {
                MessageBox.Show("Please enter your Age");
            }
            else if (txtaddress.Text == "")
            {
                MessageBox.Show("Please enter your Address");
            }
            else
            {
                try
                {
                    using (Online_Exame ent = new Online_Exame())
                    {
                        var ins = ent.Instractors.Find(ins_id);
                        int Ins_dept = (int)ins.Dept_id;

                        /* ent.insertNewStudent(txtFname.Text, txtLname.Text,
                            txtaddress.Text, int.Parse(txtage.Text), txtEmail.Text, Ins_dept);*/
                        Student std = new Student();
                        std.St_fname = txtFname.Text;
                        std.St_lname = txtLname.Text;
                        std.Address = txtaddress.Text;
                        std.Age = int.Parse(txtage.Text);
                        std.Email = txtEmail.Text;
                        std.Dept_id = Ins_dept;
                        ent.Students.Add(std);
                        ent.SaveChanges();
                        MessageBox.Show("Added Successfully");
                        load();
                        loadDataGrid();
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid format");
                }

            }
        }

        // Load
        private void MangeStudents_Load(object sender, EventArgs e)
        {
            load();
            loadDataGrid();
        }

        // Search
        private void Search_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Enter your StudentId");
            }
            else
            {
                try
                {
                    using (Online_Exame ent = new Online_Exame())
                    {
                        Student student = ent.Students.Find(int.Parse(comboBox1.Text));
                        txtFname.Text = student.St_fname;
                        txtLname.Text = student.St_lname;
                        txtEmail.Text = student.Email;
                        txtage.Text = student.Age.ToString();
                        txtaddress.Text = student.Address;

                    }
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



        // Update
        private void Update_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Enter your StudentId");
            }
            else
            {
                try
                {
                    using (Online_Exame ent = new Online_Exame())
                    {
                        var ins = ent.Instractors.Find(ins_id);
                        int Ins_dept = (int)ins.Dept_id;

                        Student student = ent.Students.Find(int.Parse(comboBox1.Text));
                        ent.UpdateStudent(student.St_id, txtFname.Text, txtLname.Text,
                         txtaddress.Text, int.Parse(txtage.Text), txtEmail.Text, Ins_dept);
                        comboBox1.Text = txtFname.Text = txtLname.Text = txtEmail.Text = txtage.Text
                            = txtaddress.Text = string.Empty;
                        MessageBox.Show("Updated successfully");
                        loadDataGrid();
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Incorrect ID");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invaild Format");
                }

            }
        }




        // Delete
        private void Delete_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Enter your StudentId");
            }
            else
            {
                try
                {
                    using (Online_Exame ent = new Online_Exame())
                    {
                        Student student = ent.Students.Find(int.Parse(comboBox1.Text));
                        //ent.deleteStudent(student.St_id);
                        ent.Students.Remove(student);
                        ent.SaveChanges();
                        MessageBox.Show("Deleted Successfully");
                        load();
                        loadDataGrid();

                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Incorrect ID");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invaild Format");
                }

            }
        }
    }
}

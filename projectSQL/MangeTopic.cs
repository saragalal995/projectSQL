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
    public partial class MangeTopic : Form
    {
        int ins_id = 0;
        public MangeTopic(int id)
        {
            InitializeComponent();
            this.ins_id = id;
        }

        public void load()
        {

            using (Online_Exame ent = new Online_Exame())
            {
                comboBox2.Items.Clear();
                var crs = ent.getInstractor_Course(ins_id).ToList();
                foreach (var item in crs)
                {
                    comboBox2.Items.Add(item);
                }

            }
        }
        public void loadDataGrid(int c_id)
        {
            using (Online_Exame ent = new Online_Exame())
            {
                dataGridView1.DataSource = string.Empty;
               var topics = from T in ent.Topics
                             where T.C_id == c_id
                             select new
                             {
                                 T.T_id,
                                 T.Name,
                                 T.C_id,
                             };

                dataGridView1.DataSource = topics.ToList();

            }
        }

        // back
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            InstructorOperation instructor = new InstructorOperation(1);
            instructor.Show();
        }

        // Load
        private void MangeTopic_Load(object sender, EventArgs e)
        {
            load();
        }

        // Course ID
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (Online_Exame ent = new Online_Exame())
            {

                int C_id = int.Parse(comboBox2.SelectedItem.ToString());
                var Topic_id = from T in ent.Topics
                               where T.C_id == C_id
                               select T;
                comboBox1.Items.Clear();
                foreach (var item in Topic_id)
                {
                    comboBox1.Items.Add(item.T_id);
                }
                loadDataGrid(C_id);
                textBox1.Text = "";
            }
        }

        // Add
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "")
            {
                MessageBox.Show("Enter your Course_Id");
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Enter your Topic_Name");
            }
            else if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Choose course id");
            }
            else
                try
                {
                    int C_id = int.Parse(comboBox2.SelectedItem.ToString());
                    int index = comboBox2.SelectedIndex;
                    using (Online_Exame ent = new Online_Exame())
                    {
                        // var msg=ent.NewTopic(C_id, textBox1.Text);
                        Topic t = new Topic();
                        t.C_id = C_id;
                        t.Name = textBox1.Text;
                        ent.Topics.Add(t);
                        ent.SaveChanges();
                        MessageBox.Show("Added Sucessfuly");
                        textBox1.Text = comboBox1.Text =comboBox2.Text=  string.Empty;
                       // comboBox2.SelectedIndex = index;
                        load();
                        loadDataGrid(C_id);
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid format");
                }
        }

        //Delete
        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Enter your Topic_Id");
            }else if (comboBox2.SelectedItem==null)
            {
                MessageBox.Show("Choose course id");
            }
            else
            {
                try
                {
                    using (Online_Exame ent = new Online_Exame())
                    {
                        //var sttt = ent.deleteTopic(int.Parse(comboBox1.SelectedItem.ToString())).ToList();
                        int T_id= int.Parse(comboBox1.SelectedItem.ToString());
                        var topic = ent.Topics.Find(T_id);
                        int c_id = (int)topic.C_id;
                        ent.Topics.Remove(topic);
                        ent.SaveChanges();
                        textBox1.Text = comboBox1.Text =comboBox2.Text= string.Empty;
                        MessageBox.Show("Deleted Successfully");
                        //MessageBox.Show(sttt.ToString());
                        load();
                        loadDataGrid(c_id);
                    }
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Incorrect ID");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid format");
                }
            }
        }


        // Update
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Choose course id");
            }
            else
            {
                using (Online_Exame ent = new Online_Exame())
                {
                    int c_id = int.Parse(comboBox2.SelectedItem.ToString());
                    ent.UpdateTopic(int.Parse(comboBox1.Text), c_id, textBox1.Text);
                    textBox1.Text = comboBox1.Text =comboBox2.Text= string.Empty;
                    loadDataGrid(c_id);
                }
            }
        }

        // Search
        private void Search_Click(object sender, EventArgs e)
        {

            if (comboBox1.Text == "")
            {
                MessageBox.Show("Enter your TopicId");
            }
            else
            {
                try
                {
                    using (Online_Exame ent = new Online_Exame())
                    {
                        var Topic = ent.Topics.Find(int.Parse(comboBox1.Text));
                        textBox1.Text = Topic.Name;

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
    }
}

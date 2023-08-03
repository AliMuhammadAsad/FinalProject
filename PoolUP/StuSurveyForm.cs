using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PoolUP
{
    public partial class StuSurveyForm : Form
    {
        // TODO: Specify the connection string for connecting the front end to the backend.
        const string constr = "Data Source=DESKTOP-OBV26O5;Initial Catalog=poolup;Integrated Security=True;";
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cm = new SqlCommand();
        public StuSurveyForm()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void panel20_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel23_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            // Submit form
        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox4_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label29_Click_1(object sender, EventArgs e)
        {

        }

        private void panel31_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel25_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void panel37_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void panel25_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void StuSurveyForm_Load(object sender, EventArgs e)
        {
            string Sql = "select hour + ':' + minutes from ValidTimes";
            con.Open();
            SqlCommand cmd = new SqlCommand(Sql, con);
            SqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                comboBox1.Items.Add(DR[0]);
                comboBox2.Items.Add(DR[0]);
                comboBox3.Items.Add(DR[0]);
                comboBox4.Items.Add(DR[0]);
                comboBox5.Items.Add(DR[0]);
                comboBox6.Items.Add(DR[0]);
                comboBox7.Items.Add(DR[0]);
                comboBox8.Items.Add(DR[0]);
                comboBox10.Items.Add(DR[0]);
                comboBox9.Items.Add(DR[0]);

            }
            DR.Close();
            comboBox13.Items.AddRange(new object[] {2022,2023,2024,2025,2026});
            comboBox14.Items.AddRange(new object[] { "Male", "Female" });

            Sql = "Select vehicle from TransportModes";
            cmd = new SqlCommand(Sql, con);
            DR = cmd.ExecuteReader();
            while (DR.Read())
            {
                comboBox15.Items.Add(DR[0]);
            }
            con.Close();
        }

        private void panel24_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string f_name = maskedTextBox8.Text;
            string l_name = maskedTextBox9.Text;
            string id = maskedTextBox7.Text;
            int year = Convert.ToInt32(comboBox13.Text);
            string gender = comboBox14.Text;

            string[] arr = {f_name,l_name, id, gender, comboBox15.Text, comboBox1.Text, comboBox2.Text, comboBox3.Text,
            comboBox4.Text, comboBox5.Text, comboBox6.Text, comboBox7.Text, comboBox8.Text, comboBox13.Text };

            foreach (string varitem in arr) {
                if (varitem == "")
                {
                    label34.Text = "Please submit correct data!";
                    return;
                }
            }
            char g1 = gender[0];

            int[] time_ids = new int[10];
            string[] str_times = new string[10];
            str_times[0] = comboBox1.Text;
            str_times[1] = comboBox2.Text;
            str_times[2] = comboBox3.Text;
            str_times[3] = comboBox4.Text;
            str_times[4] = comboBox5.Text;
            str_times[5] = comboBox6.Text;
            str_times[6] = comboBox7.Text;
            str_times[7] = comboBox8.Text;
            str_times[8] = comboBox10.Text;
            str_times[9] = comboBox9.Text;

            string Sql = "select time_id from ValidTimes where hour + ':' + minutes = @strtime";
            con.Open();
            SqlCommand cmd;
            int counter = 0;
            while (counter < 10) 
            {
                cmd = new SqlCommand(Sql, con);
                cmd.Parameters.AddWithValue("@strtime", str_times[counter]);
                time_ids[counter] = (Int32)cmd.ExecuteScalar();
                counter++;
            }

            Sql = "insert into students(ID, first_name,last_name,gender,batch,area_ID) " +
                "values (@id,@f_name,@l_name,@gender,@batch,@area_id);";
            cmd = new SqlCommand(Sql, con);

            // Specify the value of the parameters
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@f_name", f_name);
            cmd.Parameters.AddWithValue("@l_name", l_name);
            cmd.Parameters.AddWithValue("@gender", g1);
            cmd.Parameters.Add("@batch", SqlDbType.Int);
            cmd.Parameters["@batch"].Value = year;
            cmd.Parameters.AddWithValue("@area_id", 74700);

            cmd.ExecuteNonQuery();
            cmd.Dispose();



            int TM_ID = 1;
            Sql = "Select tm_id from TransportModes where vehicle = @vehicle";
            cmd = new SqlCommand(Sql, con);
            cmd.Parameters.AddWithValue("@vehicle", comboBox15.Text);
            TM_ID = (Int32) cmd.ExecuteScalar();
            cmd.Dispose();

            Sql = "insert into [Student_Transport_Modes](student_ID, TM_ID, is_owner) values(@std_id, @tm_id, 0)";
            cmd = new SqlCommand(Sql, con);

            // Specify the value of the parameters
            cmd.Parameters.AddWithValue("@std_id", id);
            cmd.Parameters.AddWithValue("@tm_id", TM_ID);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            Sql = "insert into schedule(student_id, day, in_time, out_time) values (@std_id, @day, @intime, @outtime)";
            cmd = new SqlCommand(Sql, con);
            cmd.Parameters.AddWithValue("@std_id", id);
            cmd.Parameters.AddWithValue("@day", 'M');
            cmd.Parameters.AddWithValue("@intime", time_ids[0]);
            cmd.Parameters.AddWithValue("@outtime", time_ids[1]);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            cmd = new SqlCommand(Sql, con);
            cmd.Parameters.AddWithValue("@std_id", id);
            cmd.Parameters.AddWithValue("@day", 'T');
            cmd.Parameters.AddWithValue("@intime", time_ids[2]);
            cmd.Parameters.AddWithValue("@outtime", time_ids[3]);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            cmd = new SqlCommand(Sql, con);
            cmd.Parameters.AddWithValue("@std_id", id);
            cmd.Parameters.AddWithValue("@day", 'W');
            cmd.Parameters.AddWithValue("@intime", time_ids[4]);
            cmd.Parameters.AddWithValue("@outtime", time_ids[5]);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            cmd = new SqlCommand(Sql, con);
            cmd.Parameters.AddWithValue("@std_id", id);
            cmd.Parameters.AddWithValue("@day", 'R');
            cmd.Parameters.AddWithValue("@intime", time_ids[6]);
            cmd.Parameters.AddWithValue("@outtime", time_ids[7]);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            cmd = new SqlCommand(Sql, con);
            cmd.Parameters.AddWithValue("@std_id", id);
            cmd.Parameters.AddWithValue("@day", 'F');
            cmd.Parameters.AddWithValue("@intime", time_ids[8]);
            cmd.Parameters.AddWithValue("@outtime", time_ids[9]);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            con.Close();

            var result = MessageBox.Show("Form Filled successfully!");
            this.Close();

        }

        private void comboBox15_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (comboBox15.SelectedIndex != -1)
            {
                string Sql = "select capacity from TransportModes where vehicle = @vehicle";
                con.Open();
                SqlCommand cmd;
         
                cmd = new SqlCommand(Sql, con);
                cmd.Parameters.AddWithValue("@vehicle", comboBox15.Text);
                maskedTextBox3.Text = cmd.ExecuteScalar().ToString();
            }*/
        }
    }
}

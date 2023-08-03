using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PoolUP
{

    public partial class Form4 : Form
    {
        // TODO: Specify the connection string for connecting the front end to the backend.
        const string constr = "Data Source=DESKTOP-OBV26O5;Initial Catalog=poolup;Integrated Security=True;";
        SqlConnection con = new SqlConnection(constr);

        string username;
        string password;
        public Form4(string username_, string password_)
        {
            InitializeComponent();
            username = username_;
            password = password_;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // TODO: Specify the connection string for connecting the front end to the backend.
            
            string Sql = "Select * from students where id = @username";
            con.Open();
            SqlCommand cmd = new SqlCommand(Sql, con);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader DR = cmd.ExecuteReader();
            
            con.Close();
        }

        private void reload()
        {
            string Sql = "Select * from students where id = @username";
            con.Open();
            SqlCommand cmd = new SqlCommand(Sql, con);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader DR = cmd.ExecuteReader();
            DR.Read();
            maskedTextBox7.Text = DR["id"].ToString();
            maskedTextBox8.Text = DR["first_name"].ToString();
            maskedTextBox9.Text = DR["last_name"].ToString();
            comboBox14.Text = DR["gender"].ToString();
            comboBox13.Text = DR["batch"].ToString();
            cmd.Dispose();
            DR.Close();

            Sql = "select vehicle, capacity from Student_Transport_Modes as S, TransportModes as T" +
                " where S.TM_id = T.TM_ID AND S.student_id = @username";
            cmd = new SqlCommand(Sql, con);
            cmd.Parameters.AddWithValue("@username", username);
            DR = cmd.ExecuteReader();
            DR.Read();
            comboBox15.Text = DR["vehicle"].ToString();
            maskedTextBox3.Text = DR["capacity"].ToString();
            cmd.Dispose();
            DR.Close();

            Sql = "select * from get_schedules(@username);";

            cmd = new SqlCommand(Sql, con);
            cmd.Parameters.AddWithValue("@username", username);
            DR = cmd.ExecuteReader();
            while (DR.Read())
            {
                if (DR["day"].ToString().CompareTo("M") == 0)
                {
                    comboBox1.Text = DR["intime"].ToString();
                    comboBox2.Text = DR["outtime"].ToString();
                }
                else if (DR["day"].ToString().CompareTo("T") == 0)
                {
                    comboBox3.Text = DR["intime"].ToString();
                    comboBox4.Text = DR["outtime"].ToString();
                }
                else if (DR["day"].ToString().CompareTo("W") == 0)
                {
                    comboBox5.Text = DR["intime"].ToString();
                    comboBox6.Text = DR["outtime"].ToString();
                }
                else if (DR["day"].ToString().CompareTo("R") == 0)
                {
                    comboBox7.Text = DR["intime"].ToString();
                    comboBox8.Text = DR["outtime"].ToString();
                }
                else
                {
                    comboBox10.Text = DR["intime"].ToString();
                    comboBox9.Text = DR["outtime"].ToString();
                }
            }


            con.Close();
        }
        private void Form4_Load(object sender, EventArgs e)
        {

            reload();
        }

        private void update_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            string Sql;
            string f_name = maskedTextBox8.Text;
            string l_name = maskedTextBox9.Text;
            string id = maskedTextBox7.Text;
            int year = Convert.ToInt32(comboBox13.Text);
            string gender = comboBox14.Text;

            string[] arr = {f_name,l_name, id, gender, comboBox15.Text, comboBox1.Text, comboBox2.Text, comboBox3.Text,
            comboBox4.Text, comboBox5.Text, comboBox6.Text, comboBox7.Text, comboBox8.Text, comboBox13.Text };

            con.Open();
            foreach (string varitem in arr)
            {
                if (varitem == "")
                {
                    label1.Text = "Please submit correct data!";
                    return;
                }
                else
                {
                    Sql = "exec Del_student @std_id = @username";
                    cmd = new SqlCommand(Sql, con);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

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

            Sql = "select time_id from ValidTimes where hour + ':' + minutes = @strtime";
            int counter = 0;
            while (counter < 10)
            {
                cmd = new SqlCommand(Sql, con);
                cmd.Parameters.AddWithValue("@strtime", str_times[counter]);
                time_ids[counter] = (Int32) cmd.ExecuteScalar();
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
            TM_ID = (Int32)cmd.ExecuteScalar();
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

            reload();
            update.Enabled = false;
            panel31.Enabled = false;
            panel25.Enabled = false;
            panel13.Enabled = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            update.Enabled = true;
            panel31.Enabled = true;
            panel25.Enabled = true;
            panel13.Enabled = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PoolUP
{
    
    public partial class Form1 : Form
    {
        // TODO: Specify the connection string for connecting the front end to the backend.
        const string constr = "Data Source=DESKTOP-OBV26O5;Initial Catalog=poolup;Integrated Security=True;";
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cm = new SqlCommand();
        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(new object[] { "Name", "ID", "Batch" });

            // TODO: Complete the function Load student tables
            // SQL query to select EmployeeID, FirstName, LastName from Employees
            string sql = "select Id, first_name, last_name from students";
            cm = new SqlCommand(sql, con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            std_grid.DataSource = dt;
            std_grid.Columns[0].HeaderCell.Value = "ID";
            std_grid.Columns[1].HeaderCell.Value = "First Name";
            std_grid.Columns[2].HeaderCell.Value = "Last Name";
            std_grid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            std_grid.ScrollBars = ScrollBars.Vertical;
            DataGridViewElementStates states = DataGridViewElementStates.None;
            std_grid.Width = std_grid.Columns.GetColumnsWidth(states);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string sql = "";
            if (comboBox1.SelectedIndex == 0 || comboBox1.SelectedIndex == -1)
            {
                sql = "select ID, first_name, last_name from students where first_name + ' ' + last_name LIKE '%' + @search_query + '%'";
                cm = new SqlCommand(sql, con);
                cm.Parameters.AddWithValue("@search_query", textBox1.Text);
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                sql = "select ID, first_name, last_name from students where ID LIKE '%' + @search_query + '%'";
                cm = new SqlCommand(sql, con);
                cm.Parameters.AddWithValue("@search_query", textBox1.Text);
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                sql = "select ID, first_name, last_name from students where batch = @search_query";
                cm = new SqlCommand(sql, con);
                try
                { 
                    cm.Parameters.AddWithValue("@search_query", Convert.ToInt32(textBox1.Text));
                }
                catch
                {
                    cm.Parameters.AddWithValue("@search_query", 0);

                    Console.WriteLine("could not execute");
                }
            }

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            std_grid.DataSource = dt;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            
        }

    }
}

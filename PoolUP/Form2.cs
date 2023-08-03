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

namespace PoolUP
{
    public partial class Form2 : Form
    {
        DataGridViewElementStates states = DataGridViewElementStates.None;
        // TODO: Specify the connection string for connecting the front end to the backend.
        const string constr = "Data Source=DESKTOP-OBV26O5;Initial Catalog=poolup;Integrated Security=True;";
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cm = new SqlCommand();
        public Form2()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Load_assGrid()
        {
            string sql = "select * from [AssignedGroups]";
            cm = new SqlCommand(sql, con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            con.Close();
            assGrid.DataSource = dt2;
            assGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            assGrid.ScrollBars = ScrollBars.Vertical;
            assGrid.Width = assGrid.Columns.GetColumnsWidth(states);

        }

        private void Form2_Load(object sender, EventArgs e)
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
            std_grid.Width = std_grid.Columns.GetColumnsWidth(states);

            //Load_assGrid();

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "exec Automate";
            con.Open();
            cm = new SqlCommand(sql, con);

            // Specify the value of the parameters

            cm.ExecuteNonQuery();
            cm.Dispose();
            con.Close();
            Load_assGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (assGrid.SelectedRows.Count == 1)
            {
                int index = assGrid.CurrentCell.RowIndex;
                if (index > 0)
                {
                    DataGridViewRow row = assGrid.Rows[index];
                    int group_id = Convert.ToInt32(row.Cells[0].Value);
                    string day = row.Cells[1].Value.ToString();
                    int ass_id = Convert.ToInt32(row.Cells[2].Value);
                    // TODO: Complete the function DeleteOrder 
                    // SQL query to Delete the from the order and order details table
                    string sql = "Select Id, first_name + ' ' + last_name from students where id in " +
                        "(select std_id from result where id = @ass_ID AND day = @day AND group_id = @group_id)";
                    con.Open();
                    cm = new SqlCommand(sql, con);

                    // Specify the value of the parameters
                    cm.Parameters.AddWithValue("@ass_ID", ass_id);
                    cm.Parameters.AddWithValue("@day", day);
                    cm.Parameters.AddWithValue("@group_id", ass_id);

                    SqlDataAdapter da = new SqlDataAdapter(cm);
                    DataTable d = new DataTable();
                    da.Fill(d);
                    grpStuGrid.DataSource = d;
                    cm.Dispose();
                    con.Close();

                }
            }
            else
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void grpStuGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

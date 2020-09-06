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

namespace AppForTesting
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataSet dataSet = null;    
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nikit\source\repos\AppForTesting\AppForTesting\Database1.mdf;Integrated Security=True");
            sqlConnection.Open();
            LoadData();


        }
        private void LoadData()
        {
            try
            {
               sqlDataAdapter=new SqlDataAdapter("SELECT *, 'DELETE' AS [DELETE] FROM   Table", sqlConnection);
                sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlBuilder.GetDeleteCommand();
                sqlBuilder.GetUpdateCommand();
                sqlBuilder.GetInsertCommand();
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "Users");
                dataGridView1.DataSource = dataSet.Tables["Users"];
                for(int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[6, i] = linkCell;
                }
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }   
        }
    }
}

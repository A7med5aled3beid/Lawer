using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lawer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            SqlConnection cn = new SqlConnection("Data Source=.;Initial Catalog=Lawer;Integrated Security=True");

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT Name, Password FROM dbo.User_Accounts WHERE (Name = @Name) AND (Password = @Pass)";
                cmd.Connection = cn;

                SqlParameter[] par = new SqlParameter[2];
                par[0] = new SqlParameter("@Name", textBox1.Text );
                par[1] = new SqlParameter("@Pass", textBox2.Text);
                cmd.Parameters.AddRange(par);

                cn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    Form2 fr2 = new Form2();
                    fr2.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show(" خطأ فى اسم المستخم او كلمة المرور");
                cn.Close();
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 fr3=new Form3();
            fr3.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

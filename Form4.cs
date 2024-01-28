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
    public partial class Form4 : Form
    {
        string selected;
        string selecteddel;
        
        SqlConnection cn = new SqlConnection("Data Source=.;Initial Catalog=Lawer;Integrated Security=True");
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            fillinformation();
        }

        private void fillinformation()
        {

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from Client";
            cmd.Connection = cn;

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
                comboBox2.Items.Add(dr[0]);
            }
            cn.Close();

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)//اضافة موكل جديد
        {
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "select * from Client";
                DataSet ds = new DataSet();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                cmd.Connection = cn;
                ad.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][0].ToString().Trim() == textBox1.Text)
                    {
                        MessageBox.Show("هذا الموكل موجود مسبقاً");
                        throw new Exception();
                    }
                }



                cmd.CommandText = "Insert into Client values(@name,@id,@adress,@tel)";
                cmd.Connection = cn;

                SqlParameter[] par = new SqlParameter[4];
                par[0] = new SqlParameter("@name", textBox1.Text);
                par[1] = new SqlParameter("@id", textBox4.Text);
                par[2] = new SqlParameter("@adress", textBox2.Text);
                par[3] = new SqlParameter("@tel", textBox3.Text);
                cmd.Parameters.AddRange(par);

                cn.Open();
                int f = cmd.ExecuteNonQuery();
                if (f > 0)
                {
                    MessageBox.Show("تم الاضافة بنجاع");
                    cn.Close();
                    fillinformation();
                }
                else
                {
                    MessageBox.Show("العملية لم تتم");
                    cn.Close();
                }
            }
            catch { }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected = comboBox1.SelectedItem.ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Client where cName='"+ comboBox1.Text +"'";
            cmd.Connection = cn;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            textBox7.Text=dr["cAddress"].ToString();
            textBox5.Text = dr["cNationalId"].ToString();
            textBox6.Text = dr["cTel"].ToString();
            cn.Close();


        }

        private void button2_Click(object sender, EventArgs e)//تعديل موكل
        {
           
               
                
                SqlCommand cmd = new SqlCommand();
                
                cmd.CommandText = "UPDATE Client Set cName=@name,cNationalID=@id,cAddress=@adress,cTel=@tel Where cName='"+ selected +"'";
                cmd.Connection = cn;

                SqlParameter[] par = new SqlParameter[4];
                par[0] = new SqlParameter("@name", comboBox1.Text );
                par[1] = new SqlParameter("@id", textBox5.Text);
                par[2] = new SqlParameter("@adress", textBox7.Text);
                par[3] = new SqlParameter("@tel", textBox6.Text);
                cmd.Parameters.AddRange(par);

                cn.Open();
                int af = cmd.ExecuteNonQuery();
                if (af > 0)
                {
                    MessageBox.Show("تم التعديل بنجاع");
                    cn.Close();
                    fillinformation();
                }
                else
                {
                    MessageBox.Show("التعديل لم تتم");
                    cn.Close();
                }
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecteddel = comboBox2.SelectedItem.ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Client where cName='" + comboBox2.Text + "'";
            cmd.Connection = cn;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            textBox11.Text = dr["cAddress"].ToString();
            textBox9.Text = dr["cNationalId"].ToString();
            textBox10.Text = dr["cTel"].ToString();
            cn.Close();

        }

        private void button3_Click(object sender, EventArgs e)//حذف موكل
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Delete Client where cName=@name";
            cmd.Connection = cn;
            SqlParameter par = new SqlParameter("@name", selecteddel);
            cmd.Parameters.Add(par);

            cn.Open();
            int f = cmd.ExecuteNonQuery();
            if (f>0)
            {
                MessageBox.Show("تم الحذف بنجاح");
                cn.Close();
                fillinformation();
                
            }
            else 
            {
                MessageBox.Show("لم يتم الحذف");
                cn.Close();
            }
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

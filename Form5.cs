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
    public partial class Form5 : Form
    {
        string selecteddel;
        string selected;
        SqlConnection cn = new SqlConnection("Data Source=.;Initial Catalog=Lawer;Integrated Security=True");
        public Form5()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)//الخروج من البرنامج
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "select * from Opponent";
                DataSet ds = new DataSet();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                cmd.Connection = cn;
                ad.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][0].ToString().Trim() == textBox4.Text)
                    {
                        MessageBox.Show("هذا الخصم موجود مسبقاً");
                        throw new Exception();
                    }
                }

                cmd.CommandText = "INSERT INTO Opponent  (oName, oAdress, oTel)  VALUES (@name,@adress,@tel)";
                cmd.Connection = cn;

                SqlParameter[] par = new SqlParameter[3];
                par[0] = new SqlParameter("@name", textBox4.Text);
                par[1] = new SqlParameter("@adress", textBox3.Text);
                par[2] = new SqlParameter("@tel", textBox2.Text);

                cmd.Parameters.AddRange(par);

                cn.Open();
                int f = cmd.ExecuteNonQuery();
                if (f > 0)
                {
                    MessageBox.Show("تم الاضافة بنجاح");
                    cn.Close();
                    fillcombopponent();
                    
                }
                else
                {
                    MessageBox.Show("فشل الاضافة");
                    cn.Close();
                    fillcombopponent();
                    
                }
                
            }
            catch { }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            fillcombopponent();

        }

        private void fillcombopponent()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from Opponent";
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected = comboBox1.SelectedItem.ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from opponent where oName='"+ comboBox1.Text +"'";
            cmd.Connection = cn;

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            textBox7.Text = dr["oAdress"].ToString();
            textBox6.Text = dr["oTel"].ToString();
            cn.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecteddel = comboBox2.SelectedItem.ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from opponent where oName='" + comboBox2.Text + "'";
            cmd.Connection = cn;

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            textBox11.Text = dr["oAdress"].ToString();
            textBox10.Text = dr["oTel"].ToString();
            cn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Delete Opponent where oName=@name";
            cmd.Connection = cn;
            SqlParameter par = new SqlParameter("@name", selecteddel);
            cmd.Parameters.Add(par);

            cn.Open();
            int f = cmd.ExecuteNonQuery();
            if (f > 0)
            {
                MessageBox.Show("تم الحذف بنجاح");
                cn.Close();
                fillcombopponent();

            }
            else
            {
                MessageBox.Show("لم يتم الحذف");
                cn.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "UPDATE Opponent SET oName = @name, oAdress = @adress, oTel = @tel  WHERE oName = '" + selected + "'";
            cmd.Connection = cn;

            SqlParameter[] par = new SqlParameter[3];
            par[0] = new SqlParameter("@name", comboBox1.Text);
            par[1] = new SqlParameter("@adress", textBox7.Text);
            par[2] = new SqlParameter("@tel", textBox6.Text);
            cmd.Parameters.AddRange(par);

            cn.Open();
            int af = cmd.ExecuteNonQuery();
            if (af > 0)
            {
                MessageBox.Show("تم التعديل بنجاع");
                cn.Close();
                fillcombopponent();
            }
            else
            {
                MessageBox.Show("التعديل لم تتم");
                cn.Close();
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

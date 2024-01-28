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
    public partial class Form6 : Form
    {
        string selecteddeletcase;
        string selecteddeletsesion;
        string selectededitcase;
        string selectededitsesion;
        SqlConnection cn = new SqlConnection("Data Source=.;Initial Catalog=Lawer;Integrated Security=True");
        public Form6()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)//الخروج من البرنامج
        {
            Application.Exit();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            groupBox3.Enabled = true;
            button1.Enabled = true;
           

            groupBox2.Enabled = false;
            button2.Enabled = false;
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false ;
            groupBox3.Enabled = false ;
            button1.Enabled = false ;
            

            groupBox2.Enabled = true ;
            button2.Enabled = true;
            

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

            groupBox6.Enabled = true;
            groupBox4.Enabled = true;
            button6.Enabled = true;
            
            
            groupBox5.Enabled = false;
            button3.Enabled = false;

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            groupBox9.Enabled = true;
            groupBox7.Enabled = true;
            button8.Enabled = true;

            groupBox8.Enabled = false;
            button7.Enabled = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox6.Enabled = false;
            groupBox4.Enabled = false;
            button6.Enabled = false;

            groupBox5.Enabled = true;
            button3.Enabled = true;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            groupBox9.Enabled = false;
            groupBox7.Enabled = false;
            button8.Enabled = false;

            groupBox8.Enabled = true;
            button7.Enabled = true;


        }

        private void Form6_Load(object sender, EventArgs e)
        {
            fillallcombobox();
        }

        private void fillallcombobox()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox9.Items.Clear();
            comboBox14.Items.Clear();
            comboBox8.Items.Clear();
            comboBox13.Items.Clear();
            comboBox10.Items.Clear();
            comboBox15.Items.Clear();
            comboBox5.Items.Clear();
            comboBox17.Items.Clear();

            comboBox16.Items.Clear();
            comboBox7.Items.Clear();
            SqlCommand cmd = new SqlCommand("select cName from Client", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
                comboBox9.Items.Add(dr[0].ToString());
                comboBox14.Items.Add(dr[0].ToString());
            }


            //-----------------------------------------

            cmd.CommandText = "select oName from Opponent";
            cmd.Connection = cn;
            dr.Close();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());
                comboBox13.Items.Add(dr[0].ToString());
                comboBox8.Items.Add(dr[0].ToString());
            }


            //-----------------------------------------------
            cmd.CommandText = "select CaseNo from [Case]";
            cmd.Connection = cn;
            dr.Close();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                comboBox5.Items.Add(dr[0].ToString());
                comboBox16.Items.Add(dr[0].ToString());
                comboBox17.Items.Add(dr[0].ToString());
            }


            //---------------------------------------------------------
            cmd.CommandText = "SELECT  CaseNo FROM  Sessions";
            cmd.Connection = cn;
            dr.Close();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox10.Items.Add(dr[0].ToString());
                comboBox15.Items.Add(dr[0].ToString());

            }
            cn.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//اضافة قضية
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "INSERT INTO [Case]   (CaseNo, Subject, AuthorizationNo, CourtName, Date, cName, cStatus, cOpponent, oStatus)  VALUES  (@no,@subject,@autho,@courtn,@data,@cname,@cstatus,@copponent,@ostatus)";
            SqlParameter[] par = new SqlParameter[9];
            par[0] = new SqlParameter("@no", textBox2.Text);
            par[1] = new SqlParameter("@subject", textBox1.Text);
            par[2] = new SqlParameter("@autho", textBox3.Text);
            par[3] = new SqlParameter("@courtn", textBox4.Text);
            par[4] = new SqlParameter("@data",dateTimePicker1.Value.Date  );
            par[5] = new SqlParameter("@cname", comboBox1.SelectedItem.ToString() );
            par[6] = new SqlParameter("@cstatus",comboBox3.SelectedItem.ToString());
            par[7] = new SqlParameter("@copponent",comboBox2.SelectedItem.ToString() );
            par[8] = new SqlParameter("@ostatus",comboBox4.SelectedItem.ToString() );

            cmd.Parameters.AddRange(par);

            cn.Open();
            int f = cmd.ExecuteNonQuery();
            if (f > 0)
            {
                MessageBox.Show("تم اضافة القضية بنجاح");
                cn.Close();
                fillallcombobox();
            }
            else 
            {
                MessageBox.Show("فشل عملية الاضافة");
                cn.Close();
            }
            


        }

        private void button2_Click(object sender, EventArgs e)//اضافة جلسة
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Sessions  (Date, Decision, Demands, CaseNo)  VALUES   (@data,@decision,@demands,@caseno)", cn);

            SqlParameter[] par = new SqlParameter[4];
            par[0] = new SqlParameter("@data", dateTimePicker2.Value.Date);
            par[1] = new SqlParameter("@decision", textBox5.Text);
            par[2] = new SqlParameter("@demands", textBox6.Text);
            par[3] = new SqlParameter("@caseno", comboBox5.SelectedItem.ToString());

            cmd.Parameters.AddRange(par);

            cn.Open();
            int f = cmd.ExecuteNonQuery();
            if (f > 0)
            {
                MessageBox.Show("تم اضافة الجلسة بنجاح");
                cn.Close();
                fillallcombobox();
            }
            else 
            {
                MessageBox.Show("فشل اضافة جلسة جديدة");
                cn.Close();
            }
            

        }

        private void button6_Click(object sender, EventArgs e)//تعديل قضية
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE [Case] SET   CaseNo = @cno, Subject = @sub, AuthorizationNo = @auth, CourtName = @cour, Date = @dt, cName = @cname, cStatus = @csta, cOpponent = @cop, oStatus = @osta  WHERE (CaseNo = '"+selectededitcase +"')";
            cmd.Connection = cn;
            SqlParameter[] par = new SqlParameter[9];
            par[0] = new SqlParameter("@cno", comboBox16.SelectedItem.ToString());
            par[1] = new SqlParameter("@sub", textBox12.Text);
            par[2] = new SqlParameter("@auth", textBox10.Text);
            par[3] = new SqlParameter("@cour", textBox9.Text);
            par[4] = new SqlParameter("@dt", dateTimePicker4.Value.Date);
            par[5] = new SqlParameter("@cname",comboBox9.SelectedItem.ToString());
            par[6] = new SqlParameter("@csta", comboBox7.Text );
            par[7] = new SqlParameter("@cop", comboBox8.SelectedItem.ToString());
            par[8] = new SqlParameter("@osta", comboBox6.SelectedItem.ToString());

            cmd.Parameters.AddRange(par);

            cn.Open();
            int f = cmd.ExecuteNonQuery();
            if (f > 0)
            {
                MessageBox.Show("تم التعديل بنجاح");
                cn.Close();
                fillallcombobox();
            }
            else 
            {
                MessageBox.Show("عملية التعديل لم تتم");
                cn.Close();
            }
            
        }

        private void comboBox16_SelectedIndexChanged(object sender, EventArgs e)//ملئ كومبو بوكس رقم القضية داخل تعديل القضية
        {
            selectededitcase = comboBox16.SelectedItem.ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from [Case] where CaseNo=@cno";

            SqlParameter par = new SqlParameter();
            par = new SqlParameter("@cno", comboBox16.SelectedItem.ToString());

            cmd.Parameters.Add(par);

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            textBox12.Text = dr[1].ToString();
            textBox10.Text = dr[2].ToString();
            textBox9.Text = dr[3].ToString();
            dateTimePicker4.Value = DateTime.Parse(dr[4].ToString());
            comboBox9.Text = dr[5].ToString();
            comboBox7.Text = dr[6].ToString();
            comboBox8.Text = dr[7].ToString();
            comboBox6.Text = dr[8].ToString();

            cn.Close();
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)//ملئ كومبو بوكس رقم القضية داخل تعديل الجلسة
        {
            selectededitsesion = comboBox10.SelectedItem.ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from [Sessions] where CaseNo=@cas";

            SqlParameter p = new SqlParameter();
            p = new SqlParameter("@cas", selectededitsesion );
            cmd.Parameters.Add(p);

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            
            dateTimePicker3.Value=DateTime.Parse(dr[0].ToString());
            textBox8.Text = dr[1].ToString();
            textBox7.Text = dr[2].ToString();
            cn.Close();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox15_SelectedIndexChanged(object sender, EventArgs e)//ملئ كومبو بوكس رقم القضية داخل حذف الجلسة
        {
            selecteddeletsesion = comboBox15.SelectedItem.ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from [Sessions] where CaseNo=@cas";

            SqlParameter p = new SqlParameter();
            p = new SqlParameter("@cas", selecteddeletsesion);
            cmd.Parameters.Add(p);

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            dateTimePicker5.Value = DateTime.Parse(dr[0].ToString());
            textBox14.Text = dr[1].ToString();
            textBox13.Text = dr[2].ToString();
            cn.Close();
        }

        private void button3_Click(object sender, EventArgs e)//تعديل جلسة
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "UPDATE  Sessions SET   Date = @dt, Decision = @dec, Demands = @dem WHERE   (CaseNo = '"+selectededitsesion +"')";
            
            SqlParameter[] par = new SqlParameter[3];
            par[0] = new SqlParameter("@dt", dateTimePicker3.Value.Date);
            par[1] = new SqlParameter("@dec", textBox8.Text);
            par[2] = new SqlParameter("@dem", textBox7.Text);

            cmd.Parameters.AddRange(par);

            cn.Open();
            int f = cmd.ExecuteNonQuery();
            if (f > 0)
            {
                MessageBox.Show("تم التعديل بنجاح");
                cn.Close();
                fillallcombobox();
            }
            else
            {
                MessageBox.Show("عملية التعديل لم تتم");
                cn.Close();
            }
            

        }

        private void button8_Click(object sender, EventArgs e)//حذف قضية
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "DELETE FROM [Case] WHERE  (CaseNo = @c)";

            SqlParameter p = new SqlParameter("@c", selecteddeletcase);
            cmd.Parameters.Add(p);

            cn.Open();
            int f = cmd.ExecuteNonQuery();
            if (f > 0)
            {
                MessageBox.Show("تم الحذف بنجاح");
                cn.Close();
                fillallcombobox();
            }
            else
            {
                MessageBox.Show("عملية الحذف لم تتم");
                cn.Close();
            }
            
        }

        private void comboBox17_SelectedIndexChanged(object sender, EventArgs e)//ملئ كومبو بوكس رقم القضية داخل حذف قضية
        {
            selecteddeletcase  = comboBox17.SelectedItem.ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select * from [Case] where CaseNo=@cno";

            SqlParameter par = new SqlParameter();
            par = new SqlParameter("@cno", comboBox17.SelectedItem.ToString());

            cmd.Parameters.Add(par);

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            textBox18.Text = dr[1].ToString();
            textBox16.Text = dr[2].ToString();
            textBox15.Text = dr[3].ToString();
            dateTimePicker6.Value = DateTime.Parse(dr[4].ToString());
            comboBox14.Text = dr[5].ToString();
            comboBox12.Text = dr[6].ToString();
            comboBox13.Text = dr[7].ToString();
            comboBox11.Text = dr[8].ToString();

            cn.Close();
        }

        private void button7_Click(object sender, EventArgs e)//حذف جلسة
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "DELETE FROM Sessions WHERE  (CaseNo = @c)";

            SqlParameter p = new SqlParameter("@c", selecteddeletsesion );
            cmd.Parameters.Add(p);

            cn.Open();
            int f = cmd.ExecuteNonQuery();
            if (f > 0)
            {
                MessageBox.Show("تم الحذف بنجاح");
                cn.Close();
                fillallcombobox();
            }
            else
            {
                MessageBox.Show("عملية الحذف لم تتم");
                cn.Close();
            }
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection("Data Source=.;Initial Catalog=Lawer;Integrated Security=True");

            DataSet set = new DataSet();
             set.ReadXml("XMLFile2.xml");

             byte[] decrypted = Convert.FromBase64String(set.Tables[0].Rows[0][0].ToString());

             string decryptedstring = UTF8Encoding.UTF8.GetString(decrypted);

             if (set.Tables[0].Rows[0][0].ToString() != "" && decryptedstring == textBox1.Text)
             {
                 SqlCommand cmd = new SqlCommand();
                 cmd.CommandText = "Insert Into User_Accounts values(@Name,@Pass)";
                 cmd.Connection = cn;

                 SqlParameter[] par = new SqlParameter[2];
                 par[0] = new SqlParameter("@Name", textBox2.Text);
                 par[1] = new SqlParameter("@Pass", textBox3.Text);
                 cmd.Parameters.AddRange(par);

                 cn.Open();
                 int f = cmd.ExecuteNonQuery();
                 if (f > 0)
                 {
                     MessageBox.Show("تمت العملية بنجاح");
                 }
                 else
                     MessageBox.Show("فشل اضافة مستخدم جديد");
                 cn.Close();
             }
             else MessageBox.Show("كلمة مرور الادمن غير صحيحة");
             
           
        }
    }
}

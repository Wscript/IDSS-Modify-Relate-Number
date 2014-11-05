using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace IDSS_Modify_Relate_Number
{
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text == "" || textBoxPassword.Text == "")
            {
                MessageBox.Show("用户名或密码不能为空!");
            }
            else
            {
                //定义数据内存中缓存,后面填充数据使用  
                DataSet ds = new DataSet();

                //定义数据库连接语句
                string consqlserver = ConfigurationManager.ConnectionStrings["IDSSConnectionString"].ToString() + ";Password=CSD;";

                //定义SQL查询语句  
                string sql = "SELECT * FROM SYSUSER WHERE SYSUSERID='" + textBoxUsername.Text + "'";

                //定义SQL Server连接对象  
                SqlConnection con = new SqlConnection(consqlserver);

                //数据库命令和数据库连接  
                SqlDataAdapter da = new SqlDataAdapter(sql, con);

                try
                {
                    da.Fill(ds);                                    //填充数据  
                    if (ds.Tables[0].Rows.Count > 0)                //判断是否符合条件的数据记录  
                    {
                        if (ds.Tables[0].Rows[0]["SYSPASSWORD"].ToString() == textBoxPassword.Text)
                        {
                            MessageBox.Show("用户名密码正确!");
                            //Application.Run(new RelateNumberChange());
                        }
                        else
                        {
                            MessageBox.Show("密码不正确!");
                        } 
                    }
                    else
                    {
                        MessageBox.Show("用户名不存在!");
                    }
                }
                catch (Exception msg)
                {
                    throw new Exception(msg.ToString());  //异常处理  
                }
                finally
                {
                    con.Close();                    //关闭连接  
                    con.Dispose();                  //释放连接  
                    da.Dispose();                   //释放资源  
                }  
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace ADO.NET
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (txtun.Text.Length <= 0 || pw.Password.Length <= 0)
            {
                MessageBox.Show("请输入用户名及密码");
                txtun.Clear();
                pw.Clear();
            }
            else
            {
                SqlConnection con = new SqlConnection("Data Source =.;Initial Catalog=Student;Integrated Security=true");
                SqlCommand com = new SqlCommand();
                com.CommandText = "select * from UserTable";
                com.Connection = con;
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                string n; string p;
                int m = 0;

                while (reader.Read())
                {
                    n = reader["UserName"].ToString();
                    p = reader["Password"].ToString();
                    if (n == txtun.Text.Trim() && p == pw.Password)
                    {
                        m = 1;
                    }
                }

                if(m==1)
                {  StudentWindow sw = new StudentWindow(); sw.ShowDialog();
                this.Close();
               
                }
                else
                {
                    MessageBox.Show("用户名或密码输入错误，请重新输入");
                    txtun.Clear();
                    pw.Clear();
                }
                con.Close();
            }


            //else
            //{
            //    SqlConnection con = new SqlConnection("Data Source =.;Initial Catalog=Student;Integrated Security=true");
            //    SqlCommand com = new SqlCommand();
            //    com.CommandText = string.Format("select count(*) from UserTable where UserName='{0}' and Password='{1}';", txtun.Text, pw.Password);
            //    com.Connection = con;
            //    con.Open();
            //    int m = (int)com.ExecuteScalar();
            //    con.Close();
            //    if (m == 1)
            //    {
            //        MessageBox.Show("登陆成功");
            //        this.DialogResult = true;
            //        //关闭应用程序
            //        //App.Current.Shutdown();
            //        //StudentWindow studentWindow = new StudentWindow();
            //        //this.Close();
            //        //studentWindow.Show();
            //        //studentWindow.ShowDialog();
            //    }
            //    else
            //    {
            //        MessageBox.Show("用户名或密码错误，请重新输入");
            //        txtun.Clear();
            //        pw.Clear();
            //    }
            //}
        }
    }
}


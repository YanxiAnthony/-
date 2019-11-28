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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace ADO.NET
{
    /// <summary>
    /// StudentWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StudentWindow : Window
    {
        SqlConnection con = new SqlConnection("data source=.;initial catalog=Student;integrated security=true;");
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        public StudentWindow()
        {
            InitializeComponent();
        }
        //鼠标左移事件
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        //数据填充至DataGrid
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            da = new SqlDataAdapter("select * from StudentTable", con);
            ds.Tables.Add("StudentTable");
            da.Fill(ds, "StudentTable");
            dataGrid.ItemsSource = ds.Tables[0].DefaultView;
        }

        //选择记录
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int i = dataGrid.SelectedIndex;
            if (i != -1)
            {
                try
                {
                    DataRowView dr = dataGrid.SelectedItem as DataRowView;
                    stuno.Text = dr["StuNo"].ToString();
                    stuname.Text = dr["StuName"].ToString();
                    stuborn.Text = dr["Born"].ToString();
                    stuaddress.Text = dr["Address"].ToString();
                    stuphone.Text = dr["Telphone"].ToString();
                    stutime.Text = dr["Time"].ToString();
                }
                catch
                {
                    MessageBox.Show("请选择记录");
                }
            }
            else
                return;
        }

        //删除
        private void Delect_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("是否删除？", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (r == MessageBoxResult.Yes)
            {
                SqlCommand com = new SqlCommand();

                com.CommandText = "delete from StudentTable where StuNo='" + stuno.Text + "'";
                com.Connection = con;
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

                ds.Clear();
                da.Fill(ds, "StudentTable");

                MessageBox.Show("删除成功");
            }
            else
                return;
        }

        //修改
        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBoxResult r = MessageBox.Show("是否修改？", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Information);
            //if (r == MessageBoxResult.Yes)
            //{
            //    SqlCommand com = new SqlCommand();

            //    com.CommandText = String.Format("update StudentTable set StuName='{0}',Born='{1}',Telphone='{2}',Address='{3}',Time='{4}' where StuNo='{5}'", stuname.Text, stuborn.Text, stuphone.Text, stuaddress.Text, stutime.Text, stuno.Text);
            //    com.Connection = con;
            //    con.Open();
            //    com.ExecuteNonQuery();
            //    con.Close();

            //    ds.Clear();
            //    da.Fill(ds, "StudentTable");

            //    MessageBox.Show("修改成功");
            //}
            //else
            //    return;

            MessageBoxResult r = MessageBox.Show("是否修改？", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (r == MessageBoxResult.Yes)
            {
                SqlCommand com = new SqlCommand();

                com.Parameters.Add("@no", SqlDbType.NVarChar, 50).Value = stuno.Text;
                com.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = stuname.Text;
                com.Parameters.Add("@born", SqlDbType.DateTime).Value = stuborn.Text;
                com.Parameters.Add("@phone", SqlDbType.NVarChar, 50).Value = stuphone.Text;
                com.Parameters.Add("@address", SqlDbType.NVarChar, 50).Value = stuaddress.Text;
                com.Parameters.Add("@time", SqlDbType.DateTime).Value = stutime.Text;

                com.CommandText = "update StudentTable set StuName=@name,Born=@born,Telphone=@phone,Address=@address,Time=@time where StuNo=@no";
                com.Connection = con;
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

                ds.Clear();
                da.Fill(ds, "StudentTable");

                MessageBox.Show("修改成功");
            }
            else
                return;
        }

        //添加
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            //if (addno.Text.Length == 0 || addname.Text.Length == 0 || addborn.Text.Length == 0 || addphone.Text.Length == 0 || addaddress.Text.Length == 0 || addtime.Text.Length == 0)
            //{
            //    MessageBox.Show("Sorry");
            //}

            bool b = ValidateNull();
            if (b == false)
            {
                MessageBox.Show("输入值不能为空！");
            }
            else
            {

                //string.Format("insert into StudentTable(StuNo,StuName,Born,Telphone,Address,Time) values ('{0}','{1}','{2}','{3}','{4}','{5}')", addno.Text, addname.Text, addborn.Text, addphone.Text, addaddress.Text, addtime.Text);

                SqlCommand com = new SqlCommand();

                com.Parameters.Add("@addno", SqlDbType.NVarChar, 50).Value = addno.Text;
                com.Parameters.Add("@addname", SqlDbType.NVarChar, 50).Value = stuname.Text;
                com.Parameters.Add("@addborn", SqlDbType.DateTime).Value = stuborn.Text;
                com.Parameters.Add("@addphone", SqlDbType.NVarChar, 50).Value = stuphone.Text;
                com.Parameters.Add("@addaddress", SqlDbType.NVarChar, 50).Value = stuaddress.Text;
                com.Parameters.Add("@addtime", SqlDbType.DateTime).Value = stutime.Text;

                com.CommandText = "insert into StudentTable(StuNo,StuName,Born,Telphone,Address,Time) values(@addno,@addname,@addborn,@addphone,@addaddress,@addtime);";
                com.Connection = con;
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

                ds.Clear();
                da.Fill(ds, "StudentTable");

                MessageBox.Show("添加成功！");
            }
        }

        //定义一个方法，用于文本框非空判断
        private bool ValidateNull()
        {
            if (addno.Text.Length == 0 || addname.Text.Length == 0 || addborn.Text.Length == 0 || addphone.Text.Length == 0 || addaddress.Text.Length == 0 || addtime.Text.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //搜索
        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            string s1 = "", sql = "";

            switch (combobox.Text)
            {
                case "学号": s1 = "StuNo"; break;
                case "姓名": s1 = "StuName"; break;
                case "生日": s1 = "Born"; break;
                case "电话": s1 = "Telphone"; break;
                case "地址": s1 = "Address"; break;
                case "入学时间": s1 = "Time"; break;
                default: s1 = ""; break;
            }
            if (s1 != "")
            {
                sql = "select * from StudentTable where " + s1 + "='" + searchtxt.Text + "';";
            }
            else
            {
                sql = "select * from StudentTable;";
            }
            da.SelectCommand.CommandText = sql;
            ds.Clear();
            da.Fill(ds, "StudentTable");
            MessageBox.Show("查询成功");
        }

        //搜索
        //private void Search_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    int x = combobox.SelectedIndex;
        //    string s,sql;
            
        //    SqlCommand com = new SqlCommand();
        //    switch (x)
        //    {
        //        case 0: s = "StuNo";break;
        //        case 1: s = "StuName"; break;
        //        case 2: s = "Born"; break;
        //        case 3: s = "Telphone"; break;
        //        case 4: s = "Address"; break;
        //        case 5: s = "Time"; break;
        //        default: s = "";break;
        //    }
        //    if (s != "")
        //    {
        //        sql = "select * from StudentTable where " + s + "='" + searchtxt.Text + "';";
        //    }
        //    else
        //    {
        //        sql = "select * from StudentTable;";
        //    }
        //    da.SelectCommand.CommandText = sql;
        //    ds.Clear();
        //    da.Fill(ds, "StudentTable");
            
        //    MessageBox.Show("搜索成功", "信息提示", MessageBoxButton.OK);
        //}
    }
}

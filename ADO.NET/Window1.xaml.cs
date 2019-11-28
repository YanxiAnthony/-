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
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        SqlConnection con = new SqlConnection("data source=.;initial catalog=Student;integrated security=true;");
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        public Window1()
        {
            InitializeComponent();
        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            da = new SqlDataAdapter("select * from StudentTable", con);
            ds.Tables.Add("StudentTable");
            da.Fill(ds, "StudentTable");
            dataGrid.ItemsSource = ds.Tables[0].DefaultView;

            SqlCommand com = new SqlCommand();

            com.CommandText = "select * from StudentTable;";
            com.Connection = con;
            con.Open();

            SqlDataReader r = com.ExecuteReader();
            string s = "";

            while (r.Read())
            {
                s = r["StuNo"].ToString();
                comboBox.Items.Add(s);
            }

            con.Close();
            
        }
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            da.SelectCommand.CommandText = "select * from StudentTable where StuNo='" + comboBox.SelectedItem.ToString() + "'";
            ds.Clear();
            da.Fill(ds, "StudentTable");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            da.SelectCommand.CommandText = "select * from StudentTable";
            ds.Clear();
            da.Fill(ds, "StudentTable");
        }
    }
}

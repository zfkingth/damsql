using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace LocalDbConfig
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string constring = hammergo.GlobalConfig.PubConstant.ConfigData.ConnectionString;
            txtSqlString.Text = constring;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "sql 数据库文件(*.mdf)|*.mdf";
            if(dialog.ShowDialog()== true)
            {
              txtSqlString.Text=string.Format(@"Server=(LocalDB)\MSSQLLocalDB; Integrated Security=true ;AttachDbFileName={0}",   dialog.FileName);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(txtSqlString.Text))
            {
                try
                {
                    sqlCon.Open();

                    MessageBox.Show("数据库连接测试成功!", "测试成功!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("连接测试失败", "错误!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            hammergo.GlobalConfig.PubConstant.ConnectionString = txtSqlString.Text;
            hammergo.GlobalConfig.PubConstant.updateConfigData();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConfigWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void namecbs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb != null&&instanceNameTextBox!=null)
            {
                if (cb.SelectedIndex == 2)
                {
                    instanceNameTextBox.IsReadOnly = false;
                    instanceNameTextBox.Background = new SolidColorBrush(Colors.White);
                }
                else 
                {
                    if (cb.SelectedIndex == 0)
                    {
                        instanceNameTextBox.Text = _expressInstanceName;
                    }
                    else
                    {
                        instanceNameTextBox.Text = "";
                    }

                    instanceNameTextBox.IsReadOnly = true;
                    instanceNameTextBox.Background = new SolidColorBrush(Colors.LightGray);
                }
            }
        }

        const string _expressInstanceName = "SQLEXPRESS";

        /// <summary>
        /// 连接字符串构造器
        /// </summary>
        
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            string constring=hammergo.GlobalConfig.PubConstant.ConfigData.ConnectionString;
             SqlConnectionStringBuilder ssb  = new SqlConnectionStringBuilder(constring);

            string dataSource = ssb.DataSource;
            string[] splitStrings = dataSource.Split('\\');
            serverNameTextBox.Text = splitStrings[0];
            //设置datasource
            if (splitStrings.Length >= 2)
            {
                string instanceName = splitStrings[1];
                if (instanceName.Equals(_expressInstanceName))
                {
                    //sqlexpress实例
                    dataSourceComboBox.SelectedIndex = 0;
                }
                else
                {
                    //其他命名实例
                    dataSourceComboBox.SelectedIndex = 2;
                   
                }

                instanceNameTextBox.Text = instanceName;
            }
            else
            {
                  //默认实例
                dataSourceComboBox.SelectedIndex = 1;
                instanceNameTextBox.Text = "";
            }

            //设置InitialCatalog	
            initialCatalogTextBox.Text = ssb.InitialCatalog;

            //设置验证方式
            if (ssb.IntegratedSecurity==true)
            {
                //windows集成验证
                securityComboBox.SelectedIndex = 0;
            }
            else
            {
                  //需要用户名，密码
                securityComboBox.SelectedIndex = 1;
                userNameTextBox.Text = ssb.UserID;
                passwordTextBox.Password = ssb.Password;
            }
        }

        private void dataSourceComboBox_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb != null )
            {
                if (cb.SelectedIndex == 0)
                {
                    //userNameTextBox.IsReadOnly = true;
                    //userNameTextBox.Background = new SolidColorBrush(Colors.LightGray);

                    //passwordTextBox.IsReadOnly = true;
                    //passwordTextBox.Background = new SolidColorBrush(Colors.LightGray);
                    mainGrid.RowDefinitions[4].Height = new GridLength(0, GridUnitType.Star);
                    mainGrid.RowDefinitions[5].Height = new GridLength(0, GridUnitType.Star);
                }
                else
                {
                    //userNameTextBox.IsReadOnly = false;
                    //userNameTextBox.Background = new SolidColorBrush(Colors.White);

                    //passwordTextBox.IsReadOnly = false;
                    //passwordTextBox.Background = new SolidColorBrush(Colors.White);
                    mainGrid.RowDefinitions[4].Height = new GridLength(1, GridUnitType.Star);
                    mainGrid.RowDefinitions[5].Height = new GridLength(1, GridUnitType.Star);

                }
            }
        }

        private string ConstructString()
        {
            SqlConnectionStringBuilder newssb = new SqlConnectionStringBuilder();
            //设置datasource
            string dataSource = serverNameTextBox.Text.Trim();
            switch (dataSourceComboBox.SelectedIndex)
            {
                case 0: 
                    //express
                    dataSource +=@"\"+ _expressInstanceName;
                    break;
                case 1: break;
                case 2: 
                    //自定义实例名称
                    dataSource +=@"\"+ instanceNameTextBox.Text.Trim();
                    break;
                default:
                    break;
            }
            newssb.DataSource = dataSource;

            //设置数据库
            newssb.InitialCatalog = initialCatalogTextBox.Text.Trim();

            if (securityComboBox.SelectedIndex==0)
            {
                //windows集成验证
                newssb.IntegratedSecurity = true;
              
            }
            else
            {
                //sql server 身份验证
                newssb.IntegratedSecurity = false;
                newssb.UserID = userNameTextBox.Text.Trim();
                newssb.Password = passwordTextBox.Password;
            }

            string conStr = newssb.ToString();

            return conStr;
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            hammergo.GlobalConfig.PubConstant.ConnectionString = ConstructString();
            hammergo.GlobalConfig.PubConstant.updateConfigData();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlCon=new SqlConnection(ConstructString()))
            {
                try
                {
                    sqlCon.Open();

                    MessageBox.Show("数据库连接测试成功!","测试成功!",MessageBoxButton.OK,MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("连接测试失败", "错误!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
            
    }
}

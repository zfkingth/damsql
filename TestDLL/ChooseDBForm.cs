using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Xml.Serialization;

namespace hammergo.TestDLL
{
    public partial class ChooseDBForm : DevExpress.XtraEditors.XtraForm
    {
        public ChooseDBForm()
        {
            InitializeComponent();
        }

        private const string FileName = "DBSource.xml";

        private static ChooseDBConfig configData = null;

        internal static ChooseDBConfig ConfigData
        {
            get
            {
                if (configData == null)
                {
                    if (File.Exists(FileName) == false) throw new Exception("找不到配置文件" + FileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(ChooseDBConfig));
                    // A FileStream is needed to read the XML document.
                    FileStream fs = new FileStream(FileName, FileMode.Open);
                    configData = (ChooseDBConfig)serializer.Deserialize(fs);
                    fs.Close();
                }
                return configData;
            }

        }

        public static void updateConfigData()
        {
            XmlSerializer ser = new XmlSerializer(typeof(ChooseDBConfig));
            using (TextWriter writer = new StreamWriter(FileName))
            {
                ser.Serialize(writer, ConfigData);
                writer.Close();
            }

        }

        private void selectItem()
        {
            if (ConfigData.SelectedName != null && configData.DataSourceList != null)
            {
                DataSourceItem selItm = null;

                foreach (DataSourceItem item in ConfigData.DataSourceList)
                {
                    if (item.Name == ConfigData.SelectedName)
                    {
                        selItm = item;
                        break;
                    }
                }

                if (selItm != null)
                {
                    lookUpEdit1.EditValue = selItm;
                }


            }
        }

        private void ChooseDBForm_Load(object sender, EventArgs e)
        {
            //configData = new ChooseDBConfig();
            //updateConfigData();

            dataSourceItemBindingSource.DataSource = ConfigData.DataSourceList;
            selectItem();

        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                switch (e.Button.Tag as string)
                {
                    case "open": click_open(); break;
                    case "new": click_new(); break;
                    case "delete": click_delete(); break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void click_delete()
        {
            DataSourceItem item = lookUpEdit1.EditValue as DataSourceItem;
            if (item != null)
            {
                dataSourceItemBindingSource.Remove(item);
            }

            if (dataSourceItemBindingSource.Count > 0)
            {
                item = dataSourceItemBindingSource[0] as DataSourceItem;
                lookUpEdit1.EditValue = item;
                ConfigData.SelectedName = item.Name;
            }
            else
            {
                lookUpEdit1.EditValue = null;
                ConfigData.SelectedName = null;
            }
        }

        private void click_new()
        {
            string val = Utility.Utility.InputBox("输入", "请输入数据源名称:", "", null);

            if (val != null)
            {
                string projectName = val.Trim();
                if (projectName.Length != 0)
                {
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string tempPath = folderBrowserDialog1.SelectedPath;

                        string newFilePath = String.Format("{0}\\{1}.mdb", tempPath, projectName);
                        if (File.Exists(newFilePath))
                        {
                            throw new Exception(projectName + ".mdb数据库已存在");

                        }
                        else
                        {
                            string modelDbFilePath = Application.StartupPath + @"\DataBase\dam3Mode.mdb";

                            if (File.Exists(modelDbFilePath) == false)
                            {
                                throw new Exception(string.Format("找不到模板数据库,请确定文件{0}是否存在!", modelDbFilePath));
                            }
                            else
                            {
                                File.Copy(modelDbFilePath, newFilePath, false);

                                if (File.Exists(newFilePath) == false)
                                {
                                    throw new Exception(string.Format("创建{0}数据库失败,请确定是否有写的权限!", newFilePath));
                                }

                                DataSourceItem newItem = new DataSourceItem();
                                newItem.FilePath = newFilePath;
                                newItem.Name = projectName;
                                dataSourceItemBindingSource.Add(newItem);
                                ConfigData.SelectedName = projectName;
                                lookUpEdit1.EditValue = newItem;
                            }
                        }

                    }
                }
            }
        }

        private void click_open()
        {



            string val = Utility.Utility.InputBox("输入", "请输入数据源名称:", "", null);

            if (val != null)
            {
                string name = val.Trim();
                if (name.Length != 0)
                {

                    DataSourceItem dsi = null;

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog1.FileName;
                        dsi = new DataSourceItem();
                        dsi.Name = name;
                        dsi.FilePath = filePath;

                    }
                    if (dsi != null)
                    {

                        dataSourceItemBindingSource.Add(dsi);
                       
                        lookUpEdit1.EditValue = dsi;
                    }

                }
            }
        }

        bool cancelClose = false;

        private void btnOK_Click(object sender, EventArgs e)
        {
            DataSourceItem selItem = lookUpEdit1.EditValue as DataSourceItem;

            if (selItem != null)
            {
                ConfigData.SelectedName = selItem.Name;
                updateConfigData();

                //检查选中的项，其数据库文件是否存在
                if (File.Exists(selItem.FilePath) == false)
                {
                    cancelClose = true;
                    XtraMessageBox.Show(this, "找不到配置文件" + selItem.FilePath, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    cancelClose = false;
                    string connectionStr = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Persist Security Info=False", selItem.FilePath);
                    hammergo.GlobalConfig.PubConstant.ConfigData.ConnectionString = connectionStr;
                    hammergo.GlobalConfig.PubConstant.updateConfigData();
                }

               

            }
        }



        private void lookUpEdit1_EditValueChanged_1(object sender, EventArgs e)
        {

            if (lookUpEdit1.EditValue is DataSourceItem == false)
            {
                btnOK.Enabled = false;
            }
            else
            {
                btnOK.Enabled = true;
            }
        }

        private void ChooseDBForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = cancelClose;
            cancelClose = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

    }
}
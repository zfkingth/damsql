using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace hammergo.ImportOldData
{
    public partial class ImportDataControl : DevExpress.XtraEditors.XtraUserControl
    {
        protected internal dam3ModeDataSet ds = new dam3ModeDataSet();

         

        public ImportDataControl()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                setAllButtonEnable(false);

                ImportClasses.ImportAppType ipt = new ImportClasses.ImportAppType(this);

                ipt.startWork();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected internal void setAllButtonEnable(bool enable)
        {
            foreach (Control control in this.Controls)
            {
                if (control is SimpleButton)
                {
                    (control as SimpleButton).Enabled = enable;
                }
            }
        }

        private void btnOld_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                lblOld.Text = openFileDialog1.FileName;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string oldConStr = "Provider=Microsoft.ACE.OLEDB.12.0;  Data Source=" + lblOld.Text;

                if (System.IO.File.Exists(lblOld.Text))
                {
                    oleDbCon.Close();

                    oleDbCon.ConnectionString = oldConStr;

                    oleDbCon.Open();

                    //OldDSTableAdapters.仪器TableAdapter 仪器TableAdapter1 = new OldDSTableAdapters.仪器TableAdapter();

                    dam3ModeDataSetTableAdapters.ApparatusTableAdapter 仪器TableAdapter1 = new dam3ModeDataSetTableAdapters.ApparatusTableAdapter();

                    仪器TableAdapter1.Connection = oleDbCon;


                    仪器TableAdapter1.Fill(ds.Apparatus);

                    lblResult.Text = "初始化成功";
                }
                else
                {
                    XtraMessageBox.Show(this, "选择的数据库文件不存在!", "错误!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                setAllButtonEnable(false);

                ImportClasses.ImportProjectPartAndAppratus ipa = new ImportOldData.ImportClasses.ImportProjectPartAndAppratus(this);

                ipa.startWork();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                setAllButtonEnable(false);

                ImportClasses.ImportConstantParam ipt = new ImportClasses.ImportConstantParam(this);

                ipt.startWork();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            try
            {
                setAllButtonEnable(false);

                ImportClasses.ImportMessureParamAndValue ipt = new ImportClasses.ImportMessureParamAndValue(this);

                ipt.startWork();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            try
            {
                setAllButtonEnable(false);

                ImportClasses.ImportCalculateParamAndValue ipt = new ImportClasses.ImportCalculateParamAndValue(this);

                ipt.startWork();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            try
            {
                setAllButtonEnable(false);

                ImportClasses.ImportRemarks ipt = new ImportClasses.ImportRemarks(this);

                ipt.startWork();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                setAllButtonEnable(false);

                ImportClasses.ImportInputTask ipt = new ImportClasses.ImportInputTask(this);

                ipt.startWork();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

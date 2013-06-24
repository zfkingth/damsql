using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using hammergo.Model;

namespace hammergo.TestDLL
{
    public partial class Logon : DevExpress.XtraEditors.XtraForm
    {
        public Logon()
        {
            InitializeComponent();
        }

        private void ddlConnection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        hammergo.BLL.SysUserBLL userBLL = null;
        hammergo.BLL.RoleBLL roleBLL = null;

        private void Logon_Load(object sender, EventArgs e)
        {
            userBLL = new hammergo.BLL.SysUserBLL();
            roleBLL = new hammergo.BLL.RoleBLL();
            //lookUpEdit.Properties.ValueMember = "Country";
            //lookUpEdit1.Properties.DisplayMember = "UserName";
            userBindingSource.DataSource = userBLL.GetList();


            if (userBindingSource.Count > 0)
                lookUpEdit1.EditValue = userBindingSource[0];


           //lookUpEdit1.Properties.popupf
            lookUpEdit1.Properties.PopupFormWidth = lookUpEdit1.Size.Width-20;
            


        }

        private void lookUpEdit1_Popup(object sender, EventArgs e)
        {

        }

        private void lookUpEdit1_QueryPopUp(object sender, CancelEventArgs e)
        {
           


            
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            cancelClose = true;
            try
            {
                SysUser user = lookUpEdit1.EditValue as SysUser;
                if (user == null)
                {
                    throw new Exception("未选择登录用户!");
                }
                else
                {
                    if (userBLL.checkPassword(user.UserName, txtPassword.Text) == false)
                    {
                        throw new Exception("密码错误!");
                    }
                    else
                    {
                        //获取权限

                        hammergo.GlobalConfig.PubConstant.userPower= roleBLL.GetModelBy_RoleID(user.RoleID.Value).Power.Value;
                        hammergo.GlobalConfig.PubConstant.userName = user.UserName;

                        cancelClose = false;
                        
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        bool cancelClose = false;
        private void Logon_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = cancelClose;
            cancelClose = false;
        }
    }
}
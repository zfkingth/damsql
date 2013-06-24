using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using hammergo.Model;

namespace hammergo.TestDLL
{
    public partial class ChangePassword : DevExpress.XtraEditors.XtraUserControl
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                string oldPassword = txtOld.Text;
                string newPassword = txtNew.Text;
                string reNewPassword = txtReNew.Text;

                if (userBLL.checkPassword(hammergo.GlobalConfig.PubConstant.userName, oldPassword) == false)
                {
                    throw new Exception("旧密码不正确");

                }

                if (newPassword.Length == 0)
                {
                    throw new Exception("新密码不能为空");
                }

                if (newPassword != reNewPassword)
                {
                    throw new Exception("再次输入的密码不一致");
                }


                SysUser user = userBLL.GetModelBy_UserName(hammergo.GlobalConfig.PubConstant.userName);

                user.PasswordHash = userBLL.CreatePasswordHash(newPassword, user.Salt);

                userBLL.Update(user);


                XtraMessageBox.Show(this, "修改成功", "提示!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        hammergo.BLL.SysUserBLL userBLL = null;
        private void ChangePassword_Load(object sender, EventArgs e)
        {
            userBLL = new hammergo.BLL.SysUserBLL();
        }
    }
}

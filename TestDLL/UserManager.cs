using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using hammergo.Model;
using hammergo.Tracking;

namespace hammergo.TestDLL
{
    public partial class UserManager : DevExpress.XtraEditors.XtraUserControl
    {
        public UserManager()
        {
            InitializeComponent();
        }


        hammergo.BLL.SysUserBLL userBLL = null;
        hammergo.BLL.RoleBLL roleBLL = null;
        private void UserManager_Load(object sender, EventArgs e)
        {
            userBLL = new hammergo.BLL.SysUserBLL();
            roleBLL = new hammergo.BLL.RoleBLL();

            roleBindingSource.DataSource = roleBLL.GetList();
            sysUserBindingSource.DataSource = userBLL.GetList();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                TrackedList<SysUser> userList = sysUserBindingSource.DataSource as TrackedList<SysUser>;
                userBLL.UpdateList(userList);

                XtraMessageBox.Show(this, "修改成功", "提示!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = Utility.Utility.InputBox("输入", "新输入用户名:", "", null);
                if (userName != null && userName.Trim().Length != 0)
                {
                    userName = userName.Trim();

                    string password = Utility.Utility.InputBox("输入", "请输入密码:", "", '*');
                    if (password != null && password.Length != 0)
                    {
                        SysUser user = new SysUser();
                        user.UserName = userName;
                        user.RoleID = (roleBindingSource[0] as Role).RoleID;
                        user.Salt = userBLL.CreateSalt();
                        user.PasswordHash = userBLL.CreatePasswordHash(password, user.Salt);

                        sysUserBindingSource.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                SysUser user = gridView2.GetRow(gridView2.FocusedRowHandle) as SysUser;
                if (user != null)
                {
                    sysUserBindingSource.Remove(user);

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}

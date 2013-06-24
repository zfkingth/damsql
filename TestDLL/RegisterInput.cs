using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace hammergo.TestDLL
{
    public partial class RegisterInput : DevExpress.XtraEditors.XtraForm
    {
        public RegisterInput()
        {
            InitializeComponent();
        }

        bool allowClose = true;
        private void RegisterInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (allowClose == false)
            {
                allowClose = true;
                e.Cancel = true;
            }
        }

        static string hashCode = "";

        /// <summary>
        /// NString��EString�ǹ�Կ
        /// </summary>
        private const string NString = "lNCUYUBSzGdV8lYHAXMZtMIDX6FPTYpT+FCKAaQinhgL4uGT842Lc/lZzu+QzEerXlxQ9FOp5Ku47sqwOZHPbk0wzYEAmCAczymB0ZocqvBw7gxx/KRIHo1VUSx952a8W3bisDYC6xrpzgJ2bmETapEcBxHwVTR2Z4IxX8c53us=";
        private const string EString = "AQAB";

        private void RegisterInput_Load(object sender, EventArgs e)
        {
            txtMachineCode.Text = hashCode;
        }

        public static DateTime expireDate;

        public static void checkRegister()
        {
           
            //string cpuID = hammergo.Register.Helper.getCPUID();
            //string hdID = hammergo.Register.Helper.getBaseBoardID();

            //hammergo.Register.RSACryption rsa = new hammergo.Register.RSACryption();

            //rsa.GetHash(cpuID + hdID, ref hashCode);


            //hammergo.Register.CustomRSA cusRSA = new hammergo.Register.CustomRSA();

            //string desString = "";

            //try
            //{
            //    desString = cusRSA.DecryptProcess(GlobalConfig.PubConstant.ConfigData.RegisterCode, EString, NString); //rsa.RSADecrypt(privateKey, tools.GlobalConfig.ConfigData.RegisterCode);
            //}
            //catch (Exception)
            //{
            //    throw new Exception("ע������Ч!");

            //}


            //if (desString.StartsWith(hashCode))
            //{
            //    //ע�����ǶԵģ��鿴hash

            //    string dateString = desString.Substring(hashCode.Length);


            //     expireDate = DateTime.ParseExact(dateString, "yyyy-MM-dd", null);

            //    if (System.DateTime.Now > expireDate)
            //    {
            //        throw new Exception("����ע�����ѹ���,������ע��!");
            //    }
            //}
            //else
            //{
            //    throw new Exception("ע���������Ļ����벻���!");
            //}

          

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                allowClose = false;

                GlobalConfig.PubConstant.ConfigData.RegisterCode = txtRegister.Text;


                checkRegister();

                GlobalConfig.PubConstant.updateConfigData();

                allowClose = true;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
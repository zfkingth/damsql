using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;


namespace hammergo.MainApp
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {

        private static volatile MainForm instance = null;
        private static object lockHelper = new object();

        public static MainForm Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new MainForm();
                        }
                    }
                }

                return instance;
            }
        }

        private MainForm()
        {
            InitializeComponent();
        }

        public void setAdminToolBarVisible(bool visible)
        {
            if (visible)
            {
                barSubItemAdmin.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                barSubItemAdmin.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {

                //TestClass testClass = new TestClass();
                //testClass.test();
                //testClass.testTransaction();


                this.Text += Application.ProductName + Application.ProductVersion;

                string skinName = hammergo.GlobalConfig.PubConstant.ConfigData.SkinName;
                if (skinName != null && (skinName = skinName.Trim()).Length != 0)
                {
                    defaultLookAndFeel1.LookAndFeel.SkinName = skinName;
                }




                foreach (DevExpress.Skins.SkinContainer skinContainer in DevExpress.Skins.SkinManager.Default.Skins)
                {
                    DevExpress.XtraBars.BarCheckItem bciItem = new DevExpress.XtraBars.BarCheckItem(barManager1, false);
                    bciItem.Caption = skinContainer.SkinName;
                    bciItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(bciItem_ItemClick);
                    bsiSkin.AddItem(bciItem);
                    if (bciItem.Caption == skinName)
                    {
                        bciItem.Checked = true;
                    }
                }


                string imgPath = Application.StartupPath + @"\Resources\background.jpg";
                if (System.IO.File.Exists(imgPath))
                {
                    panelControl1.ContentImage = Image.FromFile(imgPath);
                }

#if Logon
                //hammergo.TestDLL.ChooseDBForm cdbForm = new hammergo.TestDLL.ChooseDBForm();
                //if (cdbForm.ShowDialog(this) == DialogResult.OK)
                //{
                    hammergo.TestDLL.Logon logonForm = new hammergo.TestDLL.Logon();
                    if (logonForm.ShowDialog(this) == DialogResult.OK)
                    {
                        setMenuEnable();
                    }
                    else
                    {
                        this.Close();
                    }
                //}
                //else
                //{
                //    this.Close();
                //}


#endif
#if Reg
                try
                {

                    hammergo.TestDLL.RegisterInput.checkRegister();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    hammergo.TestDLL.RegisterInput ri = new hammergo.TestDLL.RegisterInput();
                    if (ri.ShowDialog(this) != DialogResult.OK)
                    {
                        this.Close();
                    }
                }
#endif


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void setMenuEnable()
        {
            if (GlobalConfig.PubConstant.userPower != GlobalConfig.PubConstant.power_admin)
            {
                btiUserManager.Enabled = btiConfig.Enabled = btiDelete.Enabled = btiType.Enabled = false;
            }
        }

        void bciItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (DevExpress.XtraBars.BarCheckItemLink bcilItem in bsiSkin.ItemLinks)
            {
                DevExpress.XtraBars.BarCheckItem bciItem = bcilItem.Item as DevExpress.XtraBars.BarCheckItem;
                bciItem.Checked = false;

            }

            DevExpress.XtraBars.ItemClickEventArgs args = e as DevExpress.XtraBars.ItemClickEventArgs;
            DevExpress.XtraBars.BarCheckItem bciChecked = args.Item as DevExpress.XtraBars.BarCheckItem;
            bciChecked.Checked = true;

            defaultLookAndFeel1.LookAndFeel.SkinName = bciChecked.Caption;

            hammergo.GlobalConfig.PubConstant.ConfigData.SkinName = defaultLookAndFeel1.LookAndFeel.SkinName;
            hammergo.GlobalConfig.PubConstant.updateConfigData();
        }





        private void navBarItem1_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            barButtonItem12_ItemClick(sender, null);
        }

        private void navBarItem2_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            barButtonItem31_ItemClick(sender, null);

        }


        private void closeTab(DevExpress.XtraTab.XtraTabControl xtc, DevExpress.XtraTab.XtraTabPage xtp)
        {
            xtc.TabPages.Remove(xtp);

            if (xtp.Controls.Count > 0)
            {
                hammergo.Utility.ICustomDispose icd = xtp.Controls[0] as hammergo.Utility.ICustomDispose;
                if (icd != null)
                {
                    icd.CustomDispose();
                }
            }
            xtp.Dispose();

            xtc.SelectedTabPageIndex = xtc.TabPages.Count - 1;
        }


        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            DevExpress.XtraTab.XtraTabControl xtc = sender as DevExpress.XtraTab.XtraTabControl;
            DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs arg = e as DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs;


            DevExpress.XtraTab.XtraTabPage xtp = arg.Page as DevExpress.XtraTab.XtraTabPage;


            closeTab(xtc, xtp);


        }

        private void xtraTabControl1_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (xtraTabControl1.TabPages.Count == 0)
            {
                xtraTabControl1.Visible = false;
            }
        }

        private void xtraTabControl1_ControlAdded(object sender, ControlEventArgs e)
        {
            xtraTabControl1.Visible = true;
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraTab.XtraTabPage xtp = xtraTabControl1.SelectedTabPage;
            xtraTabControl1.TabPages.Remove(xtp);
            xtp.Dispose();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //xtraTabControl1.TabPages.Clear();

            DevExpress.XtraTab.XtraTabControl xtc = xtraTabControl1;


            while (xtc.TabPages.Count != 0)
            {
                
                closeTab(xtc, xtc.TabPages[0]);
            }


           
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraTab.XtraTabPage xtpReserve = xtraTabControl1.SelectedTabPage;

            while (xtraTabControl1.TabPages.Count > 1)
            {
                DevExpress.XtraTab.XtraTabPage xtp0 = xtraTabControl1.TabPages[0];
                DevExpress.XtraTab.XtraTabPage xtp1 = xtraTabControl1.TabPages[1];

                DevExpress.XtraTab.XtraTabPage xtpRemove = null;
                if (xtpReserve != xtp0)
                {

                    xtpRemove = xtp0;
                }
                else
                {
                    xtpRemove = xtp1;
                }

                //xtraTabControl1.TabPages.Remove(xtpRemove);
                //xtpRemove.Dispose();
                closeTab(xtraTabControl1, xtpRemove);

            }
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {



        }

        //private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{


        //    addNewUserControlInTabPage(typeof(hammergo.TestDLL.TestControl), "测试窗口", true);
        //}

        private Control addNewUserControlInTabPage(Type type, string caption, bool exclusive)
        {
            Control ret = null;
            try
            {


                if (exclusive == false || isUserControlExist(type) == false)
                {
                    DevExpress.XtraEditors.XtraUserControl control = type.InvokeMember(null, System.Reflection.BindingFlags.CreateInstance, null, null, null)
                                                                    as DevExpress.XtraEditors.XtraUserControl;

                    DevExpress.XtraTab.XtraTabPage xtp = new DevExpress.XtraTab.XtraTabPage();

                    xtp.Text = caption;
                    control.Dock = System.Windows.Forms.DockStyle.Fill;

                    xtp.Controls.Add(control);


                    xtraTabControl1.TabPages.Add(xtp);
                    xtraTabControl1.SelectedTabPage = xtp;

                    ret = control;

                    hammergo.Utility.IShowAppData isa = control as hammergo.Utility.IShowAppData;
                    if (isa != null)
                    {
                        isa.ShowDataEvent += manage_showDataMethod;
                    }
                }
                else
                {
                    XtraMessageBox.Show(this, "窗口已存在!", "!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ret;

        }

        public Control addNewUserControlInTabPage(Type type, string caption, bool exclusive, object[] args)
        {
            Control ret = null;

            try
            {
                if (exclusive == false || isUserControlExist(type) == false)
                {
                    DevExpress.XtraEditors.XtraUserControl control = type.InvokeMember(null, System.Reflection.BindingFlags.CreateInstance, null, null, args)
                                                                    as DevExpress.XtraEditors.XtraUserControl;

                    DevExpress.XtraTab.XtraTabPage xtp = new DevExpress.XtraTab.XtraTabPage();

                    xtp.Text = caption;
                    control.Dock = System.Windows.Forms.DockStyle.Fill;

                    xtp.Controls.Add(control);


                    xtraTabControl1.TabPages.Add(xtp);
                    xtraTabControl1.SelectedTabPage = xtp;

                    ret = control;

                }
                else
                {
                    XtraMessageBox.Show(this, "窗口已存在!", "!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ret;
        }




        private bool isUserControlExist(Type type)
        {
            foreach (DevExpress.XtraTab.XtraTabPage xtp in xtraTabControl1.TabPages)
            {
                if (xtp.Controls.Count > 0)
                {
                    if (xtp.Controls[0].GetType().Equals(type))
                    {
                        return true; //the same type control exist

                    }
                }
            }

            return false;
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //addNewUserControlInTabPage(typeof(ImportOldData.CreateDBControl), "创建新数据库", true);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                //PersistLayer.Utility.initialDAL(openFileDialog1.FileName);

            }


        }

        private void xtraTabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Y < 30)
                {
                    Point point = new Point(e.X, e.Y);
                    Control control = sender as Control;


                    popupMenu1.ShowPopup(control.PointToScreen(point));
                }
                else
                {

                }
            }
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.AppManage.AllAppManage), "管理所有仪器", true);


        }

        private void manage_showDataMethod(object sender, hammergo.Utility.AppSearchEventArgs e)
        {

            Control control = addNewUserControlInTabPage(typeof(DataManage.AppData), "仪器原始数据", false, new object[] { e.AppName });

            DataManage.AppData appDataForm = control as DataManage.AppData;
            if (appDataForm != null)
            {
                appDataForm.SearchButton_Click(null, null);
            }



        }


        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(DataManage.AppData), "仪器原始数据", false);
        }

        private void navBarItem3_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            barButtonItem13_ItemClick(sender, null);
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //addNewUserControlInTabPage(typeof(hammergo.MapModule.MapShow), "仪器布置图", false);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if(System.IO.Directory.Exists(MapModule.MapShow.mapDir))
            //{
            //    string[] files = System.IO.Directory.GetFiles(MapModule.MapShow.mapDir);
            //    foreach (string path in files)
            //    {
            //        System.IO.File.Delete(path);
            //    }
            //}


        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(DataSearch.MonthReport), "制作月报", false);




        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.ImportOldData.ImportDataControl), "从旧数据库中导入数据", true);
        }

        //private void barButtonItem16_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    addNewUserControlInTabPage(typeof(hammergo.TestDLL.TestControl), "测试页面", true);
        //}

        //private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    addNewUserControlInTabPage(typeof(hammergo.TestDLL.TestControl2), "测试页面2", true);
        //}

        //private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    addNewUserControlInTabPage(typeof(hammergo.TestDLL.TestConfig), "程序配置", true);
        //}

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.AppManage.AppTypeManage), "仪器类型管理", true);
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.DataSearch.StatisticsReport), "统计报表", true);





        }

        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.Graphics.GraphicForm), "过程线", true);




        }

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.DataSearch.StressForm), "应力计算", true);




        }

        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.DataImport.ImportExcelData), "数据导入", true);



        }

        private void barButtonItem25_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.DataExport.ExportAllData), "完整导出", true);
        }

        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.DataExport.ExportMessureData), "观测数据导出", true);
        }

        private void barButtonItem27_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.DataExport.AppendExport), "三峡数据格式导出", true);
        }

        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.DataSearch.ParamsSearch), "测点参数检索", true);
        }

        private void barButtonItem29_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.DataSearch.DegreeStatistics), "测次统计", true);
        }

        private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.DataSearch.BatchSearch), "批量检索", true);
        }

        private void barButtonItem31_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Control control = addNewUserControlInTabPage(typeof(hammergo.DataManage.InputIntegrated), "批量输入", true);
            hammergo.DataManage.InputIntegrated ic = control as hammergo.DataManage.InputIntegrated;
            if (ic != null)
            {
                ic.DataObserveEvent += new EventHandler<hammergo.DataManage.TaskEventArgs>(ic_DataObserveEvent);
            }
        }

        void ic_DataObserveEvent(object sender, hammergo.DataManage.TaskEventArgs e)
        {
            Control control = addNewUserControlInTabPage(typeof(hammergo.DataManage.DataObserve), "检查输入的数据", false);
            hammergo.DataManage.DataObserve dc = control as hammergo.DataManage.DataObserve;
            if (dc != null)
            {
                dc.selectTask(e.TaskAppCollectionID, e.CurrentDate);
            }
        }

        private void barButtonItem32_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.DataManage.BatchDelete), "批量删除", true);
        }

        private void barButtonItem35_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.DataManage.BatchRemark), "批量添加、修改、删除批注", true);
        }

        private void barButtonItem34_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.DataManage.BatchChangeDate), "批量更改日期时间", true);
        }

        private void barButtonItem36_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.DataManage.DataObserve), "检查输入的数据", true);
        }

        private void navBarItem4_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            barButtonItem12_ItemClick(sender, null);
        }

        private void navBarItem5_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            barButtonItem15_ItemClick(sender, null);
        }

        private void navBarItem6_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            barButtonItem21_ItemClick(sender, null);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.TestDLL.TestConfig), "程序参数配置", true);
        }

        private void barButtonItem4_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.TestDLL.UserManager), "用户管理", true);
        }

        private void barButtonItem12_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.TestDLL.ChangePassword), "修改密码", true);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            barButtonItem8_ItemClick(null, null);
        }

        private void barButtonItem4_ItemClick_2(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //hammergo.ContourMap.InvokeSurfer ms = new hammergo.ContourMap.InvokeSurfer();
            //ms.test(Application.StartupPath);
            
        }

        private void barButtonItem19_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.DataManage.InputManage), "输入管理", true);
        }

        private void barButtonItem31_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Windows.Forms.Form form = new hammergo.TestDLL.ShowInfo();
            form.ShowDialog();
        }

        private void barButtonItem32_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.DataManage.ReCalculateForm), "重新计算测点成果量", true);
        }

        private void barButtonItem37_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void barButtonItem38_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            addNewUserControlInTabPage(typeof(hammergo.DataSearch.SearchSameDate), "合并数据", true);
        }



    }
}
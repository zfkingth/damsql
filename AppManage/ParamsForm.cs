using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using hammergo.Model;
using hammergo.GlobalConfig;
using DevExpress.XtraGrid.Views.Grid;
using hammergo.Tracking;

namespace hammergo.AppManage
{
    public partial class ParamsForm : DevExpress.XtraEditors.XtraForm
    {
        string appName = "";
        Apparatus app = null;
        public ParamsForm(string appName)
        {
            InitializeComponent();
            this.appName = appName;
        }

        hammergo.BLL.ConstantParamBLL constBLL = new hammergo.BLL.ConstantParamBLL();
        hammergo.BLL.MessureParamBLL mesBLL = new hammergo.BLL.MessureParamBLL();
        hammergo.BLL.CalculateParamBLL calcBLL = new hammergo.BLL.CalculateParamBLL();
        hammergo.BLL.ApparatusBLL appBLL = new hammergo.BLL.ApparatusBLL();
        TrackedList<ConstantParam> listConst = null;
        TrackedList<MessureParam> listMes = null;
        TrackedList<CalculateParam> listCalc = null;

        private void ParamsForm_Load(object sender, EventArgs e)
        {
            this.listConst = constBLL.GetListByappName(appName);
            this.listMes = mesBLL.GetListByappName(appName);
            this.listCalc = calcBLL.GetListByappName(appName);
            app = appBLL.GetModelBy_AppName(appName);


            constantParamBindingSource.DataSource = listConst;
            messureParamBindingSource.DataSource = listMes;
            calculateParamBindingSource.DataSource = listCalc;


            this.Text = appName + "的参数列表";

            InitComboBox();

            List<string> list = new List<string>();


            //设置权限
            if (GlobalConfig.PubConstant.userPower != GlobalConfig.PubConstant.power_admin)
            {
               btnSave.Enabled = false;
            }

        }

        private void InitComboBox()
        {
            foreach (hammergo.GlobalConfig.ParamInfo pi in PubConstant.ConfigData.ConstParamsList)
            {
                this.repositoryItemComboBox1.Items.Add(pi.Name);
            }

            foreach (hammergo.GlobalConfig.ParamInfo pi in PubConstant.ConfigData.DefaultParamsList)
            {
                this.repositoryItemComboBox2.Items.Add(pi.Name);
                this.repositoryItemComboBox3.Items.Add(pi.Name);
            }

        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {


            setChangeValue(PubConstant.ConfigData.ConstParamsList, sender, e);
        }

        private void gridView2_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            setChangeValue(PubConstant.ConfigData.DefaultParamsList, sender, e);
        }

        private void gridView3_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            setChangeValue(PubConstant.ConfigData.DefaultParamsList, sender, e);
        }

        private void setChangeValue(List<ParamInfo> paramList, object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "ParamSymbol")
            {
                ParamInfo info = paramList.Find(delegate(ParamInfo pi) { return pi.Name == e.Value.ToString(); });

                if (info != null)
                {
                    DevExpress.XtraGrid.Views.Grid.GridView gv = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                    gv.SetRowCellValue(e.RowHandle, "ParamSymbol", info.CalcSymbol);
                    gv.SetRowCellValue(e.RowHandle, "UnitSymbol", info.UnitSymbol);
                    gv.SetRowCellValue(e.RowHandle, "PrecisionNum", info.Precision);
                }


            }
        }

        bool allowClosed = true;
        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {

                appBLL.ValidateParams(app.CalculateName, listConst, listMes, listCalc);


              

                using (System.Data.IDbConnection connection = hammergo.ConnectionPool.Pool.GetOpenConnection())
                {
                    //connection.Open();

                    // Start a local transaction.
                    System.Data.IDbTransaction trans = connection.BeginTransaction();


                    try
                    {
                        constBLL.UpdateList(listConst, trans);
                        mesBLL.UpdateList(listMes, trans);
                        calcBLL.UpdateList(listCalc, trans);
                        trans.Commit();


                        allowClosed = true;
                    }
                    catch (Exception ex)
                    {


                        // Attempt to roll back the transaction.

                        trans.Rollback();
                        throw ex;
                    }
                }

            }
            catch (Exception ex)
            {


                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                allowClosed = false;

            }

        }

        private void ParamsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (allowClosed == false)
            {
                allowClosed = true;
                e.Cancel = true;
            }
        }

        //int nextMesID = 0;

        //private int getNextMesID()
        //{
        //    if (nextMesID == 0)
        //    {

        //        int? maxID = mesBLL.getMaxMessureParamID();
        //        nextMesID = maxID.HasValue ? maxID.Value + 1 : 0;
        //    }
        //    return nextMesID++;


        //}

        //int nextCalcID = 0;
        //private int getNextCalcID()
        //{
        //    if (nextCalcID == 0)
        //    {
        //        int? maxID = calcBLL.getMaxCalculateParamID();
        //        nextCalcID = maxID.HasValue ? maxID.Value + 1 : 0;
        //    }

        //    return nextCalcID++;
        //}

        //int nextConstID = 0;
        //private int getNextConstID()
        //{
        //    if (nextConstID == 0)
        //    {
        //        int? maxID = constBLL.getMaxConstantParamID();
        //        nextConstID = maxID.HasValue ? maxID.Value + 1 : 0;
        //    }

        //    return nextConstID++;
        //}

        private void 新建参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConstantParam lastParam = null;
            if (constantParamBindingSource.Count > 0)
            {
                lastParam = constantParamBindingSource[constantParamBindingSource.Count - 1] as ConstantParam;
            }
            ConstantParam cp = new ConstantParam();
            cp.ConstantParamID = Guid.NewGuid();
            cp.AppName = appName;
            cp.ParamName = "参数p";
            cp.ParamSymbol = "p";
            cp.Order = 0;
            cp.PrecisionNum = 2;

            foreach (ConstantParam item in constantParamBindingSource)
            {
                if (item.Order.HasValue)
                {
                    if (item.Order.Value > cp.Order)
                    {
                        cp.Order = item.Order.Value;
                    }
                }
            }
            cp.Order += 1;
            cp.ParamSymbol += cp.Order.Value.ToString();
            cp.ParamName += cp.Order.Value.ToString();

            cp.Val = 0;




            constantParamBindingSource.Add(cp);

            //gridView1.ClearSelection();
            //gridView1.SelectRow(constantParamBindingSource.Count - 1);
            //gridView1.FocusedRowHandle = constantParamBindingSource.Count - 1;

            Utility.Utility.selectRow(cp, gridView1);


        }



        private void 删除参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] indexes = gridView1.GetSelectedRows();

            if (indexes.Length != 0)
            {
                ConstantParam cp = gridView1.GetRow(indexes[0]) as ConstantParam;
                if (XtraMessageBox.Show(this, "删除参数 " + cp.ParamName, "删除参数!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
                           ) == DialogResult.Yes)
                {


                    constantParamBindingSource.Remove(cp);



                }
            }
        }


        //第二个gridview的菜单处理


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            MessureParam lastParam = null;
            if (messureParamBindingSource.Count > 0)
            {
                lastParam = messureParamBindingSource[messureParamBindingSource.Count - 1] as MessureParam;
            }
            MessureParam cp = new MessureParam();
            cp.MessureParamID = Guid.NewGuid();
            cp.AppName = appName;
            cp.ParamName = "参数m";
            cp.ParamSymbol = "m";
            cp.Order = 0;
            cp.PrecisionNum = 2;



            foreach (MessureParam item in messureParamBindingSource)
            {
                if (item.Order.HasValue)
                {
                    if (item.Order.Value > cp.Order)
                    {
                        cp.Order = item.Order.Value;
                    }
                }
            }
            cp.Order += 1;
            cp.ParamSymbol += cp.Order.Value.ToString();
            cp.ParamName += cp.Order.Value.ToString();


            messureParamBindingSource.Add(cp);

            //gridView2.ClearSelection();
            //gridView2.SelectRow(messureParamBindingSource.Count - 1);
            //gridView2.FocusedRowHandle = messureParamBindingSource.Count - 1;
            Utility.Utility.selectRow(cp, gridView2);

        }



        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int[] indexes = gridView2.GetSelectedRows();

            if (indexes.Length != 0)
            {
                MessureParam cp = gridView2.GetRow(indexes[0]) as MessureParam;
                if (XtraMessageBox.Show(this, "删除参数 " + cp.ParamName, "删除参数!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
                           ) == DialogResult.Yes)
                {


                    messureParamBindingSource.Remove(cp);



                }
            }
        }



        //第三个GridView的菜单
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CalculateParam lastParam = null;
            if (calculateParamBindingSource.Count > 0)
            {
                lastParam = calculateParamBindingSource[calculateParamBindingSource.Count - 1] as CalculateParam;
            }
            CalculateParam cp = new CalculateParam();
            cp.CalculateParamID = Guid.NewGuid();
            cp.AppName = appName;
            cp.ParamName = "参数c";
            cp.ParamSymbol = "c";
            cp.Order = 0;
            cp.PrecisionNum = 2;



            foreach (CalculateParam item in calculateParamBindingSource)
            {
                if (item.Order.HasValue)
                {
                    if (item.Order.Value > cp.Order)
                    {
                        cp.Order = item.Order.Value;
                    }
                }
            }
            cp.Order += 1;
            cp.ParamSymbol += cp.Order.Value.ToString();
            cp.ParamName += cp.Order.Value.ToString();


            calculateParamBindingSource.Add(cp);

            //gridView3.ClearSelection();
            //gridView3.SelectRow(calculateParamBindingSource.Count - 1);
            //gridView3.FocusedRowHandle = calculateParamBindingSource.Count - 1;
            Utility.Utility.selectRow(cp, gridView3);
        }




        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            int[] indexes = gridView3.GetSelectedRows();

            if (indexes.Length != 0)
            {
                CalculateParam cp = gridView3.GetRow(indexes[0]) as CalculateParam;
                if (XtraMessageBox.Show(this, "删除参数 " + cp.ParamName, "删除参数!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
                           ) == DialogResult.Yes)
                {


                    calculateParamBindingSource.Remove(cp);



                }
            }
        }

    }
}
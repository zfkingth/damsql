using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace hammergo.AppManage
{
    public partial class AppTypeManage : DevExpress.XtraEditors.XtraUserControl
    {
        public AppTypeManage()
        {
            InitializeComponent();
        }

        hammergo.BLL.ApparatusBLL appBLL = new hammergo.BLL.ApparatusBLL();
        hammergo.BLL.ApparatusTypeBLL typeBLL = new hammergo.BLL.ApparatusTypeBLL();



        private void AppTypeManage_Load(object sender, EventArgs e)
        {
            apparatusTypeBindingSource.DataSource = typeBLL.GetList();
        }

        private void ɾ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show(this, "ȷ��ɾ����?", "ɾ����������!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
                                       ) == DialogResult.Yes)
                {


                    if (gridView1.SelectedRowsCount != 0)
                    {

                        int[] indexes = gridView1.GetSelectedRows();
                        List<hammergo.Model.ApparatusType> delTypes = new List<hammergo.Model.ApparatusType>(4);
                        for (int i = 0; i < indexes.Length; i++)
                        {
                            hammergo.Model.ApparatusType appType = gridView1.GetRow(indexes[i]) as hammergo.Model.ApparatusType;

                            if (appBLL.GetCountByAppTypeID(appType.ApparatusTypeID.Value) != 0)
                            {
                                throw new Exception(string.Format("������������������:'{0}' ����,�޷�ɾ��!", appType.TypeName));
                            }

                            delTypes.Add(appType);
                        }

                        foreach (hammergo.Model.ApparatusType type in delTypes)
                        {
                            apparatusTypeBindingSource.Remove(type);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void �½�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string newTypeName = "������";

            hammergo.Tracking.TrackedList<hammergo.Model.ApparatusType> list = apparatusTypeBindingSource.DataSource as hammergo.Tracking.TrackedList<hammergo.Model.ApparatusType>;

            if (list.Exists(delegate(hammergo.Model.ApparatusType item) { return item.TypeName == newTypeName; }) == false)
            {



                hammergo.Model.ApparatusType newType = new hammergo.Model.ApparatusType();
                newType.TypeName = newTypeName;
                newType.ApparatusTypeID = Guid.NewGuid();//getNextID();
                apparatusTypeBindingSource.Add(newType);

                Utility.Utility.selectRow(newType, gridView1);

                //for (int i = 0; i < gridView1.RowCount; i++)
                //{
                //    hammergo.Model.ApparatusType selItem = gridView1.GetRow(i) as hammergo.Model.ApparatusType;
                //    if (newType == selItem)
                //    {
                //        gridView1.SelectRow(i);
                //        break;
                //    }
                //}
            }
            else
            {
                XtraMessageBox.Show(this, newTypeName + " �Ѵ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }


        }

        //int nextID = 0;
        //private int getNextID()
        //{
        //    if (nextID == 0)
        //    {
        //        int? maxID = typeBLL.getMaxApparatusTypeID();
        //        nextID = maxID.HasValue ? maxID.Value + 1 : 0;
        //    }

        //    return nextID++;
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                hammergo.Tracking.TrackedList<hammergo.Model.ApparatusType> list = apparatusTypeBindingSource.DataSource as hammergo.Tracking.TrackedList<hammergo.Model.ApparatusType>;

                typeBLL.UpdateList(list);
                XtraMessageBox.Show(this,"�ɹ�����", "��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AppTypeManage_Load(this, null);
        }

    }
}

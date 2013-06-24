using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using hammergo.GlobalConfig;

namespace hammergo.CommonControl
{
    public partial class InputDateForm : DevExpress.XtraEditors.XtraForm
    {
        bool useNowTime = false;
        public DateTime? []dates = null;
        public InputDateForm(  DateTime? []dates, bool useNowTime)
        {
            InitializeComponent();
            this.useNowTime = useNowTime;
            this.dates = dates;
        }

        private void InputDateForm_Load(object sender, EventArgs e)
        {

            dateEdit1.EditValue = DateTime.Parse(System.DateTime.Now.ToString(PubConstant.customString));
        }
    

        private void bntSure_Click(object sender, EventArgs e)
        {


            if (dateEdit1.EditValue!=null)
            {
                dates[0] = dateEdit1.DateTime;
            }
            else
            {
                dates[0] = null;
            }

            
        }

        private void dateEdit1_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            if (e.Value != null)
            {
                DateTime i = (DateTime)e.Value;
                e.Value = new DateTime(i.Year, i.Month, i.Day, i.Hour, i.Minute, 0);
            }
        }
    }
}
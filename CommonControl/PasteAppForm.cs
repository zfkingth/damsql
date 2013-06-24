using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace hammergo.CommonControl
{
    public partial class PasteAppForm : DevExpress.XtraEditors.XtraForm
    {
        List<string> slist = null;

        public PasteAppForm(List<string> list,string info)
        {
            InitializeComponent();

            this.slist = list;
            this.Text = info;
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string clipString = (string)System.Windows.Forms.Clipboard.GetDataObject().GetData(typeof(string));

            if (clipString == null || clipString.Length == 0) return;


            string[] sns = clipString.Split(new char[] { '\n', '\r', '\t' });

            listBoxControl1.Items.Clear();
            slist.Clear();

            for (int i = 0; i < sns.Length; i++)
            {

                if (sns[i].Length != 0)
                {
                    listBoxControl1.Items.Add(sns[i]);
                    slist.Add(sns[i]);
                }

            }

        }

        private void 清除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBoxControl1.Items.Clear();
            slist.Clear();
        }
    }
}
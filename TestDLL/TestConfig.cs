using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace hammergo.TestDLL
{
    public partial class TestConfig : DevExpress.XtraEditors.XtraUserControl
    {
        public TestConfig()
        {
            InitializeComponent();
        }

        private void TestConfig_Load(object sender, EventArgs e)
        {
            this.propertyGridControl1.SelectedObject = hammergo.GlobalConfig.PubConstant.ConfigData;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hammergo.GlobalConfig.PubConstant.updateConfigData();
        }
    }
}

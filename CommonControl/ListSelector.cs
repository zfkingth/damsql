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
    public partial class ListSelector : DevExpress.XtraEditors.XtraForm
    {
        public ListSelector()
        {
            InitializeComponent();
        }

        public ListSelector(string text)
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();
		
			this.Text=text;

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

        public static List<string> nameList = null;

        public static string selectedName = "";

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            selectedName = comboBoxEdit1.SelectedItem.ToString();
            this.Close();
         
        }

        private void ListSelector_Load(object sender, EventArgs e)
        {

            foreach (string colName in nameList)
            {
                comboBoxEdit1.Properties.Items.Add(colName);

            }
            comboBoxEdit1.Properties.Items.Add("��");

            comboBoxEdit1.SelectedItem = comboBoxEdit1.Properties.Items[0];
        }

    }
}
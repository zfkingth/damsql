using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace hammergo.TestDLL
{
	/// <summary>
	/// ShowInfo ��ժҪ˵����
	/// </summary>
	public class ShowInfo : System.Windows.Forms.Form
	{
        private Label label1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ShowInfo()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			this.ClientSize=this.BackgroundImage.Size;
			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowInfo));
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("����", 11F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(26, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // ShowInfo
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(432, 288);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("����", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ShowInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "����";
            this.Click += new System.EventHandler(this.ShowInfo_Click);
            this.Load += new System.EventHandler(this.ShowInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void ShowInfo_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

        private void ShowInfo_Load(object sender, EventArgs e)
        {
            label1.Text ="������Ȩ��ֹ����Ϊ:"+ RegisterInput.expireDate.ToLongDateString();
        }
	}
}

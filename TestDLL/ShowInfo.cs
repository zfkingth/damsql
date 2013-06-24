using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace hammergo.TestDLL
{
	/// <summary>
	/// ShowInfo 的摘要说明。
	/// </summary>
	public class ShowInfo : System.Windows.Forms.Form
	{
        private Label label1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ShowInfo()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			this.ClientSize=this.BackgroundImage.Size;
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
            this.label1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
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
            this.Font = new System.Drawing.Font("宋体", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ShowInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关于";
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
            label1.Text ="您的授权截止日期为:"+ RegisterInput.expireDate.ToLongDateString();
        }
	}
}

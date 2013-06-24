using System;
using C1.Win.C1Chart;

namespace hammergo.Graphics
{
	/// <summary>
	/// GraphicProperty 的摘要说明。
	/// </summary>
	public class GraphicProperty
	{
		/// <summary>
		/// 
		/// </summary>
		public C1Chart chart=null;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="chart"></param>
		public GraphicProperty(C1Chart chart)
		{
			this.chart=chart;
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public double 主轴最大值
		{

			get
			{
				return chart.ChartArea.AxisY.Max;
			}
			set
			{

				chart.ChartArea.AxisY.Max=value;
			}
		}

		public double 主轴最小值
		{
			get
			{
				return chart.ChartArea.AxisY.Min;
			}
			set
			{

				chart.ChartArea.AxisY.Min=value;
			}

		}


		public double 主轴间距
		{
			get
			{
				return chart.ChartArea.AxisY.UnitMajor;
			}
			set
			{
				chart.ChartArea.AxisY.UnitMajor=value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public double 副轴间距
		{
			get
			{
				return chart.ChartArea.AxisY2.UnitMajor;
			}
			set
			{
				chart.ChartArea.AxisY2.UnitMajor=value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string 主轴标注
		{
			
			get
			{
				if( chart.ChartLabels.LabelsCollection["y1"]!=null)
				{
					return chart.ChartLabels.LabelsCollection["y1"].Text;
				}

				return "";
			}
			set
			{
				if( chart.ChartLabels.LabelsCollection["y1"]!=null)
				{
					 chart.ChartLabels.LabelsCollection["y1"].Text=value;
				}
			}
		}

		public string 副轴标注
		{
			get
			{
				if( chart.ChartLabels.LabelsCollection["y2"]!=null)
				{
					return chart.ChartLabels.LabelsCollection["y2"].Text;
				}
				return "";
			}
			set
			{
				if( chart.ChartLabels.LabelsCollection["y2"]!=null)
				{
					chart.ChartLabels.LabelsCollection["y2"].Text=value;
				}
			}
		}


		public double 副轴最大值
		{

			get
			{
				return chart.ChartArea.AxisY2.Max;
			}
			set
			{

				chart.ChartArea.AxisY2.Max=value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public double 副轴最小值
		{
			get
			{
				return chart.ChartArea.AxisY2.Min;
			}
			set
			{

				chart.ChartArea.AxisY2.Min=value;
			}

		}

		/// <summary>
		/// 
		/// </summary>
		public bool 显示图例
		{
			get
			{
				return chart.Legend.Visible;
			}
			set
			{
				chart.Legend.Visible=value;
			}
		}

		public string 文本
		{
			get
			{
				if(chart.ChartLabels["info"]!=null)
				{
					return chart.ChartLabels["info"].Text;
				}
				return "";
			}
			set
			{
				if(chart.ChartLabels["info"]!=null)
				{
					chart.ChartLabels["info"].Text=value;
				}

			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string X轴格式
		{
			get
			{
				return chart.ChartArea.AxisX.AnnoFormatString;
			}
			set
			{
				chart.ChartArea.AxisX.AnnoFormatString=value;
			}
		}


		public System.Drawing.Font 文本字体
		{
			get
			{
				if( chart.ChartLabels["info"]!=null)
				{
					return  chart.ChartLabels["info"].Style.Font;
				}
				return null;
			}
			set
			{
				if( chart.ChartLabels["info"]!=null)
				{
					 chart.ChartLabels["info"].Style.Font=value;
				}

			}
		}



	}
}

using System;
using C1.Win.C1Chart;

namespace hammergo.Graphics
{
	/// <summary>
	/// GraphicProperty ��ժҪ˵����
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
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public double �������ֵ
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

		public double ������Сֵ
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


		public double ������
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
		public double ������
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
		public string �����ע
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

		public string �����ע
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


		public double �������ֵ
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
		public double ������Сֵ
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
		public bool ��ʾͼ��
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

		public string �ı�
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
		public string X���ʽ
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


		public System.Drawing.Font �ı�����
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

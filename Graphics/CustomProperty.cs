using System;
using System.Collections.Generic;
using System.Text;
using C1.Win.C1Chart;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace hammergo.Graphics
{
    class CustomProperty
    {
        /// <summary>
        /// 
        /// </summary>
        public ChartDataSeries cds;

        /// <summary>
        /// 
        /// </summary>
        public DataPointProperty dpp;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cds"></param>
        public CustomProperty(ChartDataSeries cds)
        {
            this.cds = cds;
            dpp = new DataPointProperty(cds.SymbolStyle);
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        /// <summary>
        /// ������������ɫ
        /// </summary>
        [
            Description("������������ɫ.")
        ]

        public Color ������ɫ
        {
            get
            {
                return cds.LineStyle.Color;
            }
            set
            {
                cds.LineStyle.Color = value;
            }
        }



        /// <summary>
        /// ������������ʽ
        /// </summary>
        /// 
        [
        Description("������������ʽ.")
        ]
        public LinePatternEnum ������ʽ
        {
            get
            {
                return cds.LineStyle.Pattern;
            }
            set
            {
                cds.LineStyle.Pattern = value;
            }
        }

        /// <summary>
        /// ���������Ĵ�ϸ
        /// </summary>
        /// 
        [
        Description("���������Ĵ�ϸ.")
        ]
        public int ������ϸ
        {
            get
            {
                return cds.LineStyle.Thickness;
            }
            set
            {
                cds.LineStyle.Thickness = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [
        Description("��������������.")
        ]
        public string ��������
        {
            get
            {
                return cds.Label;
            }
            set
            {
                cds.Label = value;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        [TypeConverterAttribute(typeof(DataPointPropertyConverter)),
        DescriptionAttribute("չ���Բ鿴�����ʽ��")]
        public DataPointProperty �����ʽ
        {
            get
            {
                return dpp;
            }
            set
            {
                dpp = value;
            }
        }
    }


    /// <summary>
    /// DataPointProperty ��ժҪ˵����
    /// </summary>
    public class DataPointProperty
    {
        /// <summary>
        /// 
        /// </summary>
        public ChartSymbolStyle css;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="css"></param>
        public DataPointProperty(ChartSymbolStyle css)
        {
            this.css = css;
        }



        /// <summary>
        /// ���õ��
        /// </summary>
        public SymbolShapeEnum ����״
        {
            get
            {
                return css.Shape;
            }
            set
            {
                css.Shape = value;
            }
        }



        /// <summary>
        /// ���õ����ɫ
        /// </summary>
        public Color ����ɫ
        {
            get
            {
                return css.Color;
            }
            set
            {
                css.Color = value;
            }
        }

        /// <summary>
        /// ���õ�Ĵ�С
        /// </summary>
        public int ���С
        {
            get
            {
                return css.Size;
            }
            set
            {
                css.Size = value;
            }
        }

        /// <summary>
        /// ���õ����Χ����ɫ
        /// </summary>
        public Color ����Χ����ɫ
        {
            get
            {
                return css.OutlineColor;
            }
            set
            {
                css.OutlineColor = value;
            }
        }

        /// <summary>
        /// ���õ���Χ�߿��
        /// </summary>
        public int ����Χ�߿��
        {
            get
            {
                return css.OutlineWidth;
            }
            set
            {
                css.OutlineWidth = value;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DataPointPropertyConverter : ExpandableObjectConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context,
            System.Type destinationType)
        {
            if (destinationType == typeof(DataPointProperty))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>

        public override object ConvertTo(ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            System.Type destinationType)
        {
            if (destinationType == typeof(System.String) &&
                value is DataPointProperty)
            {

                DataPointProperty so = (DataPointProperty)value;

                return "��״:" + so.����״.ToString() +
                    ",��ɫ: " + so.����ɫ +
                    ",��С: " + so.���С +
                    ",��Χ����ɫ: " + so.����Χ����ɫ +
                    ",��Χ�߿��: " + so.����Χ�߿��;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context,
            System.Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context,
            CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    string s = (string)value;

                    ChartSymbolStyle css = new ChartSymbolStyle();
                    DataPointProperty so = new DataPointProperty(css);

                    int colon = s.IndexOf(":");
                    string substring = s.Substring(colon + 1);

                    int comma = substring.IndexOf(",");

                    so.����״ = (SymbolShapeEnum)Enum.Parse(typeof(SymbolShapeEnum), substring.Substring(0, comma - 1), false);

                    colon = substring.IndexOf(":");
                    substring = substring.Substring(colon + 1);
                    comma = substring.IndexOf(",");

                    so.����ɫ = (Color)Enum.Parse(typeof(Color), substring.Substring(0, comma - 1), false);

                    colon = substring.IndexOf(":");
                    substring = substring.Substring(colon + 1);
                    comma = substring.IndexOf(",");

                    so.���С = int.Parse(substring.Substring(0, comma - 1));

                    colon = substring.IndexOf(":");
                    substring = substring.Substring(colon + 1);
                    comma = substring.IndexOf(",");

                    so.����Χ����ɫ = (Color)Enum.Parse(typeof(Color), substring.Substring(0, comma - 1), false);


                    colon = substring.IndexOf(":");
                    substring = substring.Substring(colon + 1);

                    so.����Χ�߿�� = int.Parse(substring);







                    return so;

                }
                catch
                {
                    throw new ArgumentException(
                        "�޷�����" + (string)value +
                        "��ת��Ϊ DataPointProperty ����");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }

}

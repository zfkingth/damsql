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
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 设置线条的颜色
        /// </summary>
        [
            Description("设置线条的颜色.")
        ]

        public Color 线条颜色
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
        /// 设置线条的样式
        /// </summary>
        /// 
        [
        Description("设置线条的样式.")
        ]
        public LinePatternEnum 线条样式
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
        /// 设置线条的粗细
        /// </summary>
        /// 
        [
        Description("设置线条的粗细.")
        ]
        public int 线条粗细
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
        Description("设置线条的名称.")
        ]
        public string 线条名称
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
        DescriptionAttribute("展开以查看点的样式。")]
        public DataPointProperty 点的样式
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
    /// DataPointProperty 的摘要说明。
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
        /// 设置点的
        /// </summary>
        public SymbolShapeEnum 点形状
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
        /// 设置点的颜色
        /// </summary>
        public Color 点颜色
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
        /// 设置点的大小
        /// </summary>
        public int 点大小
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
        /// 设置点的外围线颜色
        /// </summary>
        public Color 点外围线颜色
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
        /// 设置点外围线宽度
        /// </summary>
        public int 点外围线宽度
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

                return "形状:" + so.点形状.ToString() +
                    ",颜色: " + so.点颜色 +
                    ",大小: " + so.点大小 +
                    ",外围线颜色: " + so.点外围线颜色 +
                    ",外围线宽度: " + so.点外围线宽度;
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

                    so.点形状 = (SymbolShapeEnum)Enum.Parse(typeof(SymbolShapeEnum), substring.Substring(0, comma - 1), false);

                    colon = substring.IndexOf(":");
                    substring = substring.Substring(colon + 1);
                    comma = substring.IndexOf(",");

                    so.点颜色 = (Color)Enum.Parse(typeof(Color), substring.Substring(0, comma - 1), false);

                    colon = substring.IndexOf(":");
                    substring = substring.Substring(colon + 1);
                    comma = substring.IndexOf(",");

                    so.点大小 = int.Parse(substring.Substring(0, comma - 1));

                    colon = substring.IndexOf(":");
                    substring = substring.Substring(colon + 1);
                    comma = substring.IndexOf(",");

                    so.点外围线颜色 = (Color)Enum.Parse(typeof(Color), substring.Substring(0, comma - 1), false);


                    colon = substring.IndexOf(":");
                    substring = substring.Substring(colon + 1);

                    so.点外围线宽度 = int.Parse(substring);







                    return so;

                }
                catch
                {
                    throw new ArgumentException(
                        "无法将“" + (string)value +
                        "”转换为 DataPointProperty 类型");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace hammergo.ExportLib
{
    public class NumericString
    {
        string selectedNameFormat = "";
        string resultNameFormat = "";
        string timeFormat = hammergo.GlobalConfig.PubConstant.shortString;

        public NumericString(List<string> fetchExtreamNameList, List<string> nameList, string selectedName, string timeFormat):this(timeFormat)
        {
            if (fetchExtreamNameList.Count != nameList.Count)
            {
                selectedNameFormat = getNumbericString(getPrecisionFromConfig(selectedName));
            }

            resultNameFormat = getNumbericString(getPrecisionFromConfig(fetchExtreamNameList[0].ToString()));

          
        }

        public NumericString(string timeFormat)
        {
            if (timeFormat != null)
                this.timeFormat = timeFormat;
        }

        /// <summary>
        /// 从配置表中得到精度
        /// </summary>
        /// <param name="colName">参数名称</param>
        /// <returns></returns>
        public static byte getPrecisionFromConfig(string paramName)
        {
            byte precision = 2;

           
            hammergo.GlobalConfig.ParamInfo paramInfo= hammergo.GlobalConfig.PubConstant.ConfigData.DefaultParamsList.Find(delegate(hammergo.GlobalConfig.ParamInfo item)
            {
                return item.Name == paramName;
            });


            if (paramInfo != null)
            {
                precision = paramInfo.Precision;
            }
            return precision;
        }


        /// <summary>
        /// 获得excel表示小数的格式
        /// </summary>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static string getNumbericString(int precision)
        {


            string numberFormatString = "0";
            if (precision > 0)
            {
                numberFormatString += ".";//先加一个点
                for (int p = 0; p < precision; p++)
                {
                    numberFormatString += "0";
                }

            }

            return numberFormatString;
        }

        /// <summary>
        /// 伴随值在excel中的数据格式
        /// </summary>
        public string SelectedNameFormat
        {
            get
            {
                return selectedNameFormat;
            }
        }

        /// <summary>
        /// 成果在excel中的数据格式
        /// </summary>
        public string ResultNameFormat
        {
            get
            {
                return resultNameFormat;
            }
        }

        /// <summary>
        /// 时间在excel中的格式
        /// </summary>
        public string TimeFormat
        {
            get
            {
                return timeFormat;
            }
        }
    }
}

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
        /// �����ñ��еõ�����
        /// </summary>
        /// <param name="colName">��������</param>
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
        /// ���excel��ʾС���ĸ�ʽ
        /// </summary>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static string getNumbericString(int precision)
        {


            string numberFormatString = "0";
            if (precision > 0)
            {
                numberFormatString += ".";//�ȼ�һ����
                for (int p = 0; p < precision; p++)
                {
                    numberFormatString += "0";
                }

            }

            return numberFormatString;
        }

        /// <summary>
        /// ����ֵ��excel�е����ݸ�ʽ
        /// </summary>
        public string SelectedNameFormat
        {
            get
            {
                return selectedNameFormat;
            }
        }

        /// <summary>
        /// �ɹ���excel�е����ݸ�ʽ
        /// </summary>
        public string ResultNameFormat
        {
            get
            {
                return resultNameFormat;
            }
        }

        /// <summary>
        /// ʱ����excel�еĸ�ʽ
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

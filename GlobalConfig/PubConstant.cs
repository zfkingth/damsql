using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace hammergo.GlobalConfig
{

    public class PubConstant
    {
        /// <summary>
        /// ��ȡ�����ַ���
        /// </summary>
        public static string ConnectionString
        {
            get
            {
               
                return ConfigData.ConnectionString;
            }
            set
            {
                ConfigData.ConnectionString = value;
            }
        }

        public const string customString = "yyyy-MM-dd HH:mm";//�Զ������ڸ�ʽ�ַ���

        public const string shortString = "yyyy-MM-dd";
        /// <summary>
        /// ��ע�е�����
        /// </summary>
        public const string remarkColumnName = "��ע";
        /// <summary>
        /// ʱ���е�����
        /// </summary>
        public const string timeColumnName = "ʱ��";

        /// <summary>
        /// �����������
        /// </summary>
        public const string appColumnName = "�����";

        /// <summary>
        /// �������������ڱ�TaskType�е�TypeName
        /// </summary>
        public  const  string inputTaskName = "��������";


        /// <summary>
        /// ���ݼ��������ڱ�TaskType�е�TypeName
        /// </summary>
        public const string searchTaskName = "��������";


        /// <summary>
        /// �û�Ȩ��ֵ��ֵԽ��Ȩ��Խ��
        /// </summary>
        public static byte userPower = 0;

        //��ǰ��¼���û�����
        public static string userName = "";

        /// <summary>
        /// ����Ա��Ӧ��Ȩ�ޱ���
        /// </summary>
        public const byte power_admin = 255;

        /// <summary>
        /// ��ͨ�û���Ӧ��Ȩ�ޱ���
        /// </summary>
        public const byte power_user = 2;


        /// <summary>
        /// �ÿͶ�Ӧ��Ȩ�ޱ���
        /// </summary>
        public const byte power_guest = 1;

        ///// <summary>
        ///// �õ�web.config������������ݿ������ַ�����
        ///// </summary>
        ///// <param name="configName"></param>
        ///// <returns></returns>
        //public static string GetConnectionString(string configName)
        //{
        //    string connectionString = ConfigurationManager.AppSettings[configName];
        //    string ConStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
        //    if (ConStringEncrypt == "true")
        //    {
        //        connectionString = DESEncrypt.Decrypt(connectionString);
        //    }
        //    return connectionString;
        //}

        //private static DataSet configDS =null;

        //private static readonly string configPath = Application.StartupPath + "\\Resources\\config.xml";


        private const string GlobalFileName = "GlobalConfigData.xml";



        private static GlobalConfigData configData = null;

        public static GlobalConfigData ConfigData
        {
            get
            {
                if (configData == null)
                {
                    if (File.Exists(GlobalFileName) == false) throw new Exception("�Ҳ��������ļ�" + GlobalFileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(GlobalConfigData));
                    // A FileStream is needed to read the XML document.
                    FileStream fs = new FileStream(GlobalFileName, FileMode.Open);
                    configData = (GlobalConfigData)serializer.Deserialize(fs);
                    fs.Close();
                }
                return configData;
            }
        }

        public static void updateConfigData()
        {
            XmlSerializer ser = new XmlSerializer(typeof(GlobalConfigData));
            TextWriter writer = new StreamWriter(GlobalFileName);

            ser.Serialize(writer, ConfigData);

            writer.Close();

        }

        //public static void updateConfigDS()
        //{

        //    Config.WriteXml(configPath, XmlWriteMode.WriteSchema);

        //}

        //public static DataSet Config
        //{
        //    get
        //    {
        //        if (configDS == null)
        //        {
        //            if (File.Exists(configPath) == false) throw new Exception("�Ҳ��������ļ�" + configPath);
        //            configDS = new DataSet();
        //            configDS.ReadXml(configPath, XmlReadMode.ReadSchema);
        //        }

        //        return configDS;
        //    }
        //}




        //private static void generateConfigDataFile()
        //{

        //    XmlSerializer ser = new XmlSerializer(typeof(GlobalConfigData));
        //    TextWriter writer = new StreamWriter(GlobalFileName);

        //    GlobalConfigData obj = new GlobalConfigData();

        //    obj.DALAssemblyName = @"hammergo.OleDbDAL";
        //    obj.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase\dam3Mode.mdb;Persist Security Info=False";


        //    //������λ
        //    List<ParamInfo> listInfo = new List<ParamInfo>(10);

        //    foreach (DataRow row in configDS.Tables["������λ"].Rows)
        //    {
        //        ParamInfo pi = new ParamInfo();
        //        pi.Name = row["����"].ToString();
        //        pi.UnitSymbol = row["��λ"].ToString();
        //        pi.CalcSymbol = row["����"].ToString();
        //        pi.Precision = (byte)(int)row["����"];

        //        listInfo.Add(pi);

        //    }

        //    obj.ConstParamsList = listInfo;


        //    //Ĭ�ϵ�λ

        //    listInfo = new List<ParamInfo>(10);

        //    foreach (DataRow row in configDS.Tables["Ĭ�ϵ�λ"].Rows)
        //    {
        //        ParamInfo pi = new ParamInfo();
        //        pi.Name = row["����"].ToString();
        //        pi.UnitSymbol = row["��λ"].ToString();
        //        pi.CalcSymbol = row["����"].ToString();
        //        pi.Precision = (byte)(int)row["����"];

        //        listInfo.Add(pi);

        //    }

        //    obj.DefaultParamsList = listInfo;


        //    //����ֵ

        //    List<double> listVals = new List<double>(4);

        //    foreach (DataRow row in configDS.Tables["����ֵ"].Rows)
        //    {

        //        listVals.Add((double)row["errorValue"]);

        //    }

        //    obj.ErrorValList = listVals;



        //    List<LineStyleInfo> listLineInfos = new List<LineStyleInfo>(6);

        //    foreach (DataRow row in configDS.Tables["��ʽ��"].Rows)
        //    {
        //        LineStyleInfo lsi = new LineStyleInfo();

        //        lsi.ID = (int)row["ID"];

        //        lsi.LineStyle = (string)row["line��ʽ"];

        //        lsi.LineColor = (int)row["line��ɫ"];

        //        lsi.LineThickness = (int)row["line��ϸ"];

        //        lsi.SymbolShape = (string)row["symbol��״"];

        //        lsi.SymbolColor = (int)row["symbol��ɫ"];

        //        lsi.SymbolSize = (int)row["symbol��С"];

        //        lsi.SymbolOutColor = (int)row["symbol��Χ��ɫ"];

        //        lsi.SymbolOutThickness = (int)row["symbol��Χ��ϸ"];

        //        listLineInfos.Add(lsi);

        //    }

        //    obj.LineStyleInfoList = listLineInfos;




        //    obj.ErrorConvertString = "/";
        //    obj.NoDataConvertString = "*";





        //    ser.Serialize(writer, obj);
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using SchemaExplorer;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;


namespace PetShopCommon
{
    public class Utility
    {
        public static CodeSmith.Engine.MapCollection SystemToAlias = null;
        public static CodeSmith.Engine.MapCollection SqlNativeToSqlDbType = null;

        static Utility()
        {
            
            SystemToAlias = CodeSmith.Engine.MapCollection.Load(@"CodeSmithLib\System-CSharpAlias.csmap");
            SqlNativeToSqlDbType = CodeSmith.Engine.MapCollection.Load(@"CodeSmithLib\SqlNativeType-SqlDbType.csmap");
        }

        public static string GetCSharpAliasByDBColumn(ColumnSchema column)
        {
            string rts = SystemToAlias[column.SystemType.FullName];

          
            //Debugger.Break();
            return rts;
        }

        public static string GetCSharpAliasAllowEmpty(ColumnSchema column)
        {
            string rts = GetCSharpAliasByDBColumn(column);

            //��string�⣬������������ȫ����Model������Ϊ�ɿգ������Ͳ��ὫĬ��ֵ���ݵ������ֶ��У�Ҳ����˵�����ֶ�ȫ�������û�����
            if (rts == "string" || rts == "byte[]")
            {

            }
            else
            {
                rts += "?";
            }

            return rts;

        }

        // �����ж������е�������(��˹������)
        public static string GetPropertyNameByColumn(ColumnSchema column)
        {
            return ConvertToPascal(column.Name);
        }

        public static string ConvertToCamel(string str)
        {
            return "_" + str.Substring(0, 1).ToLower() + str.Substring(1);
        }


        public static string ConvertToPascal(string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }

        // �����ж������е��ֶ���(��������)
        public static string GetFieldNameByColumn(ColumnSchema column)
        {
            return ConvertToCamel(column.Name);
        }

        // ���ݱ����������������(ԭʼ)
        public static string GetPrimaryKeyName(TableSchema table)
        {
            if (table.PrimaryKey != null)
            {
                if (table.PrimaryKey.MemberColumns.Count == 1)
                {
                    return Utility.ConvertToCamel(table.PrimaryKey.MemberColumns[0].Name);
                }
                else
                {
                    throw new ApplicationException("��ģ��ֻ֧�ֵ����е�����");
                }
            }
            else
            {
                throw new ApplicationException("��ģ����Ҫ�������ı�");
            }
        }

        // ���ݱ�������������ֶ���(��������)
        public static string GetPrimaryKeyFieldName(TableSchema table)
        {
            return ConvertToCamel(GetPrimaryKeyName(table));
        }

        // ���ݱ������������������(��˹������)
        public static string GetPrimaryKeyPropertyName(TableSchema table)
        {
            return ConvertToPascal(GetPrimaryKeyName(table));
        }


        // �����ж������е�����
        public static string GetDataTypeByColumn(ColumnSchema column)
        {
            return GetCSharpAliasByDBColumn(column);
        }




        // ���ݱ����������������
        public static string GetPrimaryKeyType(TableSchema table)
        {
            if (table.PrimaryKey != null)
            {
                if (table.PrimaryKey.MemberColumns.Count == 1)
                {
                    return GetCSharpAliasByDBColumn(table.PrimaryKey.MemberColumns[0]);
                }
                else
                {
                    throw new ApplicationException("��ģ��ֻ֧�ֵ����е�����");
                }
            }
            else
            {
                throw new ApplicationException("��ģ����Ҫ�������ı�");
            }
        }

        public static bool allowGenMaxMin(ColumnSchema column)
        {
            bool allow = false;
            string typeString = GetCSharpAliasByDBColumn(column);
            switch (typeString)
            {
                case "string": allow = false; break;
                case "int": allow = true; break;
                case "DateTime": allow = true; break;
                case "float": allow = true; break;
                case "double": allow = true; break;
                case "bool": allow = false; break;
                case "byte": allow = true; break;
                case "byte[]": allow = false; break;
                case "Guid": allow = false; break;
                default: allow = false; break;
            }

            return allow;
        }

        public static string GetPrimaryKeyCondition(TableSchema table)
        {
            StringBuilder condition = new StringBuilder(30);
            if (table.PrimaryKey != null)
            {
                foreach (ColumnSchema column in table.PrimaryKey.MemberColumns)
                {
                    condition.Append(Utility.GetCSharpAliasByDBColumn(column)).Append(" ").Append(column.Name).Append(",");
                }

                condition.Remove(condition.Length - 1, 1);//ȥ�����һ������
            }
            else
            {
                throw new ApplicationException("��ģ����Ҫ�������ı�");
            }

            return condition.ToString();
        }

        public static string GetClassName(TableSchema table)
        {
            string tempTable;

            tempTable = table.Name;

            return ConvertToPascal(tempTable);
        }

        public static string GetModeCalssName(TableSchema table)
        {

            return "hammergo.Model." + Utility.GetClassName(table);
        }


        public static string GetSqlDbTypeByDBColumn(ColumnSchema column)
        {
            string rts = "SqlDbType."+SqlNativeToSqlDbType[column.NativeType];
            

            return rts;
        }




        public static string PassPrimaryKeyCondition(TableSchema table)
        {
            StringBuilder condition = new StringBuilder(30);
            if (table.PrimaryKey != null)
            {
                foreach (ColumnSchema column in table.PrimaryKey.MemberColumns)
                {
                    condition.Append(column.Name).Append(" ,");
                }

                condition.Remove(condition.Length - 1, 1);//ȥ�����һ������
            }
            else
            {
                throw new ApplicationException("��ģ����Ҫ�������ı�");
            }

            return condition.ToString();


        }


        public static string PassModelPrimaryKey(TableSchema table)
        {
            StringBuilder condition = new StringBuilder(30);
            if (table.PrimaryKey != null)
            {
                foreach (ColumnSchema column in table.PrimaryKey.MemberColumns)
                {
                    condition.Append("(").Append(GetCSharpAliasByDBColumn(column));
                    condition.Append(")").Append("model.").Append(ConvertToPascal(column.Name)).Append(" ,");
                }

                condition.Remove(condition.Length - 1, 1);//ȥ�����һ������
            }
            else
            {
                throw new ApplicationException("��ģ����Ҫ�������ı�");
            }

            return condition.ToString();

        }

        public void debugTable(TableSchema table)
        {

            System.Diagnostics.Debugger.Break();

            foreach (IndexSchema indexSchema in table.Indexes)
            {
                if (indexSchema.IsUnique)
                {
                    StringBuilder sb = new StringBuilder(50);
                    foreach (MemberColumnSchema mcs in indexSchema.MemberColumns)
                    {
                        ColumnSchema column = mcs.Column;
                        sb.Append(GetCSharpAliasByDBColumn(column));
                        sb.Append(" ").Append(column.Name).Append(",");
                    }
                    sb.Length -= 1;//remove last ,

                }
            }


        }

        public static void writeInfo(CodeSmith.Engine.CodeTemplateWriter response, TableSchema table)
        {
            response.WriteLine("Table name: " + table.Name);

            foreach (ColumnSchema col in table.Columns)
            {
                response.WriteLine(string.Format("column name: {0},data type {1}", col.Name, col.SystemType));
            }
            response.WriteLine();



            foreach (IndexSchema indexSchema in table.Indexes)
            {
                if (indexSchema.MemberColumns.Count == 0)
                {
                    Debugger.Break();
                }

                response.WriteLine(string.Format("index name: {0},unique: {1}", indexSchema.Name, indexSchema.IsUnique));
                response.WriteLine("member clumns count " + indexSchema.MemberColumns.Count);
                foreach (MemberColumnSchema mcs in indexSchema.MemberColumns)
                {
                    response.WriteLine(string.Format("member column name: {0}", mcs.Name));

                }

                response.WriteLine();

            }
        }

      
       
       

    }


}

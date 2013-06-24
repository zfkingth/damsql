using System;
using System.Collections.Generic;
using System.Text;

namespace hammergo.GlobalConfig
{
    public class ParamInfo
    {
        string name, unitSymbol, calcSymbol;
        byte precision = 0;


        /// <summary>
        /// ��������
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// ���ݼ�����λ�ķ���
        /// </summary>
        public string UnitSymbol
        {
            get
            {
                return unitSymbol;
            }
            set
            {
                unitSymbol = value;
            }
        }

        /// <summary>
        /// ������ֵ��ʽ�ķ���
        /// </summary>
        public string CalcSymbol
        {
            get
            {
                return calcSymbol;
            }
            set
            {
                calcSymbol = value;
            }
        }

        /// <summary>
        /// ����,��ʾ��������λС��
        /// </summary>
        public byte Precision
        {
            get
            {
                return precision;
            }
            set
            {
                precision = value;
            }
        }



        public override string ToString()
        {
            return Name;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace hammergo.Stress
{
    public class StressCalc
    {


        /// <summary>
        /// ��Ҫ��Ӧ��Ƶ�����ͨ�����캯�����ݹ���,Ӧ��Ƶ�������double����
        /// </summary>
        /// <param name="data">�����б�,Ӧ������Ϊ��Ӧ���ȥ��Ӧ���Ƶ����ɱ��εõ���Ӧ��Ӧ��</param>
        /// <param name="degree">��������</param>
        /// <param name="directionNum">Ӧ��Ƶ�����</param>
        /// <param name="dates">�۲���������</param>
        public StressCalc(double[,] data, int degree, int directionNum, DateTime[] dates)
        {


            this.mm = directionNum;//Ӧ�������

            this.degree = degree;//��������

            this.dat = dates;


            y = data;
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }



        private static int datCount = 3000;

        string[] filename0 = new string[10]; string[] filename = new string[10];

        string[] insunum = new string[10];
        string[] dat0 = new string[10];
        string[] hm0 = new string[10];
        string[] ou = new string[10];
        /// <summary>
        /// �۲���������
        /// </summary>
        DateTime[] dat = null;
        string[] dz = new string[datCount];
        string[] damm = new string[datCount];

        string[,] da = new string[datCount, 10];
        string[,] hm = new string[datCount, 10];

        double b, t1, t2, cr, ess, hh;


        int[] num = new int[10];
        /// <summary>
        /// Ӧ�������
        /// </summary>
        int mm;

        int n;
        int ctype = 0;
        int[] x0 = new int[10];

        /// <summary>
        /// �����ĳ��ʱ���������
        /// </summary>
        double[] x = new double[datCount];

        double[] xm = new double[datCount];

        double[] dzb0 = new double[10];
        double[] zdz0 = new double[10];
        double[] xdz0 = new double[10];

        double[,] dzb = new double[datCount, 10];
        double[,] zdz = new double[datCount, 10];
        double[,] xdz = new double[datCount, 10];


        double[,] y;
        double[] dzbm = new double[datCount];

        double[] temm = new double[datCount];
        double[,] tem = new double[datCount, 10];
        double[] tm = new double[datCount];
        double[] tem0 = new double[10];


        double[] a0 = new double[10];
        double[] b0 = new double[10];
        double[] f0 = new double[10];
        double[] r0 = new double[10];

        double[] eh = new double[datCount];
        double[] de = new double[datCount];
        double[,] exyz = new double[datCount, 5];
        double[] te = new double[300];
        double[] y1 = new double[datCount];
        double[] ym = new double[datCount];

        int[] iitype = new int[10];

        /// <summary>
        /// ��������
        /// </summary>
        int degree;

        public void crr()
        {
            // ���ȼ����ӳ���

            double a1, a2, a3, a4, a5;
            switch (ctype)
            {
                case 1:
                    // '��Ͽ����ڲ����������ȼ����ӳ���(1�Ż�����)
                    a1 = t2 - t1;         // '��t1ʱ�̼Ӻɺ������t2ʱ�̵ĳֺ�ʱ��
                    a2 = Math.Pow(a1, 0.2);
                    a3 = Math.Pow(t1, -0.7);

                    a4 = 81.3436 * a2 * a3;
                    a5 = 2.593622 + a4;
                    cr = a5 * 0.000001;               // '��λ:1/MPa
                    break;
                case 2:
                    // '��Ͽ��ӻ������������ȼ����ӳ���(2�Ż�����)
                    a1 = t2 - t1;
                    a2 = Math.Pow(a1, 0.215);

                    a3 = Math.Pow(t1, -0.475);
                    a4 = 34.80485 * a2 * a3;
                    a5 = 1.27921 + a4;
                    cr = a5 * 0.000001;           // '��λ:1/MPa

                    break;
                case 3:
                    //'��Ͽ����ⲿ���������ȼ����ӳ���(3�Ż�����)
                    a1 = t2 - t1;
                    a2 = Math.Pow(a1, 0.212174262);

                    a3 = Math.Pow(t1, -0.36224874);
                    a4 = 26.4983 * a2 * a3;
                    a5 = a4;
                    cr = a5 * 0.000001;        //  '��λ:1/MPa
                    break;
                case 4:
                    a1 = t2 - t1;
                    a2 = Math.Pow(a1, 0.26);
                    a3 = Math.Pow(t1, -0.75);
                    a4 = a2 * a3;
                    a5 = a4 / (0.01881321 * a4 + 0.009945777);
                    cr = a5 * 0.000001;            //  '��λ:1/MPa
                    break;
                default: break;

            }

        }


        public void ec()
        {
            //Rem ��䵯ģ�����ӳ���

            switch (ctype)
            {
                case 1:
                    // '��Ͽ����ڲ����������ȼ����ӳ���(1�Ż�����)
                    if (t1 < 90)
                    {
                        b = 3.9937 * Math.Log10(t1) + 7.184;  // '������ѹ��ģ;��λGPa
                    }
                    else
                        b = 3.9937 * Math.Log10(90) + 7.184;   // '������ѹ��ģ;��λGPa

                    break;
                case 2:
                    // '��Ͽ��ӻ������������ȼ����ӳ���(2�Ż�����)
                    if (t1 < 90)
                    {
                        b = 4.310077 * Math.Log10(t1) + 9.385645;  // '������ѹ��ģ;��λGPa
                    }
                    else
                        b = 4.310077 * Math.Log10(90) + 9.385645;   // '������ѹ��ģ;��λGPa


                    break;
                case 3:
                    //'��Ͽ����ⲿ���������ȼ����ӳ���(3�Ż�����)
                    if (t1 < 90)
                    {
                        b = 3.995229 * Math.Log10(t1) + 10.91182;  // '������ѹ��ģ;��λGPa
                    }
                    else
                        b = 3.995229 * Math.Log10(90) + 10.91182;   // '������ѹ��ģ;��λGPa

                    break;
                case 4:
                    if (t1 < 90)
                    {
                        b = 4.37525 * Math.Log10(t1) + 11.50359;  // '������ѹ��ģ;��λGPa
                    }
                    else
                        b = 4.37525 * Math.Log10(90) + 11.50359;   // '������ѹ��ģ;��λGPa



                    break;
                default: break;

            }

            b = 1000 * b;   //'��λ:MPa


        }

        /// <summary>
        /// �������ؽ��
        /// </summary>
        /// <returns></returns>
        public string handle()
        {



            if (mm == 5)//���5��Ӧ�����
            {
                //Ӧ��ƽ��

                for (int i = 0; i < degree; i++)
                {
                    double dz1 = y[i, 1] + y[i, 3] - y[i, 2] - y[i, 4];

                    y[i, 1] = y[i, 1] - dz1 / 4;
                    y[i, 3] = y[i, 3] - dz1 / 4;
                    y[i, 2] = y[i, 2] + dz1 / 4;
                    y[i, 4] = y[i, 4] + dz1 / 4;
                }
            }
            else if (mm == 7)//���7��Ӧ�����
            {
                //Ӧ��ƽ��

                for (int i = 0; i < degree; i++)
                {
                    double dz1 = y[i, 1] + y[i, 3] - y[i, 2] - y[i, 4];
                    double dz2 = y[i, 1] + y[i, 5] - y[i, 6] - y[i, 7];
                    y[i, 1] = y[i, 1] - (dz1 + dz2) / 2;
                    y[i, 3] = y[i, 3] - (dz1 + dz2) / 2;
                    y[i, 5] = y[i, 5] - (dz1 + dz2) / 2;
                    y[i, 2] = y[i, 2] + dz1 / 2 - (dz1 + dz2) / 4;
                    y[i, 4] = y[i, 4] + dz1 / 2 - (dz1 + dz2) / 4;
                    y[i, 6] = y[i, 6] + dz2 / 2 - (dz1 + dz2) / 4;
                    y[i, 7] = y[i, 7] + dz2 / 2 - (dz1 + dz2) / 4;
                }


            }

            //����������

            for (int i = 0; i < degree; i++)
            {
                DateTime time = dat[i];

                x[i] = round(time.Ticks / TimeSpan.TicksPerDay, 2);


            }

            //����y���Գ��󰶷���Ϊ��,x���Գ����η���Ϊ��,z����ֱ����Ϊ��

            double u = 1 / 6;
            double mu = 1 / ((1 + u) * (1 - 2 * u));

            switch (mm)
            {
                case 1:
                    for (int i = 0; i < degree; i++)
                    {
                        exyz[i, 1] = y[i, 1];//������Ӧ�����
                    }
                    break;
                case 2:
                    for (int i = 0; i < degree; i++)
                    {
                        exyz[i, 1] = y[i, 1];//��ƽ��Ӧ��״̬�������Ӧ�����ĵ���Ӧ��
                        exyz[i, 2] = y[i, 2];
                    }
                    break;
                case 3:
                    for (int i = 0; i < degree; i++)
                    {   //����Ӧ����鵥��Ӧ�����
                        exyz[i, 1] = mu * (1 - u) * y[i, 1] + mu * u * (y[i, 2] + y[i, 3]);
                        exyz[i, 2] = mu * (1 - u) * y[i, 2] + mu * u * (y[i, 3] + y[i, 1]);
                        exyz[i, 3] = mu * (1 - u) * y[i, 3] + mu * u * (y[i, 1] + y[i, 2]);
                    }
                    break;
                case 5:
                    for (int i = 0; i < degree; i++)
                    {
                        //����Ӧ����鵥��Ӧ�����
                        exyz[i, 1] = mu * (1 - u) * y[i, 5] + mu * u * (y[i, 1] + y[i, 3]);   //Y,���Ұ���
                        exyz[i, 2] = mu * (1 - u) * y[i, 1] + mu * u * (y[i, 3] + y[i, 5]);   //X,��������
                        exyz[i, 3] = mu * (1 - u) * y[i, 3] + mu * u * (y[i, 5] + y[i, 1]);   //Z,Ǧֱ��
                        exyz[i, 4] = y[i, 2] - y[i, 4];					//XZ,��Ӧ��
                    }
                    break;
                case 7:
                    for (int i = 0; i < degree; i++)
                    {
                        //����Ӧ���鵥��Ӧ�����
                        exyz[i, 1] = mu * (1 - u) * y[i, 1] + mu * u * (y[i, 5] + y[i, 3]);
                        exyz[i, 2] = mu * (1 - u) * y[i, 5] + mu * u * (y[i, 3] + y[i, 1]);
                        exyz[i, 3] = mu * (1 - u) * y[i, 3] + mu * u * (y[i, 1] + y[i, 5]);

                        exyz[i, 4] = y[i, 6] - y[i, 7];  //��Ӧ��
                        exyz[i, 5] = y[i, 2] - y[i, 4];
                    }
                    break;
                default: throw new Exception("Ӧ�������������!��˶Ժ����¼���");

            }

            //���ι۲�����ֵ����׼����ֵ=��Ի�׼�۲��յ�����ֵ

            for (int i = 0; i < degree; i++)
            {
                x[i] -= x[0];//Ϊ���㴦��,�Ե�һ�ι۲��������Ϊ��׼����
            }


            int mp;//�ɹ���?
            switch (mm)
            {
                case 1: mp = 1; break;
                case 2: mp = 2; break;
                case 3: mp = 3; break;
                case 4: mp = 3; break;
                case 5: mp = 4; break;
                case 6: mp = 4; break;
                default: mp = 5; break;//7��
            }

            //�������ι۲��м���Ի�׼���ڵ�����ֵ

            n = degree - 1;

            for (int i = 0; i < n; i++)
            {
                xm[i] = (x[i] + x[i + 1]) / 2;
            }

            //���η����㵥��Ӧ���ӳ���
            for (int i = 0; i < mp; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //�����е�Ӧ��ֵ

                    ym[j] = (exyz[j, i] + exyz[j + 1, i]) / 2;

                }

                for (int j = 0; j < n; j++)
                {
                    eh[j] = 0; de[j] = 0; exyz[j, i] = 0;
                }

                t1 = x[1];          //�Ӻ�����
                t2 = xm[1];         //��t1ʱ�̼Ӻɺ�������õ�t2ʱ��
                ec();                //��䵯ģ����
                crr();               //���ȼ���
                if (i > 2) b = 0.492 * b;
                ess = 1 / b + cr;   //��λӦ�������µ��ܱ���,b�ĵ�λMPa
                //cr�ĵ�λ1/MPa;ess�ĵ�λ1/MPa
                ess = 1 / ess;     //��Ч��ģ,��λ:Mpa
                de[1] = ess * (ym[1] * 0.000001 - eh[1]); //Ӧ������,��λ:MPa
                exyz[1, i] = de[1];

                for (int j = 2; j < n; j++)
                {
                    t2 = xm[j];

                    hh = 0;

                    for (int k = 1; k < j; k++)
                    {
                        t1 = x[k];
                        ec();  //��䵯ģ����
                        crr();  //

                        if (i > 2) b = 0.492 * b;

                        hh = hh + de[k] * (1 / b + cr);
                    }

                    eh[j] = hh;
                }


            }



            StringBuilder sb = new StringBuilder(1024);

            return sb.ToString();

        }



        public static double round(double v, int precision)
        {
            if (precision > 0)
            {
                double tmpv = v;
                if (v < 0)
                    tmpv = -v;
                double fMul = Math.Pow(10, precision);
                double fTmp = tmpv * fMul;
                int iTmp = (int)fTmp;

                double r = 0.0;
                if (fTmp - iTmp >= 0.5)
                {
                    r = (iTmp + 1) / fMul;
                }
                else
                {
                    r = iTmp / fMul;
                }

                return v > 0 ? r : -r;



            }

            return v;
        }

    }
}

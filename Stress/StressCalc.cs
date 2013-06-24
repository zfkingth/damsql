using System;
using System.Collections.Generic;
using System.Text;

namespace hammergo.Stress
{
    public class StressCalc
    {


        /// <summary>
        /// 需要将应变计的数据通过构造函数传递过来,应变计的数据是double数组
        /// </summary>
        /// <param name="data">数据列表,应变数据为总应变减去无应力计的自由变形得到的应力应变</param>
        /// <param name="degree">测量次数</param>
        /// <param name="directionNum">应变计的向数</param>
        /// <param name="dates">观测日期数组</param>
        public StressCalc(double[,] data, int degree, int directionNum, DateTime[] dates)
        {


            this.mm = directionNum;//应变计向数

            this.degree = degree;//测量次数

            this.dat = dates;


            y = data;
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }



        private static int datCount = 3000;

        string[] filename0 = new string[10]; string[] filename = new string[10];

        string[] insunum = new string[10];
        string[] dat0 = new string[10];
        string[] hm0 = new string[10];
        string[] ou = new string[10];
        /// <summary>
        /// 观测日期数组
        /// </summary>
        DateTime[] dat = null;
        string[] dz = new string[datCount];
        string[] damm = new string[datCount];

        string[,] da = new string[datCount, 10];
        string[,] hm = new string[datCount, 10];

        double b, t1, t2, cr, ess, hh;


        int[] num = new int[10];
        /// <summary>
        /// 应变计向数
        /// </summary>
        int mm;

        int n;
        int ctype = 0;
        int[] x0 = new int[10];

        /// <summary>
        /// 相对于某个时序的日序数
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
        /// 测量次数
        /// </summary>
        int degree;

        public void crr()
        {
            // 徐变度计算子程序

            double a1, a2, a3, a4, a5;
            switch (ctype)
            {
                case 1:
                    // '三峡大坝内部混凝土徐变度计算子程序(1号混凝土)
                    a1 = t2 - t1;         // '在t1时刻加荷后持续到t2时刻的持荷时间
                    a2 = Math.Pow(a1, 0.2);
                    a3 = Math.Pow(t1, -0.7);

                    a4 = 81.3436 * a2 * a3;
                    a5 = 2.593622 + a4;
                    cr = a5 * 0.000001;               // '单位:1/MPa
                    break;
                case 2:
                    // '三峡大坝基础混凝土徐变度计算子程序(2号混凝土)
                    a1 = t2 - t1;
                    a2 = Math.Pow(a1, 0.215);

                    a3 = Math.Pow(t1, -0.475);
                    a4 = 34.80485 * a2 * a3;
                    a5 = 1.27921 + a4;
                    cr = a5 * 0.000001;           // '单位:1/MPa

                    break;
                case 3:
                    //'三峡大坝外部混凝土徐变度计算子程序(3号混凝土)
                    a1 = t2 - t1;
                    a2 = Math.Pow(a1, 0.212174262);

                    a3 = Math.Pow(t1, -0.36224874);
                    a4 = 26.4983 * a2 * a3;
                    a5 = a4;
                    cr = a5 * 0.000001;        //  '单位:1/MPa
                    break;
                case 4:
                    a1 = t2 - t1;
                    a2 = Math.Pow(a1, 0.26);
                    a3 = Math.Pow(t1, -0.75);
                    a4 = a2 * a3;
                    a5 = a4 / (0.01881321 * a4 + 0.009945777);
                    cr = a5 * 0.000001;            //  '单位:1/MPa
                    break;
                default: break;

            }

        }


        public void ec()
        {
            //Rem 徐变弹模计算子程序

            switch (ctype)
            {
                case 1:
                    // '三峡大坝内部混凝土徐变度计算子程序(1号混凝土)
                    if (t1 < 90)
                    {
                        b = 3.9937 * Math.Log10(t1) + 7.184;  // '静力抗压弹模;单位GPa
                    }
                    else
                        b = 3.9937 * Math.Log10(90) + 7.184;   // '静力抗压弹模;单位GPa

                    break;
                case 2:
                    // '三峡大坝基础混凝土徐变度计算子程序(2号混凝土)
                    if (t1 < 90)
                    {
                        b = 4.310077 * Math.Log10(t1) + 9.385645;  // '静力抗压弹模;单位GPa
                    }
                    else
                        b = 4.310077 * Math.Log10(90) + 9.385645;   // '静力抗压弹模;单位GPa


                    break;
                case 3:
                    //'三峡大坝外部混凝土徐变度计算子程序(3号混凝土)
                    if (t1 < 90)
                    {
                        b = 3.995229 * Math.Log10(t1) + 10.91182;  // '静力抗压弹模;单位GPa
                    }
                    else
                        b = 3.995229 * Math.Log10(90) + 10.91182;   // '静力抗压弹模;单位GPa

                    break;
                case 4:
                    if (t1 < 90)
                    {
                        b = 4.37525 * Math.Log10(t1) + 11.50359;  // '静力抗压弹模;单位GPa
                    }
                    else
                        b = 4.37525 * Math.Log10(90) + 11.50359;   // '静力抗压弹模;单位GPa



                    break;
                default: break;

            }

            b = 1000 * b;   //'单位:MPa


        }

        /// <summary>
        /// 处理并返回结果
        /// </summary>
        /// <returns></returns>
        public string handle()
        {



            if (mm == 5)//如果5项应变计组
            {
                //应力平衡

                for (int i = 0; i < degree; i++)
                {
                    double dz1 = y[i, 1] + y[i, 3] - y[i, 2] - y[i, 4];

                    y[i, 1] = y[i, 1] - dz1 / 4;
                    y[i, 3] = y[i, 3] - dz1 / 4;
                    y[i, 2] = y[i, 2] + dz1 / 4;
                    y[i, 4] = y[i, 4] + dz1 / 4;
                }
            }
            else if (mm == 7)//如果7项应变计组
            {
                //应力平衡

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

            //计算日序数

            for (int i = 0; i < degree; i++)
            {
                DateTime time = dat[i];

                x[i] = round(time.Ticks / TimeSpan.TicksPerDay, 2);


            }

            //座标y轴以朝左岸方向为正,x轴以朝下游方向为正,z轴竖直向上为正

            double u = 1 / 6;
            double mu = 1 / ((1 + u) * (1 - 2 * u));

            switch (mm)
            {
                case 1:
                    for (int i = 0; i < degree; i++)
                    {
                        exyz[i, 1] = y[i, 1];//单向单轴应变计算
                    }
                    break;
                case 2:
                    for (int i = 0; i < degree; i++)
                    {
                        exyz[i, 1] = y[i, 1];//按平面应变状态计算二向应变计组的单轴应变
                        exyz[i, 2] = y[i, 2];
                    }
                    break;
                case 3:
                    for (int i = 0; i < degree; i++)
                    {   //三向应变计组单轴应变计算
                        exyz[i, 1] = mu * (1 - u) * y[i, 1] + mu * u * (y[i, 2] + y[i, 3]);
                        exyz[i, 2] = mu * (1 - u) * y[i, 2] + mu * u * (y[i, 3] + y[i, 1]);
                        exyz[i, 3] = mu * (1 - u) * y[i, 3] + mu * u * (y[i, 1] + y[i, 2]);
                    }
                    break;
                case 5:
                    for (int i = 0; i < degree; i++)
                    {
                        //五向应变计组单轴应变计算
                        exyz[i, 1] = mu * (1 - u) * y[i, 5] + mu * u * (y[i, 1] + y[i, 3]);   //Y,左右岸向
                        exyz[i, 2] = mu * (1 - u) * y[i, 1] + mu * u * (y[i, 3] + y[i, 5]);   //X,上下游向
                        exyz[i, 3] = mu * (1 - u) * y[i, 3] + mu * u * (y[i, 5] + y[i, 1]);   //Z,铅直向
                        exyz[i, 4] = y[i, 2] - y[i, 4];					//XZ,剪应变
                    }
                    break;
                case 7:
                    for (int i = 0; i < degree; i++)
                    {
                        //七向应变组单轴应变计算
                        exyz[i, 1] = mu * (1 - u) * y[i, 1] + mu * u * (y[i, 5] + y[i, 3]);
                        exyz[i, 2] = mu * (1 - u) * y[i, 5] + mu * u * (y[i, 3] + y[i, 1]);
                        exyz[i, 3] = mu * (1 - u) * y[i, 3] + mu * u * (y[i, 1] + y[i, 5]);

                        exyz[i, 4] = y[i, 6] - y[i, 7];  //剪应变
                        exyz[i, 5] = y[i, 2] - y[i, 4];
                    }
                    break;
                default: throw new Exception("应变计组向数不对!请核对后重新计算");

            }

            //当次观测日序值减基准日序值=相对基准观测日的日序值

            for (int i = 0; i < degree; i++)
            {
                x[i] -= x[0];//为方便处理,以第一次观测的日期作为基准日期
            }


            int mp;//成果数?
            switch (mm)
            {
                case 1: mp = 1; break;
                case 2: mp = 2; break;
                case 3: mp = 3; break;
                case 4: mp = 3; break;
                case 5: mp = 4; break;
                case 6: mp = 4; break;
                default: mp = 5; break;//7向
            }

            //计算两次观测中间相对基准日期的日序值

            n = degree - 1;

            for (int i = 0; i < n; i++)
            {
                xm[i] = (x[i] + x[i + 1]) / 2;
            }

            //变形法计算单轴应力子程序
            for (int i = 0; i < mp; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //计算中点应变值

                    ym[j] = (exyz[j, i] + exyz[j + 1, i]) / 2;

                }

                for (int j = 0; j < n; j++)
                {
                    eh[j] = 0; de[j] = 0; exyz[j, i] = 0;
                }

                t1 = x[1];          //加荷龄期
                t2 = xm[1];         //在t1时刻加荷后持续作用到t2时刻
                ec();                //徐变弹模计算
                crr();               //徐变度计算
                if (i > 2) b = 0.492 * b;
                ess = 1 / b + cr;   //单位应力作用下的总变形,b的单位MPa
                //cr的单位1/MPa;ess的单位1/MPa
                ess = 1 / ess;     //有效弹模,单位:Mpa
                de[1] = ess * (ym[1] * 0.000001 - eh[1]); //应力增量,单位:MPa
                exyz[1, i] = de[1];

                for (int j = 2; j < n; j++)
                {
                    t2 = xm[j];

                    hh = 0;

                    for (int k = 1; k < j; k++)
                    {
                        t1 = x[k];
                        ec();  //徐变弹模计算
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

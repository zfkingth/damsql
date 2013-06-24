using System;
using System.Collections.Generic;
using System.Text;

namespace hammergo.Stress
{
    /// <summary>
    /// SimpleStressCalc 的摘要说明。
    /// </summary>
    public class SimpleStressCalc
    {
        private static int datCount = 300;

        /// <summary>
        /// 轴应力和剪应力数组
        /// </summary>
        double[,] exyz = new double[datCount, 6];//新增加了平均温度
        /// <summary>
        /// 弹性模量
        /// </summary>
        double E = 0.0;

        /// <summary>
        /// 剪应力系数
        /// </summary>
        double jianParam = 0.0;

        /// <summary>
        /// 应力数组
        /// </summary>
        double[,] y;

        /// <summary>
        /// 坏掉的仪器序号数组
        /// </summary>
        int[][] bad;

        /// <summary>
        /// 平均温度的数据,求得的平均温度放入exyz[i,5]中
        /// </summary>
        double[,] tData;

        /// <summary>
        /// 测量次数
        /// </summary>
        int degree;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="aE">弹性模量</param>
        /// <param name="ajianParam">剪应力系数</param>
        /// <param name="data">数据</param>
        /// <param name="bakIndexArray">坏掉的仪器序号数组,从1开始</param>
        /// <param name="degree">测量次数</param>
        /// <param name="tData">平均温度数据</param>
        public SimpleStressCalc(double aE, double ajianParam, double[,] data, int[][] bakIndexArray, int degree, double[,] tData)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //

            E = aE;
            jianParam = ajianParam;
            y = data;

            bad = bakIndexArray;

            this.degree = degree;//测量次数

            this.tData = tData;
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="bakIndexArray">坏掉的仪器序号数组,从1开始</param>
        /// <param name="degree">测量次数</param>
        /// <param name="tData">平均温度数据</param>
        public void setData(double[,] data, int[][] bakIndexArray, int degree, double[,] tData)
        {
            y = data;

            bad = bakIndexArray;

            this.degree = degree;//测量次数

            this.tData = tData;


        }

        /// <summary>
        /// 计算应力,结果存放在返回的数组中,如果数组为null,表示计算失败
        /// </summary>
        /// <returns></returns>
        public double[,] handle()
        {
            //如果仪器损坏

            for (int i = 0; i < degree; i++)
            {

                //反求损坏仪器的数据

                int[] directions = bad[i];


                int sum = 0;
                for (int j = 1; j <= 4; j++)
                {
                    sum += directions[j];

                }
                if (sum < -1) return null;//平衡的4支中超过两支损坏

                if (directions[1] == -1)
                {
                    y[i, 1] = y[i, 2] + y[i, 4] - y[i, 3];
                }
                else if (directions[3] == -1)
                {
                    y[i, 3] = y[i, 2] + y[i, 4] - y[i, 1];
                }
                else if (directions[2] == -1)
                {
                    y[i, 2] = y[i, 1] + y[i, 3] - y[i, 4];
                }
                else if (directions[4] == -1)
                {
                    y[i, 4] = y[i, 1] + y[i, 3] - y[i, 2];
                }



            }



            //应力平衡

            for (int i = 0; i < degree; i++)
            {
                double dz1 = y[i, 1] + y[i, 3] - y[i, 2] - y[i, 4];

                //不平衡量,放入到成果里去

                exyz[i, 0] = dz1;

                y[i, 1] = y[i, 1] - dz1 / 4;
                y[i, 3] = y[i, 3] - dz1 / 4;
                y[i, 2] = y[i, 2] + dz1 / 4;
                y[i, 4] = y[i, 4] + dz1 / 4;
            }

            //座标y轴以朝左岸方向为正,x轴以朝下游方向为正,z轴竖直向上为正

            double u = 1.0 / 6;
            double mu = 1.0 / ((1 + u) * (1 - 2 * u));

            for (int i = 0; i < degree; i++)
            {
                if (bad[i][5] != -1)
                {
                    //五向应变计组单轴应变计算
                    exyz[i, 1] = mu * (1 - u) * y[i, 5] + mu * u * (y[i, 1] + y[i, 3]);   //Y,左右岸向
                    exyz[i, 2] = mu * (1 - u) * y[i, 1] + mu * u * (y[i, 3] + y[i, 5]);   //X,上下游向
                    exyz[i, 3] = mu * (1 - u) * y[i, 3] + mu * u * (y[i, 5] + y[i, 1]);   //Z,铅直向
                }
                else
                {
                    exyz[i, 1] = double.NaN;
                    exyz[i, 2] = y[i, 1];
                    exyz[i, 3] = y[i, 3];
                }
                exyz[i, 4] = y[i, 2] - y[i, 4];					//XZ,剪应变
            }

            for (int i = 0; i < degree; i++)
            {

                exyz[i, 1] *= 0.01 * E;

                exyz[i, 2] *= 0.01 * E;
                exyz[i, 3] *= 0.01 * E;
                exyz[i, 4] *= 0.01 * E * jianParam;

                //求平均温度

                double sum = 0;
                int realLength = 0;

                for (int t = 1; t < tData.GetLength(1); t++)//数据从1开始
                {
                    if (double.IsNaN(tData[i, t]) == false)
                    {
                        //非数字

                        sum += tData[i, t];
                        realLength++;

                    }
                }

                if (realLength == 0) exyz[0, 5] = double.NaN;//无平均温度
                else
                {
                    exyz[0, 5] = sum / realLength;
                }


            }



            return exyz;



        }


    }
}

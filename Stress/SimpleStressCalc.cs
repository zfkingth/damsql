using System;
using System.Collections.Generic;
using System.Text;

namespace hammergo.Stress
{
    /// <summary>
    /// SimpleStressCalc ��ժҪ˵����
    /// </summary>
    public class SimpleStressCalc
    {
        private static int datCount = 300;

        /// <summary>
        /// ��Ӧ���ͼ�Ӧ������
        /// </summary>
        double[,] exyz = new double[datCount, 6];//��������ƽ���¶�
        /// <summary>
        /// ����ģ��
        /// </summary>
        double E = 0.0;

        /// <summary>
        /// ��Ӧ��ϵ��
        /// </summary>
        double jianParam = 0.0;

        /// <summary>
        /// Ӧ������
        /// </summary>
        double[,] y;

        /// <summary>
        /// �����������������
        /// </summary>
        int[][] bad;

        /// <summary>
        /// ƽ���¶ȵ�����,��õ�ƽ���¶ȷ���exyz[i,5]��
        /// </summary>
        double[,] tData;

        /// <summary>
        /// ��������
        /// </summary>
        int degree;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="aE">����ģ��</param>
        /// <param name="ajianParam">��Ӧ��ϵ��</param>
        /// <param name="data">����</param>
        /// <param name="bakIndexArray">�����������������,��1��ʼ</param>
        /// <param name="degree">��������</param>
        /// <param name="tData">ƽ���¶�����</param>
        public SimpleStressCalc(double aE, double ajianParam, double[,] data, int[][] bakIndexArray, int degree, double[,] tData)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //

            E = aE;
            jianParam = ajianParam;
            y = data;

            bad = bakIndexArray;

            this.degree = degree;//��������

            this.tData = tData;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="data">����</param>
        /// <param name="bakIndexArray">�����������������,��1��ʼ</param>
        /// <param name="degree">��������</param>
        /// <param name="tData">ƽ���¶�����</param>
        public void setData(double[,] data, int[][] bakIndexArray, int degree, double[,] tData)
        {
            y = data;

            bad = bakIndexArray;

            this.degree = degree;//��������

            this.tData = tData;


        }

        /// <summary>
        /// ����Ӧ��,�������ڷ��ص�������,�������Ϊnull,��ʾ����ʧ��
        /// </summary>
        /// <returns></returns>
        public double[,] handle()
        {
            //���������

            for (int i = 0; i < degree; i++)
            {

                //����������������

                int[] directions = bad[i];


                int sum = 0;
                for (int j = 1; j <= 4; j++)
                {
                    sum += directions[j];

                }
                if (sum < -1) return null;//ƽ���4֧�г�����֧��

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



            //Ӧ��ƽ��

            for (int i = 0; i < degree; i++)
            {
                double dz1 = y[i, 1] + y[i, 3] - y[i, 2] - y[i, 4];

                //��ƽ����,���뵽�ɹ���ȥ

                exyz[i, 0] = dz1;

                y[i, 1] = y[i, 1] - dz1 / 4;
                y[i, 3] = y[i, 3] - dz1 / 4;
                y[i, 2] = y[i, 2] + dz1 / 4;
                y[i, 4] = y[i, 4] + dz1 / 4;
            }

            //����y���Գ��󰶷���Ϊ��,x���Գ����η���Ϊ��,z����ֱ����Ϊ��

            double u = 1.0 / 6;
            double mu = 1.0 / ((1 + u) * (1 - 2 * u));

            for (int i = 0; i < degree; i++)
            {
                if (bad[i][5] != -1)
                {
                    //����Ӧ����鵥��Ӧ�����
                    exyz[i, 1] = mu * (1 - u) * y[i, 5] + mu * u * (y[i, 1] + y[i, 3]);   //Y,���Ұ���
                    exyz[i, 2] = mu * (1 - u) * y[i, 1] + mu * u * (y[i, 3] + y[i, 5]);   //X,��������
                    exyz[i, 3] = mu * (1 - u) * y[i, 3] + mu * u * (y[i, 5] + y[i, 1]);   //Z,Ǧֱ��
                }
                else
                {
                    exyz[i, 1] = double.NaN;
                    exyz[i, 2] = y[i, 1];
                    exyz[i, 3] = y[i, 3];
                }
                exyz[i, 4] = y[i, 2] - y[i, 4];					//XZ,��Ӧ��
            }

            for (int i = 0; i < degree; i++)
            {

                exyz[i, 1] *= 0.01 * E;

                exyz[i, 2] *= 0.01 * E;
                exyz[i, 3] *= 0.01 * E;
                exyz[i, 4] *= 0.01 * E * jianParam;

                //��ƽ���¶�

                double sum = 0;
                int realLength = 0;

                for (int t = 1; t < tData.GetLength(1); t++)//���ݴ�1��ʼ
                {
                    if (double.IsNaN(tData[i, t]) == false)
                    {
                        //������

                        sum += tData[i, t];
                        realLength++;

                    }
                }

                if (realLength == 0) exyz[0, 5] = double.NaN;//��ƽ���¶�
                else
                {
                    exyz[0, 5] = sum / realLength;
                }


            }



            return exyz;



        }


    }
}

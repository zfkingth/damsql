using System;
using System.Collections.Generic;
using System.Text;

namespace hammergo.Utility
{
    public class ThreadParameters
    {
        AppIntegratedInfo appInfo = null;
        DateTime date;
        private ThreadParameters(AppIntegratedInfo appInfo, DateTime date)
        {
            this.appInfo = appInfo;
            this.date = date;
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }


        public static  void startNewThreadToUpdateData(AppIntegratedInfo appInfo, DateTime updatedDate)
        {
            ThreadParameters tp = new ThreadParameters(appInfo, updatedDate);
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(tp.UpdateData));
            thread.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateData()
        {
            //��ȡ�������



            appInfo.Update();//������׳��쳣
       
            

            //if (success)
            //{
                //���������Żᵽ��һ��
                UtilityUpdateData.reCalculateLink(appInfo.App.CalculateName, date);

               
            //}



        }


        
    }
}

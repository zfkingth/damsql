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
            // TODO: 在此处添加构造函数逻辑
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
            //获取相关数据



            appInfo.Update();//出错会抛出异常
       
            

            //if (success)
            //{
                //操作正常才会到这一步
                UtilityUpdateData.reCalculateLink(appInfo.App.CalculateName, date);

               
            //}



        }


        
    }
}

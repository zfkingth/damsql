 /**////////////////////////////////////////////////////////////////////////////////////////
 // Description: Entity Model class.
 // ---------------------
 // Copyright  2009 hammergo@163.com
 // ---------------------
 // History
 //    2013年1月27日 14:05:40    zfking    
 /**////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using hammergo.Tracking;

namespace hammergo.Model
{
    /// <summary>
    /// 实体类
    /// </summary>
    [Serializable()]
    public partial class Remark:INotifyPropertyChanged, ITrackable
    {
		public event PropertyChangedEventHandler PropertyChanged;
		
		private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
		
		#region ITrackable 成员

        TrackingInfo _trackingState = TrackingInfo.Unchanged;
        public TrackingInfo TrackingState
        {
            get
            {
                return _trackingState;
            }
            set
            {
                _trackingState = value;
            }
        }

        #endregion
		

        private string _appName;
        private System.DateTime? _date;
        private string _remarkText;
        
        public Remark() {}
        
		//除string外，其它数据类型全部在Model中设置为可空，这样就不会将默认值传递到必填字段中，也就是说必填字段全部得用用户设置
       
		
        
        public string AppName
        {
            get{ return this._appName; }
            set
			{
                if (this._appName != value)
                {
                   this._appName = value;
                    NotifyPropertyChanged("AppName");

                }
			}
        }
        
        public System.DateTime? Date
        {
            get{ return this._date; }
            set
			{
                if (this._date != value)
                {
                   this._date = value;
                    NotifyPropertyChanged("Date");

                }
			}
        }
        
        public string RemarkText
        {
            get{ return this._remarkText; }
            set
			{
                if (this._remarkText != value)
                {
                   this._remarkText = value;
                    NotifyPropertyChanged("RemarkText");

                }
			}
        }
        
    }
}




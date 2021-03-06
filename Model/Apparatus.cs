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
    public partial class Apparatus:INotifyPropertyChanged, ITrackable
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
        private string _calculateName;
        private System.Guid? _projectPartID;
        private System.Guid? _appTypeID;
        private string _x;
        private string _y;
        private string _z;
        private System.DateTime? _buriedTime;
        private string _otherInfo;
        
        public Apparatus() {}
        
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
        
        public string CalculateName
        {
            get{ return this._calculateName; }
            set
			{
                if (this._calculateName != value)
                {
                   this._calculateName = value;
                    NotifyPropertyChanged("CalculateName");

                }
			}
        }
        
        public System.Guid? ProjectPartID
        {
            get{ return this._projectPartID; }
            set
			{
                if (this._projectPartID != value)
                {
                   this._projectPartID = value;
                    NotifyPropertyChanged("ProjectPartID");

                }
			}
        }
        
        public System.Guid? AppTypeID
        {
            get{ return this._appTypeID; }
            set
			{
                if (this._appTypeID != value)
                {
                   this._appTypeID = value;
                    NotifyPropertyChanged("AppTypeID");

                }
			}
        }
        
        public string X
        {
            get{ return this._x; }
            set
			{
                if (this._x != value)
                {
                   this._x = value;
                    NotifyPropertyChanged("X");

                }
			}
        }
        
        public string Y
        {
            get{ return this._y; }
            set
			{
                if (this._y != value)
                {
                   this._y = value;
                    NotifyPropertyChanged("Y");

                }
			}
        }
        
        public string Z
        {
            get{ return this._z; }
            set
			{
                if (this._z != value)
                {
                   this._z = value;
                    NotifyPropertyChanged("Z");

                }
			}
        }
        
        public System.DateTime? BuriedTime
        {
            get{ return this._buriedTime; }
            set
			{
                if (this._buriedTime != value)
                {
                   this._buriedTime = value;
                    NotifyPropertyChanged("BuriedTime");

                }
			}
        }
        
        public string OtherInfo
        {
            get{ return this._otherInfo; }
            set
			{
                if (this._otherInfo != value)
                {
                   this._otherInfo = value;
                    NotifyPropertyChanged("OtherInfo");

                }
			}
        }
        
    }
}




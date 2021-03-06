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
    public partial class Role:INotifyPropertyChanged, ITrackable
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
		

        private int? _roleID;
        private string _roleName;
        private string _description;
        private byte? _power;
        
        public Role() {}
        
		//除string外，其它数据类型全部在Model中设置为可空，这样就不会将默认值传递到必填字段中，也就是说必填字段全部得用用户设置
       
		
        
        public int? RoleID
        {
            get{ return this._roleID; }
            set
			{
                if (this._roleID != value)
                {
                   this._roleID = value;
                    NotifyPropertyChanged("RoleID");

                }
			}
        }
        
        public string RoleName
        {
            get{ return this._roleName; }
            set
			{
                if (this._roleName != value)
                {
                   this._roleName = value;
                    NotifyPropertyChanged("RoleName");

                }
			}
        }
        
        public string Description
        {
            get{ return this._description; }
            set
			{
                if (this._description != value)
                {
                   this._description = value;
                    NotifyPropertyChanged("Description");

                }
			}
        }
        
        public byte? Power
        {
            get{ return this._power; }
            set
			{
                if (this._power != value)
                {
                   this._power = value;
                    NotifyPropertyChanged("Power");

                }
			}
        }
        
    }
}




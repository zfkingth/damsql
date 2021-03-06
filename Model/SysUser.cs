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
    public partial class SysUser:INotifyPropertyChanged, ITrackable
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
		

        private string _userName;
        private string _passwordHash;
        private string _salt;
        private string _question;
        private string _answer;
        private int? _roleID;
        
        public SysUser() {}
        
		//除string外，其它数据类型全部在Model中设置为可空，这样就不会将默认值传递到必填字段中，也就是说必填字段全部得用用户设置
       
		
        
        public string UserName
        {
            get{ return this._userName; }
            set
			{
                if (this._userName != value)
                {
                   this._userName = value;
                    NotifyPropertyChanged("UserName");

                }
			}
        }
        
        public string PasswordHash
        {
            get{ return this._passwordHash; }
            set
			{
                if (this._passwordHash != value)
                {
                   this._passwordHash = value;
                    NotifyPropertyChanged("PasswordHash");

                }
			}
        }
        
        public string Salt
        {
            get{ return this._salt; }
            set
			{
                if (this._salt != value)
                {
                   this._salt = value;
                    NotifyPropertyChanged("Salt");

                }
			}
        }
        
        public string Question
        {
            get{ return this._question; }
            set
			{
                if (this._question != value)
                {
                   this._question = value;
                    NotifyPropertyChanged("Question");

                }
			}
        }
        
        public string Answer
        {
            get{ return this._answer; }
            set
			{
                if (this._answer != value)
                {
                   this._answer = value;
                    NotifyPropertyChanged("Answer");

                }
			}
        }
        
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
        
    }
}




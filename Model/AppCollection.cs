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
    public partial class AppCollection:INotifyPropertyChanged, ITrackable
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
		

        private System.Guid? _appCollectionID;
        private int? _taskTypeID;
        private string _collectionName;
        private string _description;
        private int? _order;
        
        public AppCollection() {}
        
		//除string外，其它数据类型全部在Model中设置为可空，这样就不会将默认值传递到必填字段中，也就是说必填字段全部得用用户设置
       
		
        
        public System.Guid? AppCollectionID
        {
            get{ return this._appCollectionID; }
            set
			{
                if (this._appCollectionID != value)
                {
                   this._appCollectionID = value;
                    NotifyPropertyChanged("AppCollectionID");

                }
			}
        }
        
        public int? TaskTypeID
        {
            get{ return this._taskTypeID; }
            set
			{
                if (this._taskTypeID != value)
                {
                   this._taskTypeID = value;
                    NotifyPropertyChanged("TaskTypeID");

                }
			}
        }
        
        public string CollectionName
        {
            get{ return this._collectionName; }
            set
			{
                if (this._collectionName != value)
                {
                   this._collectionName = value;
                    NotifyPropertyChanged("CollectionName");

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
        
        public int? Order
        {
            get{ return this._order; }
            set
			{
                if (this._order != value)
                {
                   this._order = value;
                    NotifyPropertyChanged("Order");

                }
			}
        }
        
    }
}




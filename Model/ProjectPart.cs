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
    public partial class ProjectPart:INotifyPropertyChanged, ITrackable
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
		

        private System.Guid? _projectPartID;
        private string _partName;
        private System.Guid? _parentPart;
        
        public ProjectPart() {}
        
		//除string外，其它数据类型全部在Model中设置为可空，这样就不会将默认值传递到必填字段中，也就是说必填字段全部得用用户设置
       
		
        
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
        
        public string PartName
        {
            get{ return this._partName; }
            set
			{
                if (this._partName != value)
                {
                   this._partName = value;
                    NotifyPropertyChanged("PartName");

                }
			}
        }
        
        public System.Guid? ParentPart
        {
            get{ return this._parentPart; }
            set
			{
                if (this._parentPart != value)
                {
                   this._parentPart = value;
                    NotifyPropertyChanged("ParentPart");

                }
			}
        }
        
    }
}




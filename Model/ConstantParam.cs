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
    public partial class ConstantParam : INotifyPropertyChanged, ITrackable, IParamInterface
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
		

        private System.Guid? _constantParamID;
        private string _appName;
        private string _paramName;
        private string _paramSymbol;
        private string _unitSymbol;
        private byte? _precisionNum;
        private byte? _order;
        private double? _val;
        private string _description;
        
        public ConstantParam() {}
        
		//除string外，其它数据类型全部在Model中设置为可空，这样就不会将默认值传递到必填字段中，也就是说必填字段全部得用用户设置
       
		
        
        public System.Guid? ConstantParamID
        {
            get{ return this._constantParamID; }
            set
			{
                if (this._constantParamID != value)
                {
                   this._constantParamID = value;
                    NotifyPropertyChanged("ConstantParamID");

                }
			}
        }
        
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
        
        public string ParamName
        {
            get{ return this._paramName; }
            set
			{
                if (this._paramName != value)
                {
                   this._paramName = value;
                    NotifyPropertyChanged("ParamName");

                }
			}
        }
        
        public string ParamSymbol
        {
            get{ return this._paramSymbol; }
            set
			{
                if (this._paramSymbol != value)
                {
                   this._paramSymbol = value;
                    NotifyPropertyChanged("ParamSymbol");

                }
			}
        }
        
        public string UnitSymbol
        {
            get{ return this._unitSymbol; }
            set
			{
                if (this._unitSymbol != value)
                {
                   this._unitSymbol = value;
                    NotifyPropertyChanged("UnitSymbol");

                }
			}
        }
        
        public byte? PrecisionNum
        {
            get{ return this._precisionNum; }
            set
			{
                if (this._precisionNum != value)
                {
                   this._precisionNum = value;
                    NotifyPropertyChanged("PrecisionNum");

                }
			}
        }
        
        public byte? Order
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
        
        public double? Val
        {
            get{ return this._val; }
            set
			{
                if (this._val != value)
                {
                   this._val = value;
                    NotifyPropertyChanged("Val");

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
        
    }
}




using System;
namespace Baby.Model
{
    /// <summary>
    /// V_Info:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Information
    {
        public Information()
        { }
        #region Model
        private string _logtime;
        private string _name;
        private string _bsex;
        private int _bID;
        private int? _hID;
        private string _in;
        private string _day;
        private string _hName;
        /// <summary>
        /// 
        /// </summary>
        public string Recordtime
        {
            set { _logtime = value; }
            get { return _logtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BName
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sex
        {
            set { _bsex = value; }
            get { return _bsex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BID
        {
            set { _bID = value; }
            get { return _bID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Hid
        {
            set { _hID = value; }
            get { return _hID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Intime
        {
            set { _in = value; }
            get { return _in; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Birthday
        {
            set { _day = value; }
            get { return _day; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HName
        {
            set { _hName = value; }
            get { return _hName; }
        }
        #endregion Model

    }
}
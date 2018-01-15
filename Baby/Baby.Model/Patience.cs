/**  版本信息模板在安装目录下，可自行修改。
* Patience.cs
*
* 功 能： N/A
* 类 名： Patience
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018/1/13 19:25:28   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Baby.Model
{
	/// <summary>
	/// Patience:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Patience
	{
		public Patience()
		{}
		#region Model
		private int _bid;
		private int _mid;
		private string _bname;
		private string _sex;
		private int? _hid;
		private DateTime? _intime;
		private DateTime? _leavetime;
		private DateTime? _birthday;
		private DateTime? _birthtime;
		private int? _pregnantweeks;
		private string _birthplace;
		private string _babynum;
		private int? _babytime;
		private double _babyweight;
		private double _babyheight;
		private DateTime? _recordtime;
		/// <summary>
		/// 
		/// </summary>
		public int BID
		{
			set{ _bid=value;}
			get{return _bid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int MID
		{
			set{ _mid=value;}
			get{return _mid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BName
		{
			set{ _bname=value;}
			get{return _bname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Hid
		{
			set{ _hid=value;}
			get{return _hid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Leavetime
		{
			set{ _leavetime=value;}
			get{return _leavetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Birthday
		{
			set{ _birthday=value;}
			get{return _birthday;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Birthtime
		{
			set{ _birthtime=value;}
			get{return _birthtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PregnantWeeks
		{
			set{ _pregnantweeks=value;}
			get{return _pregnantweeks;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BirthPlace
		{
			set{ _birthplace=value;}
			get{return _birthplace;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BabyNum
		{
			set{ _babynum=value;}
			get{return _babynum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BabyTime
		{
			set{ _babytime=value;}
			get{return _babytime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double BabyWeight
		{
			set{ _babyweight=value;}
			get{return _babyweight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double BabyHeight
		{
			set{ _babyheight=value;}
			get{return _babyheight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Recordtime
		{
			set{ _recordtime=value;}
			get{return _recordtime;}
		}
		#endregion Model

        #region 扩展属性
        private string _midwife;
        private string _hname;

        public string Midwife
        {
            set { _midwife = value; }
            get { return _midwife; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HName
        {
            set { _hname = value; }
            get { return _hname; }
        }

        #endregion


    }
}


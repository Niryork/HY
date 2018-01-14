/**  版本信息模板在安装目录下，可自行修改。
* V_Info.cs
*
* 功 能： N/A
* 类 名： V_Info
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018/1/13 20:19:29   N/A    初版
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
	/// V_Info:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class V_Info
	{
		public V_Info()
		{}
		#region Model
		private DateTime? _recordtime;
		private string _bname;
		private string _sex;
		private int _bid;
		private int? _hid;
		private DateTime? _intime;
		private DateTime? _birthday;
		private string _hname;
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Recordtime
		{
			set{ _recordtime=value;}
			get{return _recordtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BName
		{
            set { _bname = value; }
            get { return _bname; }
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
		public int BID
		{
			set{ _bid=value;}
			get{return _bid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Hid
		{
			set{ _hid=value;}
            get { return _hid; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Intime
		{
			set{ _intime=value;}
            get { return _intime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Birthday
		{
			set{ _birthday=value;}
            get { return _birthday; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string HName
		{
			set{ _hname=value;}
			get{return _hname;}
		}
		#endregion Model

	}
}


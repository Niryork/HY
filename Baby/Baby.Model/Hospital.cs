/**  版本信息模板在安装目录下，可自行修改。
* Hospital.cs
*
* 功 能： N/A
* 类 名： Hospital
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018/1/13 19:25:27   N/A    初版
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
	/// Hospital:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Hospital
	{
		public Hospital()
		{}
		#region Model
		private int _mid;
		private string _midwife;
		private string _hname;
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
		public string Midwife
		{
			set{ _midwife=value;}
			get{return _midwife;}
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


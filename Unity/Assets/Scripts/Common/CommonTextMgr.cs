//using System;
using System.Collections.Generic;

public class CommonTextMgr
{
	private static Dictionary<string, string> m_data;

	private static CommonTextMgr _instance;
	public static CommonTextMgr instance{
		get{ 
			if (_instance == null) {
				_instance = new CommonTextMgr ();
				m_data  = new Dictionary<string, string>();
				DBCommonTextMgr db = new DBCommonTextMgr ();
				for (int i = 0; i < db.count; i++) {
					DBCommonTextBean db_ctb = db.GetDataByRow (i);
					string Value_ = db_ctb.Value_.Replace ("\\n","\n");
					m_data.Add (db_ctb.Key_, Value_);
				}
			}
			return _instance;
		}
	}

	/// <summary>
	/// 通过键名获取键值
	/// </summary>
	/// <returns>The data.</returns>
	/// <param name="_key">Key.</param>
	public string GetDataByKey(string _key){
		string _value = " ";
		if (m_data.ContainsKey (_key))
			_value = m_data [_key];
		return _value;
	}

	public void Clear(){
		_instance = null;
		m_data.Clear ();
	}
}

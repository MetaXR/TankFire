using UnityEngine;
using System.Collections;
using ExcelParser;

public class DBCommonTextBean : IDataBean {
 
 

	private int _id;
	public int Id {
		get {
			return _id;
		}
		set {
			_id = value;
		}
	}
 

	private string _key_;
	public string Key_ {
		get {
			return _key_;
		}
		set {
			_key_ = value;
		}
	}
 

	private string _value_;
	public string Value_ {
		get {
			return _value_;
		}
		set {
			_value_ = value;
		}
	}
 
}

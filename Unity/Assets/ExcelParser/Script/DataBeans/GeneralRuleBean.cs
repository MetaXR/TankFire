using UnityEngine;
using System.Collections;
using ExcelParser;

public class GeneralRuleBean : IDataBean {
 
 

	private int _id;
	public int Id {
		get {
			return _id;
		}
		set {
			_id = value;
		}
	}
 

	private string _title;
	public string Title {
		get {
			return _title;
		}
		set {
			_title = value;
		}
	}
 

	private string _desc;
	public string Desc {
		get {
			return _desc;
		}
		set {
			_desc = value;
		}
	}
 

	private string _functions;
	public string Functions {
		get {
			return _functions;
		}
		set {
			_functions = value;
		}
	}
 

	private string _functions2;
	public string Functions2 {
		get {
			return _functions2;
		}
		set {
			_functions2 = value;
		}
	}
 

	private string _functionsBtnName;
	public string FunctionsBtnName {
		get {
			return _functionsBtnName;
		}
		set {
			_functionsBtnName = value;
		}
	}
 

	private string _functionsBtnName2;
	public string FunctionsBtnName2 {
		get {
			return _functionsBtnName2;
		}
		set {
			_functionsBtnName2 = value;
		}
	}
 

	private float _waitTime;
	public float WaitTime {
		get {
			return _waitTime;
		}
		set {
			_waitTime = value;
		}
	}
 

	private int _nextid;
	public int Nextid {
		get {
			return _nextid;
		}
		set {
			_nextid = value;
		}
	}
 
}

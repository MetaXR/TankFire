using UnityEngine;
using System.Collections;
using ExcelParser;

public class LabInfoBean : IDataBean {
 
 

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
 
}

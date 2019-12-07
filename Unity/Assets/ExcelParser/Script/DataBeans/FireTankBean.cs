using UnityEngine;
using System.Collections;
using ExcelParser;

public class FireTankBean : IDataBean {
 
 

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
 

	private string _prefab;
	public string Prefab {
		get {
			return _prefab;
		}
		set {
			_prefab = value;
		}
	}
 

	private int _lifevalue;
	public int Lifevalue {
		get {
			return _lifevalue;
		}
		set {
			_lifevalue = value;
		}
	}
 

	private int _lossspeed;
	public int Lossspeed {
		get {
			return _lossspeed;
		}
		set {
			_lossspeed = value;
		}
	}
 
}

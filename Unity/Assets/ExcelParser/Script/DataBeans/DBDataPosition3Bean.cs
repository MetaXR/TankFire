using UnityEngine;
using System.Collections;
using ExcelParser;

public class DBDataPosition3Bean : IDataBean {
 
 

	private int _id;
	public int Id {
		get {
			return _id;
		}
		set {
			_id = value;
		}
	}
 

	private string _time;
	public string Time {
		get {
			return _time;
		}
		set {
			_time = value;
		}
	}
 

	private float _CO;
	public float CO {
		get {
			return _CO;
		}
		set {
			_CO = value;
		}
	}
 

	private float _CO2;
	public float CO2 {
		get {
			return _CO2;
		}
		set {
			_CO2 = value;
		}
	}
 

	private float _WEICENG;
	public float WEICENG {
		get {
			return _WEICENG;
		}
		set {
			_WEICENG = value;
		}
	}
 

	private float _WenDu;
	public float WenDu {
		get {
			return _WenDu;
		}
		set {
			_WenDu = value;
		}
	}
 

	private float _ShiDu;
	public float ShiDu {
		get {
			return _ShiDu;
		}
		set {
			_ShiDu = value;
		}
	}
 

	private float _FengSu;
	public float FengSu {
		get {
			return _FengSu;
		}
		set {
			_FengSu = value;
		}
	}
 

	private float _ZaoS;
	public float ZaoS {
		get {
			return _ZaoS;
		}
		set {
			_ZaoS = value;
		}
	}
 
}

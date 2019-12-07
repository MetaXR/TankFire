using UnityEngine;
using System.Collections;
using ExcelParser;

public class TankBean : IDataBean {
 
 

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
 

	private int _cellcount;
	public int Cellcount {
		get {
			return _cellcount;
		}
		set {
			_cellcount = value;
		}
	}
 

	private string _cell1;
	public string Cell1 {
		get {
			return _cell1;
		}
		set {
			_cell1 = value;
		}
	}
 

	private string _desc1;
	public string Desc1 {
		get {
			return _desc1;
		}
		set {
			_desc1 = value;
		}
	}
 

	private string _cell2;
	public string Cell2 {
		get {
			return _cell2;
		}
		set {
			_cell2 = value;
		}
	}
 

	private string _desc2;
	public string Desc2 {
		get {
			return _desc2;
		}
		set {
			_desc2 = value;
		}
	}
 

	private string _cell3;
	public string Cell3 {
		get {
			return _cell3;
		}
		set {
			_cell3 = value;
		}
	}
 

	private string _desc3;
	public string Desc3 {
		get {
			return _desc3;
		}
		set {
			_desc3 = value;
		}
	}
 

	private string _cell4;
	public string Cell4 {
		get {
			return _cell4;
		}
		set {
			_cell4 = value;
		}
	}
 

	private string _desc4;
	public string Desc4 {
		get {
			return _desc4;
		}
		set {
			_desc4 = value;
		}
	}
 

string[] celllist = new string[4];

    public string[] CellList
    {
        get { return celllist; }
        set { celllist = value; }
    }

    private string[] desclist = new string[4];

    public string[] DescList
    {
        get { return desclist; }
        set { desclist = value; }
    }
}







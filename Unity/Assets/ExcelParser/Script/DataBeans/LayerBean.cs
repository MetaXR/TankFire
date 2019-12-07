using UnityEngine;
using System.Collections;
using ExcelParser;

public class LayerBean : IDataBean {
 
 

	private int id;
	public int Id {
		get {
			return id;
		}
		set {
			id = value;
		}
	}
 

	private string name;
	public string Name {
		get {
			return name;
		}
		set {
			name = value;
		}
	}
 

	private int layer;
	public int Layer {
		get {
			return layer;
		}
		set {
			layer = value;
		}
	}
 
}

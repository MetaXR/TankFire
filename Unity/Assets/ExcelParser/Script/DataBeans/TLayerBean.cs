using UnityEngine;
using System.Collections;
using ExcelParser;

public class TLayerBean : IDataBean {
 
 

	private int id;
	public int Id {
		get {
			return id;
		}
		set {
			id = value;
		}
	}
 

	private string title;
	public string Title {
		get {
			return title;
		}
		set {
			title = value;
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

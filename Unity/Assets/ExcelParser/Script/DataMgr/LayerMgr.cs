using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class LayerMgr : DataMgrBase<LayerMgr> {


	protected override string GetXlsxPath ()
	{
		return "Layer";
	}


	protected override System.Type GetBeanType ()
	{
		return typeof(LayerBean);
	}


	public LayerBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (LayerBean)dataBean;
		}else{
			return null;
		}
	}



}

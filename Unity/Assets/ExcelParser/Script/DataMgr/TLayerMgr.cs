using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class TLayerMgr : DataMgrBase<TLayerMgr> {


	protected override string GetXlsxPath ()
	{
		return "TLayer";
	}


	protected override System.Type GetBeanType ()
	{
		return typeof(TLayerBean);
	}


	public TLayerBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (TLayerBean)dataBean;
		}else{
			return null;
		}
	}



}

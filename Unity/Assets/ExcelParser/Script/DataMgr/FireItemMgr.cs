using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class FireItemMgr : DataMgrBase<FireItemMgr> {

    public FireItemMgr()
    {
		InitData ();
	}

	protected override string GetXlsxPath ()
	{
		return "FireItem";
	}

	protected override System.Type GetBeanType ()
	{
		return typeof(FireItemBean);
	}

	public FireItemBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (FireItemBean)dataBean;
		}else{
			return null;
		}
	}

	public FireItemBean GetDataByRow(int row)
	{
		IDataBean dataBean = _GetDataByRow(row);

		if(dataBean!=null)
		{
			return (FireItemBean)dataBean;
		}else{
			return null;
		}
	}
}

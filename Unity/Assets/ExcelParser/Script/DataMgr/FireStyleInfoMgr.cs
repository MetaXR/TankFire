using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class FireStyleInfoMgr : DataMgrBase<FireStyleInfoMgr> {

    public FireStyleInfoMgr()
    {
		InitData ();
	}

	protected override string GetXlsxPath ()
	{
		return "FireStyleInfo";
	}

	protected override System.Type GetBeanType ()
	{
		return typeof(FireStyleInfoBean);
	}

	public FireStyleInfoBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (FireStyleInfoBean)dataBean;
		}else{
			return null;
		}
	}

	public FireStyleInfoBean GetDataByRow(int row)
	{
		IDataBean dataBean = _GetDataByRow(row);

		if(dataBean!=null)
		{
			return (FireStyleInfoBean)dataBean;
		}else{
			return null;
		}
	}
}

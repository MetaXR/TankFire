using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class FireTankMgr : DataMgrBase<FireTankMgr> {

    public FireTankMgr()
    {
		InitData ();
	}

	protected override string GetXlsxPath ()
	{
		return "FireTank";
	}

	protected override System.Type GetBeanType ()
	{
		return typeof(FireTankBean);
	}

	public FireTankBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (FireTankBean)dataBean;
		}else{
			return null;
		}
	}

	public FireTankBean GetDataByRow(int row)
	{
		IDataBean dataBean = _GetDataByRow(row);

		if(dataBean!=null)
		{
			return (FireTankBean)dataBean;
		}else{
			return null;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class TankMgr : DataMgrBase<TankMgr> {

    public TankMgr()
    {
		InitData ();
	}

	protected override string GetXlsxPath ()
	{
		return "Tank";
	}

	protected override System.Type GetBeanType ()
	{
		return typeof(TankBean);
	}

	public TankBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (TankBean)dataBean;
		}else{
			return null;
		}
	}

	public TankBean GetDataByRow(int row)
	{
		IDataBean dataBean = _GetDataByRow(row);

		if(dataBean!=null)
		{
			return (TankBean)dataBean;
		}else{
			return null;
		}
	}
}
   
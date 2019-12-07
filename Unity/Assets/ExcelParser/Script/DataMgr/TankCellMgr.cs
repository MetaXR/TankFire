using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class TankCellMgr : DataMgrBase<TankCellMgr> {

    public TankCellMgr()
    {
		InitData ();
	}

	protected override string GetXlsxPath ()
	{
		return "TankCell";
	}

	protected override System.Type GetBeanType ()
	{
		return typeof(TankCellBean);
	}

	public TankCellBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (TankCellBean)dataBean;
		}else{
			return null;
		}
	}

	public TankCellBean GetDataByRow(int row)
	{
		IDataBean dataBean = _GetDataByRow(row);

		if(dataBean!=null)
		{
			return (TankCellBean)dataBean;
		}else{
			return null;
		}
	}
}

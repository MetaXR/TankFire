using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class FlowWDXFireMgr : DataMgrBase<FlowWDXFireMgr> {

    public FlowWDXFireMgr()
    {
		InitData ();
	}

	protected override string GetXlsxPath ()
	{
		return "FlowWDXFire";
	}

	protected override System.Type GetBeanType ()
	{
		return typeof(FlowWDXFireBean);
	}

	public FlowWDXFireBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (FlowWDXFireBean)dataBean;
		}else{
			return null;
		}
	}

	public FlowWDXFireBean GetDataByRow(int row)
	{
		IDataBean dataBean = _GetDataByRow(row);

		if(dataBean!=null)
		{
			return (FlowWDXFireBean)dataBean;
		}else{
			return null;
		}
	}
}

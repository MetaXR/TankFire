using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class FlowDiMianFireMgr : DataMgrBase<FlowDiMianFireMgr> {

    public FlowDiMianFireMgr()
    {
		InitData ();
	}

	protected override string GetXlsxPath ()
	{
		return "FlowDiMianFire";
	}

	protected override System.Type GetBeanType ()
	{
		return typeof(FlowDiMianFireBean);
	}

	public FlowDiMianFireBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (FlowDiMianFireBean)dataBean;
		}else{
			return null;
		}
	}

	public FlowDiMianFireBean GetDataByRow(int row)
	{
		IDataBean dataBean = _GetDataByRow(row);

		if(dataBean!=null)
		{
			return (FlowDiMianFireBean)dataBean;
		}else{
			return null;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class FlowFeiYiFireMgr : DataMgrBase<FlowFeiYiFireMgr> {

    public FlowFeiYiFireMgr()
    {
		InitData ();
	}

	protected override string GetXlsxPath ()
	{
		return "FlowFeiYiFire";
	}

	protected override System.Type GetBeanType ()
	{
		return typeof(FlowFeiYiFireBean);
	}

	public FlowFeiYiFireBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (FlowFeiYiFireBean)dataBean;
		}else{
			return null;
		}
	}

	public FlowFeiYiFireBean GetDataByRow(int row)
	{
		IDataBean dataBean = _GetDataByRow(row);

		if(dataBean!=null)
		{
			return (FlowFeiYiFireBean)dataBean;
		}else{
			return null;
		}
	}
}

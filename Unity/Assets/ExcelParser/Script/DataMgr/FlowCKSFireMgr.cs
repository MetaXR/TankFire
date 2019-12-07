using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class FlowCKSFireMgr : DataMgrBase<FlowCKSFireMgr> {

    public FlowCKSFireMgr()
    {
		InitData ();
	}

	protected override string GetXlsxPath ()
	{
		return "FlowCKSFire";
	}

	protected override System.Type GetBeanType ()
	{
		return typeof(FlowCKSFireBean);
	}

	public FlowCKSFireBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (FlowCKSFireBean)dataBean;
		}else{
			return null;
		}
	}

	public FlowCKSFireBean GetDataByRow(int row)
	{
		IDataBean dataBean = _GetDataByRow(row);

		if(dataBean!=null)
		{
			return (FlowCKSFireBean)dataBean;
		}else{
			return null;
		}
	}
}

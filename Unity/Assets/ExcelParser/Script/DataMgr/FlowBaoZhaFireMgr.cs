using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class FlowBaoZhaFireMgr : DataMgrBase<FlowBaoZhaFireMgr> {

    public FlowBaoZhaFireMgr()
    {
		InitData ();
	}

	protected override string GetXlsxPath ()
	{
		return "FlowBaoZhaFire";
	}

	protected override System.Type GetBeanType ()
	{
		return typeof(FlowBaoZhaFireBean);
	}

	public FlowBaoZhaFireBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (FlowBaoZhaFireBean)dataBean;
		}else{
			return null;
		}
	}

	public FlowBaoZhaFireBean GetDataByRow(int row)
	{
		IDataBean dataBean = _GetDataByRow(row);

		if(dataBean!=null)
		{
			return (FlowBaoZhaFireBean)dataBean;
		}else{
			return null;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class WDXFireFlowMgr : DataMgrBase<WDXFireFlowMgr> {

    public WDXFireFlowMgr()
    {
		InitData ();
	}

	protected override string GetXlsxPath ()
	{
		return "WDXFireFlow";
	}

	protected override System.Type GetBeanType ()
	{
		return typeof(WDXFireFlowBean);
	}

	public WDXFireFlowBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (WDXFireFlowBean)dataBean;
		}else{
			return null;
		}
	}

	public WDXFireFlowBean GetDataByRow(int row)
	{
		IDataBean dataBean = _GetDataByRow(row);

		if(dataBean!=null)
		{
			return (WDXFireFlowBean)dataBean;
		}else{
			return null;
		}
	}
}

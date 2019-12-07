using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class LabInfoMgr : DataMgrBase<LabInfoMgr> {

    public LabInfoMgr()
    {
		InitData ();
	}

	protected override string GetXlsxPath ()
	{
		return "LabInfo";
	}

	protected override System.Type GetBeanType ()
	{
		return typeof(LabInfoBean);
	}

	public LabInfoBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (LabInfoBean)dataBean;
		}else{
			return null;
		}
	}

	public LabInfoBean GetDataByRow(int row)
	{
		IDataBean dataBean = _GetDataByRow(row);

		if(dataBean!=null)
		{
			return (LabInfoBean)dataBean;
		}else{
			return null;
		}
	}
}

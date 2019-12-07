using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class CommEquipMgr : DataMgrBase<CommEquipMgr> {

    public CommEquipMgr()
    {
		InitData ();
	}

	protected override string GetXlsxPath ()
	{
		return "CommEquip";
	}

	protected override System.Type GetBeanType ()
	{
		return typeof(CommEquipBean);
	}

	public CommEquipBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (CommEquipBean)dataBean;
		}else{
			return null;
		}
	}

	public CommEquipBean GetDataByRow(int row)
	{
		IDataBean dataBean = _GetDataByRow(row);

		if(dataBean!=null)
		{
			return (CommEquipBean)dataBean;
		}else{
			return null;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class DBCommonTextMgr : DataMgrBase<DBCommonTextMgr> {

    public DBCommonTextMgr()
    {
		InitData ();
	}

	protected override string GetXlsxPath ()
	{
		return "DBCommonText";
	}

	protected override System.Type GetBeanType ()
	{
		return typeof(DBCommonTextBean);
	}

	public DBCommonTextBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (DBCommonTextBean)dataBean;
		}else{
			return null;
		}
	}

	public DBCommonTextBean GetDataByRow(int row)
	{
		IDataBean dataBean = _GetDataByRow(row);

		if(dataBean!=null)
		{
			return (DBCommonTextBean)dataBean;
		}else{
			return null;
		}
	}
}

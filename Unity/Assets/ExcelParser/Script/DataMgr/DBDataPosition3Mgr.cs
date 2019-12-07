using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class DBDataPosition3Mgr : DataMgrBase<DBDataPosition3Mgr> {

    public DBDataPosition3Mgr()
    {
		InitData ();
	}

	protected override string GetXlsxPath ()
	{
		return "DBDataPosition3";
	}

	protected override System.Type GetBeanType ()
	{
		return typeof(DBDataPosition3Bean);
	}

	public DBDataPosition3Bean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (DBDataPosition3Bean)dataBean;
		}else{
			return null;
		}
	}

	public DBDataPosition3Bean GetDataByRow(int row)
	{
		IDataBean dataBean = _GetDataByRow(row);

		if(dataBean!=null)
		{
			return (DBDataPosition3Bean)dataBean;
		}else{
			return null;
		}
	}
}

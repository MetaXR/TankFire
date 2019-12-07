using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExcelParser;

public partial class GeneralRuleMgr : DataMgrBase<GeneralRuleMgr> {

    public GeneralRuleMgr()
    {
		InitData ();
	}

	protected override string GetXlsxPath ()
	{
		return "GeneralRule";
	}

	protected override System.Type GetBeanType ()
	{
		return typeof(GeneralRuleBean);
	}

	public GeneralRuleBean GetDataById(object id)
	{
		IDataBean dataBean = _GetDataById(id);

		if(dataBean!=null)
		{
			return (GeneralRuleBean)dataBean;
		}else{
			return null;
		}
	}

	public GeneralRuleBean GetDataByRow(int row)
	{
		IDataBean dataBean = _GetDataByRow(row);

		if(dataBean!=null)
		{
			return (GeneralRuleBean)dataBean;
		}else{
			return null;
		}
	}
}

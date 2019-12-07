using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExcelParser;
using System;
using System.Reflection;

public partial class TankMgr
{
    public static TankMgr Instance;
    public void InitList()
    {
        Type beanType = GetBeanType();
        //MemberInfo mInfo = beanType.GetMembers("TankBean");
        //Type[] beanTypes = Assembly.Load("TankBean").GetTypes();
        MemberInfo[] memberInfo = beanType.GetMembers();
        foreach (var var in memberInfo)
        {
            Debug.Log(var.Name + ":memberInfo.name"+var.MemberType.ToString()+"type");
        }
    }

    public string[] GetStrbyCellID(int id, int cellid)
    {
        IDataBean dataBean = _GetDataById(id);
        string[] cellstr = new string[2];

        if (dataBean != null)
        {
            TankBean item = (TankBean)dataBean;
            item.CellList[0] = item.Cell1;
            item.CellList[1] = item.Cell2;
            item.CellList[2] = item.Cell3;
            item.CellList[3] = item.Cell4;

            item.DescList[0] = item.Desc1;
            item.DescList[1] = item.Desc2;
            item.DescList[2] = item.Desc3;
            item.DescList[3] = item.Desc4;
            for (int i = 0; i < item.Cellcount; i++)
            {
                if (i == cellid)
                {
                    cellstr[0] = item.CellList[i];
                    cellstr[1] = item.DescList[i];
                    return cellstr;
                }
            }
            return null;
        }
        else
        {
            return null;
        }
    }
}

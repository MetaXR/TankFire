using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EquipSetting : MonoBehaviour
{
    public int itemID;
    public Button confirmBtn;
    public Button closedBtn;
    public Text txtTitle;
    public Text txtContent;
    public InputField InputVectory;
    public InputField InputValue;
    public Dictionary<string,int> SetDic=new Dictionary<string, int>();

    public void InitData(UnityAction func, int id)
    {
        itemID = id;
        FireItemMgr db=new FireItemMgr();
        FireItemBean dbBean = db.GetDataById(itemID);
        txtTitle.text = dbBean.Title;
        txtContent.text = "设备射速：3-10(m/s)；设备流量：20-100(l/s)";
        confirmBtn.onClick.AddListener(ConfirmClick);
        confirmBtn.onClick.AddListener(func);
        closedBtn.onClick.AddListener(delegate() { Destroy(this.gameObject); });
    }

    private void ConfirmClick()
    {
        int vectory = 8;
        int value = 50;
        if (string.IsNullOrEmpty(InputValue.text) || string.IsNullOrEmpty(InputVectory.text))
        {
            vectory = 8;
            value = 50;
        }
        vectory = System.Convert.ToInt32(InputVectory.text);
        value = System.Convert.ToInt32(InputValue.text);
        vectory = Mathf.Clamp(vectory, 3, 10);
        value = Mathf.Clamp(value, 20, 100);
        SetDic.Add("Vectory",vectory);
        SetDic.Add("Value",value);
        WDXFireMgr.Instance.SetSettingDic(SetDic);
        Destroy(this.gameObject);
    }
}

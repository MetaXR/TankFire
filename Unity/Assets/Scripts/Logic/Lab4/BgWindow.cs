using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Model;

public class BgWindow : MonoBehaviour
{
    public Button startBtn;
    public Button backBtn;
    public Button SettingBtn;
    public Text txtTitle;
    public Text txtContent;
    public ToggleGroup ItemParent;
    private int SelectItemID;
    public GameObject panelBg;
    private GameObject settingWin;
    void Start()
    {
        //backBtn.onClick.AddListener(delegate() { });
    }

    public void InitData(UnityAction func,string title,string content)
    {
        txtTitle.text = title;
        txtContent.text = content;
        startBtn.onClick.AddListener(func);
    }

    public void InitData(UnityAction func, UnityAction backFunc, string title, string content)
    {
        txtTitle.text = title;
        txtContent.text = content;
        startBtn.onClick.AddListener(func);
        backBtn.onClick.AddListener(backFunc);
    }

    public void InitDataDouble(UnityAction func, UnityAction backFunc,FireItemMgr db,bool ifBg)
    {
        txtTitle.text = "提示";
        txtContent.text = "请选择使用的液体";
        SettingBtn.gameObject.SetActive(true);
        startBtn.GetComponent<RectTransform>().anchoredPosition=new Vector3(129,20,0);
        SettingBtn.GetComponent<RectTransform>().anchoredPosition = new Vector3(-129, 20, 0);
        SettingBtn.onClick.AddListener(delegate() { EquipSettingWin(func); });
        startBtn.onClick.AddListener(delegate() { Destroy(this.gameObject); });
        backBtn.onClick.AddListener(backFunc);
        ToggleGroup toggleGroup = ItemParent.GetComponent<ToggleGroup>();
        for (int i = 1; i < 3; i++)
        {
            GameObject obj = Instantiate(Resources.Load(FilePath.uiPath + "UIItem")) as GameObject;
            obj.transform.parent = ItemParent.transform;
            obj.transform.localPosition=new Vector3(130+(i-1)*200,0,0);
            obj.transform.localScale=Vector3.one;
            UIItem uiItem = obj.GetComponent<UIItem>();
            FireItemBean dbBean = db.GetDataById(i);
            if (dbBean == null)
            {
                continue;
            }
            uiItem.InitUIItem(dbBean.Id,dbBean.ImgPath, dbBean.Title, false, toggleGroup);
        }
        panelBg.SetActive(ifBg);
    }

    public int CheckToggleGroup()
    {
        ToggleGroup toggleGroup = ItemParent.GetComponent<ToggleGroup>();
        IEnumerable<Toggle> toggles = toggleGroup.ActiveToggles();
        foreach (var tog in toggles)
        {
            if (tog == null)
            {
                continue;
            }
            SelectItemID=tog.gameObject.transform.parent.gameObject.GetComponent<UIItem>().ItemID;
        }
        return SelectItemID;
    }

    private void EquipSettingWin(UnityAction call)
    {
        int itemID = CheckToggleGroup();
        if (settingWin == null)
        {
            settingWin = Instantiate(Resources.Load(FilePath.uiPath + "Panel_EquipSetting")) as GameObject;
            settingWin.transform.parent = gameObject.transform;
            settingWin.transform.localPosition=Vector3.zero;
            settingWin.transform.localScale=Vector3.one;
        }
        EquipSetting equipSetting = settingWin.GetComponent<EquipSetting>();
        equipSetting.InitData(call, itemID);
    }

    void SendEquipInit()
    {
        //SessionComponent.Instance.Session.Send();
    }
}

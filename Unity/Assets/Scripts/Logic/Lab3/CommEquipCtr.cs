using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommEquipCtr : MonoBehaviour
{
    public Text tTitle;
    public Text rTitle;
    public Text rDesc;
    public Text lTitle;
    public Text lDesc;
    public Transform modelPiovt;
    private CommEquipMgr commEuipDB;
    public Transform bottomPanel;
    public GameObject prefabEquipUI;
    public Button menueBtn;
	// Use this for initialization
	void Start ()
	{
        menueBtn.onClick.AddListener(delegate() { SceneLoading.GetInstance().ChangeToScene(SceneName.Main); });
        Client.Instance.changeCommEquip += this.SetEuip;
        commEuipDB = new CommEquipMgr();
        CommEquipBean item = commEuipDB.GetDataById(Random.Range(0, commEuipDB.count));
	    if (item == null)
	    {
            item = commEuipDB.GetDataById(1);
	    }
	    SetInfo(item.Title, item.Title, item.Desc, item.Title, item.Desc);
        GameObject obj = Instantiate(Resources.Load(item.Prefab) as GameObject);
        obj.transform.parent = modelPiovt;
        obj.transform.localPosition = new Vector3(0,0,0);
        RefreshUI();
    }
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    void RefreshUI()
    {
       
            for (int i = 1; i < commEuipDB.count+1; i++)
            {
                CommEquipBean item = commEuipDB.GetDataById(i);
                GameObject obj = Instantiate(prefabEquipUI);
                EquipItem equpItem = obj.GetComponent<EquipItem>();
                obj.transform.parent = bottomPanel;
                obj.transform.localScale = new Vector3(1, 1, 1);
                equpItem.SetData(item.Id,item.Title,item.ImgPath,item.Desc);
            }
       
    }

    public void SetInfo(string title, string rtitle, string rdesc, string ltitle, string ldesc)
    {
        tTitle.text = title;
        rTitle.text = rtitle;
        rDesc.text = rdesc;
        lTitle.text = ltitle;
        lDesc.text = ldesc;
    }

    public void SetEuip(int id)
    {
        CommEquipBean item = commEuipDB.GetDataById(id);
        SetInfo(item.Title, item.Title, item.Desc, item.Title, item.Desc);
        string[] testName = item.Prefab.Split('/');
        int idx = 0;
        for (int i = 0; i < modelPiovt.childCount; i++)
        {
            GameObject child = modelPiovt.GetChild(i).gameObject;
            string name = modelPiovt.GetChild(i).name;
            Debug.Log(testName[1]);
            if (testName[1]+"(Clone)" == child.name)
            {
                child.gameObject.SetActive(true);
                idx++;
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
        if (idx <= 0)
        {
            GameObject obj = Instantiate(Resources.Load(item.Prefab) as GameObject);
            obj.transform.parent = modelPiovt;
            obj.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    void OnDestroy()
    {
        Client.Instance.changeCommEquip -= this.SetEuip;
        Debug.Log("CommEquip ONDestroy!");
    }
}

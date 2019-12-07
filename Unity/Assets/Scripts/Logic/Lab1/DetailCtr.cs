using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DetailCtr : MonoBehaviour
{
    public Text tTitle;
    public Text rTitle;
    public Text rDesc;
    public Text lTitle;
    public Text lDesc;
    public GameObject tankPivot;
    public GameObject btnPanel;
    public GameObject btnPrefabs;
	void Start ()
	{
	    Client.Instance.setTankCell += this.SetCellInfo;
        TankMgr db = new TankMgr();
	    for (int i = 1; i < db.count+1; i++)
	    {
            TankBean temp = db.GetDataById(i);
            GameObject btnUI = Instantiate(btnPrefabs) as GameObject;
	        btnUI.transform.parent = btnPanel.transform;
            btnUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(20, 20+40*(i-1));
            btnUI.transform.localScale=new Vector3(0.8f,0.8f,0.8f);
	        btnUI.transform.GetChild(0).GetComponent<Text>().text = temp.Title;
            Debug.Log(i.ToString());
            btnUI.GetComponent<Button>().onClick.AddListener(delegate() { SetEuip(temp.Id);});
	    }
        TankBean item = db.GetDataById(Random.Range(0, db.count));
	    if (item == null)
	    {
	        item = db.GetDataById(1);
	    }
	    SetInfo(item.Title, item.Cell1, item.Desc1, item.Title, item.Desc);
        GameObject obj = Instantiate(Resources.Load(item.Prefab) as GameObject);
	    obj.transform.parent = tankPivot.transform;
        obj.transform.localPosition=new Vector3(0,obj.transform.localPosition.y,0);
	}

    void SetEuip(int modelId)
    {
        Debug.Log(modelId.ToString());
        int id = modelId;
        TankMgr db = new TankMgr();
        TankBean item = db.GetDataById(id);
        SetInfo(item.Title, item.Cell1, item.Desc1, item.Title, item.Desc);
        string[] testName = item.Prefab.Split('/');
        int idx = 0;
        for (int i = 0; i < tankPivot.transform.childCount; i++)
        {
            GameObject child = tankPivot.transform.GetChild(i).gameObject;
            if (testName[1] + "(Clone)" == child.name)
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
            obj.transform.parent = tankPivot.transform;
            obj.transform.localPosition = new Vector3(0, obj.transform.localPosition.y, 0);
        }
        
    }

    public void SetInfo(string title,string rtitle,string rdesc,string ltitle,string ldesc)
    {
        tTitle.text =  title;
        rTitle.text = rtitle;
        rDesc.text = rdesc;
        lTitle.text = ltitle;
        lDesc.text = ldesc;
    }

    public void SetCellInfo(int modelID,int cellID)
    {
        TankMgr db = new TankMgr();
        TankBean item = db.GetDataById(modelID);
        string[] str = new string[2];
        str = db.GetStrbyCellID(modelID, cellID);
        SetInfo(item.Title, str[0], str[1], item.Title, item.Desc);
    }

    public void ClosedBtn()
    {
        Debug.Log("closed btn");
        GlobalDate.GetInstance().ModelID = 0;
        SceneManager.LoadSceneAsync(SceneName.Lab1);
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy");
        //GlobalDate.GetInstance().ModelID = 0;
        Client.Instance.setTankCell -= this.SetCellInfo;
    }
}

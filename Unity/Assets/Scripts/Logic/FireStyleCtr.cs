using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MirzaBeig.VFX;

public class FireStyleCtr : MonoBehaviour
{
    public List<ParticleSystems> FXList = new List<ParticleSystems>();
    public Dictionary<string, ParticleSystems> FXDic = new Dictionary<string, ParticleSystems>();
    public List<Button> BtnList = new List<Button>();
    public Dictionary<Button, ParticleSystems> BtnDic = new Dictionary<Button, ParticleSystems>();
    private Dictionary<string, Vector3> PosList = new Dictionary<string, Vector3>();
    public BtnMove FireInfoWin;
    int lastid=0;
    public DetailCameraCtr CurCam;
    public Button menueBtn;
    void Awake()
    {
        PosList.Add("orign", FireInfoWin.transform.localPosition);
        PosList.Add("des", new Vector3(FireInfoWin.transform.localPosition.x, FireInfoWin.transform.localPosition.y+120, FireInfoWin.transform.localPosition.z));
        FireInfoWin.Init(PosList);
    }
    void Start ()
    {
        menueBtn.onClick.AddListener(delegate () { SceneLoading.GetInstance().ChangeToScene(SceneName.Main); });
        for (int i = 0; i < FXList.Count; i++)
        {
            FXDic.Add(FXList[i].gameObject.name, FXList[i]);
        }
        for (int i = 0; i < BtnList.Count; i++)
        {
            BtnDic.Add(BtnList[i], FXList[i]);
        }
        for (int i = 0; i < BtnList.Count; i++)
        {
            int k = i;
            BtnList[i].onClick.AddListener(delegate() { SetFxOnByName(FXList[k].gameObject); });
            int j = i+1;
            BtnList[i].onClick.AddListener(delegate () { SetFireInfoById(j);});
        }

        Init();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Init()
    {
        SetFxOnByName(FXList[0].gameObject);
        SetFireInfoById(1);
    }

    /// <summary>
    /// reset all fx to init state.
    /// </summary>
    void ResetFx()
    {
        for (int i = 0; i < FXList.Count; i++)
        {
            FXList[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// set the selected one Fx on by FxGameObject name
    /// </summary>
    /// <param name="name">FX gameobj name</param>
    void SetFxOnByName(GameObject obj)
    {
        CurCam.targetObject = obj.transform;
        if (FXDic.ContainsKey(obj.name))
        {
            ResetFx();
            FXDic[obj.name].gameObject.SetActive(true);
            FXDic[obj.name].play();
        }
    }

    /// <summary>
    /// set the top tips by fireid
    /// </summary>
    /// <param name="id"></param>
    void SetFireInfoById(int id)
    {        
        FireStyleInfoMgr db = new FireStyleInfoMgr();
        FireStyleInfoBean fireBean = db.GetDataById(id);       
        FireInfoWin.GetComponent<FireInfoWinCtr>().SetData(fireBean);
        if (!FireInfoWin.init)
        { FireInfoWin.Init(PosList); }
        else if (lastid == id)
        {
            FireInfoWin.MoveBtnY();
        }
        else
        {
            FireInfoWin.MoveBtnY_Show();
        }
       
        lastid = id;
    }    
}

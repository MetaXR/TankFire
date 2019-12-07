using System;
using UnityEngine;
using UnityEngine.UI;
using MirzaBeig.VFX;

public class LabCoolCtr : MonoBehaviour
{
    public Button menueBtn;
    public Button CalCulateBtn;
    public Button PenLinBtn;
    public Button WaterGunBtn;
    public ParticleSystems FXPenLin;
    public ParticleSystems FXWaterGun;

    public GameObject CalPanel;
    public InputField InNearTankDia_a;
    public InputField InNearTankDia_b;
    public InputField InNearTankDia_c;
    public InputField InNearTankDia_d;
    public InputField InNearTankDia_e;
    public InputField InNearTankDia_time;

    public Text Res_gun;
    public Text Res_Water;

    public Button CalBtn;
    public Button ClearBtn;
    public Button CloseBtn;

    public Text Text_Tip;

    void Start()
    {
        menueBtn.onClick.AddListener(delegate () { SceneLoading.GetInstance().ChangeToScene(SceneName.Main); });
        CalBtn.onClick.AddListener(CalculateSupplyValue);
        ClearBtn.onClick.AddListener(Clear);
        CloseBtn.onClick.AddListener(ClosePanel);
        CalCulateBtn.onClick.AddListener(OpenCalPanel);
        PenLinBtn.onClick.AddListener(UpPMClick);
        WaterGunBtn.onClick.AddListener(DownPMClick);
        FXWaterGun.stop();
        FXPenLin.stop();
        CalPanel.SetActive(false);
    }
   
    public void OverLookClick()
    {
        //WintipObj.SetActive(true);
        FXWaterGun.stopImediate();
        FXPenLin.stopImediate();
        ClosePanel();
    }

    public void DownPMClick()
    {
        //WintipObj.SetActive(false);
        FXWaterGun.play();
        FXPenLin.stopImediate();
        ClosePanel();
    }

    public void UpPMClick()
    {
        //WintipObj.SetActive(false);
        FXWaterGun.stopImediate();
        FXPenLin.play();
        ClosePanel();
    }

    void OpenCalPanel()
    {
        CalPanel.SetActive(true);
    }

    void ClosePanel()
    {
        CalPanel.SetActive(false);
    }

    void CalculateSupplyValue()
    {
        if (InNearTankDia_a.text == string.Empty || InNearTankDia_b.text == string.Empty || InNearTankDia_c.text == string.Empty
            || InNearTankDia_c.text == string.Empty || InNearTankDia_d.text == string.Empty || InNearTankDia_e.text == string.Empty
            || InNearTankDia_time.text == string.Empty)
        {
            Text_Tip.text = string.Format("<color=#DA0000FF>{0}</color>", "输入为空！请检查后重新输入");
        }
        float res_gun;
        float res_water;

        res_gun = (Convert.ToSingle(InNearTankDia_a.text) * (3.1415f)) / 8 + (Convert.ToSingle(InNearTankDia_b.text) * (3.1415f)) / 16
            + (Convert.ToSingle(InNearTankDia_c.text) * (3.1415f)) / 16 + (Convert.ToSingle(InNearTankDia_d.text) * (3.1415f)) / 16
            + (Convert.ToSingle(InNearTankDia_e.text) * (3.1415f)) / 16;
        res_water = res_gun * 7.5f * Convert.ToSingle(InNearTankDia_time.text) * 60 / 1000;
        Res_gun.text = Convert.ToInt32(res_gun).ToString();
        Res_Water.text = Convert.ToDouble(res_water).ToString();
        Text_Tip.text = string.Format("<color=#00A5DBFF>{0}</color>", "提示：计算得出2个结果，左侧：冷却枪支；右侧：灭火用水量。");
    }

    public void Clear()
    {
        InNearTankDia_a.text = string.Empty;
        InNearTankDia_b.text = string.Empty;
        InNearTankDia_c.text = string.Empty;
        InNearTankDia_d.text = string.Empty;
        InNearTankDia_e.text = string.Empty;
        InNearTankDia_time.text = string.Empty;
        Res_gun.text = string.Empty;
        Res_Water.text = string.Empty;
        Text_Tip.text = string.Format("<color=#B4AF00FF>{0}</color>", "提示：计算得出2个结果，左侧：冷却枪支；右侧：灭火用水量。");
    }

    public void OnMenueBtn()
    {
        SceneLoading.GetInstance().ChangeToScene(SceneName.Main);
    }

   

}

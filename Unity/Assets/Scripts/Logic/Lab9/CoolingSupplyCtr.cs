using System ;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolingSupplyCtr : MonoBehaviour
{
    public Button FireLevelBtn;
    public Button ControlAreaBtn;
    public Button WaterSupplyValueBtn;
    public GameObject PanelFireLevel;
    public GameObject PanelControlArea;
    public GameObject PanelWaterSupply;
    public InputField InputWaterVolume;
    public InputField InputMpa;
    public InputField InputArea;
    public Text TextResult;
    public Text TextResultUnit;
    public ToggleGroup TogGroup;
    public Toggle TogConArea;
    public Toggle TogWaterSupply;
    public Button CalculateBtn;

	void Start () 
    {
		FireLevelBtn.onClick.AddListener(FireLevelBtnClick);
        WaterSupplyValueBtn.onClick.AddListener(WaterSupplyValueClick);
        CalculateBtn.onClick.AddListener(CalculateSupplyValue);
        TogConArea.onValueChanged.AddListener(OnTogConAreaClick);
        TogWaterSupply.onValueChanged.AddListener(OnTogWaterSupplyClick);
	}
    void FireLevelBtnClick()
    {
        PanelFireLevel.SetActive(!PanelFireLevel.activeSelf);
    }
    void WaterSupplyValueClick()
    {
        PanelWaterSupply.SetActive(!PanelWaterSupply.activeSelf);
    }

    void CalculateSupplyValue()
    {
        float result;
        IEnumerable<Toggle> togs = TogGroup.ActiveToggles();
        foreach (var tog in togs)
        {
            if (tog == null)
            {
                continue;
            }
            if (tog == TogConArea)
            {
                result = Convert.ToSingle(InputWaterVolume.text) / Convert.ToSingle(InputMpa.text);
                TextResult.text = result.ToString();
               
            }
            if (tog == TogWaterSupply)
            {
                result = Convert.ToSingle(InputArea.text) * Convert.ToSingle(InputMpa.text);
                TextResult.text = result.ToString();
              
            }
        }
    }

    public void ClosePanel(GameObject panel)
    { 
        panel.SetActive(false);
    }

    public void Clear()
    {
        TextResult.text = string.Empty;
    }

    public void OnTogConAreaClick(bool on)
    {
        InputArea.gameObject.SetActive(!on);
        InputWaterVolume.gameObject.SetActive(on);
        TextResultUnit.text = string.Format("单位:{0}", "M2(平方米)");
    }

    public void OnTogWaterSupplyClick(bool on)
    {
        InputArea.gameObject.SetActive(on);
        InputWaterVolume.gameObject.SetActive(!on);
        TextResultUnit.text = string.Format("单位:{0}", "L/S(升/秒)");
    }

    public void OnMenueBtn()
    {
        SceneLoading.GetInstance().ChangeToScene(SceneName.Main);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySlider : MonoBehaviour
{
    public static EnergySlider instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        // DontDestroyOnLoad(gameObject);
    }


    public float sliderEnergy = 0f;

	public Image sliderFront1;
	public Image sliderFront2;

    public void ChangeEnergyBar()
    {
		float a = StageInfoManager.instance.energy;
		float b = StageInfoManager.instance.StageEnergy[StageInfoManager.instance.stageCount];
		
		sliderEnergy = a / b; //현재 진행도  //물론 -도 포함.

		sliderFront1.fillAmount = (sliderEnergy * 2) * 0.95f + 0.05f;
		sliderFront2.fillAmount = (sliderEnergy * 2 - 1f);
    }
}

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
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
        // DontDestroyOnLoad(gameObject);
    }


    public float sliderEnergy = 0f;

	public Image sliderFront1;
	public Image sliderFront2;

    public void ChangeEnergyBar()
    {
		float a = StageInfoManager.instance.energy;
		float b = StageInfoManager.instance.StageEnergy[StageInfoManager.instance.stageCount];
		
		sliderEnergy = a / b; //���� ���൵  //���� -�� ����.

		sliderFront1.fillAmount = (sliderEnergy * 2) * 0.95f + 0.05f;
		sliderFront2.fillAmount = (sliderEnergy * 2 - 1f);
    }
}

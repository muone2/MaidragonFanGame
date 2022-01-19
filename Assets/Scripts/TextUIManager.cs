using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUIManager : MonoBehaviour
{
    public static TextUIManager instance;

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

    public Text energyText;
    public Text heartText;
    public GameObject TextEffectPrefeb;
    public GameObject slotMachineManagerObj;

    GameObject tmp;

    public void AddEnergy(int slotScore)
    {
        StageInfoManager.instance.energy += slotScore;
        changeEnergyText();
        EnergySlider.instance.ChangeEnergyBar();
    }
    public void AddSlotUseCount()
    {
        StageInfoManager.instance.slotUseCount += 1;
        changeHeartText();
    }

    public void changeEnergyText()
    {

        energyText.text = StageInfoManager.instance.energy + " / " + StageInfoManager.instance.StageEnergy[StageInfoManager.instance.stageCount];

        /*
        if (energy < -100)
            energyText.text = "Energy : -99" + "\n" + "Count : " + slotUseCount;
        else if (energy < -10)
            energyText.text = "Energy : -" + (-energy) + "\n" + "Count : " + slotUseCount;
        else if (energy < 0)
            energyText.text = "Energy : -0" + (-energy) + "\n" + "Count : " + slotUseCount;
        else if (energy < 10)
            energyText.text = "Energy : 00" + energy + "\n" + "Count : " + slotUseCount;
        else if (energy < 100)
            energyText.text = "Energy : 0" + energy + "\n" + "Count : " + slotUseCount;
        else
            energyText.text = "Energy : " + energy + "\n" + "Count : " + slotUseCount;
         */
    }


    public void changeHeartText()
    {
        heartText.text = (StageInfoManager.instance.StageHeart[StageInfoManager.instance.stageCount] - StageInfoManager.instance.slotUseCount) + 
            " / " + StageInfoManager.instance.StageHeart[StageInfoManager.instance.stageCount];
    }

    public void makeTextEffectSlotScore(GameObject targetObj, int score)
    {
        tmp = Instantiate(TextEffectPrefeb);

        if (score < -10) //������ ��
            tmp.GetComponent<Text>().text = "-" + (-score);
        else if (score < 0) //������ ��
            tmp.GetComponent<Text>().text = "-0" + (-score);
        else if (score < 10)
            tmp.GetComponent<Text>().text = "+0" + score;
        else if (score < 100)
            tmp.GetComponent<Text>().text = "+" + score;

        tmp.transform.parent = targetObj.transform;
        tmp.transform.localPosition = Vector3.zero;
        tmp.transform.parent = slotMachineManagerObj.transform; //��ư�� ���̸� ��ư ����ũ���� ���� �޾Ƽ� �ٲ�
        tmp.transform.localScale = Vector3.one;  //�ٸ� ������ �ͼ� �׷��� ���� �������� �ٲ�淡 ���� �ʱ�ȭ

        //��� ��ü �ڵ忡�� �˾Ƽ� �����̰� �˾Ƽ� ���� �� ��
    }
}

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
    public GameObject TextEffectPrefeb;
    public GameObject slotMachineManagerObj;

    public int slotUseCount = 0;
    public int energy = 0;
    GameObject tmp;

    public void AddEnergy(int slotScore)
    {
        energy += slotScore;
        changeEnergyText();
    }

    public void changeEnergyText()
    {
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
        else
            tmp.GetComponent<Text>().text = "+er";

        tmp.transform.parent = targetObj.transform;
        tmp.transform.localPosition = Vector3.zero;
        tmp.transform.parent = slotMachineManagerObj.transform; //��ư�� ���̸� ��ư ����ũ���� ���� �޾Ƽ� �ٲ�
        tmp.transform.localScale = Vector3.one;  //�ٸ� ������ �ͼ� �׷��� ���� �������� �ٲ�淡 ���� �ʱ�ȭ

        //��� ��ü �ڵ忡�� �˾Ƽ� �����̰� �˾Ƽ� ���� �� ��
    }
}

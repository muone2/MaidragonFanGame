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
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
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

        if (score < -10) //음수일 때
            tmp.GetComponent<Text>().text = "-" + (-score);
        else if (score < 0) //음수일 때
            tmp.GetComponent<Text>().text = "-0" + (-score);
        else if (score < 10)
            tmp.GetComponent<Text>().text = "+0" + score;
        else if (score < 100)
            tmp.GetComponent<Text>().text = "+" + score;
        else
            tmp.GetComponent<Text>().text = "+er";

        tmp.transform.parent = targetObj.transform;
        tmp.transform.localPosition = Vector3.zero;
        tmp.transform.parent = slotMachineManagerObj.transform; //버튼의 밑이면 버튼 마스크에도 영향 받아서 바꿈
        tmp.transform.localScale = Vector3.one;  //다른 곳에서 와서 그런지 로컬 스케일이 바뀌길래 강제 초기화

        //얘는 자체 코드에서 알아서 움직이고 알아서 삭제 될 것
    }
}

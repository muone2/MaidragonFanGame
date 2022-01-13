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
    int energy = 0;

    public void AddEnergy(int slotScore)
    {
        energy += slotScore;
        changeEnergyText();
    }

    public void changeEnergyText()
    {
        if (energy < 10)
            energyText.text = "energy : 00" + energy;
        else if (energy < 100)
            energyText.text = "energy : 0" + energy;
        else if (energy < 1000)
            energyText.text = "energy : " + energy;
        else if (energy >= 1000)
            energyText.text = "energy : 999";
    }





}

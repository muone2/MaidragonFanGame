using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfoManager : MonoBehaviour
{
    public static StageInfoManager instance;
    public GameObject dialogManagerObj;

    public GameObject badendingPanel1;
    public GameObject goodendingPanel1;

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
        dialogManagerObj.SetActive(true);
    }

    public int stageCount = 0; //���� �� ������������.
    public int[] StageEnergy ; //��, 0�� ���
    public int[] StageHeart ;

    public int slotUseCount = 0;
    public int energy = 0;


    public void StageClearCheck()
    {   
        if (StageEnergy[stageCount] <= energy) //���
        {
            dialogManagerObj.SetActive(true);
        }
        else if (StageEnergy[stageCount] > energy) //����
        {
            badendingPanel1.SetActive(true); //
        }
    }
}

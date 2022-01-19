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
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        // DontDestroyOnLoad(gameObject);
        dialogManagerObj.SetActive(true);
    }

    public int stageCount = 0; //현재 몇 스테이지인지.
    public int[] StageEnergy ; //즉, 0은 허상
    public int[] StageHeart ;

    public int slotUseCount = 0;
    public int energy = 0;


    public void StageClearCheck()
    {   
        if (StageEnergy[stageCount] <= energy) //통과
        {
            dialogManagerObj.SetActive(true);
        }
        else if (StageEnergy[stageCount] > energy) //실패
        {
            badendingPanel1.SetActive(true); //
        }
    }
}

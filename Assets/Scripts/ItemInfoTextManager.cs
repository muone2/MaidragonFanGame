using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoTextManager : MonoBehaviour
{
    public static ItemInfoTextManager instance;

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

    public string[] ItmeInfoText = new string[50];

    public void Start()
    {
        for (int i = 0; i < ItmeInfoText.Length; i++)
        {
            ItmeInfoText[i] = null;
        }

        ItmeInfoText[0] = "빈칸 표시용 점 이미지. 근데 이건 나오면 안 되는 설명인데 왜 나왔지?";
        ItmeInfoText[1] = "= 칸나" + "\n" + "전기와 사랑이 고픈 우주 최강 고스로리 드래곤." + "\n\n" + "무엇이든 인접시 +5" + "\n" + "리코, 클로에 인접시 +10" + "\n" + "멜론빵 인접시 +10";
        ItmeInfoText[2] = "= 리코" + "\n" + "칸나 너무 좋아 보헤에에에. 자존심과 자기애가 강한 귀찮은 성격의 아이." + "\n\n" + "칸나 인접시+15 " ;
        ItmeInfoText[3] = "= 클로에" + "\n" + "칸나의 미국 친구. 엄청난 재력의 소유자." + "\n\n" + "칸나 인접시 +20" + "\n" + "리코 인접시 -6";
        ItmeInfoText[4] = "= 이루루" + "\n" + "몸에 거대한 화염 주머니를 달고 다니는 아직은 마음이 어린 드래곤." + "\n\n" + "아이들 인접시 +8" + "\n" + "코바야시 인접시 +12";
        ItmeInfoText[5] = "= 토루" + "\n" + "러브츄 페로츄 러브츄 페로츄 다이스키 코바야시상~♪ 최강의 메이드래곤" + "\n\n" + "코바야시 인접시 +40" + "\n" + "엘마, 이루루 인접시 -11";

        for (int i = 0; i < ItmeInfoText.Length; i++)
        {
            if (ItmeInfoText[i] == null)
            {
                ItmeInfoText[i] = "아직 못 만듬";
            }
        }
    }
}

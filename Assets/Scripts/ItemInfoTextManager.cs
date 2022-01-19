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

    public string[] ItmeInfoText = new string[30];

    public void Start()
    {
        for (int i = 0; i < ItmeInfoText.Length; i++)
        {
            ItmeInfoText[i] = null;
        }

        ItmeInfoText[0] = "빈칸 표시용 점 이미지. 근데 이건 나오면 안 되는 설명인데 왜 나왔지?";
        ItmeInfoText[1] = "= 칸나" + "\n" + "전기와 사랑이 고픈 우주 최강 고스로리 드래곤" + "\n\n" + "무엇이든 인접시 +5" + "\n" + "리코, 클로에 인접시 +10";
        ItmeInfoText[2] = "= 리코" + "\n" + "칸나 너무 좋아 보헤에에에 "+ "\n" + "자존심과 자기애가 강해 주변을 피곤하게 만든다" + "\n\n" + "칸나 인접시 + 15 " ;
        ItmeInfoText[3] = "= 클로에" + "\n" + "칸나의 미국 친구" + "\n" + "엄청난 재력의 소유자" + "\n\n" + "칸나 인접시 +20" + "\n" + "리코 인접시 -6";
        ItmeInfoText[4] = "= 이루루" + "\n" + "아직은 마음이 어린 드래곤" + "\n" + "몸에 거대한 화염 주머니를 달고 다닌다" + "\n\n" + "아이들 인접시 +8" + "\n" + "타케, 코바야시 인접시 +12";
        ItmeInfoText[5] = "= 토르" + "\n" + "러브츄 페로츄 러브츄 페로츄 다이스키 코바야시상~~~" + "\n" + "혼돈 세력 최강의 메이드래곤" + "\n\n" + "코바야시 인접시 +40" + "\n" + "엘마, 이루루 인접시 -13";
        ItmeInfoText[6] = "= 엘마" + "\n" + "책임감이 강한 조화 세력의 드래곤" + "\n" + "인간이 만든 음식을 좋아한다" + "\n\n" + "코바야시 인접시 +17" + "\n" + "토르 인접시 +22";
        ItmeInfoText[7] = "= 코바야시" + "\n" + "수많은 드래곤에게 호감을 산 지고쿠메쿠리 상사주임 겸 오보로즈카 일대 수호자" + "\n\n" + "무엇이든 인접시 +5" + "\n" + "토르 인접시 +10";
        ItmeInfoText[8] = "= 파프닐" + "\n" + "보물과 재화를 좋아하며 짜증과 불만이 많은 드래곤" + "\n" + "그럴만한 힘을 가졌다" + "\n\n" + "기본 -10" + "\n" + "무엇이든 인접시 -6";
        ItmeInfoText[9] = "= 루코아" + "\n" + "술을 좋아하는 방관 세력의 드래곤" + "\n" + "예전에는 신이었으나..." + "\n\n" + "쇼타 인접시 +30";
        ItmeInfoText[10] = "= 쇼타" + "\n" + "마법사 가문의 자제이자 성실하고 재능 있는 '소년'" + "\n\n" + "루코아 인접시 +20" + "\n" + "아이들 인접시 +3";
        ItmeInfoText[11] = "= 죠지" + "\n" + "코바야시가 인정한 메이드계의 귀감이자 리코네 집의 메이드" + "\n" + "사실 리코의 언니다" + "\n\n" + "코바야시, 리코 인접시 +25";
        ItmeInfoText[12] = "= 타케" + "\n" + "꽤나 평범한 고등학생" + "\n\n" + "이루루 인접시 +10";
        ItmeInfoText[13] = "= 타키야" + "\n" + "내면에 진짜 모습을 숨기고 있는 평범한 직장인" + "\n" + "쇼타의 동경의 대상이다" + "\n\n" + "파프닐 인접시 +5";
        ItmeInfoText[14] = "= 찹쌀떡" + "\n" + "오보로즈카 시장의 명물" + "\n" + "시즌 한정 찹쌀떡 " + "\n\n" + "무엇이든 인접시 +10" + "\n" + "엘마 인접시 +10";
        ItmeInfoText[15] = "= 토르의 꼬리 구이" + "\n" + "토르의 꼬리로 만든 맛있는(?) 구이 요리" + "\n\n" + "무엇이든 인접시 +15" + "\n" + "코바야시 인접시 -10";



        for (int i = 0; i < ItmeInfoText.Length; i++)
        {
            if (ItmeInfoText[i] == null)
            {
                ItmeInfoText[i] = "아직 못 만듬";
            }
        }
    }
}

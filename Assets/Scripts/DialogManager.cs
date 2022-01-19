using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

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

    public SlotMachineManager slotMachineManager;
    public GameObject itemShopPanel;

    public Text dialogText;
    public Text nameTagText;
    public Image SayingCharImage;
    public List<string> dialog = new List<string>();
    public List<string> ItmeName = new List<string>();
    public List<int> SayingCharNum = new List<int>();

    public int dialogReadCount = 0;
    public Sprite nullSprite;
    bool first = true;

    public void OnEnable()
    {
        if (first)
        {
            first = false;
            for (int i = 0; i < 30; i++)
            {
                ItmeName.Add(null);
            }

            ItmeName[0] = " ";
            ItmeName[1] = "칸나";
            ItmeName[2] = "리코";
            ItmeName[3] = "클로에";
            ItmeName[4] = "이루루";
            ItmeName[5] = "토르";
            ItmeName[6] = "엘마";
            ItmeName[7] = "코바야시";
            ItmeName[8] = "파프닐";
            ItmeName[9] = "루코아";
            ItmeName[10] = "쇼타";
            ItmeName[11] = "죠지";
            ItmeName[12] = "타케";
            ItmeName[13] = "타키야";
            ItmeName[14] = "찹쌀떡";
            ItmeName[15] = "꼬리구이";

            for (int i = 0; i < ItmeName.Count; i++)
            {
                if (ItmeName[i] == null)
                {
                    ItmeName[i] = "나오면 안 됨";
                }
            }
        }
        StageInfoManager.instance.stageCount++ ; //대화 켜지는 순간부터가 다음 스테이지. (1스테이지는 켜지자마자니까 초기 값은 0이어야 함)
        StageInfoManager.instance.energy = 0;
        StageInfoManager.instance.slotUseCount = 0;  //스테이지에서 쌓은 정보 초기화
        dialogReadCount = 0; //대화 진행도 초기화

        if (StageInfoManager.instance.stageCount == 1)
            makeStage1Dialog();
        else if (StageInfoManager.instance.stageCount == 2)
            makeStage2Dialog();
        else if (StageInfoManager.instance.stageCount == 3)
            makeStage3Dialog();
        else if (StageInfoManager.instance.stageCount == 4)
            makeStage4Dialog();
        else if (StageInfoManager.instance.stageCount == 5)
            makeStage5Dialog();
        else if (StageInfoManager.instance.stageCount == 6)
            makeStage6Dialog();
        else
        {
            Debug.Log("STAGE COUNT errrrrrrer");
            gameObject.SetActive(false); //오브젝트를 끔
        }

        writeDialogAndImage(); //0번 대사 출력
    }

    public void makeStage1Dialog()
    {
        dialog.Clear();
        SayingCharNum.Clear();
        for (int i = 0; i < 70; i++)
        {
            dialog.Add(null);
            SayingCharNum.Add(-1);
        }

        dialog[0] = "하굣길, 코바야시네 아파트 앞";
        dialog[1] = "사이카와, 잘 가.";
        dialog[2] = "응, 내일 봐.";
        dialog[3] = "(아직 칸나랑 더 있고 싶은데... 으으... 뭔가 없을까?)";
        dialog[4] = "(...저거다!)";
        dialog[5] = "사이카와? 왜 그래?";
        dialog[6] = "응? 헤헤.";
        dialog[7] = "앗, 저거 칸나네 집 우편함 아니야? 뭔가 있는데?";
        dialog[8] = "진짜다. 뭔가 있어.";
        dialog[9] = "가보자.";
            dialog[10] = "칸나가 우편함 속의 작은 상자를 꺼낸다.";
            dialog[11] = "클로에!";
            dialog[12] = "클-로-에-?!";
            dialog[13] = "뭘 보낸 거지?";
            dialog[14] = "칸나, 그거 이리 줘. 위험한 건 아닌지 내가 먼저 뜯어볼게.";
            dialog[15] = "싫어. 내가 뜯을래.";
            dialog[16] = "앗?! 칸나! 어디 가!";
            dialog[17] = "걱정 안 해도 돼. 위험한 거 아니야.";
            dialog[18] = "(만약 위험한 거라면 인간은 약하니까 내가 확인해야 해. 아마 괜찮겠지만.)";
            dialog[19] = "위험한 거면 어쩌려고! 칸나! 멈춰!";
            dialog[20] = "칸나가 상자를 뜯자 게임기처럼 생긴 물건과 편지 봉투가 나온다.";
        dialog[21] = "칸나는 계속 달리면서 게임기를 요리조리 살펴본다.";
            dialog[22] = "역시 위험해 보이지는... 윽?!";
            dialog[23] = "등록이 완료되었습니다.";
            dialog[24] = "칸나?! 왜 그래! 괜찮아?";
            dialog[25] = "으응. 조금 어지러워서. 괜찮아.";
            dialog[26] = "정말, 내가 먼저 확인해 본다니까. 이리 줘.";
            dialog[27] = "그것 때문은 아닐 것 같은데.";
            dialog[28] = "클로에 걔는 이런 위험한 걸 칸나한테 보내면 어쩌자는 거야. 다음에 만나면 한마디 해줘야겠어.";
            dialog[29] = "조금 어지러웠을 뿐이라니까.";
            dialog[30] = "그래도! 칸나, 정말 괜찮아? 정말로?";
            dialog[31] = "봐, 멀쩡해. 다친 곳도 없어.";
            dialog[32] = "칸나가 힘차게 폴짝폴짝 뛴다.";
            dialog[33] = "그렇다면 다행이지만.";
            dialog[34] = "등록이 완료되었습니다.";
            dialog[35] = "응? 무슨 소리지? 앗, 칸나! 진짜 괜찮은 거 맞아?";
            dialog[36] = "으으. 걱정하지 마, 사이카와.";
            dialog[37] = "자꾸 그러는데 어떻게 걱정을 안 해!";
            dialog[38] = "(왠지 기운이 없어졌어. 일단 사이카와를 안심시키자. 주의를 돌려야 해.)";
            dialog[39] = "그런데 방금 소리는 뭐였지?";
            dialog[40] = "갑자기? 어... 여기서 난 것 같은데.";
            dialog[41] = "어? 나랑 칸나다. 클로에도 있고.";
            dialog[42] = "이건 게임기인가 봐. 어떻게 나랑 칸나가 나오는지는 모르겠지만.";
            dialog[43] = "셋 중에 하나를 선택하는 건가? 그럼 당연히 칸나로...";
            dialog[44] = "(휴, 잘 넘어간 것 같아.)";
        

        SayingCharNum[0] = 0;
        SayingCharNum[1] = 1;
        SayingCharNum[2] = 2;
        SayingCharNum[3] = 2;
        SayingCharNum[4] = 2;
        SayingCharNum[5] = 1;
        SayingCharNum[6] = 2;
        SayingCharNum[7] = 2;
        SayingCharNum[8] = 1;
        SayingCharNum[9] = 2;
        SayingCharNum[10] = 0;
        SayingCharNum[11] = 1;
        SayingCharNum[12] = 2;
        SayingCharNum[13] = 1;
        SayingCharNum[14] = 2;
        SayingCharNum[15] = 1;
        SayingCharNum[16] = 2;
        SayingCharNum[17] = 1;
        SayingCharNum[18] = 1;
        SayingCharNum[19] = 2;
        SayingCharNum[20] = 0;
        SayingCharNum[21] = 0;
        SayingCharNum[22] = 1;
        SayingCharNum[23] = 0;
        SayingCharNum[24] = 2;
        SayingCharNum[25] = 1;
        SayingCharNum[26] = 2;
        SayingCharNum[27] = 1;
        SayingCharNum[28] = 2;
        SayingCharNum[29] = 1;
        SayingCharNum[30] = 2;
        SayingCharNum[31] = 1;
        SayingCharNum[32] = 0;
        SayingCharNum[33] = 2;
        SayingCharNum[34] = 0;
        SayingCharNum[35] = 2;
        SayingCharNum[36] = 1;
        SayingCharNum[37] = 2;
        SayingCharNum[38] = 1;
        SayingCharNum[39] = 1;
        SayingCharNum[40] = 2;
        SayingCharNum[41] = 2;
        SayingCharNum[42] = 2;
        SayingCharNum[43] = 2;
        SayingCharNum[44] = 1;
    }
    public void makeStage2Dialog()
    {
        dialog.Clear();
        SayingCharNum.Clear();
        for (int i = 0; i < 70; i++)
        {
            dialog.Add(null);
            SayingCharNum.Add(-1);
        }

        dialog[0] = "충전이 완료되었습니다.";
        SayingCharNum[0] = 0;
        dialog[1] = "끝났어";
        SayingCharNum[1] = 1;
        dialog[2] = "그러게. 목표는 달성했는데. 뭐가 충전되었다는 거지?";
        SayingCharNum[2] = 2;
        dialog[3] = "모르겠어";
        SayingCharNum[3] = 1;
        dialog[4] = "혹시 게임기가 아닌가? 그런 것 같지는...";
        SayingCharNum[4] = 2;
        dialog[5] = "줘 봐. 내가 확인해 볼게.";
        SayingCharNum[5] = 1;
        dialog[6] = "안 돼 아직 안전한지 모르잖아.";
        SayingCharNum[6] = 2;
        dialog[7] = "싫어. 나도 게임 할래. 사이카와만 했어. 치사해.";
        SayingCharNum[7] = 1;
        dialog[8] = "아. 그런 거라면. 자.";
        SayingCharNum[8] = 2;
        dialog[9] = "(사이카와 쉬워.)";
        SayingCharNum[9] = 1;
        dialog[10] = "(이 틈에 아까는 왜 그랬는지 알아내야 해. 사이카와가 다시 가져가지 전에.)";
        SayingCharNum[10] = 1;
        dialog[11] = "칸나, 거기서 뭐해요.";
        SayingCharNum[11] = 5;
        dialog[12] = "토르 님. 엘마 님. 이루루.";
        SayingCharNum[12] = 1;
        dialog[13] = "안녕하세요.";
        SayingCharNum[13] = 2;
        dialog[14] = "안녕";
        SayingCharNum[14] = 6;
        dialog[15] = "뭐하고 있어? 그건 뭐야?";
        SayingCharNum[15] = 4;
        dialog[16] = "토르 님. 토르 님.";
        SayingCharNum[16] = 1;
        dialog[17] = "네? 왜 그래요? 귀요?";
        SayingCharNum[17] = 5;
        dialog[18] = "(이게 뭔지 모르겠어. 위험한 걸 수도 있어. 혹시 알아?)";
        SayingCharNum[18] = 1;
        dialog[19] = "(글쎄요. 저도 잘... 한 번 확인해볼까요.)";
        SayingCharNum[19] = 5;
    }
    public void makeStage3Dialog()
    {
        dialog.Clear();
        SayingCharNum.Clear();
        for (int i = 0; i < 70; i++)
        {
            dialog.Add(null);
            SayingCharNum.Add(-1);
        }

        dialog[0] = "(으으. 힘이 안 나.)";
        SayingCharNum[0] = 1;
        dialog[1] = "토르 님. 코바야시는 정말로 알까?";
        SayingCharNum[1] = 1;
        dialog[2] = "코바야시 씨가 모르는 건 없다고요. 분명 이게 뭔지 아실 거예요.";
        SayingCharNum[2] = 5;
        dialog[3] = "그런데 칸나, 괜찮아요? 아까보다 안색이 더 안 좋아졌는데.";
        SayingCharNum[3] = 5;
        dialog[4] = "괜찮아. 걱정 안 해도 돼.";
        SayingCharNum[4] = 1;
        dialog[5] = "그러지 말고요. 사이카와도 집에 갔잖아요. 솔직히 말해요.";
        SayingCharNum[5] = 5;
        dialog[6] = "그걸 만지고 나서부터 왠지 몸이 엄청 무거워.";
        SayingCharNum[6] = 1;
        dialog[7] = "그리고요?";
        SayingCharNum[7] = 5;
        dialog[8] = "힘이 안 나와. 충전해 놓은 게 거의 다 떨어진 느낌. 걷는 것도 힘들어.";
        SayingCharNum[8] = 1;
        dialog[9] = "조금만 참아요. 거의 다 왔어요.";
        SayingCharNum[9] = 5;
        dialog[10] = "토르? 여기서 뭐해?";
        SayingCharNum[10] = 7;
        dialog[11] = "앗, 코바야시 씨! 어라? 회사 안에 있으신 거 아니셨나요?";
        SayingCharNum[11] = 5;
        dialog[12] = "응. 잠깐 나왔어. 그보다 다들...";
        SayingCharNum[12] = 7;
        dialog[13] = "예이.";
        SayingCharNum[13] = 4;
        dialog[14] = "다른 사람들은 그렇다치고, 엘마는 반차 내지 않았어?";
        SayingCharNum[14] = 7;
        dialog[15] = "사려던 한정 찹쌀떡은 무사히 샀다. 정말 맛있었지.";
        SayingCharNum[15] = 6;
        dialog[16] = "우르르 몰려온 거 보니까 귀찮은 일일 것 같은데. 이루루는?";
        SayingCharNum[16] = 7;
        dialog[17] = "재밌어 보여서 따라왔다.";
        SayingCharNum[17] = 4;
        dialog[18] = "뒤에 분들은요?";
        SayingCharNum[18] = 7;
        dialog[19] = "어쩌다보니";
        SayingCharNum[19] = 9;
        dialog[20] = "타키야한테 볼일이 있다. 먼저 가지.";
        SayingCharNum[20] = 8;
        dialog[21] = "둘은 길이 같았어요. 그보다 코바야시 씨, 이것 좀 봐주시겠어요?";
        SayingCharNum[21] = 5;
        dialog[22] = "응? 게임기? 슬롯 머신?";
        SayingCharNum[22] = 7;
        dialog[23] = "칸나가 이걸 쓴 뒤로 상태가 안 좋대요. 혹시 짐작가는 거 있으신가요?";
        SayingCharNum[23] = 5;
        dialog[24] = "으으. 또 힘이 빠져나가고 있어.";
        SayingCharNum[24] = 1;
        dialog[25] = "칸나? 으음. 드래곤이 아픈 거에 대해서는 나도 딱히.";
        SayingCharNum[25] = 7;
        dialog[26] = "그런가요. 코바야시 씨라면 뭔가 아실 것 같았는데 말이죠.";
        SayingCharNum[26] = 5;
        dialog[27] = "칸나 말로는 그 기계가 의심스럽다고 하니까요.";
        SayingCharNum[27] = 5;
        dialog[28] = "게임기... 그런데 이건 어디서 난 거야? 처음 보는데.";
        SayingCharNum[28] = 7;
        dialog[29] = "그러게요. 칸나?";
        SayingCharNum[29] = 5;
        dialog[30] = "집에 온 택배에 있었어. 클로에가 보냈어.";
        SayingCharNum[30] = 1;
        dialog[31] = "클로에? 그럼 나보다는 클로에가 뭔가 알지 않을까.";
        SayingCharNum[31] = 7;
        dialog[32] = "아.";
        SayingCharNum[32] = 1;
        dialog[33] = "그렇군요! 역시 코바야시 씨에요!";
        SayingCharNum[33] = 5;
    }
    public void makeStage4Dialog()
    {
        dialog.Clear();
        SayingCharNum.Clear();
        for (int i = 0; i < 70; i++)
        {
            dialog.Add(null);
            SayingCharNum.Add(-1);
        }

        dialog[0] = "전화도 불편한 점이 있었네요. 안 받으면 별 수 없다니.";
        SayingCharNum[0] = 5;
        dialog[1] = "응, 토르 님. 태워줘서 고마워.";
        SayingCharNum[1] = 1;
        dialog[2] = "그 뒤로 몇 사람 더 만났지만 별 수 없었으니까요.";
        SayingCharNum[2] = 5;
        dialog[3] = "죠지 씨랑 타케 씨는 넘어가도, 쇼타까지 모른다는 건 저쪽 세계의 지식 만으로는 어쩔 방도가 없다는 뜻이에요.";
        SayingCharNum[3] = 5;
        dialog[4] = "맞다. 사실 나 하나 눈치챘어.";
        SayingCharNum[4] = 1;
        dialog[5] = "네? 뭐가요?";
        SayingCharNum[5] = 5;
        dialog[6] = "사람들을 만날 때마다 몸에서 힘이 빠져나가.";
        SayingCharNum[6] = 1;
        dialog[7] = "그런가요? 저는 잘...";
        SayingCharNum[7] = 5;
        dialog[8] = "아, 제가 괜찮다는 건 드래곤이라서 그런 건 아니라는 거네요.";
        SayingCharNum[8] = 5;
        dialog[9] = "토르 님이 너무 강해서 그런 걸 지도 몰라.";
        SayingCharNum[9] = 1;
        dialog[10] = "그럴 수도 있겠네요.";
        SayingCharNum[10] = 5;
        dialog[11] = "슬슬 땅이 보여요. 내려갈 테니 조심해요.";
        SayingCharNum[11] = 5;
    }
    public void makeStage5Dialog()
    {
        dialog.Clear();
        SayingCharNum.Clear();
        for (int i = 0; i < 70; i++)
        {
            dialog.Add(null);
            SayingCharNum.Add(-1);
        }

        dialog[0] = "칸나! 이렇게 늦은 시간에 어쩐 일이야.";
        SayingCharNum[0] = 3;
        dialog[1] = "클로에! 이거.";
        SayingCharNum[1] = 1;
        dialog[2] = "내가 보낸 충전기! 잘 받았구나. 다행이다.";
        SayingCharNum[2] = 3;
        dialog[3] = "충전기? 다들 게임기라고 했어.";
        SayingCharNum[3] = 1;
        dialog[4] = "응. 게임을 해야 쓸 수 있는 충전기야. 써봤어?";
        SayingCharNum[4] = 3;
        dialog[5] = "널 위해 특별 주문 제작으로 만들었어.";
        SayingCharNum[5] = 3;
        dialog[6] = "잘 모르겠어. 이게 충전기라면 어떻게 쓰는지도 모르겠고. 그래서 물어보려고 왔어.";
        SayingCharNum[6] = 1;
        dialog[7] = "뭐? 편지에 어떻게 쓰는지 적어놨잖아. 설마 못 봤어?";
        SayingCharNum[7] = 3;
        dialog[8] = "앗.";
        SayingCharNum[8] = 1;
        dialog[9] = "앗?";
        SayingCharNum[9] = 3;
        dialog[10] = "그보다, 그거 뭔가 이상해. 가지고 있으면 몸에서 자꾸만 힘이 빠져나가.";
        SayingCharNum[10] = 1;
        dialog[11] = "지금도... 서 있기도...";
        SayingCharNum[11] = 1;
        dialog[12] = "앗, 칸나?!";
        SayingCharNum[12] = 3;
        dialog[13] = "칸나!";
        SayingCharNum[13] = 5;
    }
    public void makeStage6Dialog()
    {
        dialog.Clear();
        SayingCharNum.Clear();
        for (int i = 0; i < 70; i++)
        {
            dialog.Add(null);
            SayingCharNum.Add(-1);
        }

        dialog[0] = "칸나! 정신이 들어?";
        SayingCharNum[0] = 3;
        dialog[1] = "...우으. 클로에? 그리고 토르 님? 코바야시?";
        SayingCharNum[1] = 1;
        dialog[2] = "집이야. 몸은 좀 어때? 괜찮아?";
        SayingCharNum[2] = 7;
        dialog[3] = "미안해! 그게 그런 건 줄 몰랐어. 나 때문이야.";
        SayingCharNum[3] = 3;
        dialog[4] = "뭐가?";
        SayingCharNum[4] = 1;
        dialog[5] = "그 게임기 있잖아, 사실 전기를 충전할 수 있는 기계인데.";
        SayingCharNum[5] = 7;
        dialog[6] = "응, 그건 들었어.";
        SayingCharNum[6] = 1;
        dialog[7] = "주변의 전류를 흡수해서 충전해야 쓸 수 있는 거였대.";
        SayingCharNum[7] = 7;
        dialog[8] = "...아! 그래서.";
        SayingCharNum[8] = 1;
        dialog[9] = "미안해, 칸나.";
        SayingCharNum[9] = 3;
        dialog[10] = "괜찮아. 지금은 괜찮아졌고.";
        SayingCharNum[10] = 1;
        dialog[11] = "제 마력을 줬으니까요. 코바야시 씨가 어찌나 걱정을 하던지.";
        SayingCharNum[11] = 5;
        dialog[12] = "애가 기절을 했잖아. 걱정을 안 할 수가 없지.";
        SayingCharNum[12] = 7;
        dialog[13] = "이건 가져가서 칸나가 쓸 수 있게 고쳐서 다시 보내줄게. 정말 미안해.";
        SayingCharNum[13] = 3;
        dialog[14] = "응. 괜찮다니까.";
        SayingCharNum[14] = 1;
        dialog[15] = "맞다. 그럼 사이카와 것도 보내줘. 게임 재밌게 하는 것 같았어.";
        SayingCharNum[15] = 1;
        dialog[16] = "알았어. 그럼 내 것도 만들어서 다같이 세트로 해야겠다.";
        SayingCharNum[16] = 3;
        dialog[17] = "으음... 뭐, 큰일 없이 마무리 된 건가. 다행이다.";
        SayingCharNum[17] = 7;
    }



    public void clickDialogPanel() //대화창 열려있을 떄 화면 누르면 되게 버튼 만들 거임.
    {
        dialogReadCount++; //스택 추가 후
        writeDialogAndImage(); //텍스트 갱신
        SoundManager.instance.DialogClickSoundPlay();
    }
    public void clickSkipButton() //스킵 클릭시
    {
        dialogReadCount = dialog.Count - 2; //스택을 빈 곳일 수 밖에 없는 곳으로 옮기고
        writeDialogAndImage(); //텍스트 갱신 (끝났는지 확인하는 코드 들어있음)
        SoundManager.instance.ButtonSoundPlay();
    }

    public void writeDialogAndImage()
    {
        if (dialog[dialogReadCount] == null || SayingCharNum[dialogReadCount] == -1) //0부터 49까지 기본적으로 이렇게 초기화 함
        {
            if(StageInfoManager.instance.stageCount == 6) { StageInfoManager.instance.goodendingPanel1.SetActive(true); }
            itemShopPanel.SetActive(true);
            TextUIManager.instance.changeHeartText();
            TextUIManager.instance.changeEnergyText();
            EnergySlider.instance.ChangeEnergyBar();
            gameObject.SetActive(false); //오브젝트를 끔
        }
        else
        {
            dialogText.text = dialog[dialogReadCount];
            nameTagText.text = ItmeName[SayingCharNum[dialogReadCount]];
            SayingCharImage.sprite = slotMachineManager.itemSprite[SayingCharNum[dialogReadCount]];
            if(SayingCharNum[dialogReadCount] == 0)
            {
                SayingCharImage.sprite = nullSprite;
            }
        }
    }

}

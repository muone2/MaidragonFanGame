using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlotMachineManager : MonoBehaviour
{
    public GameObject[] slotMovingObj; //실제로 움직일 슬롯 오브젝트
    public Button[] slot;  //슬롯인데 버튼으로 만들었고, 못 누르게 막아놨음.
    public Button SlotButton; //이건 슬롯하는 버튼
    public Button itemBoxButton; //아이템 상자 여는 버튼

    public Sprite[] itemSprite; //미리 지정해두는 아이템 아이콘, 0이 꽝이다.
    public List<Sprite> itemBackpack = new List<Sprite>();//현재 가지고 있는 아이템들 (즉, 가방) 0도 포함된다.
    public List<int> itemInfoBackpack = new List<int>(); //랜덤 뽑기를 위한 리스트

    [System.Serializable]
    public class DisplayItemSlot
    {
        public List<Image> itemImage = new List<Image>();
    }
    public DisplayItemSlot[] displayItemSlots; //배열리스트

    public Image displayResultImage; //화면에 보여줄 결과 이미지

    public int[,] resultIndexArray;  //당첨된 것의 정보를 저장할 배열. 스타트에서 초기화 해줌

    int itemCountInOneSlot;  //슬롯에 올라간 총 아이템 갯수
    int itemCount = 0; //현재 가지고 있는 아이템 수
    int randomIndex;

    int[] answer = { 0, 0, 0 }; //내가 지정해둔 정보
    float[] moveDistance = { 20f, 25f, 50f }; //가능한 숫자(2.5 / 5 / 10 / 12.5 / 20 / 25 / 50 /100)중 자연스러운 것만
    public List<int> choiceNum = new List<int>();
    bool isScoreZero = true;

    public GameObject itemShopPanel;
    public GameObject itemBoxPanel;

    // Start is called before the first frame update
    void Start()
    {

        TextUIManager.instance.changeEnergyText(); //슬롯 사용 횟수 증가, ui갱신
        TextUIManager.instance.changeHeartText(); //슬롯 사용 횟수 증가, ui갱신

        itemCountInOneSlot = displayItemSlots[0].itemImage.Count - 3; //3개는 이미지 용이라서 사실 허상임.
        resultIndexArray = new int[slot.Length, itemCountInOneSlot]; //이거 현재는 3이랑 5다.

        for (int x = 0; x < slot.Length; x++)   //슬롯 갯수만큼 반복
        {
            for (int y = 0; y < displayItemSlots[0].itemImage.Count; y++)
            {
                displayItemSlots[x].itemImage[y].sprite = itemSprite[0]; //허상 포함 모든 칸을 점으로
            }
        }
        for (int i = 0; i < itemBackpack.Count; i++)  //가방 칸 갯수만큼 랜덤값 넣을 칸 생성
        {
            itemBackpack[i] = itemSprite[0]; //모든 가방 칸을 빈칸으로
            itemInfoBackpack.Add(0); //전부 빈칸이니까. 정보도 0으로 //임시로 하드코딩
        }
        for (int i = 0; i < slot.Length; i++)   //슬롯 갯수만큼 반복
        {
            slot[i].interactable = false; //슬롯의 버튼은 비활성화
        }
    }

    public void SlotUsingCoin()
    {
        TextUIManager.instance.AddSlotUseCount(); //슬롯 사용 횟수 증가, ui갱신
        SoundManager.instance.SlotSoundPlay(); //슬롯버튼 사운드 재생

        SlotButton.interactable = false; //슬롯 시작되면 버튼 클릭 불가.
        itemBoxButton.interactable = false; //마찬가지로 슬롯 도중은 클릭 불가

        choiceNum.Clear(); //중복확인용 리스트 초기화.
        for (int i = 0; i < slot.Length; i++)   //슬롯 갯수만큼 반복
        {
            for (int j = 0; j < itemCountInOneSlot ; j++)  //돌아가는 게 보이게 하기 위해 이미지용 오브젝트에 넣을 것을 랜덤 선택
            {
                
                randomIndex = Random.Range(0, itemInfoBackpack.Count);  //0부터 현재 가방 칸 갯수까지 중에 숫자 선택

                for (int a = 0; a < 1000; a++)  //무한 반복이 무서워서 일단 최소한의 제어기를...
                {
                    if (choiceNum.Contains(randomIndex))
                    {
                        randomIndex = Random.Range(0, itemInfoBackpack.Count); // 겹치면 다시 선택
                    }
                    else
                    {
                        choiceNum.Add(randomIndex);  //안 겹치면
                        a=1001; //반복문 탈출
                    }
                }

                resultIndexArray[i, j] = randomIndex; //선택된 가방 위치가 무엇인지 결과 배열에 저장 (총 슬롯 갯수만큼 저장 됨)
                
            }
        }

        for (int i = 0; i < slot.Length; i++)  //슬롯 갯수만큼 코루틴 실행
        {
            StartCoroutine(StartSlot(i));
        }
    }

    IEnumerator StartSlot(int slotIndex) //몇번째 슬롯인가
    {
        if (true) //원래 다른 거 있었는데 없앰.
        {
            int randNum = Random.Range(0, moveDistance.Length);

            yield return new WaitForSeconds( (1.4f / (slot.Length - 1)) * slotIndex); //0.5곱하기 슬롯 번호만큼 쉼. 즉 0번이면 0초

            // for (int i = 0; i < (itemCountInOneSlot * (2 + slotIndex) + answer[slotIndex]) * 4; i++)
            for (int i = 0; i < 200; i++)
            {
                slotMovingObj[slotIndex].transform.localPosition -= new Vector3(0, moveDistance[randNum], 0);  //ui컴포넌트가 운직이는 거라서 로컬 포지션 사용

                if (slotMovingObj[slotIndex].transform.localPosition.y < 100f)
                {
                    slotMovingObj[slotIndex].transform.localPosition += new Vector3(0, 500f, 0); //위치가 575가 된다.
                    for (int a = 0; a < itemCountInOneSlot; a++)
                    {
                        displayItemSlots[slotIndex].itemImage[a].sprite = itemBackpack[resultIndexArray[slotIndex, a]]; //지금 안 보이는 5개 바로 바꿔주기
                    }
                }
                else if (slotMovingObj[slotIndex].transform.localPosition.y < 200f) //100보다는 큰데 200보다는 작은 순간. (잘 모르지만 아무튼 허상은 안 보이는 중)
                {
                    for (int a = 0; a < displayItemSlots[0].itemImage.Count - itemCountInOneSlot; a++) //이건 한 라인당 당첨슬롯 갯수다.
                    {
                        displayItemSlots[slotIndex].itemImage[a + itemCountInOneSlot].sprite = itemBackpack[resultIndexArray[slotIndex, a]]; //처음 그림들과 마지막 그림들을 같게 해줄 코드
                    }
                }
                yield return new WaitForSeconds(0.03f);  //슬롯은 각각 4초 돌 듯
            }
            SoundManager.instance.StopSlotSoundPlay(); //점수 사운드 재생
            yield return new WaitForSeconds((1.4f / (slot.Length - 1)) * (slot.Length - slotIndex)); //0번이면 0.5곱하기 (2-0), 즉 1초 쉰다.
        }

        if (slotIndex == slot.Length - 1)
        {
            yield return new WaitForSeconds(0.2f);
            if (true)
            {
                checkAllResultSlotIsItem();  //전체를 본 후에 칸 별로 계산하기 위해 굳이 이렇게 함. 아이템별로 해야할 듯.
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem01();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem02();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem03();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem04();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem05();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem06();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem07();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem08();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem09();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem10();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem11();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem12();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem13();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem14();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem15();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsBingo();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
            }

            yield return new WaitForSeconds(1.1f); //좀 더 기다린 후에 엔드 엑션
            if (StageInfoManager.instance.StageHeart[StageInfoManager.instance.stageCount] - StageInfoManager.instance.slotUseCount <= 0 ||
                StageInfoManager.instance.StageEnergy[StageInfoManager.instance.stageCount] - StageInfoManager.instance.energy <= 0 )
            { //남은 생명이 0이거나 에너지가 스테이지 목표를 넘어가면
                StageInfoManager.instance.StageClearCheck();
            }
            else
            {
                itemShopPanel.SetActive(true);
            }
            SlotButton.interactable = true; //패널이 켜진 다음 버튼 클릭 다시 가능
            itemBoxButton.interactable = true;
        }
    }

    public void ClickButton (int slotNum, int imageNum)
    {
        displayResultImage.sprite = itemBackpack[resultIndexArray[slotNum, imageNum]];
    }
    public void ClickitemBoxButton()
    {
        itemBoxPanel.SetActive(true);
        SoundManager.instance.ButtonSoundPlay(); //버튼 사운드 재생
    }


    public void AddItemByButton(int num) //num은 아이템 번호
    {
        if (itemCount < itemBackpack.Count) //현재 아이템 갯수가 19일 때, 가방의 전체 칸은 20이니까 여기 들어옴
        {
            itemCount++; //아이템 1개 늘어남
            itemBackpack[itemCount - 1] = itemSprite[num]; //넘겨받은 번호의 아이템으로 빈칸을 교체
            itemInfoBackpack[itemCount - 1] = num; // 교체된 칸의 아이템 번호도 같이 교체하는 하드코딩
        }
        else if (itemCount >= itemBackpack.Count) //현재 아이템 갯수가 20일 때, 추가되면 21개가 될 거니까.
        {
            itemCount++; //아이템 1개 늘어남
            itemBackpack.Add(itemSprite[num]); //제일 끝에 추가.
            itemInfoBackpack.Add(num); // 임시로 하드 코딩
        }
    }

    void checkAllResultSlotIsItem()
    {
        isScoreZero = true;
        for (int i = 0; i < slot.Length; i++)   //슬롯 갯수만큼 반복
        {
            for (int j = 0; j < 3; j++)  //결과는 3~5지만, 결과는 3까지만 계산
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] > 0) //아이템 번호가 0이 아니면
                {
                    AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, 1);
                }
            }
        }
    }
    void checkAllResultSlotIsItem01()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 1) //해당 칸이 칸나일 때
                {
                    if (j > 0 &&(
                        itemInfoBackpack[resultIndexArray[i, j - 1]] > 0)) //윗칸 검사
                        scoreAddResult += 5; //누군가 있으면 5점
                    if (j < 2 &&(
                        itemInfoBackpack[resultIndexArray[i, j + 1]] > 0)) //아래칸 검사
                        scoreAddResult += 5; //누군가 있으면 5점
                    if (i > 0 &&(
                        itemInfoBackpack[resultIndexArray[i - 1, j]] > 0)) //왼쪽칸 검사
                        scoreAddResult += 5; //누군가 있으면 5점
                    if (i < 2 &&(
                        itemInfoBackpack[resultIndexArray[i + 1, j]] > 0)) //오른쪽칸 검사
                        scoreAddResult += 5; //누군가 있으면 5점

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 3)) //윗칸 검사
                        scoreAddResult += 10; //리코, 클로에 10점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 3)) //아래칸 검사
                        scoreAddResult += 10; 
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 3)) //왼쪽칸 검사
                        scoreAddResult += 10; 
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 3)) //오른쪽칸 검사
                        scoreAddResult += 10; 

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //사용이 끝났으니까 바로 0으로
                }
            }
        }
    }
    void checkAllResultSlotIsItem02()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 2) //해당 칸이 리코일 때
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 1)) //윗칸 검사
                        scoreAddResult += 15; //근처에 칸나가 있으면 15점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 1)) //아래칸 검사
                        scoreAddResult += 15;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 1)) //왼쪽칸 검사
                        scoreAddResult += 15;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 1)) //오른쪽칸 검사
                        scoreAddResult += 15;
                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //사용이 끝났으니까 바로 0으로
                }
            }
        }
    }
    void checkAllResultSlotIsItem03()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 3) //해당 칸이 클로에일때
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 1)) //윗칸 검사
                        scoreAddResult += 20; //칸나가 있으면 20점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 1)) //아래칸 검사
                        scoreAddResult += 20; 
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 1)) //왼쪽칸 검사
                        scoreAddResult += 20;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 1)) //오른쪽칸 검사
                        scoreAddResult += 20;

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 2)) //윗칸 검사
                        scoreAddResult -= 6; //리코가 있으면 -6점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 2)) //아래칸 검사
                        scoreAddResult -= 6;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 2)) //왼쪽칸 검사
                        scoreAddResult -= 6;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 2)) //오른쪽칸 검사
                        scoreAddResult -= 6;
                    if (scoreAddResult != 0 )
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //사용이 끝났으니까 바로 0으로
                }
            }
        }
    }
    void checkAllResultSlotIsItem04()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 4) //해당 칸이 이루루일때
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 1 ||
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 3)) //윗칸 검사
                        scoreAddResult += 8; //어린애들 중 하나가 있으면 +8점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 1 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 3)) //아래칸 검사
                        scoreAddResult += 8;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 1 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 3)) //왼쪽칸 검사
                        scoreAddResult += 8;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 1 ||
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 3)) //오른쪽칸 검사
                        scoreAddResult += 8;

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 12 ||
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 7)) //윗칸 검사
                        scoreAddResult += 12; //코바야시, 타케가 있으면 12점 
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 12 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 7)) //아래칸 검사
                        scoreAddResult += 12;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 12 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 7)) //왼쪽칸 검사
                        scoreAddResult += 12;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 12 ||
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 7)) //오른쪽칸 검사
                        scoreAddResult += 12;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //사용이 끝났으니까 바로 0으로
                }
            }
        }
    }
    void checkAllResultSlotIsItem05()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 5) //해당 칸이 토루일때
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 7)) //윗칸 검사
                        scoreAddResult += 40; //코바야시가 있으면 40점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 7)) //아래칸 검사
                        scoreAddResult += 40;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 7)) //왼쪽칸 검사
                        scoreAddResult += 40;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 7)) //오른쪽칸 검사
                        scoreAddResult += 40;

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 4 ||
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 6)) //윗칸 검사
                        scoreAddResult -= 13; //엘마, 이루루가 있으면 -11점 
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 4 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 6)) //아래칸 검사
                        scoreAddResult -= 13;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 4 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 6)) //왼쪽칸 검사
                        scoreAddResult -= 13;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 4 ||
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 6)) //오른쪽칸 검사
                        scoreAddResult -= 13;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //사용이 끝났으니까 바로 0으로
                }
            }
        }
    }
    void checkAllResultSlotIsItem06()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 6) //해당 칸이 엘마일때
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 7)) //윗칸 검사
                        scoreAddResult += 17; //코바야시가 있으면 20점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 7)) //아래칸 검사
                        scoreAddResult += 17;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 7)) //왼쪽칸 검사
                        scoreAddResult += 17;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 7)) //오른쪽칸 검사
                        scoreAddResult += 17;

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 5)) //윗칸 검사
                        scoreAddResult += 22; //토루가 있으면 22점 
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 5)) //아래칸 검사
                        scoreAddResult += 22;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 5)) //왼쪽칸 검사
                        scoreAddResult += 22;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 5)) //오른쪽칸 검사
                        scoreAddResult += 22;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //사용이 끝났으니까 바로 0으로
                }
            }
        }
    }
    void checkAllResultSlotIsItem07()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 7) //해당 칸이 코바야시일때
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] > 0)) //윗칸 검사
                        scoreAddResult += 5; //누군가 있으면 5점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] > 0)) //아래칸 검사
                        scoreAddResult += 5; //누군가 있으면 5점
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] > 0)) //왼쪽칸 검사
                        scoreAddResult += 5; //누군가 있으면 5점
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] > 0)) //오른쪽칸 검사
                        scoreAddResult += 5; //누군가 있으면 5점

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 5)) //윗칸 검사
                        scoreAddResult += 10; //토루가 있으면 10점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 5)) //아래칸 검사
                        scoreAddResult += 10;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 5)) //왼쪽칸 검사
                        scoreAddResult += 10;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 5)) //오른쪽칸 검사
                        scoreAddResult += 10;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //사용이 끝났으니까 바로 0으로
                }
            }
        }
    }
    void checkAllResultSlotIsItem08()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        scoreAddResult -= 10; //등장하면 기본적으로 -10점은 확정
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 8) //해당 칸이 파프닐일때
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] > 0 )) //윗칸 검사
                        scoreAddResult -= 6; //인근 칸에 뭔가가 -6 있으면
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] > 0)) //아래칸 검사
                        scoreAddResult -= 6;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] > 0)) //왼쪽칸 검사
                        scoreAddResult -= 6;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] > 0)) //오른쪽칸 검사
                        scoreAddResult -= 6;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //사용이 끝났으니까 바로 0으로
                }
            }
        }
    }
    void checkAllResultSlotIsItem09()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 9) //해당 칸이 루코아일때
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 10)) //윗칸 검사
                        scoreAddResult += 30; //쇼타가 있으면 30점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 10)) //아래칸 검사
                        scoreAddResult += 30;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 10)) //왼쪽칸 검사
                        scoreAddResult += 30;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 10)) //오른쪽칸 검사
                        scoreAddResult += 30;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //사용이 끝났으니까 바로 0으로
                }
            }
        }
    }
    void checkAllResultSlotIsItem10()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 10) //해당 칸이 쇼타일때
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 9)) //윗칸 검사
                        scoreAddResult += 20; //루코아가 있으면 9점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 9)) //아래칸 검사
                        scoreAddResult += 20;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 9)) //왼쪽칸 검사
                        scoreAddResult += 20;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 9)) //오른쪽칸 검사
                        scoreAddResult += 20;

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 1 ||
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 3)) //윗칸 검사
                        scoreAddResult += 3; //엘마, 이루루가 있으면 -11점 
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 1 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 3)) //아래칸 검사
                        scoreAddResult += 3;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 1 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 3)) //왼쪽칸 검사
                        scoreAddResult += 3;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 1 ||
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 3)) //오른쪽칸 검사
                        scoreAddResult += 3;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //사용이 끝났으니까 바로 0으로
                }
            }
        }
    }
    void checkAllResultSlotIsItem11()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 11) //해당 칸이 죠지일때
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 7)) //윗칸 검사
                        scoreAddResult += 25; //코바야시가 있으면 40점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 7)) //아래칸 검사
                        scoreAddResult += 25;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 7)) //왼쪽칸 검사
                        scoreAddResult += 25;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 7)) //오른쪽칸 검사
                        scoreAddResult += 25;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //사용이 끝났으니까 바로 0으로
                }
            }
        }
    }
    void checkAllResultSlotIsItem12()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 12) //해당 칸이 타케일때
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 4)) //윗칸 검사
                        scoreAddResult += 10; //이루루 10점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 4)) //아래칸 검사
                        scoreAddResult += 10;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 4)) //왼쪽칸 검사
                        scoreAddResult += 10;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 4)) //오른쪽칸 검사
                        scoreAddResult += 10;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //사용이 끝났으니까 바로 0으로
                }
            }
        }
    }
    void checkAllResultSlotIsItem13()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 13) //해당 칸이 토루일때
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 8)) //윗칸 검사
                        scoreAddResult += 5; //파프닐5점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 8)) //아래칸 검사
                        scoreAddResult += 5;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 8)) //왼쪽칸 검사
                        scoreAddResult += 5;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 8)) //오른쪽칸 검사
                        scoreAddResult += 5;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //사용이 끝났으니까 바로 0으로
                }
            }
        }
    }
    void checkAllResultSlotIsItem14()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 14) //해당 칸이 찹쌀떡일때
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] > 0)) //윗칸 검사
                        scoreAddResult += 10; //뭐든지10점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] > 0)) //아래칸 검사
                        scoreAddResult += 10;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] > 0)) //왼쪽칸 검사
                        scoreAddResult += 10;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] > 0)) //오른쪽칸 검사
                        scoreAddResult += 10;

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 6)) //윗칸 검사
                        scoreAddResult += 10; //엘마, 이루루가 있으면 -11점 
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 6)) //아래칸 검사
                        scoreAddResult += 10;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 6)) //왼쪽칸 검사
                        scoreAddResult += 10;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 6)) //오른쪽칸 검사
                        scoreAddResult += 10;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //사용이 끝났으니까 바로 0으로
                }
            }
        }
    }
    void checkAllResultSlotIsItem15()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 15) //해당 칸이 토루꼬리일때
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] > 0)) //윗칸 검사
                        scoreAddResult += 15; //뭐든지 15점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] > 0)) //아래칸 검사
                        scoreAddResult += 15;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] > 0)) //왼쪽칸 검사
                        scoreAddResult += 15;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] > 0)) //오른쪽칸 검사
                        scoreAddResult += 15;

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 7)) //윗칸 검사
                        scoreAddResult -= 10; //코바야시 -10점
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 7)) //아래칸 검사
                        scoreAddResult -= 10;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 7)) //왼쪽칸 검사
                        scoreAddResult -= 10;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 7)) //오른쪽칸 검사
                        scoreAddResult -= 10;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //사용이 끝났으니까 바로 0으로
                }
            }
        }
    }
    void checkAllResultSlotIsBingo()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] > 0)
                {
                    if ((itemInfoBackpack[resultIndexArray[i, 0]] == itemInfoBackpack[resultIndexArray[i, 1]]) &&
                            (itemInfoBackpack[resultIndexArray[i, 1]] == itemInfoBackpack[resultIndexArray[i, 2]]))
                        scoreAddResult += 50; //뭐든지 50점
                    if ((itemInfoBackpack[resultIndexArray[0, j]] == itemInfoBackpack[resultIndexArray[1, j]]) &&
                            (itemInfoBackpack[resultIndexArray[1, j]] == itemInfoBackpack[resultIndexArray[2, j]]))
                        scoreAddResult += 50; //뭐든지 50점
                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //사용이 끝났으니까 바로 0으로
                }
            }
        }
    }

    void AddScoreByUIManager(GameObject obj, int score)
    {
        TextUIManager.instance.AddEnergy(score);
        TextUIManager.instance.makeTextEffectSlotScore(obj, score);
        isScoreZero = false; //점수가 출력됐다면 그 라인은 아무튼 점수가 0점이 아니니까 여기 넣음.

        if (score >= 50)
            SoundManager.instance.JackPotSoundPlay(); //점수 사운드 재생
        else
            SoundManager.instance.ScoreUpSoundPlay(); //점수 사운드 재생
    }
}

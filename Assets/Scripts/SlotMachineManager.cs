using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlotMachineManager : MonoBehaviour
{
    public GameObject[] slotMovingObj; //실제로 움직일 슬롯 오브젝트
    public Button[] slot;  //슬롯인데 누를 수도 있긴 함.
    public Button SlotButton; //이건 슬롯하는 버튼

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


    public int[,] resultIndexArray = new int[3, 3]; //당첨된 것의 정보를 저장할 배열 (무조건 3,3일 예정)

    int itemCountInOneSlot;  //슬롯에 올라간 총 아이템 갯수
    int itemCount = 0; //현재 가지고 있는 아이템 수
    int randomIndex;

    int[] answer = { 0, 0, 0 }; //내가 지정해둔 정보
    public List<int> choiceNum = new List<int>();

    public GameObject itemShopPanel;

    // Start is called before the first frame update
    void Start()
    {
        itemCountInOneSlot = displayItemSlots[0].itemImage.Count - 3; //3개는 이미지 용이라서 사실 허상임.
        for (int x = 0; x < slot.Length; x++)   //슬롯 갯수만큼 반복
        {
            for (int y = 0; y < itemCountInOneSlot; y++)
            {
                displayItemSlots[x].itemImage[y].sprite = itemSprite[0]; //모든 칸을 점으로
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
        SlotButton.interactable = false; //슬롯 시작되면 버튼 클릭 불가.
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
                Debug.Log(randomIndex);

                if (j <= 2) //당첨이 3*3개
                {
                    resultIndexArray[i, j] = randomIndex; //선택된 가방위치가 무엇인지 결과 리스트에 저장 (총 슬롯 갯수만큼 저장 됨)
                    displayItemSlots[i].itemImage[j + itemCountInOneSlot].sprite = itemBackpack[randomIndex]; //처음 그림들과 마지막 그림들을 같게 해줄 코드
                }
                displayItemSlots[i].itemImage[j].sprite = itemBackpack[randomIndex]; //슬롯에 해당 이미지를 출력
            }
        }

        for (int i = 0; i < slot.Length; i++)  //슬롯 갯수만큼 코루틴 실행
        {
            StartCoroutine(StartSlot(i));
        }
    }

    IEnumerator StartSlot(int slotIndex) //몇번째 슬롯인가
    {
        for (int i = 0; i < (itemCountInOneSlot * (2 + slotIndex) + answer[slotIndex]) * 4; i++)
        {
            slotMovingObj[slotIndex].transform.localPosition -= new Vector3(0, 25f, 0);  //ui컴포넌트가 운직이는 거라서 로컬 포지션 사용
            if (slotMovingObj[slotIndex].transform.localPosition.y < 125f)
            {
                slotMovingObj[slotIndex].transform.localPosition += new Vector3(0, 500f, 0);
            }
            yield return new WaitForSeconds(0.05f);
        }
        if (slotIndex == slot.Length - 1)
        {
            checkSlotScore();
            yield return new WaitForSeconds(2f);
            slotEndAction();
        }
    }

    public void ClickButton (int index)
    {
        displayResultImage.sprite = itemBackpack[resultIndexArray[index,0]];
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

    void checkSlotScore()
    {
        for (int i = 0; i < slot.Length; i++)   //슬롯 갯수만큼 반복
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] >= 1 && itemInfoBackpack[resultIndexArray[i, j]] <= 4) //1부터 4까지 임시로.
                {
                    TextUIManager.instance.AddEnergy(1);
                  //  Debug.Log(itemInfoBackpack[resultIndexArray[i, j]]);
                }
            }
        }
    }

    void slotEndAction()
    {
        SlotButton.interactable = true; //슬롯이 다 돌아가기 전에 재클릭 불가능
        itemShopPanel.SetActive(true);
    }
}

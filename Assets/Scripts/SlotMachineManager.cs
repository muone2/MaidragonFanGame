using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlotMachineManager : MonoBehaviour
{
    public GameObject[] slotMovingObj; //실제로 움직일 슬롯 오브젝트
    public Button[] slot;  //버튼

    public Sprite[] itemSprite; //미리 지정해두는 아이템 아이콘, 0이 꽝이다.
    public List<Sprite> itemBackpack = new List<Sprite>();//현재 가지고 있는 아이템들 (즉, 가방) 0도 포함된다.


    int MaxItemValue;  // 총 가방 수는 유니티에서 추가해놓은 만큼.

    [System.Serializable]
    public class DisplayItemSlot
    {
        public List<Image> itemImage = new List<Image>();
    }
    public DisplayItemSlot[] displayItemSlots; //배열리스트

    public Image displayResultImage; //화면에 보여줄 결과 이미지

    public List<int> itemList = new List<int>(); //랜덤 뽑기를 위한 리스트
    public List<int> resultIndexList = new List<int>(); //당첨된 것을 저장할 리스트

    int itemCountInOneSlot = 4;  //슬롯에 올라간 총 아이템 갯수
    int itemNum = 0; //현재 가지고 있는 아이템 수
    int randomIndex;

    int[] answer = { 0, 0, 0 }; //내가 지정해둔 정보

    // Start is called before the first frame update
    void Start()
    {
        MaxItemValue = itemBackpack.Count;
        for (int i = 0; i < MaxItemValue; i++)  //아이템 갯수만큼 랜덤값 넣을 칸 생성 (빈칸 포함)
        {
            itemList.Add(i);
        }
        for (int i = 0; i < slot.Length; i++)   //슬롯 갯수만큼 반복
        {
            slot[i].interactable = true; //버튼은 항상 비활성화
        }
    }

    public void SlotUsingCoin()
    {
        resultIndexList.Clear();
        // itemList는 20개
        for (int i = 0; i < slot.Length; i++)   //슬롯 갯수만큼 반복
        {
            for (int j = 0; j < itemCountInOneSlot; j++)  //돌아가는 게 보이게 하기 위해 이미지용 오브젝트에 넣을 것을 랜덤 선택
            {
                randomIndex = Random.Range(0, itemList.Count);  //0부터 아이템 갯수 (최소가 20)까지 중에 숫자 선택

                Debug.Log(randomIndex);
                if (j == 0) //이건 그냥 내가 계산 편하게 하려고, 무조건 각 슬롯의 0번째가 당첨되게 한 것
                {
                    resultIndexList.Add(itemList[randomIndex]); //당첨된 숫자가 무엇인지 정보를 결과 리스트에 저장 (총 슬롯 갯수만큼 저장 됨)
                }

                displayItemSlots[i].itemImage[j].sprite = itemBackpack[itemList[randomIndex]]; //각 이미지에 결과 이미지를 저장

                if (j == 0)
                {
                    displayItemSlots[i].itemImage[itemCountInOneSlot].sprite = itemBackpack[itemList[randomIndex]]; //처음 그림과 마지막 그림을 같게 해줌
                }
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
            if (slotMovingObj[slotIndex].transform.localPosition.y < 25f)
            {
                slotMovingObj[slotIndex].transform.localPosition += new Vector3(0, 400f, 0);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void ClickButton (int index)
    {
        displayResultImage.sprite = itemBackpack[resultIndexList[index]];
    }

    public void AddItemByButton()
    {
        if (itemNum < itemBackpack.Count) //현재 아이템 갯수가 19일 때, 가방의 전체 칸은 20이니까 여기 들어옴
        {
            itemNum++; //아이템 1개 늘어남
            itemBackpack[itemNum - 1] = itemSprite[Random.Range(1, itemSprite.Length)]; //아이템 중 하나 랜덤 추가
        }
        else if (itemNum >= itemBackpack.Count) //현재 아이템 갯수가 20일 때, 추가되면 21개가 될 거니까.
        {
            itemNum++; //아이템 1개 늘어남
            itemBackpack.Add(itemSprite[Random.Range(1, itemSprite.Length)]); //제일 끝에 추가.
        }
    }
}

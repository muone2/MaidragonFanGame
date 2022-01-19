using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    public SlotMachineManager slotMachineManager;
    public Button[] ShopButton;  //상점 아이템 버튼
    public List<int> resultItemNum = new List<int>();  //각 칸의 아이템의 번호
    public Text[] itemNameText;
    

    public List<int> resultIndexList = new List<int>(); //당첨된 것을 저장할 리스트

    int randomIndex;
    int[] limitStageItem = { 3, 6, 9, 15};

    //스크립트가 활성화 될때마다 실행되는 함수
    private void OnEnable()
    {
        resultItemNum.Clear(); //쓰기 전에 초기화
        for (int i = 0; i < ShopButton.Length; i++)
        {
            randomIndex = Random.Range(1, StageInfoManager.instance.stageCount * 3 + 1 ); //스테이지마다 3개 단위로...

            for (int a = 0; a < 1000; a++)
            {
                if (resultItemNum.Contains(randomIndex) || randomIndex > slotMachineManager.itemSprite.Length) //겹치거나 없는 거 찾으면
                {
                    randomIndex = Random.Range(1, StageInfoManager.instance.stageCount * 3 + 1); // 겹치면 다시 선택
                }
                else
                {
                    resultItemNum.Add(randomIndex);  //안 겹치면
                    a = 1001; //반복문 탈출
                }
            }
            ShopButton[i].GetComponent<Image>().sprite = slotMachineManager.itemSprite[resultItemNum[i]];
            itemNameText[i].text = DialogManager.instance.ItmeName[resultItemNum[i]];
        }
    }

    public void clickItemInShop(int buttonNum) //버튼에 넣을 때 적어줄 값 (배열 번호랑 같게.)
    {
        slotMachineManager.AddItemByButton(resultItemNum[buttonNum]);
        gameObject.SetActive(false);
        SoundManager.instance.ButtonSoundPlay(); //버튼 사운드 재생

    }

    public void ClickCloseButtonInShop() //아이템 안 사고 그냥 나갈 수도 있게 함.
    {
        gameObject.SetActive(false);
        SoundManager.instance.ButtonSoundPlay(); //버튼 사운드 재생

    }
}

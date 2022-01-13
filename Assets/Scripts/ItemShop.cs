using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    public SlotMachineManager slotMachineManager;
    public Button[] ShopButton;  //상점 아이템 버튼
    public int[] resultItemNum;  //각 칸의 아이템의 번호

    public List<int> resultIndexList = new List<int>(); //당첨된 것을 저장할 리스트

    //스크립트가 활성화 될때마다 실행되는 함수
    private void OnEnable()
    {
        for (int i = 0; i < ShopButton.Length; i++)
        {
            resultItemNum[i] = Random.Range(1, slotMachineManager.itemSprite.Length); //0포함해서 길이가 5라서 됨
            ShopButton[i].GetComponent<Image>().sprite = slotMachineManager.itemSprite[resultItemNum[i]];
        }
    }

    public void clickItemInShop(int buttonNum) //버튼에 넣을 때 적어줄 값 (배열 번호랑 같게.)
    {
        slotMachineManager.AddItemByButton(resultItemNum[buttonNum]);
        gameObject.SetActive(false);
    }
}

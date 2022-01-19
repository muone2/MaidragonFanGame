using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxManager : MonoBehaviour
{
    public SlotMachineManager slotMachineManager;
    public GameObject itemBlockPrefeb;
    public GameObject itemBoxGroup;
    GameObject tmp;

    public List<int> itemCountByItemNumber = new List<int>();
    public List<GameObject> itemlistInBackpackPanel = new List<GameObject>();

    public GameObject PanelForCantClickOther;

    private void OnEnable() //패널이 켜지면
    {

        itemCountByItemNumber.Clear(); //안전책

        for (int i = 0; i < slotMachineManager.itemSprite.Length; i++) //전체 아이템 개수만큼 리스트 생성.
        {
            itemCountByItemNumber.Add(0);
        }

        for (int i = 0; i < slotMachineManager.itemInfoBackpack.Count; i++) //가방 연 동안은 템 늘어날 일 없으니 ㄱㅊ
        {
            //slotMachineManager.itemInfoBackpack[i]는 i가 0일때는 0번 칸에 들어있는 아이템의 고유 번호가 있다.

            itemCountByItemNumber[slotMachineManager.itemInfoBackpack[i]] += 1; //각 아이템의 갯수
        }

        for (int i = 1; i < slotMachineManager.itemSprite.Length; i++) //빈칸 빼고 전체 아이템 개수만큼 반복
        {
            if (itemCountByItemNumber[i] > 0) //해당 번호의 아이템이 있으면
            {
                tmp = Instantiate(itemBlockPrefeb);
                tmp.GetComponent<Image>().sprite = slotMachineManager.itemSprite[i];
                tmp.transform.parent = itemBoxGroup.transform;
                tmp.transform.localScale = Vector3.one;  //다른 곳에서 와서 그런지 로컬 스케일이 바뀌길래 강제 초기화
                tmp.GetComponent<ItemBoxBlockClick>().SetItemBlockInfo(i, itemCountByItemNumber[i], slotMachineManager.itemSprite[i], PanelForCantClickOther);

                itemlistInBackpackPanel.Add(tmp);
            }
        }
    }


    public void ClickXButton()
    {
        for (int i = 0; i < itemlistInBackpackPanel.Count; i++)
        {
            Destroy(itemlistInBackpackPanel[i]);
        }
        itemlistInBackpackPanel.Clear();

        SoundManager.instance.ButtonSoundPlay(); //버튼 사운드 재생
        gameObject.SetActive(false);
    }
}

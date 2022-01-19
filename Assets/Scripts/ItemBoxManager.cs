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

    private void OnEnable() //�г��� ������
    {

        itemCountByItemNumber.Clear(); //����å

        for (int i = 0; i < slotMachineManager.itemSprite.Length; i++) //��ü ������ ������ŭ ����Ʈ ����.
        {
            itemCountByItemNumber.Add(0);
        }

        for (int i = 0; i < slotMachineManager.itemInfoBackpack.Count; i++) //���� �� ������ �� �þ �� ������ ����
        {
            //slotMachineManager.itemInfoBackpack[i]�� i�� 0�϶��� 0�� ĭ�� ����ִ� �������� ���� ��ȣ�� �ִ�.

            itemCountByItemNumber[slotMachineManager.itemInfoBackpack[i]] += 1; //�� �������� ����
        }

        for (int i = 1; i < slotMachineManager.itemSprite.Length; i++) //��ĭ ���� ��ü ������ ������ŭ �ݺ�
        {
            if (itemCountByItemNumber[i] > 0) //�ش� ��ȣ�� �������� ������
            {
                tmp = Instantiate(itemBlockPrefeb);
                tmp.GetComponent<Image>().sprite = slotMachineManager.itemSprite[i];
                tmp.transform.parent = itemBoxGroup.transform;
                tmp.transform.localScale = Vector3.one;  //�ٸ� ������ �ͼ� �׷��� ���� �������� �ٲ�淡 ���� �ʱ�ȭ
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

        SoundManager.instance.ButtonSoundPlay(); //��ư ���� ���
        gameObject.SetActive(false);
    }
}

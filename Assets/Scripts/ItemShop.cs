using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    public SlotMachineManager slotMachineManager;
    public Button[] ShopButton;  //���� ������ ��ư
    public int[] resultItemNum;  //�� ĭ�� �������� ��ȣ

    public List<int> resultIndexList = new List<int>(); //��÷�� ���� ������ ����Ʈ

    //��ũ��Ʈ�� Ȱ��ȭ �ɶ����� ����Ǵ� �Լ�
    private void OnEnable()
    {
        for (int i = 0; i < ShopButton.Length; i++)
        {
            resultItemNum[i] = Random.Range(1, slotMachineManager.itemSprite.Length); //0�����ؼ� ���̰� 5�� ��
            ShopButton[i].GetComponent<Image>().sprite = slotMachineManager.itemSprite[resultItemNum[i]];
        }
    }

    public void clickItemInShop(int buttonNum) //��ư�� ���� �� ������ �� (�迭 ��ȣ�� ����.)
    {
        slotMachineManager.AddItemByButton(resultItemNum[buttonNum]);
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    public SlotMachineManager slotMachineManager;
    public Button[] ShopButton;  //���� ������ ��ư
    public List<int> resultItemNum = new List<int>();  //�� ĭ�� �������� ��ȣ
    public Text[] itemNameText;
    

    public List<int> resultIndexList = new List<int>(); //��÷�� ���� ������ ����Ʈ

    int randomIndex;
    int[] limitStageItem = { 3, 6, 9, 15};

    //��ũ��Ʈ�� Ȱ��ȭ �ɶ����� ����Ǵ� �Լ�
    private void OnEnable()
    {
        resultItemNum.Clear(); //���� ���� �ʱ�ȭ
        for (int i = 0; i < ShopButton.Length; i++)
        {
            randomIndex = Random.Range(1, StageInfoManager.instance.stageCount * 3 + 1 ); //������������ 3�� ������...

            for (int a = 0; a < 1000; a++)
            {
                if (resultItemNum.Contains(randomIndex) || randomIndex > slotMachineManager.itemSprite.Length) //��ġ�ų� ���� �� ã����
                {
                    randomIndex = Random.Range(1, StageInfoManager.instance.stageCount * 3 + 1); // ��ġ�� �ٽ� ����
                }
                else
                {
                    resultItemNum.Add(randomIndex);  //�� ��ġ��
                    a = 1001; //�ݺ��� Ż��
                }
            }
            ShopButton[i].GetComponent<Image>().sprite = slotMachineManager.itemSprite[resultItemNum[i]];
            itemNameText[i].text = DialogManager.instance.ItmeName[resultItemNum[i]];
        }
    }

    public void clickItemInShop(int buttonNum) //��ư�� ���� �� ������ �� (�迭 ��ȣ�� ����.)
    {
        slotMachineManager.AddItemByButton(resultItemNum[buttonNum]);
        gameObject.SetActive(false);
        SoundManager.instance.ButtonSoundPlay(); //��ư ���� ���

    }

    public void ClickCloseButtonInShop() //������ �� ��� �׳� ���� ���� �ְ� ��.
    {
        gameObject.SetActive(false);
        SoundManager.instance.ButtonSoundPlay(); //��ư ���� ���

    }
}

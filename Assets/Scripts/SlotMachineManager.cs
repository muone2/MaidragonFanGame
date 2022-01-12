using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlotMachineManager : MonoBehaviour
{
    public GameObject[] slotMovingObj; //������ ������ ���� ������Ʈ
    public Button[] slot;  //��ư

    public Sprite[] itemSprite; //�̸� �����صδ� ������ ������, 0�� ���̴�.
    public List<Sprite> itemBackpack = new List<Sprite>();//���� ������ �ִ� �����۵� (��, ����) 0�� ���Եȴ�.


    int MaxItemValue;  // �� ���� ���� ����Ƽ���� �߰��س��� ��ŭ.

    [System.Serializable]
    public class DisplayItemSlot
    {
        public List<Image> itemImage = new List<Image>();
    }
    public DisplayItemSlot[] displayItemSlots; //�迭����Ʈ

    public Image displayResultImage; //ȭ�鿡 ������ ��� �̹���

    public List<int> itemList = new List<int>(); //���� �̱⸦ ���� ����Ʈ
    public List<int> resultIndexList = new List<int>(); //��÷�� ���� ������ ����Ʈ

    int itemCountInOneSlot = 4;  //���Կ� �ö� �� ������ ����
    int itemNum = 0; //���� ������ �ִ� ������ ��
    int randomIndex;

    int[] answer = { 0, 0, 0 }; //���� �����ص� ����

    // Start is called before the first frame update
    void Start()
    {
        MaxItemValue = itemBackpack.Count;
        for (int i = 0; i < MaxItemValue; i++)  //������ ������ŭ ������ ���� ĭ ���� (��ĭ ����)
        {
            itemList.Add(i);
        }
        for (int i = 0; i < slot.Length; i++)   //���� ������ŭ �ݺ�
        {
            slot[i].interactable = true; //��ư�� �׻� ��Ȱ��ȭ
        }
    }

    public void SlotUsingCoin()
    {
        resultIndexList.Clear();
        // itemList�� 20��
        for (int i = 0; i < slot.Length; i++)   //���� ������ŭ �ݺ�
        {
            for (int j = 0; j < itemCountInOneSlot; j++)  //���ư��� �� ���̰� �ϱ� ���� �̹����� ������Ʈ�� ���� ���� ���� ����
            {
                randomIndex = Random.Range(0, itemList.Count);  //0���� ������ ���� (�ּҰ� 20)���� �߿� ���� ����

                Debug.Log(randomIndex);
                if (j == 0) //�̰� �׳� ���� ��� ���ϰ� �Ϸ���, ������ �� ������ 0��°�� ��÷�ǰ� �� ��
                {
                    resultIndexList.Add(itemList[randomIndex]); //��÷�� ���ڰ� �������� ������ ��� ����Ʈ�� ���� (�� ���� ������ŭ ���� ��)
                }

                displayItemSlots[i].itemImage[j].sprite = itemBackpack[itemList[randomIndex]]; //�� �̹����� ��� �̹����� ����

                if (j == 0)
                {
                    displayItemSlots[i].itemImage[itemCountInOneSlot].sprite = itemBackpack[itemList[randomIndex]]; //ó�� �׸��� ������ �׸��� ���� ����
                }
            }
        }

        for (int i = 0; i < slot.Length; i++)  //���� ������ŭ �ڷ�ƾ ����
        {
            StartCoroutine(StartSlot(i));
        }
    }

    IEnumerator StartSlot(int slotIndex) //���° �����ΰ�
    {
        for (int i = 0; i < (itemCountInOneSlot * (2 + slotIndex) + answer[slotIndex]) * 4; i++)
        {
            slotMovingObj[slotIndex].transform.localPosition -= new Vector3(0, 25f, 0);  //ui������Ʈ�� �����̴� �Ŷ� ���� ������ ���
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
        if (itemNum < itemBackpack.Count) //���� ������ ������ 19�� ��, ������ ��ü ĭ�� 20�̴ϱ� ���� ����
        {
            itemNum++; //������ 1�� �þ
            itemBackpack[itemNum - 1] = itemSprite[Random.Range(1, itemSprite.Length)]; //������ �� �ϳ� ���� �߰�
        }
        else if (itemNum >= itemBackpack.Count) //���� ������ ������ 20�� ��, �߰��Ǹ� 21���� �� �Ŵϱ�.
        {
            itemNum++; //������ 1�� �þ
            itemBackpack.Add(itemSprite[Random.Range(1, itemSprite.Length)]); //���� ���� �߰�.
        }
    }
}

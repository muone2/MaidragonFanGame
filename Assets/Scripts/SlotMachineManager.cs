using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlotMachineManager : MonoBehaviour
{
    public GameObject[] slotMovingObj; //������ ������ ���� ������Ʈ
    public Button[] slot;  //�����ε� ���� ���� �ֱ� ��.
    public Button SlotButton; //�̰� �����ϴ� ��ư

    public Sprite[] itemSprite; //�̸� �����صδ� ������ ������, 0�� ���̴�.
    public List<Sprite> itemBackpack = new List<Sprite>();//���� ������ �ִ� �����۵� (��, ����) 0�� ���Եȴ�.
    public List<int> itemInfoBackpack = new List<int>(); //���� �̱⸦ ���� ����Ʈ

    [System.Serializable]
    public class DisplayItemSlot
    {
        public List<Image> itemImage = new List<Image>();
    }
    public DisplayItemSlot[] displayItemSlots; //�迭����Ʈ

    public Image displayResultImage; //ȭ�鿡 ������ ��� �̹���


    public int[,] resultIndexArray = new int[3, 3]; //��÷�� ���� ������ ������ �迭 (������ 3,3�� ����)

    int itemCountInOneSlot;  //���Կ� �ö� �� ������ ����
    int itemCount = 0; //���� ������ �ִ� ������ ��
    int randomIndex;

    int[] answer = { 0, 0, 0 }; //���� �����ص� ����
    public List<int> choiceNum = new List<int>();

    public GameObject itemShopPanel;

    // Start is called before the first frame update
    void Start()
    {
        itemCountInOneSlot = displayItemSlots[0].itemImage.Count - 3; //3���� �̹��� ���̶� ��� �����.
        for (int x = 0; x < slot.Length; x++)   //���� ������ŭ �ݺ�
        {
            for (int y = 0; y < itemCountInOneSlot; y++)
            {
                displayItemSlots[x].itemImage[y].sprite = itemSprite[0]; //��� ĭ�� ������
            }
        }
        for (int i = 0; i < itemBackpack.Count; i++)  //���� ĭ ������ŭ ������ ���� ĭ ����
        {
            itemBackpack[i] = itemSprite[0]; //��� ���� ĭ�� ��ĭ����
            itemInfoBackpack.Add(0); //���� ��ĭ�̴ϱ�. ������ 0���� //�ӽ÷� �ϵ��ڵ�
        }
        for (int i = 0; i < slot.Length; i++)   //���� ������ŭ �ݺ�
        {
            slot[i].interactable = false; //������ ��ư�� ��Ȱ��ȭ
        }
    }

    public void SlotUsingCoin()
    {
        SlotButton.interactable = false; //���� ���۵Ǹ� ��ư Ŭ�� �Ұ�.
        choiceNum.Clear(); //�ߺ�Ȯ�ο� ����Ʈ �ʱ�ȭ.
        for (int i = 0; i < slot.Length; i++)   //���� ������ŭ �ݺ�
        {
            for (int j = 0; j < itemCountInOneSlot ; j++)  //���ư��� �� ���̰� �ϱ� ���� �̹����� ������Ʈ�� ���� ���� ���� ����
            {
                
                randomIndex = Random.Range(0, itemInfoBackpack.Count);  //0���� ���� ���� ĭ �������� �߿� ���� ����

                for (int a = 0; a < 1000; a++)  //���� �ݺ��� �������� �ϴ� �ּ����� ����⸦...
                {
                    if (choiceNum.Contains(randomIndex))
                    {
                        randomIndex = Random.Range(0, itemInfoBackpack.Count); // ��ġ�� �ٽ� ����
                    }
                    else
                    {
                        choiceNum.Add(randomIndex);  //�� ��ġ��
                        a=1001; //�ݺ��� Ż��
                    }
                }
                Debug.Log(randomIndex);

                if (j <= 2) //��÷�� 3*3��
                {
                    resultIndexArray[i, j] = randomIndex; //���õ� ������ġ�� �������� ��� ����Ʈ�� ���� (�� ���� ������ŭ ���� ��)
                    displayItemSlots[i].itemImage[j + itemCountInOneSlot].sprite = itemBackpack[randomIndex]; //ó�� �׸���� ������ �׸����� ���� ���� �ڵ�
                }
                displayItemSlots[i].itemImage[j].sprite = itemBackpack[randomIndex]; //���Կ� �ش� �̹����� ���
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

    public void AddItemByButton(int num) //num�� ������ ��ȣ
    {
        if (itemCount < itemBackpack.Count) //���� ������ ������ 19�� ��, ������ ��ü ĭ�� 20�̴ϱ� ���� ����
        {
            itemCount++; //������ 1�� �þ
            itemBackpack[itemCount - 1] = itemSprite[num]; //�Ѱܹ��� ��ȣ�� ���������� ��ĭ�� ��ü
            itemInfoBackpack[itemCount - 1] = num; // ��ü�� ĭ�� ������ ��ȣ�� ���� ��ü�ϴ� �ϵ��ڵ�
        }
        else if (itemCount >= itemBackpack.Count) //���� ������ ������ 20�� ��, �߰��Ǹ� 21���� �� �Ŵϱ�.
        {
            itemCount++; //������ 1�� �þ
            itemBackpack.Add(itemSprite[num]); //���� ���� �߰�.
            itemInfoBackpack.Add(num); // �ӽ÷� �ϵ� �ڵ�
        }
    }

    void checkSlotScore()
    {
        for (int i = 0; i < slot.Length; i++)   //���� ������ŭ �ݺ�
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] >= 1 && itemInfoBackpack[resultIndexArray[i, j]] <= 4) //1���� 4���� �ӽ÷�.
                {
                    TextUIManager.instance.AddEnergy(1);
                  //  Debug.Log(itemInfoBackpack[resultIndexArray[i, j]]);
                }
            }
        }
    }

    void slotEndAction()
    {
        SlotButton.interactable = true; //������ �� ���ư��� ���� ��Ŭ�� �Ұ���
        itemShopPanel.SetActive(true);
    }
}

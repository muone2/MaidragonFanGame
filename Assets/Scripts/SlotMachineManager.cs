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
    bool isScoreZero = true;

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
        TextUIManager.instance.slotUseCount++;
        TextUIManager.instance.changeEnergyText(); //���� ��� Ƚ�� ����, ui����

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
               // Debug.Log(randomIndex);

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
            if (slotMovingObj[slotIndex].transform.localPosition.y < 100f)
            {
                slotMovingObj[slotIndex].transform.localPosition += new Vector3(0, 500f, 0);
            }
            yield return new WaitForSeconds(0.05f);
        }
        if (slotIndex == slot.Length - 1)
        {
            yield return new WaitForSeconds(1.0f);
            checkAllResultSlotIsItem();  //��ü�� �� �Ŀ� ĭ ���� ����ϱ� ���� ���� �̷��� ��. �����ۺ��� �ؾ��� ��.
            if (isScoreZero == false)
                yield return new WaitForSeconds(1.0f);
            checkAllResultSlotIsItem01();
            if (isScoreZero == false)
                yield return new WaitForSeconds(1.0f);
            checkAllResultSlotIsItem02();
            if (isScoreZero == false)
                yield return new WaitForSeconds(1.0f);
            checkAllResultSlotIsItem03();
            if (isScoreZero == false)
                yield return new WaitForSeconds(1.0f);
            checkAllResultSlotIsItem04();
            if (isScoreZero == false)
                yield return new WaitForSeconds(1.0f);
            checkAllResultSlotIsItem05();
            if (isScoreZero == false)
                yield return new WaitForSeconds(1.0f);
            checkAllResultSlotIsItem06();
            if (isScoreZero == false)
                yield return new WaitForSeconds(1.0f);
            checkAllResultSlotIsItem07();
            if (isScoreZero == false)
                yield return new WaitForSeconds(1.0f);
            checkAllResultSlotIsItem08();
            if (isScoreZero == false)
                yield return new WaitForSeconds(1.0f);


            yield return new WaitForSeconds(1.1f); //�� �� ��ٸ� �Ŀ� ���� ����
            itemShopPanel.SetActive(true);
            SlotButton.interactable = true; //�г��� ���� ���� ��ư�� ��
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

    void checkAllResultSlotIsItem()
    {
        isScoreZero = true;
        for (int i = 0; i < slot.Length; i++)   //���� ������ŭ �ݺ�
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] > 0) //������ ��ȣ�� 0�� �ƴϸ�
                {
                    Debug.Log("afa");
                    AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, 1);
                }
            }
        }
    }
    void checkAllResultSlotIsItem01()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 1) //�ش� ĭ�� ĭ���� ��
                {
                    if (j > 0 &&(
                        itemInfoBackpack[resultIndexArray[i, j - 1]] > 0)) //��ĭ �˻�
                        scoreAddResult += 5; //������ ������ 5��
                    if (j < 2 &&(
                        itemInfoBackpack[resultIndexArray[i, j + 1]] > 0)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 5; //������ ������ 5��
                    if (i > 0 &&(
                        itemInfoBackpack[resultIndexArray[i - 1, j]] > 0)) //����ĭ �˻�
                        scoreAddResult += 5; //������ ������ 5��
                    if (i < 2 &&(
                        itemInfoBackpack[resultIndexArray[i + 1, j]] > 0)) //������ĭ �˻�
                        scoreAddResult += 5; //������ ������ 5��
                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //����� �������ϱ� �ٷ� 0����
                }
            }
        }
    }
    void checkAllResultSlotIsItem02()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 2) //�ش� ĭ�� ������ ��
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 1)) //��ĭ �˻�
                        scoreAddResult += 15; //��ó�� ĭ���� ������ 15��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 1)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 15;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 1)) //����ĭ �˻�
                        scoreAddResult += 15;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 1)) //������ĭ �˻�
                        scoreAddResult += 15;
                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //����� �������ϱ� �ٷ� 0����
                }
            }
        }
    }
    void checkAllResultSlotIsItem03()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 3) //�ش� ĭ�� Ŭ�ο��϶�
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 1)) //��ĭ �˻�
                        scoreAddResult += 20; //ĭ���� ������ 20��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 1)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 20; 
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 1)) //����ĭ �˻�
                        scoreAddResult += 20;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 1)) //������ĭ �˻�
                        scoreAddResult += 20;

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 2)) //��ĭ �˻�
                        scoreAddResult -= 6; //���ڰ� ������ -6��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 2)) //�Ʒ�ĭ �˻�
                        scoreAddResult -= 6;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 2)) //����ĭ �˻�
                        scoreAddResult -= 6;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 2)) //������ĭ �˻�
                        scoreAddResult -= 6;
                    if (scoreAddResult != 0 )
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //����� �������ϱ� �ٷ� 0����
                }
            }
        }
    }
    void checkAllResultSlotIsItem04()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 4) //�ش� ĭ�� �̷���϶�
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 1 ||
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 3)) //��ĭ �˻�
                        scoreAddResult += 8; //��ֵ� �� �ϳ��� ������ +8��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 1 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 3)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 8;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 1 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 3)) //����ĭ �˻�
                        scoreAddResult += 8;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 1 ||
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 3)) //������ĭ �˻�
                        scoreAddResult += 8;

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 7)) //��ĭ �˻�
                        scoreAddResult += 12; //�ڹپ߽ð� ������ 12�� 
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 7)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 12;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 7)) //����ĭ �˻�
                        scoreAddResult += 12;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 7)) //������ĭ �˻�
                        scoreAddResult += 12;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //����� �������ϱ� �ٷ� 0����
                }
            }
        }
    }
    void checkAllResultSlotIsItem05()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 5) //�ش� ĭ�� ����϶�
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 7)) //��ĭ �˻�
                        scoreAddResult += 40; //�ڹپ߽ð� ������ 40��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 7)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 40;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 7)) //����ĭ �˻�
                        scoreAddResult += 40;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 7)) //������ĭ �˻�
                        scoreAddResult += 40;

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 4 ||
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 6)) //��ĭ �˻�
                        scoreAddResult -= 11; //����, �̷�簡 ������ -11�� 
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 4 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 6)) //�Ʒ�ĭ �˻�
                        scoreAddResult -= 11;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 4 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 6)) //����ĭ �˻�
                        scoreAddResult -= 11;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 4 ||
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 6)) //������ĭ �˻�
                        scoreAddResult -= 11;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //����� �������ϱ� �ٷ� 0����
                }
            }
        }
    }
    void checkAllResultSlotIsItem06()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 6) //�ش� ĭ�� �����϶�
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 7)) //��ĭ �˻�
                        scoreAddResult += 20; //�ڹپ߽ð� ������ 20��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 7)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 20;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 7)) //����ĭ �˻�
                        scoreAddResult += 20;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 7)) //������ĭ �˻�
                        scoreAddResult += 20;

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 5)) //��ĭ �˻�
                        scoreAddResult += 22; //��簡 ������ 22�� 
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 5)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 22;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 5)) //����ĭ �˻�
                        scoreAddResult += 22;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 5)) //������ĭ �˻�
                        scoreAddResult += 22;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //����� �������ϱ� �ٷ� 0����
                }
            }
        }
    }
    void checkAllResultSlotIsItem07()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 7) //�ش� ĭ�� �ڹپ߽��϶�
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 5)) //��ĭ �˻�
                        scoreAddResult += 10; //��簡 ������ 10��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 5)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 10;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 5)) //����ĭ �˻�
                        scoreAddResult += 10;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 5)) //������ĭ �˻�
                        scoreAddResult += 10;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //����� �������ϱ� �ٷ� 0����
                }
            }
        }
    }
    void checkAllResultSlotIsItem08()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        scoreAddResult -= 10; //�����ϸ� �⺻������ -10���� Ȯ��
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 8) //�ش� ĭ�� �������϶�
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] > 0 )) //��ĭ �˻�
                        scoreAddResult -= 6; //�α� ĭ�� ������ -6 ������
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] > 0)) //�Ʒ�ĭ �˻�
                        scoreAddResult -= 6;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] > 0)) //����ĭ �˻�
                        scoreAddResult -= 6;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] > 0)) //������ĭ �˻�
                        scoreAddResult -= 6;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //����� �������ϱ� �ٷ� 0����
                }
            }
        }
    }


    void AddScoreByUIManager(GameObject obj, int score)
    {
        TextUIManager.instance.AddEnergy(score);
        TextUIManager.instance.makeTextEffectSlotScore(obj, score);
        isScoreZero = false; //������ ��µƴٸ� �� ������ �ƹ�ư ������ 0���� �ƴϴϱ� ���� ����.
    }
}

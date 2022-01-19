using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlotMachineManager : MonoBehaviour
{
    public GameObject[] slotMovingObj; //������ ������ ���� ������Ʈ
    public Button[] slot;  //�����ε� ��ư���� �������, �� ������ ���Ƴ���.
    public Button SlotButton; //�̰� �����ϴ� ��ư
    public Button itemBoxButton; //������ ���� ���� ��ư

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

    public int[,] resultIndexArray;  //��÷�� ���� ������ ������ �迭. ��ŸƮ���� �ʱ�ȭ ����

    int itemCountInOneSlot;  //���Կ� �ö� �� ������ ����
    int itemCount = 0; //���� ������ �ִ� ������ ��
    int randomIndex;

    int[] answer = { 0, 0, 0 }; //���� �����ص� ����
    float[] moveDistance = { 20f, 25f, 50f }; //������ ����(2.5 / 5 / 10 / 12.5 / 20 / 25 / 50 /100)�� �ڿ������� �͸�
    public List<int> choiceNum = new List<int>();
    bool isScoreZero = true;

    public GameObject itemShopPanel;
    public GameObject itemBoxPanel;

    // Start is called before the first frame update
    void Start()
    {

        TextUIManager.instance.changeEnergyText(); //���� ��� Ƚ�� ����, ui����
        TextUIManager.instance.changeHeartText(); //���� ��� Ƚ�� ����, ui����

        itemCountInOneSlot = displayItemSlots[0].itemImage.Count - 3; //3���� �̹��� ���̶� ��� �����.
        resultIndexArray = new int[slot.Length, itemCountInOneSlot]; //�̰� ����� 3�̶� 5��.

        for (int x = 0; x < slot.Length; x++)   //���� ������ŭ �ݺ�
        {
            for (int y = 0; y < displayItemSlots[0].itemImage.Count; y++)
            {
                displayItemSlots[x].itemImage[y].sprite = itemSprite[0]; //��� ���� ��� ĭ�� ������
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
        TextUIManager.instance.AddSlotUseCount(); //���� ��� Ƚ�� ����, ui����
        SoundManager.instance.SlotSoundPlay(); //���Թ�ư ���� ���

        SlotButton.interactable = false; //���� ���۵Ǹ� ��ư Ŭ�� �Ұ�.
        itemBoxButton.interactable = false; //���������� ���� ������ Ŭ�� �Ұ�

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

                resultIndexArray[i, j] = randomIndex; //���õ� ���� ��ġ�� �������� ��� �迭�� ���� (�� ���� ������ŭ ���� ��)
                
            }
        }

        for (int i = 0; i < slot.Length; i++)  //���� ������ŭ �ڷ�ƾ ����
        {
            StartCoroutine(StartSlot(i));
        }
    }

    IEnumerator StartSlot(int slotIndex) //���° �����ΰ�
    {
        if (true) //���� �ٸ� �� �־��µ� ����.
        {
            int randNum = Random.Range(0, moveDistance.Length);

            yield return new WaitForSeconds( (1.4f / (slot.Length - 1)) * slotIndex); //0.5���ϱ� ���� ��ȣ��ŭ ��. �� 0���̸� 0��

            // for (int i = 0; i < (itemCountInOneSlot * (2 + slotIndex) + answer[slotIndex]) * 4; i++)
            for (int i = 0; i < 200; i++)
            {
                slotMovingObj[slotIndex].transform.localPosition -= new Vector3(0, moveDistance[randNum], 0);  //ui������Ʈ�� �����̴� �Ŷ� ���� ������ ���

                if (slotMovingObj[slotIndex].transform.localPosition.y < 100f)
                {
                    slotMovingObj[slotIndex].transform.localPosition += new Vector3(0, 500f, 0); //��ġ�� 575�� �ȴ�.
                    for (int a = 0; a < itemCountInOneSlot; a++)
                    {
                        displayItemSlots[slotIndex].itemImage[a].sprite = itemBackpack[resultIndexArray[slotIndex, a]]; //���� �� ���̴� 5�� �ٷ� �ٲ��ֱ�
                    }
                }
                else if (slotMovingObj[slotIndex].transform.localPosition.y < 200f) //100���ٴ� ū�� 200���ٴ� ���� ����. (�� ������ �ƹ�ư ����� �� ���̴� ��)
                {
                    for (int a = 0; a < displayItemSlots[0].itemImage.Count - itemCountInOneSlot; a++) //�̰� �� ���δ� ��÷���� ������.
                    {
                        displayItemSlots[slotIndex].itemImage[a + itemCountInOneSlot].sprite = itemBackpack[resultIndexArray[slotIndex, a]]; //ó�� �׸���� ������ �׸����� ���� ���� �ڵ�
                    }
                }
                yield return new WaitForSeconds(0.03f);  //������ ���� 4�� �� ��
            }
            SoundManager.instance.StopSlotSoundPlay(); //���� ���� ���
            yield return new WaitForSeconds((1.4f / (slot.Length - 1)) * (slot.Length - slotIndex)); //0���̸� 0.5���ϱ� (2-0), �� 1�� ����.
        }

        if (slotIndex == slot.Length - 1)
        {
            yield return new WaitForSeconds(0.2f);
            if (true)
            {
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
                checkAllResultSlotIsItem09();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem10();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem11();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem12();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem13();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem14();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsItem15();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
                checkAllResultSlotIsBingo();
                if (isScoreZero == false)
                    yield return new WaitForSeconds(1.0f);
            }

            yield return new WaitForSeconds(1.1f); //�� �� ��ٸ� �Ŀ� ���� ����
            if (StageInfoManager.instance.StageHeart[StageInfoManager.instance.stageCount] - StageInfoManager.instance.slotUseCount <= 0 ||
                StageInfoManager.instance.StageEnergy[StageInfoManager.instance.stageCount] - StageInfoManager.instance.energy <= 0 )
            { //���� ������ 0�̰ų� �������� �������� ��ǥ�� �Ѿ��
                StageInfoManager.instance.StageClearCheck();
            }
            else
            {
                itemShopPanel.SetActive(true);
            }
            SlotButton.interactable = true; //�г��� ���� ���� ��ư Ŭ�� �ٽ� ����
            itemBoxButton.interactable = true;
        }
    }

    public void ClickButton (int slotNum, int imageNum)
    {
        displayResultImage.sprite = itemBackpack[resultIndexArray[slotNum, imageNum]];
    }
    public void ClickitemBoxButton()
    {
        itemBoxPanel.SetActive(true);
        SoundManager.instance.ButtonSoundPlay(); //��ư ���� ���
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
            for (int j = 0; j < 3; j++)  //����� 3~5����, ����� 3������ ���
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] > 0) //������ ��ȣ�� 0�� �ƴϸ�
                {
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

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 3)) //��ĭ �˻�
                        scoreAddResult += 10; //����, Ŭ�ο� 10��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 3)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 10; 
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 3)) //����ĭ �˻�
                        scoreAddResult += 10; 
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 3)) //������ĭ �˻�
                        scoreAddResult += 10; 

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
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 12 ||
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 7)) //��ĭ �˻�
                        scoreAddResult += 12; //�ڹپ߽�, Ÿ�ɰ� ������ 12�� 
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 12 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 7)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 12;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 12 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 7)) //����ĭ �˻�
                        scoreAddResult += 12;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 12 ||
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
                        scoreAddResult -= 13; //����, �̷�簡 ������ -11�� 
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 4 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 6)) //�Ʒ�ĭ �˻�
                        scoreAddResult -= 13;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 4 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 6)) //����ĭ �˻�
                        scoreAddResult -= 13;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 4 ||
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 6)) //������ĭ �˻�
                        scoreAddResult -= 13;

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
                        scoreAddResult += 17; //�ڹپ߽ð� ������ 20��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 7)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 17;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 7)) //����ĭ �˻�
                        scoreAddResult += 17;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 7)) //������ĭ �˻�
                        scoreAddResult += 17;

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
                        itemInfoBackpack[resultIndexArray[i, j - 1]] > 0)) //��ĭ �˻�
                        scoreAddResult += 5; //������ ������ 5��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] > 0)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 5; //������ ������ 5��
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] > 0)) //����ĭ �˻�
                        scoreAddResult += 5; //������ ������ 5��
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] > 0)) //������ĭ �˻�
                        scoreAddResult += 5; //������ ������ 5��

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
    void checkAllResultSlotIsItem09()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 9) //�ش� ĭ�� ���ھ��϶�
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 10)) //��ĭ �˻�
                        scoreAddResult += 30; //��Ÿ�� ������ 30��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 10)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 30;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 10)) //����ĭ �˻�
                        scoreAddResult += 30;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 10)) //������ĭ �˻�
                        scoreAddResult += 30;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //����� �������ϱ� �ٷ� 0����
                }
            }
        }
    }
    void checkAllResultSlotIsItem10()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 10) //�ش� ĭ�� ��Ÿ�϶�
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 9)) //��ĭ �˻�
                        scoreAddResult += 20; //���ھư� ������ 9��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 9)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 20;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 9)) //����ĭ �˻�
                        scoreAddResult += 20;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 9)) //������ĭ �˻�
                        scoreAddResult += 20;

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 1 ||
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 3)) //��ĭ �˻�
                        scoreAddResult += 3; //����, �̷�簡 ������ -11�� 
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 1 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 3)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 3;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 1 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 3)) //����ĭ �˻�
                        scoreAddResult += 3;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 1 ||
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 3)) //������ĭ �˻�
                        scoreAddResult += 3;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //����� �������ϱ� �ٷ� 0����
                }
            }
        }
    }
    void checkAllResultSlotIsItem11()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 11) //�ش� ĭ�� �����϶�
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 7)) //��ĭ �˻�
                        scoreAddResult += 25; //�ڹپ߽ð� ������ 40��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 7)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 25;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 7)) //����ĭ �˻�
                        scoreAddResult += 25;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 2 ||
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 7)) //������ĭ �˻�
                        scoreAddResult += 25;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //����� �������ϱ� �ٷ� 0����
                }
            }
        }
    }
    void checkAllResultSlotIsItem12()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 12) //�ش� ĭ�� Ÿ���϶�
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 4)) //��ĭ �˻�
                        scoreAddResult += 10; //�̷�� 10��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 4)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 10;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 4)) //����ĭ �˻�
                        scoreAddResult += 10;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 4)) //������ĭ �˻�
                        scoreAddResult += 10;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //����� �������ϱ� �ٷ� 0����
                }
            }
        }
    }
    void checkAllResultSlotIsItem13()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 13) //�ش� ĭ�� ����϶�
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 8)) //��ĭ �˻�
                        scoreAddResult += 5; //������5��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 8)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 5;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 8)) //����ĭ �˻�
                        scoreAddResult += 5;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 8)) //������ĭ �˻�
                        scoreAddResult += 5;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //����� �������ϱ� �ٷ� 0����
                }
            }
        }
    }
    void checkAllResultSlotIsItem14()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 14) //�ش� ĭ�� ���Ҷ��϶�
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] > 0)) //��ĭ �˻�
                        scoreAddResult += 10; //������10��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] > 0)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 10;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] > 0)) //����ĭ �˻�
                        scoreAddResult += 10;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] > 0)) //������ĭ �˻�
                        scoreAddResult += 10;

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 6)) //��ĭ �˻�
                        scoreAddResult += 10; //����, �̷�簡 ������ -11�� 
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 6)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 10;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 6)) //����ĭ �˻�
                        scoreAddResult += 10;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 6)) //������ĭ �˻�
                        scoreAddResult += 10;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //����� �������ϱ� �ٷ� 0����
                }
            }
        }
    }
    void checkAllResultSlotIsItem15()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] == 15) //�ش� ĭ�� ��粿���϶�
                {
                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] > 0)) //��ĭ �˻�
                        scoreAddResult += 15; //������ 15��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] > 0)) //�Ʒ�ĭ �˻�
                        scoreAddResult += 15;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] > 0)) //����ĭ �˻�
                        scoreAddResult += 15;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] > 0)) //������ĭ �˻�
                        scoreAddResult += 15;

                    if (j > 0 && (
                        itemInfoBackpack[resultIndexArray[i, j - 1]] == 7)) //��ĭ �˻�
                        scoreAddResult -= 10; //�ڹپ߽� -10��
                    if (j < 2 && (
                        itemInfoBackpack[resultIndexArray[i, j + 1]] == 7)) //�Ʒ�ĭ �˻�
                        scoreAddResult -= 10;
                    if (i > 0 && (
                        itemInfoBackpack[resultIndexArray[i - 1, j]] == 7)) //����ĭ �˻�
                        scoreAddResult -= 10;
                    if (i < 2 && (
                        itemInfoBackpack[resultIndexArray[i + 1, j]] == 7)) //������ĭ �˻�
                        scoreAddResult -= 10;

                    if (scoreAddResult != 0)
                        AddScoreByUIManager(displayItemSlots[i].itemImage[j].gameObject, scoreAddResult);
                    scoreAddResult = 0; //����� �������ϱ� �ٷ� 0����
                }
            }
        }
    }
    void checkAllResultSlotIsBingo()
    {
        isScoreZero = true;
        int scoreAddResult = 0;
        for (int i = 0; i < slot.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (itemInfoBackpack[resultIndexArray[i, j]] > 0)
                {
                    if ((itemInfoBackpack[resultIndexArray[i, 0]] == itemInfoBackpack[resultIndexArray[i, 1]]) &&
                            (itemInfoBackpack[resultIndexArray[i, 1]] == itemInfoBackpack[resultIndexArray[i, 2]]))
                        scoreAddResult += 50; //������ 50��
                    if ((itemInfoBackpack[resultIndexArray[0, j]] == itemInfoBackpack[resultIndexArray[1, j]]) &&
                            (itemInfoBackpack[resultIndexArray[1, j]] == itemInfoBackpack[resultIndexArray[2, j]]))
                        scoreAddResult += 50; //������ 50��
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

        if (score >= 50)
            SoundManager.instance.JackPotSoundPlay(); //���� ���� ���
        else
            SoundManager.instance.ScoreUpSoundPlay(); //���� ���� ���
    }
}

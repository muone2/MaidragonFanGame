using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
        // DontDestroyOnLoad(gameObject);
    }

    public SlotMachineManager slotMachineManager;
    public GameObject itemShopPanel;

    public Text dialogText;
    public Text nameTagText;
    public Image SayingCharImage;
    public List<string> dialog = new List<string>();
    public List<string> ItmeName = new List<string>();
    public List<int> SayingCharNum = new List<int>();

    public int dialogReadCount = 0;
    public Sprite nullSprite;
    bool first = true;

    public void OnEnable()
    {
        if (first)
        {
            first = false;
            for (int i = 0; i < 30; i++)
            {
                ItmeName.Add(null);
            }

            ItmeName[0] = " ";
            ItmeName[1] = "ĭ��";
            ItmeName[2] = "����";
            ItmeName[3] = "Ŭ�ο�";
            ItmeName[4] = "�̷��";
            ItmeName[5] = "�丣";
            ItmeName[6] = "����";
            ItmeName[7] = "�ڹپ߽�";
            ItmeName[8] = "������";
            ItmeName[9] = "���ھ�";
            ItmeName[10] = "��Ÿ";
            ItmeName[11] = "����";
            ItmeName[12] = "Ÿ��";
            ItmeName[13] = "ŸŰ��";
            ItmeName[14] = "���Ҷ�";
            ItmeName[15] = "��������";

            for (int i = 0; i < ItmeName.Count; i++)
            {
                if (ItmeName[i] == null)
                {
                    ItmeName[i] = "������ �� ��";
                }
            }
        }
        StageInfoManager.instance.stageCount++ ; //��ȭ ������ �������Ͱ� ���� ��������. (1���������� �����ڸ��ڴϱ� �ʱ� ���� 0�̾�� ��)
        StageInfoManager.instance.energy = 0;
        StageInfoManager.instance.slotUseCount = 0;  //������������ ���� ���� �ʱ�ȭ
        dialogReadCount = 0; //��ȭ ���൵ �ʱ�ȭ

        if (StageInfoManager.instance.stageCount == 1)
            makeStage1Dialog();
        else if (StageInfoManager.instance.stageCount == 2)
            makeStage2Dialog();
        else if (StageInfoManager.instance.stageCount == 3)
            makeStage3Dialog();
        else if (StageInfoManager.instance.stageCount == 4)
            makeStage4Dialog();
        else if (StageInfoManager.instance.stageCount == 5)
            makeStage5Dialog();
        else if (StageInfoManager.instance.stageCount == 6)
            makeStage6Dialog();
        else
        {
            Debug.Log("STAGE COUNT errrrrrrer");
            gameObject.SetActive(false); //������Ʈ�� ��
        }

        writeDialogAndImage(); //0�� ��� ���
    }

    public void makeStage1Dialog()
    {
        dialog.Clear();
        SayingCharNum.Clear();
        for (int i = 0; i < 70; i++)
        {
            dialog.Add(null);
            SayingCharNum.Add(-1);
        }

        dialog[0] = "�ϱ���, �ڹپ߽ó� ����Ʈ ��";
        dialog[1] = "����ī��, �� ��.";
        dialog[2] = "��, ���� ��.";
        dialog[3] = "(���� ĭ���� �� �ְ� ������... ����... ���� ������?)";
        dialog[4] = "(...���Ŵ�!)";
        dialog[5] = "����ī��? �� �׷�?";
        dialog[6] = "��? ����.";
        dialog[7] = "��, ���� ĭ���� �� ������ �ƴϾ�? ���� �ִµ�?";
        dialog[8] = "��¥��. ���� �־�.";
        dialog[9] = "������.";
            dialog[10] = "ĭ���� ������ ���� ���� ���ڸ� ������.";
            dialog[11] = "Ŭ�ο�!";
            dialog[12] = "Ŭ-��-��-?!";
            dialog[13] = "�� ���� ����?";
            dialog[14] = "ĭ��, �װ� �̸� ��. ������ �� �ƴ��� ���� ���� ����.";
            dialog[15] = "�Ⱦ�. ���� ������.";
            dialog[16] = "��?! ĭ��! ��� ��!";
            dialog[17] = "���� �� �ص� ��. ������ �� �ƴϾ�.";
            dialog[18] = "(���� ������ �Ŷ�� �ΰ��� ���ϴϱ� ���� Ȯ���ؾ� ��. �Ƹ� ����������.)";
            dialog[19] = "������ �Ÿ� ��¼����! ĭ��! ����!";
            dialog[20] = "ĭ���� ���ڸ� ���� ���ӱ�ó�� ���� ���ǰ� ���� ������ ���´�.";
        dialog[21] = "ĭ���� ��� �޸��鼭 ���ӱ⸦ �丮���� ���캻��.";
            dialog[22] = "���� ������ ��������... ��?!";
            dialog[23] = "����� �Ϸ�Ǿ����ϴ�.";
            dialog[24] = "ĭ��?! �� �׷�! ������?";
            dialog[25] = "����. ���� ����������. ������.";
            dialog[26] = "����, ���� ���� Ȯ���� ���ٴϱ�. �̸� ��.";
            dialog[27] = "�װ� ������ �ƴ� �� ������.";
            dialog[28] = "Ŭ�ο� �´� �̷� ������ �� ĭ������ ������ ��¼�ڴ� �ž�. ������ ������ �Ѹ��� ����߰ھ�.";
            dialog[29] = "���� ���������� ���̶�ϱ�.";
            dialog[30] = "�׷���! ĭ��, ���� ������? ������?";
            dialog[31] = "��, ������. ��ģ ���� ����.";
            dialog[32] = "ĭ���� ������ ��¦��¦ �ڴ�.";
            dialog[33] = "�׷��ٸ� ����������.";
            dialog[34] = "����� �Ϸ�Ǿ����ϴ�.";
            dialog[35] = "��? ���� �Ҹ���? ��, ĭ��! ��¥ ������ �� �¾�?";
            dialog[36] = "����. �������� ��, ����ī��.";
            dialog[37] = "�ڲ� �׷��µ� ��� ������ �� ��!";
            dialog[38] = "(���� ����� ��������. �ϴ� ����ī�͸� �Ƚɽ�Ű��. ���Ǹ� ������ ��.)";
            dialog[39] = "�׷��� ��� �Ҹ��� ������?";
            dialog[40] = "���ڱ�? ��... ���⼭ �� �� ������.";
            dialog[41] = "��? ���� ĭ����. Ŭ�ο��� �ְ�.";
            dialog[42] = "�̰� ���ӱ��ΰ� ��. ��� ���� ĭ���� ���������� �𸣰�����.";
            dialog[43] = "�� �߿� �ϳ��� �����ϴ� �ǰ�? �׷� �翬�� ĭ����...";
            dialog[44] = "(��, �� �Ѿ �� ����.)";
        

        SayingCharNum[0] = 0;
        SayingCharNum[1] = 1;
        SayingCharNum[2] = 2;
        SayingCharNum[3] = 2;
        SayingCharNum[4] = 2;
        SayingCharNum[5] = 1;
        SayingCharNum[6] = 2;
        SayingCharNum[7] = 2;
        SayingCharNum[8] = 1;
        SayingCharNum[9] = 2;
        SayingCharNum[10] = 0;
        SayingCharNum[11] = 1;
        SayingCharNum[12] = 2;
        SayingCharNum[13] = 1;
        SayingCharNum[14] = 2;
        SayingCharNum[15] = 1;
        SayingCharNum[16] = 2;
        SayingCharNum[17] = 1;
        SayingCharNum[18] = 1;
        SayingCharNum[19] = 2;
        SayingCharNum[20] = 0;
        SayingCharNum[21] = 0;
        SayingCharNum[22] = 1;
        SayingCharNum[23] = 0;
        SayingCharNum[24] = 2;
        SayingCharNum[25] = 1;
        SayingCharNum[26] = 2;
        SayingCharNum[27] = 1;
        SayingCharNum[28] = 2;
        SayingCharNum[29] = 1;
        SayingCharNum[30] = 2;
        SayingCharNum[31] = 1;
        SayingCharNum[32] = 0;
        SayingCharNum[33] = 2;
        SayingCharNum[34] = 0;
        SayingCharNum[35] = 2;
        SayingCharNum[36] = 1;
        SayingCharNum[37] = 2;
        SayingCharNum[38] = 1;
        SayingCharNum[39] = 1;
        SayingCharNum[40] = 2;
        SayingCharNum[41] = 2;
        SayingCharNum[42] = 2;
        SayingCharNum[43] = 2;
        SayingCharNum[44] = 1;
    }
    public void makeStage2Dialog()
    {
        dialog.Clear();
        SayingCharNum.Clear();
        for (int i = 0; i < 70; i++)
        {
            dialog.Add(null);
            SayingCharNum.Add(-1);
        }

        dialog[0] = "������ �Ϸ�Ǿ����ϴ�.";
        SayingCharNum[0] = 0;
        dialog[1] = "������";
        SayingCharNum[1] = 1;
        dialog[2] = "�׷���. ��ǥ�� �޼��ߴµ�. ���� �����Ǿ��ٴ� ����?";
        SayingCharNum[2] = 2;
        dialog[3] = "�𸣰ھ�";
        SayingCharNum[3] = 1;
        dialog[4] = "Ȥ�� ���ӱⰡ �ƴѰ�? �׷� �� ������...";
        SayingCharNum[4] = 2;
        dialog[5] = "�� ��. ���� Ȯ���� ����.";
        SayingCharNum[5] = 1;
        dialog[6] = "�� �� ���� �������� ���ݾ�.";
        SayingCharNum[6] = 2;
        dialog[7] = "�Ⱦ�. ���� ���� �ҷ�. ����ī�͸� �߾�. ġ����.";
        SayingCharNum[7] = 1;
        dialog[8] = "��. �׷� �Ŷ��. ��.";
        SayingCharNum[8] = 2;
        dialog[9] = "(����ī�� ����.)";
        SayingCharNum[9] = 1;
        dialog[10] = "(�� ƴ�� �Ʊ�� �� �׷����� �˾Ƴ��� ��. ����ī�Ͱ� �ٽ� �������� ����.)";
        SayingCharNum[10] = 1;
        dialog[11] = "ĭ��, �ű⼭ ���ؿ�.";
        SayingCharNum[11] = 5;
        dialog[12] = "�丣 ��. ���� ��. �̷��.";
        SayingCharNum[12] = 1;
        dialog[13] = "�ȳ��ϼ���.";
        SayingCharNum[13] = 2;
        dialog[14] = "�ȳ�";
        SayingCharNum[14] = 6;
        dialog[15] = "���ϰ� �־�? �װ� ����?";
        SayingCharNum[15] = 4;
        dialog[16] = "�丣 ��. �丣 ��.";
        SayingCharNum[16] = 1;
        dialog[17] = "��? �� �׷���? �Ϳ�?";
        SayingCharNum[17] = 5;
        dialog[18] = "(�̰� ���� �𸣰ھ�. ������ �� ���� �־�. Ȥ�� �˾�?)";
        SayingCharNum[18] = 1;
        dialog[19] = "(�۽��. ���� ��... �� �� Ȯ���غ����.)";
        SayingCharNum[19] = 5;
    }
    public void makeStage3Dialog()
    {
        dialog.Clear();
        SayingCharNum.Clear();
        for (int i = 0; i < 70; i++)
        {
            dialog.Add(null);
            SayingCharNum.Add(-1);
        }

        dialog[0] = "(����. ���� �� ��.)";
        SayingCharNum[0] = 1;
        dialog[1] = "�丣 ��. �ڹپ߽ô� ������ �˱�?";
        SayingCharNum[1] = 1;
        dialog[2] = "�ڹپ߽� ���� �𸣴� �� ���ٰ��. �и� �̰� ���� �ƽ� �ſ���.";
        SayingCharNum[2] = 5;
        dialog[3] = "�׷��� ĭ��, �����ƿ�? �Ʊ�� �Ȼ��� �� �� �������µ�.";
        SayingCharNum[3] = 5;
        dialog[4] = "������. ���� �� �ص� ��.";
        SayingCharNum[4] = 1;
        dialog[5] = "�׷��� �����. ����ī�͵� ���� ���ݾƿ�. ������ ���ؿ�.";
        SayingCharNum[5] = 5;
        dialog[6] = "�װ� ������ �������� ���� ���� ��û ���ſ�.";
        SayingCharNum[6] = 1;
        dialog[7] = "�׸����?";
        SayingCharNum[7] = 5;
        dialog[8] = "���� �� ����. ������ ���� �� ���� �� ������ ����. �ȴ� �͵� �����.";
        SayingCharNum[8] = 1;
        dialog[9] = "���ݸ� ���ƿ�. ���� �� �Ծ��.";
        SayingCharNum[9] = 5;
        dialog[10] = "�丣? ���⼭ ����?";
        SayingCharNum[10] = 7;
        dialog[11] = "��, �ڹپ߽� ��! ���? ȸ�� �ȿ� ������ �� �ƴϼ̳���?";
        SayingCharNum[11] = 5;
        dialog[12] = "��. ��� ���Ծ�. �׺��� �ٵ�...";
        SayingCharNum[12] = 7;
        dialog[13] = "����.";
        SayingCharNum[13] = 4;
        dialog[14] = "�ٸ� ������� �׷���ġ��, ������ ���� ���� �ʾҾ�?";
        SayingCharNum[14] = 7;
        dialog[15] = "����� ���� ���Ҷ��� ������ ���. ���� ���־���.";
        SayingCharNum[15] = 6;
        dialog[16] = "�츣�� ������ �� ���ϱ� ������ ���� �� ������. �̷���?";
        SayingCharNum[16] = 7;
        dialog[17] = "��վ� ������ ����Դ�.";
        SayingCharNum[17] = 4;
        dialog[18] = "�ڿ� �е�����?";
        SayingCharNum[18] = 7;
        dialog[19] = "��¼�ٺ���";
        SayingCharNum[19] = 9;
        dialog[20] = "ŸŰ������ ������ �ִ�. ���� ����.";
        SayingCharNum[20] = 8;
        dialog[21] = "���� ���� ���Ҿ��. �׺��� �ڹپ߽� ��, �̰� �� ���ֽðھ��?";
        SayingCharNum[21] = 5;
        dialog[22] = "��? ���ӱ�? ���� �ӽ�?";
        SayingCharNum[22] = 7;
        dialog[23] = "ĭ���� �̰� �� �ڷ� ���°� �� �����. Ȥ�� ���۰��� �� �����Ű���?";
        SayingCharNum[23] = 5;
        dialog[24] = "����. �� ���� ���������� �־�.";
        SayingCharNum[24] = 1;
        dialog[25] = "ĭ��? ����. �巡���� ���� �ſ� ���ؼ��� ���� ����.";
        SayingCharNum[25] = 7;
        dialog[26] = "�׷�����. �ڹپ߽� ����� ���� �ƽ� �� ���Ҵµ� ������.";
        SayingCharNum[26] = 5;
        dialog[27] = "ĭ�� ���δ� �� ��谡 �ǽɽ����ٰ� �ϴϱ��.";
        SayingCharNum[27] = 5;
        dialog[28] = "���ӱ�... �׷��� �̰� ��� �� �ž�? ó�� ���µ�.";
        SayingCharNum[28] = 7;
        dialog[29] = "�׷��Կ�. ĭ��?";
        SayingCharNum[29] = 5;
        dialog[30] = "���� �� �ù迡 �־���. Ŭ�ο��� ���¾�.";
        SayingCharNum[30] = 1;
        dialog[31] = "Ŭ�ο�? �׷� �����ٴ� Ŭ�ο��� ���� ���� ������.";
        SayingCharNum[31] = 7;
        dialog[32] = "��.";
        SayingCharNum[32] = 1;
        dialog[33] = "�׷�����! ���� �ڹپ߽� ������!";
        SayingCharNum[33] = 5;
    }
    public void makeStage4Dialog()
    {
        dialog.Clear();
        SayingCharNum.Clear();
        for (int i = 0; i < 70; i++)
        {
            dialog.Add(null);
            SayingCharNum.Add(-1);
        }

        dialog[0] = "��ȭ�� ������ ���� �־��׿�. �� ������ �� �� ���ٴ�.";
        SayingCharNum[0] = 5;
        dialog[1] = "��, �丣 ��. �¿��༭ ����.";
        SayingCharNum[1] = 1;
        dialog[2] = "�� �ڷ� �� ��� �� �������� �� �� �������ϱ��.";
        SayingCharNum[2] = 5;
        dialog[3] = "���� ���� Ÿ�� ���� �Ѿ��, ��Ÿ���� �𸥴ٴ� �� ���� ������ ���� �����δ� ��¿ �浵�� ���ٴ� ���̿���.";
        SayingCharNum[3] = 5;
        dialog[4] = "�´�. ��� �� �ϳ� ��ġë��.";
        SayingCharNum[4] = 1;
        dialog[5] = "��? ������?";
        SayingCharNum[5] = 5;
        dialog[6] = "������� ���� ������ ������ ���� ��������.";
        SayingCharNum[6] = 1;
        dialog[7] = "�׷�����? ���� ��...";
        SayingCharNum[7] = 5;
        dialog[8] = "��, ���� �����ٴ� �� �巡���̶� �׷� �� �ƴ϶�� �ų׿�.";
        SayingCharNum[8] = 5;
        dialog[9] = "�丣 ���� �ʹ� ���ؼ� �׷� �� ���� ����.";
        SayingCharNum[9] = 1;
        dialog[10] = "�׷� ���� �ְڳ׿�.";
        SayingCharNum[10] = 5;
        dialog[11] = "���� ���� ������. ������ �״� �����ؿ�.";
        SayingCharNum[11] = 5;
    }
    public void makeStage5Dialog()
    {
        dialog.Clear();
        SayingCharNum.Clear();
        for (int i = 0; i < 70; i++)
        {
            dialog.Add(null);
            SayingCharNum.Add(-1);
        }

        dialog[0] = "ĭ��! �̷��� ���� �ð��� ��¾ ���̾�.";
        SayingCharNum[0] = 3;
        dialog[1] = "Ŭ�ο�! �̰�.";
        SayingCharNum[1] = 1;
        dialog[2] = "���� ���� ������! �� �޾ұ���. �����̴�.";
        SayingCharNum[2] = 3;
        dialog[3] = "������? �ٵ� ���ӱ��� �߾�.";
        SayingCharNum[3] = 1;
        dialog[4] = "��. ������ �ؾ� �� �� �ִ� �������. ��þ�?";
        SayingCharNum[4] = 3;
        dialog[5] = "�� ���� Ư�� �ֹ� �������� �������.";
        SayingCharNum[5] = 3;
        dialog[6] = "�� �𸣰ھ�. �̰� �������� ��� �������� �𸣰ڰ�. �׷��� ������� �Ծ�.";
        SayingCharNum[6] = 1;
        dialog[7] = "��? ������ ��� ������ ������ݾ�. ���� �� �þ�?";
        SayingCharNum[7] = 3;
        dialog[8] = "��.";
        SayingCharNum[8] = 1;
        dialog[9] = "��?";
        SayingCharNum[9] = 3;
        dialog[10] = "�׺���, �װ� ���� �̻���. ������ ������ ������ �ڲٸ� ���� ��������.";
        SayingCharNum[10] = 1;
        dialog[11] = "���ݵ�... �� �ֱ⵵...";
        SayingCharNum[11] = 1;
        dialog[12] = "��, ĭ��?!";
        SayingCharNum[12] = 3;
        dialog[13] = "ĭ��!";
        SayingCharNum[13] = 5;
    }
    public void makeStage6Dialog()
    {
        dialog.Clear();
        SayingCharNum.Clear();
        for (int i = 0; i < 70; i++)
        {
            dialog.Add(null);
            SayingCharNum.Add(-1);
        }

        dialog[0] = "ĭ��! ������ ���?";
        SayingCharNum[0] = 3;
        dialog[1] = "...����. Ŭ�ο�? �׸��� �丣 ��? �ڹپ߽�?";
        SayingCharNum[1] = 1;
        dialog[2] = "���̾�. ���� �� �? ������?";
        SayingCharNum[2] = 7;
        dialog[3] = "�̾���! �װ� �׷� �� �� ������. �� �����̾�.";
        SayingCharNum[3] = 3;
        dialog[4] = "����?";
        SayingCharNum[4] = 1;
        dialog[5] = "�� ���ӱ� ���ݾ�, ��� ���⸦ ������ �� �ִ� ����ε�.";
        SayingCharNum[5] = 7;
        dialog[6] = "��, �װ� �����.";
        SayingCharNum[6] = 1;
        dialog[7] = "�ֺ��� ������ ����ؼ� �����ؾ� �� �� �ִ� �ſ���.";
        SayingCharNum[7] = 7;
        dialog[8] = "...��! �׷���.";
        SayingCharNum[8] = 1;
        dialog[9] = "�̾���, ĭ��.";
        SayingCharNum[9] = 3;
        dialog[10] = "������. ������ ����������.";
        SayingCharNum[10] = 1;
        dialog[11] = "�� ������ �����ϱ��. �ڹپ߽� ���� ��� ������ �ϴ���.";
        SayingCharNum[11] = 5;
        dialog[12] = "�ְ� ������ ���ݾ�. ������ �� �� ���� ����.";
        SayingCharNum[12] = 7;
        dialog[13] = "�̰� �������� ĭ���� �� �� �ְ� ���ļ� �ٽ� �����ٰ�. ���� �̾���.";
        SayingCharNum[13] = 3;
        dialog[14] = "��. �����ٴϱ�.";
        SayingCharNum[14] = 1;
        dialog[15] = "�´�. �׷� ����ī�� �͵� ������. ���� ��հ� �ϴ� �� ���Ҿ�.";
        SayingCharNum[15] = 1;
        dialog[16] = "�˾Ҿ�. �׷� �� �͵� ���� �ٰ��� ��Ʈ�� �ؾ߰ڴ�.";
        SayingCharNum[16] = 3;
        dialog[17] = "����... ��, ū�� ���� ������ �� �ǰ�. �����̴�.";
        SayingCharNum[17] = 7;
    }



    public void clickDialogPanel() //��ȭâ �������� �� ȭ�� ������ �ǰ� ��ư ���� ����.
    {
        dialogReadCount++; //���� �߰� ��
        writeDialogAndImage(); //�ؽ�Ʈ ����
        SoundManager.instance.DialogClickSoundPlay();
    }
    public void clickSkipButton() //��ŵ Ŭ����
    {
        dialogReadCount = dialog.Count - 2; //������ �� ���� �� �ۿ� ���� ������ �ű��
        writeDialogAndImage(); //�ؽ�Ʈ ���� (�������� Ȯ���ϴ� �ڵ� �������)
        SoundManager.instance.ButtonSoundPlay();
    }

    public void writeDialogAndImage()
    {
        if (dialog[dialogReadCount] == null || SayingCharNum[dialogReadCount] == -1) //0���� 49���� �⺻������ �̷��� �ʱ�ȭ ��
        {
            if(StageInfoManager.instance.stageCount == 6) { StageInfoManager.instance.goodendingPanel1.SetActive(true); }
            itemShopPanel.SetActive(true);
            TextUIManager.instance.changeHeartText();
            TextUIManager.instance.changeEnergyText();
            EnergySlider.instance.ChangeEnergyBar();
            gameObject.SetActive(false); //������Ʈ�� ��
        }
        else
        {
            dialogText.text = dialog[dialogReadCount];
            nameTagText.text = ItmeName[SayingCharNum[dialogReadCount]];
            SayingCharImage.sprite = slotMachineManager.itemSprite[SayingCharNum[dialogReadCount]];
            if(SayingCharNum[dialogReadCount] == 0)
            {
                SayingCharImage.sprite = nullSprite;
            }
        }
    }

}

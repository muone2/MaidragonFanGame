using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoTextManager : MonoBehaviour
{
    public static ItemInfoTextManager instance;

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

    public string[] ItmeInfoText = new string[50];

    public void Start()
    {
        for (int i = 0; i < ItmeInfoText.Length; i++)
        {
            ItmeInfoText[i] = null;
        }

        ItmeInfoText[0] = "��ĭ ǥ�ÿ� �� �̹���. �ٵ� �̰� ������ �� �Ǵ� �����ε� �� ������?";
        ItmeInfoText[1] = "= ĭ��" + "\n" + "����� ����� ���� ���� �ְ� ���θ� �巡��." + "\n\n" + "�����̵� ������ +5" + "\n" + "����, Ŭ�ο� ������ +10" + "\n" + "��л� ������ +10";
        ItmeInfoText[2] = "= ����" + "\n" + "ĭ�� �ʹ� ���� ���쿡����. �����ɰ� �ڱ�ְ� ���� ������ ������ ����." + "\n\n" + "ĭ�� ������+15 " ;
        ItmeInfoText[3] = "= Ŭ�ο�" + "\n" + "ĭ���� �̱� ģ��. ��û�� ����� ������." + "\n\n" + "ĭ�� ������ +20" + "\n" + "���� ������ -6";
        ItmeInfoText[4] = "= �̷��" + "\n" + "���� �Ŵ��� ȭ�� �ָӴϸ� �ް� �ٴϴ� ������ ������ � �巡��." + "\n\n" + "���̵� ������ +8" + "\n" + "�ڹپ߽� ������ +12";
        ItmeInfoText[5] = "= ���" + "\n" + "������ ����� ������ ����� ���̽�Ű �ڹپ߽û�~�� �ְ��� ���̵巡��" + "\n\n" + "�ڹپ߽� ������ +40" + "\n" + "����, �̷�� ������ -11";

        for (int i = 0; i < ItmeInfoText.Length; i++)
        {
            if (ItmeInfoText[i] == null)
            {
                ItmeInfoText[i] = "���� �� ����";
            }
        }
    }
}

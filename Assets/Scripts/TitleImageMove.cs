using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleImageMove : MonoBehaviour
{
    public List<Sprite> titleImages;
    public List<int> titleimagesNum;

    public Image titleImageObjImage;

    public float timeForOneImage;

    int isMadeRightTrue1Flase2;
    int randomIndex;
    bool isMoveEnd ;
    bool isFirstImage ;

    // Start is called before the first frame update
    void Start()
    {
        isMoveEnd = true;
        isFirstImage = true;
    }


    // Update is called once per frame
    void Update()
    {
        if(isMoveEnd == true)
        {
            isMoveEnd = false; //1���� �ǰ�
            MakeRandomTitle();
        }

    }

    void MakeRandomTitle()
    {
        isMadeRightTrue1Flase2 = Random.Range(1, 3); //1�ƴϸ� 2

        if (isMadeRightTrue1Flase2 == 1)
            titleImageObjImage.transform.localPosition = new Vector3(25, 0, 0);
        else if (isMadeRightTrue1Flase2 == 2)
            titleImageObjImage.transform.localPosition = new Vector3(-25, 0, 0);

        randomIndex = Random.Range(0, titleImages.Count);  //0���� ���� ���� ĭ �������� �߿� ���� ����

        for (int a = 0; a < 1000; a++)  //���� �ݺ��� �������� �ϴ� �ּ����� ����⸦...
        {
            if (isFirstImage == true)
            {
                randomIndex = 0;  //ù �̹����� 0�� �̹����� ����
                titleimagesNum.Add(randomIndex);
                isFirstImage = false;
                a = 1001; //�ݺ��� Ż��
            }
            else if (titleimagesNum.Contains(randomIndex))
            {
                randomIndex = Random.Range(0, titleImages.Count); // ��ġ�� �ٽ� ����
            }
            else 
            {
                titleimagesNum.Add(randomIndex);  //�� ��ġ��
                a = 1001; //�ݺ��� Ż��
            }
        }

        if (titleimagesNum.Count > 10) //��� �̹��� ���� 10���� �Ѿ��
            titleimagesNum.RemoveAt(0); //���� ������ ���ƴ� �̹����� ����Ʈ���� ����

        titleImageObjImage.sprite = titleImages[randomIndex];

        StartCoroutine(MoveImageObj());
    }

    IEnumerator MoveImageObj()
    {
        if (isMadeRightTrue1Flase2 == 1)
        {
            for (int i = 0; i < 500; i++)
            {
                titleImageObjImage.transform.localPosition -= new Vector3(0.1f, 0, 0); //100���̴ϱ� ���������δ� 50���� -50������
                yield return new WaitForSeconds(0.002f * timeForOneImage); //�����Ϳ��� ���� �ð���ŭ ��. (����� �׺��� ���� �ణ ���Դ�.)
            }
        }
        else if (isMadeRightTrue1Flase2 == 2)
        {
            for (int i = 0; i < 500; i++)
            {
                titleImageObjImage.transform.localPosition += new Vector3(0.1f, 0, 0); //100���̴ϱ� ���������δ� 50���� -50������
                yield return new WaitForSeconds(0.002f * timeForOneImage); //�����Ϳ��� ���� �ð���ŭ ��.
            }
        }
        //���� �������� ���� �ڿ� �ڿ� ���ǹ� Ǯ��
        isMoveEnd = true;
    }

}

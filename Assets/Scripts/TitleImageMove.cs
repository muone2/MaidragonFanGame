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
            isMoveEnd = false; //1번만 되게
            MakeRandomTitle();
        }

    }

    void MakeRandomTitle()
    {
        isMadeRightTrue1Flase2 = Random.Range(1, 3); //1아니면 2

        if (isMadeRightTrue1Flase2 == 1)
            titleImageObjImage.transform.localPosition = new Vector3(25, 0, 0);
        else if (isMadeRightTrue1Flase2 == 2)
            titleImageObjImage.transform.localPosition = new Vector3(-25, 0, 0);

        randomIndex = Random.Range(0, titleImages.Count);  //0부터 현재 가방 칸 갯수까지 중에 숫자 선택

        for (int a = 0; a < 1000; a++)  //무한 반복이 무서워서 일단 최소한의 제어기를...
        {
            if (isFirstImage == true)
            {
                randomIndex = 0;  //첫 이미지는 0번 이미지로 고정
                titleimagesNum.Add(randomIndex);
                isFirstImage = false;
                a = 1001; //반복문 탈출
            }
            else if (titleimagesNum.Contains(randomIndex))
            {
                randomIndex = Random.Range(0, titleImages.Count); // 겹치면 다시 선택
            }
            else 
            {
                titleimagesNum.Add(randomIndex);  //안 겹치면
                a = 1001; //반복문 탈출
            }
        }

        if (titleimagesNum.Count > 10) //사용 이미지 수가 10개를 넘어가면
            titleimagesNum.RemoveAt(0); //제일 예전에 사용됐던 이미지를 리스트에서 제거

        titleImageObjImage.sprite = titleImages[randomIndex];

        StartCoroutine(MoveImageObj());
    }

    IEnumerator MoveImageObj()
    {
        if (isMadeRightTrue1Flase2 == 1)
        {
            for (int i = 0; i < 500; i++)
            {
                titleImageObjImage.transform.localPosition -= new Vector3(0.1f, 0, 0); //100번이니까 최종적으로는 50에서 -50까지감
                yield return new WaitForSeconds(0.002f * timeForOneImage); //에디터에서 적은 시간만큼 감. (사실은 그보다 아주 약간 적게다.)
            }
        }
        else if (isMadeRightTrue1Flase2 == 2)
        {
            for (int i = 0; i < 500; i++)
            {
                titleImageObjImage.transform.localPosition += new Vector3(0.1f, 0, 0); //100번이니까 최종적으로는 50에서 -50까지감
                yield return new WaitForSeconds(0.002f * timeForOneImage); //에디터에서 적은 시간만큼 감.
            }
        }
        //전부 움직임이 끝난 뒤에 뒤에 조건문 풀기
        isMoveEnd = true;
    }

}

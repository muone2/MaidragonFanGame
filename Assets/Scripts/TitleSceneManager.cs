using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    public GameObject rullPanel;
    public GameObject uiGroup;
    public GameObject uioffButton;

    bool isUIoff = false;

    public void uiOnOff()
    {
        if(isUIoff == false)
        {
            isUIoff = true;
            uiGroup.SetActive(false);
            Color color = uioffButton.GetComponent<Image>().color;
            color.a = 0.2f;
            uioffButton.GetComponent<Image>().color = color;
        }
        else if (isUIoff == true)
        {
            isUIoff = false;
            uiGroup.SetActive(true);
            Color color = uioffButton.GetComponent<Image>().color;
            color.a = 1.0f;
            uioffButton.GetComponent<Image>().color = color;
        }
    }

    public void OpenRull()
    {
        rullPanel.SetActive(true); //���� ��Ģ ������ ���� ������ �ʱ�ȭ�ϴ� ���뵵 ����.
    }

    public void CloseRull()
    {
        rullPanel.SetActive(false);
    }

    public void MoveIngameScene()
    {
        SceneManager.LoadScene("InGameSlotMachine");
    }

    public void ClickGameEnd()
    {
        Application.Quit();
    }
}

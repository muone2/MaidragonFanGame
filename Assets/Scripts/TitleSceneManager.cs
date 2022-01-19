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

    public GameObject buttonSound;

    bool isUIoff = false;

    public void uiOnOff()
    {
        if(isUIoff == false)
        {
            ButtonSoundPlay();
            isUIoff = true;
            uiGroup.SetActive(false);
            Color color = uioffButton.GetComponent<Image>().color;
            color.a = 0.2f;
            uioffButton.GetComponent<Image>().color = color;
        }
        else if (isUIoff == true)
        {
            ButtonSoundPlay();
            isUIoff = false;
            uiGroup.SetActive(true);
            Color color = uioffButton.GetComponent<Image>().color;
            color.a = 1.0f;
            uioffButton.GetComponent<Image>().color = color;
        }
    }

    public void OpenRull()
    {
        ButtonSoundPlay();
        rullPanel.SetActive(true); //게임 규칙 보던거 스택 있으면 초기화하는 내용도 넣자.
    }

    public void CloseRull()
    {
        ButtonSoundPlay();
        rullPanel.SetActive(false);
    }

    public void MoveIngameScene()
    {
        ButtonSoundPlay();
        SceneManager.LoadScene("InGameSlotMachine");
    }

    public void ClickGameEnd()
    {
        ButtonSoundPlay();
        Application.Quit();
    }

    public void ButtonSoundPlay()
    {
        buttonSound.gameObject.SetActive(false);
        buttonSound.gameObject.SetActive(true);
    }

}

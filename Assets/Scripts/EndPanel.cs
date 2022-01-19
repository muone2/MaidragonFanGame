using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndPanel : MonoBehaviour
{
    public Sprite[] endsprite;
    public GameObject endingPopup;
    public float endBgmVolume;

    public void OnEnable()
    {
        int randomNum = Random.Range(0, endsprite.Length);
        gameObject.GetComponent<Image>().sprite = endsprite[randomNum];
        SoundManager.instance.bgm.volume = endBgmVolume; //���忣�忡�� ��
    }

    public void ClickReStart()
    {
        SoundManager.instance.ButtonSoundPlay(); //��ư ���� ���
        SceneManager.LoadScene("InGameSlotMachine");
    }

    public void ClickGoTitle()
    {
        SoundManager.instance.ButtonSoundPlay(); //��ư ���� ���
        SceneManager.LoadScene("Title");
    }
    public void ClickGameEnd()
    {
        SoundManager.instance.ButtonSoundPlay(); //��ư ���� ���
        Application.Quit();
    }
    public void ClickCloseButton()
    {
        SoundManager.instance.ButtonSoundPlay(); //��ư ���� ���
        endingPopup.SetActive(false);
    }

}

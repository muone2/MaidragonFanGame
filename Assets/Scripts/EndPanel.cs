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
        SoundManager.instance.bgm.volume = endBgmVolume; //베드엔드에선 끔
    }

    public void ClickReStart()
    {
        SoundManager.instance.ButtonSoundPlay(); //버튼 사운드 재생
        SceneManager.LoadScene("InGameSlotMachine");
    }

    public void ClickGoTitle()
    {
        SoundManager.instance.ButtonSoundPlay(); //버튼 사운드 재생
        SceneManager.LoadScene("Title");
    }
    public void ClickGameEnd()
    {
        SoundManager.instance.ButtonSoundPlay(); //버튼 사운드 재생
        Application.Quit();
    }
    public void ClickCloseButton()
    {
        SoundManager.instance.ButtonSoundPlay(); //버튼 사운드 재생
        endingPopup.SetActive(false);
    }

}

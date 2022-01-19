using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

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
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        // DontDestroyOnLoad(gameObject);
    }

    public GameObject soundOnOffButton;
    public Sprite soundOffImage;
    public Sprite soundOnImage;

    public AudioSource bgm;
    public AudioSource button;
    public AudioSource slot;
    public AudioSource scoreUp;
    public AudioSource jackPot;
    public AudioSource stopslot;
    public AudioSource dialogClick;

    bool SoundOn = true;

    public void ClickButtonSoundOnOff()
    {
        if (SoundOn == true)
        {
            SoundOn = false;
            bgm.volume = 0.0f;
            button.volume = 0.0f;
            slot.volume = 0.0f;
            scoreUp.volume = 0.0f;
            jackPot.volume = 0.0f;
            stopslot.volume = 0.0f;
            dialogClick.volume = 0.0f;
            soundOnOffButton.GetComponent<Image>().sprite = soundOffImage;
        }
        else if (SoundOn == false)
        {
            SoundOn = true;
            bgm.volume = 0.5f;
            button.volume = 0.5f;
            slot.volume = 1.0f;
            scoreUp.volume = 0.7f;
            jackPot.volume = 0.8f;
            stopslot.volume = 1.0f;
            dialogClick.volume = 0.7f;
            soundOnOffButton.GetComponent<Image>().sprite = soundOnImage;
            ButtonSoundPlay();
        }
    }

    public void ButtonSoundPlay()
    {
        button.gameObject.SetActive(false);
        button.gameObject.SetActive(true);
    }
    public void SlotSoundPlay()
    {
        slot.gameObject.SetActive(false);
        slot.gameObject.SetActive(true);
    }
    public void ScoreUpSoundPlay()
    {
        scoreUp.gameObject.SetActive(false);
        scoreUp.gameObject.SetActive(true);
    }
    public void JackPotSoundPlay()
    {
        jackPot.gameObject.SetActive(false);
        jackPot.gameObject.SetActive(true);
    }
    public void StopSlotSoundPlay()
    {
        stopslot.gameObject.SetActive(false);
        stopslot.gameObject.SetActive(true);
    }
    public void DialogClickSoundPlay()
    {
        dialogClick.gameObject.SetActive(false);
        dialogClick.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanel : MonoBehaviour
{
    public void ClickReStart()
    {
        SceneManager.LoadScene("InGameSlotMachine");
    }

    public void ClickGoTitle()
    {
        SceneManager.LoadScene("Title");
    }
    public void ClickGameEnd()
    {
        Application.Quit();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxBlockClick : MonoBehaviour
{
    public Text countText;
    public GameObject PanelForCantClickOther;

    public GameObject infoGroup;
    public Image infoImage;
    public Text itemInfoText;

    public int ItemNum;
    bool infoOn;

    public void SetItemBlockInfo(int Num, int itemCount, Sprite itemSprite, GameObject panelObj)
    {
        ItemNum = Num;
        countText.text = "X" + itemCount;
        PanelForCantClickOther = panelObj;

        infoImage.sprite = itemSprite;
        itemInfoText.text = ItemInfoTextManager.instance.ItmeInfoText[ItemNum];
    }

    public void ClickItemBlock()
    {
        PanelForCantClickOther.SetActive(true);
        infoGroup.SetActive(true);
        infoGroup.transform.parent = PanelForCantClickOther.transform;  //부모 위치 옮겨서 패널 앞으로.
        infoGroup.transform.localPosition = Vector3.zero; //조정
        infoGroup.transform.localScale = Vector3.one;

        infoOn = true;
    }

    private void Update()
    {
        if (infoOn == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                infoGroup.transform.parent = gameObject.transform; //원래 위치로
                infoGroup.SetActive(false);
                PanelForCantClickOther.SetActive(false);
                infoOn = false;  
            }
        }
    }


}

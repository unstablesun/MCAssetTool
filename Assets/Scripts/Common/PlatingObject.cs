using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatingObject : MonoBehaviour 
{
    public int ID;

    public GameObject ItemImage;


    public void OnVirtualButtonClick()
    {
        Debug.Log("OnVirtualButtonClick");

        SendMessageUpwards("MapPinClicked", ID);
    }


    public void SetItemImage(Sprite s)
    {

        ItemImage.GetComponent<Image>().sprite = s;
        ItemImage.GetComponent<Image>().SetNativeSize();

    }


}

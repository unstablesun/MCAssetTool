using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MasterChef.data;

public class mcSceneJsonController : MonoBehaviour 
{
    PantryItemData itemData = null;

	void Start () 
    {
        itemData = new PantryItemData();
        itemData.PantryItemList = new List<PantryItemRecord>();


        Image attachedImage = GetComponent<Image>();


        int depth = attachedImage.depth;
        float pWidth = attachedImage.preferredWidth;
        float pHeight = attachedImage.preferredHeight;

        Sprite sImage = attachedImage.sprite;
        string sName = sImage.name;


        /*
        Debug.Log("ObjInfo ->");
        Debug.Log("... depth = " + depth);
        Debug.Log("... pWidth = " + pWidth);
        Debug.Log("... pHeight = " + pHeight);
        Debug.Log("... sName = " + sName);
        */

	}


    public void onButtonClickParseChildren()
    {

        foreach (Transform childObj in transform)
        {
            Debug.Log("Foreach loop: " + childObj);


            GameObject go = childObj.gameObject;

            mcSceneJsonObj obj = go.GetComponent<mcSceneJsonObj>();

            if(obj != null)
            {


            }




        }


    }

	
	void Update () 
    {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MasterChef.data;

public class mcSceneJsonObj : MonoBehaviour 
{
    public bool IncludeInExport = true;

    public List<mcSearchTags> tagList = new List<mcSearchTags>();

    public bool IsPrize = false;
    public bool IsSubImage = false;

    public string ItemName = "NameID";
    public string ItemDesc = "DescID";
    public string ItemPrice = "1000";
    public string ItemCreationTime = "2018-07-05"; //ISO 8601

    public List<GameObject> extraImageList = null;

	void Start () 
    {
        /*
        Image attachedImage = GetComponent<Image>();


        int depth = attachedImage.depth;
        float pWidth = attachedImage.preferredWidth;
        float pHeight = attachedImage.preferredHeight;

        Sprite sImage = attachedImage.sprite;
        string sName = sImage.name;

        System.Guid myGUID = System.Guid.NewGuid();
        //Debug.Log("myGUID ->" + myGUID.ToString());

        */

        /*
        Debug.Log("ObjInfo ->");
        Debug.Log("... depth = " + depth);
        Debug.Log("... pWidth = " + pWidth);
        Debug.Log("... pHeight = " + pHeight);
        Debug.Log("... sName = " + sName);
        */



	
	}
	
	void Update () 
    {
		
	}


    public void GetObjStruct()
    {

    }

}

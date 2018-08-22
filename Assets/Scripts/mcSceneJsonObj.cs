using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MasterChef.data;
using System;


/*
 *------------------------------------------------------------------------------ 
 *      mcSceneJsonObj - tool only obj for creating pantry json
 * 
 *------------------------------------------------------------------------------ 
 */
public class mcSceneJsonObj : MonoBehaviour 
{
    public bool IncludeInExport = true;

    public List<mcSearchTags> tagList = new List<mcSearchTags>();

    public Int32 Id;

    public bool IsPrize = false;
    public bool IsSubImage = false;

    public string ItemName = "NameID";
    public string ItemDesc = "DescID";
    public string ItemPrice = "1000";
    public int ItemQuantity = 5;
    public string ItemCreationTime = "2018-07-05"; //ISO 8601

    public GameObject CenterOffset = null;
    public List<GameObject> extraImageList = null;

	void Start () 
    {
        /* DEBUG
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
	
}

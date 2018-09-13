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


    public Int32 Id;

    public bool SaveToggle = false;
 
    public string ItemName = "NameID";
    public string ItemDesc = "DescID";
    public string ItemPrice = "1000";
    public int ItemQuantity = 5;
    public PantryManager.PurchaseCurrency PurchaceCurrency = PantryManager.PurchaseCurrency.coins;
    public string ItemCreationTime = "2018-07-05"; //ISO 8601 - set from Pantry Menu

    public GameObject CenterOffset = null;


    //Which tags this object can be
    public List<mcSearchTags> tagList = new List<mcSearchTags>();


    public GameObject StackOffset1 = null;
    public GameObject StackOffset2 = null;
    public GameObject StackOffset3 = null;
    public GameObject StackOffset4 = null;

    public List<mcSearchTags> stackTagList1 = new List<mcSearchTags>();
    public List<mcSearchTags> stackTagList2 = new List<mcSearchTags>();
    public List<mcSearchTags> stackTagList3 = new List<mcSearchTags>();
    public List<mcSearchTags> stackTagList4 = new List<mcSearchTags>();


    void Start () 
    {

        //ItemCreationTime = System.DateTime.UtcNow.ToString("yyyy-MM-dd");

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

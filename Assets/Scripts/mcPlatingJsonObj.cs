﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MasterChef.data;

public class mcPlatingJsonObj : MonoBehaviour
{
    public bool IsPlatingObj = true;
    public bool IsRequired = true;
    public int PlacementOrder = 0;
    public int StackGroup = 0;
    public List<mcSearchTags> tagList = new List<mcSearchTags>();
    public GameObject OffetMarker;
}

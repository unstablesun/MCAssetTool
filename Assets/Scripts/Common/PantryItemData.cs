using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace MasterChef.data
{
    public class PantryItemTag
    {
        public string ItemTag;
    }

    public class PantryItemImage
    {
        public string filename;
        public float offsetX;
        public float offsetY;
    }


    public class PantryItemRecord
    {
        public string guid;
        public string ItemIsPrize;
        public string ItemNameLabel;
        public string ItemDescLabel;
        public string ItemPriceLabel;
        public string ItemCreationTime;

        //legacy
        public string filename;


        public List<PantryItemTag> TagList { get; set; }
        public List<PantryItemImage> ImageList { get; set; }

        public static implicit operator List<object>(PantryItemRecord v)
        {
            throw new NotImplementedException();
        }
    }

    public class PantryItemData
    {
        public string IngredientDataSet { get; set; }
        public string version { get; set; }

        public List<PantryItemRecord> PantryItemList { get; set; }

    }


}



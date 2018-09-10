using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;





/*
 * ------------------------------------------------------------------------

                            SearchTags


                                CSV


 * ------------------------------------------------------------------------
 */

public partial class SearchTags : MonoBehaviour
{
    static SearchTagRecord L1Record = null;
    static SearchTagRecord L2Record = null;
    static SearchTagRecord L3Record = null;
    static SearchTagRecord L4Record = null;
    static SearchTagRecord L5Record = null;

    private char fieldSeperator = ',';

    public void ReadCSV()
    {
        string path = "Assets/Resources/MasterTagSheet.csv";

        _searchTagData = new SearchTagData();

        _searchTagData.SearchTagDataSet = "csv file : MasterTagSheet";
        _searchTagData.version = "0.1.1";
        _searchTagData.SearchTagList = new List<SearchTagRecord>();

        StreamReader reader = new StreamReader(path);

        string line;
        do
        {
            line = reader.ReadLine();

            int fieldIndex = 0;

            if (line != null)
            {
                //note there is one tag + id per line, it's position determines it depth level
                string[] fields = line.Split(fieldSeperator);

                int index = 0;
                foreach (string field in fields)
                {
                    if(field == "")
                    {
                        //skip empty fields
                    }
                    else
                    {
                        fieldIndex = index;

                        //get ID from field + 1
                        string idStr = fields[fieldIndex + 1];
                        Int32 fId = Convert.ToInt32(idStr);

                        Debug.Log(fieldIndex.ToString() + " field = " + field + " fId = " + fId.ToString());

                        if (fieldIndex == 0)
                        {
                            L1Record = CreateRecord(field, fieldIndex, fId);
                            _searchTagData.SearchTagList.Add(L1Record);
                        }
                        else if (fieldIndex == 1)
                        {
                            L2Record = CreateRecord(field, fieldIndex, fId);
                            if (L1Record != null)
                            {
                                L1Record.SearchTagList.Add(L2Record);
                            }
                        }
                        else if (fieldIndex == 2)
                        {
                            L3Record = CreateRecord(field, fieldIndex, fId);
                            if (L2Record != null)
                            {
                                L2Record.SearchTagList.Add(L3Record);
                            }
                        }
                        else if (fieldIndex == 3)
                        {
                            L4Record = CreateRecord(field, fieldIndex, fId);
                            if (L3Record != null)
                            {
                                L3Record.SearchTagList.Add(L4Record);
                            }
                        }
                        else if (fieldIndex == 4)
                        {
                            L5Record = CreateRecord(field, fieldIndex, fId);
                            if (L4Record != null)
                            {
                                L4Record.SearchTagList.Add(L5Record);
                            }
                        }

                        break;//goto next line

                    }
                    index++;
                }
            }
        }
        while (line != null);

        reader.Close();
    }

}

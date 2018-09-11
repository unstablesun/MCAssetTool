using UnityEditor;
using UnityEngine;
using System.Collections;
using System;

[CustomEditor(typeof(SearchTags))]
public class SearchTagsEditor : Editor 
{
    public GUIStyle mystyle;

    string[] _TagStringList = new[] { 
        "Up", 
        "Down", 
        "Left", 
        "Right" 
    };

    int _choiceIndex = 0;


    void OnEnable()
    {
        SearchTags myTarget = (SearchTags)target;

        // Set the choice index to the previously selected index
        _choiceIndex = Array.IndexOf(_TagStringList, myTarget.TagItem);

    }

    public override void OnInspectorGUI()
    {
        mystyle = new GUIStyle();
        mystyle.normal.textColor = new Color(0.75f, 0.5f, 1f, 1.0f);
        mystyle.fontSize = 18;
        mystyle.fixedHeight = 20;
        mystyle.normal.background = MakeTex(600, 1, new Color(0.1f, 0.1f, 0.1f, 1.0f));

        SearchTags myTarget = (SearchTags)target;

        _choiceIndex = EditorGUILayout.Popup("TagItem", _choiceIndex, _TagStringList, mystyle);
        if (_choiceIndex < 0)
            _choiceIndex = 0;
        myTarget.TagItem = _TagStringList[_choiceIndex];
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];

        for (int i = 0; i < pix.Length; i++)
            pix[i] = col;

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }

}


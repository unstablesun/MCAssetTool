using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StringInList : PropertyAttribute {
  public delegate string[] GetStringList();

  public StringInList(params string [] list) {
    List = list;
  }

  public StringInList(Type type, string methodName) {
    var method = type.GetMethod (methodName);
    if (method != null) {
      List = method.Invoke (null, null) as string[];
    } else {
      Debug.LogError ("NO SUCH METHOD " + methodName + " FOR " + type);
    }
  }

  public string[] List {
    get;
    private set;
  }
}

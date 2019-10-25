using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


//Inherit all SaveObjs from this
[Serializable]
public class SaveObj 
{
    [SerializeField]
    string identifier;
    public SaveObj(string id)
    {
        identifier = id;
    }
    public virtual string GetJsonStr()
    {
        return JsonUtility.ToJson(this);
    }
    public virtual string GetIdentifier()
    {
        return identifier;
    }
    public virtual bool IsUnique()
    {
        return false;
    }
}

public class SettingsSaverLoader : MonoBehaviour
{
    static List<string> readJsonObjects = new List<string>();
    static string GetSaveFilePath()
    {
        return Application.persistentDataPath + "Save.phat";
    }

    public static void SaveObject(SaveObj obj)
    {
        if(obj.IsUnique())
        {
            SaveObjectUnique(obj);
        }
        else
        {
            string jsonstr = obj.GetJsonStr();
            Debug.Log("Saving object " + jsonstr);
            readJsonObjects.Add(jsonstr);
        }
    }

    private static void SaveObjectUnique(SaveObj obj)
    {
        for(int i = readJsonObjects.Count-1; i >= 0; i--)
        {
            SaveObj outObj = JsonUtility.FromJson<SaveObj>(readJsonObjects[i]);
            if(obj.GetIdentifier() == outObj.GetIdentifier())
            {
                readJsonObjects.RemoveAt(i);
            }

        }
        string jsonstr = obj.GetJsonStr();
        Debug.Log("Saving object " + jsonstr);
        readJsonObjects.Add(jsonstr);
    }

    // Will remove the object on loading, if successful
    public static T LoadObject<T>(string identifier) where T : SaveObj
    {
        foreach (string item in readJsonObjects)
        {
            T outItem = JsonUtility.FromJson<T>(item);
            if(outItem != null && identifier == outItem.GetIdentifier())
            {
                readJsonObjects.Remove(item);
                return outItem;
            }
        }
        Debug.LogWarning("Failed to load object with identifier: " + identifier);
        return default(T);
    }

    private void OnDestroy() {
        Debug.Log("Saving lines to " + GetSaveFilePath());
        System.IO.File.WriteAllLines(GetSaveFilePath(), readJsonObjects);
    }

    private void Awake() 
    {
        if(System.IO.File.Exists(GetSaveFilePath()))
        {
            Debug.Log("Found save file " + GetSaveFilePath());
            string[] inStr = System.IO.File.ReadAllLines(GetSaveFilePath());
            
            Debug.Log("Found " + inStr.Length + " lines of savefile");
            foreach(string jsonStr in inStr)
            {
                Debug.Log(jsonStr);
                readJsonObjects.Add(jsonStr);
            }
        }
        else
        {
            Debug.Log("Did not find any save file");
        }
    }
}
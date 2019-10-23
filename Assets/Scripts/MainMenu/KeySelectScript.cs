using System;
using TMPro;
using UnityEngine;

[Serializable]
class KeySaveObject : SaveObj
{
    [SerializeField]
    string keycode = "";
    public KeySaveObject(string id, KeyCode keycode_) : base(id)
    {
        keycode = keycode_.ToString();
    }

    public override string GetJsonStr()
    {
        return JsonUtility.ToJson(this);
    }

    public KeyCode GetKeyCode()
    {
        return (KeyCode) System.Enum.Parse(typeof(KeyCode), keycode) ;
    }
}

public class KeySelectScript : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI text = null;

    [SerializeField]
    private GameObject pressAnyKeyText = null;
    
    private KeyCode myKeycode;
    private static bool anyKeyBeingChanged = false;
    private STATE state;
    enum STATE
    {
        NOT_BEING_CHANGED,
        CHANGING
    }

    private void Start ()
    {
        myKeycode = KeyCode.W;
        KeySaveObject obj = SettingsSaverLoader.LoadObject<KeySaveObject>("someId");
        Debug.Log(obj.GetKeyCode());
        KeySaveObject saveObj = new KeySaveObject("someId", myKeycode);
        SettingsSaverLoader.SaveObject(saveObj);
        SettingsSaverLoader.PushToFile();
        pressAnyKeyText.SetActive(false);
        SetText("W"); //TODO, get info from non-volatile storage
    }

    void SetText(string newText)
    {
        if(text != null)
        {
            text.text = newText;
        }
        else
        {
            Debug.LogWarning(this.GetType().Name + "Did not have component TextMeshProUGUI");
        }
    }

    void OnGUI ()
    {
        if (state == STATE.CHANGING)
        {
            
            Event e = Event.current;
            if (e.isKey)
            {
                myKeycode = e.keyCode;
                SetText(myKeycode.ToString().ToUpper());
                state = STATE.NOT_BEING_CHANGED;
                pressAnyKeyText.SetActive(false);
                anyKeyBeingChanged = false;
            }
        }
    }

    public void OnKeyButtonPressed ()
    {
        if (!anyKeyBeingChanged && state != STATE.CHANGING)
        {
            anyKeyBeingChanged = true;
            state = STATE.CHANGING;
            pressAnyKeyText.SetActive(true);
        }
    }
}

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

    public override bool IsUnique()
    {
        return true;
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
    
    [SerializeField]
    private string saveId = "";

    [SerializeField]
    private KeyCode initialKeyCode = KeyCode.W;

    private KeyCode myKeycode;
    private static bool anyKeyBeingChanged = false;
    private STATE state = STATE.NOT_BEING_CHANGED;
    enum STATE
    {
        NOT_BEING_CHANGED,
        CHANGING
    }

    private void Start ()
    {
        KeySaveObject obj = SettingsSaverLoader.LoadObject<KeySaveObject>(saveId);
        if(obj != null)
        {
            Debug.Log("Loaded keycode " + obj.GetKeyCode());
            SetKey(obj.GetKeyCode());
        }
        else
        {
            SetKey(initialKeyCode);
        }
        pressAnyKeyText.SetActive(false);
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
                SetKey(e.keyCode);
                state = STATE.NOT_BEING_CHANGED;
                pressAnyKeyText.SetActive(false);
                anyKeyBeingChanged = false;
            }
        }
    }

    void SetKey(KeyCode k)
    {
        myKeycode = k;
        SetText(myKeycode.ToString().ToUpper());
        KeySaveObject save = new KeySaveObject(saveId, myKeycode);
        SettingsSaverLoader.SaveObject(save);
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class KeySelectScript : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI text;

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
                SetText(myKeycode.ToString());
                state = STATE.NOT_BEING_CHANGED;
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
        }
    }
}

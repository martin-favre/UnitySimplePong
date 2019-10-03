using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartTextComponent : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = Helpers.GetComponentMandatory<Text>(gameObject);
    }
    void Start()
    {
        GameResetController.RegisterResetFunction(Reset);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            text.enabled = false;
        }
    }

    void Reset()
    {
        text.enabled = true;
    }

}

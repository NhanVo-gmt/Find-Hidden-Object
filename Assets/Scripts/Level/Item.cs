using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour
{
    public int Index;

    public Action OnClicked;
    

    public void BindData(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Click()
    {
        OnClicked?.Invoke();
        
        gameObject.SetActive(false);
    }
}

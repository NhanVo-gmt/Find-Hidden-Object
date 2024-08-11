using System;
using System.Collections;
using System.Collections.Generic;
using GameFoundation.Scripts.Utilities.Extension;
using UnityEngine;
using UserData.Controller;
using Zenject;

public class Player : MonoBehaviour
{
    #region Inject

    [Inject] private LevelManager levelManager;

    #endregion
    
    
    private void Start()
    {
        this.GetCurrentContainer().Inject(this);
    }

    private void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(touch.position));
                
                if (rayHit.transform != null && rayHit.transform.TryGetComponent<Item>(out Item item))
                {
                    levelManager.SelectItem(item.Id, item.Index);
                    item.Click();
                }
            }
        }
    }
}

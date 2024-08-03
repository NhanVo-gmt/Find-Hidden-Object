using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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

                if (rayHit.transform != null)
                {
                    Debug.Log(rayHit.transform.name);
                }
            }
        }
    }
}

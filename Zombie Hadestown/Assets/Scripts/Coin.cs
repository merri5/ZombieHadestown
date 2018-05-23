using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : BehaveObject
{
    

    // Use this for initialization
    void Start()
    {
        restartPosition = transform.position;
        restartRotation = transform.rotation;
    }

    protected override void Update()
    {
        if (GameManager.instance.PlayerActive)
        {
            if (transform.position == restartPosition)
            {
                gameObject.GetComponent<Renderer>().enabled = true;
            }
            base.Update();
        }


    }

}

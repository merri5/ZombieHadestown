using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaveObject : MonoBehaviour
{

    [SerializeField] private float objectSpeed;
    [SerializeField] private float resetPosition = 29.4f;
    [SerializeField] private float startPosition = -115.24f;

    protected Vector3 restartPosition;
    protected Quaternion restartRotation;


    // Use this for initialization
    void Start()
    {
        restartPosition = transform.position;
        restartRotation = transform.rotation;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (GameManager.instance.Replayed)
        {
            BehaveObject[] list = FindObjectsOfType(typeof(BehaveObject)) as BehaveObject[];
            foreach (BehaveObject item in list)
            {
                item.restart();
            }


        }
        if (!GameManager.instance.GameOver)
        {
            transform.Translate(Vector3.right * (objectSpeed * Time.deltaTime));
            if (transform.localPosition.x >= resetPosition)
            {
                Vector3 newPos = new Vector3(startPosition, transform.position.y, transform.position.z);
                transform.position = newPos;
            }
        }


    }
    private void restart()
    {
        transform.SetPositionAndRotation(restartPosition, restartRotation);
    }
}

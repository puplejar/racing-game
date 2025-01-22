using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayController : MonoBehaviour
{
    public float resetPos = 40f;
    public float speed = 10f;
    
    GameManager gm;
    void Start()
    {
        gm = GameManager.Instance;
    }


    void Update()
    {
        if (gm.isLive)
        {
            transform.position += new Vector3(0, 0, -1) * speed * Time.deltaTime;
            if (transform.position.z < -resetPos)
                transform.position = new Vector3(transform.position.x, transform.position.y, resetPos);
        }
    }
}

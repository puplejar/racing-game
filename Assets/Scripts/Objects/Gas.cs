using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : BaseObjects
{

    void Start()
    {
        gameObject.layer = layer;
    }


    void Update()
    {
        transform.position += new Vector3(0, 0, -1) * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            CarController cc = other.gameObject.GetComponent<CarController>();
            cc.gasGauge += 30;
            Destroy(this.gameObject);
        }
    }

}

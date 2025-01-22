using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject car;
    public Rigidbody rb;
    private GameManager gm;

    public float speed= 1f;
    public int gasMaxGauge = 100;
    public int gasGauge = 100;

    public float maxTime = 1;
    private float currentTime = 0;

    public int clicked = 0;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        gm = GameManager.Instance;
    }
    
    void FixedUpdate()
    {
        if (gm.isLive)
        {
            Vector3 dir = new Vector3(clicked, 0, 0);
            rb.AddForce(dir * speed, ForceMode.Impulse);

            currentTime += Time.fixedDeltaTime;
            if (gasMaxGauge < gasGauge) gasGauge = gasMaxGauge;
            if (currentTime >= maxTime)
            {
                gasGauge -= 10;
                currentTime = 0;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockDoors : MonoBehaviour
{
    public OpenClose door1Script;
    public OpenClose door2Script;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            door1Script.reLock();
            door2Script.reLock();
            this.enabled = false;
        }
    }
}

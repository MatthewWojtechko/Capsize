using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointToward : MonoBehaviour
{
    public Transform obj;

    void awake()
    {
        transform.LookAt(obj);
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(obj);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

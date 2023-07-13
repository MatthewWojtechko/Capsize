using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectTrigger : MonoBehaviour
{
    public GameObject actionObject;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            actionObject.SetActive(true);
        }
    }
}

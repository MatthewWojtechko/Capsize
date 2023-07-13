using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpenMovement : MonoBehaviour
{
    public Transform towardTrans;
    public float speed;
    public float seconds = 10;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(towardTrans);
        StartCoroutine(die());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;   
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(seconds);
        GameObject.Destroy(this.gameObject);
    }
}

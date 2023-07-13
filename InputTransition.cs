using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputTransition : MonoBehaviour
{
    private bool waitOver;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && waitOver)
        {
            SceneManager.LoadScene("PlayScene");
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.4f);
        waitOver = true;
    }
}

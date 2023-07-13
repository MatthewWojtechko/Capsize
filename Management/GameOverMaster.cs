using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMaster : MonoBehaviour
{
    AudioSource[] audioSources;
    public AudioSource shipSFX;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void playMusic()
    {
        //audioSources[0].Stop();
        audioSources[1].Stop();
        audioSources[2].Play();
        shipSFX.Play();
    }

    public void takeoffSFX()
    {
        audioSources[3].Play();
    }
}

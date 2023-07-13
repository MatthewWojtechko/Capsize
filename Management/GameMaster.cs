using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public int pickupCount = 0;
    public GameObject pauseMenu;
    public bool GameIsPaused { get; set; }
    public GameObject finalEffect;
    public AudioSource firstSong;
    public AudioSource secondSong;

    private void Awake()
    {
        GameIsPaused = false;
        Messenger.AddListener(GameEvent.PICK_UP, IncrementPickupCount);
        Messenger.AddListener(GameEvent.END_LEVEL, endLevel);
        Messenger.AddListener(GameEvent.SIGN_ROTATE, changeMusic);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PICK_UP, IncrementPickupCount);
        Messenger.RemoveListener(GameEvent.END_LEVEL, endLevel);
        Messenger.RemoveListener(GameEvent.SIGN_ROTATE, changeMusic);
    }

    void Start()
    {
        Messenger.Broadcast(GameEvent.CAMERA_SHAKE_LONG);
        instance = this;
    }

    void Update()
    {
        // This is just for testing
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ShakeCamera();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
        if (GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                AudioListener.pause = false;
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                TogglePauseGame();
            }
        }
    }

    private void TogglePauseGame()
    {
        if (GameIsPaused)
        {
            AudioListener.pause = false;
            GameIsPaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        else
        {
            AudioListener.pause = true;
            GameIsPaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    private void endLevel()
    {
        finalEffect.SetActive(true);
        //Messenger.Broadcast(GameEvent.FREEZE_PLAYER);
        StartCoroutine(waitTrans());
    }

    private void IncrementPickupCount()
    {
        pickupCount++;
    }

    public void ShakeCamera()
    {
        Messenger.Broadcast(GameEvent.CAMERA_SHAKE);
    }

    private void changeMusic()
    {
        firstSong.Stop();
        secondSong.Play();
    }

    IEnumerator waitTrans()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("GameOver");
    }


}

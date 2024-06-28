using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField] GameObject endGamePanel;
    [SerializeField] GameObject questPanel;
    [SerializeField] PlayerHP playerHP;
    [SerializeField] Transform well;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] mainTheme;
    Vector3 wellPosition;
    public bool isPlayerInScene = true;
    public GameObject interactionButtonGO;
    int clipIndex = 0;


    void Start()
    {
        endGamePanel.SetActive(false);
        wellPosition = well.position;
        audioSource.clip = mainTheme[clipIndex];
        audioSource.Play();
    }


    void Update()
    {
        if (!audioSource.isPlaying)
        {
            clipIndex++;
            if(clipIndex >= mainTheme.Length)
            {
                clipIndex = 0;
            }
            audioSource.clip = mainTheme[clipIndex];
            audioSource.Play();
        }
        if (playerHP.IsAlive == false && !endGamePanel.activeSelf)
        {
            endGamePanel.SetActive(true);
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            isPlayerInScene = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenQuestPanel()
    {
        questPanel.SetActive(true);
    }

    public void PlayerIsBack(Transform playerPos)
    {
        playerPos.position = wellPosition;
    }

    private void OnCollisionStay(Collision collision)
    {

    }

    public void MuteAudio(bool isMuted)
    {
        audioSource.mute = isMuted;
    }

    private void SavePlayerProgress()
    {
        //PlayerPrefs.SetInt("points", points);
    }
}

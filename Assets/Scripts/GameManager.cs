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
    public Transform restartPoint;
    int clipIndex = 0;
    float saveTimer = 60f;
    float currentSaveTimer = 60f;


    void Start()
    {
        MenuHandler.IsGamePaused = false;
        Time.timeScale = 1.0f;
        endGamePanel.SetActive(false);
        wellPosition = well.position;
        audioSource.clip = mainTheme[clipIndex];
        audioSource.Play();
        currentSaveTimer = saveTimer;
        //PlayerPrefs.DeleteKey("playeHP");
        LoadPleyerProgress();
    }


    void Update()
    {
        if (!audioSource.isPlaying)
        {
            clipIndex++;
            if (clipIndex >= mainTheme.Length)
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

        currentSaveTimer -= Time.deltaTime;
        if (currentSaveTimer <= 0f)
        {
            SavePlayerProgress();
            currentSaveTimer = saveTimer;
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
    public void QuitQuestPanel()
    {
        questPanel.SetActive(false);
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

    public void SavePlayerProgress()
    {
        //PlayerPrefs.SetInt("points", points);
        PlayerPrefs.SetFloat("restartPointX", playerHP.transform.position.x);
        PlayerPrefs.SetFloat("restartPointY", playerHP.transform.position.y);

        PlayerPrefs.SetInt("playeHP", playerHP.currentHP);

        PlayerPrefs.SetString("restartScene", SceneManager.GetActiveScene().name);

        PlayerPrefs.Save();

        Debug.Log($"Game saved, position: {PlayerPrefs.GetFloat("restartPointX")}, {PlayerPrefs.GetFloat("restartPointY")}");
        Debug.Log($"Game saved, scene: {PlayerPrefs.GetString("restartScene")}");
    }

    public void LoadPleyerProgress()
    {
        Debug.Log($"Game load, position: {PlayerPrefs.GetFloat("restartPointX")}, {PlayerPrefs.GetFloat("restartPointY")}");
        GameObject player = GameObject.Find("Player");
        if (PlayerPrefs.HasKey("restartPointX"))
        {
            player.transform.position = new Vector3(PlayerPrefs.GetFloat("restartPointX"), PlayerPrefs.GetFloat("restartPointY"), 0f);
        }
        else
        {
            player.transform.position = player.GetComponent<PlayerHP>().RespawnPoint.transform.position;
        }

        Debug.Log($"scene: {PlayerPrefs.GetString("restartScene")}");
        if (PlayerPrefs.GetString("restartScene") != SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("restartScene"));
        }

        PlayerHP HpPlayer = GameObject.Find("Player").GetComponent<PlayerHP>();
        if (PlayerPrefs.HasKey("playerHP"))
        {
            HpPlayer.currentHP = PlayerPrefs.GetInt("playeHP");
        }
        else
        {
            HpPlayer.currentHP = 100;
        }
        HpPlayer.LoadHPBar();

    }
}

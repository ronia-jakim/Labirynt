using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text timeText;
    public Text crystalText;
    public Text goldText;
    public Text greenText;
    public Text redText;

    public Text pauseText;
    public Text infoText;

    public GameObject gamePanel;
    public Text reloadText;

    public static GameManager gameManager;
    [SerializeField] private int timeToEnd;

    public PostProcessProfile normalProfile;
    public PostProcessProfile lessTime;
    public PostProcessVolume volume;

    private bool gamePaused = false;

    private bool endGame = false;
    private bool win = false;

    public int greenKeys = 0;
    public int redKeys = 0;
    public int goldKeys = 0;

    public int points = 0;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null) 
            gameManager = this;

        InvokeRepeating("Stopper", 2, 1);

        audioSource = GetComponent<AudioSource>();

        timeText.text = timeToEnd.ToString();
        crystalText.text = points.ToString();
        goldText.text = goldKeys.ToString();
        greenText.text = greenKeys.ToString();
        redText.text = redKeys.ToString();

        pauseText.text = "";
        infoText.text = "";

        gamePanel.SetActive(false);
    }

    [SerializeField]
    AudioClip resumeClip;
    [SerializeField]
    AudioClip pauseClip;
    [SerializeField]
    AudioClip winClip;
    [SerializeField]
    AudioClip loseClip;

    public void PlayClip (AudioClip playClip) {
        audioSource.clip = playClip;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        PauseCheck();

        if (endGame) {
            if (Input.GetKeyDown(KeyCode.Y)) SceneManager.LoadScene(0);
            
            if (Input.GetKeyDown(KeyCode.N)) Application.Quit();
        }
    }

    public void WinGame() {
        win = true;
        endGame = true;
    }

    public void EndGame()
    {
        gamePanel.SetActive(true);

        CancelInvoke("Stopper");
        if (win){
            Debug.Log("Wygrałeś!");
            PlayClip(winClip);

            reloadText.text = "WON!";
        }
        else {
            Debug.Log("Przegrałeś :<");
            PlayClip(loseClip);
            
            reloadText.text = "LOST :<";
        }
    }

    void Stopper()
    {
        timeToEnd--;
        Debug.Log("Time to end: " + timeToEnd + " s");
        timeText.text = timeToEnd.ToString();

        if (timeToEnd <= 0)
        {
            timeToEnd = 0;
            endGame = true;
        }
        if (endGame) EndGame();

        if (timeToEnd < 10) LessTimeOn();
        else LessTimeOff();
    }

    public void GamePause()
    {
        Debug.Log("Gra jest zapauzowana");
        Time.timeScale = 0f;
        gamePaused = true;
        PlayClip(pauseClip);

        pauseText.text = "Game Paused";
        infoText.text = "Press P to resume";
    }

    public void GameResume()
    {
        Debug.Log("Gra została wznowiona");
        Time.timeScale = 1f;
        gamePaused = false;
        PlayClip(resumeClip);

        pauseText.text = "";
        infoText.text = "";
    }

    public void PauseCheck()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gamePaused) 
                GameResume();
            else 
                GamePause();
        }
    }

    public void AddPoints(int p)
    {
        points += p;
        Debug.Log(points);
        crystalText.text = points.ToString();
    }

    public void AddKey(KeyColor color)
    {
        if (color == KeyColor.Gold)
        {
            goldKeys++;
            goldText.text = goldKeys.ToString();
        }
        else if (color == KeyColor.Red)
        {
            redKeys++;
            redText.text = redKeys.ToString();
        }
        else if (color == KeyColor.Green)
        {
            greenKeys++;
            greenText.text = greenKeys.ToString();
        }
    }

    public void AddTime(int time)
    {
        timeToEnd += time;
        timeText.text = timeToEnd.ToString();
    }

    public void LessTimeOn () {
        volume.profile = lessTime;
    }
    public void LessTimeOff () {
        volume.profile = normalProfile;
    }

    public void FreezeTime(int f)
    {
        CancelInvoke("Stopper");
        InvokeRepeating("Stopper", f, 1);
    }
}

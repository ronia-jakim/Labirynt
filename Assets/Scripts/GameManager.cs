using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
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
    }

    public void EndGame()
    {
        CancelInvoke("Stopper");
        if (win){
            Debug.Log("Wygrałeś!");
            PlayClip(winClip);
        }
        else {
            Debug.Log("Przegrałeś :<");
            PlayClip(loseClip);
        }
    }

    void Stopper()
    {
        timeToEnd--;
        Debug.Log("Time to end: " + timeToEnd + " s");

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
    }

    public void GameResume()
    {
        Debug.Log("Gra została wznowiona");
        Time.timeScale = 1f;
        gamePaused = false;
        PlayClip(resumeClip);
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
    }

    public void AddKey(KeyColor color)
    {
        if (color == KeyColor.Gold)
        {
            goldKeys++;
        }
        else if (color == KeyColor.Red)
        {
            redKeys++;
        }
        else if (color == KeyColor.Green)
        {
            greenKeys++;
        }
    }

    public void AddTime(int time)
    {
        timeToEnd += time;
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

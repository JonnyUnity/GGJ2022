using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerController Player;
    [SerializeField] private GameObject _tutorialShield;


    private LevelManager levelManager;

    private int spawnIndex;

    private int sceneIndex = 1;

    private int currentPlayerHealth;

    [SerializeField] private int _tutorialLevel;

    private Controls _controls;

    private void Awake()
    {
        Debug.Log("GAME MANAGER AWAKE");

        SceneManager.sceneLoaded += LoadLevel;

        DontDestroyOnLoad(gameObject);

        _controls = new Controls();
        
        
    }


    void Start()
    {
        Debug.Log("GAME MANAGER START");
        SpawnPlayer();

        Player.OnPlayerDied.AddListener(OnPlayerDied);

    }

    private void LoadLevel(Scene scene, LoadSceneMode mode)
    {
        var gridObject = GameObject.Find("/Grid");
        levelManager = gridObject.GetComponent<LevelManager>();

        Debug.Log("New LEVEL!");

        if (Player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                Player = playerObj.GetComponent<PlayerController>();
            }
        }
               

        SpawnPlayer();

    }



    private void SpawnPlayer()
    {
        _controls.Enable();

        Player.InitValues(_tutorialLevel);
        //var spawn = levelManager.GetSpawn(spawnIndex);

        var checkpointPos = levelManager.GetCheckpointPosition(spawnIndex);

        //Player.transform.position = spawn.transform.position;
        Player.transform.position = checkpointPos;
    }


    void Update()
    {
        
    }


    public void OnPlayerDied()
    {

        _controls.Disable();
        Debug.Log("GAME OVER MAN!");

        

        if (_tutorialLevel == 0)
        {
            ResetTutorial();
        }

        //Player.InitValues(IsTutorial);
        SpawnPlayer();

    }

    public void UpdateSpawnIndex(int spawnIndex)
    {
        if (spawnIndex > this.spawnIndex)
        {
            this.spawnIndex = spawnIndex;
        }        
    }

    public void LoadNextLevel()
    {
        sceneIndex++;
        SceneManager.LoadScene(sceneIndex);
    }

   
   
    public void UpdateTutorial(int tutorialLevel)
    {
        _tutorialLevel = tutorialLevel;
        Player.CanThrow = (_tutorialLevel > 0);
        Player.CanRoll = (_tutorialLevel > 1);

    }

    private void ResetTutorial()
    {
        _tutorialShield.SetActive(true);
   
    }
    

}

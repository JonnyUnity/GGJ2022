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

    public bool IsTutorial = true;

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

        SpawnPlayer();

    }

    private void SpawnPlayer()
    {
        _controls.Enable();

        Player.InitValues(IsTutorial);
        var spawn = levelManager.GetSpawn(spawnIndex);
        Player.transform.position = spawn.transform.position;
    }


    void Update()
    {
        
    }


    public void OnPlayerDied()
    {

        _controls.Disable();
        Debug.Log("GAME OVER MAN!");

        

        if (IsTutorial)
        {
            ResetTutorial();
        }

        Player.InitValues(IsTutorial);
        SpawnPlayer();

    }

    public void UpdateSpawnIndex(int spawnIndex)
    {
        if (spawnIndex > this.spawnIndex)
        {
            this.spawnIndex = spawnIndex;
            IsTutorial = !IsTutorial;
        }        
    }

    public void LoadNextLevel()
    {
        sceneIndex++;
        SceneManager.LoadScene(sceneIndex);
    }

    public void TutorialPassed()
    {

        
        IsTutorial = true;

    }
    
    private void ResetTutorial()
    {

        _tutorialShield.SetActive(true);

    }
    

}

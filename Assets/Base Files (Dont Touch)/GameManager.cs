using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.WSA;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text impactText;
    private const int StartingLives = 3;
    public int remainingLives;
    public int numberOfGames;
    private List<string> remainingGames = new List<string>();

    private const float ShortTime = 3.3f;
    private const float LongTime = 6.7f;

    private bool firstGame;
    
    public float startWaitTime;
    public int indexOffset;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        remainingLives = StartingLives;
        for(int i = 0; i<numberOfGames;i++) remainingGames.Add(NameFromIndex(i+indexOffset));
        firstGame = true;
        StartCoroutine(LoadNextGame()); //TODO: replace with title screen stuff
    }

    private static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        print(name.Substring(0, dot));
        return name.Substring(0, dot);
    }
    
    struct GameInfo
    {
       public string name;
       public int id;
    }

    private IEnumerator LoadNextGame()
    {
        yield return new WaitForSeconds(.1f);
        var nextGame = GetNextGame();
        var sceneName = nextGame.name;
        AsyncOperation scene = SceneManager.LoadSceneAsync(nextGame.id);
        scene.allowSceneActivation = false;
        SetImpactText(sceneName);
        yield return new WaitForSeconds(firstGame ? 3f : 3f);//TODO: Replace with startWaitTime
        ImpactWord.instance.HandleImpactText();
        yield return new WaitForSeconds(.3f);
        if (firstGame) firstGame = false;
        scene.allowSceneActivation = true;
    }

    private void SetImpactText(string sceneName)
    {
        sceneName += "!";
        impactText.text = sceneName;
    }

    private GameInfo GetNextGame()
    {
        GameInfo game = new GameInfo();
        game.id = Random.Range(0, remainingGames.Count);
        game.name = remainingGames[game.id];
        game.id += indexOffset;
        //remainingGames.Remove(game);
        return game;
    }


    public void onMinigameStart(Minigame minigame)
    {
        TimerManager.Instance.StartTimer(minigame.gameTime);
        var waitTime = minigame.gameTime == Minigame.GameTime.Short ? ShortTime : LongTime;
        StartCoroutine(WaitForMinigameEnd(minigame, waitTime));
    }

    private IEnumerator WaitForMinigameEnd(Minigame minigame, float time)
    {
        yield return new WaitForSeconds(.1f);
        AsyncOperation scene = SceneManager.LoadSceneAsync("Main");
        scene.allowSceneActivation = false;
        yield return new WaitForSeconds(time);
        if (!minigame.gameWin) remainingLives -= 1;
        scene.allowSceneActivation = true;
        StartCoroutine(LoadNextGame());
    }




}

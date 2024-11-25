using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        //// Press the space key to start coroutine
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    // Use a coroutine to load the Scene in the background
        //    StartCoroutine(LoadCutscene());
        //}
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene("Controls"));
    }

    public void MainLevel()
    {
        StartCoroutine(LoadScene("MainLevel"));
    }

    public void MainMenu()
    {
        StartCoroutine(LoadScene("MainMenu"));
    }

    public void About()
    {
        StartCoroutine(LoadScene("About"));
    }

    public void Cutscene()
    {
        StartCoroutine(LoadScene("Cutscene"));
    }

    public IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}

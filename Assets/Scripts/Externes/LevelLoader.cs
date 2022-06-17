using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator endButton;
    public AudioSource sceneAudio;

    public float transitionTime = 1f;
    private AsyncOperation asyncLoad;
    private float currentVolume = 1;
    private bool decreaseVolume = false;

    private void Update()
    {
        if(decreaseVolume == true)
        {
            currentVolume = Mathf.Lerp(currentVolume, 0f, 1f * Time.deltaTime);
            sceneAudio.volume = currentVolume;
        }
    }
    public void Load()
    {
        endButton.SetBool("appear", false);
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    public void showLoadedScene()
    {
        asyncLoad.allowSceneActivation = true;
    }

    public void LoadAsyncScene(string sceneName)
    {
        StartCoroutine(AsyncScene(sceneName));
    }

    IEnumerator AsyncScene(string sN)
    {

        asyncLoad = SceneManager.LoadSceneAsync(sN);
        asyncLoad.allowSceneActivation = false;

        // yield to other processes until the scene is loaded
        while (!asyncLoad.isDone)
        {
            decreaseVolume = true;
            yield return null;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void RestartFromIntro()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RestarFromMenu()
    {
        SceneManager.LoadScene("Menu");
    }


}

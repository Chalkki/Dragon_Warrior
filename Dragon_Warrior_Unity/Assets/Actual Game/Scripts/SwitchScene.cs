using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public static bool leftToRight;
    public string nextSceneName;
    public bool goingRight;
    private bool isPlayerClose = false;
    GameObject doNotDesotry;
    // Start is called before the first frame update
    void Start()
    {
        loadingScreen = GameObject.Find("DoNotDestory").transform.Find("Canvas").transform.Find("LoadingScreen").gameObject;
        slider = loadingScreen.transform.Find("Slider").GetComponent<Slider>();
        isPlayerClose = false;
        doNotDesotry = GameObject.Find("DoNotDestory");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerClose)
        {
            transform.Find("interaction_text").gameObject.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E))
            {
                leftToRight = goingRight;
                if(!nextSceneName.Equals("Village") && !nextSceneName.Equals("Cave"))
                {
                    DontDestroyOnLoad(doNotDesotry);
                }
                else
                {
                    Destroy(doNotDesotry);
                }

                LoadLevel(nextSceneName);
            }
        }
        else
        {
            transform.Find("interaction_text").gameObject.SetActive(false);
        }
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.activeSceneChanged += SceneSwitched;
        StartCoroutine(LoadLevelAsynchronously(sceneName));
    }

    IEnumerator LoadLevelAsynchronously(string sceneName)
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/.9f);
            slider.value = progress;
            yield return null;
        }
    }
    private void SceneSwitched(Scene arg0, Scene arg1)
    {
        // avoid duplicate Scene Swithced being called
        SceneManager.activeSceneChanged -= SceneSwitched;
        loadingScreen = GameObject.Find("DoNotDestory").transform.Find("Canvas").transform.Find("LoadingScreen").gameObject;
        slider = loadingScreen.transform.Find("Slider").GetComponent<Slider>();
        doNotDesotry = GameObject.Find("DoNotDestory");
        // this function will be called in the new scene
        if (leftToRight)
        {
            // if going from left to right, spawn the player in the left spawn in the next scene
            doNotDesotry.transform.Find("PlayerSword").gameObject.transform.position = GameObject.Find("LeftSpawnPoint").transform.position;
        }
        else
        {
            // if going from right to left, spawn the player in the right spawn in the next scene
            doNotDesotry.transform.Find("PlayerSword").gameObject.transform.position = GameObject.Find("RightSpawnPoint").transform.position;
        }
        loadingScreen.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerClose = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchScene : MonoBehaviour
{
    public static bool leftToRight;
    public string nextSceneName;
    public bool goingRight;
    private bool isPlayerClose = false;
    GameObject doNotDesotry;
    // Start is called before the first frame update
    void Start()
    {
        isPlayerClose = false;
        doNotDesotry = GameObject.Find("DoNotDestory");
        SceneManager.activeSceneChanged += SceneSwitched;
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
                if(!nextSceneName.Equals("Village"))
                {
                    DontDestroyOnLoad(doNotDesotry);
                }
                else
                {
                    Destroy(doNotDesotry);
                }
                SceneManager.LoadScene(nextSceneName);
            }
        }
        else
        {
            transform.Find("interaction_text").gameObject.SetActive(false);
        }
    }


    private void SceneSwitched(Scene arg0, Scene arg1)
    {
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

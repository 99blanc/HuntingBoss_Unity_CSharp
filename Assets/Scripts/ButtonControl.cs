using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    private bool Check = false;
    Animator Anime;

    void Start()
    {
        Anime = GameObject.Find("Player").GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if (!Check && SceneManager.GetSceneByName("Win").isLoaded)
        {
            Anime.SetBool("Win", true);
            Check = true;
        }
        if (!Check && SceneManager.GetSceneByName("Lose").isLoaded)
        {
            Anime.SetBool("Lose", true);
            Check = true;
        }
        if (!SceneManager.GetSceneByName("Win").isLoaded && !SceneManager.GetSceneByName("Lose").isLoaded)
        {
            if (SceneManager.GetActiveScene().name == "Title")
            {
                Anime.SetBool("Win", false);
                Anime.SetBool("Lose", false);
                Check = false;
            }
        }
    }

    public void OnClickLevel()
    {
        SceneManager.LoadScene("Level");
        Debug.Log("'Level' 화면으로 이동");
    }

    public void OnClickStart1()
    {
        SceneManager.LoadScene("Field1");
        Debug.Log("'Field1' 화면으로 이동");
    }

    public void OnClickStart2()
    {
        SceneManager.LoadScene("Field2");
        Debug.Log("'Field2' 화면으로 이동");
    }

    public void OnClickStart3()
    {
        SceneManager.LoadScene("Field3");
        Debug.Log("'Field3' 화면으로 이동");
    }

    public void OnClickNotice()
    {
        SceneManager.LoadScene("Info");
        Debug.Log("'Info' 화면으로 이동");
    }

    public void OnClickFrom()
    {
        SceneManager.LoadScene("From");
        Debug.Log("'From' 화면으로 이동");
    }

    public void OnClickExit()
    {
        Application.Quit();
        Debug.Log("'Quit' 화면으로 이동");
    }

    public void OnClickShop()
    {
        SceneManager.LoadScene("Shop");
        Debug.Log("'Shop' 화면으로 이동");
    }

    public void OnClickTitle()
    {
        SceneManager.LoadScene("Title");
        Debug.Log("'Title' 화면으로 이동");
    }
}

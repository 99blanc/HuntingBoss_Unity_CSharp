using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public string NextScene;
    public bool Win;
    public bool Lose;
    Animator Anime;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            Anime.SetBool("Win", Win);
            Anime.SetBool("Lose", Lose);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
            SceneManager.LoadScene(NextScene);
    }
}
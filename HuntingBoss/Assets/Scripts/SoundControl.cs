using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundControl : MonoBehaviour
{
    public static SoundControl instance;
    public AudioClip AudioWalkFootStep;
    public AudioClip AudioRunFootStep;
    public AudioClip AudioSprintFootStep;
    public AudioClip AudioDodge;
    public AudioClip AudioAttack;
    public AudioClip AudioSmash;
    public AudioClip AudioStrike;
    private AudioSource AudioSource;
    Animator Anime;
    Animator BAnime;
    Animator GAnime;

    void Start()
    {
        Anime = GameObject.Find("Player").GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name == "Field1")
            BAnime = GameObject.Find("Boss").GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name == "Field2")
            GAnime = GameObject.Find("Gurdian1").GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!AudioSource.isPlaying)
        {
            if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Dodge"))
            {
                AudioSource.pitch = 1f;
                AudioSource.PlayOneShot(AudioDodge);
                return;
            }
            if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Smash"))
            {
                AudioSource.pitch = 2f;
                AudioSource.PlayOneShot(AudioSmash);
                return;
            }
            if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                AudioSource.pitch = 2f;
                AudioSource.PlayOneShot(AudioAttack);
                return;
            }
            if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Strike"))
            {
                AudioSource.pitch = 1f;
                AudioSource.PlayOneShot(AudioStrike);
                return;
            }
            if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Sprint"))
            {
                AudioSource.pitch = 1f;
                AudioSource.clip = AudioSprintFootStep;
                AudioSource.Play();
                return;
            }
            if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                AudioSource.pitch = 1f;
                AudioSource.clip = AudioRunFootStep;
                AudioSource.Play();
                return;
            }
            if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                AudioSource.pitch = 1f;
                AudioSource.clip = AudioWalkFootStep;
                AudioSource.Play();
                return;
            }
            else
                AudioSource.Stop();
        }
    }
}

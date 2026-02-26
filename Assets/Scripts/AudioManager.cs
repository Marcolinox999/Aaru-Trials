using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //Singleton Patterns
    //1. Only one instance of AudioManager
    //2. Accessible from any kind of entity into your project.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static AudioManager instance; //throne.
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            //Its not going to be destroyeda
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySFX(AudioClip clipToPlay)
    {
        sfxSource.PlayOneShot(clipToPlay);
    }

    public void PlayMusic(AudioClip musicToPlay)
    {
        musicSource.Stop();
        musicSource.clip = musicToPlay;
        musicSource.Play();
    }
}
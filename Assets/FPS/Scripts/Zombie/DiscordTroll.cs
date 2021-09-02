using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscordTroll : MonoBehaviour
{
    public AudioClip clip;
    public float lowerInterval;
    public float upperInterval;
    AudioSource m_MyAudioSource;
    bool play;
    float playInterval;

    // Start is called before the first frame update
    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        playInterval = Random.Range(lowerInterval, upperInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (playInterval >= 0) 
        {
            playInterval -= Time.deltaTime;
        }
        else
        {
            playInterval = Random.Range(lowerInterval, upperInterval);
            m_MyAudioSource.PlayOneShot(clip);
        }
    }
}

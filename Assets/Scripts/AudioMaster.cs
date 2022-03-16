using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    AudioSource source;
    bool isPlaying;
    float playTime;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        isPlaying = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M) == true)
        {
            if(source.enabled == true)
            {
                playTime = source.time;
            }
            source.enabled = !(source.enabled);
            
            if(source.enabled == true)
            {
                source.time = playTime;
            }
        }
    }
}

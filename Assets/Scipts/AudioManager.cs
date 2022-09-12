using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 播放音乐音效
/// </summary>
public class AudioManager : MonoBehaviour
{
    public AudioSource audioS;
    public static AudioManager instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audioS = GetComponent<AudioSource>();
    }
    //播放指定的音效
    public void AudioPlay(AudioClip clip) {
        audioS.PlayOneShot(clip);
    }
}

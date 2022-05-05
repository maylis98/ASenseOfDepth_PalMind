using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MultiSound : MonoBehaviour
{
    public AudioClip[] audioClips;
    public int iCurrentClip = -1;
    public float volumeLevel = 1;

    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (iCurrentClip >= 0 && !source.isPlaying)
        {
            iCurrentClip = Mathf.Min(audioClips.Length - 1, iCurrentClip);
            source.PlayOneShot(audioClips[iCurrentClip]);
            source.volume = volumeLevel;

        }
    }

}

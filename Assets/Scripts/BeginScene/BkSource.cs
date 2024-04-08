using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BkSource : MonoBehaviour
{
    private static BkSource instance;
    public static BkSource Instance => instance;

    private AudioSource audioSource;
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();

        MusicData data = DataManager.Instance.musicData;
        SetIsOpen(data.musicOpen);
        ChangeVal(data.musicVal);

    }

    public void SetIsOpen(bool isOpen)
    {
        audioSource.mute = !isOpen;
    }

    public void ChangeVal(float val)
    {
        audioSource.volume = val;
    }
}

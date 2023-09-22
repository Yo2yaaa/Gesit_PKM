using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShootingSound()
    {
        PlaySound(audioClipRefsSO.shooting);
    }

    private void PlaySound(AudioClip audioClip)
    {
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, 1);
        // audioSource.PlayOneShot(audioClip);
    }


}

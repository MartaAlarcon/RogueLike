using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool isKeyUnlocked = false;
    public static Key instance;
    [SerializeField] private AudioClip keySound;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        instance = this;
    }

    public void UnlockKey()
    {
        audioSource.PlayOneShot(keySound);
        isKeyUnlocked = true;
    }

}

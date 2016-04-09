using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    private AudioClip[] bubbleSounds;

    [SerializeField]
    private AudioClip[] puffSounds;

    [SerializeField]
    private AudioClip[] fullReleaseSounds;

    [SerializeField]
    private AudioClip[] glassHitSounds;

    [SerializeField]
    private AudioClip[] catSounds;

    [SerializeField]
    private AudioClip[] inhaleSounds;

    [SerializeField]
    private AudioClip[] leakSounds;

    [SerializeField]
    private AudioClip[] deflateSounds;

    [SerializeField]
    private AudioClip[] chargeSounds;
    private int generateRandomInt(int max)
    {
       return Random.Range(0, max * 50000) % max;
    }

    public AudioClip getRandomBubbleSound()
    {
        return bubbleSounds[generateRandomInt(bubbleSounds.Length)];
    }
    public AudioClip getRandomPuffSound()
    {
        return puffSounds[generateRandomInt(puffSounds.Length)];
    }
    public AudioClip getRandomFullRleaseSound()
    {
        return fullReleaseSounds[generateRandomInt(fullReleaseSounds.Length)];
    }
    public AudioClip getRandomGlassHitSound()
    {
        return glassHitSounds[generateRandomInt(glassHitSounds.Length)];
    }
    public AudioClip getRandomCatSound()
    {
        return catSounds[generateRandomInt(catSounds.Length)];
    }
    public AudioClip getRandomInhaleSound()
    {
        return inhaleSounds[generateRandomInt(inhaleSounds.Length)];
    }
    public AudioClip getRandomLeakSound()
    {
        return leakSounds[generateRandomInt(leakSounds.Length)];
    }
    public AudioClip getRandomDeflateSound()
    {
        return deflateSounds[generateRandomInt(deflateSounds.Length)];
    }

    public AudioClip getRandomChargeSound()
    {
        return chargeSounds[generateRandomInt(chargeSounds.Length)];
    }
}

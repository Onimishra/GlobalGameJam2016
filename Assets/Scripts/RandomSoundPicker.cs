using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomSoundPicker : MonoBehaviour {
	public AudioClip[] clips;
    public AudioSource MyAudioSource;

    [Header("Limit Triggered Sounds")]
    public int MaxSimultaneous = 2;
    [SerializeField]
    private int _currentlyPlaying = 0;

    [Header("Play looped?")]
    public bool SelfLooping;
	public bool Randomize;
    public float MinWait = 1f;
    public float MaxWait = 2f;

    void Start() {
        MyAudioSource = GetComponent<AudioSource>();

        if (SelfLooping) {
            StartCoroutine(PlaySelfLooping());
        }
    }

    public float PlayRandomSound() {
        if (_currentlyPlaying < MaxSimultaneous) {
            AudioClip clip = clips[Random.Range(0, clips.Length)];
            MyAudioSource.PlayOneShot(clip);
            _currentlyPlaying++;
            StartCoroutine(WaitForSound(clip));
            return clip.length;
        } else {
            return -1f;
        }
    }

    private IEnumerator WaitForSound(AudioClip clip) {
        float length = clip.length;
        yield return new WaitForSeconds(length);
        _currentlyPlaying--;
    }

    IEnumerator PlaySelfLooping() {
        int counter = 0;
        while (true) {
            yield return new WaitForSeconds(Random.RandomRange(MinWait, MaxWait));
            AudioClip ac = clips[counter % clips.Length];
			if (Randomize) {
				ac = clips[Random.Range(0, clips.Length)];
			}
            //print("playing loop index " + (counter % clips.Length));
            
            MyAudioSource.PlayOneShot(ac);
            counter++;
            yield return new WaitForSeconds(ac.length);
        }
    }
}

using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField]
    private AudioSource _sfxSource;
    [SerializeField]
    private AudioSource _bgmSource;

    private bool _fadeDone;

    //Creates singleton
    private void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != this) Destroy(this);
    }

    //Plays a given clip at a given volume in the according audio source (sound effects)
    //*Does NOT replace current clip being played
    public void PlaySFX(AudioClip clip, float volume)
    {
        _sfxSource.PlayOneShot(clip, volume);
    }

    //Plays a given clip at a given volume in the according audio source (background effects)
    //*REPLACES old BGM with new clip
    public IEnumerator PlayBGM(AudioClip clip, float volume, bool fade, float fadeOutTime, float fadeInTime)
    {
        //fades old audio out, and new audio in
        if (fade)
        {
            _fadeDone = false;
            StartCoroutine(FadeAudio(_bgmSource, 0, fadeOutTime));
            while (!_fadeDone) yield return null;
            _bgmSource.Stop();
            _bgmSource.clip = clip;
            _bgmSource.Play();
            StartCoroutine(FadeAudio(_bgmSource, 1, fadeInTime));
        }
        //stops old sound and plays new sound (no transition)
        else
        {
            _bgmSource.Stop();
            _bgmSource.clip = clip;
            _bgmSource.Play();
        }        
    }

    //Public so it can be called anywhere to do things such as fade out music before switching scenes
    public IEnumerator FadeAudio(AudioSource audSrc, float targetVol, float duration)
    {
        float startVol = audSrc.volume;
        float time = 0;
        targetVol = Mathf.Clamp(targetVol, 0, 1);
        if (duration < 0) duration = 0;
        while (time < duration)
        {
            audSrc.volume = Mathf.Lerp(startVol, targetVol, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        audSrc.volume = targetVol;
        _fadeDone = true;
        yield break;
    }
    
    //Here bcause properties can't be serialized in editor
    public AudioSource Get_BGMSource()
    {
        return _bgmSource;
    }

    public AudioSource Get_SFXSource()
    {
        return _sfxSource;
    }
}

using System.Collections;
using UnityEngine;

namespace ACoolTeam
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        public float MaxVol = 1f;

        [SerializeField]
        private AudioSource _sfxSource;
        [SerializeField]
        private AudioSource _bgmSource;
        [SerializeField]
        private AudioSource _playerFootstepsSource;

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

        private void Start()
        {
            _bgmSource.playOnAwake = false;
        }

        public void StartFootsteps()
        {
            _playerFootstepsSource.Play();
        }

        public void EndFoodsteps()
        {
            _playerFootstepsSource.Stop();
        }

        //Plays a given clip at a given volume in the according audio source (sound effects)
        //*Does NOT replace current clip being played
        public void PlaySFX(AudioClip clip, float volume)
        {
            _sfxSource.PlayOneShot(clip, Mathf.Min(MaxVol, volume));
        }

        public void StartBGM()
        {
            _bgmSource.volume = MaxVol;
            _bgmSource.Play();
        }

        public void StopBGM()
        {
            _bgmSource.Stop();
        }

        //Plays a given clip at a given volume in the according audio source (background effects)
        //*REPLACES old BGM with new clip
        public IEnumerator PlayBGM(AudioClip clip, float volume, bool fade, float fadeOutTime, float fadeInTime)
        {
            volume = Mathf.Min(MaxVol, volume);
            //if no clip, just fade out volume and stop then reset volume to 1 (not playing anything)
            if (clip == null)
            {
                _fadeDone = false;
                StartCoroutine(FadeAudio(_bgmSource, 0, fadeOutTime));
                while (!_fadeDone) yield return null;
                _bgmSource.Stop();
                _bgmSource.volume = volume;
                yield break;
            }
            //fades old audio out, and new audio in if given a clip
            else if (fade)
            {
                _fadeDone = false;
                StartCoroutine(FadeAudio(_bgmSource, 0, fadeOutTime));
                while (!_fadeDone) yield return null;
                _bgmSource.Stop();
                _bgmSource.clip = clip;
                _bgmSource.Play();
                StartCoroutine(FadeAudio(_bgmSource, volume, fadeInTime));
            }
            //stops old sound and plays new sound (no transition)
            else
            {
                _bgmSource.Stop();
                _bgmSource.volume = volume;
                _bgmSource.clip = clip;
                _bgmSource.Play();
            }
        }

        //Public so it can be called anywhere to do things such as fade out music before switching scenes
        public IEnumerator FadeAudio(AudioSource audSrc, float targetVol, float duration)
        {
            targetVol = Mathf.Min(MaxVol, targetVol);
            _fadeDone = false;
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
}

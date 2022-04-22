using UnityEngine;

public class SM_PlaySound : MonoBehaviour
{
    [Header("Clip Settings")]
    //Settings for the clip to be played
    [SerializeField]
    private AudioClip _clip;
    [SerializeField, Range(0f, 1f)]
    private float _volume;
    public enum SoundType { SFX, BGM }
    [SerializeField]
    private SoundType _soundType;

    [Space]

    //Additional settings for if the clip is for BGM
    [SerializeField]
    private bool _fadeBGM;
    [SerializeField]
    private float _fadeOutTime;
    [SerializeField]
    private float _fadeInTime;
    
    [Header("Script Settings")]
    //If looking to play the clip on collision/trigger, select the according one here
    [SerializeField]
    private CheckFor _checkFor;
    private enum CheckFor { COLLISON, TRIGGER, NONE }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_checkFor == CheckFor.TRIGGER) PlayClip(_soundType);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_checkFor == CheckFor.COLLISON) PlayClip(_soundType);
    }

    //Tells SoundManager to play the clip in the audio source described by _soundType
    //If neither a collision/trigger should be used, you can attach this component to a gameobject and call this public method
    public void PlayClip(SoundType soundType)
    {
        if (soundType == SoundType.SFX)
        {
            SoundManager.Instance.PlaySFX(_clip, _volume);
        }
        else
        {
            StartCoroutine(SoundManager.Instance.PlayBGM(_clip, _volume, _fadeBGM, _fadeOutTime, _fadeInTime));
        }
    }
}

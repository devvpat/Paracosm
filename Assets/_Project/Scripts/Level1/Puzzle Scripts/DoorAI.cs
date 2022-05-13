using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ACoolTeam
{
    [RequireComponent(typeof(SM_PlaySound))]
    public class DoorAI : MonoBehaviour
    {
        [SerializeField] private DoorAI _link;
        [SerializeField] private GameObject _player;
        [SerializeField] private bool _canEnter;
        [SerializeField] private Image _blackBackground;
        [SerializeField] private float _fadeTime;
        [SerializeField] private float _doorSpawnOffset;    //players teleports this much down (y pos) relative to linked door's pos

        private PlayerInput _playerInput;
        private bool _playerInBounds;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Player.Interact.started += Interact;
        }


        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }
        private void Interact(InputAction.CallbackContext obj)
        {
            if (_playerInBounds && _canEnter) 
            {
                StartCoroutine(FadeScreen());
            }
        }

        private IEnumerator FadeScreen()
        {
            GetComponent<SM_PlaySound>().PlayClip(SM_PlaySound.SoundType.SFX);  //play door sound
            if (_blackBackground.color.a == 0)  //play animation if animation not already playing
            {
                float timeCount = 0;
                while (_blackBackground.color.a < 1)
                {
                    _blackBackground.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, timeCount / (_fadeTime / 2)));
                    timeCount += Time.deltaTime;
                    yield return null;
                }
                _blackBackground.color = new Color(0, 0, 0, 1);
                timeCount = 0;
                _player.transform.position = _link.transform.position - new Vector3(0, _doorSpawnOffset, 0);
                while (_blackBackground.color.a > 0)
                {
                    _blackBackground.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, timeCount / (_fadeTime / 2)));
                    timeCount += Time.deltaTime;
                    yield return null;
                }
                _blackBackground.color = new Color(0, 0, 0, 0);
            }
            else _player.transform.position = _link.transform.position - new Vector3(0, _doorSpawnOffset, 0);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _playerInBounds = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _playerInBounds = false;
            }
        }
    }
}

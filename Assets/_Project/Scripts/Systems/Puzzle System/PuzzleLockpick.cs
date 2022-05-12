using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class PuzzleLockpick : MonoBehaviour, IPuzzle
    {
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = new PlayerInput();

        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        public void OnPuzzleStart()
        {
            
        }

        public void OnPuzzleEnd()
        {
            
        }

    }
}

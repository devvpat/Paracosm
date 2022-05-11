using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class PuzzleManager : MonoBehaviour
    {
        public delegate void PuzzleAction();
        public static event PuzzleAction OnPuzzleInteract;
        public static PuzzleManager Instance { get; private set; }

        private IPuzzle _currentPuzzle;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else if (Instance != this) Destroy(this);
        }

        public void StartPuzzle(IPuzzle puzzle)
        {
            _currentPuzzle = puzzle;
            Debug.Log("Puzzle started");
        }
    }
}

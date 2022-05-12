using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace ACoolTeam
{
    public class PuzzleManager : MonoBehaviour
    {
        public delegate void PuzzleAction();
        public static event PuzzleAction OnPuzzleInteract;
        public static PuzzleManager Instance { get; private set; }

        [SerializeField] private GameObject _taskView;

        private IPuzzle _currentPuzzle;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else if (Instance != this) Destroy(this);

            _taskView.SetActive(false);
        }

        public void StartPuzzle(IPuzzle puzzle)
        {
            _currentPuzzle = puzzle;

            _taskView.SetActive(true);
            _currentPuzzle.OnPuzzleStart();

            Debug.Log("Puzzle started");
        }
    }
}

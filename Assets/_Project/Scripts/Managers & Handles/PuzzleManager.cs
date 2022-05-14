using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace ACoolTeam
{
    public class PuzzleManager : MonoBehaviour
    {
        public delegate void PuzzleStartAction();
        public static event PuzzleStartAction OnPuzzleStart;
        public delegate void PuzzleEndAction();
        public static event PuzzleEndAction OnPuzzleEnd;
        public static PuzzleManager Instance { get; private set; }
        public static bool PuzzlePlaying;

        [SerializeField] private GameObject _taskviewHolder;
        [SerializeField] private GameObject _puzzleHolder;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else if (Instance != this) Destroy(this);

            ClearPuzzle();
            _taskviewHolder.SetActive(false);
        }

        public void StartPuzzle(GameObject puzzle)
        {
            if(puzzle.TryGetComponent(out IPuzzle currentPuzzle))
            {
                ClearPuzzle();

                Instantiate(puzzle, _puzzleHolder.transform);
                _taskviewHolder.SetActive(true);
                OnPuzzleStart?.Invoke();

                currentPuzzle.OnPuzzleStart();
                StartCoroutine(PlayingPuzzle(currentPuzzle));
            }
            else
            {
                Debug.LogError("No Puzzle found in gameobject: " + puzzle.name);
            }
        }

        private IEnumerator PlayingPuzzle(IPuzzle currentPuzzle)
        {
            while (PuzzlePlaying) yield return null;

            ClearPuzzle();

            _taskviewHolder.SetActive(false);
            currentPuzzle.OnPuzzleEnd();
            OnPuzzleEnd?.Invoke();
        }

        private void ClearPuzzle()
        {
            foreach (Transform child in _puzzleHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}

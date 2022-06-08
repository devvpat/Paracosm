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
        [SerializeField] private GameObject _inventorySlots;
        [SerializeField] private GameObject _selectedSlot;

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

        public void StartPuzzle(GameObject puzzle, bool enableUI)
        {
            if(puzzle.TryGetComponent(out IPuzzle currentPuzzle))
            {
                ClearPuzzle();

                Instantiate(puzzle, _puzzleHolder.transform);
                _puzzleHolder.GetComponent<Image>().enabled = enableUI;
                _taskviewHolder.GetComponent<Image>().enabled = enableUI;
                _inventorySlots.SetActive(enableUI);
                _taskviewHolder.SetActive(true);
                _selectedSlot.SetActive(enableUI);
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
            _inventorySlots.SetActive(true);
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

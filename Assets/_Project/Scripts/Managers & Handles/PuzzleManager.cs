using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class PuzzleManager : MonoBehaviour
    {
        public static PuzzleManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else if (Instance != this) Destroy(this);
        }

        public void StartPuzzle()
        {
            Debug.Log("Puzzle started");
        }
    }
}

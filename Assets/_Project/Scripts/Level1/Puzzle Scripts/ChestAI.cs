using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class ChestAI : MonoBehaviour
    {
        public delegate void InRangeAction();
        public static event InRangeAction OnEnterRange;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                OnEnterRange?.Invoke();
            }
        }
    }
}

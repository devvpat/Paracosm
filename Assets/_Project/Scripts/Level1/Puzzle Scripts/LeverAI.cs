using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class LeverAI : MonoBehaviour
    {
        public delegate void LeverPressAction();
        public static event LeverPressAction OnLeverPress;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                OnLeverPress?.Invoke();
            }
        }
    }
}

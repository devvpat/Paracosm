using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class EnterRoom3 : MonoBehaviour
    {
        [SerializeField] private GameObject _room2Object;
        [SerializeField] private Collider2D _room2Collider;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _room2Object.SetActive(false);
                _room2Collider.enabled = true;
            }
        }
    }
}

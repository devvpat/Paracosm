using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class EnterRoom2 : MonoBehaviour
    {
        [SerializeField] private GameObject _room1Object;
        [SerializeField] private GameObject _room3Object;
        [SerializeField] private Collider2D _room1Collider;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _room1Object.SetActive(false);
                _room3Object.SetActive(false);
                _room1Collider.enabled = true;
            }
        }
    }
}

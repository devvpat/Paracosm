using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class EnableRoom3 : MonoBehaviour
    {
        [SerializeField] private GameObject _room3Object;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _room3Object.SetActive(true);
            }
        }
    }
}

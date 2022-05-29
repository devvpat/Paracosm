using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace ACoolTeam
{
    public class EnterRoom1 : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;

        private float _intervalTimer = 0.045f;
        private int _counter = 20;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                StartCoroutine(ZoomOutCamera());
            }
        }

        private IEnumerator ZoomOutCamera()
        {
            for (int i = 0; i < _counter; i++)
            {
                _camera.m_Lens.OrthographicSize += 0.1f;
                yield return new WaitForSeconds(_intervalTimer);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACoolTeam
{
    public class LightPuzzle : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _light;
        [SerializeField] private GameObject _chest;

        private bool _onRightSide = true;
        private bool _triggerEntered = true;

        private void OnEnable()
        {
            ChestAI.OnEnterRange += ChangePosition;
            LeverAI.OnLeverPress += StopChestMechanic;
        }

        private void OnDisable()
        {
            ChestAI.OnEnterRange -= ChangePosition;
            LeverAI.OnLeverPress -= StopChestMechanic;
        }

        private void ChangePosition()
        {
            if (_triggerEntered)
            {
                StartCoroutine(FlickerChest());
                _triggerEntered = false;
            }
        }

        private void StopChestMechanic()
        {
            _triggerEntered = false;
        }


        private IEnumerator FlickerChest()
        {
            ToggleChest(false);
            yield return new WaitForSeconds(0.15f);
            ToggleChest(true);
            yield return new WaitForSeconds(0.25f);
            ToggleChest(false);
            yield return new WaitForSeconds(0.15f);
            ToggleChest(true);
            yield return new WaitForSeconds(0.08f);
            ToggleChest(false);
            yield return new WaitForSeconds(0.05f);
            ToggleChest(true);
            yield return new WaitForSeconds(0.03f);
            ToggleChest(false);
            yield return new WaitForSeconds(0.01f);
            ToggleChest(true);
            SetPositions();
        }
        private void ToggleChest(bool var)
        {
            _light.SetActive(var);
            _chest.SetActive(var);
        }

        private void SetPositions()
        {
            int offset = 8;

            if (_onRightSide)
            {
                _chest.transform.position = new Vector2(_chest.transform.position.x - offset, _chest.transform.position.y);
                _light.transform.position = new Vector2(_light.transform.position.x - offset, _light.transform.position.y);
            }
            else
            {
                _chest.transform.position = new Vector2(_chest.transform.position.x + offset, _chest.transform.position.y);
                _light.transform.position = new Vector2(_light.transform.position.x + offset, _light.transform.position.y);
            }
            _onRightSide = !_onRightSide;
            _triggerEntered = true;
        }
    }
}

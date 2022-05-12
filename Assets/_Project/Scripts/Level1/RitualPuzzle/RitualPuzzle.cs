using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualPuzzle : MonoBehaviour
{
    [Header("Puzzle References")]
    [SerializeField] private KeyCode _puzzleStartKey;    
    [Header("Animations")]
    [SerializeField] private GameObject _painting;
    [SerializeField] private GameObject _tentacleLeft;
    [SerializeField] private GameObject _tentacleRight;
    [Header("Ritual Mechanic")]
    [SerializeField] private RitualMechanic _ritualMechanic;
    [SerializeField] private float _delayToMechanicStart;

    private bool _inTriggerZone;
    private bool _puzzleStarted;

    private void Start()
    {
        _inTriggerZone = false;
        _puzzleStarted = false;
    }

    //ADD: hook this up to be called when interact key is pressed
    public void TryStartPuzzle()
    {
        if (!_puzzleStarted && _inTriggerZone) StartCoroutine(BeginPuzzle());
    }

    private IEnumerator BeginPuzzle()
    {
        _puzzleStarted = true;
        //ADD: freeze player movement

        //ADD: play tentacles coming out of painting animation
        
        //ADD: play tentacles coming out of ground animation
        
        //begin mechanic after delay (so players can see animations clearly before mechanic)
        yield return new WaitForSeconds(_delayToMechanicStart);
        _ritualMechanic.gameObject.SetActive(true);
        StartCoroutine(_ritualMechanic.BeginMechanic());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.name == "Player") _inTriggerZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.root.name == "Player") _inTriggerZone = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualMechanic : MonoBehaviour
{
    //Ref: When using 1920x1080 in game tab:
    //0,0 = center of UI, UI encompasses [-400, -225] (bottom left) to [400, 225] (top right) = 800x450 px

    [Header("Mechanic Settings")]
    [SerializeField, Min(0f)] private int _totalRounds;
    [SerializeField] private Vector2 _greenZoneRangeX;     //[0] = left bound, [1] = right bound --> green zone will spawn somewhere in this range, make sure [1] > [0]
    [SerializeField] private float _greenZonePosY;    //Y pos in the UI, 
    [SerializeField, Min(0f)] private float _tickBuffer;    //check for +- these units from exact pos of green zone's center
    [SerializeField, Min(1f)] private float _tickSpeed;
    [SerializeField] private float _speedIncreaseByRound;   //multiplied with current speed at end of round
    [SerializeField, Min(0f)] private float _timeBetweenRounds;
    [SerializeField, Min(0f)] private float _tentacleSpeed;      //how fast tentacles move
    [SerializeField, Range(0f, 1f)] private float _correctClickReward;     //how much tentacles go back (away from center) on good click, 0.0-1.0
    [SerializeField, Range(0f, 1f)] private float _incorrectClickPenalty;  //how much tentacles go forward (toward center) on bad click, 0.0-1.0
    //ADD: change these UI image objects to have appropriate sprite/animations, use same width/height as current image objects if possible
    [Header("Mechanic References")]
    [SerializeField] private RectTransform _greenZoneRectTransform;
    [SerializeField] private RectTransform _tickMarkRectTransform;
    [Header("Tentacle Screen Effects")]
    [SerializeField] private RectTransform _topTentacleRectTransform;
    [SerializeField] private RectTransform _bottomTentacleRectTransform;

    private IEnumerator _screenTentacleEffect;
    private IEnumerator _moveTickMark;  
    private float _greenZonePosX;   //stores exact pos of green zone, used with buffer var to determine validity of click
    private float inputPos;     //stores pos of tick mark on click
    private float _canvasWidth;
    private float _canvasHeight;
    private float _tentacleProgress;    //0 - 1 (0-100%)

    public IEnumerator BeginMechanic()
    {
        _canvasWidth = this.GetComponent<RectTransform>().rect.width;
        _canvasHeight = this.GetComponent<RectTransform>().rect.height;
        _screenTentacleEffect = ScreenTentacleEffect();
        _moveTickMark = MoveTickMark();
        int successfulRounds = 0;
        while (successfulRounds < _totalRounds)
        {
            RoundSetup();
            inputPos = float.MaxValue;
            //uncomment below if tick should restart at middle every round
            //_moveTickMark = MoveTickMark();
            StartCoroutine(_moveTickMark);
            StartCoroutine(_screenTentacleEffect);
            while (true)
            {
                //ADD: make this code get called on interact key press
                if (Input.GetKeyDown(KeyCode.W))
                {
                    StopCoroutine(_moveTickMark);
                    inputPos = _tickMarkRectTransform.localPosition.x;
                    //if guess in zone-buffer <= guess <= zone+buffer, count as successful gues
                    if (_greenZonePosX - _tickBuffer <= inputPos && inputPos <= _greenZonePosX + _tickBuffer) break;
                    else
                    {
                        //move tentacles forward on bad click
                        _tentacleProgress = Mathf.Min(_tentacleProgress + _incorrectClickPenalty, 1);
                        StartCoroutine(_moveTickMark);
                    }
                }
                yield return null;
            }
            StopCoroutine(_screenTentacleEffect);
            successfulRounds++;
            //move tentacles back
            _tentacleProgress = Mathf.Max(_tentacleProgress - _correctClickReward, 0);
            //increase tick speed every round
            _tickSpeed *= _speedIncreaseByRound;
            yield return new WaitForSeconds(_timeBetweenRounds);
        }
        PlayerWinGame();
    }

    //sets green zone pos between 5% - 95% of the valid range (_greenZoneRangeX[0] to _greenZoneRangeX[1])
    private void RoundSetup()
    {
        _greenZonePosX = Random.Range(5, 96) * (_greenZoneRangeX[1] - _greenZoneRangeX[0]) / 100f + _greenZoneRangeX[0];
        _greenZoneRectTransform.localPosition = new Vector3(_greenZonePosX, _greenZonePosY, 0);
    }

    private IEnumerator MoveTickMark()
    {
        //starting pos = middle
        float tickPos = (_greenZoneRangeX[1] + _greenZoneRangeX[0]) / 2f;
        _tickMarkRectTransform.localPosition = new Vector3(tickPos, _greenZonePosY, 0);
        //go to right first
        int dir = 1;
        while (true)
        {
            if (dir == 1)
            {
                tickPos += Time.deltaTime * _tickSpeed;
                _tickMarkRectTransform.localPosition = new Vector3(tickPos, _greenZonePosY, 0);
                //go to left once right bound is hit
                if (tickPos >= _greenZoneRangeX[1]) dir = -1;
                yield return null;
            }
            else
            {
                tickPos -= Time.deltaTime * _tickSpeed;
                _tickMarkRectTransform.localPosition = new Vector3(tickPos, _greenZonePosY, 0);
                //go to right once left bound is hit
                if (tickPos <= _greenZoneRangeX[0]) dir = 1;
                yield return null;
            }
        }
    }

    private IEnumerator ScreenTentacleEffect()
    {
        _tentacleProgress = 0;
        float topTentaclePos = _canvasHeight / 4 + 225;
        _topTentacleRectTransform.localPosition = new Vector3(0, topTentaclePos, 0);
        float bottomTentaclePos = -1 * topTentaclePos;
        _bottomTentacleRectTransform.localPosition = new Vector3(0, bottomTentaclePos, 0);
        while (true)
        {
            _tentacleProgress += Time.deltaTime * _tentacleSpeed;    
            topTentaclePos = _canvasHeight / 4 + 225 - _canvasHeight /2 * _tentacleProgress;
            topTentaclePos = Mathf.Max(_canvasHeight / 4, topTentaclePos);
            _topTentacleRectTransform.localPosition = new Vector3(0, topTentaclePos, 0);
            bottomTentaclePos = -1 * topTentaclePos;
            _bottomTentacleRectTransform.localPosition = new Vector3(0, bottomTentaclePos, 0);
            if (_tentacleProgress >= 1) break;
            yield return null;
        }
        PlayerLoseGame();
    }

    private void PlayerLoseGame()
    {
        StopCoroutine(_screenTentacleEffect);
        //ADD: unfreeze movement

        gameObject.SetActive(false);
        Debug.Log("LOSE");
    }

    private void PlayerWinGame()
    {
        StopCoroutine(_screenTentacleEffect);
        //ADD: unfreeze movement

        gameObject.SetActive(false);
        Debug.Log("WIN");
    }

}

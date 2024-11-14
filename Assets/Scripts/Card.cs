using System;
using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour, ICard
{
    [SerializeField] private Image cardImage;
    [SerializeField] private Image cardBackImage;
    [SerializeField] private Image cardSubstrateImage;
    [SerializeField] private Button cardButton;

    private Sprite _frontSideSprite;
    private Sprite _backSideSprite;
    private GameConfig _config;
    private bool _flipped;
    private bool _turning;
    private IGame _game;
    private int _indexInField;

    public event Action<int> CardClicked;

    public void SetUp(int indexInField, Sprite backSide, Sprite frontSide, GameConfig gameConfig, IGame game)
    {
        _indexInField = indexInField;
        _frontSideSprite = frontSide;
        _backSideSprite = backSide;
        _config = gameConfig;
        _game = game;
        
        cardBackImage.sprite = _backSideSprite;
        cardImage.sprite = _frontSideSprite;
        ResetCardState();
    }

    public void OnCardClicked()
    {
        if (_flipped || _turning) return;
        if (!_game.GameIsOn) return;
        Flip();
        StartCoroutine(RiseCardClickedEventSuspended());
    }
    
    private IEnumerator RiseCardClickedEventSuspended()
    {
        yield return new WaitForSeconds(_config.CardClickedDelay);
        CardClicked?.Invoke(_indexInField);
    }
    
    // perform a 180 degree flip
    public void Flip()
    {
        _turning = true;
        StartCoroutine(Flip90(transform, true));
    }

    public void Hide()
    {
        StartCoroutine(ReduceSizeAndHide());
        cardButton.interactable = false;
    }
    
    private IEnumerator ReduceSizeAndHide()
    {
        var t = 0.0f;
        while (t <=_config.HideCardTime)
        {
            t += Time.deltaTime/_config.HideCardTime;
            var scaledValue = Mathf.Lerp(1.0f, 0.0f, t);
            var newScaleValue = new Vector3(scaledValue,scaledValue, 1);
            cardSubstrateImage.rectTransform.localScale = newScaleValue;

            yield return null;
        }
        cardSubstrateImage.gameObject.SetActive(false);
        
    }

    private void ShowCardSide(bool frontSide)
    {
        cardBackImage.gameObject.SetActive(!frontSide);
        cardImage.gameObject.SetActive(frontSide);
    }

    private IEnumerator Flip90(Transform thisTransform, bool changeSprite)
    {
        var rotation = thisTransform.rotation;
        Quaternion startRotation = rotation;
        Quaternion endRotation = rotation * Quaternion.Euler(new Vector3(0, 90, 0));
        var time = 0.0f;
        float interpolator = 0.0f;
        while (interpolator <= 1)
        {
            interpolator = time / _config.Flip90CardTime;
            time += Time.deltaTime;
            thisTransform.rotation = Quaternion.Slerp(startRotation, endRotation, interpolator);
            yield return null;
        }
        thisTransform.rotation = endRotation;
        //change sprite and continue flipping
        if (changeSprite)
        {
            _flipped = !_flipped; 
            ShowCardSide(_flipped);
            StartCoroutine(Flip90(transform, false));
        }
        else
            _turning = false;
    }

    public void ResetCardState()
    {
        ShowCardSide(false);
        cardButton.interactable = true;
        cardSubstrateImage.gameObject.SetActive(true);
        cardSubstrateImage.rectTransform.localScale = Vector3.one;
        _flipped = false;
        _turning = false;
    }
}

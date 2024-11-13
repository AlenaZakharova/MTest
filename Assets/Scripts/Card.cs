using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour, ICard
{
    [SerializeField] private Image cardImage;
    [SerializeField] private Image cardBackImage;
    [SerializeField] private Button cardButton;

    private Sprite _frontSideSprite;
    private Sprite _backSideSprite;

    private bool _flipped;
    private bool _turning;

    private IGame _game;

    public void SetUp(Sprite backSide, Sprite frontSide, IGame game)
    {
        _frontSideSprite = frontSide;
        _backSideSprite = backSide;
        _game = game;
        
        cardBackImage.sprite = _backSideSprite;
        cardImage.sprite = _frontSideSprite;
    }

    public void OnCardClicked()
    {
        if (_flipped || _turning) return;
        if (!_game.GameIsOn) return;
        Flip();
    }
    
    // perform a 180 degree flip
    private void Flip()
    {
        _turning = true;
        StartCoroutine(Flip90(transform, 0.25f, true));
    }

    public void Hide()
    {
        //play vfx
        cardImage.enabled = false;
    }

    private void ShowCardSide(bool frontSide)
    {
        //rotate card
        cardBackImage.gameObject.SetActive(!frontSide);
        cardImage.gameObject.SetActive(frontSide);
    }

    private IEnumerator Flip90(Transform thisTransform, float time, bool changeSprite)
    {
        var rotation = thisTransform.rotation;
        Quaternion startRotation = rotation;
        Quaternion endRotation = rotation * Quaternion.Euler(new Vector3(0, 90, 0));
        float rate = 1.0f / time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            thisTransform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            yield return null;
        }
        //change sprite and continue flipping
        if (changeSprite)
        {
            _flipped = !_flipped;
            ShowCardSide(_flipped);
            StartCoroutine(Flip90(transform, time, false));
        }
        else
            _turning = false;
    }
}

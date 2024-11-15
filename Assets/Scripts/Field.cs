using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour, IField
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private GameObject rowPrefab;

    private List<GameObject> _rows = new List<GameObject> { };
    private List<Card> _cards = new List<Card>();
    private Transform _fieldTransform;

    private GameConfig _config;

    public IReadOnlyList<ICard> Cards => _cards;

    public void SetUp(GameConfig gameConfig)
    {
        _config = gameConfig;
    }

    public void ShowAllCardsBeforePlaying()
    {
        StartCoroutine(ShowCardsBeforePlaying());
    }
    
    private IEnumerator ShowCardsBeforePlaying()
    {
        foreach (var card in Cards)
            card.Flip(false);   
        yield return new WaitForSeconds(_config.ShowAllCardsTime);
        foreach (var card in Cards)
            card.Flip(false);
    }


    private void OnEnable()
    {
        _fieldTransform = transform;
    }

    public void RebuildField(int width, int height, bool withDelay)
    {
        StartCoroutine(RebuildFieldWithDelay(width, height, withDelay));
    }

    private IEnumerator RebuildFieldWithDelay(int width, int height, bool withDelay)
    {
        if(withDelay)
            yield return new WaitForSeconds(_config.ShowWinPanelDelay); 
        RebuildField(width, height);
    }

    private void RebuildField(int width, int height)
    {
        if (_rows.Count > 0 || _cards.Count > 0)
            DespawnFieldChildren();

        for (int i = 0; i < height; i++)
        {
            var rowGameObject = Lean.Pool.LeanPool.Spawn(rowPrefab);
            rowGameObject.transform.SetParent(_fieldTransform);
            _rows.Add(rowGameObject);
            for (int j = 0; j < width; j++)
            {
                var newCard = Lean.Pool.LeanPool.Spawn(cardPrefab);
                {
                    newCard.transform.SetParent(rowGameObject.transform);
                    _cards.Add(newCard);
                }
            }
        }
    }

    private void DespawnFieldChildren()
    {
        _fieldTransform.GetComponentsInChildren<Card>().ToList().ForEach(card =>
        {
            _cards.Remove(card);
            card.ResetCardState();
            Lean.Pool.LeanPool.Despawn(card);
        });

        _fieldTransform.GetComponentsInChildren<HorizontalLayoutGroup>().ToList().ForEach(row =>
        {
            _rows.Remove(row.gameObject);
            Lean.Pool.LeanPool.Despawn(row);
        });
    }
}
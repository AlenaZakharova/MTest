using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class Field : MonoBehaviour, IField
{
    [SerializeField] private Card card;

    private List<HorizontalLayoutGroup> _rows = new List<HorizontalLayoutGroup>{ };
    private List<ICard> _cards = new List<ICard>();
    private Transform _fieldTransform;

    private GameConfig config;
    
    private void OnEnable()
    {
        _fieldTransform = transform;
    }

    public void Init(GameConfig gameConfig)
    {
        config = gameConfig;
    }

    public void RebuildField(int width, int height)
    {
        _rows.Clear();
        _cards.Clear();
        _fieldTransform.gameObject.DestroyChildren();
        
        for (int i = 0; i < height; i++)
        {
            var rowGameObject = new GameObject();
            rowGameObject.transform.SetParent(_fieldTransform);
            var horizontalLGroup = rowGameObject.AddComponent<HorizontalLayoutGroup>();
            horizontalLGroup.spacing = config.FieldSpacing;
            for (int j = 0; j < width; j++)
            {
                var newCard = Instantiate(card);
                {
                    newCard.transform.SetParent(horizontalLGroup.transform);
                    _cards.Add(newCard);
                }
            }
        }
    }
}

public static class GameObjectExtensions
{
    public static void DestroyChildren(this GameObject t)
    {
        t.transform.Cast<Transform>().ToList().ForEach(c => Object.Destroy(c.gameObject));
    }
}

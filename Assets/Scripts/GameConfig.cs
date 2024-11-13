using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "MemTrainer/GameConfig", order = 0)]
public class GameConfig : ScriptableObject
{
    [Header("Sprites")]
    [SerializeField] private Sprite[] cardImageSprites;
    [SerializeField] private Sprite cardBackSprite;

    [Header("UI")] 
    [SerializeField] private int fieldSpacing = 20;

    [SerializeField] private int maxFieldSideDimension = 8;
      
    //Sprites
    public Sprite[] CardImageSprites => cardImageSprites;
    public Sprite CardBackSprite => cardBackSprite;
    
    //UI
    public int FieldSpacing => fieldSpacing;
    public int MaxFieldSideDimension => maxFieldSideDimension;
}
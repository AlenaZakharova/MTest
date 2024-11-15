using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "MemTrainer/GameConfig", order = 0)]
public class GameConfig : ScriptableObject
{
    [Header("Sprites")]
    [SerializeField] private Sprite[] cardImageSprites;
    [SerializeField] private Sprite cardBackSprite;

    [Header("UI")] 
    [SerializeField] private int maxFieldSideDimension = 8;
    
    [Header("Timing")] 
    [SerializeField] private float flip90CardTime = 0.25f;
    [SerializeField] private float hideCardTime = 0.2f;
    [SerializeField] private float cardClickedDelay = 0.6f;
    [SerializeField] private float bounceTime = 0.5f;
    [SerializeField] private float showAllCardsTime = 2f;
    [SerializeField] private float showWinPanelDelay = 2f;
      
    //Sprites
    public Sprite[] CardImageSprites => cardImageSprites;
    public Sprite CardBackSprite => cardBackSprite;
    
    //UI
    public int MaxFieldSideDimension => maxFieldSideDimension;
    
    //Timing
    public float Flip90CardTime => flip90CardTime;
    public float HideCardTime => hideCardTime;
    public float CardClickedDelay => cardClickedDelay;
    public float BounceTime => bounceTime;
    public float ShowAllCardsTime => showAllCardsTime;
    public float ShowWinPanelDelay => showWinPanelDelay;
}
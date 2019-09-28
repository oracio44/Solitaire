using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solitaire : MonoBehaviour
{
    [SerializeField]
    GameObject CardPrefab = null;
    [SerializeField]
    Transform[] bottomOrigin = new Transform[7];

    [SerializeField]
    private float _YOffset = 0.5f;
    [SerializeField]
    private float _ZOffset = 0.15f;

    [SerializeField]
    State DealtFaceDownState;

    public float YOffset { get { return _YOffset; } }
    public float ZOffset { get { return _ZOffset; } }

    public PlayingCardsDeck Deck;

    public static Solitaire GameManager;


    private void Awake()
    {
        if (GameManager == null)
        {
            GameManager = this;
        }
        if (GameManager != this)
        {
            Destroy(this);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Deck = new PlayingCardsDeck();
        Deck.ShuffleDeck();
        StartCoroutine(DealSolitaireCoroutine());
    }

    void DealFinished()
    {
        this.gameObject.AddComponent<PlayerInput>();
    }

    IEnumerator DealSolitaireCoroutine()
    {
        GameObject[,] playGrid = new GameObject[7, 7];
        GameObject lastAdded = null;
        for (int i = 0; i < bottomOrigin.Length; i++)
        {
            for (int j = bottomOrigin.Length - 1; j >= i; j--)
            {
                yield return new WaitForSeconds(0.10f);
                Vector3 newPosition = new Vector3(bottomOrigin[j].position.x, (bottomOrigin[j].position.y - (YOffset * i)), -(ZOffset * i));
                PlayingCard cardData = Deck.DealTopCard();
                lastAdded = CreateCardAtPosition(cardData, newPosition);
                lastAdded.GetComponent<StateController>().ChangeState(DealtFaceDownState);
                playGrid[i, j] = lastAdded;
                if (i == 0)
                {
                    lastAdded.transform.SetParent(bottomOrigin[j]);
                }
                else
                {
                    lastAdded.transform.SetParent(playGrid[i - 1, j].transform);
                }
            }
            CardGameObject lastAddedGameObject = lastAdded.GetComponent<CardGameObject>();
            lastAddedGameObject.TurnFaceUp();
            lastAddedGameObject.Activate();
        }
        DealFinished();
    }

    public GameObject CreateCardAtPosition(PlayingCard cardData, Vector3 newPosition)
    {
        GameObject newCard = Instantiate(CardPrefab);
        newCard.transform.position = newPosition;
        newCard.GetComponent<CardGameObject>().SetupCard(cardData);
        return newCard;
    }
}

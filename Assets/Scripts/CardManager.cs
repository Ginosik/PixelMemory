using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Carta card1, card2;
    public List<Carta> cartas;
    public List<GameObject> Prefabs;
    public delegate void VictoryConditionEventHandler();
    public event VictoryConditionEventHandler VictoryConditionSatisfied;

    private void Awake()
    {
        cartas = new List<Carta>();
        SpawnCards();
        PositionCards();
        //Para cada.
        /*
        foreach (Carta carta in GetComponentsInChildren<Carta>())
        {
            //Confere carta e se não existir adiciona.
            if (!cartas.Contains(carta))
            {
                //Adicionar carta aqui.
                cartas.Add(carta);
                carta.SetCardManager(this);
            }
        }
        */
    }

    private List<Carta> ShuffleList()
    {
        List<Carta> shuffle = cartas;
        for (int i = 0; i < shuffle.Count; i++)
        {
            Carta temp = shuffle[i];
            int randomIndex = Random.Range(i, shuffle.Count);
            shuffle[i] = shuffle[randomIndex];
            shuffle[randomIndex] = temp;
        }
        return shuffle;
    }

    private void PositionCards()
    {
        cartas = ShuffleList();
        Vector2 cardPosition = new Vector2(-3.5f, -1f);
        int index = 0;
        for (int y = 1; y <= 2; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                cartas[index].transform.localPosition = cardPosition;
                cardPosition.x += 1;
                index++;
            }
            cardPosition.y += 2;
            cardPosition.x = -3.5f;
        }
    }

    private void SpawnCards()
    {
        for (int i = 0; i < GM.instance.cardPairs; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                GameObject obj = Instantiate(Prefabs[i], gameObject.transform);
                Carta carta = obj.GetComponent<Carta>();
                if (carta)
                {
                    cartas.Add(carta);
                    carta.SetCardManager(this);
                }
            }
        }
    }

    public bool CardClicked(Carta newCard)
    {
        if (card1 == null)
        {
            card1 = newCard;
            return true;
        }
        else if (newCard != card1)
        {
            if (card2 == null)
            {
                card2 = newCard;
                return true;
            }
            /*
            else if (newCard != card2)
            {
                if (card1 != null && card2 != null)
                {
                    card1.TurnCard();
                    card2.TurnCard();
                    card1 = null;
                    card2 = null;
                    card1 = newCard;
                    return true;
                }
            }
            */
        }

        return false;
    }

    public void CompareCards()
    {
        if (card1 != null && card2 != null && !card1.IsTurning() && !card2.IsTurning())
        {

            if (card1.num == card2.num)
            {
                if (cartas.Contains(card1))
                {
                    cartas.Remove(card1);
                }
                if (cartas.Contains(card2))
                {
                    cartas.Remove(card2);
                }
                Destroy(card1.gameObject);
                Destroy(card2.gameObject);
                if (cartas.Count == 0)
                {
                    if (VictoryConditionSatisfied != null)
                    {
                        VictoryConditionSatisfied();
                    }
                }
            }
            else
            {
                card1.TurnCard();
                card2.TurnCard();
                card1 = null;
                card2 = null;
            }
        }
    }
}
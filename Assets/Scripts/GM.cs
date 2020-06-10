using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public CardManager cm;
    public static GM instance { get; private set; }
    [Range(2, 8)]
    public int cardPairs = 3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        

        cm.VictoryConditionSatisfied += VictoryConditionSatisfied;
    }

    void VictoryConditionSatisfied()
    {
        string result = (false) ? "hue" : "haha";
        Debug.Log(result);
    }

}
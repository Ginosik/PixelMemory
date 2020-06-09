using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour
{
    float giro = 0;
    public float graus;
    public int num;
    private bool turned = false;
    private CardManager cm;
    private Coroutine turneCoroutine;
    public float turnSpeed = 300;

    public bool IsTurning()
    {
        return turneCoroutine != null;
    }

    private void OnMouseDown()
    {
        if (turneCoroutine == null)
        {
            if (cm.CardClicked(this))
            {
                TurnCard();
            }
        }
    }

    public void TurnCard()
    {
        turneCoroutine = StartCoroutine(TurneCoroutine());
    }

    public void StopCard()
    {
        if (turneCoroutine != null)
        {
            StopCoroutine(turneCoroutine);
            turneCoroutine = null;
        }
    }

    IEnumerator TurneCoroutine()
    {
        if (!turned)
        {
            while (!turned)
            {
                yield return new WaitForFixedUpdate();
                giro = turnSpeed * Time.deltaTime;
                graus = transform.localEulerAngles.y;
                if (graus < 180)
                {
                    transform.Rotate(Vector3.up, giro);
                }
                else
                {
                    transform.localEulerAngles = new Vector3(0, 180, 0);
                    turned = true;
                }
            }
        }
        else
        {
            while (turned)
            {
                yield return new WaitForFixedUpdate();
                giro = turnSpeed * Time.deltaTime;
                graus = transform.localEulerAngles.y;
                if (graus > 0 && graus >= 180)
                {
                    transform.Rotate(Vector3.up, giro);
                }
                else
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                    turned = false;
                }
            }
        }
        turneCoroutine = null;
        cm.CompareCards();
    }

    public void SetCardManager(CardManager newCardManager)
    {
        cm = newCardManager;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row10 : MonoBehaviour
{

    private int randomValue; // número de rotações do row, quanto tempo cada row vai ficar girando
    private float timeInterval; // diminui o movimento da row durante o spinning

    public bool rowStopped;
    public RewardEnum stoppedSlot; // nome do item que deu no row

    public enum RewardEnum { coins, a, b, c, d, e, f, g, h, i, coins2};

    void Start()
    {
        rowStopped = true;
        GameControl10.HandlePulled += StartRotating;
    }
    private void StartRotating()
    {
        stoppedSlot = RewardEnum.coins;
        StartCoroutine(Rotate());
    }
    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.025f; // time between shifts of roll position when it's spinning
        
        for (int i = 0; i < 30; i++)
        {
            if (transform.localPosition.y <= -3.12f) // -3.12
                transform.localPosition = new Vector2(transform.localPosition.x, 4.38f); // 4.38

            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 0.25f);

            yield return new WaitForSeconds(timeInterval);
        }
        
        randomValue = Random.Range(50, 100);

        switch (randomValue % 3)
        {
            case 1:
                randomValue += 2;
                break;
            case 2:
                randomValue += 1;
                break;
        }

        for (int i = 0; i < randomValue; i++)
        {
            if (transform.localPosition.y <= -3.12f)
                transform.localPosition = new Vector2(transform.localPosition.x, 4.38f);

            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 0.25f);

            if (i > Mathf.RoundToInt(randomValue * 0.25f))
                timeInterval = 0.05f;
            if (i > Mathf.RoundToInt(randomValue * 0.5f))
                timeInterval = 0.1f;
            if (i > Mathf.RoundToInt(randomValue * 0.75f))
                timeInterval = 0.15f;
            if (i > Mathf.RoundToInt(randomValue * 0.95f))
                timeInterval = 0.2f;

            yield return new WaitForSeconds(timeInterval);
        }

        for (int j = 0; j < 11; j++)
        {
            float position = -3.12f + 0.75f * j;
            if (transform.localPosition.y == position)
            {
                stoppedSlot = (RewardEnum)j;
            }
        }

        


        rowStopped = true;
    }
    private void OnDestroy()
    {
        GameControl10.HandlePulled -= StartRotating;
    }

  
}

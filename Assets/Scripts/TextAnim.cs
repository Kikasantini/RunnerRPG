using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class TextAnim : MonoBehaviour
{
    public Text[] text;

    public void Losing(int points, int index)
    {
        StartCoroutine(LosingHPoints(points, index));
    }

    IEnumerator LosingHPoints(int points, int index)
    {
        Debug.Log("XXXXXXXXXXXXX");
        text[index].DOFade(0, 0);
        text[index].enabled = true;
        Vector3 originalScale = new Vector3(0, 0, 0);
        Vector3 newScale = new Vector3(0, 0, 0);
        originalScale = text[index].transform.localScale;
        newScale = text[index].transform.localScale;

        float jump = 0.01f;

        Vector2 pointsPosInit = new Vector2(0, 0);
        pointsPosInit = text[index].transform.position;
        text[index].text = "-" + points;

        text[index].DOFade(1, 0.5f); // fade in

        for (int i = 0; i < 50; i++)
        {
            pointsPosInit.y += jump;
            text[index].transform.position = pointsPosInit;

            newScale.x += 0.01f;
            newScale.y += 0.01f;
            newScale.z += 0.01f;
            text[index].transform.localScale = newScale;

            yield return new WaitForSeconds(0.01f);
        }

        text[index].DOFade(0, 0.5f); // fade out

        for (int i = 50; i < 100; i++)
        {
            pointsPosInit.y += jump;
            text[index].transform.position = pointsPosInit;

            newScale.x += 0.01f;
            newScale.y += 0.01f;
            newScale.z += 0.01f;
            text[index].transform.localScale = newScale;

            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);
        text[index].enabled = false;
        pointsPosInit.y -= jump * 100;
        text[index].transform.position = pointsPosInit; // texto volta à posição original
        text[index].transform.localScale = originalScale; // texto volta ao tamanho original
        
    }

    IEnumerator GainingHPoints(int points)
    {
        text[4].DOFade(0, 0);
        text[4].enabled = true;
        Vector3 originalScale = new Vector3(0, 0, 0);
        Vector3 newScale = new Vector3(0, 0, 0);
        originalScale = text[4].transform.localScale;
        newScale = text[4].transform.localScale;
        text[4].text = "+" + points;

        text[4].DOFade(1, 0.5f); // fade in

        for (int i = 0; i < 100; i++)
        {
            newScale.x += 0.01f;
            newScale.y += 0.01f;
            newScale.z += 0.01f;
            text[4].transform.localScale = newScale;

            // escalar e rotacionar número
            yield return new WaitForSeconds(0.005f);
        }
        text[4].DOFade(0, 1f); // fade out
        text[4].enabled = false;
        yield return new WaitForSeconds(1f);
        text[4].transform.localScale = originalScale; // texto volta ao tamanho original


    }

}

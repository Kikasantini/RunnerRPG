using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public AudioSource click;

    public void PlayClick()
    {

        click.Play();
    }
}

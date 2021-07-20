using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void CloseGame()
    {
        Debug.Log("Close game.");
        Application.Quit();
    }
}

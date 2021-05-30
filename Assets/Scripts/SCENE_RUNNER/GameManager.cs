using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public int wood = 0;
    public int iron = 0;
    public int gold = 0;
    public int coins = 0;

    public GameManager Instance;

    private void Start()
    {
        Instance = this;
    }

    bool gameHasEnded = false;
    public float restartDelay = 2f;
    public GameObject completeLevelUI;
    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }
    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game over");

            Invoke("Restart", restartDelay); // Invoke chama uma função, mas dá um delay antes
            //Restart();
        }
        
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

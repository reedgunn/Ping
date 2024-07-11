using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueOrQuit : MonoBehaviour {
    
    public GameObject whoWins;

    void Start() {
        if (!BallMovement.playerWins) whoWins.GetComponent<TMPro.TextMeshProUGUI>().text = "You lost!";
    }

    public void Continue() {
        SceneManager.LoadScene(0);
    }
    
}
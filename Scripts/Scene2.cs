using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2 : MonoBehaviour {
    
    public GameObject whoWins;

    void Start() {
        if (Scene1.playerScore > Scene1.computerScore) {
            whoWins.GetComponent<TMPro.TextMeshProUGUI>().text = "You won!\n" + Scene1.playerScore.ToString() + " - " + Scene1.computerScore.ToString();
        } else {
            whoWins.GetComponent<TMPro.TextMeshProUGUI>().text =  "You lost.\n" + Scene1.playerScore.ToString() + " - " + Scene1.computerScore.ToString();
        }
    }

    public void GoBackToMainMenu() {
        SceneManager.LoadScene(0);
    }
    
}
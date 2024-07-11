using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyLevel : MonoBehaviour {
    
    public static float ballSpeedX;
    public static float paddleScaleY;
    public static float ballDiameter;

    public void Easy() {
        paddleScaleY = 1.4f;
        ballSpeedX = 9;
        ballDiameter = 0.5f;
        SceneManager.LoadScene(1);
    }

    public void Moderate() {
        paddleScaleY = 1.2f;
        ballSpeedX = 12;
        ballDiameter = 0.45f;
        SceneManager.LoadScene(1);
    }

    public void Hard() {
        paddleScaleY = 1.1f;
        ballSpeedX = 15;
        ballDiameter = 0.4f;
        SceneManager.LoadScene(1);
    }
    
}
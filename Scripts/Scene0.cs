using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene0 : MonoBehaviour {
    
    public static float ballSpeedX;
    public static float paddleScaleY;
    public static float ballDiameter;

    public void Easy() {
        ballSpeedX = 7f;
        paddleScaleY = 1.4f;
        ballDiameter = 0.35f;
        SceneManager.LoadScene(1);
    }

    public void Moderate() {
        ballSpeedX = 9f;
        paddleScaleY = 1.2f;
        ballDiameter = 0.3f;
        SceneManager.LoadScene(1);
    }

    public void Hard() {
        ballSpeedX = 11f;
        paddleScaleY = 1f;
        ballDiameter = 0.25f;
        SceneManager.LoadScene(1);
    }
    
}
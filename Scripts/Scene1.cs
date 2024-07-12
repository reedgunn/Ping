using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour {
    
    const float PADDLE_SCALE_X = 0.3821f;
    const int PADDLE_SCALE_Z = 0;
    const int SCORE_TO_WIN = 7;
    const float MAX_X_POS = 8;
    const float MAX_Y_POS = 5;
    const float DIST_ORIGIN_TO_PADDLE = 6.8090f;
    const int BALL_Z_POSITION = 0;
    const int PADDLES_Z_POSITION = 0;
    const int PADDLES_ABS_X_POS = 7;
    static int playerScore = 0;
    static int computerScore = 0;
    public static bool playerWins;
    public GameObject leftPaddle;
    public GameObject rightPaddle;
    public GameObject playerScoreVisual;
    public GameObject computerScoreVisual;
    float velocityX;
    float velocityY;
    float ballMaxYPos;
    float ballMaxXPos;
    float initialDirection;
    float paddleSpeed;
    float paddleRadius;
    float ballRadius;
    float paddleAbsMaxPosY;
    float ballAbsXPosWhenBouncePaddle;
    bool leftPaddleMovingUp;
    bool leftPaddleMovingDown;
    bool rightPaddleMovingUp;
    bool rightPaddleMovingDown;

    void Start() {
        // Set ball size:
        transform.localScale = new Vector3(DifficultyLevel.ballDiameter, DifficultyLevel.ballDiameter, DifficultyLevel.ballDiameter);
        // Set paddle size:
        leftPaddle.transform.localScale = new Vector3(PADDLE_SCALE_X, DifficultyLevel.paddleScaleY, PADDLE_SCALE_Z);
        rightPaddle.transform.localScale = new Vector3(PADDLE_SCALE_X, DifficultyLevel.paddleScaleY, PADDLE_SCALE_Z);
        // Calculate paddle speed:
        paddleSpeed = Mathf.Sqrt(3) * DifficultyLevel.ballSpeedX / 2;
        // Calculating initial direction of ball:
        initialDirection = Random.Range(-Mathf.PI/4, Mathf.PI/4);
        if (Random.value > 0.5) initialDirection += Mathf.PI;
        // Calculating velocityX:
        if (Mathf.Cos(initialDirection) > 0) velocityX = DifficultyLevel.ballSpeedX;
        else velocityX = -DifficultyLevel.ballSpeedX;
        // Calculate velocityY:
        velocityY = Mathf.Tan(initialDirection) * DifficultyLevel.ballSpeedX;
        // Useful values to precalculate:
        paddleRadius = DifficultyLevel.paddleScaleY / 2;
        ballRadius = transform.localScale.x / 2;
        paddleAbsMaxPosY = MAX_Y_POS - paddleRadius - ballRadius;
        ballMaxXPos = MAX_X_POS - ballRadius;
        ballMaxYPos = MAX_Y_POS - ballRadius;
        ballAbsXPosWhenBouncePaddle = DIST_ORIGIN_TO_PADDLE - ballRadius;
        // Update score visuals:
        playerScoreVisual.GetComponent<TMPro.TextMeshProUGUI>().text = playerScore.ToString();
        computerScoreVisual.GetComponent<TMPro.TextMeshProUGUI>().text = computerScore.ToString();
    }

    void Update() {
        // Ball movement:
        transform.position = new Vector3(transform.position.x + velocityX * Time.deltaTime, transform.position.y + velocityY * Time.deltaTime, BALL_Z_POSITION);
        // Left paddle movement:
        if (Input.GetKey(KeyCode.W) && leftPaddle.transform.position.y < paddleAbsMaxPosY) {
            leftPaddleMovingUp = true;
            leftPaddleMovingDown = false;
            leftPaddle.transform.position = new Vector3(-PADDLES_ABS_X_POS, leftPaddle.transform.position.y + paddleSpeed * Time.deltaTime, PADDLES_Z_POSITION);
        }
        else if (Input.GetKey(KeyCode.S) && leftPaddle.transform.position.y > -paddleAbsMaxPosY) {
            leftPaddleMovingUp = false;
            leftPaddleMovingDown = true;
            leftPaddle.transform.position = new Vector3(-PADDLES_ABS_X_POS, leftPaddle.transform.position.y - paddleSpeed * Time.deltaTime, PADDLES_Z_POSITION);
        }
        else {
            leftPaddleMovingUp = false;
            leftPaddleMovingDown = false;
        }
        // Right paddle movement:
        if (rightPaddle.transform.position.y < transform.position.y - paddleRadius && rightPaddle.transform.position.y < paddleAbsMaxPosY && velocityX > 0) {
            rightPaddleMovingUp = true;
            rightPaddleMovingDown = false;
            rightPaddle.transform.position = new Vector3(PADDLES_ABS_X_POS, rightPaddle.transform.position.y + paddleSpeed * Time.deltaTime, PADDLES_Z_POSITION);
        }
        else if (rightPaddle.transform.position.y > transform.position.y + paddleRadius && rightPaddle.transform.position.y > -paddleAbsMaxPosY && velocityX > 0) {
            rightPaddleMovingUp = false;
            rightPaddleMovingDown = true;
            rightPaddle.transform.position = new Vector3(PADDLES_ABS_X_POS, rightPaddle.transform.position.y - paddleSpeed * Time.deltaTime, PADDLES_Z_POSITION);
        }
        else {
            rightPaddleMovingUp = false;
            rightPaddleMovingDown = false;
        }
        // Bounce off ceiling or floor:
        if ((transform.position.y >= ballMaxYPos && velocityY > 0) || (transform.position.y <= -ballMaxYPos && velocityY < 0)) velocityY *= -1;
        // Bounce off left paddle:
        else if ((Mathf.Abs(transform.position.x + ballAbsXPosWhenBouncePaddle) <= -velocityX * Time.deltaTime) && Mathf.Abs(transform.position.y - leftPaddle.transform.position.y) <= paddleRadius) {
            velocityX *= -1;
            if (leftPaddleMovingUp) velocityY += 0.5f * velocityX;
            else if (leftPaddleMovingDown) velocityY -= 0.5f * velocityX;
        }
        // Bounce off right paddle:
        else if (Mathf.Abs(transform.position.x - ballAbsXPosWhenBouncePaddle) <= velocityX * Time.deltaTime && Mathf.Abs(transform.position.y - rightPaddle.transform.position.y) <= paddleRadius) {
            velocityX *= -1;
            if (rightPaddleMovingUp) velocityY += 0.5f * velocityX;
            else if (rightPaddleMovingDown) velocityY -= 0.5f * velocityX;
        }
        // Point scored:
        else if (Mathf.Abs(transform.position.x) >= ballMaxXPos) {
            // Player scored:
            if (transform.position.x > 0) {
                playerScore++;
                // Immediately update score visual:
                playerScoreVisual.GetComponent<TMPro.TextMeshProUGUI>().text = playerScore.ToString();
            }
            // Computer scored:
            else {
                computerScore++;
                // Immediately update score visual:
                computerScoreVisual.GetComponent<TMPro.TextMeshProUGUI>().text = computerScore.ToString();
            }
            // Continue game:
            if (playerScore < SCORE_TO_WIN && computerScore < SCORE_TO_WIN) SceneManager.LoadScene(1);
            // End game:
            else {
                if (playerScore == 7) playerWins = true;
                else playerWins = false;
                playerScore = 0;
                computerScore = 0;
                SceneManager.LoadScene(2);
            }
        }
    }
    
}
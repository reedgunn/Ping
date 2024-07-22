using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1 : MonoBehaviour {
    
    public GameObject leftPaddle;
    public GameObject rightPaddle;
    public GameObject playerScoreVisual;
    public GameObject computerScoreVisual;
    const int SCORE_TO_WIN = 7;
    const float PADDLE_SCALE_X = 0.2f;
    const float ABS_MAX_X_POS = 8f;
    const float ABS_MAX_Y_POS = 5f;
    const float DIST_ORIGIN_TO_PADDLE = 6.9f;
    const float PADDLES_ABS_X_POS = 7f;
    const float MAX_INITIAL_BALL_ANGLE = Mathf.PI/8f;
    public static int playerScore;
    public static int computerScore;
    float ballVelocityX;
    float ballVelocityY;
    float ballRadius;
    float ballAbsMaxXPos;
    float ballAbsMaxYPos;
    float ballInitialAngle;
    float paddleSpeed;
    float paddleRadius;
    float paddleAbsMaxPosY;
    float ballAbsXPosWhenBouncePaddle;
    bool roundLive;
    bool leftPaddleMovingUp;
    bool leftPaddleMovingDown;
    bool rightPaddleMovingUp;
    bool rightPaddleMovingDown;

    void Start() {
        roundLive = false;
        playerScore = 0;
        computerScore = 0;
        transform.localScale = new Vector2(Scene0.ballDiameter, Scene0.ballDiameter);
        leftPaddle.transform.localScale = new Vector2(PADDLE_SCALE_X, Scene0.paddleScaleY);
        rightPaddle.transform.localScale = new Vector2(PADDLE_SCALE_X, Scene0.paddleScaleY);
        paddleSpeed = Mathf.Sqrt(3f)/2f * Scene0.ballSpeedX;
        // Useful values to precalculate:
        paddleRadius = Scene0.paddleScaleY / 2;
        ballRadius = Scene0.ballDiameter / 2;
        paddleAbsMaxPosY = ABS_MAX_Y_POS - paddleRadius - ballRadius;
        ballAbsMaxXPos = ABS_MAX_X_POS - ballRadius;
        ballAbsMaxYPos = ABS_MAX_Y_POS - ballRadius;
        ballAbsXPosWhenBouncePaddle = DIST_ORIGIN_TO_PADDLE - ballRadius;
        Reset();
    }

    void Update() {
        if (roundLive) {
            // Ball movement:
            transform.position = new Vector2(transform.position.x + ballVelocityX * Time.deltaTime, transform.position.y + ballVelocityY * Time.deltaTime);
            // Left paddle movement:
            if (Input.GetKey("w") && leftPaddle.transform.position.y < paddleAbsMaxPosY) {
                leftPaddle.transform.position = new Vector2(-PADDLES_ABS_X_POS, leftPaddle.transform.position.y + paddleSpeed * Time.deltaTime);
                leftPaddleMovingUp = true;
                leftPaddleMovingDown = false;
            } else if (Input.GetKey("s") && leftPaddle.transform.position.y > -paddleAbsMaxPosY) {
                leftPaddle.transform.position = new Vector2(-PADDLES_ABS_X_POS, leftPaddle.transform.position.y - paddleSpeed * Time.deltaTime);
                leftPaddleMovingUp = false;
                leftPaddleMovingDown = true;
            } else {
                leftPaddleMovingUp = false;
                leftPaddleMovingDown = false;
            }
            // Right paddle movement:
            if (ballVelocityX > 0) {
                if (transform.position.y > rightPaddle.transform.position.y && rightPaddle.transform.position.y < paddleAbsMaxPosY) {
                    rightPaddle.transform.position = new Vector2(PADDLES_ABS_X_POS, rightPaddle.transform.position.y + paddleSpeed * Time.deltaTime);
                    rightPaddleMovingUp = true;
                    rightPaddleMovingDown = false;
                } else if (transform.position.y < rightPaddle.transform.position.y && rightPaddle.transform.position.y > -paddleAbsMaxPosY) {
                    rightPaddle.transform.position = new Vector2(PADDLES_ABS_X_POS, rightPaddle.transform.position.y - paddleSpeed * Time.deltaTime);
                    rightPaddleMovingUp = false;
                    rightPaddleMovingDown = true;
                }
            } else {
                rightPaddleMovingUp = false;
                rightPaddleMovingDown = false;
            }
            if ((transform.position.y >= ballAbsMaxYPos && ballVelocityY > 0) || (transform.position.y <= -ballAbsMaxYPos && ballVelocityY < 0)) {
                ballVelocityY *= -1;
            } else if (Mathf.Abs(transform.position.x + ballAbsXPosWhenBouncePaddle) < Scene0.ballSpeedX * Time.deltaTime && Mathf.Abs(transform.position.y - leftPaddle.transform.position.y) <= paddleRadius && ballVelocityX < 0) {
                if (leftPaddleMovingUp) {
                    ballVelocityY += paddleSpeed / 5f;
                } else if (leftPaddleMovingDown) {
                    ballVelocityY -= paddleSpeed / 5f;
                }
                ballVelocityX *= -1;
            } else if (Mathf.Abs(transform.position.x - ballAbsXPosWhenBouncePaddle) < Scene0.ballSpeedX * Time.deltaTime && Mathf.Abs(transform.position.y - rightPaddle.transform.position.y) <= paddleRadius && ballVelocityX > 0) {
                if (rightPaddleMovingUp) {
                    ballVelocityY += paddleSpeed / 5f;
                } else if (rightPaddleMovingDown) {
                    ballVelocityY -= paddleSpeed / 5f;
                }
                ballVelocityX *= -1;
            } else if (Mathf.Abs(transform.position.x) >= ballAbsMaxXPos) {
                roundLive = false;
                if (transform.position.x < 0) {
                    computerScore++;
                    computerScoreVisual.GetComponent<TMPro.TextMeshProUGUI>().text = computerScore.ToString();
                    if (computerScore == SCORE_TO_WIN) {
                        SceneManager.LoadScene(2);
                    } else {
                        Reset();
                    }
                } else {
                    playerScore++;
                    playerScoreVisual.GetComponent<TMPro.TextMeshProUGUI>().text = playerScore.ToString();
                    if (playerScore == SCORE_TO_WIN) {
                        SceneManager.LoadScene(2);
                    } else {
                        Reset();
                    }
                }
            }
        }
    }

    void Reset() {
        transform.position = new Vector2(0, 0);
        leftPaddle.transform.position = new Vector2(-PADDLES_ABS_X_POS, 0);
        rightPaddle.transform.position = new Vector2(PADDLES_ABS_X_POS, 0);
        ballInitialAngle = Random.Range(-MAX_INITIAL_BALL_ANGLE, MAX_INITIAL_BALL_ANGLE);
        ballVelocityY = Mathf.Tan(ballInitialAngle) * Scene0.ballSpeedX;
        ballVelocityX = Scene0.ballSpeedX;
        if (Random.value > 0.5f) {
            ballVelocityX *= -1;
        }
        Invoke("StartRound", 1.5f);
    }
    
    void StartRound() {
        roundLive = true;
    }
    
}
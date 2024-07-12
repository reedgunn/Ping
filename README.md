# Ping

Replica of the classic video game Pong. Made with Unity. The first scene allows the user to select from three difficulty levels (Easy, Medium, and Hard), each of which entail a different paddle size, ball size, and horizontal ball speed. The second scene features the game, where the user controls the paddle on the left using the 'w' key to move up and the 's' key to move down. The paddle on the right (opponent) is controlled by a script running in the project. Due to the potentially unlimited skill of the right paddle player (computer), the script controlling the movement of the right paddle is modified such that the human user can reasonably compete. There exists a simulated physical component of friction between the paddles and the ball which affects the trajectory of the ball after it collides with a moving paddle (colliding with a upward-moving paddle increases the vertical component of the velocity of the ball, and vise versa, each by a factor of the horizontal speed of the ball. Each side's scores are displayed and updated smoothly throughout gameplay. Whichever player reaches seven points first wins, at which point the user is sent to the third and final scene of the game, which tells them who won and gives them the option to go back to the first scene. You can play the game on my website at https://reedgunn.github.io/.

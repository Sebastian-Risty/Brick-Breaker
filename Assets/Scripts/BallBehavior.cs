using UnityEngine;
using System;

public class BallBehavior : MonoBehaviour
{
    public Rigidbody2D rbBall;
    public Transform tPaddle;
    public Transform tBall;
    public GameObject paddle;



    void Start()
    {
        LaunchBall();
    }

    void Update()
    {
        if (!GameData.BallIsMoving) {
            rbBall.velocity = new Vector2(0f, 0f);
            //follow paddle powerup
            if (GameData.FollowPaddle) {
                tBall.position = new Vector2(tPaddle.position.x, tPaddle.position.y + 2);
            }
            else {
                tBall.position = new Vector2(GameData.BallStartX, GameData.BallStartY);
            }
            LaunchBall();
        }

    }

    void LaunchBall() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (GameData.InverseLaunch) {
                rbBall.AddForce(Vector2.up * GameData.LaunchSpeed);
            }
            else {
                rbBall.AddForce(Vector2.down * GameData.LaunchSpeed);
            }
            GameData.BallIsMoving = true;
        }
    }
    //detect ball going out of bounds or if ball has no collide power up
    void OnTriggerEnter2D(Collider2D Object) {
        if (Object.CompareTag("bottom")) {
            GameData.Life -= 1;
            GameData.BallIsMoving = false;
        }
    }

    public void ManageBallSpeed(float brickLevel) {
        //increase ball speed slightly for every brick broken (need to find good maximum to keep game possible, yet challenging)
        Vector2 tmp = rbBall.velocity;
        float dynamicSpeed = GameData.SpeedIncrease;
        if (brickLevel != 0f) {
            dynamicSpeed = (GameData.SpeedIncrease + (brickLevel * GameData.BrickStrengthMultiplier));
        }
        
        if (tmp.x > 0) {
            tmp.x += dynamicSpeed;
        }
        else {
            tmp.x -= dynamicSpeed;
        }
        if (tmp.y > 0) {
            tmp.y += dynamicSpeed;
        }
        else {
            tmp.y -= dynamicSpeed;
        }
        rbBall.velocity = tmp;
    }
}

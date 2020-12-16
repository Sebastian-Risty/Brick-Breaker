using UnityEngine;
using System.Collections;

public class BrickBehavior : MonoBehaviour
{
    public GameManager gameManager;
    public BallBehavior ballBehavior;
    public ParticleSystem brokenBrick;
    //scale level with stronger bricks
    void Start()
    {
        GameData.LevelUpChance += GameData.LevelUpChanceIncrease * (GameData.Level - 1);
        ScaleLevel();
    }
    //scale levels
    public void ScaleLevel() {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("brick");
        //make some bricks take multiple hits
        foreach (GameObject brick in bricks) {
            //make sure every brick is at least lvl 1
            brick.transform.position = new Vector3(brick.transform.position.x, brick.transform.position.y, brick.transform.position.z + 1f);
            //upgrade bricks based on chance
            float originalLevelUpChance = GameData.LevelUpChance;
            while (Random.Range(0f, 1f) <= GameData.LevelUpChance) {
                if(GameData.LevelUpChance == 0) {
                    break;
                }
                else {
                    brick.transform.position = new Vector3(brick.transform.position.x, brick.transform.position.y, brick.transform.position.z + 1f);
                }
                if(GameData.LevelUpChance >= 1) {
                    GameData.LevelUpChance -= 1f;
                }
                else {
                    GameData.LevelUpChance /= 4f; //make this an adjustable var 
                }
            }
            GameData.BrickStrengthLevel.Add(brick.transform.position.z);
            GameData.LevelUpChance = originalLevelUpChance;
        }
        //generate colors
        foreach(float brickStrength in GameData.BrickStrengthLevel) {
            if(brickStrength > GameData.StrongestBrick) {
                GameData.StrongestBrick = brickStrength;
            }
        }
        gameManager.GenerateColor(GameData.StrongestBrick);

        //apply color to leveled bricks
        foreach (GameObject brick in bricks) {
            string currentBrick = brick.name.Substring(brick.name.IndexOf("(") + 1, brick.name.IndexOf(")") - brick.name.IndexOf("(") - 1);
            gameManager.ChangeBrickColor(brick.transform.position.z, currentBrick);
        }

    }
    //manage brick damage
    void OnCollisionEnter2D(Collision2D Object) {
        //explosive powerup
        if(Object.transform.CompareTag("brick") && GameData.Explosive) { //add checks for if brick to the left or right is on opposite side of screen
            //13
            string currentBrick = Object.transform.name.Substring(Object.transform.name.IndexOf("(") + 1, Object.transform.name.IndexOf(")") - Object.transform.name.IndexOf("(") - 1);
            float currentBrickNumber = float.Parse(currentBrick);
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("brick");
            foreach (GameObject brick in bricks) {
                //above
                if(brick.name == "Brick (" + (currentBrickNumber + 13) + ")") {
                    currentBrick = brick.name.Substring(brick.name.IndexOf("(") + 1, brick.name.IndexOf(")") - brick.name.IndexOf("(") - 1);
                    brokenBrick.transform.position = new Vector2(brick.transform.position.x, brick.transform.position.y);
                    if (brick.transform.position.z > 0) {
                        gameManager.ChangeParticleColor(brick.transform.position.z, currentBrick);
                        brokenBrick.Emit(GameData.BrickHitParticleCount); //change how these particles shoot out for this hit type
                        brick.transform.position = new Vector3(brick.transform.position.x, brick.transform.position.y, brick.transform.position.z - 1f);
                        gameManager.ChangeBrickColor(brick.transform.position.z, currentBrick);
                    }
                    else {
                        gameManager.ChangeParticleColor(0f, currentBrick);
                        brokenBrick.Emit(GameData.BrickBreakParticleCount); //change how these particles shoot out for this hit type
                        ballBehavior.ManageBallSpeed(float.Parse(currentBrick));
                        GameData.Score += 100; //rework score system
                        GameData.BrickCount++;
                        Destroy(brick.gameObject);
                    }
                }
                //below
                if (brick.name == "Brick (" + (currentBrickNumber - 13) + ")") {
                    currentBrick = brick.name.Substring(brick.name.IndexOf("(") + 1, brick.name.IndexOf(")") - brick.name.IndexOf("(") - 1);
                    brokenBrick.transform.position = new Vector2(brick.transform.position.x, brick.transform.position.y);
                    if (brick.transform.position.z > 0) {
                        gameManager.ChangeParticleColor(brick.transform.position.z, currentBrick);
                        brokenBrick.Emit(GameData.BrickHitParticleCount); //change how these particles shoot out for this hit type
                        brick.transform.position = new Vector3(brick.transform.position.x, brick.transform.position.y, brick.transform.position.z - 1f);
                        gameManager.ChangeBrickColor(brick.transform.position.z, currentBrick);
                    }
                    else {
                        gameManager.ChangeParticleColor(0f, currentBrick);
                        brokenBrick.Emit(GameData.BrickBreakParticleCount); //change how these particles shoot out for this hit type
                        ballBehavior.ManageBallSpeed(float.Parse(currentBrick));
                        GameData.Score += 100; //rework score system
                        GameData.BrickCount++;
                        Destroy(brick.gameObject);
                    }
                }
                //right
                if (Object.transform.name != "Brick (12)" && Object.transform.name != "Brick (25)" && Object.transform.name != "Brick (38)" && Object.transform.name != "Brick (51)" && Object.transform.name != "Brick (64)" && Object.transform.name != "Brick (77)" && Object.transform.name != "Brick (90)") {
                    if (brick.name == "Brick (" + (currentBrickNumber + 1) + ")") {
                        currentBrick = brick.name.Substring(brick.name.IndexOf("(") + 1, brick.name.IndexOf(")") - brick.name.IndexOf("(") - 1);
                        brokenBrick.transform.position = new Vector2(brick.transform.position.x, brick.transform.position.y);
                        if (brick.transform.position.z > 0) {
                            gameManager.ChangeParticleColor(brick.transform.position.z, currentBrick);
                            brokenBrick.Emit(GameData.BrickHitParticleCount); //change how these particles shoot out for this hit type
                            brick.transform.position = new Vector3(brick.transform.position.x, brick.transform.position.y, brick.transform.position.z - 1f);
                            gameManager.ChangeBrickColor(brick.transform.position.z, currentBrick);
                        }
                        else {
                            gameManager.ChangeParticleColor(0f, currentBrick);
                            brokenBrick.Emit(GameData.BrickBreakParticleCount); //change how these particles shoot out for this hit type
                            ballBehavior.ManageBallSpeed(float.Parse(currentBrick));
                            GameData.Score += 100; //rework score system
                            GameData.BrickCount++;
                            Destroy(brick.gameObject);
                        }
                    }
                }
                //left
                if (Object.transform.name != "Brick (0)" && Object.transform.name != "Brick (13)" && Object.transform.name != "Brick (26)" && Object.transform.name != "Brick (39)" && Object.transform.name != "Brick (52)" && Object.transform.name != "Brick (65)" && Object.transform.name != "Brick (78)") {
                    if (brick.name == "Brick (" + (currentBrickNumber - 1) + ")") {
                        currentBrick = brick.name.Substring(brick.name.IndexOf("(") + 1, brick.name.IndexOf(")") - brick.name.IndexOf("(") - 1);
                        brokenBrick.transform.position = new Vector2(brick.transform.position.x, brick.transform.position.y);
                        if (brick.transform.position.z > 0) {
                            gameManager.ChangeParticleColor(brick.transform.position.z, currentBrick);
                            brokenBrick.Emit(GameData.BrickHitParticleCount); //change how these particles shoot out for this hit type
                            brick.transform.position = new Vector3(brick.transform.position.x, brick.transform.position.y, brick.transform.position.z - 1f);
                            gameManager.ChangeBrickColor(brick.transform.position.z, currentBrick);
                        }
                        else {
                            gameManager.ChangeParticleColor(0f, currentBrick);
                            brokenBrick.Emit(GameData.BrickBreakParticleCount); //change how these particles shoot out for this hit type
                            ballBehavior.ManageBallSpeed(float.Parse(currentBrick));
                            GameData.Score += 100; //rework score system
                            GameData.BrickCount++;
                            Destroy(brick.gameObject);
                        }
                    }
                }
            }
            currentBrick = Object.transform.name.Substring(Object.transform.name.IndexOf("(") + 1, Object.transform.name.IndexOf(")") - Object.transform.name.IndexOf("(") - 1);
            if (Object.transform.position.z > 0) {
                gameManager.ChangeParticleColor(Object.transform.position.z, currentBrick);
            }
            else {
                gameManager.ChangeParticleColor(0f, currentBrick);
            }
            brokenBrick.Emit(GameData.BrickBreakParticleCount); //change particles for this type of hit
            ballBehavior.ManageBallSpeed(float.Parse(currentBrick));
            GameData.Score += 100; //rework score system
            GameData.BrickCount++;
            Destroy(Object.gameObject);
        }

        //normal brick damage default
        else if (Object.transform.CompareTag("brick")) {
            if (Object.transform.position.z > 0) {
                string currentBrick = Object.transform.name.Substring(Object.transform.name.IndexOf("(") + 1, Object.transform.name.IndexOf(")") - Object.transform.name.IndexOf("(") - 1);
                foreach(ContactPoint2D brickHit in Object.contacts) {
                    Vector2 hitPos = brickHit.point;
                    brokenBrick.transform.position = new Vector2(hitPos.x, hitPos.y);
                }
                gameManager.ChangeParticleColor(Object.transform.position.z, currentBrick);
                brokenBrick.Emit(GameData.BrickHitParticleCount);
                Object.transform.position = new Vector3(Object.transform.position.x, Object.transform.position.y, Object.transform.position.z - 1f);
                gameManager.ChangeBrickColor(Object.transform.position.z, currentBrick);
            }
            else {
                string currentBrick = Object.transform.name.Substring(Object.transform.name.IndexOf("(") + 1, Object.transform.name.IndexOf(")") - Object.transform.name.IndexOf("(") - 1);
                foreach (ContactPoint2D brickHit in Object.contacts) {
                    Vector2 hitPos = brickHit.point;
                    brokenBrick.transform.position = new Vector2(hitPos.x, hitPos.y);
                }
                gameManager.ChangeParticleColor(0f, currentBrick);
                brokenBrick.Emit(GameData.BrickBreakParticleCount);
                ballBehavior.ManageBallSpeed(float.Parse(currentBrick));
                GameData.Score += 100; //rework score system
                GameData.BrickCount++;
                Destroy(Object.gameObject);
            }
        }
    }

    //no collide powerup
    void OnTriggerEnter2D(Collider2D Object) {
        if (Object.CompareTag("brick")) {
            string currentBrick = Object.transform.name.Substring(Object.transform.name.IndexOf("(") + 1, Object.transform.name.IndexOf(")") - Object.transform.name.IndexOf("(") - 1);
            brokenBrick.transform.position = new Vector2(Object.transform.position.x, Object.transform.position.y);
            //makes sure that the list isnt called with nil index
            if(Object.transform.position.z > 0) {
                gameManager.ChangeParticleColor(Object.transform.position.z, currentBrick);
            }
            else {
                gameManager.ChangeParticleColor(0f, currentBrick);
            }
            brokenBrick.Emit(GameData.BrickBreakParticleCount); //change particles for this type of hit
            ballBehavior.ManageBallSpeed(float.Parse(currentBrick));
            GameData.Score += 100; //rework score system
            GameData.BrickCount++;
            Destroy(Object.gameObject);
        }

    }

}

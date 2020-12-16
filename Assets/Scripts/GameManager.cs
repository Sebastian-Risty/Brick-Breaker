using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject tLife;
    public GameObject tScore;
    public GameObject tStatus;
    public GameObject tLevel;
    public ParticleSystem brokenBrick;
    Color brickColor;

    public Sprite sprite;
    void Start()
    {
        //always reset brickcount each level
        GameData.BrickCount = 0;
    }

    void Update()
    {
        //constantly update life and score gui
        tLife.GetComponent<Text>().text = GameData.Life.ToString();
        tScore.GetComponent<Text>().text = GameData.Score.ToString();
        tLevel.GetComponent<Text>().text = GameData.Level.ToString();
        //freezes ball, resets score + life values, resets scene    *** make sure to reset any vals for loss here***
        if (GameData.Life <= 0) {
            tStatus.GetComponent<Text>().text = "You lose!";
            GameData.BallIsMoving = false;
            GameData.Score = 0;
            GameData.Life = 3;
            GameData.Level = 1;
            StartCoroutine(SetGameStatus(2f, -1));
            //GameData.ColorList.Clear();
        }

        //checks to see if all bricks are broken, there are 91 per level then freezes ball, resets scene, all values are kept in GameData.cs
        //91
        if (GameData.BrickCount >= 91) {
            tStatus.GetComponent<Text>().text = "Level Clear!";
            GameData.BallIsMoving = false;  //instead of teleporting the ball, see if we can set scene in slow motion then fade out then into new level??
            //GameData.ColorList.Clear();
            StartCoroutine(SetGameStatus(1f, 1));
            //GameData.ColorList.Clear();
            //GameData.Level += 1;
            
        }
    }

    //resets the scene
    IEnumerator SetGameStatus(float pauseTime, int status) {
        yield return new WaitForSeconds(pauseTime);
        //only run once after pause time depending on status
        if(status == 1) {
            GameData.Level += 1;
        }
        GameData.LevelUpChance = GameData.DefaultLevelUpChance;
        SceneManager.LoadScene("Brick Break");
        GameData.ColorList.Clear();
        GameData.BrickStrengthLevel.Clear();
    }

    //changes brick color and brick particle color
    public void ChangeBrickColor(float z = 0f, string brickID = "-1") {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("brick");
        foreach (GameObject brick in bricks) {
            string currentBrick = brick.name.Substring(brick.name.IndexOf("(") + 1, brick.name.IndexOf(")") - brick.name.IndexOf("(") - 1);
            if (brickID == "-1") {
                brick.GetComponent<SpriteRenderer>().color = GameData.ColorList[(int)z];
            }
            else if (currentBrick == brickID) {
                brick.GetComponent<SpriteRenderer>().color = GameData.ColorList[(int)z];
            }
        }
    } 

    public void ChangeParticleColor(float z = 0f, string brickID = "-1") {
        ParticleSystem ps = brokenBrick.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule psmain = ps.main;
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("brick");
        foreach (GameObject brick in bricks) {
            string currentBrick = brick.name.Substring(brick.name.IndexOf("(") + 1, brick.name.IndexOf(")") - brick.name.IndexOf("(") - 1);
            if (currentBrick == brickID) {
                psmain.startColor = GameData.ColorList[(int)z];
            }
        }
    }

    public void GenerateColor(float highestBrick) { //2 -> 3
        for(int i = 0; i <= highestBrick; i++) {
            brickColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            GameData.ColorList.Add(brickColor);
        }
    }

}




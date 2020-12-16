using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersManager : MonoBehaviour
{
    void Update()
    {

        //brick mod
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("brick");
        foreach (GameObject brick in bricks) {
            //no collide
            if (GameData.NoCollide) {
                brick.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else {
                brick.GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }

        //super secret dev mode
        if (GameData.DevMode) {
            GameData.LeftEdge = -7.1f;
            GameData.RightEdge = 7.1f;
            GameData.MaxHeight = 4.7f;
            GameData.MinHeight = -4.7f;
            if (Input.GetKeyDown(KeyCode.R)) {
                GameData.BallIsMoving = false;
            }
            if (Input.GetKeyDown(KeyCode.T)) {
                if (GameData.InverseLaunch) {
                    Debug.Log("Flipped launch direction. Direction: DOWN");
                }
                else {
                    Debug.Log("Flipped launch direction. Direction: UP");
                }
                GameData.InverseLaunch = !GameData.InverseLaunch;
            }
            //add inf health
        }
        else {
            GameData.LeftEdge = -5.49f;
            GameData.RightEdge = 5.49f;
            GameData.MaxHeight = -2.5f;
            GameData.MinHeight = -4f;
            GameData.InverseLaunch = false;
        }
    }
}

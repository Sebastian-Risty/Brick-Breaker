using System.Collections.Generic;
using UnityEngine;

public static class GameData {
    //for inspector
    public const bool IsReady = true;
    //GUI
    public static int Score { get; set; } = 0;
    public static int Life { get; set; } = 3;
    public static int BrickCount { get; set; } = 0;
    public static int Level { get; set; } = 1;

    //////ball control
    public static bool BallIsMoving { get; set; } = false;
    public static float BallStartX { get; set; } = 0f;
    public static float BallStartY { get; set; } = -2f;
    public static float LaunchSpeed { get; set; } = 2500;
    public static float SpeedIncrease { get; set; } = 0.001f; //play test + modify rate based on remaining bricks after life loss to ~= same amount
    public static float BrickStrengthMultiplier { get; set; } = 0.0005f; //play test
    public static bool InverseLaunch { get; set; } = false;
    //////

    //////particles
    public static int BrickBreakParticleCount { get; set; } = 30;
    public static int BrickHitParticleCount { get; set; } = 5;
    public static int ExplosiveParticleCount { get; set; } = 60;
    //////

    //////paddle control
    public static float PaddleSpeed { get; set; } = 7f;
    public static float LeftEdge { get; set; } = -5.49f;
    public static float RightEdge { get; set; } = 5.49f;
    public static float MaxHeight { get; set; } = -2.5f;
    public static float MinHeight { get; set; } = -4f;
    //////

    //////level difficulty scaling
    public static float DefaultLevelUpChance { get; set; } = 10.6f; //play test
    public static float LevelUpChance { get; set; } = 10.6f;
    public static float LevelUpChanceIncrease { get; set; } = 0.4f; //play test
    public static float StrongestBrick { get; set; } = -10f;
    public static List<Color> ColorList { get; set; } = new List<Color>();
    public static List<float> BrickStrengthLevel { get; set; } = new List<float>();
    //////

    //////powerups and downs
    public static bool NoCollide { get; set; } = false;
    public static bool Explosive { get; set; } = false;
    public static bool FollowPaddle { get; set; } = true;
    public static bool DevMode { get; set; } = true;
    //////
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataInspector : MonoBehaviour
{
    public bool IsReady;
    public ParticleSystem brokenBrick;

    //public init for inspector
    #region GUI
    public int Score;
    public int Life;
    public int BrickCount;
    public int Level;
    #endregion

    #region ball control
    public float BallStartX;
    public float BallStartY;
    public float LaunchSpeed;
    public float SpeedIncrease;
    public float BrickStrengthMultiplier;
    #endregion

    #region particles
    public int BrickBreakParticleCount;
    public int BrickHitParticleCount;
    public int ExplosiveParticleCount;
    #endregion

    #region paddle control
    public float PaddleSpeed;
    #endregion

    #region level difficulty scaling
    public float DefaultLevelUpChance;
    public float LevelUpChance;
    public float LevelUpChanceIncrease;
    public List<Color> ColorList;
    public List<float> BrickStrengthLevel;
    #endregion

    #region powerups and downs
    public bool NoCollide;
    public bool Explosive;
    public bool FollowPaddle;
    public bool DevMode;
    #endregion

    void Update()
    {
        //update inspector vars with GameData

        IsReady = GameData.IsReady;

        #region GUI
        Score = GameData.Score;
        Life = GameData.Life;
        BrickCount = GameData.BrickCount;
        Level = GameData.Level;
        #endregion 

        #region ball control
        BallStartX = GameData.BallStartX;
        BallStartY = GameData.BallStartY;
        LaunchSpeed = GameData.LaunchSpeed;
        SpeedIncrease = GameData.SpeedIncrease;
        BrickStrengthMultiplier = GameData.BrickStrengthMultiplier;
        #endregion

        #region particles
        BrickBreakParticleCount = GameData.BrickBreakParticleCount;
        BrickHitParticleCount = GameData.BrickHitParticleCount;
        ExplosiveParticleCount = GameData.ExplosiveParticleCount;
        #endregion

        #region paddle control
        PaddleSpeed = GameData.PaddleSpeed;
        #endregion

        #region level difficulty scaling
        DefaultLevelUpChance = GameData.DefaultLevelUpChance;
        LevelUpChance = GameData.LevelUpChance;
        LevelUpChanceIncrease = GameData.LevelUpChanceIncrease;
        ColorList = GameData.ColorList;
        BrickStrengthLevel = GameData.BrickStrengthLevel;
        #endregion

        #region powerups and downs
        NoCollide = GameData.NoCollide;
        Explosive = GameData.Explosive;
        FollowPaddle = GameData.FollowPaddle;
        DevMode = GameData.DevMode;
        #endregion
    }

    private void OnValidate() {
        //update GameData when values changed in inspector
        if (IsReady) {
            //manually brick colors
            ParticleSystem ps = brokenBrick.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule psmain = ps.main;
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("brick");
            foreach (GameObject brick in bricks) {
                brick.GetComponent<SpriteRenderer>().color = GameData.ColorList[(int)brick.transform.position.z];
                psmain.startColor = GameData.ColorList[(int)brick.transform.position.z];
            }


            #region GUI
            GameData.Score = Score;
            GameData.Life = Life;
            GameData.BrickCount = BrickCount;
            GameData.Level = Level;
            #endregion

            #region ball control
            GameData.BallStartX = BallStartX;
            GameData.BallStartY = BallStartY;
            GameData.LaunchSpeed = LaunchSpeed;
            GameData.SpeedIncrease = SpeedIncrease;
            GameData.BrickStrengthMultiplier = BrickStrengthMultiplier;
            #endregion

            #region particles
            GameData.BrickBreakParticleCount = BrickBreakParticleCount;
            GameData.BrickHitParticleCount = BrickHitParticleCount;
            GameData.ExplosiveParticleCount = ExplosiveParticleCount;
            #endregion

            #region paddle control
            GameData.PaddleSpeed = PaddleSpeed;
            #endregion

            #region level difficulty scaling
            GameData.DefaultLevelUpChance = DefaultLevelUpChance; //use private var to simplify
            GameData.LevelUpChance = LevelUpChance;
            GameData.LevelUpChanceIncrease = LevelUpChanceIncrease;
            GameData.BrickStrengthLevel = BrickStrengthLevel;
            GameData.ColorList = ColorList;
            #endregion

            //add keybind vars for powerups
            #region powerups and downs
            GameData.NoCollide = NoCollide;
            GameData.Explosive = Explosive;
            GameData.FollowPaddle = FollowPaddle;
            GameData.DevMode = DevMode;
            #endregion
        }
    }
}

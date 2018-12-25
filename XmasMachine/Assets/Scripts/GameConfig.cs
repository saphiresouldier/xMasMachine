using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    [Header("Camera")]
    public float cameraMovementSpeed = 1.0f;

    [Header("Player")]
     public float playerScaleDownFactor = 1.0f;

    [Header("Background")]
    public float backgroundWidth = 30.0f;
    public float backgroundScrollSpeed = 1.0f;

    [Header("Background Music")]
    public AudioClip intro;
    public AudioClip level;
    public AudioClip selector;

    [Header("Word Behaviour")]
    public int stringLength = 6; 

    [Header("Game Over")]
    public float gameOverDuration = 3;

}

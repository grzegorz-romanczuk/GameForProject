using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int health;
    public int cashValue;
    public int spawnValue;
    public int damage;
    public int score;

    private void Awake()
    {
        var diff = GameDifficulty.getDifficulty();
        health *= diff;
        score *= diff;
        damage *= diff;        
    }
}

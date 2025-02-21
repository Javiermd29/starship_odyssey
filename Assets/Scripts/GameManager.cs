using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] private GameObject enemyPrefab;

    // This bool is used by the "Enemy" script to know if a powerup has been dropped. if it has been dropped, no enemy will drop more powerups
    public bool hasDroppedPowerUpOnce = false;

    // This bool is used in the "Weapon" script to know if player has the grabbed the powerup
    public bool hasPowerUp;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one instance");
        }
        Instance = this;
    }



}

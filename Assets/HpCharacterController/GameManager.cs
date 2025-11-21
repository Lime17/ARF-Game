using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    //--Variables-- 
    public float maxHP = 100f;

    public float player2maxHP = 100f;
    public float playerHP;
    public float player2HP;
    public Player1Identify playerID;

  private void Awake()
{
    if (Instance != null  && Instance != this){
        Destroy(gameObject);
        return;
    }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //Set Hp to the Max at the start
        playerHP = maxHP;
        player2HP = player2maxHP;
}
    //Add health from anywhere in the scene
    public void AddHP(float hpToAdd)
    {
        if(hpToAdd == 0) return;
        playerHP += hpToAdd;
        if (playerHP < 0) playerHP = 0;
        if (playerHP > maxHP) playerHP = maxHP;
        if (player2HP < 0) player2HP = 0;
        if (player2HP > player2maxHP) player2HP = player2maxHP;
    }

    //remove health from anywhere in the scene
    public void RemoveHP(float hpToRemove)
    {
        if (hpToRemove == 0) return;
        if(playerID.playerNumber == 1){ 
       playerHP -= hpToRemove;
        } else if (playerID.playerNumber == 2)
        {
            player2HP -= hpToRemove;
        }
        if (playerHP < 0) playerHP = 0; // if player energy is less than 0  reset it to 0
        if (playerHP > maxHP) playerHP = maxHP; // prevents player HP from going above max
        if (player2HP < 0) player2HP = 0; // if player energy is less than 0  reset it to 0
        if (player2HP > player2maxHP) player2HP = player2maxHP; // prevents player HP from going above max
    }
}

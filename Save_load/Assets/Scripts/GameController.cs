using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameController : MonoBehaviour
{
    public static GameController control;



    public int attack;
    public int defense;
    public int health;




    private void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
            try
            {
                LoadGame();


            }
            catch
            {
                print("Unable to load game. Game file might not exist.");
                SetDefaultValue();
            }

        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }


    private void SetDefaultValue()
    {
        attack = 5;
        defense = 2;
        health = 10;
    }
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 56;
        print(attack.ToString());
        GUI.Label(new Rect(10, 160, 100, 30), "Attack : " + attack.ToString(), style);
        GUI.Label(new Rect(10, 110, 100, 30), "Defense : " + defense.ToString(), style);
        GUI.Label(new Rect(10, 60, 100, 30), "Health : " + health.ToString(), style);

    }


    public void IncreaseAttack()
    {
        attack += 1;
    }


    public void IncreaseDefense()
    {
        defense += 1;
    }


    public void IncreaseHealth()
    {
        health += 10;
    }


    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Create);
        PlayerData playerData = new PlayerData();
        playerData.attack = attack;
        playerData.defense = defense;
        playerData.health = health;


        bf.Serialize(file, playerData);
        file.Close();
    }


    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {
            throw new Exception("Game file not existing");
        }




        FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
        PlayerData playerData = (PlayerData)bf.Deserialize(file);
        file.Close();
        attack = playerData.attack;
        defense = playerData.defense;
        health = playerData.health;

    }



}
[Serializable]
class PlayerData
{
    public int attack;
    public int defense;
    public int health;
}
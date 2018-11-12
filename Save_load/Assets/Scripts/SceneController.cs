using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SceneController : MonoBehaviour {

    public static SceneController sceneControl;

    public float health;
    public float experience;
    public int scene;

    private void Awake() {
        if(sceneControl == null) {
            DontDestroyOnLoad(gameObject);
            sceneControl = this;
            LoadScene();
        } else if(sceneControl != this) {
            Destroy(gameObject);
        }
    }

    public void NextScene() {
        if ((SceneManager.sceneCountInBuildSettings-1) > SceneManager.GetActiveScene().buildIndex) {
            print("loading " + (SceneManager.GetActiveScene().buildIndex + 1));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
            
        } else {
            print("This is the last scene");
        }
        
    }

    public void PreviousScene() {
        if (SceneManager.GetActiveScene().buildIndex != 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            print("loading " + (SceneManager.GetActiveScene().buildIndex -1 ));
        } else {
            print("This is the first scene");
        }
        
    }
    private void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.fontSize = 56;
        GUI.Label(new Rect(10, 10, 180, 80), "Active scene index : "  + SceneManager.GetActiveScene().buildIndex, style);
    }

    public void SaveScene()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Open(Application.persistentDataPath + "/sceneInfo.dat", FileMode.Create);
        SaveData sceneData = new SaveData();
        //.sceneSave = scene;
        sceneData.sceneSave = SceneManager.GetActiveScene().buildIndex;



        bf.Serialize(file, sceneData);
        file.Close();
    }


    public void LoadScene()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.persistentDataPath + "/sceneInfo.dat"))
        {
            throw new Exception("Game file not existing");
        }




        FileStream file = File.Open(Application.persistentDataPath + "/sceneInfo.dat", FileMode.Open);
        SaveData sceneData = (SaveData)bf.Deserialize(file);
        file.Close();
        //scene = sceneData.sceneSave;
        SceneManager.LoadScene(sceneData.sceneSave);
       


    }



}
[Serializable]
class SaveData
{
    public int sceneSave;

}


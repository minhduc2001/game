using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveController
{ 
    public static void SaveGameObject(SaveModel saveModel)
    {
        BinaryFormatter binary = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        Debug.Log(path);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        binary.Serialize(fileStream, saveModel);
        fileStream.Close();
    }

    public static SaveModel LoadSaveGame()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            SaveModel save = (SaveModel) binary.Deserialize(fileStream);

            fileStream.Close();
            return save;
        }

        return null;
    }
}
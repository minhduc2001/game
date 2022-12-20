using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveModel
{
    private Dictionary<string, int> inventory;
    private float[] position = new float[3];
    private int level = 0;

    public void setInventory(Dictionary<string, int> data)
    {
        inventory = data;
    }

    public void setPosition(Vector3 vector3)
    {
        position[0] = vector3.x;
        position[1] = vector3.y;
        position[2] = vector3.z;
    }

    public Dictionary<string, int> getInventory()
    {
        return inventory;
    }

    public float[] getPosition()
    {
        return position;
    }

    public void setLevel(int index)
    {
        level = index;
    }

    public int getLevel()
    {
        return level;
    }


}
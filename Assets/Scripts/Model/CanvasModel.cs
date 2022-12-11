using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasModel : MonoBehaviour
{
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        inventory.Add("keyBlue", 0);
        inventory.Add("keyRed", 0);
        inventory.Add("keyYellow", 0);
        inventory.Add("keyGreen", 0);
        inventory.Add("coinGold", 0);
        inventory.Add("gemBlue", 0);
        inventory.Add("heart", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Dictionary<string, int> GetInventory(){ return inventory;}

    public void SetInventory(Dictionary<string, int> newInventory){ inventory = newInventory;}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button btn_Remove;
    public Button btn_Upgrade;
    private GameManager gm => GameManager.Instance;
    
    void Start() {
        Debug.Log("Good");
        btn_Remove.onClick.AddListener(() => Remove());
        btn_Upgrade.onClick.AddListener(() => Upgrade());
    }

    void Remove() {
        GameObject Tower = gm.Selected();
        gm.CreateTB(Tower);
        Destroy(Tower);
        gm.NoneTargeting();
    }
    void Upgrade() {
        GameObject Tower = gm.Selected();
        if (Tower.GetComponent<Tower>().nowUp < 10) {
            Tower.GetComponent<Tower>().dmg += 5;
            Tower.GetComponent<Tower>().nowUp += 1;
        }
        gm.Status();
    }
}

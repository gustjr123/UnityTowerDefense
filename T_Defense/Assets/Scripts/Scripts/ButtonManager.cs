using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button btn_Remove;
    private GameManager gm => GameManager.Instance;
    
    void Start() {
        Debug.Log("Good");
        btn_Remove.onClick.AddListener(() => Remove());
    }

    void Remove() {
        GameObject Tower = gm.Selected();
        gm.CreateTB(Tower);
        Destroy(Tower);
        gm.NoneTargeting();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{

    // Mouse Input 
    public Ray ray;
    public RaycastHit hit;
    public GameObject TowerPrefab;
    private GameManager gm => GameManager.Instance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // OnClick to create tower
    void Update()
    {
        if (Input.GetMouseButton(0))  // 게임 화면에 마우스 좌클릭 하면
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // RayCast 검출 지정
            LayerMask mask = LayerMask.GetMask("BaseTower") | LayerMask.GetMask("Coin") | LayerMask.GetMask("Wave");
            if (Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                // Debug.Log("Click : " + hit.collider.gameObject.name);
                GameObject obj = hit.transform.gameObject;

                // 타워 생성 버튼 (TowerBase를 누를경우)
                if (obj.CompareTag("TowerBase")) {
                    gm.CreateTower(obj);
                    gm.NoneTargeting();
                }
                // Coin 클릭시 Gold 상승
                if (obj.CompareTag("Coin")) {
                    gm.GetGold(obj);
                    gm.NoneTargeting();
                }
                // wave 시작버튼
                if (obj.CompareTag("WaveStart")) {
                    gm.WaveStart(obj);
                    gm.NoneTargeting();
                }
                // Tower Click
                if (obj.CompareTag("Tower")) {
                    gm.Targeting(obj);
                }
                
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("參考物件")]
    public Camera PlayerCamera;
    public Transform attackPoint;

    [Header("子彈預置物件")]
    public float pushForce = 50.0f;
    public GameObject bullet;

    private void Update()
    {
        // 判斷有沒有按下左鍵
        if (Input.GetMouseButtonDown(0) == true)
        {
            Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));  // 從攝影機射出一條射線
            RaycastHit hit;  // 宣告一個射擊點
            Vector3 targetPoint;  // 宣告一個位置點變數，到時候如果有打到東西，就存到這個變數

            // 如果射線有打到具備碰撞體的物件
            if (Physics.Raycast(ray, out hit) == true)
                targetPoint = hit.point;         // 將打到物件的位置點存進 targetPoint
            else
                targetPoint = ray.GetPoint(75);  // 如果沒有打到物件，就以長度75的末端點取得一個點，存進 targetPoint

            Debug.DrawRay(ray.origin, targetPoint - ray.origin, Color.red, 10); // 在測試階段將射線設定為紅色線條，來看看線條長度夠不夠？

            Vector3 directionWithoutSpread = targetPoint - attackPoint.position; // 計算攻擊點與射線終點的方向
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); // 生成子彈物件
            currentBullet.transform.forward = directionWithoutSpread.normalized; // 把子彈轉向，讓子彈方向與飛行方向相同

            // 推動子彈飛行
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * pushForce, ForceMode.Impulse);
        }
    }
}

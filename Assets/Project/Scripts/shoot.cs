using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Photon.Pun;

public class shoot : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject cameraobj;
    [SerializeField] VisualEffect fire;
    [SerializeField] weaponflame weaponflame;
    [SerializeField] GameObject cube;
    int playernum;
    void Start()
    {
        cube = GameObject.Find("Cube");
        if (photonView.IsMine)
        {
            for (int i = 0; i < weaponflame.Equipment.Count; i++)
            {
                weaponflame.Equipment[i].init();
            }
            fire.Stop();
            playernum = photonView.OwnerActorNr;
        }
    }
    private void Update()
    {
        //攻撃系の入力を扱う
        if (photonView.IsMine)
        {
            //クリックを扱う武器種の処理
            if (Input.GetMouseButton(0))
            {
                //ホーミング武器
                if (weaponflame.Equipment[weaponflame.choiceWeapon].Homing == true)
                {
                    Debug.Log("call2");
                    LockOnTarget();
                }
                //直進武器
                else
                {
                    StraightShot();
                }

            }
            //クリックを離した時を扱う武器種の処理
            else if (Input.GetMouseButtonUp(0))
            {
                //ホーミング武器
                if (weaponflame.Equipment[weaponflame.choiceWeapon].Homing == true)
                {
                    HomingShot();
                }
            }

            //マウスホイール入力による武器種変更
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                weaponflame.weaponchange(1);
                Debug.Log("down");
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                weaponflame.weaponchange(-1);
                Debug.Log("up");
            }
        }
    }

    //カメラの中心の座標を構えている武器の弾に渡す
    void StraightShot()
    {
        //直線射撃時の処理
        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(center);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity/*,layerMask*/);
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 5, false);
        //Debug.Log(hit.collider.gameObject);

        //プレイヤーからRayの当たった座標へのVector3を作成
        Vector3 playerVec = this.transform.position;
        Vector3 targetVec = hit.point;
        Vector3 bulletVec = targetVec - playerVec;
        if (weaponflame.Equipment[weaponflame.choiceWeapon].Shot() == null) { return; }
        //RPCを用いてプレイヤー間で発射タイミングと方向を同期する
        photonView.RPC(nameof(shotRPC), RpcTarget.All, bulletVec, playernum, weaponflame.choiceWeapon);
    }

    //射撃同期の関数
    [PunRPC]
    private void shotRPC(Vector3 target, int playernumber, int weaponID)
    {
        //弾の発射処理
        var bulletobj = Instantiate(weaponflame.Equipment[weaponID].bullet, this.transform);
        bulletobj.GetComponent<bulletMove>().SetTargetPosition(target, playernumber, weaponID);
        //bulletobj.SetActive(true);
        bulletobj.transform.Rotate(new Vector3(1, 0, 0), 90);//TODOあんまうまくいってない気がする
        bulletobj.transform.parent = null;
        fire.SendEvent("OnPlay");

    }

    //カメラの中心に最も近く、サークル範囲内のオブジェクトのplayernumを渡す
    void LockOnTarget()
    {
        GameObject[] tmp2 = GameObject.FindGameObjectsWithTag("Player");
        RaycastHit Hit;
        Vector3 TargetPos;
        weapon weapon = weaponflame.Equipment[weaponflame.choiceWeapon];
        float objdistance = 30000;
        Transform tmptarget = null;
        Debug.Log("call");
        //ロックオン完了時はなにもしない
        if (weapon.readyNextBullet) { return; }

        //すでにロックオン中
        if (weapon.lockOnTarget != null)
        {
            //レイを飛ばす
            if (Physics.Raycast(Camera.main.transform.position, weapon.lockOnTarget.position - Camera.main.transform.position, out Hit, (weapon.lockOnTarget.position - Camera.main.transform.position).magnitude, LayerMask.GetMask("Field")) == false)
            {
                //サークル内か判定
                TargetPos = RectTransformUtility.WorldToScreenPoint(Camera.main, weapon.lockOnTarget.position);
                TargetPos.x = TargetPos.x - (Screen.width / 2);
                TargetPos.y = TargetPos.y - (Screen.height / 2);

                //サークル内なら処理を継続。サークル外ならキャンセル
                if (TargetPos.magnitude < 300)
                {
                    weaponflame.Equipment[weaponflame.choiceWeapon].Targetting(weapon.lockOnTarget);
                    Debug.Log("Targetting...");
                    return;
                }
                else
                {
                    weaponflame.Equipment[weaponflame.choiceWeapon].Targetting();
                }
            }

        }

        //ロックオン中ではない時、新しいロックオンターゲットを探す
        //障害物がなく、中心に最も近いプレイヤータグのついたオブジェクトを求める
        for (int i = 0; i < tmp2.Length; i++)
        {
            //遮蔽がないか判定
            if (Physics.Raycast(Camera.main.transform.position, tmp2[i].transform.position - Camera.main.transform.position, out Hit, (tmp2[i].transform.position - Camera.main.transform.position).magnitude, LayerMask.GetMask("Field")) == false)
            {
                //自分ではないことを確認
                if (tmp2[i].GetComponent<PlayerStatus>().playernum == this.playernum) { }
                //カメラよりも前方にあることを確認
                else if (Camera.main.transform.InverseTransformPoint(tmp2[i].transform.position).z < 0) { }
                //中心からの距離を求める
                else
                {
                    TargetPos = RectTransformUtility.WorldToScreenPoint(Camera.main, tmp2[i].transform.position);
                    TargetPos.x = TargetPos.x - (Screen.width / 2);
                    TargetPos.y = TargetPos.y - (Screen.height / 2);
                    //最も中心に近いオブジェクトを保持
                    if (TargetPos.magnitude < objdistance)
                    {
                        objdistance = TargetPos.magnitude;
                        tmptarget = tmp2[i].transform;
                    }
                }
            }
        }
        //ターゲットと画面の中心の距離が一定以下であればロックオン
        if (objdistance < 300)
        {
            weaponflame.Equipment[weaponflame.choiceWeapon].Targetting(tmptarget);
            Debug.Log("Targetting...");
            return;
        }
        //存在しなければロックオンすることはない
        else
        {
            weaponflame.Equipment[weaponflame.choiceWeapon].Targetting();
        }
    }

    void HomingShot()
    {
        //ホーミング弾を撃てるなら発射
        if (weaponflame.Equipment[weaponflame.choiceWeapon].readyNextBullet == true)
        {
            var targetnum = weaponflame.Equipment[weaponflame.choiceWeapon].lockOnTarget.gameObject.GetComponent<PlayerStatus>().playernum;
            Debug.Log("HomingShot target is " + targetnum);
            photonView.RPC(nameof(HomingshotRPC), RpcTarget.All, targetnum, playernum, weaponflame.choiceWeapon);
            weaponflame.Equipment[weaponflame.choiceWeapon].readyNextBullet = false;
        }
        //撃てなければロックオンを解除
        weaponflame.Equipment[weaponflame.choiceWeapon].StopTargeting();
    }


    [PunRPC]
    private void HomingshotRPC(int targetnumber, int playernumber, int weaponID)
    {
        //弾の発射処理
        var bulletobj = Instantiate(weaponflame.Equipment[weaponID].bullet, this.transform);
        bulletobj.transform.parent = null;
        fire.SendEvent("OnPlay");
        Debug.Log("call homingshot");
        bulletobj.GetComponent<MissileMove>().SetTargetPosition(targetnumber, playernumber, weaponID);
    }
}

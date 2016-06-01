using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BombMgr
{
    static public List<Bomb> bombs = new List<Bomb>();
    static public float TileSize = 1.2f; //<타일크기 가져오기
    static public void CheckColiider(Bomb bomb)
    {
        if (bombs == null
            || bomb == null)
            return;

        int Count = bombs.Count;

        for (int i = 0; i < Count; ++i)
        {
            if (i >= bombs.Count)
                break;

            Vector2 BombLT = new Vector2(bomb.CenterPos.x - (bomb.BombSize * TileSize), bomb.CenterPos.y + (TileSize / 2));
            Vector2 BombRB = new Vector2(bomb.CenterPos.x + (bomb.BombSize * TileSize), bomb.CenterPos.y - (TileSize / 2));

            if (BombLT.x < bombs[i].CenterPos.x
                && BombRB.x > bombs[i].CenterPos.x
                && BombLT.y > bombs[i].CenterPos.y
                && BombRB.y < bombs[i].CenterPos.y)
            {
                bombs[i].Burst();
                //Debug.LogError("!!!!!!");
            }
        }

        for (int i = 0; i < Count; ++i)
        {
            if (i <= bombs.Count)
                break;

            Vector2 BombLT = new Vector2(bomb.CenterPos.x - (TileSize / 2), bomb.CenterPos.y + (bomb.BombSize * TileSize));
            Vector2 BombRB = new Vector2(bomb.CenterPos.x + (TileSize / 2), bomb.CenterPos.y - (bomb.BombSize * TileSize));

            if (BombLT.x < bombs[i].CenterPos.x
                && BombRB.x > bombs[i].CenterPos.x
                && BombLT.y > bombs[i].CenterPos.y
                && BombRB.y < bombs[i].CenterPos.y)
            {
                bombs[i].Burst();
            }
        }
    }
}

public class Bomb : MonoBehaviour
{
    public List<GameObject> images = new List<GameObject>();
    public List<GameObject> centerImages = new List<GameObject>();
    public GameObject CenterImage = null;
    public FirstBomb _FirstBomb = new FirstBomb();
    public Vector3 CenterPos;
    public Quaternion Quat;
    public int BombSize = 0;
    private bool IsBurst;

    //클래스 직속의 속성을 사용 하기 위해서 필요.
    [System.Serializable]
    public class FirstBomb
    {
        /// <물풍선 화력 이미지와 충돌체크를 위한 변수>.
        public Vector3 GetCenterBubblePosition;
        public Vector3 GetUpBubblePosition;
        public Vector3 GetDownBubblePosition;
        public Vector3 GetRightBubblePosition;
        public Vector3 GetLeftBubblePosition;
        public GameObject BombImage;
        public GameObject BomUp_2;
        /// </summary>
    }

    public void CreateBomb(Vector3 pos, Quaternion quat, int bombSize)
    {
        this.gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z);
        CenterPos = pos;
        Quat = quat;
        IsBurst = false;
        StartCoroutine(BusrTimer());
        BombSize = bombSize;
        CenterImage = (GameObject)Instantiate(Resources.Load("FirstPlayerBubble"), this.transform.position, this.transform.rotation);


        BombMgr.bombs.Add(this);
    }

    public IEnumerator BusrTimer()
    {
        yield return new WaitForSeconds(3.0f);
        Burst();
    }

    //public void ShowWaterTail()
    //{

    //}

    public void Burst()
    {
        if (IsBurst == true)
            return;

        IsBurst = true;
        //물풍선의 중심을 기준으로 퍼져나갈 물줄기를 위하여 받아온다.
        _FirstBomb.GetCenterBubblePosition = _FirstBomb.GetUpBubblePosition = _FirstBomb.GetDownBubblePosition =
                _FirstBomb.GetRightBubblePosition = _FirstBomb.GetLeftBubblePosition = CenterPos;

        GameObject bombCenter = (GameObject)GameObject.Instantiate(Resources.Load("bombwater_center1"), _FirstBomb.GetCenterBubblePosition, Quat);
        GameObject.Destroy(bombCenter, 0.3f);


        for (int i = 1; i <= BombSize; ++i)
        {
            _FirstBomb.GetCenterBubblePosition = _FirstBomb.GetUpBubblePosition = _FirstBomb.GetDownBubblePosition =
             _FirstBomb.GetRightBubblePosition = _FirstBomb.GetLeftBubblePosition = CenterPos;

            _FirstBomb.GetDownBubblePosition.y -= ( i * 0.6f);
            _FirstBomb.GetUpBubblePosition.y += (i * 0.6f);
            //_FirstBomb.GetRightBubblePosition.x += 0.6f;
            //_FirstBomb.GetLeftBubblePosition.x -= 0.6f;
            GameObject bombDown = (GameObject)GameObject.Instantiate(Resources.Load("bombwater_down"), _FirstBomb.GetDownBubblePosition, Quat);
            GameObject bombUp = (GameObject)GameObject.Instantiate(Resources.Load("bombwater_up"), _FirstBomb.GetUpBubblePosition, Quat);
            //GameObject bombRight = (GameObject)GameObject.Instantiate(Resources.Load("bombwater_right"), _FirstBomb.GetRightBubblePosition, Quat);
            //GameObject bombLeft = (GameObject)GameObject.Instantiate(Resources.Load("bombwater_left"), _FirstBomb.GetLeftBubblePosition, Quat);
            
            GameObject.Destroy(bombDown, 0.6f);
            GameObject.Destroy(bombUp, 0.6f);
            //GameObject.Destroy(bombRight, 0.6f);
            //GameObject.Destroy(bombLeft, 0.6f);
            _FirstBomb.GetDownBubblePosition.y = 0.0f;
            _FirstBomb.GetUpBubblePosition.y = 0.0f;

            if (BombSize == i)
            {
                _FirstBomb.GetCenterBubblePosition = _FirstBomb.GetUpBubblePosition = _FirstBomb.GetDownBubblePosition =
           _FirstBomb.GetRightBubblePosition = _FirstBomb.GetLeftBubblePosition = CenterPos;

                _FirstBomb.GetDownBubblePosition.y -= (i * 0.6f);
                _FirstBomb.GetUpBubblePosition.y += (i * 0.6f);
                //_FirstBomb.GetRightBubblePosition.x += 0.6f;
                //_FirstBomb.GetLeftBubblePosition.x -= 0.6f;
                bombDown = (GameObject)GameObject.Instantiate(Resources.Load("bombwater_downend"), _FirstBomb.GetDownBubblePosition, Quat);
                bombUp = (GameObject)GameObject.Instantiate(Resources.Load("bombwater_upend"), _FirstBomb.GetUpBubblePosition, Quat);
                //GameObject bombRight = (GameObject)GameObject.Instantiate(Resources.Load("bombwater_rightend"), _FirstBomb.GetRightBubblePosition, Quat);
                //GameObject bombLeft = (GameObject)GameObject.Instantiate(Resources.Load("bombwater_leftend"), _FirstBomb.GetLeftBubblePosition, Quat);

                GameObject.Destroy(bombDown, 0.6f);
                GameObject.Destroy(bombUp, 0.6f);
                //GameObject.Destroy(bombRight, 0.6f);
                //GameObject.Destroy(bombLeft, 0.6f);
            }
        }




        for (int i = 1; i <= BombSize; ++i)
        {

            _FirstBomb.GetCenterBubblePosition = _FirstBomb.GetUpBubblePosition = _FirstBomb.GetDownBubblePosition =
           _FirstBomb.GetRightBubblePosition = _FirstBomb.GetLeftBubblePosition = CenterPos;

            _FirstBomb.GetRightBubblePosition.x +=  (i * 0.6f);
            _FirstBomb.GetLeftBubblePosition.x -= (i * 0.6f);
            
            GameObject bombRight = (GameObject)GameObject.Instantiate(Resources.Load("bombwater_right"), _FirstBomb.GetRightBubblePosition, Quat);
            GameObject bombLeft = (GameObject)GameObject.Instantiate(Resources.Load("bombwater_left"), _FirstBomb.GetLeftBubblePosition, Quat);
            
            GameObject.Destroy(bombRight, 0.6f);
            GameObject.Destroy(bombLeft, 0.6f);

            _FirstBomb.GetDownBubblePosition.x = 0.0f;
            _FirstBomb.GetUpBubblePosition.x = 0.0f;

            if (BombSize == i)
            {
                _FirstBomb.GetCenterBubblePosition = _FirstBomb.GetUpBubblePosition = _FirstBomb.GetDownBubblePosition =
           _FirstBomb.GetRightBubblePosition = _FirstBomb.GetLeftBubblePosition = CenterPos;

                _FirstBomb.GetRightBubblePosition.x += (i * 0.6f);
                _FirstBomb.GetLeftBubblePosition.x -= (i * 0.6f);
                bombRight = (GameObject)GameObject.Instantiate(Resources.Load("bombwater_rightend"), _FirstBomb.GetRightBubblePosition, Quat);
                bombLeft = (GameObject)GameObject.Instantiate(Resources.Load("bombwater_leftend"), _FirstBomb.GetLeftBubblePosition, Quat);

                GameObject.Destroy(bombRight, 0.6f);
                GameObject.Destroy(bombLeft, 0.6f);
            }
        }


        BombMgr.bombs.Remove(this);
        BombMgr.CheckColiider(this);
        FirstPlayerInfo.iSetBuubleCount -= 1;
        Destroy(CenterImage);
    }
}

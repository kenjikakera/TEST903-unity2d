using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    
    public VariableJoystick variableJoystick;
    public float vJoy = 50;
    public float rJoy = 70;
    public Rigidbody2D rd;
    public GameObject[] button;
    public GameObject goText;
    public Sprite[] buttunImage;
    public GameObject[] myItem;
    public Sprite [] myItemImage;
    public ParticleSystem[] particle;
    public GameObject goFloor;

    public Vector2 my;
    public Vector2 item;

    private Vector2 direction;
    private float posX = -4.6f;
    private float posY = -3.8f;
    private const int posZ = 110;

    private GameObject goGM;
    private GameManager scGM;
    private CreateMaze scCM;
    private GameObject goSM;
    private SoundMgr scSM;
    private GameObject boxGO;
    private GameObject enemyGO;


    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos3D = new Vector3(posX,posY,posZ);
        pos3D.z = posZ;
        this.transform.position = pos3D;

        goGM = GameObject.Find("GameManager");
        scGM = goGM.GetComponent<GameManager>();
        scCM = goGM.GetComponent<CreateMaze>();
        goSM = GameObject.Find("SoundManager");
        scSM = goSM.GetComponent<SoundMgr>();
        enemyGO = null;

        variableJoystick.SetMode(JoystickType.Fixed);
        variableJoystick.AxisOptions = AxisOptions.Both;
        variableJoystick.SnapX = false;
        variableJoystick.SnapY = false;
        rd.freezeRotation = true;

        scCM.makeMaze(0);

        setMyItemImage(0, 0);
        setMyItemImage(1, 1);
        scGM.attack = 100;
        scGM.deffence = 100;

        setButtonImage(0, 0);
        setButtonImage(1, 0);

        Text text = goText.GetComponent<Text>();
        text.text = "";
        scGM.heal = 0;

    }

    // Update is called once per frame
    void Update()
    {
                // joystick packの仮想joystick
                direction = variableJoystick.Direction;
                Vector2 pos = this.transform.position;
                pos += direction/vJoy;
                this.transform.position = pos;
                Vector3 pos3D = this.transform.position;
                pos3D.z = posZ;
                this.transform.position = pos3D;

                // xinputの物理joystick ( inputマネージャーで、typeをJoystick Axisに設定してあります。
                pos = this.transform.position;
                pos.x += Input.GetAxis("Horizontal")/rJoy;
                pos.y += Input.GetAxis("Vertical")/rJoy;
                this.transform.position = pos;
                pos3D = this.transform.position;
                pos3D.z = posZ;
                this.transform.position = pos3D;
        /*


        // joystick packの仮想joystick ( 4方向限定 ) 
        direction = variableJoystick.Direction;
        Vector2 pos = this.transform.position;
        if(Mathf.Abs(direction.x)> Mathf.Abs(direction.y))
        {
            direction.y = 0;
        } else
        {
            direction.x = 0;
        }
        pos += direction / vJoy;
        this.transform.position = pos;
        Vector3 pos3D = this.transform.position;
        pos3D.z = posZ;
        this.transform.position = pos3D;

        // xinputの物理joystick ( inputマネージャーで、typeをJoystick Axisに設定してあります。
        pos = this.transform.position;
        vi = Input.GetAxis("Vertical") / rJoy;
        hi = Input.GetAxis("Horizontal") / rJoy;
        if (Mathf.Abs(hi) > Mathf.Abs(vi))
        {
            if (Mathf.Abs(hi) > 0.005) pos.x += hi;
        } else {
            if (Mathf.Abs(vi) > 0.005) pos.y += vi;

        }
        this.transform.position = pos;
        pos3D = this.transform.position;
        pos3D.z = posZ;
        this.transform.position = pos3D;
        */
    }


    // パーティカルの再生と破壊
    void partialSub(int nPT,float destroyTume)
    {
        Vector3 pos3D = transform.position;
        pos3D.z = 90;
        ParticleSystem pt = Instantiate(particle[nPT], pos3D, transform.rotation);
        Destroy(pt, destroyTume);
    }

    void healSub()
    {
        partialSub(0, 3.0f);
        scSM.PlaySE(2);
        scGM.life += (scGM.maxLife * 0.2f);
        if (scGM.life > scGM.maxLife) scGM.life = scGM.maxLife;
    }

<<<<<<< HEAD
=======


>>>>>>> d19bcfa56a38796ca7f2cdfd72dd1b793264b7eb
    public void actionButton()
    {
        Image bimage = button[0].GetComponent<Image>();
        if (bimage.sprite == buttunImage[1])
        {
            healSub();
        }
        else if (bimage.sprite == buttunImage[2])
        {
            scCM.DestroyMaze();
            scGM.floor++;
            scSM.PlaySE(3);
            scSM.PlayBGM(true, true, scGM.floor+1);
            scCM.makeMaze(scGM.floor);
        }
        else if (bimage.sprite == buttunImage[3])
        {
            scCM.DestroyMaze();
            scGM.floor--;
            scSM.PlaySE(3);
            scSM.PlayBGM(true, true, scGM.floor+1);
            scCM.makeMaze(scGM.floor);
        }
        else if (bimage.sprite == buttunImage[4])
        {
            if (boxGO != null)
            {
                mazeItem scMI = boxGO.GetComponent<mazeItem>();
                scSM.PlaySE(4);
                scCM.maze[scMI.x,scMI.y,scMI.floor] = 6;
                switch (scMI.itemNum)
                {
                    // 肉
                    case 0:
                        scGM.heal++;
                        Vector2 pos2D = transform.position;
                        my = pos2D;
                        setButtonImage(1, 5);
                        Text text = goText.GetComponent<Text>();
                        text.text = scGM.heal.ToString();
                        break;
                    // ロングソード
                    case 1:
                        partialSub(1, 3.0f);
                        setMyItemImage(0,2);
                        scGM.attack = 125;
                        break;
                    // シルバーアーマー
                    case 2:
                        partialSub(1, 3.0f);
                        setMyItemImage(1, 3);
                        scGM.deffence = 125;
                        break;
                    case 3:
                        partialSub(1, 3.0f);
                        setMyItemImage(0, 4);
                        scGM.attack = 150;
                        break;
                    case 4:
                        partialSub(1, 3.0f);
                        setMyItemImage(1, 5);
                        scGM.deffence = 150;
                        break;
                }
                Instantiate(scCM.mazeItem[6], scMI.pos3D, Quaternion.identity, scCM.goFloor.transform);
                Destroy(boxGO);
            }
        }
        else
        {
            scSM.PlaySE(1);
        }

    }

    public void useButton()
    {
        if (scGM.heal > 0)
        {
            healSub();
            Text text;
            scGM.heal--;
            if (scGM.heal == 0)
            {
                setButtonImage(1, 0);
                text = goText.GetComponent<Text>();
                text.text = "";
            } else
            {
                text = goText.GetComponent<Text>();
                text.text = scGM.heal.ToString();
            }
        }
        else
        {
            scSM.PlaySE(1);
        }
    }

       void OnTriggerEnter2D(Collider2D coll)
       {
           if (coll.tag == "enemy" && enemyGO == null)
           {
                enemyGO = coll.gameObject;
                StartCoroutine("BattleMain");
            }
        }

    //
    void setImageSub(GameObject go,Sprite sprite)
    {
        Image bimage = go.GetComponent<Image>();
        bimage.sprite = sprite;
    }

    // ボタンにイメージを設定する
    void setButtonImage(int nButton,int nImage)
    {
        setImageSub(button[nButton], buttunImage[nImage]);
    }

    // アイテムにイメージを設定する
    void setMyItemImage(int num, int nImage)
    {
        setImageSub(myItem[num], myItemImage[nImage]);
    }


    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "rest")
        {

            boxGO = null;
            Vector2 pos2D = transform.position;
            my = pos2D;
            item = coll.transform.position;
            if (coll.transform.position.x-0.63f/4 < pos2D.x && coll.transform.position.x + 0.63f/4 > pos2D.x && coll.transform.position.y - 0.63f/4 < pos2D.y && coll.transform.position.y + 0.63f/4 > pos2D.y)
            {
                setButtonImage(0, 1);
            } else
            {
                setButtonImage(0, 0);
            }
        }
        if (coll.tag == "floordown")
        {

            boxGO = null;
            Vector2 pos2D = transform.position;
            my = pos2D;
            item = coll.transform.position;
            if (coll.transform.position.x - 0.63f / 4 < pos2D.x && coll.transform.position.x + 0.63f / 4 > pos2D.x && coll.transform.position.y - 0.63f / 4 < pos2D.y && coll.transform.position.y + 0.63f / 4 > pos2D.y)
            {
                setButtonImage(0, 2);
            }
            else
            {
                setButtonImage(0, 0);
            }
        }
        if (coll.tag == "floorup")
        {

            boxGO = null;
            Vector2 pos2D = transform.position;
            my = pos2D;
            item = coll.transform.position;
            if (coll.transform.position.x - 0.63f / 4 < pos2D.x && coll.transform.position.x + 0.63f / 4 > pos2D.x && coll.transform.position.y - 0.63f / 4 < pos2D.y && coll.transform.position.y + 0.63f / 4 > pos2D.y)
            {
                setButtonImage(0, 3);
            }
            else
            {
                setButtonImage(0, 0);
            }
        }
        if (coll.tag == "box")
        {
            boxGO = coll.gameObject;
            Vector2 pos2D = transform.position;
            my = pos2D;
            item = coll.transform.position;
            if (coll.transform.position.x - 0.63f / 4 < pos2D.x && coll.transform.position.x + 0.63f / 4 > pos2D.x && coll.transform.position.y - 0.63f / 4 < pos2D.y && coll.transform.position.y + 0.63f / 4 > pos2D.y)
            {
                setButtonImage(0, 4);
            }
            else
            {
                setButtonImage(0, 0);
            }
        }
        if (coll.tag == "enemy" && enemyGO == null)
        {
            enemyGO = coll.gameObject;
            StartCoroutine("BattleMain");
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        boxGO = null;
        enemyGO = null;
        setButtonImage(0, 0);
    }


    IEnumerator BattleMain()
    {
        Vector3 pos3D;
        ParticleSystem pt;
        Enemy scEY;
        float tmp;
        while ( true) {
            // こちらの攻撃
            if (enemyGO == null) yield break;
            pos3D = transform.position;
            pos3D.z = 90;
            scSM.PlaySE(5);
            pt = Instantiate(particle[2], pos3D, transform.rotation);
            scEY = enemyGO.GetComponent<Enemy>();
            tmp = scGM.attack * scGM.attackExp - scEY.deffence;
            if (tmp < 40.0f) tmp = 40.0f;
            scEY.life -= tmp * Random.Range(0, 0.5f);
            if( scEY.life<=0 )
            {
                scGM.life += scEY.lifeAdd;
                scGM.maxLife += scEY.lifeAdd;
                scGM.attackExp *= scEY.attackExp;
                scGM.deffenceExp *= scEY.deffenceExp;
                scGM.score += scEY.score;
                Destroy(enemyGO);
                enemyGO = null;
                if (scEY.type == 3)
                {
                    SceneManager.LoadScene(4);
                }
                yield break;
            }
            yield return new WaitForSeconds(0.3f);

            // 敵の攻撃
            if (enemyGO == null) yield break;
            pos3D = transform.position;
            pos3D.z = 90;
            pt = Instantiate(particle[3], pos3D, transform.rotation);
            scSM.PlaySE(6);
            scEY = enemyGO.GetComponent<Enemy>();
            tmp = scEY.attack - scGM.deffence * scGM.deffenceExp;
            if (tmp < 40.0f) tmp = 40.0f;
            scGM.life -= tmp * Random.Range(0, 0.5f);
            if (scGM.life <= 0)
            {
                SceneManager.LoadScene(3);
            }

            yield return new WaitForSeconds(0.3f);
        }
    }
}

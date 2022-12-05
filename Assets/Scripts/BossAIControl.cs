using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossAIControl : MonoBehaviour
{
    public static BossAIControl instance;
    public List<GameObject> Paths = new List<GameObject>();
    public List<GameObject> Souls = new List<GameObject>();
    public List<GameObject> LColors = new List<GameObject>();
    public List<GameObject> RColors = new List<GameObject>();
    public List<GameObject> Gates = new List<GameObject>();
    public bool Patrol = false;
    public bool Pattern1Set = false;
    public bool Pattern2Set = false;
    public bool Pattern3Set = false;
    public bool Pattern1Hide = false;
    public bool Dash = false;
    public bool Damage = false;
    public float Chasing = 8f;
    public float AttackDistance = 5f;
    public float RoSpeed = 55f;
    public float Speed = 1.25f;
    public float Accel = 10f;
    public float PDelay = 10f;
    public int PColor = 0;
    public GameObject Player;
    public GameObject Wall;
    public GameObject Notice;
    public Text NoticeText;
    public Vector3 PlayerPos;
    private bool AttackType = false;
    private bool RandomCheck = false;
    private bool PatCheck = false;
    private float PatrolDelay;
    private float PatternDelay;
    private float TextDelay;
    private int RandomVal;
    private int Number;
    private int LCNumber;
    private int RCNumber;
    NavMeshAgent BossAI;
    Animator Anime;
    Animator PAnime;
    Rigidbody PRigid;
    BossUIControl BossUI;
    PlayerUIControl PlayerUI;
    Vector3 OldBossAngle;

    void Start()
    {
        PlayerUI = GameObject.Find("PlayerStatus").GetComponent<PlayerUIControl>();
        BossUI = GameObject.Find("BossStatus").GetComponent<BossUIControl>();
        BossAI = GetComponent<NavMeshAgent>();
        Anime = GetComponent<Animator>();
        PAnime = GameObject.Find("Player").GetComponent<Animator>();
        PRigid = GameObject.Find("Player").GetComponent<Rigidbody>();
        BossAI.speed = Speed;
        OldBossAngle = transform.rotation.eulerAngles;
        PatrolDelay = 0f;
        PatternDelay = 0f;
        TextDelay = 0f;
        Number = Random.Range(0, Paths.Count);
        Wall.SetActive(false);
        Notice.SetActive(false);
        for (int i = 0; i < Souls.Count; ++i)
            Souls[i].SetActive(false);
        for (int i = 0; i < LColors.Count; ++i)
            LColors[i].SetActive(false);
        for (int i = 0; i < RColors.Count; ++i)
            RColors[i].SetActive(false);
        LCNumber = Random.Range(0, LColors.Count);
        RCNumber = Random.Range(0, RColors.Count);
    }

    void Update()
    {
        if (PlayerUI.Dead || BossUI.Dead)
            return;
        if (OldBossAngle == Player.transform.rotation.eulerAngles)
            Anime.SetBool("IsWalking", false);
        if (BossUI.PhaseVar == "1" || BossUI.PhaseVar == "2" || BossUI.PhaseVar == "3")
        {
            BossAI.speed = Speed;
            BossAI.acceleration = Accel;
        }
        if (BossUI.PhaseVar == "2" || BossUI.PhaseVar == "3")
        {
            if (Dash)
                BossAI.speed = Speed * 5f;
            else
            {
                BossAI.speed = Speed;
                BossAI.acceleration = Accel;
            }
        }
        if (BossUI.PhaseVar == "F")
        {
            if (Dash)
                BossAI.speed = Speed * 5f;
            else
            {
                BossAI.speed = Speed * 2f;
                BossAI.acceleration = Accel * 1.5f;
            }
        }
        if (BossUI.PhaseVar == "B")
        {
            if (Dash)
                BossAI.speed = Speed * 5f;
            else
            {
                BossAI.speed = Speed * 2.5f;
                BossAI.acceleration = Accel * 2f;
            }
        }
    }

    void FixedUpdate()
    {
        if (PlayerUI.Dead || BossUI.Dead)
        {
            BossAI.SetDestination(transform.position);
            return;
        }
        if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Groggy"))
        {
            BossAI.SetDestination(transform.position);
            return;
        }
        if (Damage && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
        {
            Damage = false;
            Debug.Log("'Boss' 피해 판정");
            return;
        }
        if (Dash)
            return;
        if (Pattern1Set)
        {
            Pattern1();
            return;
        }
        AnimationLoad();
        RotTransform();
        if (Pattern2Set)
        {
            Pattern2();
            return;
        }
        if (Pattern3Set)
        {
            Pattern3();
            return;
        }
        PatternCheck();
    }

    void AnimationLoad()
    {
        if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Push"))
            Anime.SetBool("IsPushing", false);
        if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("LeftJump"))
            Anime.SetBool("IsJumpLing", false);
        if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("RightJump"))
            Anime.SetBool("IsJumpRing", false);
        if (GetDistanceFromPlayer() <= AttackDistance)
        {
            if (BossUI.PhaseVar == "2" || BossUI.PhaseVar == "3" || BossUI.PhaseVar == "F" || BossUI.PhaseVar == "B")
            {
                if (!RandomCheck)
                {
                    RandomCheck = true;
                    RandomVal = Random.Range(0, 99);
                    Debug.Log("'Boss' 공격(Push)을 위한 난수(" + RandomVal + ") 생성");
                    Anime.SetBool("IsWalking", true);
                }
                if (RandomCheck && RandomVal >= 0 && RandomVal <= 19)
                {
                    RandomCheck = false;
                    if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Push"))
                        PushMovement();
                }
                else
                    AttackMovement();
            }
            else
                AttackMovement();
        }
        else if (GetDistanceFromPlayer() > AttackDistance && GetDistanceFromPlayer() <= Chasing)
        {
            if (BossUI.PhaseVar == "3" || BossUI.PhaseVar == "F" || BossUI.PhaseVar == "B")
            {
                if (!RandomCheck)
                {
                    RandomCheck = true;
                    RandomVal = Random.Range(0, 99);
                    Debug.Log("'Boss' 공격(Jump)을 위한 난수(" + RandomVal + ") 생성");
                    Anime.SetBool("IsWalking", true);
                }
                if (RandomCheck && (RandomVal >= 0 && RandomVal <= 4))
                {
                    RandomCheck = false;
                    if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("LeftJump"))
                        LeftJumpMovement();
                }
                if (RandomCheck && (RandomVal >= 95 && RandomVal <= 99))
                {
                    RandomCheck = false;
                    if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("RightJump"))
                        RightJumpMovement();
                }
                else
                    WalkMovement();
            }
            else
                WalkMovement();
        }
        else
            if (BossUI.PhaseVar == "F" || BossUI.PhaseVar == "B")
        {
            if (!RandomCheck)
            {
                RandomCheck = true;
                RandomVal = Random.Range(0, 99);
                Debug.Log("'Boss' 공격(Dash)을 위한 난수(" + RandomVal + ") 생성");
                Anime.SetBool("IsWalking", true);
            }
            if (RandomCheck && (RandomVal >= 0 && RandomVal <= 49))
            {
                RandomCheck = false;
                if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
                    DashMovement();
            }
            else
                PatrolMovement();
        }
        else
            PatrolMovement();
    }

    void PatrolMovement()
    {
        PatrolDelay += Time.deltaTime;
        if (!Patrol && PatrolDelay > PDelay)
        {
            Anime.SetBool("IsWalking", true);
            Number = Random.Range(0, Paths.Count);
            BossAI.SetDestination(Paths[Number].transform.position);
            Patrol = true;
            PatrolDelay = 0f;
            Debug.Log("'Boss'의 이동(Patrol" + Number + ") 확인");
            RandomCheck = false;
        }
        if (Patrol && (Paths[Number].transform.position - transform.position).magnitude < 0.5f)
        {
            Anime.SetBool("IsWalking", false);
            Patrol = false;
            PatrolDelay = 0f;
            Debug.Log("'Boss'의 이동(Remain" + Number + ") 확인");
            RandomCheck = false;
        }
    }

    void RotTransform()
    {
        if (GetDistanceFromPlayer() <= Chasing)
        {
            if (OldBossAngle != Player.transform.rotation.eulerAngles)
            {
                PatrolDelay = 0f;
                Anime.SetBool("IsWalking", true);
                Quaternion Look = Quaternion.LookRotation(Player.transform.position - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Look, Time.deltaTime * RoSpeed);
                OldBossAngle = transform.rotation.eulerAngles;
            }
            else
            {
                Anime.SetBool("IsWalking", true);
                RandomCheck = false;
            }
        }
        else
            BossUI.HealthRegen();
    }

    void WalkMovement()
    {
        PatrolDelay = 0f;
        Anime.SetBool("IsWalking", true);
        BossAI.SetDestination(Player.transform.position);
    }

    void AttackMovement()
    {
        if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Attack1")
            && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Attack2")
            && Anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            RandomCheck = false;
            Anime.SetBool("IsWalking", false);
            BossAI.SetDestination(transform.position);
            if (AttackType)
            {
                Anime.Play("Attack2");
                Anime.SetBool("IsAttack2ing", true);
                Debug.Log("'Boss'의 공격(Attack2) 확인");
                AttackType = false;
            }
            else
            {
                Anime.Play("Attack1");
                Anime.SetBool("IsAttack1ing", true);
                Debug.Log("'Boss'의 공격(Attack1) 확인");
                AttackType = true;
            }
        }
        else if (Anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            Anime.SetBool("IsAttack1ing", false);
            Anime.SetBool("IsAttack2ing", false);
            Anime.SetBool("IsWalking", true);
        }
    }

    void PushMovement()
    {
        Anime.SetBool("IsAttack1ing", false);
        Anime.SetBool("IsAttack2ing", false);
        Anime.SetBool("IsWalking", false);
        Anime.Play("Push");
        Anime.SetBool("IsPushing", true);
        Debug.Log("'Boss'의 공격(Push) 확인");
        BossAI.SetDestination(transform.position);
        PRigid.AddForce(transform.rotation * Vector3.back * -4f, ForceMode.VelocityChange);
    }

    void DashMovement()
    {
        if (Dash)
            return;
        else
        {
            Anime.SetBool("IsAttack1ing", false);
            Anime.SetBool("IsAttack2ing", false);
            Anime.SetBool("IsWalking", false);
            Anime.SetBool("IsPushing", false);
            Anime.Play("Dash");
            Anime.SetBool("IsDashing", true);
            Debug.Log("'Boss'의 돌진(Dash) 확인");
            Dash = true;
            BossAI.SetDestination(Player.transform.position);
            PlayerPos = Player.transform.position;
            BossAI.speed = Speed * 5f;
            PlayerUI.CheckCal = false;
        }
    }

    void LeftJumpMovement()
    {
        Anime.SetBool("IsAttack1ing", false);
        Anime.SetBool("IsAttack2ing", false);
        Anime.SetBool("IsWalking", false);
        Anime.SetBool("IsPushing", false);
        Anime.Play("LeftJump");
        Anime.SetBool("IsJumpLing", true);
        Debug.Log("'Boss'의 점프(Jump) 확인");
        BossAI.SetDestination(transform.position);
        PRigid.AddForce(transform.rotation * Vector3.up * 2.5f, ForceMode.VelocityChange);
        PRigid.AddForce(transform.rotation * Vector3.left * 2.5f, ForceMode.VelocityChange);
        PlayerUI.CheckCal = false;
        PlayerUI.Invoke("CalDamage", 1f);
    }

    void RightJumpMovement()
    {
        Anime.SetBool("IsAttack1ing", false);
        Anime.SetBool("IsAttack2ing", false);
        Anime.SetBool("IsWalking", false);
        Anime.SetBool("IsPushing", false);
        Anime.Play("RightJump");
        Anime.SetBool("IsJumpRing", true);
        Debug.Log("'Boss'의 점프(Jump) 확인");
        BossAI.SetDestination(transform.position);
        PRigid.AddForce(transform.rotation * Vector3.up * 2.5f, ForceMode.VelocityChange);
        PRigid.AddForce(transform.rotation * Vector3.right * 2.5f, ForceMode.VelocityChange);
        PlayerUI.CheckCal = false;
        PlayerUI.Invoke("CalDamage", 1f);
    }

    void PatternCheck()
    {
        if (BossUI.PhaseVar == "2" && !PatCheck)
            Pattern1();
        else if (BossUI.PhaseVar == "3" && PatCheck)
            Pattern2();
        else if (BossUI.PhaseVar == "F" && !PatCheck)
            Pattern3();
    }

    void Pattern1()
    {
        if (Pattern1Set && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            Anime.Play("Jump");
        if (!Pattern1Set)
        {
            Wall.SetActive(true);
            Debug.Log("'Boss' 특수 공격(Pattern1) 확인");
            NoticeText.text = string.Format("보스가 지진을 일으키려 합니다.");
            Notice.SetActive(true);
            Anime.SetBool("IsAttack1ing", false);
            Anime.SetBool("IsAttack2ing", false);
            Anime.SetBool("IsWalking", false);
            Anime.SetBool("IsPushing", false);
            Anime.SetBool("IsDashing", false);
            Anime.SetBool("IsJumpLing", false);
            Anime.SetBool("IsJumpRing", false);
            Anime.Play("Jump");
            BossAI.SetDestination(transform.position);
            Pattern1Set = true;
        }
        else
        {
            PatternDelay += Time.deltaTime;
            if (PatternDelay > 9.5f && Pattern1Set)
            {
                if (Pattern1Hide)
                {
                    TextDelay += Time.deltaTime;
                    NoticeText.text = string.Format("성공적으로 지진을 피했습니다.");
                    if (TextDelay > 1f)
                    {
                        Wall.SetActive(false);
                        Notice.SetActive(false);
                        NoticeText.text = string.Format("");
                        Pattern1Set = false;
                        PatCheck = true;
                        Pattern1Hide = false;
                        PatternDelay = 0f;
                        TextDelay = 0f;
                        Debug.Log("'Boss' 회피(Pattern1) 확인");
                        if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
                            DashMovement();
                    }
                }
                else
                {
                    TextDelay += Time.deltaTime;
                    NoticeText.text = string.Format("지진을 피하지 못했습니다.");
                    if (TextDelay > 1f)
                    {
                        Notice.SetActive(false);
                        NoticeText.text = string.Format("");
                        Pattern1Set = false;
                        PatCheck = true;
                        Pattern1Hide = false;
                        PatternDelay = 0f;
                        TextDelay = 0f;
                        PlayerUI.ThisHP -= 1000f;
                        Debug.Log("'Boss' 적중(Pattern1) 확인");
                    }
                }
            }
        }
    }

    void Pattern2()
    {
        if (!Pattern2Set)
        {
            for (int i = 0; i < Souls.Count; ++i)
                Souls[i].SetActive(true);
            Debug.Log("'Boss' 특수 공격(Pattern2) 확인");
            NoticeText.text = string.Format("보스의 원혼들이 지속 피해를 입힙니다.");
            Notice.SetActive(true);
            Anime.SetBool("IsAttack1ing", false);
            Anime.SetBool("IsAttack2ing", false);
            Anime.SetBool("IsWalking", false);
            Anime.SetBool("IsPushing", false);
            Anime.SetBool("IsDashing", false);
            Anime.SetBool("IsJumpLing", false);
            Anime.SetBool("IsJumpRing", false);
            Anime.Play("Jump");
            BossAI.SetDestination(transform.position);
            Pattern2Set = true;
        }
        else
        {
            PatternDelay += Time.deltaTime;
            PlayerUI.ThisHP -= Time.deltaTime * 2f;
            if (PatternDelay > 26.5f && Pattern2Set)
            {
                if (GameObject.FindGameObjectsWithTag("Soul").Length < 1)
                {
                    TextDelay += Time.deltaTime;
                    NoticeText.text = string.Format("성공적으로 원혼들을 제거하였습니다.");
                    if (TextDelay > 1f)
                    {
                        Notice.SetActive(false);
                        NoticeText.text = string.Format("");
                        Pattern2Set = false;
                        PatCheck = false;
                        PatternDelay = 0f;
                        TextDelay = 0f;
                        Debug.Log("'Boss' 회피(Pattern2) 확인");
                    }
                }
                else
                {
                    TextDelay += Time.deltaTime;
                    NoticeText.text = string.Format("원혼들을 제거하지 못하였습니다.");
                    if (TextDelay > 1f)
                    {
                        Notice.SetActive(false);
                        NoticeText.text = string.Format("");
                        Pattern2Set = false;
                        PatCheck = true;
                        PatternDelay = 0f;
                        TextDelay = 0f;
                        PlayerUI.ThisHP -= 1000f;
                        Debug.Log("'Boss' 적중(Pattern2) 확인");
                    }
                }
            }
            else if (Pattern2Set)
            {
                if (GameObject.FindGameObjectsWithTag("Soul").Length < 1)
                {
                    TextDelay += Time.deltaTime;
                    NoticeText.text = string.Format("성공적으로 원혼들을 제거하였습니다.");
                    if (TextDelay > 1f)
                    {
                        Notice.SetActive(false);
                        NoticeText.text = string.Format("");
                        Pattern2Set = false;
                        PatCheck = false;
                        PatternDelay = 0f;
                        TextDelay = 0f;
                        Debug.Log("'Boss' 회피(Pattern2) 확인");
                    }
                }
            }
        }
    }

    void Pattern3()
    {
        if (!Pattern3Set)
        {
            LColors[LCNumber].SetActive(true);
            RColors[RCNumber].SetActive(true);
            Debug.Log("'Boss' 특수 공격(Pattern3) 확인");
            NoticeText.text = string.Format("보스의 무기에 기운이 생겼습니다.");
            Notice.SetActive(true);
            Anime.SetBool("IsAttack1ing", false);
            Anime.SetBool("IsAttack2ing", false);
            Anime.SetBool("IsWalking", false);
            Anime.SetBool("IsPushing", false);
            Anime.SetBool("IsDashing", false);
            Anime.SetBool("IsJumpLing", false);
            Anime.SetBool("IsJumpRing", false);
            Anime.Play("Jump");
            BossAI.SetDestination(transform.position);
            Pattern3Set = true;
        }
        else
        {
            PatternDelay += Time.deltaTime;
            if (PatternDelay > 34.5f && Pattern3Set)
            {
                Debug.Log(LCNumber + ", " + RCNumber + ", " + PColor);
                if ((LCNumber == RCNumber && RCNumber == PColor && PColor == LCNumber)
                    || (LCNumber == 0 && RCNumber == 1 && PColor == 3)
                    || (LCNumber == 1 && RCNumber == 0 && PColor == 3)
                    || (LCNumber == 1 && RCNumber == 2 && PColor == 4)
                    || (LCNumber == 2 && RCNumber == 1 && PColor == 4)
                    || (LCNumber == 2 && RCNumber == 0 && PColor == 5)
                    || (LCNumber == 0 && RCNumber == 2 && PColor == 5))
                {
                    TextDelay += Time.deltaTime;
                    NoticeText.text = string.Format("성공적으로 기운을 저지하였습니다.");
                    if (TextDelay > 1f)
                    {
                        Notice.SetActive(false);
                        LColors[LCNumber].SetActive(false);
                        RColors[RCNumber].SetActive(false);
                        NoticeText.text = string.Format("");
                        Pattern3Set = false;
                        PatCheck = true;
                        PatternDelay = 0f;
                        TextDelay = 0f;
                        Debug.Log("'Boss' 회피(Pattern3) 확인(" + LCNumber + ", " + RCNumber + ", " + PColor + ")");
                    }
                }
                else
                {
                    TextDelay += Time.deltaTime;
                    NoticeText.text = string.Format("기운을 저지하지 못하였습니다.");
                    if (TextDelay > 1f)
                    {
                        Notice.SetActive(false);
                        LColors[LCNumber].SetActive(false);
                        RColors[RCNumber].SetActive(false);
                        NoticeText.text = string.Format("");
                        Pattern3Set = false;
                        PatCheck = true;
                        PatternDelay = 0f;
                        TextDelay = 0f;
                        PlayerUI.ThisHP -= 1000f;
                        Debug.Log("'Boss' 적중(Pattern3) 확인(" + LCNumber + ", " + RCNumber + ", " + PColor + ")");
                    }
                }
            }
        }
    }

    float GetDistanceFromPlayer()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        return distance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            if (PAnime.GetBool("IsAttacking") || PAnime.GetBool("IsSmashing"))
                BossUI.CalDamage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            if (PAnime.GetBool("IsStriking"))
                BossUI.CalDamage();
        }
    }
}
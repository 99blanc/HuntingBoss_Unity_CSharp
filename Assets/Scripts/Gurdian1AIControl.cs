using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Gurdian1AIControl : MonoBehaviour
{
    public static Gurdian1AIControl instance;
    public List<GameObject> Paths = new List<GameObject>();
    public List<GameObject> Walls = new List<GameObject>();
    public bool Patrol = false;
    public bool Pattern1Set = false;
    public bool Pattern2Set = false;
    public bool Pattern3Set = false;
    public bool Pattern2Hide = false;
    public bool Pattern3Hide = false;
    public bool Dash = false;
    public bool Damage = false;
    public bool Rope = false;
    public float Chasing = 8f;
    public float AttackDistance = 5f;
    public float RoSpeed = 55f;
    public float Speed = 1.25f;
    public float Accel = 10f;
    public float PDelay = 10f;
    public int PColor = 0;
    public int WallCount = 0;
    public GameObject Player;
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
    NavMeshAgent Gurdian1AI;
    Animator Anime;
    Animator PAnime;
    Rigidbody PRigid;
    Gurdian1UIControl Gurdian1UI;
    PlayerUIControl PlayerUI;
    Vector3 OldGurdian1Angle;

    void Start()
    {
        PlayerUI = GameObject.Find("PlayerStatus").GetComponent<PlayerUIControl>();
        Gurdian1UI = GameObject.Find("Gurdian1Status").GetComponent<Gurdian1UIControl>();
        Gurdian1AI = GetComponent<NavMeshAgent>();
        Anime = GetComponent<Animator>();
        PAnime = GameObject.Find("Player").GetComponent<Animator>();
        PRigid = GameObject.Find("Player").GetComponent<Rigidbody>();
        Gurdian1AI.speed = Speed;
        OldGurdian1Angle = transform.rotation.eulerAngles;
        PatrolDelay = 0f;
        PatternDelay = 0f;
        TextDelay = 0f;
        Notice.SetActive(false);
        for (int i = 0; i < Walls.Count; ++i)
            Walls[i].SetActive(false);
    }

    void Update()
    {
        if (PlayerUI.Dead || Gurdian1UI.Dead)
            return;
        if (OldGurdian1Angle == Player.transform.rotation.eulerAngles)
            Anime.SetBool("IsWalking", false);
        if (Gurdian1UI.PhaseVar == "1" || Gurdian1UI.PhaseVar == "2" || Gurdian1UI.PhaseVar == "3")
        {
            if (Patrol)
                Gurdian1AI.speed = Speed * 10f;
            else
            {
                Gurdian1AI.speed = Speed;
                Gurdian1AI.acceleration = Accel;
            }
        }
        if (Gurdian1UI.PhaseVar == "2" || Gurdian1UI.PhaseVar == "3")
        {
            if (Dash)
                Gurdian1AI.speed = Speed * 5f;
            else if (Patrol)
                Gurdian1AI.speed = Speed * 10f;
            else
            {
                Gurdian1AI.speed = Speed;
                Gurdian1AI.acceleration = Accel;
            }
        }
        if (Gurdian1UI.PhaseVar == "F")
        {
            if (Dash)
                Gurdian1AI.speed = Speed * 5f;
            else if (Patrol)
                Gurdian1AI.speed = Speed * 10f;
            else
            {
                Gurdian1AI.speed = Speed * 2f;
                Gurdian1AI.acceleration = Accel * 1.5f;
            }
        }
        if (Gurdian1UI.PhaseVar == "B")
        {
            if (Dash)
                Gurdian1AI.speed = Speed * 5f;
            else if (Patrol)
                Gurdian1AI.speed = Speed * 10f;
            else
            {
                Gurdian1AI.speed = Speed * 2.5f;
                Gurdian1AI.acceleration = Accel * 2f;
            }
        }
    }

    void FixedUpdate()
    {
        if (PlayerUI.Dead || Gurdian1UI.Dead)
        {
            Gurdian1AI.SetDestination(transform.position);
            return;
        }
        if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Groggy"))
        {
            Gurdian1AI.SetDestination(transform.position);
            return;
        }
        if (Damage && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
        {
            Damage = false;
            Debug.Log("'Gurdian1' 피해 판정");
            return;
        }
        if (Pattern3Set)
        {
            Pattern3();
            return;
        }
        if (Dash)
            return;
        AnimationLoad();
        RotTransform();
        if (Pattern1Set)
        {
            Pattern1();
            return;
        }
        if (Pattern2Set)
        {
            Pattern2();
            return;
        }
        PatternCheck();
    }

    void AnimationLoad()
    {
        if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Push"))
            Anime.SetBool("IsPushing", false);
        if (GetDistanceFromPlayer() <= AttackDistance)
        {
            Anime.SetBool("IsWalking", true);
            Anime.SetBool("IsRunning", false);
            if (Gurdian1UI.PhaseVar == "2" || Gurdian1UI.PhaseVar == "3" || Gurdian1UI.PhaseVar == "F" || Gurdian1UI.PhaseVar == "B")
            {
                if (!RandomCheck)
                {
                    RandomCheck = true;
                    RandomVal = Random.Range(0, 99);
                    Debug.Log("'Gurdian1' 공격(Push)을 위한 난수(" + RandomVal + ") 생성");
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
            Anime.SetBool("IsWalking", true);
            Anime.SetBool("IsAttack1ing", false);
            Anime.SetBool("IsAttack2ing", false);
            if (Gurdian1UI.PhaseVar == "3" || Gurdian1UI.PhaseVar == "F" || Gurdian1UI.PhaseVar == "B")
            {
                if (!RandomCheck)
                {
                    RandomCheck = true;
                    RandomVal = Random.Range(0, 99);
                    Debug.Log("'Gurdian1' 공격(Jump)을 위한 난수(" + RandomVal + ") 생성");
                    Anime.SetBool("IsWalking", true);
                }
                if (RandomCheck && (RandomVal >= 0 && RandomVal <= 4))
                {
                    RandomCheck = false;
                    if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("LeftJump"))
                        ShootMovement();
                }
                else
                    WalkMovement();
            }
            else
                WalkMovement();
        }
        else
            if (Gurdian1UI.PhaseVar == "F" || Gurdian1UI.PhaseVar == "B")
        {
            if (!RandomCheck)
            {
                RandomCheck = true;
                RandomVal = Random.Range(0, 99);
                Debug.Log("'Gurdian1' 공격(Dash)을 위한 난수(" + RandomVal + ") 생성");
                Anime.SetBool("IsRunning", true);
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
            Debug.Log("'Gurdian1'의 (" + Gurdian1AI.speed + ", " + Gurdian1AI.acceleration + ") 속도 이동");
            Anime.SetBool("IsWalking", false);
            Anime.SetBool("IsRunning", false);
            Anime.SetBool("IsLanding", false);
            Anime.SetBool("IsFlying", true);

            Number = Random.Range(0, Paths.Count);
            Gurdian1AI.SetDestination(Paths[Number].transform.position);
            Patrol = true;
            PatrolDelay = 0f;
            Debug.Log("'Gurdian1'의 이동(Patrol" + Number + ") 확인");
            RandomCheck = false;
        }
        if (Patrol && (Paths[Number].transform.position - transform.position).magnitude < 0.5f)
        {
            Anime.SetBool("IsWalking", false);
            Anime.SetBool("IsRunning", false);
            Anime.SetBool("IsFlying", false);
            Anime.SetBool("IsLanding", true);
            Patrol = false;
            PatrolDelay = 0f;
            Debug.Log("'Gurdian1'의 이동(Remain" + Number + ") 확인");
            RandomCheck = false;
        }
    }

    void RotTransform()
    {
        if (GetDistanceFromPlayer() <= Chasing)
        {
            if (OldGurdian1Angle != Player.transform.rotation.eulerAngles)
            {
                PatrolDelay = 0f;
                Anime.SetBool("IsWalking", true);
                Quaternion Look = Quaternion.LookRotation(Player.transform.position - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Look, Time.deltaTime * RoSpeed);
                OldGurdian1Angle = transform.rotation.eulerAngles;
            }
            else
            {
                Anime.SetBool("IsWalking", true);
                RandomCheck = false;
            }
        }
        else
            Gurdian1UI.HealthRegen();
    }

    void WalkMovement()
    {
        PatrolDelay = 0f;
        if (Gurdian1AI.speed > Speed)
        {
            Anime.SetBool("IsWalking", false);
            Anime.SetBool("IsRunning", true);
            Gurdian1AI.SetDestination(Player.transform.position);
            return;
        }
        else
        {
            Anime.SetBool("IsWalking", true);
            Anime.SetBool("IsRunning", false);
            Gurdian1AI.SetDestination(Player.transform.position);
            return;
        }
    }

    void AttackMovement()
    {
        if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Attack1")
            && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Attack2")
            && Anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            RandomCheck = false;
            Anime.SetBool("IsWalking", false);
            Gurdian1AI.SetDestination(transform.position);
            if (AttackType)
            {
                Anime.Play("Attack2");
                Anime.SetBool("IsAttack2ing", true);
                Debug.Log("'Gurdian1'의 공격(Attack2) 확인");
                AttackType = false;
            }
            else
            {
                Anime.Play("Attack1");
                Anime.SetBool("IsAttack1ing", true);
                Debug.Log("'Gurdian1'의 공격(Attack1) 확인");
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
        Anime.Play("Push"); //
        Anime.SetBool("IsPushing", true);
        Debug.Log("'Gurdian1'의 공격(Push) 확인");
        Gurdian1AI.SetDestination(transform.position);
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
            Debug.Log("'Gurdian1'의 돌진(Dash) 확인");
            Dash = true;
            Gurdian1AI.SetDestination(Player.transform.position);
            PlayerPos = Player.transform.position;
            Gurdian1AI.speed = Speed * 5f;
            PlayerUI.CheckCal = false;
        }
    }

    void ShootMovement()
    {
        Anime.SetBool("IsAttack1ing", false);
        Anime.SetBool("IsAttack2ing", false);
        Anime.SetBool("IsWalking", false);
        Anime.SetBool("IsPushing", false);
        Anime.Play("LeftJump");
        Anime.SetBool("IsJumpLing", true);
        Debug.Log("'Gurdian1'의 점프(Jump) 확인");
        Gurdian1AI.SetDestination(transform.position);
        PRigid.AddForce(transform.rotation * Vector3.up * 2.5f, ForceMode.VelocityChange);
        PRigid.AddForce(transform.rotation * Vector3.left * 2.5f, ForceMode.VelocityChange);
        PlayerUI.CheckCal = false;
        PlayerUI.Invoke("CalDamage", 1f);
    }

    void PatternCheck()
    {
        if (Gurdian1UI.PhaseVar == "2" && !PatCheck)
            Pattern1();
        else if (Gurdian1UI.PhaseVar == "3" && PatCheck)
            Pattern2();
        else if (Gurdian1UI.PhaseVar == "F" && !PatCheck)
            Pattern1();
        else if (Gurdian1UI.PhaseVar == "B" && PatCheck)
            Pattern3();
    }

    void Pattern1()
    {
        if (!Pattern1Set)
        {
            for (int i = 0; i < Walls.Count; ++i)
                Walls[i].SetActive(true);
            Debug.Log("'Gurdian1' 특수 공격(Pattern1) 확인");
            NoticeText.text = string.Format("보스가 뿌리들을 소환하였습니다.");
            Notice.SetActive(true);
            Anime.SetBool("IsAttack1ing", false);
            Anime.SetBool("IsAttack2ing", false);
            Anime.SetBool("IsWalking", false);
            Anime.SetBool("IsLanding", false);
            Anime.SetBool("IsDashing", false);
            Anime.Play("Scream");
            Gurdian1AI.SetDestination(transform.position);
            Pattern1Set = true;
        }
        else
        {
            if (WallCount == 4)
            {
                TextDelay += Time.deltaTime;
                NoticeText.text = string.Format("성공적으로 뿌리들을 제거하였습니다.");
                if (TextDelay > 1f)
                {
                    Notice.SetActive(false);
                    NoticeText.text = string.Format("");
                    Pattern1Set = false;
                    PatCheck = true;
                    WallCount = 0;
                    TextDelay = 0f;
                    Debug.Log("'Gurdian1' 회피(Pattern1) 확인");
                }
            }
        }
    }

    void Pattern2()
    {
        if (!Pattern2Set)
        {
            Debug.Log("'Gurdian1' 특수 공격(Pattern2) 확인");
            NoticeText.text = string.Format("보스가 화염을 뿜으려 합니다.");
            Notice.SetActive(true);
            Anime.SetBool("IsAttack1ing", false);
            Anime.SetBool("IsAttack2ing", false);
            Anime.SetBool("IsWalking", false);
            Anime.SetBool("IsPushing", false);
            Anime.SetBool("IsDashing", false);
            Anime.Play("Shoot");
            Gurdian1AI.SetDestination(transform.position);
            Pattern2Set = true;
        }
        else
        {
            PatternDelay += Time.deltaTime;
            if (PatternDelay > 19.5f && Pattern2Set)
            {
                if (Pattern2Hide)
                {
                    TextDelay += Time.deltaTime;
                    NoticeText.text = string.Format("성공적으로 화염을 피했습니다.");
                    if (TextDelay > 1f)
                    {
                        Notice.SetActive(false);
                        NoticeText.text = string.Format("");
                        Pattern2Set = false;
                        PatCheck = false;
                        Pattern2Hide = false;
                        PatternDelay = 0f;
                        TextDelay = 0f;
                        Debug.Log("'Gurdian1' 회피(Pattern2) 확인");
                        if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
                            DashMovement();
                    }
                }
                else
                {
                    TextDelay += Time.deltaTime;
                    NoticeText.text = string.Format("화염을 피하지 못했습니다.");
                    if (TextDelay > 1f)
                    {
                        Notice.SetActive(false);
                        NoticeText.text = string.Format("");
                        Pattern2Set = false;
                        PatCheck = false;
                        Pattern2Hide = false;
                        PatternDelay = 0f;
                        TextDelay = 0f;
                        PlayerUI.ThisHP -= 1000f;
                        Debug.Log("'Gurdian1' 적중(Pattern2) 확인");
                    }
                }
            }
            else if (Pattern2Set)
            {
                if (Pattern2Hide)
                {
                    TextDelay += Time.deltaTime;
                    NoticeText.text = string.Format("성공적으로 화염을 피했습니다.");
                    if (TextDelay > 1f)
                    {
                        Notice.SetActive(false);
                        NoticeText.text = string.Format("");
                        Pattern2Set = false;
                        PatCheck = false;
                        Pattern2Hide = false;
                        PatternDelay = 0f;
                        TextDelay = 0f;
                        Debug.Log("'Gurdian1' 회피(Pattern2) 확인");
                        if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
                            DashMovement();
                    }
                }
            }
        }
    }

    void Pattern3()
    {
        if (Pattern3Set && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Scream"))
            Anime.Play("Scream");
        if (Pattern3Set && !PAnime.GetCurrentAnimatorStateInfo(0).IsName("Battle"))
            PAnime.Play("Battle");
        if (!Pattern3Set)
        {
            Debug.Log("'Gurdian1' 특수 공격(Pattern3) 확인");
            NoticeText.text = string.Format("보스가 힘겨루기를 하려고 합니다.");
            Notice.SetActive(true);
            Anime.SetBool("IsAttack1ing", false);
            Anime.SetBool("IsAttack2ing", false);
            Anime.SetBool("IsWalking", false);
            Anime.SetBool("IsPushing", false);
            Anime.SetBool("IsDashing", false);
            Anime.Play("Scream");
            Gurdian1AI.SetDestination(transform.position);
            Pattern3Set = true;
            Gurdian1UI.Battle.SetActive(true);
        }
        else
        {
            PatternDelay += Time.deltaTime;
            if (PatternDelay > 35.5f && Pattern3Set)
            {
                if (Pattern3Hide)
                {
                    TextDelay += Time.deltaTime;
                    NoticeText.text = string.Format("성공적으로 힘겨루기를 승리하였습니다.");
                    if (TextDelay > 1f)
                    {
                        Notice.SetActive(false);
                        NoticeText.text = string.Format("");
                        Pattern3Set = false;
                        PatCheck = false;
                        Pattern3Hide = false;
                        PatternDelay = 0f;
                        Gurdian1UI.Battle.SetActive(false);
                        TextDelay = 0f;
                        Debug.Log("'Gurdian1' 회피(Pattern3) 확인");
                        if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
                            DashMovement();
                    }
                }
                else
                {
                    TextDelay += Time.deltaTime;
                    NoticeText.text = string.Format("힘겨루기에서 패배하였습니다.");
                    if (TextDelay > 1f)
                    {
                        Notice.SetActive(false);
                        NoticeText.text = string.Format("");
                        Pattern3Set = false;
                        PatCheck = false;
                        Pattern3Hide = false;
                        PatternDelay = 0f;
                        Gurdian1UI.Battle.SetActive(false);
                        TextDelay = 0f;
                        PlayerUI.ThisHP -= 1000f;
                        Debug.Log("'Gurdian1' 적중(Pattern3) 확인");
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
                Gurdian1UI.CalDamage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            if (PAnime.GetBool("IsStriking"))
                Gurdian1UI.CalDamage();
        }
    }
}

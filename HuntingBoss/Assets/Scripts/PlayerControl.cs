using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;
    public bool Damage = false;
    public float RSpeed = 2f;
    public float SSpeed = 3.5f;
    public float DodgeRange = 6f;
    public float Hori;
    public float Verti;
    private float MouseRot;
    private float AttackDelay = 0f;
    private bool AttackType = false;
    private bool KeyMouse0 = false;
    private bool KeyMouse1 = false;
    private bool KeyShift = false;
    private bool KeySpace = false;
    private bool MouseLock = true;
    Rigidbody Rigid;
    Animator Anime;
    Animator BAnime;
    Animator G1Anime;
    PlayerUIControl PlayerUI;
    BossUIControl BossUI;
    Gurdian1UIControl Gurdian1UI;
    BossAIControl BossAI;
    Gurdian1AIControl Gurdian1AI;
    Vector3 Vec;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Field1")
        {
            BAnime = GameObject.Find("Boss").GetComponent<Animator>();
            BossAI = GameObject.Find("Boss").GetComponent<BossAIControl>();
            BossUI = GameObject.Find("BossStatus").GetComponent<BossUIControl>();
        }
        if (SceneManager.GetActiveScene().name == "Field2")
        {
            G1Anime = GameObject.Find("Gurdian1").GetComponent<Animator>();
            Gurdian1AI = GameObject.Find("Gurdian1").GetComponent<Gurdian1AIControl>();
            Gurdian1UI = GameObject.Find("Gurdian1Status").GetComponent<Gurdian1UIControl>();
        }
        PlayerUI = GameObject.Find("PlayerStatus").GetComponent<PlayerUIControl>();
        Rigid = GetComponent<Rigidbody>();
        Anime = GetComponent<Animator>();
    }

    void Update()
    {
        if (PlayerUI.Dead)
            return;
        KeyMouse0 = Input.GetKey(KeyCode.Mouse0);
        KeyMouse1 = Input.GetKey(KeyCode.Mouse1);
        KeySpace = Input.GetKey(KeyCode.Space);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (MouseLock)
            {
                Cursor.lockState = CursorLockMode.None;
                MouseLock = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                MouseLock = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
            Debug.Log("'Player'의 이동(Run) 확인");
        KeyShift = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetKeyDown(KeyCode.LeftShift))
            Debug.Log("'Player'의 이동(Sprint) 확인");
        MouseRot = Input.GetAxis("Mouse X");
        Hori = Input.GetAxisRaw("Horizontal");
        Verti = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PlayerUI.PotionControl();
    }

    void FixedUpdate()
    {
        if (transform.position.y < -2)
            transform.position = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
        if (PlayerUI.Dead)
            return;
        PlayerUI.StaminaRegen();
        PlayerUI.HealthRegen();
        if (SceneManager.GetActiveScene().name == "Field2")
        {
            if (Gurdian1AI.Pattern3Set)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                    Gurdian1UI.ThisBP += 0.25f;
                return;
            }
        }
        if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Breathless"))
            return;
        if (Damage && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
        {
            Debug.Log("'Player' 피해 판정");
            Anime.SetBool("IsDamaging", false);
            Damage = false;
            return;
        }
        AttackMovement();
        if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Smash")
            || Anime.GetCurrentAnimatorStateInfo(0).IsName("Strike"))
            return;
        RunMovement();
        AnimationLoad();
        DodgeMovement();
    }

    public void RunMovement()
    {
        if (Verti == 0 && Hori == 0)
            return;
        if (Anime.GetBool("IsSprinting"))
        {
            SprintMovement();
            return;
        }
        Vec.Set(Hori, 0f, Verti);
        if (Verti >= 0)
            Vec = transform.rotation * Vec.normalized * RSpeed * Time.deltaTime;
        else
            Vec = transform.rotation * Vec.normalized * RSpeed / 1.5f * Time.deltaTime;
        Rigid.MovePosition(transform.position + Vec);
        PlayerUI.CalStamina();
    }

    public void SprintMovement()
    {
        Vec.Set(Hori, 0f, Verti);
        if (Verti >= 0)
            Vec = transform.rotation * Vec.normalized * SSpeed * Time.deltaTime;
        Rigid.MovePosition(transform.position + Vec);
        PlayerUI.CalStamina();
    }

    private void AnimationLoad()
    {
        if (KeySpace || KeyMouse0 || (Hori == 0 && Verti == 0))
        {
            Anime.SetBool("IsRunning", false);
            Anime.SetBool("IsSprinting", false);
            Anime.SetBool("IsWalking", false);
        }
        else
        {
            if (Verti < 0 || Hori != 0)
            {
                Anime.SetBool("IsRunning", false);
                Anime.SetBool("IsSprinting", false);
                Anime.SetBool("IsWalking", true);
            }
            else
            {
                Anime.SetBool("IsRunning", true);
                if (KeyShift)
                    Anime.SetBool("IsSprinting", true);
                else
                    Anime.SetBool("IsSprinting", false);
            }
        }
        if (MouseRot != 0)
            Anime.SetBool("IsWalking", true);
    }

    private void AttackMovement()
    {
        if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Attack")
            && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Smash")
            && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Strike"))
        {
            Anime.SetBool("IsAttacking", false);
            Anime.SetBool("IsSmashing", false);
            AttackDelay += Time.deltaTime;
            if (KeyMouse0)
            {
                if (AttackType)
                {
                    if (AttackDelay > 1f)
                    {
                        AttackDelay = 0f;
                        AttackType = false;
                        return;
                    }
                    Anime.Play("Smash");
                    AttackDelay = 0f;
                    Anime.SetBool("IsSmashing", true);
                    Debug.Log("'Player'의 공격(Smash) 확인");
                    AttackType = false;
                }
                else
                {
                    Anime.Play("Attack");
                    AttackDelay = 0f;
                    Debug.Log("'Player'의 공격(Attack) 확인");
                    Anime.SetBool("IsAttacking", true);
                    AttackType = true;
                }
            }
            if (KeyMouse1)
            {
                Anime.Play("Strike");
                Debug.Log("'Player'의 공격(Strike) 확인");
                Anime.SetBool("IsStriking", true);
                PlayerUI.CalStamina();
            }
        }
        if (Anime.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6f)
            Anime.SetBool("IsStriking", false);
    }

    public void DodgeMovement()
    {
        if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Dodge"))
        {
            if (KeySpace)
            {
                if (!Anime.GetBool("IsDodging"))
                {
                    Rigid.AddForce(transform.rotation * Vector3.forward * DodgeRange, ForceMode.VelocityChange);
                    Anime.SetBool("IsDodging", true);
                    Debug.Log("'Player'의 이동(Dodge) 확인");
                    PlayerUI.Dodge = true;
                    PlayerUI.CalStamina();
                }
            }
        }
        else
        {
            Anime.SetBool("IsDodging", false);
            PlayerUI.Dodge = false;
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "Field1")
        {
            if (other.CompareTag("BossWeaponR"))
            {
                if (BAnime.GetBool("IsAttack1ing"))
                    PlayerUI.CalDamage();
            }
            if (other.CompareTag("BossWeaponL"))
            {
                if (BAnime.GetBool("IsAttack2ing"))
                    PlayerUI.CalDamage();
            }
            if (other.CompareTag("BossWeaponR") || other.CompareTag("BossWeaponL"))
            {
                if (BAnime.GetBool("IsAttack3ing"))
                    PlayerUI.CalDamage();
            }
            if (other.CompareTag("Soul") && BossAI.Pattern2Set)
            {
                if (Anime.GetBool("IsAttacking") || Anime.GetBool("IsSmashing"))
                {
                    other.gameObject.SetActive(false);
                    Destroy(other.gameObject);
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "Field2")
        {
            if (other.CompareTag("BossWeapon"))
            {
                if (G1Anime.GetBool("IsAttack2ing"))
                    PlayerUI.CalDamage();
            }
            if (other.CompareTag("BossWeaponH"))
            {
                if (G1Anime.GetBool("IsAttack1ing"))
                    PlayerUI.CalDamage();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "Field1")
        {
            if (other.CompareTag("Wall"))
            {
                if (BossAI.Pattern1Set)
                    BossAI.Pattern1Hide = true;
            }
            if (other.CompareTag("Red"))
            {
                if (BossAI.Pattern3Set)
                    BossAI.PColor = 0;
            }
            if (other.CompareTag("Green"))
            {
                if (BossAI.Pattern3Set)
                    BossAI.PColor = 1;
            }
            if (other.CompareTag("Blue"))
            {
                if (BossAI.Pattern3Set)
                    BossAI.PColor = 2;
            }
            if (other.CompareTag("Yellow"))
            {
                if (BossAI.Pattern3Set)
                    BossAI.PColor = 3;
            }
            if (other.CompareTag("Sky"))
            {
                if (BossAI.Pattern3Set)
                    BossAI.PColor = 4;
            }
            if (other.CompareTag("Pink"))
            {
                if (BossAI.Pattern3Set)
                    BossAI.PColor = 5;
            }
        }
        if (SceneManager.GetActiveScene().name == "Field2")
        {
            if (other.CompareTag("Wall") && Gurdian1AI.Pattern1Set)
            {
                if (G1Anime.GetCurrentAnimatorStateInfo(0).IsName("Attack1") || G1Anime.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
                {
                    other.gameObject.SetActive(false);
                    Gurdian1UI.ThisHP -= 750f;
                    Gurdian1AI.WallCount += 1;
                    Debug.Log("'Boss'의 적중(Wall) 확인(" + Gurdian1AI.WallCount + ")");
                    if (!Gurdian1UI.Groggy)
                        G1Anime.Play("Damage");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "Field1")
        {
            if (other.CompareTag("Wall"))
            {
                if (BossAI.Pattern1Set)
                    BossAI.Pattern1Hide = false;
            }
        }
    }
}
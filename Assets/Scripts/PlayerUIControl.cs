using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUIControl : MonoBehaviour
{
    public static PlayerUIControl instance;
    public float MaxHP = 1000f;
    public float ThisHP;
    public float HPRegen = 0.5f;
    public float MaxSP = 100f;
    public float ThisSP;
    public float SPRegen = 8f;
    public float BreathVar = 20f;
    public int MaxPotion = 4;
    public int ThisPotion;
    public bool Dodge = false;
    public bool Dead = false;
    public bool CheckCal = false;
    public Text HP;
    public Image HealthBar;
    public Text SP;
    public Text AT;
    public Image StaminaBar;
    public Text BT;
    private string AText;
    private float TotalH;
    private float TotalS;
    private float LerpSpeed = 1f;
    private float AttackVal = 100f;
    private float DeadDelay;
    private bool Breath = false;
    Animator Anime;
    Animator BAnime;
    Animator GAnime;
    NavMeshAgent BossAI;
    NavMeshAgent Gurdian1AI;
    PlayerControl Player;
    BossAIControl Boss;
    Gurdian1AIControl Gurdian1;
    BossUIControl BossUI;
    Gurdian1UIControl Gurdian1UI;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Field1")
        {
            Boss = GameObject.Find("Boss").GetComponent<BossAIControl>();
            BAnime = GameObject.Find("Boss").GetComponent<Animator>();
            BossAI = GameObject.Find("Boss").GetComponent<NavMeshAgent>();
            BossUI = GameObject.Find("BossStatus").GetComponent<BossUIControl>();
        }
        if (SceneManager.GetActiveScene().name == "Field2")
        {
            Gurdian1 = GameObject.Find("Gurdian1").GetComponent<Gurdian1AIControl>();
            GAnime = GameObject.Find("Gurdian1").GetComponent<Animator>();
            Gurdian1AI = GameObject.Find("Gurdian1").GetComponent<NavMeshAgent>();
            Gurdian1UI = GameObject.Find("Gurdian1Status").GetComponent<Gurdian1UIControl>();
        }
        Player = GameObject.Find("Player").GetComponent<PlayerControl>();
        Anime = GameObject.Find("Player").GetComponent<Animator>();
        DeadDelay = 0f;
        ThisHP = MaxHP;
        ThisSP = MaxSP;
        ThisPotion = MaxPotion;
        BT.text = string.Format("{0} / {1}", ThisPotion, MaxPotion);
    }

    void Update()
    {
        if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Dodge"))
            Dodge = false;
        if (SceneManager.GetActiveScene().name == "Field1")
        {
            if (!BAnime.GetCurrentAnimatorStateInfo(0).IsName("Attack1")
            && !BAnime.GetCurrentAnimatorStateInfo(0).IsName("Attack2")
            && BAnime.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
                CheckCal = false;
            if (Boss.GetComponent<BossAIControl>().Dash)
            {
                Boss.transform.position = new Vector3
                (Boss.transform.position.x, Boss.transform.position.y, Boss.transform.position.z);
                if (Vector3.Distance(Boss.PlayerPos, Boss.transform.position) < 5f)
                {
                    BAnime.SetBool("IsDashing", false);
                    Boss.GetComponent<BossAIControl>().Dash = false;
                    BossAI.SetDestination(transform.position);
                    BAnime.Play("Attack3");
                    BAnime.SetBool("IsAttack3ing", true);
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "Field2")
        {
            if (!GAnime.GetCurrentAnimatorStateInfo(0).IsName("Attack1")
            && !GAnime.GetCurrentAnimatorStateInfo(0).IsName("Attack2")
            && GAnime.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
                CheckCal = false;
            if (Gurdian1.GetComponent<Gurdian1AIControl>().Dash)
            {
                Gurdian1.transform.position = new Vector3
                (Gurdian1.transform.position.x, Gurdian1.transform.position.y, Gurdian1.transform.position.z);
                if (Vector3.Distance(Gurdian1.PlayerPos, Gurdian1.transform.position) < 5f)
                {
                    GAnime.SetBool("IsDashing", false);
                    Gurdian1.GetComponent<Gurdian1AIControl>().Dash = false;
                    Gurdian1AI.SetDestination(transform.position);
                    GAnime.Play("Attack3");
                    GAnime.SetBool("IsAttack3ing", true);
                }
            }
        }
        if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Breathless"))
        {
            Anime.SetBool("IsWalking", false);
            Anime.SetBool("IsAttacking", false);
            Anime.SetBool("IsSmashing", false);
            Anime.SetBool("IsStriking", false);
        }
        if (ThisSP >= BreathVar)
        {
            Breath = false;
            Anime.SetBool("IsBreathless", false);
        }
    }

    void FixedUpdate()
    {
        UpdateHPStatus();
        UpdateSPStatus();
        CalAnimation();
    }

    private void CalAnimation()
    {
        if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            AText = "Walk";
        else if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            AText = "Run";
        else if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Sprint"))
            AText = "Sprint";
        else if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Breathless"))
            AText = "Breath";
        else if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Dodge"))
            AText = "Dodge";
        else if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
            AText = "Damage";
        else if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
            AText = "Dead";
        else if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            AText = "Attack";
        else if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Smash"))
            AText = "Smash";
        else if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Strike"))
            AText = "Strike";
        else
            AText = "NULL";
        AT.text = string.Format("{0}", (string)AText);
    }

    private void UpdateHPStatus()
    {
        if (ThisHP <= 0)
        {
            ThisHP = 0;
            HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, 0, Time.deltaTime * LerpSpeed);
            Dead = true;
            if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
            {
                Breath = false;
                Anime.SetBool("IsBreathless", false);
                Anime.SetBool("IsDodging", false);
                Anime.SetBool("IsAttacking", false);
                Anime.SetBool("IsSmashing", false);
                Anime.SetBool("IsStriking", false);
                Anime.SetBool("IsWalking", false);
                Anime.SetBool("IsRunning", false);
                Anime.SetBool("IsSprinting", false);
                Anime.SetBool("IsDamaging", false);
                Anime.Play("Dead");
                Anime.SetBool("IsDead", true);
            }
            if (Dead)
            {
                DeadDelay += Time.deltaTime;
                if (DeadDelay > 8f)
                {
                    SceneManager.LoadScene("Lose");
                    Debug.Log("'Lose' 화면으로 이동");
                    DeadDelay = 0f;
                }
            }
        }
        if (ThisHP > MaxHP)
            ThisHP = MaxHP;
        else
        {
            TotalH = ThisHP / MaxHP;
            HP.text = string.Format("{0} / {1}", (int)ThisHP, MaxHP);
            if (TotalH != HealthBar.fillAmount)
                HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, TotalH, Time.deltaTime * LerpSpeed);
        }
    }

    private void UpdateSPStatus()
    {
        if (ThisSP <= 0)
        {
            ThisSP = 0;
            StaminaBar.fillAmount = Mathf.Lerp(StaminaBar.fillAmount, 0, Time.deltaTime * LerpSpeed);
            if (Dead == true)
                return;
            Breath = true;
            if (Breath && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Breathless"))
                Anime.SetBool("IsBreathless", true);
        }
        if (ThisSP > MaxSP)
            ThisSP = MaxSP;
        else
        {
            TotalS = ThisSP / MaxSP;
            SP.text = string.Format("{0} / {1}", (int)ThisSP, MaxSP);
            if (TotalS != StaminaBar.fillAmount)
                StaminaBar.fillAmount = Mathf.Lerp(StaminaBar.fillAmount, TotalS, Time.deltaTime * LerpSpeed);
        }
    }

    public void CalDamage()
    {
        if (!CheckCal)
        {
            if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Dodge"))
                return;
            else
            {
                if (SceneManager.GetActiveScene().name == "Field1")
                {
                    if (BAnime.GetBool("IsAttack1ing"))
                    {
                        if (BossUI.PhaseVar == "F")
                            ThisHP -= AttackVal * 1.5f;
                        else if (BossUI.PhaseVar == "B")
                            ThisHP -= AttackVal * 3f;
                        else
                            ThisHP -= AttackVal;
                        Debug.Log("'Boss'의 적중(Attack1) 확인");
                        CheckCal = true;
                        Anime.SetBool("IsDamaging", true);
                        Player.Damage = true;
                    }
                    else if (BAnime.GetBool("IsAttack2ing"))
                    {
                        if (BossUI.PhaseVar == "F")
                            ThisHP -= AttackVal * 1.5f;
                        else if (BossUI.PhaseVar == "B")
                            ThisHP -= AttackVal * 3f;
                        else
                            ThisHP -= AttackVal;
                        Debug.Log("'Boss'의 적중(Attack2) 확인");
                        CheckCal = true;
                        Anime.SetBool("IsDamaging", true);
                        Player.Damage = true;
                    }
                    else if (BAnime.GetBool("IsAttack3ing"))
                    {
                        ThisHP -= AttackVal * 5f;
                        Debug.Log("'Boss'의 적중(Attack3) 확인");
                        BAnime.SetBool("IsAttack3ing", false);
                        CheckCal = true;
                        Anime.SetBool("IsDamaging", true);
                        Player.Damage = true;
                    }
                    else if (BAnime.GetBool("IsJumpLing"))
                    {
                        if (BossUI.PhaseVar == "F" || BossUI.PhaseVar == "B")
                            ThisHP -= AttackVal / 3f;
                        else
                            ThisHP -= AttackVal / 5f;
                        Debug.Log("'Boss'의 적중(Jump) 확인");
                        CheckCal = true;
                        Anime.SetBool("IsDamaging", true);
                        Player.Damage = true;
                    }
                    else if (BAnime.GetBool("IsJumpRing"))
                    {
                        if (BossUI.PhaseVar == "F" || BossUI.PhaseVar == "B")
                            ThisHP -= AttackVal / 3f;
                        else
                            ThisHP -= AttackVal / 5f;
                        Debug.Log("'Boss'의 적중(Jump) 확인");
                        CheckCal = true;
                        Anime.SetBool("IsDamaging", true);
                        Player.Damage = true;
                    }
                }
                if (SceneManager.GetActiveScene().name == "Field2")
                {
                    if (GAnime.GetBool("IsAttack1ing"))
                    {
                        if (Gurdian1UI.PhaseVar == "F")
                            ThisHP -= AttackVal * 3f;
                        else if (Gurdian1UI.PhaseVar == "B")
                            ThisHP -= AttackVal * 6f;
                        else
                            ThisHP -= AttackVal;
                        Debug.Log("'Gurdian1'의 적중(Attack1) 확인");
                        CheckCal = true;
                        Anime.SetBool("IsDamaging", true);
                        Player.Damage = true;
                    }
                    else if (GAnime.GetBool("IsAttack2ing"))
                    {
                        if (Gurdian1UI.PhaseVar == "F")
                            ThisHP -= AttackVal * 3f;
                        else if (Gurdian1UI.PhaseVar == "B")
                            ThisHP -= AttackVal * 6f;
                        else
                            ThisHP -= AttackVal;
                        Debug.Log("'Gurdian1'의 적중(Attack2) 확인");
                        CheckCal = true;
                        Anime.SetBool("IsDamaging", true);
                        Player.Damage = true;
                    }
                }
            }
        }
    }

    public void HealthRegen()
    {
        if (ThisHP <= 0)
            ThisHP = 0;
        else if (ThisHP != MaxHP)
            ThisHP += Time.deltaTime * HPRegen;
    }

    public void CalStamina()
    {
        if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            ThisSP -= Time.deltaTime * 2f;
        if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Sprint"))
            ThisSP -= Time.deltaTime * 4f;
        if (Anime.GetBool("IsDodging"))
        {
            if (Dodge)
                ThisSP -= 10f;
            else
                return;
        }
        if (Anime.GetBool("IsStriking"))
            ThisSP -= 40f;
    }

    public void StaminaRegen()
    {
        if (ThisSP != MaxSP)
        {
            if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Run")
            && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Sprint")
            && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Dodge")
            && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Smash")
            && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Strike"))
                ThisSP += Time.deltaTime * SPRegen;

        }
    }

    public void PotionControl()
    {
        if (ThisHP == MaxHP)
            return;
        else
        {
            if (ThisPotion > 4)
            {
                ThisPotion = MaxPotion;
                return;
            }
            else if (ThisPotion <= 4 && ThisPotion > 0)
            {
                ThisPotion -= 1;
                ThisHP += MaxHP * 0.4f;
                BT.text = string.Format("{0} / {1}", ThisPotion, MaxPotion);
            }
            else
                return;
        }
    }
}
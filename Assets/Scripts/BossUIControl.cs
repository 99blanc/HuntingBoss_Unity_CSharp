using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossUIControl : MonoBehaviour
{
    public static BossUIControl instance;
    public float MaxHP = 10000f;
    public float ThisHP;
    public float HPRegen = 2f;
    public float MaxGP = 1000f;
    public float ThisGP;
    public float GPDelay = 5f;
    public Text HP;
    public Image HealthBar;
    public Text GP;
    public Image GroggyBar;
    public Text Phase;
    public bool Groggy = false;
    public bool Dead = false;
    public bool CheckCal = false;
    public string PhaseVar;
    private float TotalH;
    private float TotalG;
    private float LerpSpeed = 1f;
    private float AttackVal = 100f;
    private float SmashVal = 140f;
    private float StrikeVal = 400f;
    private float GroggyVal = 0.75f;
    private float GroggyDelay;
    private float DeadDelay;
    private bool GroggyAnime;
    Animator Anime;
    Animator PAnime;
    NavMeshAgent Boss;
    BossAIControl BossAI;
    GameObject Reward;
    Reward RewardUI;

    void Start()
    {
        if (PlayerPrefs.GetInt("WeaponLoad") == 1)
        {
            AttackVal += AttackVal * 0.002f;
            SmashVal += SmashVal * 0.002f;
            StrikeVal += StrikeVal * 0.002f;
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 2)
        {
            AttackVal += AttackVal * 0.025f;
            SmashVal += SmashVal * 0.025f;
            StrikeVal += StrikeVal * 0.025f;
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 3)
        {
            AttackVal += AttackVal * 0.05f;
            SmashVal += SmashVal * 0.05f;
            StrikeVal += StrikeVal * 0.05f;
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 4)
        {
            AttackVal += AttackVal * 0.1f;
            SmashVal += SmashVal * 0.1f;
            StrikeVal += StrikeVal * 0.1f;
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 5)
        {
            AttackVal += AttackVal * 0.2f;
            SmashVal += SmashVal * 0.2f;
            StrikeVal += StrikeVal * 0.2f;
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 6)
        {
            AttackVal += AttackVal * 0.8f;
            SmashVal += SmashVal * 0.8f;
            StrikeVal += StrikeVal * 0.8f;
        }
        if (PlayerPrefs.GetInt("WeaponLoad") == 7)
        {
            AttackVal += AttackVal * 1.2f;
            SmashVal += SmashVal * 1.2f;
            StrikeVal += StrikeVal * 1.2f;
        }
        Anime = GameObject.Find("Boss").GetComponent<Animator>();
        Boss = GameObject.Find("Boss").GetComponent<NavMeshAgent>();
        BossAI = GameObject.Find("Boss").GetComponent<BossAIControl>();
        PAnime = GameObject.Find("Player").GetComponent<Animator>();
        Reward = GameObject.Find("Reward");
        RewardUI = Reward.GetComponent<Reward>();
        ThisHP = MaxHP;
        ThisGP = MaxGP;
        GroggyDelay = 0f;
        DeadDelay = 0f;
    }

    void Update()
    {
        if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
            return;
        if (Groggy)
        {
            GroggyDelay += Time.deltaTime;
            if (GroggyDelay > GPDelay)
            {
                Anime.SetBool("IsGrogging", false);
                Anime.SetBool("IsRoaring", true);
            }
            if (Anime.GetBool("IsRoaring")
            && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Roar"))
            {
                ThisGP = MaxGP;
                Groggy = false;
                GroggyDelay = 0f;
                Anime.SetBool("IsRoaring", false);
            }
        }
        if (!PAnime.GetCurrentAnimatorStateInfo(0).IsName("Attack")
            && !PAnime.GetCurrentAnimatorStateInfo(0).IsName("Smash")
            && !PAnime.GetCurrentAnimatorStateInfo(0).IsName("Strike")
            && !PAnime.GetCurrentAnimatorStateInfo(0).IsName("Dodge"))
            CheckCal = false;
    }

    void FixedUpdate()
    {
        UpdateHPStatus();
        UpdateGPStatus();
    }
    private void UpdateHPStatus()
    {
        if (ThisHP <= 0)
        {
            ThisHP = 0;
            HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, TotalH, Time.deltaTime * LerpSpeed);
            if (!Dead)
                RewardUI.BossReward();
            Dead = true;
            if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
            {
                Boss.SetDestination(GameObject.Find("Boss").transform.position);
                Groggy = false;
                Anime.SetBool("IsGrogging", false);
                Anime.SetBool("IsDead", true);
                Anime.Play("Dead");
            }
            if (Dead)
            {
                DeadDelay += Time.deltaTime;
                if (DeadDelay > 8f)
                {
                    SceneManager.LoadScene("Win");
                    Debug.Log("'Win' 화면으로 이동");
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
            if (TotalH >= 0.8f)
            {
                Phase.text = "1";
                PhaseVar = "1";
            }
            else if (TotalH >= 0.6f && TotalH < 0.8f)
            {
                Phase.text = "2";
                PhaseVar = "2";
            }
            else if (TotalH >= 0.4f && TotalH < 0.6f)
            {
                Phase.text = "3";
                PhaseVar = "3";
            }
            else if (TotalH >= 0.2f && TotalH < 0.4f)
            {
                Phase.text = "F";
                PhaseVar = "F";
            }
            else if (TotalH < 0.1f)
            {
                Phase.text = "B";
                PhaseVar = "B";
            }
        }
    }

    private void UpdateGPStatus()
    {
        if (ThisGP <= 0)
        {
            ThisGP = 0;
            GroggyBar.fillAmount = Mathf.Lerp(GroggyBar.fillAmount, 0, Time.deltaTime * LerpSpeed);
            Groggy = true;
            if (Groggy && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Groggy"))
            {
                if (Dead == true)
                    return;
                Anime.Play("Groggy");
                Anime.SetBool("IsGrogging", true);
                Anime.SetBool("IsWalking", false);
                Anime.SetBool("IsAttack1ing", false);
                Anime.SetBool("IsAttack2ing", false);
                Anime.SetBool("IsRoaring", false);
            }
        }
        if (ThisGP > MaxGP)
            ThisGP = MaxGP;
        else
        {
            TotalG = ThisGP / MaxGP;
            GP.text = string.Format("{0} / {1}", (int)ThisGP, MaxGP);
            if (TotalG != GroggyBar.fillAmount)
                GroggyBar.fillAmount = Mathf.Lerp(GroggyBar.fillAmount, TotalG, Time.deltaTime * LerpSpeed);
        }
    }

    public void CalDamage()
    {
        if (!CheckCal)
        {
            if (BossAI.Pattern1Set)
                return;
            if (PAnime.GetBool("IsAttacking"))
            {
                ThisHP -= AttackVal;
                ThisGP -= AttackVal * GroggyVal;
                Debug.Log("'Player'의 적중(Attack) 확인");
                CheckCal = true;
                if (!Groggy)
                    Anime.Play("Damage");
                BossAI.Damage = true;
            }
            else if (PAnime.GetBool("IsSmashing"))
            {
                ThisHP -= SmashVal;
                ThisGP -= SmashVal * GroggyVal;
                Debug.Log("'Player'의 적중(Smash) 확인");
                CheckCal = true;
                Anime.SetBool("IsAttack1ing", false);
                Anime.SetBool("IsAttack2ing", false);
                Anime.SetBool("IsAttack3ing", false);
                Anime.SetBool("IsJumpLing", false);
                Anime.SetBool("IsJumpRing", false);
                if (!Groggy)
                    Anime.Play("Damage");
                BossAI.Damage = true;
            }
            else if (PAnime.GetBool("IsStriking"))
            {
                ThisHP -= StrikeVal * GroggyVal;
                ThisGP -= StrikeVal;
                Debug.Log("'Player'의 적중(Strike) 확인");
                CheckCal = true;
                Anime.SetBool("IsAttack1ing", false);
                Anime.SetBool("IsAttack2ing", false);
                Anime.SetBool("IsAttack3ing", false);
                Anime.SetBool("IsJumpLing", false);
                Anime.SetBool("IsJumpRing", false);
                if (!Groggy)
                    Anime.Play("Damage");
                BossAI.Damage = true;
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
}
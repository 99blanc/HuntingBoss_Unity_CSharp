using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gurdian1UIControl : MonoBehaviour
{

    public static Gurdian1UIControl instance;
    public float MaxHP = 20000f;
    public float ThisHP;
    public float HPRegen = 2f;
    public float MaxGP = 4000f;
    public float ThisGP;
    public float MaxBP = 100f;
    public float ThisBP;
    public float GPDelay = 5f;
    public Text HP;
    public Image HealthBar;
    public Text GP;
    public Image GroggyBar;
    public Text BP;
    public Image BattleBar;
    public Text Phase;
    public bool Groggy = false;
    public bool Dead = false;
    public bool CheckCal = false;
    public string PhaseVar;
    private float TotalH;
    private float TotalG;
    private float TotalB;
    private float LerpSpeed = 1f;
    private float AttackVal = 110f;
    private float SmashVal = 154f;
    private float StrikeVal = 440f;
    private float GroggyVal = 0.75f;
    private float GroggyDelay;
    private float DeadDelay;
    private bool GroggyAnime;
    public GameObject Battle;
    Animator Anime;
    Animator PAnime;
    NavMeshAgent Gurdian1;
    Gurdian1AIControl Gurdian1AI;
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
        Anime = GameObject.Find("Gurdian1").GetComponent<Animator>();
        Gurdian1 = GameObject.Find("Gurdian1").GetComponent<NavMeshAgent>();
        Gurdian1AI = GameObject.Find("Gurdian1").GetComponent<Gurdian1AIControl>();
        PAnime = GameObject.Find("Player").GetComponent<Animator>();
        Reward = GameObject.Find("Reward");
        RewardUI = Reward.GetComponent<Reward>();
        ThisHP = MaxHP;
        ThisGP = MaxGP;
        ThisBP = 0;
        GroggyDelay = 0f;
        DeadDelay = 0f;
        Battle.SetActive(false);
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
                ThisGP = MaxGP;
                Groggy = false;
                GroggyDelay = 0f;
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
        UpdateBPStatus();
    }
    private void UpdateHPStatus()
    {
        if (ThisHP <= 0)
        {
            ThisHP = 0;
            HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, TotalH, Time.deltaTime * LerpSpeed);
            if (!Dead)
                RewardUI.Gurdian1Reward();
            Dead = true;
            if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
            {
                Gurdian1.SetDestination(GameObject.Find("Gurdian1").transform.position);
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
            GroggyAnime = true;
            if (Groggy && !Anime.GetCurrentAnimatorStateInfo(0).IsName("Groggy"))
            {
                if (Dead == true)
                    return;
                if (GroggyAnime)
                {
                    Anime.Play("Groggy");
                    GroggyAnime = false;
                }
                if (Gurdian1AI.Pattern2Set)
                    Gurdian1AI.Pattern2Hide = true;
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

    private void UpdateBPStatus()
    {
        if (ThisBP <= 0)
        {
            ThisBP = 0;
            BattleBar.fillAmount = Mathf.Lerp(BattleBar.fillAmount, 0, Time.deltaTime * LerpSpeed);
        }
        if (ThisBP > MaxBP)
        {
            ThisBP = MaxBP;
            Gurdian1AI.Pattern3Hide = true;
            return;
        }
        else
        {
            TotalB = ThisBP / MaxBP;
            BP.text = string.Format("{0} / {1}", (int)ThisBP, MaxBP);
            if (TotalB != BattleBar.fillAmount)
                BattleBar.fillAmount = Mathf.Lerp(BattleBar.fillAmount, TotalB, Time.deltaTime * LerpSpeed);
            if (ThisBP >= 85)
                Gurdian1AI.Pattern3Hide = false;
            ThisBP -= Time.deltaTime * 2.25f;
        }
    }

    public void CalDamage()
    {
        if (!CheckCal)
        {
            if (!Groggy && (Gurdian1AI.Pattern1Set || Gurdian1AI.Pattern3Set))
                return;
            if (!Groggy && PAnime.GetBool("IsAttacking"))
            {
                ThisHP -= AttackVal;
                ThisGP -= AttackVal * GroggyVal;
                Debug.Log("'Player'의 적중(Attack) 확인");
                CheckCal = true;
                Anime.SetBool("IsAttack1ing", false);
                Anime.SetBool("IsAttack2ing", false);
                if (!Groggy)
                    Anime.Play("Damage");
                Gurdian1AI.Damage = true;
            }
            else if (!Groggy && PAnime.GetBool("IsSmashing"))
            {
                ThisHP -= SmashVal;
                ThisGP -= SmashVal * GroggyVal;
                Debug.Log("'Player'의 적중(Smash) 확인");
                CheckCal = true;
                Anime.SetBool("IsAttack1ing", false);
                Anime.SetBool("IsAttack2ing", false);
                if (!Groggy)
                    Anime.Play("Damage");
                Gurdian1AI.Damage = true;
            }
            else if (!Groggy && PAnime.GetBool("IsStriking"))
            {
                ThisHP -= StrikeVal * GroggyVal;
                ThisGP -= StrikeVal;
                Debug.Log("'Player'의 적중(Strike) 확인");
                CheckCal = true;
                Anime.SetBool("IsAttack1ing", false);
                Anime.SetBool("IsAttack2ing", false);
                if (!Groggy)
                    Anime.Play("Damage");
                Gurdian1AI.Damage = true;
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

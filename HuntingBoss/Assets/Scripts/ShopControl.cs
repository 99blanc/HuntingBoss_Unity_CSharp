using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControl : MonoBehaviour
{
    public List<GameObject> Weapons = new List<GameObject>();
    public List<Text> Text = new List<Text>();
    public Image Image;
    public Text Info;
    public GameObject Infomation;
    private int[] WeaponBuy = new int[7];
    private int SReward1;
    private int SReward2;
    private int SReward3;
    private int SReward4;
    private int SReward5;
    private int SReward6;
    private int SReward7;
    private int SReward8;
    private int SReward9;
    private int SReward10;
    private int SGold;
    private float TextDelay;
    Reward Reward;
    PlayerWeapon PW;

    void Start()
    {
        TextDelay = 0f;
        Infomation.SetActive(false);
        Reward = GameObject.Find("Reward").GetComponent<Reward>();
        PW = GameObject.Find("Player").GetComponent<PlayerWeapon>();
        if (!PlayerPrefs.HasKey("SSetting"))
        {
            PlayerPrefs.SetInt("Setting", 1);
            PlayerPrefs.SetInt("Reward1", 0);
            PlayerPrefs.SetInt("Reward2", 0);
            PlayerPrefs.SetInt("Reward3", 0);
            PlayerPrefs.SetInt("Reward4", 0);
            PlayerPrefs.SetInt("Reward5", 0);
            PlayerPrefs.SetInt("Reward6", 0);
            PlayerPrefs.SetInt("Reward7", 0);
            PlayerPrefs.SetInt("Reward8", 0);
            PlayerPrefs.SetInt("Reward9", 0);
            PlayerPrefs.SetInt("Reward10", 0);
            PlayerPrefs.SetInt("Gold", 0);
            PlayerPrefs.SetInt("SSetting", 1);
            PlayerPrefs.SetInt("SReward1", 0);
            PlayerPrefs.SetInt("SReward2", 0);
            PlayerPrefs.SetInt("SReward3", 0);
            PlayerPrefs.SetInt("SReward4", 0);
            PlayerPrefs.SetInt("SReward5", 0);
            PlayerPrefs.SetInt("SReward6", 0);
            PlayerPrefs.SetInt("SReward7", 0);
            PlayerPrefs.SetInt("SReward8", 0);
            PlayerPrefs.SetInt("SReward9", 0);
            PlayerPrefs.SetInt("SReward10", 0);
            PlayerPrefs.SetInt("Weapon1", 1);
            PlayerPrefs.SetInt("Weapon2", 0);
            PlayerPrefs.SetInt("Weapon3", 0);
            PlayerPrefs.SetInt("Weapon4", 0);
            PlayerPrefs.SetInt("Weapon5", 0);
            PlayerPrefs.SetInt("Weapon6", 0);
            PlayerPrefs.SetInt("Weapon7", 0);
            PlayerPrefs.SetInt("SGold", 0);
            PlayerPrefs.SetInt("WeaponLoad", 1);
        }
        else
        {
            SReward1 = PlayerPrefs.GetInt("SReward1");
            SReward2 = PlayerPrefs.GetInt("SReward2");
            SReward3 = PlayerPrefs.GetInt("SReward3");
            SReward4 = PlayerPrefs.GetInt("SReward4");
            SReward5 = PlayerPrefs.GetInt("SReward5");
            SReward6 = PlayerPrefs.GetInt("SReward6");
            SReward7 = PlayerPrefs.GetInt("SReward7");
            SReward8 = PlayerPrefs.GetInt("SReward8");
            SReward9 = PlayerPrefs.GetInt("SReward9");
            SReward10 = PlayerPrefs.GetInt("SReward10");
            WeaponBuy[0] = PlayerPrefs.GetInt("Weapon1");
            WeaponBuy[1] = PlayerPrefs.GetInt("Weapon2");
            WeaponBuy[2] = PlayerPrefs.GetInt("Weapon3");
            WeaponBuy[3] = PlayerPrefs.GetInt("Weapon4");
            WeaponBuy[4] = PlayerPrefs.GetInt("Weapon5");
            WeaponBuy[5] = PlayerPrefs.GetInt("Weapon6");
            WeaponBuy[6] = PlayerPrefs.GetInt("Weapon7");
            SGold = PlayerPrefs.GetInt("SGold");
        }
    }

    void Update()
    {
        if (TextDelay > 0)
        {
            TextDelay += Time.deltaTime;
            if (TextDelay > 2.5f)
            {
                TextDelay = 0f;
                Infomation.SetActive(false);
            }
        }
    }

    public void OnClickWeapon1()
    {
        for (int i = 0; i < Weapons.Count; ++i)
            Weapons[i].SetActive(false);
        Weapons[0].SetActive(true);
        PlayerPrefs.SetInt("SReward1", 0);
        PlayerPrefs.SetInt("SReward2", 0);
        PlayerPrefs.SetInt("SReward3", 0);
        PlayerPrefs.SetInt("SReward4", 0);
        PlayerPrefs.SetInt("SReward5", 200);
        PlayerPrefs.SetInt("SReward6", 100);
        PlayerPrefs.SetInt("SReward7", 50);
        PlayerPrefs.SetInt("SReward8", 0);
        PlayerPrefs.SetInt("SReward9", 1);
        PlayerPrefs.SetInt("SReward10", 0);
        PlayerPrefs.SetInt("SGold", 2000);
        UpdateAmount();
    }

    public void OnClickWeapon1Buy()
    {
        if (PlayerPrefs.GetInt("Weapon1") == 1)
        {
            TextDelay += Time.deltaTime;
            Info.text = string.Format("이미 존재하는 무기입니다.");
            Infomation.SetActive(true);
            PlayerPrefs.SetInt("WeaponLoad", 1);
            return;
        }
        else
        {
            Reward.Reward1 = PlayerPrefs.GetInt("Reward1");
            Reward.Reward2 = PlayerPrefs.GetInt("Reward2");
            Reward.Reward3 = PlayerPrefs.GetInt("Reward3");
            Reward.Reward4 = PlayerPrefs.GetInt("Reward4");
            Reward.Reward5 = PlayerPrefs.GetInt("Reward5");
            Reward.Reward6 = PlayerPrefs.GetInt("Reward6");
            Reward.Reward7 = PlayerPrefs.GetInt("Reward7");
            Reward.Reward8 = PlayerPrefs.GetInt("Reward8");
            Reward.Reward9 = PlayerPrefs.GetInt("Reward9");
            Reward.Reward10 = PlayerPrefs.GetInt("Reward10");
            Reward.Gold = PlayerPrefs.GetInt("Gold");
            if (Reward.Reward1 >= 0 && Reward.Reward2 >= 0
                && Reward.Reward3 >= 0 && Reward.Reward4 >= 0
                && Reward.Reward5 >= 200 && Reward.Reward6 >= 100
                && Reward.Reward7 >= 50 && Reward.Reward8 >= 0
                && Reward.Reward9 >= 1 && Reward.Reward10 >= 0
                && Reward.Gold >= 2000)
            {
                PlayerPrefs.SetInt("Reward1", Reward.Reward1 - 0);
                PlayerPrefs.SetInt("Reward2", Reward.Reward2 - 0);
                PlayerPrefs.SetInt("Reward3", Reward.Reward3 - 0);
                PlayerPrefs.SetInt("Reward4", Reward.Reward4 - 0);
                PlayerPrefs.SetInt("Reward5", Reward.Reward5 - 200);
                PlayerPrefs.SetInt("Reward6", Reward.Reward6 - 100);
                PlayerPrefs.SetInt("Reward7", Reward.Reward7 - 50);
                PlayerPrefs.SetInt("Reward8", Reward.Reward8 - 0);
                PlayerPrefs.SetInt("Reward9", Reward.Reward9 - 1);
                PlayerPrefs.SetInt("Reward10", Reward.Reward10 - 0);
                PlayerPrefs.SetInt("Gold", Reward.Gold - 2000);
                TextDelay += Time.deltaTime;
                Info.text = string.Format("성공적으로 무기1를 구매하였습니다.");
                Infomation.SetActive(true);
                Reward.UpdateReward();
                PlayerPrefs.SetInt("WeaponLoad", 1);
                PlayerPrefs.SetInt("Weapon1", 1);
                return;
            }
            else
            {
                TextDelay += Time.deltaTime;
                Info.text = string.Format("재료 수량이 부족합니다.");
                Infomation.SetActive(true);
                return;
            }
        }
    }

    public void OnClickWeapon2()
    {
        for (int i = 0; i < Weapons.Count; ++i)
            Weapons[i].SetActive(false);
        Weapons[1].SetActive(true);
        PlayerPrefs.SetInt("SReward1", 0);
        PlayerPrefs.SetInt("SReward2", 1);
        PlayerPrefs.SetInt("SReward3", 0);
        PlayerPrefs.SetInt("SReward4", 5);
        PlayerPrefs.SetInt("SReward5", 1500);
        PlayerPrefs.SetInt("SReward6", 250);
        PlayerPrefs.SetInt("SReward7", 100);
        PlayerPrefs.SetInt("SReward8", 10);
        PlayerPrefs.SetInt("SReward9", 5);
        PlayerPrefs.SetInt("SReward10", 0);
        PlayerPrefs.SetInt("SGold", 20000);
        UpdateAmount();
    }

    public void OnClickWeapon2Buy()
    {
        for (int i = 0; i < Weapons.Count; ++i)
            Weapons[i].SetActive(false);
        Weapons[1].SetActive(true);
        if (PlayerPrefs.GetInt("Weapon2") == 1)
        {
            TextDelay += Time.deltaTime;
            Info.text = string.Format("이미 존재하는 무기입니다.");
            Infomation.SetActive(true);
            PlayerPrefs.SetInt("WeaponLoad", 2);
            return;
        }
        else
        {
            Reward.Reward1 = PlayerPrefs.GetInt("Reward1");
            Reward.Reward2 = PlayerPrefs.GetInt("Reward2");
            Reward.Reward3 = PlayerPrefs.GetInt("Reward3");
            Reward.Reward4 = PlayerPrefs.GetInt("Reward4");
            Reward.Reward5 = PlayerPrefs.GetInt("Reward5");
            Reward.Reward6 = PlayerPrefs.GetInt("Reward6");
            Reward.Reward7 = PlayerPrefs.GetInt("Reward7");
            Reward.Reward8 = PlayerPrefs.GetInt("Reward8");
            Reward.Reward9 = PlayerPrefs.GetInt("Reward9");
            Reward.Reward10 = PlayerPrefs.GetInt("Reward10");
            Reward.Gold = PlayerPrefs.GetInt("Gold");
            if (Reward.Reward1 >= 0 && Reward.Reward2 >= 1
                && Reward.Reward3 >= 0 && Reward.Reward4 >= 5
                && Reward.Reward5 >= 1500 && Reward.Reward6 >= 250
                && Reward.Reward7 >= 100 && Reward.Reward8 >= 10
                && Reward.Reward9 >= 5 && Reward.Reward10 >= 0
                && Reward.Gold >= 20000)
            {
                PlayerPrefs.SetInt("Reward1", Reward.Reward1 - 0);
                PlayerPrefs.SetInt("Reward2", Reward.Reward2 - 1);
                PlayerPrefs.SetInt("Reward3", Reward.Reward3 - 0);
                PlayerPrefs.SetInt("Reward4", Reward.Reward4 - 5);
                PlayerPrefs.SetInt("Reward5", Reward.Reward5 - 1500);
                PlayerPrefs.SetInt("Reward6", Reward.Reward6 - 250);
                PlayerPrefs.SetInt("Reward7", Reward.Reward7 - 100);
                PlayerPrefs.SetInt("Reward8", Reward.Reward8 - 10);
                PlayerPrefs.SetInt("Reward9", Reward.Reward9 - 5);
                PlayerPrefs.SetInt("Reward10", Reward.Reward10 - 0);
                PlayerPrefs.SetInt("Gold", Reward.Gold - 20000);
                TextDelay += Time.deltaTime;
                Info.text = string.Format("성공적으로 무기2를 구매하였습니다.");
                Infomation.SetActive(true);
                Reward.UpdateReward();
                PlayerPrefs.SetInt("WeaponLoad", 2);
                PlayerPrefs.SetInt("Weapon2", 1);
                return;
            }
            else
            {
                TextDelay += Time.deltaTime;
                Info.text = string.Format("재료 수량이 부족합니다.");
                Infomation.SetActive(true);
                return;
            }
        }
    }

    public void OnClickWeapon3()
    {
        for (int i = 0; i < Weapons.Count; ++i)
            Weapons[i].SetActive(false);
        Weapons[2].SetActive(true);
        PlayerPrefs.SetInt("SReward1", 1);
        PlayerPrefs.SetInt("SReward2", 0);
        PlayerPrefs.SetInt("SReward3", 0);
        PlayerPrefs.SetInt("SReward4", 5);
        PlayerPrefs.SetInt("SReward5", 5000);
        PlayerPrefs.SetInt("SReward6", 125);
        PlayerPrefs.SetInt("SReward7", 50);
        PlayerPrefs.SetInt("SReward8", 5);
        PlayerPrefs.SetInt("SReward9", 5);
        PlayerPrefs.SetInt("SReward10", 0);
        PlayerPrefs.SetInt("SGold", 15000);
        UpdateAmount();
    }

    public void OnClickWeapon3Buy()
    {
        for (int i = 0; i < Weapons.Count; ++i)
            Weapons[i].SetActive(false);
        Weapons[2].SetActive(true);
        if (PlayerPrefs.GetInt("Weapon3") == 1)
        {
            TextDelay += Time.deltaTime;
            Info.text = string.Format("이미 존재하는 무기입니다.");
            Infomation.SetActive(true);
            PlayerPrefs.SetInt("WeaponLoad", 3);
            return;
        }
        else
        {
            Reward.Reward1 = PlayerPrefs.GetInt("Reward1");
            Reward.Reward2 = PlayerPrefs.GetInt("Reward2");
            Reward.Reward3 = PlayerPrefs.GetInt("Reward3");
            Reward.Reward4 = PlayerPrefs.GetInt("Reward4");
            Reward.Reward5 = PlayerPrefs.GetInt("Reward5");
            Reward.Reward6 = PlayerPrefs.GetInt("Reward6");
            Reward.Reward7 = PlayerPrefs.GetInt("Reward7");
            Reward.Reward8 = PlayerPrefs.GetInt("Reward8");
            Reward.Reward9 = PlayerPrefs.GetInt("Reward9");
            Reward.Reward10 = PlayerPrefs.GetInt("Reward10");
            Reward.Gold = PlayerPrefs.GetInt("Gold");
            if (Reward.Reward1 >= 1 && Reward.Reward2 >= 0
                && Reward.Reward3 >= 0 && Reward.Reward4 >= 5
                && Reward.Reward5 >= 5000 && Reward.Reward6 >= 125
                && Reward.Reward7 >= 50 && Reward.Reward8 >= 5
                && Reward.Reward9 >= 5 && Reward.Reward10 >= 0
                && Reward.Gold >= 15000)
            {
                PlayerPrefs.SetInt("Reward1", Reward.Reward1 - 1);
                PlayerPrefs.SetInt("Reward2", Reward.Reward2 - 0);
                PlayerPrefs.SetInt("Reward3", Reward.Reward3 - 0);
                PlayerPrefs.SetInt("Reward4", Reward.Reward4 - 5);
                PlayerPrefs.SetInt("Reward5", Reward.Reward5 - 5000);
                PlayerPrefs.SetInt("Reward6", Reward.Reward6 - 125);
                PlayerPrefs.SetInt("Reward7", Reward.Reward7 - 50);
                PlayerPrefs.SetInt("Reward8", Reward.Reward8 - 5);
                PlayerPrefs.SetInt("Reward9", Reward.Reward9 - 5);
                PlayerPrefs.SetInt("Reward10", Reward.Reward10 - 0);
                PlayerPrefs.SetInt("Gold", Reward.Gold - 15000);
                TextDelay += Time.deltaTime;
                Info.text = string.Format("성공적으로 무기3를 구매하였습니다.");
                Infomation.SetActive(true);
                Reward.UpdateReward();
                PlayerPrefs.SetInt("WeaponLoad", 3);
                PlayerPrefs.SetInt("Weapon3", 1);
                return;
            }
            else
            {
                TextDelay += Time.deltaTime;
                Info.text = string.Format("재료 수량이 부족합니다.");
                Infomation.SetActive(true);
                return;
            }
        }
    }

    public void OnClickWeapon4()
    {
        for (int i = 0; i < Weapons.Count; ++i)
            Weapons[i].SetActive(false);
        Weapons[3].SetActive(true);
        PlayerPrefs.SetInt("SReward1", 1);
        PlayerPrefs.SetInt("SReward2", 1);
        PlayerPrefs.SetInt("SReward3", 25);
        PlayerPrefs.SetInt("SReward4", 25);
        PlayerPrefs.SetInt("SReward5", 10000);
        PlayerPrefs.SetInt("SReward6", 1000);
        PlayerPrefs.SetInt("SReward7", 500);
        PlayerPrefs.SetInt("SReward8", 10);
        PlayerPrefs.SetInt("SReward9", 20);
        PlayerPrefs.SetInt("SReward10", 10);
        PlayerPrefs.SetInt("SGold", 75000);
        UpdateAmount();
    }

    public void OnClickWeapon4Buy()
    {
        for (int i = 0; i < Weapons.Count; ++i)
            Weapons[i].SetActive(false);
        Weapons[3].SetActive(true);
        if (PlayerPrefs.GetInt("Weapon4") == 1)
        {
            TextDelay += Time.deltaTime;
            Info.text = string.Format("이미 존재하는 무기입니다.");
            Infomation.SetActive(true);
            PlayerPrefs.SetInt("WeaponLoad", 4);
            return;
        }
        else
        {
            Reward.Reward1 = PlayerPrefs.GetInt("Reward1");
            Reward.Reward2 = PlayerPrefs.GetInt("Reward2");
            Reward.Reward3 = PlayerPrefs.GetInt("Reward3");
            Reward.Reward4 = PlayerPrefs.GetInt("Reward4");
            Reward.Reward5 = PlayerPrefs.GetInt("Reward5");
            Reward.Reward6 = PlayerPrefs.GetInt("Reward6");
            Reward.Reward7 = PlayerPrefs.GetInt("Reward7");
            Reward.Reward8 = PlayerPrefs.GetInt("Reward8");
            Reward.Reward9 = PlayerPrefs.GetInt("Reward9");
            Reward.Reward10 = PlayerPrefs.GetInt("Reward10");
            Reward.Gold = PlayerPrefs.GetInt("Gold");
            if (Reward.Reward1 >= 1 && Reward.Reward2 >= 1
                && Reward.Reward3 >= 25 && Reward.Reward4 >= 25
                && Reward.Reward5 >= 10000 && Reward.Reward6 >= 1000
                && Reward.Reward7 >= 500 && Reward.Reward8 >= 10
                && Reward.Reward9 >= 20 && Reward.Reward10 >= 10
                && Reward.Gold >= 75000)
            {
                PlayerPrefs.SetInt("Reward1", Reward.Reward1 - 1);
                PlayerPrefs.SetInt("Reward2", Reward.Reward2 - 1);
                PlayerPrefs.SetInt("Reward3", Reward.Reward3 - 25);
                PlayerPrefs.SetInt("Reward4", Reward.Reward4 - 25);
                PlayerPrefs.SetInt("Reward5", Reward.Reward5 - 10000);
                PlayerPrefs.SetInt("Reward6", Reward.Reward6 - 1000);
                PlayerPrefs.SetInt("Reward7", Reward.Reward7 - 500);
                PlayerPrefs.SetInt("Reward8", Reward.Reward8 - 10);
                PlayerPrefs.SetInt("Reward9", Reward.Reward9 - 20);
                PlayerPrefs.SetInt("Reward10", Reward.Reward10 - 10);
                PlayerPrefs.SetInt("Gold", Reward.Gold - 75000);
                TextDelay += Time.deltaTime;
                Info.text = string.Format("성공적으로 무기4를 구매하였습니다.");
                Infomation.SetActive(true);
                Reward.UpdateReward();
                PlayerPrefs.SetInt("WeaponLoad", 4);
                PlayerPrefs.SetInt("Weapon4", 1);
                return;
            }
            else
            {
                TextDelay += Time.deltaTime;
                Info.text = string.Format("재료 수량이 부족합니다.");
                Infomation.SetActive(true);
                return;
            }
        }
    }

    public void OnClickWeapon5()
    {
        for (int i = 0; i < Weapons.Count; ++i)
            Weapons[i].SetActive(false);
        Weapons[4].SetActive(true);
        PlayerPrefs.SetInt("SReward1", 1);
        PlayerPrefs.SetInt("SReward2", 1);
        PlayerPrefs.SetInt("SReward3", 25);
        PlayerPrefs.SetInt("SReward4", 25);
        PlayerPrefs.SetInt("SReward5", 12500);
        PlayerPrefs.SetInt("SReward6", 2000);
        PlayerPrefs.SetInt("SReward7", 1000);
        PlayerPrefs.SetInt("SReward8", 100);
        PlayerPrefs.SetInt("SReward9", 0);
        PlayerPrefs.SetInt("SReward10", 10);
        PlayerPrefs.SetInt("SGold", 100000);
        UpdateAmount();
    }

    public void OnClickWeapon5Buy()
    {
        for (int i = 0; i < Weapons.Count; ++i)
            Weapons[i].SetActive(false);
        Weapons[4].SetActive(true);
        if (PlayerPrefs.GetInt("Weapon5") == 1)
        {
            TextDelay += Time.deltaTime;
            Info.text = string.Format("이미 존재하는 무기입니다.");
            Infomation.SetActive(true);
            PlayerPrefs.SetInt("WeaponLoad", 5);
            return;
        }
        else
        {
            Reward.Reward1 = PlayerPrefs.GetInt("Reward1");
            Reward.Reward2 = PlayerPrefs.GetInt("Reward2");
            Reward.Reward3 = PlayerPrefs.GetInt("Reward3");
            Reward.Reward4 = PlayerPrefs.GetInt("Reward4");
            Reward.Reward5 = PlayerPrefs.GetInt("Reward5");
            Reward.Reward6 = PlayerPrefs.GetInt("Reward6");
            Reward.Reward7 = PlayerPrefs.GetInt("Reward7");
            Reward.Reward8 = PlayerPrefs.GetInt("Reward8");
            Reward.Reward9 = PlayerPrefs.GetInt("Reward9");
            Reward.Reward10 = PlayerPrefs.GetInt("Reward10");
            Reward.Gold = PlayerPrefs.GetInt("Gold");
            if (Reward.Reward1 >= 1 && Reward.Reward2 >= 1
                && Reward.Reward3 >= 25 && Reward.Reward4 >= 25
                && Reward.Reward5 >= 12500 && Reward.Reward6 >= 2000
                && Reward.Reward7 >= 1000 && Reward.Reward8 >= 100
                && Reward.Reward9 >= 0 && Reward.Reward10 >= 10
                && Reward.Gold >= 100000)
            {
                PlayerPrefs.SetInt("Reward1", Reward.Reward1 - 1);
                PlayerPrefs.SetInt("Reward2", Reward.Reward2 - 1);
                PlayerPrefs.SetInt("Reward3", Reward.Reward3 - 25);
                PlayerPrefs.SetInt("Reward4", Reward.Reward4 - 25);
                PlayerPrefs.SetInt("Reward5", Reward.Reward5 - 12500);
                PlayerPrefs.SetInt("Reward6", Reward.Reward6 - 2000);
                PlayerPrefs.SetInt("Reward7", Reward.Reward7 - 1000);
                PlayerPrefs.SetInt("Reward8", Reward.Reward8 - 100);
                PlayerPrefs.SetInt("Reward9", Reward.Reward9 - 0);
                PlayerPrefs.SetInt("Reward10", Reward.Reward10 - 10);
                PlayerPrefs.SetInt("Gold", Reward.Gold - 100000);
                TextDelay += Time.deltaTime;
                Info.text = string.Format("성공적으로 무기5를 구매하였습니다.");
                Infomation.SetActive(true);
                Reward.UpdateReward();
                PlayerPrefs.SetInt("WeaponLoad", 5);
                PlayerPrefs.SetInt("Weapon5", 1);
                return;
            }
            else
            {
                TextDelay += Time.deltaTime;
                Info.text = string.Format("재료 수량이 부족합니다.");
                Infomation.SetActive(true);
                return;
            }
        }
    }

    public void OnClickWeapon6()
    {
        for (int i = 0; i < Weapons.Count; ++i)
            Weapons[i].SetActive(false);
        Weapons[5].SetActive(true);
        PlayerPrefs.SetInt("SReward1", 5);
        PlayerPrefs.SetInt("SReward2", 5);
        PlayerPrefs.SetInt("SReward3", 50);
        PlayerPrefs.SetInt("SReward4", 50);
        PlayerPrefs.SetInt("SReward5", 100000);
        PlayerPrefs.SetInt("SReward6", 10000);
        PlayerPrefs.SetInt("SReward7", 5000);
        PlayerPrefs.SetInt("SReward8", 100);
        PlayerPrefs.SetInt("SReward9", 25);
        PlayerPrefs.SetInt("SReward10", 25);
        PlayerPrefs.SetInt("SGold", 500000);
        UpdateAmount();
    }

    public void OnClickWeapon6Buy()
    {
        for (int i = 0; i < Weapons.Count; ++i)
            Weapons[i].SetActive(false);
        Weapons[5].SetActive(true);
        if (PlayerPrefs.GetInt("Weapon6") == 1)
        {
            TextDelay += Time.deltaTime;
            Info.text = string.Format("이미 존재하는 무기입니다.");
            Infomation.SetActive(true);
            PlayerPrefs.SetInt("WeaponLoad", 6);
            return;
        }
        else
        {
            Reward.Reward1 = PlayerPrefs.GetInt("Reward1");
            Reward.Reward2 = PlayerPrefs.GetInt("Reward2");
            Reward.Reward3 = PlayerPrefs.GetInt("Reward3");
            Reward.Reward4 = PlayerPrefs.GetInt("Reward4");
            Reward.Reward5 = PlayerPrefs.GetInt("Reward5");
            Reward.Reward6 = PlayerPrefs.GetInt("Reward6");
            Reward.Reward7 = PlayerPrefs.GetInt("Reward7");
            Reward.Reward8 = PlayerPrefs.GetInt("Reward8");
            Reward.Reward9 = PlayerPrefs.GetInt("Reward9");
            Reward.Reward10 = PlayerPrefs.GetInt("Reward10");
            Reward.Gold = PlayerPrefs.GetInt("Gold");
            if (Reward.Reward1 >= 5 && Reward.Reward2 >= 5
                && Reward.Reward3 >= 50 && Reward.Reward4 >= 50
                && Reward.Reward5 >= 100000 && Reward.Reward6 >= 10000
                && Reward.Reward7 >= 5000 && Reward.Reward8 >= 100
                && Reward.Reward9 >= 25 && Reward.Reward10 >= 25
                && Reward.Gold >= 500000)
            {
                PlayerPrefs.SetInt("Reward1", Reward.Reward1 - 5);
                PlayerPrefs.SetInt("Reward2", Reward.Reward2 - 5);
                PlayerPrefs.SetInt("Reward3", Reward.Reward3 - 50);
                PlayerPrefs.SetInt("Reward4", Reward.Reward4 - 50);
                PlayerPrefs.SetInt("Reward5", Reward.Reward5 - 100000);
                PlayerPrefs.SetInt("Reward6", Reward.Reward6 - 10000);
                PlayerPrefs.SetInt("Reward7", Reward.Reward7 - 5000);
                PlayerPrefs.SetInt("Reward8", Reward.Reward8 - 100);
                PlayerPrefs.SetInt("Reward9", Reward.Reward9 - 25);
                PlayerPrefs.SetInt("Reward10", Reward.Reward10 - 25);
                PlayerPrefs.SetInt("Gold", Reward.Gold - 500000);
                TextDelay += Time.deltaTime;
                Info.text = string.Format("성공적으로 무기6를 구매하였습니다.");
                Infomation.SetActive(true);
                Reward.UpdateReward();
                PlayerPrefs.SetInt("WeaponLoad", 6);
                PlayerPrefs.SetInt("Weapon6", 1);
                return;
            }
            else
            {
                TextDelay += Time.deltaTime;
                Info.text = string.Format("재료 수량이 부족합니다.");
                Infomation.SetActive(true);
                return;
            }
        }
    }

    public void OnClickWeapon7()
    {
        for (int i = 0; i < Weapons.Count; ++i)
            Weapons[i].SetActive(false);
        Weapons[6].SetActive(true);
        PlayerPrefs.SetInt("SReward1", 50);
        PlayerPrefs.SetInt("SReward2", 50);
        PlayerPrefs.SetInt("SReward3", 100);
        PlayerPrefs.SetInt("SReward4", 100);
        PlayerPrefs.SetInt("SReward5", 1000000);
        PlayerPrefs.SetInt("SReward6", 100000);
        PlayerPrefs.SetInt("SReward7", 50000);
        PlayerPrefs.SetInt("SReward8", 2500);
        PlayerPrefs.SetInt("SReward9", 100);
        PlayerPrefs.SetInt("SReward10", 100);
        PlayerPrefs.SetInt("SGold", 10000000);
        UpdateAmount();
    }

    public void OnClickWeapon7Buy()
    {
        for (int i = 0; i < Weapons.Count; ++i)
            Weapons[i].SetActive(false);
        Weapons[6].SetActive(true);
        if (PlayerPrefs.GetInt("Weapon7") == 1)
        {
            TextDelay += Time.deltaTime;
            Info.text = string.Format("이미 존재하는 무기입니다.");
            Infomation.SetActive(true);
            PlayerPrefs.SetInt("WeaponLoad", 7);
            return;
        }
        else
        {
            Reward.Reward1 = PlayerPrefs.GetInt("Reward1");
            Reward.Reward2 = PlayerPrefs.GetInt("Reward2");
            Reward.Reward3 = PlayerPrefs.GetInt("Reward3");
            Reward.Reward4 = PlayerPrefs.GetInt("Reward4");
            Reward.Reward5 = PlayerPrefs.GetInt("Reward5");
            Reward.Reward6 = PlayerPrefs.GetInt("Reward6");
            Reward.Reward7 = PlayerPrefs.GetInt("Reward7");
            Reward.Reward8 = PlayerPrefs.GetInt("Reward8");
            Reward.Reward9 = PlayerPrefs.GetInt("Reward9");
            Reward.Reward10 = PlayerPrefs.GetInt("Reward10");
            Reward.Gold = PlayerPrefs.GetInt("Gold");
            if (Reward.Reward1 >= 50 && Reward.Reward2 >= 50
                && Reward.Reward3 >= 100 && Reward.Reward4 >= 100
                && Reward.Reward5 >= 1000000 && Reward.Reward6 >= 100000
                && Reward.Reward7 >= 50000 && Reward.Reward8 >= 2500
                && Reward.Reward9 >= 100 && Reward.Reward10 >= 100
                && Reward.Gold >= 10000000)
            {
                PlayerPrefs.SetInt("Reward1", Reward.Reward1 - 50);
                PlayerPrefs.SetInt("Reward2", Reward.Reward2 - 50);
                PlayerPrefs.SetInt("Reward3", Reward.Reward3 - 100);
                PlayerPrefs.SetInt("Reward4", Reward.Reward4 - 100);
                PlayerPrefs.SetInt("Reward5", Reward.Reward5 - 1000000);
                PlayerPrefs.SetInt("Reward6", Reward.Reward6 - 100000);
                PlayerPrefs.SetInt("Reward7", Reward.Reward7 - 50000);
                PlayerPrefs.SetInt("Reward8", Reward.Reward8 - 2500);
                PlayerPrefs.SetInt("Reward9", Reward.Reward9 - 100);
                PlayerPrefs.SetInt("Reward10", Reward.Reward10 - 100);
                PlayerPrefs.SetInt("Gold", Reward.Gold - 10000000);
                TextDelay += Time.deltaTime;
                Info.text = string.Format("성공적으로 무기7를 구매하였습니다.");
                Infomation.SetActive(true);
                Reward.UpdateReward();
                PlayerPrefs.SetInt("WeaponLoad", 7);
                PlayerPrefs.SetInt("Weapon7", 1);
                return;
            }
            else
            {
                TextDelay += Time.deltaTime;
                Info.text = string.Format("재료 수량이 부족합니다.");
                Infomation.SetActive(true);
                return;
            }
        }
    }

    private void UpdateAmount()
    {
        SReward1 = PlayerPrefs.GetInt("SReward1");
        SReward2 = PlayerPrefs.GetInt("SReward2");
        SReward3 = PlayerPrefs.GetInt("SReward3");
        SReward4 = PlayerPrefs.GetInt("SReward4");
        SReward5 = PlayerPrefs.GetInt("SReward5");
        SReward6 = PlayerPrefs.GetInt("SReward6");
        SReward7 = PlayerPrefs.GetInt("SReward7");
        SReward8 = PlayerPrefs.GetInt("SReward8");
        SReward9 = PlayerPrefs.GetInt("SReward9");
        SReward10 = PlayerPrefs.GetInt("SReward10");
        SGold = PlayerPrefs.GetInt("SGold");
        Text[0].text = string.Format("{0}", SReward1);
        Text[1].text = string.Format("{0}", SReward2);
        Text[2].text = string.Format("{0}", SReward3);
        Text[3].text = string.Format("{0}", SReward4);
        Text[4].text = string.Format("{0}", SReward5);
        Text[5].text = string.Format("{0}", SReward6);
        Text[6].text = string.Format("{0}", SReward7);
        Text[7].text = string.Format("{0}", SReward8);
        Text[8].text = string.Format("{0}", SReward9);
        Text[9].text = string.Format("{0}", SReward10);
        Text[10].text = string.Format("{0} " + "G", SGold);
    }
}

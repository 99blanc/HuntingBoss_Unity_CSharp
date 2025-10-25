using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    public List<Text> Text = new List<Text>();
    public int Reward1;
    public int Reward2;
    public int Reward3;
    public int Reward4;
    public int Reward5;
    public int Reward6;
    public int Reward7;
    public int Reward8;
    public int Reward9;
    public int Reward10;
    public int Gold;
    private int RandomVal;
    private bool RandomCheck = false;
    ShopControl ShopControl;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Shop")
            ShopControl = GameObject.Find("ShopButton").GetComponent<ShopControl>();
        if (!PlayerPrefs.HasKey("Setting"))
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
            Reward1 = PlayerPrefs.GetInt("Reward1");
            Reward2 = PlayerPrefs.GetInt("Reward2");
            Reward3 = PlayerPrefs.GetInt("Reward3");
            Reward4 = PlayerPrefs.GetInt("Reward4");
            Reward5 = PlayerPrefs.GetInt("Reward5");
            Reward6 = PlayerPrefs.GetInt("Reward6");
            Reward7 = PlayerPrefs.GetInt("Reward7");
            Reward8 = PlayerPrefs.GetInt("Reward8");
            Reward9 = PlayerPrefs.GetInt("Reward9");
            Reward10 = PlayerPrefs.GetInt("Reward10");
            Gold = PlayerPrefs.GetInt("Gold");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
            UpdateReward();
        }
        if (SceneManager.GetActiveScene().name == "Shop")
        {
            UpdateReward();
            gameObject.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "Win")
        {
            UpdateReward();
            gameObject.SetActive(true);
        }
        else
            gameObject.SetActive(false);
    }

    public void BossReward()
    {
        if (!RandomCheck)
        {
            RandomCheck = true;
            RandomVal = Random.Range(0, 99);
            Debug.Log("'Reward', 보상 제공을 위한 난수(" + RandomVal + ") 생성");
            if (RandomCheck && RandomVal >= 0 && RandomVal <= 19)
            {
                RandomVal = Random.Range(0, 1);
                Debug.Log("'Reward', 보상 수령을 위한 난수(" + RandomVal + ") 생성");
                if (RandomVal == 0)
                {
                    Reward1 += Random.Range(1, 3);
                    PlayerPrefs.SetInt("Reward1", Reward1);
                    Reward3 += Random.Range(1, 10);
                    PlayerPrefs.SetInt("Reward3", Reward3);
                    Reward4 += Random.Range(1, 10);
                    PlayerPrefs.SetInt("Reward4", Reward4);
                    Reward5 += Random.Range(200, 1000);
                    PlayerPrefs.SetInt("Reward5", Reward5);
                    Reward6 += Random.Range(100, 200);
                    PlayerPrefs.SetInt("Reward6", Reward6);
                    Reward7 += Random.Range(50, 100);
                    PlayerPrefs.SetInt("Reward7", Reward7);
                    Reward8 += Random.Range(1, 20);
                    PlayerPrefs.SetInt("Reward8", Reward8);
                    Reward9 += Random.Range(1, 5);
                    PlayerPrefs.SetInt("Reward9", Reward9);
                    Gold += Random.Range(1000, 100000);
                    PlayerPrefs.SetInt("Gold", Gold);
                    RandomCheck = false;
                }
                if (RandomVal == 1)
                {
                    Reward2 += Random.Range(1, 3);
                    PlayerPrefs.SetInt("Reward2", Reward2);
                    Reward3 += Random.Range(1, 10);
                    PlayerPrefs.SetInt("Reward3", Reward3);
                    Reward4 += Random.Range(1, 10);
                    PlayerPrefs.SetInt("Reward4", Reward4);
                    Reward5 += Random.Range(200, 1000);
                    PlayerPrefs.SetInt("Reward5", Reward5);
                    Reward6 += Random.Range(100, 200);
                    PlayerPrefs.SetInt("Reward6", Reward6);
                    Reward7 += Random.Range(50, 100);
                    PlayerPrefs.SetInt("Reward7", Reward7);
                    Reward8 += Random.Range(1, 20);
                    PlayerPrefs.SetInt("Reward8", Reward8);
                    Reward9 += Random.Range(1, 5);
                    PlayerPrefs.SetInt("Reward9", Reward9);
                    Gold += Random.Range(1000, 100000);
                    PlayerPrefs.SetInt("Gold", Gold);
                    RandomCheck = false;
                }
                Debug.Log("'Reward', '명인' 보상 수령");
            }
            else if (RandomCheck && RandomVal >= 20 && RandomVal <= 50)
            {
                RandomVal = Random.Range(0, 1);
                Debug.Log("'Reward', 보상 수령을 위한 난수(" + RandomVal + ") 생성");
                if (RandomVal == 0)
                {
                    Reward3 += Random.Range(1, 5);
                    PlayerPrefs.SetInt("Reward3", Reward3);
                    Reward5 += Random.Range(200, 2000);
                    PlayerPrefs.SetInt("Reward5", Reward5);
                    Reward6 += Random.Range(100, 200);
                    PlayerPrefs.SetInt("Reward6", Reward6);
                    Reward7 += Random.Range(50, 100);
                    PlayerPrefs.SetInt("Reward7", Reward7);
                    Reward8 += Random.Range(1, 20);
                    PlayerPrefs.SetInt("Reward8", Reward8);
                    Reward9 += Random.Range(1, 3);
                    PlayerPrefs.SetInt("Reward9", Reward9);
                    Gold += Random.Range(1000, 10000);
                    PlayerPrefs.SetInt("Gold", Gold);
                    RandomCheck = false;
                }
                if (RandomVal == 1)
                {
                    Reward4 += Random.Range(1, 5);
                    PlayerPrefs.SetInt("Reward4", Reward4);
                    Reward5 += Random.Range(200, 2000);
                    PlayerPrefs.SetInt("Reward5", Reward5);
                    Reward6 += Random.Range(100, 200);
                    PlayerPrefs.SetInt("Reward6", Reward6);
                    Reward7 += Random.Range(50, 100);
                    PlayerPrefs.SetInt("Reward7", Reward7);
                    Reward8 += Random.Range(1, 20);
                    PlayerPrefs.SetInt("Reward8", Reward8);
                    Reward9 += Random.Range(1, 3);
                    PlayerPrefs.SetInt("Reward9", Reward9);
                    Gold += Random.Range(1000, 10000);
                    PlayerPrefs.SetInt("Gold", Gold);
                    RandomCheck = false;
                }
                Debug.Log("'Reward', '장인' 보상 수령");
            }
            else
            {
                RandomVal = Random.Range(0, 1);
                Debug.Log("'Reward', 보상 수령을 위한 난수(" + RandomVal + ") 생성");
                if (RandomVal == 0)
                {
                    Reward5 += Random.Range(200, 2000);
                    PlayerPrefs.SetInt("Reward5", Reward5);
                    Reward6 += Random.Range(100, 200);
                    PlayerPrefs.SetInt("Reward6", Reward6);
                    Reward7 += Random.Range(50, 100);
                    PlayerPrefs.SetInt("Reward7", Reward7);
                    Gold += Random.Range(1000, 10000);
                    PlayerPrefs.SetInt("Gold", Gold);
                    RandomCheck = false;
                }
                if (RandomVal == 1)
                {
                    Reward5 += Random.Range(200, 2000);
                    PlayerPrefs.SetInt("Reward5", Reward5);
                    Reward6 += Random.Range(100, 200);
                    PlayerPrefs.SetInt("Reward6", Reward6);
                    Reward7 += Random.Range(50, 100);
                    PlayerPrefs.SetInt("Reward7", Reward7);
                    Reward8 += Random.Range(1, 20);
                    PlayerPrefs.SetInt("Reward8", Reward8);
                    Gold += Random.Range(1000, 10000);
                    PlayerPrefs.SetInt("Gold", Gold);
                    RandomCheck = false;
                }
                Debug.Log("'Reward', '일반' 보상 수령");
            }
        }
        UpdateReward();
    }

    public void Gurdian1Reward()
    {
        if (!RandomCheck)
        {
            RandomCheck = true;
            RandomVal = Random.Range(0, 99);
            Debug.Log("'Reward', 보상 제공을 위한 난수(" + RandomVal +") 생성");
            if (RandomCheck && RandomVal >= 0 && RandomVal <= 19)
            {
                RandomVal = Random.Range(0, 1);
                Debug.Log("'Reward', 보상 수령을 위한 난수(" + RandomVal + ") 생성");
                if (RandomVal == 0)
                {
                    Reward1 += Random.Range(1, 3);
                    PlayerPrefs.SetInt("Reward1", Reward1);
                    Reward3 += Random.Range(1, 10);
                    PlayerPrefs.SetInt("Reward3", Reward3);
                    Reward4 += Random.Range(1, 10);
                    PlayerPrefs.SetInt("Reward4", Reward4);
                    Reward5 += Random.Range(200, 1000);
                    PlayerPrefs.SetInt("Reward5", Reward5);
                    Reward6 += Random.Range(100, 200);
                    PlayerPrefs.SetInt("Reward6", Reward6);
                    Reward7 += Random.Range(50, 100);
                    PlayerPrefs.SetInt("Reward7", Reward7);
                    Reward8 += Random.Range(1, 20);
                    PlayerPrefs.SetInt("Reward8", Reward8);
                    Reward10 += Random.Range(1, 5);
                    PlayerPrefs.SetInt("Reward10", Reward10);
                    Gold += Random.Range(10000, 100000);
                    PlayerPrefs.SetInt("Gold", Gold);
                    RandomCheck = false;
                }
                if (RandomVal == 1)
                {
                    Reward2 += Random.Range(1, 3);
                    PlayerPrefs.SetInt("Reward2", Reward2);
                    Reward3 += Random.Range(1, 10);
                    PlayerPrefs.SetInt("Reward3", Reward3);
                    Reward4 += Random.Range(1, 10);
                    PlayerPrefs.SetInt("Reward4", Reward4);
                    Reward5 += Random.Range(200, 1000);
                    PlayerPrefs.SetInt("Reward5", Reward5);
                    Reward6 += Random.Range(100, 200);
                    PlayerPrefs.SetInt("Reward6", Reward6);
                    Reward7 += Random.Range(50, 100);
                    PlayerPrefs.SetInt("Reward7", Reward7);
                    Reward8 += Random.Range(1, 20);
                    PlayerPrefs.SetInt("Reward8", Reward8);
                    Reward10 += Random.Range(1, 5);
                    PlayerPrefs.SetInt("Reward10", Reward10);
                    Gold += Random.Range(10000, 100000);
                    PlayerPrefs.SetInt("Gold", Gold);
                    RandomCheck = false;
                }
                Debug.Log("'Reward', '명인' 보상 수령");
            }
            else if (RandomCheck && RandomVal >= 20 && RandomVal <= 50)
            {
                RandomVal = Random.Range(0, 1);
                Debug.Log("'Reward', 보상 수령을 위한 난수(" + RandomVal + ") 생성");
                if (RandomVal == 0)
                {
                    Reward3 += Random.Range(1, 5);
                    PlayerPrefs.SetInt("Reward3", Reward3);
                    Reward5 += Random.Range(200, 2000);
                    PlayerPrefs.SetInt("Reward5", Reward5);
                    Reward6 += Random.Range(100, 200);
                    PlayerPrefs.SetInt("Reward6", Reward6);
                    Reward7 += Random.Range(50, 100);
                    PlayerPrefs.SetInt("Reward7", Reward7);
                    Reward8 += Random.Range(1, 20);
                    PlayerPrefs.SetInt("Reward8", Reward8);
                    Reward10 += Random.Range(1, 3);
                    PlayerPrefs.SetInt("Reward10", Reward10);
                    Gold += Random.Range(1000, 50000);
                    PlayerPrefs.SetInt("Gold", Gold);
                    RandomCheck = false;
                }
                if (RandomVal == 1)
                {
                    Reward4 += Random.Range(1, 5);
                    PlayerPrefs.SetInt("Reward4", Reward4);
                    Reward5 += Random.Range(200, 2000);
                    PlayerPrefs.SetInt("Reward5", Reward5);
                    Reward6 += Random.Range(100, 200);
                    PlayerPrefs.SetInt("Reward6", Reward6);
                    Reward7 += Random.Range(50, 100);
                    PlayerPrefs.SetInt("Reward7", Reward7);
                    Reward8 += Random.Range(1, 20);
                    PlayerPrefs.SetInt("Reward8", Reward8);
                    Reward10 += Random.Range(1, 3);
                    PlayerPrefs.SetInt("Reward10", Reward10);
                    Gold += Random.Range(1000, 50000);
                    PlayerPrefs.SetInt("Gold", Gold);
                    RandomCheck = false;
                }
                Debug.Log("'Reward', '장인' 보상 수령");
            }
            else
            {
                RandomVal = Random.Range(0, 1);
                Debug.Log("'Reward', 보상 수령을 위한 난수(" + RandomVal + ") 생성");
                if (RandomVal == 0)
                {
                    Reward5 += Random.Range(200, 2000);
                    PlayerPrefs.SetInt("Reward5", Reward5);
                    Reward6 += Random.Range(100, 200);
                    PlayerPrefs.SetInt("Reward6", Reward6);
                    Reward7 += Random.Range(50, 100);
                    PlayerPrefs.SetInt("Reward7", Reward7);
                    Gold += Random.Range(5000, 10000);
                    PlayerPrefs.SetInt("Gold", Gold);
                    RandomCheck = false;
                }
                if (RandomVal == 1)
                {
                    Reward5 += Random.Range(200, 2000);
                    PlayerPrefs.SetInt("Reward5", Reward5);
                    Reward6 += Random.Range(100, 200);
                    PlayerPrefs.SetInt("Reward6", Reward6);
                    Reward7 += Random.Range(50, 100);
                    PlayerPrefs.SetInt("Reward7", Reward7);
                    Reward8 += Random.Range(1, 20);
                    PlayerPrefs.SetInt("Reward8", Reward8);
                    Gold += Random.Range(5000, 10000);
                    PlayerPrefs.SetInt("Gold", Gold);
                    RandomCheck = false;
                }
                Debug.Log("'Reward', '일반' 보상 수령");
            }
        }
        UpdateReward();
    }

    public void UpdateReward()
    {
        Reward1 = PlayerPrefs.GetInt("Reward1");
        Reward2 = PlayerPrefs.GetInt("Reward2");
        Reward3 = PlayerPrefs.GetInt("Reward3");
        Reward4 = PlayerPrefs.GetInt("Reward4");
        Reward5 = PlayerPrefs.GetInt("Reward5");
        Reward6 = PlayerPrefs.GetInt("Reward6");
        Reward7 = PlayerPrefs.GetInt("Reward7");
        Reward8 = PlayerPrefs.GetInt("Reward8");
        Reward9 = PlayerPrefs.GetInt("Reward9");
        Reward10 = PlayerPrefs.GetInt("Reward10");
        Gold = PlayerPrefs.GetInt("Gold");
        Text[0].text = string.Format("{0}", Reward1);
        Text[1].text = string.Format("{0}", Reward2);
        Text[2].text = string.Format("{0}", Reward3);
        Text[3].text = string.Format("{0}", Reward4);
        Text[4].text = string.Format("{0}", Reward5);
        Text[5].text = string.Format("{0}", Reward6);
        Text[6].text = string.Format("{0}", Reward7);
        Text[7].text = string.Format("{0}", Reward8);
        Text[8].text = string.Format("{0}", Reward9);
        Text[9].text = string.Format("{0}", Reward10);
        Text[10].text = string.Format("{0} " + "G", Gold);
    }
}

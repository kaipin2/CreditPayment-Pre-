//ゲームで使用する変数やゲーム状況を監視するスクリプト
using System;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using Const;//定数を定義している

public class GameControllerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject FinishPanel; //ゲーム結果を表示するobject
    [SerializeField]
    private GameObject MoneyPanel; //残金を表示するobject
    [SerializeField]
    private GameObject MentalPanel; //精神力を表示するobject
    [SerializeField]
    private GameObject PhysicalPanel; //体力を表示するobject
    [SerializeField]
    private GameObject DayPanel; //体力を表示するobject
    [SerializeField]
    private GameObject SettingPanel; //ゲーム設定を表示するobject

    private int IntSalaryDay; // 給料日
    private int IntCreditDay; //クレジットカードの引き落とし日
    private int IntCreditMoney; //クレジットカード使用額
    private int IntMonthlyIncome; // 月収
    private int oldMoney;  //計算前の残金
    private int Money;  //残金
    private int oldMental;  //計算前の精神力
    private int Mental; //精神力
    private int oldPhysical;  //計算前の体力
    private int Physical; //体力
    private int CountDay; //経過日数
    private int IntGetMoney; //取得金額
    private int IntUseMoney; //消費金額
    private int IntGetMental; //取得精神力
    private int IntUseMental; //消費精神力
    private int IntGetPhysical; //取得体力
    private int IntUsePhysical; //消費体力
    private DateTime StartDay;　//初期の日付
    private DateTime CurrentDay;　//現在の日付
    private GameObject ObjGoodsControll; //全商品の親object
    private GameObject ObjChangeMoneyText; //残金の増減を表示するテキスト
    private GameObject ObjRestMoneyText; //残金の残りを表示するテキスト
    private GameObject ObjChangeMentalText; //精神力の増減を表示するテキスト
    private GameObject ObjRestMentalText; //精神力の残りを表示するテキスト
    private GameObject ObjChangePhysicalText; //体力の増減を表示するテキスト
    private GameObject ObjRestPhysicalText; //体力の残りを表示するテキスト
    private GameObject ObjYearText; //現在の年を表示するテキスト
    private GameObject ObjMonthText; //現在の月を表示するテキスト
    private GameObject ObjDayText; //現在の日を表示するテキスト
    private GameObject ObjSalaryText; //給料日を表示するテキスト
    private GameObject ObjWithdrawalText; //引き落とし日を表示するテキスト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        #region TextObject取得
            ObjChangeMoneyText = MoneyPanel.transform.Find(Const.CO.PlayerStatusChangeTextPass).gameObject;
            ObjRestMoneyText = MoneyPanel.transform.Find(Const.CO.PlayerStatusRestTextPass).gameObject;
            ObjChangeMentalText = MentalPanel.transform.Find(Const.CO.PlayerStatusChangeTextPass).gameObject;
            ObjRestMentalText = MentalPanel.transform.Find(Const.CO.PlayerStatusRestTextPass).gameObject;
            ObjChangePhysicalText = PhysicalPanel.transform.Find(Const.CO.PlayerStatusChangeTextPass).gameObject;
            ObjRestPhysicalText = PhysicalPanel.transform.Find(Const.CO.PlayerStatusRestTextPass).gameObject;
            ObjYearText = DayPanel.transform.Find(Const.CO.YearTextPass).gameObject;
            ObjMonthText = DayPanel.transform.Find(Const.CO.MonthTextPass).gameObject;
            ObjDayText = DayPanel.transform.Find(Const.CO.DayTextPass).gameObject;
            ObjSalaryText= SettingPanel.transform.Find(Const.CO.SalaryTextPass).gameObject;
            ObjWithdrawalText= SettingPanel.transform.Find(Const.CO.WithdrawalTextPass).gameObject;
        #endregion

        #region 変数設定
        StartDay = DateTime.Now; // 初期の日付を設定
            CurrentDay = StartDay; //現在の日付を初期化
            IntSalaryDay = 25; //給料日を設定
            IntCreditDay = 15;//クレジットカードの引き落とし日を設定
            IntMonthlyIncome = 10000; //月収を設定
            IntCreditMoney = 0; //クレジット使用額を初期化

            Money = IntMonthlyIncome; //初期残金を設定
            oldMoney = Money; //計算前残高を初期化
            Mental = 10; //初期精神力を設定
            oldMental = Mental; //計算前精神力を設定
            Physical = 20; //初期体力を設定
            oldPhysical = Physical; // 計算前体力を設定
            
            //スコア関係の値を初期化
            CountDay = 0; //経過日数を初期化
            IntGetMoney = 0;　//取得金額を初期化
            IntUseMoney = 0; //消費金額を初期化
            IntGetMental = 0; //取得精神力を初期化
            IntUseMental = 0; //消費精神力を初期化
            IntGetPhysical = 0; //取得体力を初期化
            IntUsePhysical = 0; //消費体力を初期化
        #endregion

        #region 設定値を画面に表示
        ObjSalaryText.GetComponent<TextMeshProUGUI>().text = "毎月" + IntSalaryDay + "日";
            ObjWithdrawalText.GetComponent<TextMeshProUGUI>().text = "毎月" + IntCreditDay + "日";
        #endregion

        FinishPanel.GetComponent<Canvas>().enabled = false; // 終了画面を非表示

        //商品の配置を行う
        ObjGoodsControll = this.transform.Find(Const.CO.GoodsControllObjectPass).gameObject;
        foreach (Transform child in ObjGoodsControll.transform)
        {
            ObjGoodsControll.GetComponent<GoodsStateDisplayUpdateScript>().StateUpdate_ALL(
                child,
                Const.CO.GoodsList[UnityEngine.Random.Range(0, UnityEngine.Random.Range(0, Const.CO.GoodsList.Count))]
            );
        }
        //初期ステータスを画面に表示
        DisPlayStatus();
    }


    //ステータスの更新処理
    public void UpdateStatus(int intPrice,string strEffectType,int intEffectNumber)
    {


        
        IntCreditMoney += intPrice;　//クレジット使用額に購入商品分を足す

        //商品の効果種類によって精神力か体力を更新
        if (strEffectType == Const.CO.PlayerMentalName)
        {
            Mental += intEffectNumber; //精神力を更新
            //精神力が減少する場合は消費変数を、増加する場合は取得変数を更新
            if (intEffectNumber < 0) IntUseMental += Mathf.Abs(intEffectNumber);
            else if (intEffectNumber > 0) IntGetMental += Mathf.Abs(intEffectNumber);
        }
        else if(strEffectType == Const.CO.PlayerPhysicalName)
        {
            Physical += intEffectNumber; //体力力を更新
            //体力が減少する場合は消費変数を、増加する場合は取得変数を更新
            if (intEffectNumber < 0) IntUsePhysical += Mathf.Abs(intEffectNumber);
            else if (intEffectNumber > 0) IntGetPhysical += Mathf.Abs(intEffectNumber);
        }

        //日付を更新する際の処理
        DayCountUp();
        
        
    }

    //画面上ステータスを反映する関数
    private void DisPlayStatus()
    {
        //ステータス残量を画面に表示
        ObjRestMoneyText.GetComponent<TextMeshProUGUI>().text = Const.CO.PlayerMoneyName+"：" + Money;
        ObjRestMentalText.GetComponent<TextMeshProUGUI>().text = Const.CO.PlayerMentalName+"：" + Mental;
        ObjRestPhysicalText.GetComponent<TextMeshProUGUI>().text = Const.CO.PlayerPhysicalName+"：" + Physical;
        
        //更新した日付を画面に表示
        ObjYearText.GetComponent<TextMeshProUGUI>().text = CurrentDay.Year.ToString()+"年";
        ObjMonthText.GetComponent<TextMeshProUGUI>().text = CurrentDay.Month.ToString() + "月";
        ObjDayText.GetComponent<TextMeshProUGUI>().text = CurrentDay.Day.ToString() + "日";

        //金額変化を表示
        StatusChange(Money - oldMoney, ObjChangeMoneyText);
        oldMoney = Money; //計算前残高を更新

        //精神力変化を表示
        StatusChange(Mental - oldMental, ObjChangeMentalText);
        oldMental = Mental; //計算前精神力を更新
        
        //体力変化を表示
        StatusChange(Physical - oldPhysical, ObjChangePhysicalText);
        oldPhysical = Physical; //計算前体力を更新

        //残金、精神力、体力のいずれかが負の場合ゲーム終了
        if(Money < 0 || Mental < 0 || Physical < 0) GameFinishCheck();

    }

    //日付を更新する際に処理する関数
    private void DayCountUp()
    {
        //経過日数に+1
        CountDay++;
        //現在の日付を更新
        CurrentDay = StartDay.AddDays(CountDay);
        
        //給料日の場合、給料を加算
        if (CurrentDay.Day == IntSalaryDay)
        {
            Debug.Log($"給料日");
            Money += IntMonthlyIncome;
            IntGetMoney += IntMonthlyIncome; //取得金額に給料を足す
        }
        //クレジットカード引き落とし日の場合、残金を更新
        if (CurrentDay.Day == IntCreditDay)
        {
            Debug.Log($"引き落とし日");
            Money -= IntCreditMoney;
            IntUseMoney += IntCreditMoney; //消費金額に現在のクレジット使用量を足す
            IntCreditMoney = 0; //クレジット使用量を初期化
        }

        //画面上のステータスを更新
        DisPlayStatus();
    }

    //ステータスが変化したときに行う処理
    private void StatusChange(int Diff,GameObject StatusText)
    {
        if (Diff > 0)
        {
            StatusText.GetComponent<TextMeshProUGUI>().text = "+" + Diff.ToString();
            StatusText.GetComponent<Animator>().Play(Const.CO.PlusCahngeStatusAnime);
        }
        else if (Diff < 0)
        {
            StatusText.GetComponent<TextMeshProUGUI>().text = Diff.ToString();
            StatusText.GetComponent<Animator>().Play(Const.CO.MinusCahngeStatusAnime);
        }
    }

    //ゲームが終了している場合の処理
    private void GameFinishCheck()
    {
        FinishPanel.GetComponent<Canvas>().enabled = true; // 終了画面を表示

        //スコア表示
        //経過日数
        FinishPanel.transform.Find(Const.CO.ScoreDayTextPass).gameObject.GetComponent<TextMeshProUGUI>().text = "経過日数："+CountDay.ToString()+"日";
        //取得給与
        FinishPanel.transform.Find(Const.CO.ScoreGetSalaryTextPass).gameObject.GetComponent<TextMeshProUGUI>().text = "取得給与：" + IntGetMoney.ToString() + "円";
        //購入金額
        FinishPanel.transform.Find(Const.CO.ScoreUseSalaryTextPass).gameObject.GetComponent<TextMeshProUGUI>().text = "購入金額：" + IntUseMoney.ToString() + "円";
        //取得精神力
        FinishPanel.transform.Find(Const.CO.ScoreGetMentalTextPass).gameObject.GetComponent<TextMeshProUGUI>().text = "取得精神力：" + IntGetMental.ToString();
        //消費精神力
        FinishPanel.transform.Find(Const.CO.ScoreUseMentalTextPass).gameObject.GetComponent<TextMeshProUGUI>().text = "消費精神力：" + IntUseMental.ToString();
        //取得体力
        FinishPanel.transform.Find(Const.CO.ScoreGetPhysicalTextPass).gameObject.GetComponent<TextMeshProUGUI>().text = "取得体力：" + IntGetPhysical.ToString();
        //消費体力
        FinishPanel.transform.Find(Const.CO.ScoreUsePhysicalTextPass).gameObject.GetComponent<TextMeshProUGUI>().text = "消費体力：" + IntUsePhysical.ToString();
        //スコア点数
        int intScore = ScoreNumberCal();
        FinishPanel.transform.Find(Const.CO.ScoreNumberTextPass).gameObject.GetComponent<TextMeshProUGUI>().text = intScore.ToString() + "点";
        //スコアランク
        FinishPanel.transform.Find(Const.CO.ScoreRankTextPass).gameObject.GetComponent<TextMeshProUGUI>().text = ScoreRankCal(intScore);
    }

    //スコア計算する関数
    private int ScoreNumberCal()
    {
        //出力用変数
        int intScore = 0;

        //経過日数×400
        intScore += 400 * CountDay;
        //購入金額×1%(少数点以下切り捨て)
        intScore += (int)(0.01f * IntUseMoney);
        //取得精神力×10%(少数点以下切り捨て)
        intScore += (int)(0.1f * IntGetMental);
        //取得体力×10%(少数点以下切り捨て)
        intScore += (int)(0.1f * IntGetPhysical);

        return intScore;
    }

    //スコアランクを計算する関数
    private string ScoreRankCal(int intScore)
    {
        string strScoreRank = "";
        Debug.Log(Const.CO.RankList[0, 1]);
        for (int Rank = 0;Rank <  Const.CO.RankList.GetLength(0);Rank++){
            
            if(intScore >= int.Parse(Const.CO.RankList[Rank, 1]))
            {
                strScoreRank = Const.CO.RankList[Rank, 0];
                break;
            }
        }
        return strScoreRank;
    }
}

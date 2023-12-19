using Godot;
using System;
using System.Collections.Generic;

public class KanjiCard : Sprite
{
    private Sprite Kanji;
    private Label OnyomiLabel;
    private Label KunyomiLabel;
    private Label MeaningLabel;
    private bool IsLabelsHidden;

    [Signal] delegate void CardClickedSignal(KanjiCard _card);

    private Dictionary<string,string>[] KanjiArray = new Dictionary<string,string>[100]
    {
        new Dictionary<string,string>
        {
            {"Onyomi" , "nichi"},
            {"Kunyomi", "hi"},
            {"Meaning", "day"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "ichi"},
            {"Kunyomi", "hito(tsu)"},
            {"Meaning", "one"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "koku"},
            {"Kunyomi", "kuni"},
            {"Meaning", "country"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "kai"},
            {"Kunyomi", "a(u)"},
            {"Meaning", "meeting"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "nin"},
            {"Kunyomi", "hito"},
            {"Meaning", "person"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "nen"},
            {"Kunyomi", "toshi"},
            {"Meaning", "year"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "dai"},
            {"Kunyomi", "oo(kii)"},
            {"Meaning", "big"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "juu"},
            {"Kunyomi", "tou"},
            {"Meaning", "ten"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "ni"},
            {"Kunyomi", "futa(tsu)"},
            {"Meaning", "two"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "hon"},
            {"Kunyomi", "moto"},
            {"Meaning", "book"}
        },
        //10
        new Dictionary<string,string>
        {
            {"Onyomi" , "chuu"},
            {"Kunyomi", "naka"},
            {"Meaning", "inside"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "chou"},
            {"Kunyomi", "naga(i)"},
            {"Meaning", "long"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "shutsu"},
            {"Kunyomi", "de(ru)"},
            {"Meaning", "exit"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "san"},
            {"Kunyomi", "mi(tsu)"},
            {"Meaning", "three"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "dou"},
            {"Kunyomi", "onaji"},
            {"Meaning", "agree"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "ji"},
            {"Kunyomi", "toki"},
            {"Meaning", "time"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "sei"},
            {"Kunyomi", "matsurigoto"},
            {"Meaning", "politics"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "ji"},
            {"Kunyomi", "koto"},
            {"Meaning", "matter"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "ji"},
            {"Kunyomi", "mizuka(ra)"},
            {"Meaning", "oneself"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "kou"},
            {"Kunyomi", "i(ku)"},
            {"Meaning", "journey"}
        },
        //20
        new Dictionary<string,string>
        {
            {"Onyomi" , "sha"},
            {"Kunyomi", "yashiro"},
            {"Meaning", "office"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "ken"},
            {"Kunyomi", "mi(ru)"},
            {"Meaning", "see"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "getsu"},
            {"Kunyomi", "tsuki"},
            {"Meaning", "moon"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "bun"},
            {"Kunyomi", "wa(keru)"},
            {"Meaning", "understand"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "gi"},
            {"Kunyomi", ""},
            {"Meaning", "debate"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "go"},
            {"Kunyomi", "ato"},
            {"Meaning", "later"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "zen"},
            {"Kunyomi", "mae"},
            {"Meaning", "before"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "min"},
            {"Kunyomi", "tami"},
            {"Meaning", "people"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "sei"},
            {"Kunyomi", "i(kiru)"},
            {"Meaning", "life"}
        },
        //30
        new Dictionary<string,string>
        {
            {"Onyomi" , "ren"},
            {"Kunyomi", "tsura(naru)"},
            {"Meaning", "take along"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "go"},
            {"Kunyomi", "itsu(tsu)"},
            {"Meaning", "five"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "hatsu, hotsu"},
            {"Kunyomi", ""},
            {"Meaning", "departure"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "kan"},
            {"Kunyomi", "aida"},
            {"Meaning", "space"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "jou"},
            {"Kunyomi", "ue"},
            {"Meaning", "above"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "tai"},
            {"Kunyomi", ""},
            {"Meaning", "opposite"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "bu"},
            {"Kunyomi", ""},
            {"Meaning", "section"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "tou"},
            {"Kunyomi", "higashi"},
            {"Meaning", "east"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "sha"},
            {"Kunyomi", "mono"},
            {"Meaning", "someone"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "tou"},
            {"Kunyomi", "nakama"},
            {"Meaning", "party"}
        },
        //40
        new Dictionary<string,string>
        {
            {"Onyomi" , "chi"},
            {"Kunyomi", ""},
            {"Meaning", "earth"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "gou"},
            {"Kunyomi", "a(u)"},
            {"Meaning", "fit"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "shi"},
            {"Kunyomi", "ichi"},
            {"Meaning", "market"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "gyou"},
            {"Kunyomi", "waza"},
            {"Meaning", "business"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "nai"},
            {"Kunyomi", "uchi"},
            {"Meaning", "inside"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "sou"},
            {"Kunyomi", "ai"},
            {"Meaning", "together"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "hou"},
            {"Kunyomi", "kata"},
            {"Meaning", "direction"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "shi"},
            {"Kunyomi", "yo(tsu)"},
            {"Meaning", "four"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "tei"},
            {"Kunyomi", "sada(meru)"},
            {"Meaning", "determine"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "kon"},
            {"Kunyomi", "ima"},
            {"Meaning", "now"}
        },
        //50
        new Dictionary<string,string>
        {
            {"Onyomi" , "kai"},
            {"Kunyomi", "mawa(su)"},
            {"Meaning", "times"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "shin"},
            {"Kunyomi", "atara(shii)"},
            {"Meaning", "new"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "jou"},
            {"Kunyomi", "ba"},
            {"Meaning", "location"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "kin"},
            {"Kunyomi", "kane"},
            {"Meaning", "gold"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "in"},
            {"Kunyomi", ""},
            {"Meaning", "employee"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "kyuu"},
            {"Kunyomi", "kokono(tsu)"},
            {"Meaning", "nine"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "nyuu"},
            {"Kunyomi", "i(ru)"},
            {"Meaning", "enter"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "sen"},
            {"Kunyomi", "era(bu)"},
            {"Meaning", "elect"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "ritsu"},
            {"Kunyomi", "ta(tsu)"},
            {"Meaning", "stand up"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "kai"},
            {"Kunyomi", "hira(ku)"},
            {"Meaning", "open"}
        },
        //60
        new Dictionary<string,string>
        {
            {"Onyomi" , "shu"},
            {"Kunyomi", "te"},
            {"Meaning", "hand"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "bei"},
            {"Kunyomi", "kome"},
            {"Meaning", "rice"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "ryoku"},
            {"Kunyomi", "chikara"},
            {"Meaning", "power"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "gaku"},
            {"Kunyomi", "mana(bu)"},
            {"Meaning", "study"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "mon"},
            {"Kunyomi", "to(u)"},
            {"Meaning", "question"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "kou"},
            {"Kunyomi", "taka(i)"},
            {"Meaning", "tall"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "dai"},
            {"Kunyomi", "ka(wari)"},
            {"Meaning", "substitute"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "mei"},
            {"Kunyomi", "aka(rui)"},
            {"Meaning", "bright"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "jitsu"},
            {"Kunyomi", "mi"},
            {"Meaning", "reality"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "en"},
            {"Kunyomi", "maru(i)"},
            {"Meaning", "circle"}
        },
        //70
        new Dictionary<string,string>
        {
            {"Onyomi" , "kan"},
            {"Kunyomi", "seki"},
            {"Meaning", "connection"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "ketsu"},
            {"Kunyomi", "ki(meru)"},
            {"Meaning", "decide"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "shi"},
            {"Kunyomi", "ko"},
            {"Meaning", "child"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "dou"},
            {"Kunyomi", "ugo(ku)"},
            {"Meaning", "move"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "kyou"},
            {"Kunyomi", "miyako"},
            {"Meaning", "capital"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "zen"},
            {"Kunyomi", "matta(ku)"},
            {"Meaning", "complete"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "boku"},
            {"Kunyomi", "me"},
            {"Meaning", "eye"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "hyou"},
            {"Kunyomi", "omote"},
            {"Meaning", "surface"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "sen"},
            {"Kunyomi", "tataka(u)"},
            {"Meaning", "battle"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "kei"},
            {"Kunyomi", "he(ru)"},
            {"Meaning", "expire"}
        },
        //80
        new Dictionary<string,string>
        {
            {"Onyomi" , "tsuu"},
            {"Kunyomi", "too(ru)"},
            {"Meaning", "pass through avenue"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "gai"},
            {"Kunyomi", "soto"},
            {"Meaning", "outside"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "sai"},
            {"Kunyomi", "motto(mo)"},
            {"Meaning", "most"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "gen"},
            {"Kunyomi", "koto"},
            {"Meaning", "word"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "shi"},
            {"Kunyomi", "uji"},
            {"Meaning", "clan"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "gen"},
            {"Kunyomi", "arawa(reru)"},
            {"Meaning", "present"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "ri"},
            {"Kunyomi", ""},
            {"Meaning", "logic"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "chou"},
            {"Kunyomi", "shira(beru)"},
            {"Meaning", "tune"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "tai"},
            {"Kunyomi", "karada"},
            {"Meaning", "body"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "ka"},
            {"Kunyomi", "ba(keru)"},
            {"Meaning", "delude"}
        },
        //90
        new Dictionary<string,string>
        {
            {"Onyomi" , "den"},
            {"Kunyomi", "ta"},
            {"Meaning", "rice field"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "tou"},
            {"Kunyomi", "a(taru)"},
            {"Meaning", "hit"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "hachi"},
            {"Kunyomi", "ya(tsu)"},
            {"Meaning", "eight"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "roku"},
            {"Kunyomi", "mu(tsu)"},
            {"Meaning", "six"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "yaku"},
            {"Kunyomi", "tsuzu(maru)"},
            {"Meaning", "promise"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "shu"},
            {"Kunyomi", "omo"},
            {"Meaning", "master"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "dai"},
            {"Kunyomi", ""},
            {"Meaning", "subject"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "ka"},
            {"Kunyomi", "shita"},
            {"Meaning", "below"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "shu"},
            {"Kunyomi", "kubi"},
            {"Meaning", "neck"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "i"},
            {"Kunyomi", ""},
            {"Meaning", "heart"}
        },
        new Dictionary<string,string>
        {
            {"Onyomi" , "hou"},
            {"Kunyomi", "nori"},
            {"Meaning", "law"}
        },
    };

    public override void _Ready()
    {
        IsLabelsHidden = true;
        Kanji = GetNode<Sprite>("Kanji");
        OnyomiLabel = GetNode<Label>("Labels/ReadingLabels/OnyomiLabel");
        KunyomiLabel = GetNode<Label>("Labels/ReadingLabels/KunyomiLabel");
        MeaningLabel = GetNode<Label>("Labels/MeaningLabel");

        Connect("CardClickedSignal", GetParent().GetParent(), "MoveCard");
    }

    private void SetTexture(int characterNumber)
    {
        Kanji.Frame = characterNumber;
    }

    private void SetLabel(int characterNumber)
    {
        SetOnyomi(characterNumber);
        SetKunyomi(characterNumber);
        SetMeaning(characterNumber);
    }

    private void SetOnyomi(int characterNumber)
    {
        OnyomiLabel.Text = KanjiArray[characterNumber]["Onyomi"];
    }

    private void SetKunyomi(int characterNumber)
    {
        KunyomiLabel.Text = KanjiArray[characterNumber]["Kunyomi"];
    }

    private void SetMeaning(int characterNumber)
    {
        MeaningLabel.Text = KanjiArray[characterNumber]["Meaning"];
    }

    public void SetCard(int characterNumber)
    {
        SetTexture(characterNumber);
        SetLabel(characterNumber);
    }

    public void MoveCardX(float xdelta)
    {
        Position = new Vector2(xdelta, Position.y);
    }

    public void MoveCardY(float ydelta)
    {
        Position = new Vector2(Position.x, ydelta);
    }

    public void ShowKanjiCard()
    {
        if (IsLabelsHidden)
        {
            OnyomiLabel.Visible = true;
            KunyomiLabel.Visible = true;
            MeaningLabel.Visible = true;
            IsLabelsHidden = false;
        }
        else
        {
            EmitSignal("CardClickedSignal", this);
        }
    }

    public void HideKanjiCard()
    {
        OnyomiLabel.Visible = false;
        KunyomiLabel.Visible = false;
        MeaningLabel.Visible = false;
        IsLabelsHidden = true;
    }

    public void KanjiClickedInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton && 
            ((InputEventMouseButton)@event).ButtonIndex == (int)ButtonList.Left && 
            ((InputEventMouseButton)@event).Pressed)
        {
            if (GetRect().HasPoint(ToLocal(((InputEventMouseButton)@event).Position)))
            {
                if (GetPositionInParent() != GetParent().GetChildCount() - 1)
                {
                    return;
                }
                else
                {
                    ShowKanjiCard();
                }
            }
        }
    }
}

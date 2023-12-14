using Godot;
using System;
using System.Collections.Generic;

public class Card : Sprite
{
    static Random random = new Random();
    public Texture CharSheet;
    public Sprite Char;
    public Label Label;
    private Tween twn;

    [Signal] delegate void RemoveCardSignal(Card _card);

    public Dictionary<int,string> characters = new Dictionary<int, string>
    {
        //hiragana 46
        {0,"a"},{1,"ka"},{2,"sa"},{3,"ta"},{4,"na"},{5,"ha"},{6,"ma"},{7,"ya"},{8,"ra"},{9,"wa"},
        {10,"i"},{11,"ki"},{12,"shi"},{13,"chi"},{14,"ni"},{15,"hi"},{16,"mi"},{17,"n"},{18,"ri"},{19,"ro"},
        {20,"u"},{21,"ku"},{22,"su"},{23,"tsu"},{24,"nu"},{25,"fu"},{26,"mu"},{27,"yu"},{28,"ru"},{29,"wo"},
        {30,"e"},{31,"ke"},{32,"se"},{33,"te"},{34,"ne"},{35,"he"},{36,"me"},{37,"yo"},{38,"re"},{39,"mo"},
        {40,"o"},{41,"ko"},{42,"so"},{43,"to"},{44,"no"},{45,"ho"},

        //katakana 46
        {50,"a"},{51,"ka"},{52,"sa"},{53,"ta"},{54,"na"},{55,"ha"},{56,"ma"},{57,"ya"},{58,"ra"},{59,"wa"},
        {60,"n"},{61,"i"},{62,"ki"},{63,"shi"},{64,"chi"},{65,"ni"},{66,"hi"},{67,"mi"},{68,"ri"},{69,"u"},
        {70,"ku"},{71,"su"},{72,"tsu"},{73,"nu"},{74,"fu"},{75,"mu"},{76,"yu"},{77,"ru"},{78,"e"},{79,"ke"},
        {80,"se"},{81,"te"},{82,"ne"},{83,"he"},{84,"me"},{85,"re"},{86,"o"},{87,"ko"},{88,"so"},{89,"to"},
        {90,"no"},{91,"ho"},{92,"mo"},{93,"yo"},{94,"ro"},{95,"wo"},
        //count: 92
    };

    public override void _Ready()
    {
        base._Ready();
        twn = GetNode<Tween>("Tween");
        Char = GetNode<Sprite>("Char");
        Label = GetNode<Label>("Label");
        CharSheet = ResourceLoader.Load<Texture>("res://hiragana_to_katakana_.png");
        
        Connect("RemoveCardSignal", GetParent().GetParent(), "RemoveCard");
    }

    public void SignalRemoveCard()
    {
        EmitSignal("RemoveCardSignal", this);
    }

    public void Resize(float size)
    {
        twn.InterpolateProperty(this, "scale", this.Scale, new Vector2(size,size), 0.5f);
        twn.Start();
    }

    public void SetTexture(int characterNumber)
    {
        Char.Frame = characterNumber;
    }

    public void SetLabel(int characterNumber)
    {
        Label.Text = characters[characterNumber];
    }

    public void SetCard(int cardNumber)
    {
        SetTexture(cardNumber);
        SetLabel(cardNumber);
    }

    public void ShowCard()
    {
        if (Label.Visible)
        {
            SignalRemoveCard();
        }
        else
        {
            Label.Visible = true;
        }
    }

    public void HideLabel()
    {
        Label.Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
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
                    ShowCard();
                }
            }
        }
    }
}

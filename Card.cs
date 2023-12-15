using Godot;
using System;
using System.Collections.Generic;

public class Card : Sprite
{
    // static Random random = new Random();
    public Texture CharSheet;
    public Sprite Char;
    public Label Label;
    private Tween twn;

    [Signal] delegate void RemoveCardSignal(Card _card);

    Dictionary<int,string> characters = new Dictionary<int, string>
    {
        //hiragana + katakana
        {0,"a"},{1,"i"},{2,"su"},{3,"te"},{4,"ho"},
        {5,"ka"},{6,"ki"},{7,"tsu"},{8,"ne"},{9,"mo"},
        {10,"sa"},{11,"shi"},{12,"nu"},{13,"he"},{14,"yo"},
        {15,"ta"},{16,"chi"},{17,"fu"},{18,"me"},{19,"ro"},
        {20,"na"},{21,"ni"},{22,"mu"},{23,"re"},{24,"wo"},
        {25,"ha"},{26,"hi"},{27,"yu"},{28,"o"},{29,"n"},
        {30,"ma"},{31,"mi"},{32,"ru"},{33,"ko"},{34,"ku"},
        {35,"ya"},{36,"ri"},{37,"e"},{38,"so"},{39,"se"},
        {40,"ra"},{41,"u"},{42,"ke"},{43,"to"},{44,"no"},
        {45,"wa"}
        //count: 46
    };


    public override void _Ready()
    {
        base._Ready();
        twn = GetNode<Tween>("Tween");
        Char = GetNode<Sprite>("Char");
        Label = GetNode<Label>("Label");
        CharSheet = ResourceLoader.Load<Texture>("res://hiragana_katakana_duo.png");
        
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

    public void CardInput(InputEvent @event)
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
                    ShowCard();
                }
            }
        }
    }
}

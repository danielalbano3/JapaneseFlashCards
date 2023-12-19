using Godot;
using System;
using System.Collections.Generic;

public class Card : Sprite
{
    protected Texture CharacterSheet;
    private Sprite Char;
    private Label Label;
    protected Tween twn;

    [Signal] delegate void RemoveCardSignal(Card _card);

    private string[] HiraganaKatakana = new string[46]
    {
        "a","i","su","te","ho",
        "ka","ki","tsu","ne","mo",
        "sa","shi","nu","he","yo",
        "ta","chi","fu","me","ro",
        "na","ni","mu","re","wo",
        "ha","hi","yu","o","n",
        "ma","mi","ru","ko","ku",
        "ya","ri","e","so","se",
        "ra","u","ke","to","no",
        "wa"
    };

    public override void _Ready()
    {
        base._Ready();
        twn = GetNode<Tween>("Tween");
        Char = GetNode<Sprite>("Char");
        Label = GetNode<Label>("Label");
        CharacterSheet = ResourceLoader.Load<Texture>("res://hiragana_katakana_duo.png");
        
        Connect("RemoveCardSignal", GetParent().GetParent(), "RemoveCard");
    }

    public void SignalRemoveCard()
    {
        EmitSignal("RemoveCardSignal", this);
    }

    protected void Resize(float size)
    {
        twn.InterpolateProperty(this, "scale", this.Scale, new Vector2(size,size), 0.5f);
        twn.Start();
    }

    protected virtual void SetTexture(int characterNumber)
    {
        Char.Frame = characterNumber;
    }

    protected virtual void SetLabel(int characterNumber)
    {
        Label.Text = HiraganaKatakana[characterNumber];
    }

    public virtual void SetCard(int cardNumber)
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

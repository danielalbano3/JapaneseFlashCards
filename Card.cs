using Godot;
using System;

public class Card : Sprite
{
    public Texture HiraganaSheet;
    public Texture KatakanaSheet;
    public Sprite Char;
    public Label Label;
    public override void _Ready()
    {
        base._Ready();
        int randomNumber = RandomChar();
        GD.Print(randomNumber);
        Char = GetNode<Sprite>("Char");
        Label = GetNode<Label>("Label");
        HiraganaSheet = ResourceLoader.Load<Texture>("res://hiragana_sheet.png");
        Char.Texture = HiraganaSheet;
        Char.Frame = randomNumber;
    }

    public int RandomChar()
    {
        Random rand = new Random();
        int newNumber;
        do 
        {
            newNumber = rand.Next(0,50);
        } 
        while (newNumber == 18 || newNumber == 20 || newNumber == 38 || newNumber == 40);
        return newNumber;
    }
}

using Godot;
using System;

public class Menu : Control
{
    private Button HiraganaKatakanaBtn;
    private Button KanjiBtn;
    private Button ExitBtn;

    private PackedScene HiraganaKatakanaScene;

    public override void _Ready()
    {
        base._Ready();
        HiraganaKatakanaScene = ResourceLoader.Load<PackedScene>("res://Main.tscn");
        HiraganaKatakanaBtn = GetNode<Button>("MarginContainer/VBoxContainer/Buttons/HKButton");
        KanjiBtn = GetNode<Button>("MarginContainer/VBoxContainer/Buttons/KanjiButton");
        ExitBtn = GetNode<Button>("MarginContainer/VBoxContainer/Buttons/ExitButton");

        HiraganaKatakanaBtn.Connect("pressed", this, "GoToHiraganaKatakana");
        KanjiBtn.Connect("pressed", this, "GoToKanji");
        ExitBtn.Connect("pressed", this, "GoToExit");
    }

    public void GoToHiraganaKatakana()
    {
        GetTree().ChangeSceneTo(HiraganaKatakanaScene);
    }

    public void GoToKanji()
    {
        GD.Print("Konbanwa desu");
    }

    public void GoToExit()
    {
        GetTree().Quit();
    }
}

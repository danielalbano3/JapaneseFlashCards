using Godot;
using System;

public class KanjiMain : Node2D
{
    private KanjiDeck Deck;
    private Path2D Path;
    private PathFollow2D Follow;
    private Button BackBtn;
    private Button ReshuffleBtn;
    private Button ExitBtn;
    private Tween Tween;
    private PackedScene MainMenu;
    private AudioStreamPlayer SFX;

    private KanjiCard tempCard;


    public override void _Ready()
    {
        base._Ready();
        SFX = GetNode<AudioStreamPlayer>("CardSfx");
        MainMenu = ResourceLoader.Load<PackedScene>("res://Menu.tscn");
        Tween = GetNode<Tween>("Tween");
        Deck = GetNode<KanjiDeck>("KanjiDeck");
        Path = GetNode<Path2D>("Path2D");
        Follow = GetNode<PathFollow2D>("Path2D/PathFollow2D");

        BackBtn = GetNode<Button>("Buttons/Back");
        ReshuffleBtn = GetNode<Button>("Buttons/Reshuffle");
        ExitBtn = GetNode<Button>("Buttons/Exit");

        BackBtn.Connect("pressed", this, "GoToMainMenu");
        ReshuffleBtn.Connect("pressed", this, "ReshuffleDeck");
        ExitBtn.Connect("pressed", this, "ExitApp");
    }
 
    private void ExitApp()
    {
        GetTree().Quit();
    }

    private void ReshuffleDeck()
    {
        GetTree().ReloadCurrentScene();
    }

    private void GoToMainMenu()
    {
        GetTree().ChangeSceneTo(MainMenu);
    }

    private async void MoveCard(KanjiCard _card)
    {
        tempCard = _card;
        Tween.InterpolateProperty(Follow, "unit_offset", 0.0, 0.55, 0.25f);
        Tween.Start();
        await ToSignal(Tween, "tween_all_completed");
        Deck.MoveChild(_card, 0);
        Tween.InterpolateProperty(Follow, "unit_offset", 0.55, 1.0, 0.25f);
        Tween.Start();
        await ToSignal(Tween, "tween_all_completed");
        _card.HideKanjiCard();

        for (int i = 0; i < Deck.GetChildCount(); i++)
        {
            Deck.GetChild<KanjiCard>(i).MoveCardX(i * 0.7f);
            Deck.GetChild<KanjiCard>(i).MoveCardY(i * -0.5f);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        if (Tween.IsActive())
        {
            tempCard.GlobalPosition = Follow.GlobalPosition;
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Tween.IsActive())
        {
            return;
        } 
        else
        {
            base._Input(@event);
            int cardCount = Deck.GetChildCount();
            var cardClicked = Deck.GetChild<KanjiCard>(cardCount - 1);
            cardClicked.KanjiClickedInput(@event);
        }
    }

}

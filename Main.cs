using Godot;
using System;
using System.Security.Cryptography;
using System.Threading;

public class Main : Node2D
{
    private PackedScene CardScene;
    private PackedScene MainMenuScene;
    private Position2D Top;
    private Node2D Deck_;
    private Tween Tween;
    private PathFollow2D Follow;
    private int[] ShuffledDeck;
    private Card currentCard;
    private TextureRect Background;
    private AudioStreamPlayer SFX;
    private Button BackBtn;
    private Button ReshuffleBtn;
    private Button ExitBtn;

    
  
    public override void _Ready()
    {
        base._Ready();
        ShuffledDeck = ShuffleDeck.GetShuffle(46);
        CardScene = ResourceLoader.Load<PackedScene>("res://Card.tscn");
        Tween = GetNode<Tween>("Tween");

        SetScene();
    }

    private void SetScene()
    {
        MainMenuScene = ResourceLoader.Load<PackedScene>("res://Menu.tscn");
        Deck_ = GetNode<Node2D>("Deck_");
        Follow = GetNode<PathFollow2D>("Path/Follow");
        Top = GetNode<Position2D>("TopPosition");
        SFX = GetNode<AudioStreamPlayer>("SFX");

        BackBtn = GetNode<Button>("Buttons/Back");
        ReshuffleBtn = GetNode<Button>("Buttons/Reshuffle");
        ExitBtn = GetNode<Button>("Buttons/Exit");

        BackBtn.Connect("pressed", this, "ReturnToMainMenu");
        ReshuffleBtn.Connect("pressed", this, "ReshuffleDeck");
        ExitBtn.Connect("pressed", this, "ExitApp");

        SetDeck();
    }

    private void ReturnToMainMenu()
    {
        GetTree().ChangeSceneTo(MainMenuScene);
    }

    private void ReshuffleDeck()
    {
        GetTree().ReloadCurrentScene();
    }

    private void ExitApp()
    {
        GetTree().Quit();
    }

    private async void RemoveCard(Card _card)
    {
        currentCard = _card;
        Tween.InterpolateProperty(Follow, "unit_offset", 0.0, 0.48, 0.25f);
        Tween.Start();
        SFX.Play();
        await ToSignal(Tween, "tween_all_completed");
        Deck_.MoveChild(_card,0);
        Tween.InterpolateProperty(Follow, "unit_offset", 0.48, 1.0, 0.25f);
        Tween.Start();
        await ToSignal(Tween, "tween_all_completed");
        _card.HideLabel();

        for (int i = 0; i < Deck_.GetChildCount(); i++)
        {
            Deck_.GetChild<Card>(i).MoveCardPos(i * 0.7f, i * -0.5f);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        if (Tween.IsActive())
        {
            currentCard.GlobalPosition = Follow.GlobalPosition;
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
 
    }

    private Card SpawnCard(int cardindex)
    {
        Card _card = (Card)CardScene.Instance();
        Deck_.AddChild(_card);
        _card.SetCard(ShuffledDeck[cardindex]);  
        _card.GlobalPosition = Top.GlobalPosition;
        return _card;
    }

    private void SetDeck()
    {
        for (int i = 0; i < ShuffledDeck.Length; i++)
        {
            Card _card = SpawnCard(i);
            _card.MoveCardPos(i * 0.7f, i * -0.5f);
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
            int cardCount = Deck_.GetChildCount();
            var cardClicked = Deck_.GetChild<Card>(cardCount - 1);
            cardClicked.CardInput(@event);
        }
    }
}

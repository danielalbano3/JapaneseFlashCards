using Godot;
using System;
using System.Security.Cryptography;
using System.Threading;

public class Main : Node2D
{
    private PackedScene CardScene;
    private PackedScene MainMenuScene;
    private Position2D Top;
    private Sprite Deck;
    private Tween Tween;
    private PathFollow2D Follow;
    private int[] ShuffledDeck;
    private Card currentCard;
    private TextureRect Background;

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
        Deck = GetNode<Sprite>("Deck");
        Follow = GetNode<PathFollow2D>("Path/Follow");
        Top = GetNode<Position2D>("TopPosition");

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
        await ToSignal(Tween, "tween_all_completed");
        Deck.MoveChild(_card,0);
        _card.ZIndex = -1;
        Tween.InterpolateProperty(Follow, "unit_offset", 0.48, 1.0, 0.25f);
        Tween.Start();
        await ToSignal(Tween, "tween_all_completed");
        _card.HideLabel();
        _card.ZIndex = 0;
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

    private void SpawnCard(int cardindex)
    {
        Card _card = (Card)CardScene.Instance();
        Deck.AddChild(_card);
        _card.SetCard(ShuffledDeck[cardindex]);  
        _card.GlobalPosition = Top.GlobalPosition;
    }

    private void SetDeck()
    {
        for (int i = 0; i < ShuffledDeck.Length; i++)
        {
            SpawnCard(i);
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
            var cardClicked = Deck.GetChild<Card>(cardCount - 1);
            cardClicked.CardInput(@event);
        }
    }
}

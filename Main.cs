using Godot;
using System;
using System.Security.Cryptography;
using System.Threading;

public class Main : Node2D
{
    public PackedScene CardScene;
    private Position2D Top;
    // private Position2D Mid;
    // private Position2D Bottom;
    private Sprite Deck;
    private Tween Tween;
    private PathFollow2D Follow;
    private int[] ShuffledDeck;
    private Card temp;
  
    public override void _Ready()
    {
        base._Ready();
        ShuffledDeck = ShuffleDeck.GetShuffle();
        CardScene = ResourceLoader.Load<PackedScene>("res://Card.tscn");
        Deck = GetNode<Sprite>("Deck");
        Tween = GetNode<Tween>("Tween");
        Follow = GetNode<PathFollow2D>("Path/Follow");
        Top = GetNode<Position2D>("TopPosition");
        // Mid = GetNode<Position2D>("MidPosition");
        // Bottom = GetNode<Position2D>("BottomPosition");

        SetDeck();
    }

    public async void RemoveCard(Card _card)
    {
        temp = _card;
        Tween.InterpolateProperty(Follow, "unit_offset", 0.0, 0.48, 0.5f);
        Tween.Start();
        await ToSignal(Tween, "tween_all_completed");
        Deck.MoveChild(_card,0);
        _card.ZIndex = -1;
        Tween.InterpolateProperty(Follow, "unit_offset", 0.48, 1.0, 0.5f);
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
            temp.GlobalPosition = Follow.GlobalPosition;
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
 
    }

    public void SpawnCard(int cardindex)
    {
        Card _card = (Card)CardScene.Instance();
        Deck.AddChild(_card);
        _card.SetCard(ShuffledDeck[cardindex]);  
        _card.GlobalPosition = Top.GlobalPosition;
    }

    public void SetDeck()
    {
        for (int i = 0; i < ShuffledDeck.Length; i++)
        {
            SpawnCard(i);
        }
    }
}

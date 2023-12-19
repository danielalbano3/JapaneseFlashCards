using Godot;
using System;

public class KanjiDeck : Node2D
{
    private int[] Deck;
    private PackedScene CardScene;
    private Texture KanjiSheet;
    private int DeckSize = 100;

    public override void _Ready()
    {
        Deck = ShuffleDeck.GetShuffle(DeckSize);
        CardScene = ResourceLoader.Load<PackedScene>("res://KanjiCard.tscn");
        KanjiSheet = ResourceLoader.Load<Texture>("res://kanji_100.png");
        SetDeck();
    }   

    private KanjiCard SpawnCard(int characterNumber)
    {
        KanjiCard _card = (KanjiCard)CardScene.Instance();
        AddChild(_card);
        _card.SetCard(Deck[characterNumber]);
        return _card;
    }

    private void SetDeck()
    {
        for (int i = 0; i < DeckSize; i++)
        {
            KanjiCard _card = SpawnCard(i);
            _card.MoveCardX(i * 0.7f);
            _card.MoveCardY(i * -0.5f);
        }
    }

    private void MoveDeckX(float xdelta)
    {
        Position = new Vector2(xdelta, Position.y);
    }
}

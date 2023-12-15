using Godot;
using System;
using System.Collections.Generic;

public class ShuffleDeck
{
  private static int[] CardIndexArray;
  
  public static int[] GetShuffle(int size)
  {
    CardIndexArray = new int[size];
    for (int i = 0; i < size; i++)
    {
      CardIndexArray[i] = i;
    } 

    Random random = new Random();
    while (size > 1)
    {
      size--;
      int k = random.Next(size + 1);
      int value = CardIndexArray[k];
      CardIndexArray[k] = CardIndexArray[size];
      CardIndexArray[size] = value;
    }

    return CardIndexArray;
  }

}
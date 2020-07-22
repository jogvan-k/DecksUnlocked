using System;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Types
{
  public sealed class Delegates
  {
    public delegate void Callback(MutableState state);

    public static Callback NoChange => state => { };
  }
}
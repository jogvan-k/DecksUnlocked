using System;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Types
{
  public static class Delegates
  {
    public delegate void Callback(MutableState state, string invokerId);

    public static Callback NoChange => (state, id) => { };
  }
}
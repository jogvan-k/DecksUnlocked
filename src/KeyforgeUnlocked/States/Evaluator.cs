using UnlockedCore;

namespace KeyforgeUnlocked.States
{
  public class Evaluator : IEvaluator
  {
    public int Evaluate(ICoreState state)
    {
      return Evaluate((IState) state);
    }

    int Evaluate(IState state)
    {
      if (state.Keys[Player.Player1] >= 3)
        return 1000;
      if (state.Keys[Player.Player2] >= 3)
        return -1000;
      var value = 0;

      value += 10 * (state.Keys[Player.Player1] - state.Keys[Player.Player2]);
      value += state.Aember[Player.Player1] - state.Aember[Player.Player2];
      return value;
    }
  }
}
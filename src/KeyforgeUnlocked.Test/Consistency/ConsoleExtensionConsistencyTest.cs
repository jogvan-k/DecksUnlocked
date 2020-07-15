using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedConsole;
using KeyforgeUnlockedConsole.ConsoleExtensions;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Consistency
{
  [TestFixture]
  class ConsoleExtensionConsistencyTest
  {
    static readonly Assembly KeyForgeUnlockedAssembly = Assembly.GetAssembly(typeof(IState));
    static readonly Assembly KeyForgeUnlockedConsoleAssembly = Assembly.GetAssembly(typeof(Entry));

    [Test]
    public void Action_AssertToConsoleImplemented()
    {
      var interf = typeof(UnlockedCore.Actions.ICoreAction);
      var type = typeof(ActionConsoleExtensions);
      AssertToConsoleFunctionsExists(interf, type);
    }

    [Test]
    public void ActionGroup_AssertToConsoleImplemented()
    {
      var interf = typeof(KeyforgeUnlocked.ActionGroups.IActionGroup);
      var type = typeof(ActionGroupConsoleExtensions);
      AssertToConsoleFunctionsExists(interf, type);
    }

    [Test]
    public void ResolvedEffect_AssertToConsoleImplemented()
    {
      var interf = typeof(KeyforgeUnlocked.ResolvedEffects.IResolvedEffect);
      var type = typeof(ResolvedEffectsExtensions);
      AssertToConsoleFunctionsExists(interf, type);
    }

    void AssertToConsoleFunctionsExists(Type @interface,
      Type type)
    {
      var v = GetDerivativesOf(@interface);
      var m = GetMethodsOf(type);

      foreach (var derived in v)
      {
        if (!m.Any(f => f.GetParameters().Single().ParameterType == derived))
          Assert.Fail($"Class {type} does not implement ToConsole for class {derived}");
      }
    }

    IEnumerable<MethodInfo> GetMethodsOf(Type type)
    {
      return from m in KeyForgeUnlockedConsoleAssembly.GetType(type.FullName).GetRuntimeMethods()
        where m.Name == "ToConsole"
              && m.ReturnType == typeof(string)
              && m.GetParameters().Length == 1
        select m;
    }


    static IEnumerable<Type> GetDerivativesOf(Type interf)
    {
      return from t in KeyForgeUnlockedAssembly.GetTypes()
        where t.GetInterfaces().Contains(interf)
              && !t.IsAbstract
        select t;
    }
  }
}
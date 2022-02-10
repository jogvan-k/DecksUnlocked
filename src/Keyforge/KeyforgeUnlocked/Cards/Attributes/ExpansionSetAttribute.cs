using System;

namespace KeyforgeUnlocked.Cards.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ExpansionSetAttribute : Attribute
    {
        public readonly Expansion Expansion;
        public readonly int Number;


        public ExpansionSetAttribute(Expansion expansion, int number)
        {
            Expansion = expansion;
            Number = number;
        }
    }
}
module UnlockedCore.AI.PvsAI

open UnlockedCore
open UnlockedCore.AI.AIBase
open UnlockedCore.Algorithms.PVS

type PvsAI(evaluator : IEvaluator, depth, ?searchConfig0: SearchConfiguration, ?loggingConfiguration0 : LoggingConfiguration) =
    inherit BaseAI(evaluator, depth, defaultArg searchConfig0 SearchConfiguration.NoRestrictions, defaultArg loggingConfiguration0 LoggingConfiguration.LogAll)
    override this.AICall d s acc pv = pvs d s acc pv

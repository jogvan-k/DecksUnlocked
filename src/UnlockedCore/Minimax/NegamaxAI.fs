namespace UnlockedCore.AITypes

open UnlockedCore
open UnlockedCore.AI.AIBase
open UnlockedCore.AI.MinimaxTypes
open UnlockedCore.Algorithms.Negamax

type NegamaxAI
    (
        evaluator: IEvaluator,
        depth,
        ?searchConfig0: SearchConfiguration,
        ?loggingConfiguration0: LoggingConfiguration
    ) =
    inherit BaseAI(evaluator,
                   depth,
                   defaultArg searchConfig0 SearchConfiguration.NoRestrictions,
                   defaultArg loggingConfiguration0 LoggingConfiguration.LogAll)

    override this.AICall d s acc pv = negamax d s acc pv

namespace UnlockedCore.AITypes

open UnlockedCore
open UnlockedCore.AI.AIBase
open UnlockedCore.Algorithms.Negamax

type NegamaxAI(evaluator : IEvaluator, depth, searchDepthConfig: SearchDepthConfiguration, ?searchConfig0: SearchConfiguration, ?loggingConfiguration0 : LoggingConfiguration) =
    inherit BaseAI(evaluator, depth, searchDepthConfig, defaultArg searchConfig0 SearchConfiguration.NoRestrictions, defaultArg loggingConfiguration0 LoggingConfiguration.LogAll)
    override this.AICall d s acc = negamax d s acc |> snd |> List.toArray

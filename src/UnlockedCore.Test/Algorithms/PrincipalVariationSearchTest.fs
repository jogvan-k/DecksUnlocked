module UnlockedCore.Test.Algorithms.PrincipalVariationSearchTest

open System

open NUnit.Framework

open UnlockedCore
open UnlockedCore.AI.MinimaxTypes
open UnlockedCore.Algorithms.Accumulator
open UnlockedCore.Algorithms.PVS
open UnlockedCore.Algorithms.TranspositionTable
open UnlockedCore.Algorithms.AISupportTypes
open UnlockedCoreTest.TestTypes

[<TestFixture>]
type PrincipalVariationSearchTest () =
    let mutable hashIndex = 0
    let evalFun counter =
        let d, h = counter
        let playerTurn = if(d % 2 = 0)
                         then Player.Player1
                         else Player.Player2
        hashIndex <- hashIndex + 1
        (playerTurn, d, h, hashIndex)
        
    let state = (complexTree evalFun 4 2).build()
    let bestPath = [1;0;1;0]
    
    [<Test>]
    member this.PrincipalPathIsBestPath () =
        let mutable nodesEvaluated = 0
        let evalFuncWithEvalCount = fun s ->
            nodesEvaluated <- nodesEvaluated + 1
            evaluatorFunc s
        let tTable = transpositionTable(LoggingConfiguration.LogAll)
        
        let acc = accumulator(evalFuncWithEvalCount, searchTime.Unlimited, LoggingConfiguration.LogAll)
        let result = recPVS -Int32.MaxValue Int32.MaxValue 1 (Plies 10) state acc tTable bestPath
        
        Assert.That(fst result, Is.EqualTo(10))
        Assert.That(snd result, Is.EqualTo(bestPath))
        Assert.That(nodesEvaluated, Is.EqualTo(7))
    
    [<Test>]
    member this.PrincipalPathIsNotBestPath () =
        let mutable nodesEvaluated = 0
        let evalFuncWithEvalCount = fun s ->
            nodesEvaluated <- nodesEvaluated + 1
            evaluatorFunc s
        let tTable = transpositionTable(LoggingConfiguration.LogAll)
        let pv = [0;0;1;0]

        let acc = accumulator(evalFuncWithEvalCount, searchTime.Unlimited, LoggingConfiguration.LogAll)
        let result = recPVS -Int32.MaxValue Int32.MaxValue 1 (Plies 10) state acc tTable pv
        
        Assert.That(fst result, Is.EqualTo(10))
        Assert.That(snd result, Is.EqualTo(bestPath))
        Assert.That(nodesEvaluated, Is.EqualTo(14))
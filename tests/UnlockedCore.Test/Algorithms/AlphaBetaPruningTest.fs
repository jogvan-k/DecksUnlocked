module UnlockedCore.Test.AlphaBetaPruningTest

open NUnit.Framework
open UnlockedCore
open UnlockedCore.AI.MinimaxTypes
open UnlockedCore.AITypes
open UnlockedCoreTest.TestTypes

open FsUnit

[<TestFixture>]
type alphaBetaPruningTest() =

    let leaf no =
        nb(p2, 1, 0, 1 * no)
            .addChildren (
                [ nb (p1, 2, 10 + no, 10 * no)
                  nb (p1, 2, 30 + no, 100 * no)
                  nb (p1, 2, 20 + no, 1000 * no) ]
            )

    let alphaBetaPruningTree =
        nb(p1, 0, 0, 0)
            .addChildren ([ leaf 3; leaf 1; leaf 2 ])

    [<Test>]
    member this.PruningOnMaximizingPlayer() =
        let sut =
            NegamaxAI(
                evaluator,
                searchLimit.Turn(4, searchTime.Unlimited),
                loggingConfiguration0 =
                    LoggingConfiguration.LogEvaluatedEndStates
                    + LoggingConfiguration.LogCalculatedSteps
            )

        let path =
            (sut :> IGameAI)
                .DetermineAction(alphaBetaPruningTree.build ())

        path |> should equal [| 0; 0 |]

        sut.LatestLogInfo.stepsCalculated
        |> should equal 8

        sut.LatestLogInfo.endNodesEvaluated
        |> should equal 5

    [<Test>]
    member this.PruningOnMinimizingPlayer() =
        let sut =
            NegamaxAI(
                evaluator,
                searchLimit.Turn(4, searchTime.Unlimited),
                loggingConfiguration0 =
                    LoggingConfiguration.LogEvaluatedEndStates
                    + LoggingConfiguration.LogCalculatedSteps
            )

        let invertedTree = invertTree alphaBetaPruningTree

        let path =
            (sut :> IGameAI)
                .DetermineAction(invertedTree.build ())

        path |> should equal [| 1; 1 |]

        sut.LatestLogInfo.stepsCalculated
        |> should equal 11

        sut.LatestLogInfo.endNodesEvaluated
        |> should equal 8

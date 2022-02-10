namespace UnlockedCore.Algorithms

open System.Diagnostics
open UnlockedCore

module Utility =
    let toStopwatchTics (st: searchTime) =
        match st with
        | Minutes s -> Some(Stopwatch.Frequency * int64 (60 * s))
        | Seconds s -> Some(Stopwatch.Frequency * int64 (s))
        | MilliSeconds s -> Some(Stopwatch.Frequency / int64 (1000) * int64 (s))
        | Unlimited -> None

    let toMilliseconds (st: searchTime) =
        match st with
        | Minutes s -> 1000 * 60 * s
        | Seconds s -> 1000 * s
        | MilliSeconds s -> s
        | Unlimited -> -1

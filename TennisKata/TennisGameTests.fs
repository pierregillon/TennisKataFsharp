module TennisGameTests

open Xunit
open TennisGameModule

[<Fact>]
let ``Default score of a new tennis game is "love - love"`` () =
    let defaultScore = Points(Love, Love)
    Assert.Equal(defaultScore, setupGame)

[<Fact>]
let ``Score is "15 - 15" where both player won a point`` () =
    let score = setupGame |> playerOneWin |> playerTwoWin
    Assert.Equal(Points(Fifty, Fifty), score)

[<Fact>]
let ``At love, winning a point increases player's score to fifty`` () =
    let score = setupGame |> playerOneWin
    Assert.Equal(Points(Fifty, Love), score)

[<Fact>]
let ``At fifty, winning a point increases player's score to thirty`` () =
    let score = setupGame |> playerOneWin |> playerOneWin
    Assert.Equal(Points(Thirty, Love), score)

[<Fact>]
let ``At thirty, winning a point increases player's score to forty`` () =
    let score = setupGame |> playerOneWin |> playerOneWin |> playerOneWin
    Assert.Equal(Points(Forty, Love), score)

[<Fact>]
let ``At forty, winning a point increases player's score to win`` () =
    let score =
        setupGame |> playerOneWin |> playerOneWin |> playerOneWin |> playerOneWin

    Assert.Equal(Game PlayerOne, score)

let deuceGame =
    setupGame
    |> playerOneWin
    |> playerOneWin
    |> playerOneWin
    |> playerTwoWin
    |> playerTwoWin
    |> playerTwoWin

[<Fact>]
let ``Two players at forty updates score to deuce`` () =
    let score =
        setupGame
        |> playerOneWin
        |> playerOneWin
        |> playerOneWin
        |> playerTwoWin
        |> playerTwoWin
        |> playerTwoWin

    Assert.Equal(Deuce, score)

[<Fact>]
let ``At deuce, winning a point updates score to player advantage`` () =
    let score = deuceGame |> playerOneWin
    Assert.Equal(Advantage PlayerOne, score)

[<Fact>]
let ``At player advantage, winning a point updates score to game of player`` () =
    let score = deuceGame |> playerOneWin |> playerOneWin
    Assert.Equal(Game PlayerOne, score)

[<Fact>]
let ``At player advantage, losing next point updates score to deuce`` () =
    let score = deuceGame |> playerOneWin |> playerTwoWin
    Assert.Equal(Deuce, score)

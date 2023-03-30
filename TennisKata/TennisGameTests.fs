module TennisGameTests

open Xunit
open TennisGameModule

[<Fact>]
let ``Default score of a new tennis game is "love - love"`` () =
    let defaultScore = Points(Love, Love)
    Assert.Equal(defaultScore, setupGame)

[<Fact>]
let ``Score is "15 - 15" where both player won a point`` () =
    let game = setupGame |> player1Win |> player2Win
    Assert.Equal(Points(Fifty, Fifty), game)

[<Fact>]
let ``At love, winning a point increases player's score to fifty`` () =
    let game = setupGame |> player1Win
    Assert.Equal(Points(Fifty, Love), game)

[<Fact>]
let ``At fifty, winning a point increases player's score to thirty`` () =
    let game = setupGame |> player1Win |> player1Win
    Assert.Equal(Points(Thirty, Love), game)

[<Fact>]
let ``At thirty, winning a point increases player's score to forty`` () =
    let game = setupGame |> player1Win |> player1Win |> player1Win
    Assert.Equal(Points(Forty, Love), game)

[<Fact>]
let ``At forty, winning a point increases player's score to win`` () =
    let game = setupGame |> player1Win |> player1Win |> player1Win |> player1Win
    Assert.Equal(Game PlayerOne, game)

[<Fact>]
let ``Two players at forty updates score to deuce`` () =
    let game =
        setupGame
        |> player1Win
        |> player1Win
        |> player1Win
        |> player2Win
        |> player2Win
        |> player2Win

    Assert.Equal(Deuce, game)

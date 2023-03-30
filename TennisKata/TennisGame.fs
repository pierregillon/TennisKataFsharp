module TennisGameModule

type Point =
    | Love
    | Fifty
    | Thirty

type Player =
    | PlayerOne
    | PlayerTwo

type FortyData =
    {
        Player: Player
        OtherPlayerPoint: Point
    }

type Score =
    | Points of Point * Point
    | Forty of FortyData
    | Deuce
    | Advantage of Player
    | Game of Player

let addOnePoint point =
    match point with
    | Love -> Fifty
    | Fifty -> Thirty
    | _ -> failwith "not implemented: cannot increase point"

let setupGame: Score = Points(Love, Love)

let playerWin currentScore playerWinningPoint : Score =
    match currentScore with
    | Game _ -> failwith "already won"
    | Deuce -> Advantage playerWinningPoint
    | Advantage player when player = playerWinningPoint -> Game playerWinningPoint
    | Advantage _ -> Deuce
    | Forty forty when forty.Player = playerWinningPoint -> Game playerWinningPoint
    | Forty forty when forty.Player <> playerWinningPoint && forty.OtherPlayerPoint = Thirty -> Deuce
    | Forty forty when forty.Player <> playerWinningPoint && forty.OtherPlayerPoint <> Thirty ->
        Forty(
            { forty with
                OtherPlayerPoint = addOnePoint forty.OtherPlayerPoint
            }
        )
    | Points (Thirty, player2Score) when playerWinningPoint = PlayerOne ->
        Forty(
            {
                Player = playerWinningPoint
                OtherPlayerPoint = player2Score
            }
        )
    | Points (player1Score, Thirty) when playerWinningPoint = PlayerTwo ->
        Forty(
            {
                Player = playerWinningPoint
                OtherPlayerPoint = player1Score
            }
        )
    | Points (player1Score, player2Score) when playerWinningPoint = PlayerOne ->
        Points(addOnePoint player1Score, player2Score)
    | Points (player1Score, player2Score) when playerWinningPoint <> PlayerOne ->
        Points(player1Score, addOnePoint player2Score)
    | _ -> failwith "invalid case"

let playerOneWin score : Score = playerWin score PlayerOne

let playerTwoWin score : Score = playerWin score PlayerTwo

module TennisGameModule

type Point =
    | Love
    | Fifty
    | Thirty
    | Forty

let addOnePoint point =
    match point with
    | Love -> Fifty
    | Fifty -> Thirty
    | Thirty -> Forty
    | _ -> failwith "not implemented: cannot increase point"

type Player =
    | PlayerOne
    | PlayerTwo

type Score =
    | Points of Point * Point
    | Deuce
    | Advantage of Player
    | Game of Player

let setupGame: Score = Points(Love, Love)

let playerWin currentScore player : Score =
    match currentScore with
    | Game _ -> failwith "already won"
    | Deuce -> Advantage player
    | Advantage otherPlayer when otherPlayer <> player -> Deuce
    | Advantage player -> Game player
    | Points (Forty, _) when player = PlayerOne -> Game PlayerOne
    | Points (_, Forty) when player = PlayerTwo -> Game PlayerTwo
    | Points (player1Score, player2Score) ->
        let newScore =
            if player = PlayerOne then
                Points(addOnePoint player1Score, player2Score)
            else
                Points(player1Score, addOnePoint player2Score)

        if newScore = Points(Forty, Forty) then Deuce else newScore
    | _ -> failwith "invalid case"

let playerOneWin (score: Score) : Score = playerWin score PlayerOne

let playerTwoWin (score: Score) : Score = playerWin score PlayerTwo

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
    | Game of Player

let setupGame: Score = Points(Love, Love)

let playerWin score player : Score =
    match score with
    | Game _ -> failwith "already won"
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

let player1Win (game: Score) : Score = playerWin game PlayerOne

let player2Win (game: Score) : Score = playerWin game PlayerTwo

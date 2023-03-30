module TennisGameModule

type Point =
    | Love
    | Fifty
    | Thirty
    | Forty

let increaseScore point =
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
    | Game of Player

let setupGame: Score = Points(Love, Love)

let playerWin (score: Score) updateScore : Score =
    match score with
    | Game _ -> failwith "already won"
    | Points (Forty, _) -> Game PlayerOne
    | Points (_, Forty) -> Game PlayerTwo
    | Points (player1Score, player2Score) -> updateScore (player1Score, player2Score)
    | _ -> failwith "invalid case"

let player1Win (game: Score) : Score =
    playerWin game (fun (x, y) -> Points(increaseScore x, y))

let player2Win (game: Score) : Score =
    playerWin game (fun (x, y) -> Points(x, (increaseScore y)))

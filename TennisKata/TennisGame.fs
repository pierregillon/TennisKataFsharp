module TennisGameModule

type Score =
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

type TennisGame =
    | GameScore of Score * Score
    | Player1Won
    | Player2Won

let setupGame: TennisGame = GameScore(Love, Love)

let playerWin (game: TennisGame) updateScore : TennisGame =
    match game with
    | Player1Won -> failwith "already won"
    | Player2Won -> failwith "already won"
    | GameScore (Forty, _) -> Player1Won
    | GameScore (_, Forty) -> Player2Won
    | GameScore (player1Score, player2Score) -> updateScore (player1Score, player2Score)
    | _ -> failwith "invalid case"

let player1Win (game: TennisGame) : TennisGame =
    playerWin game (fun (x, y) -> GameScore(increaseScore x, y))

let player2Win (game: TennisGame) : TennisGame =
    playerWin game (fun (x, y) -> GameScore(x, (increaseScore y)))

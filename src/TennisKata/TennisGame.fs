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
    
type PointsData = Point * Point

type Score =
    | Points of PointsData
    | Forty of FortyData
    | Deuce
    | Advantage of Player
    | Game of Player

let setupGame: Score = Points(Love, Love)

let increasePoint point =
    match point with
    | Love -> Fifty
    | Fifty -> Thirty
    | _ -> failwith "not implemented: cannot increase point"

let addOnePoint playerWinningPoint (player1Score, player2Score) =
    if playerWinningPoint = PlayerOne && player1Score = Thirty then
        Forty { Player = playerWinningPoint; OtherPlayerPoint = player2Score }
    elif playerWinningPoint = PlayerTwo && player2Score = Thirty then
        Forty { Player = playerWinningPoint; OtherPlayerPoint = player1Score }
    elif playerWinningPoint = PlayerOne then
        Points (increasePoint player1Score, player2Score)
    else
        Points (player1Score, increasePoint player2Score)
 
let addOnePointAtForty playerWinningPoint forty =
    if forty.Player = playerWinningPoint then Game playerWinningPoint
    elif forty.OtherPlayerPoint = Thirty then Deuce
    else Forty { forty with OtherPlayerPoint = increasePoint forty.OtherPlayerPoint }

let playerWin currentScore playerWinningPoint : Score =
    match currentScore with
    | Points points -> addOnePoint playerWinningPoint points
    | Forty forty -> addOnePointAtForty playerWinningPoint forty
    | Deuce -> Advantage playerWinningPoint
    | Advantage player when player = playerWinningPoint -> Game playerWinningPoint
    | Advantage _ -> Deuce
    | Game _ -> failwith "already won"
    | _ -> failwith "invalid case"

let playerOneWin score : Score = playerWin score PlayerOne

let playerTwoWin score : Score = playerWin score PlayerTwo

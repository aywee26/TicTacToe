# TicTacToe
ASP.NET Core Web API project that allows to play a game of TicTacToe. Built as a solution of Sense Capital entry task.

# Getting started
## Running the project locally
1. Set up database in SQL Server
     - Create user ```TicTacToe```;
     - Create database ```TicTacToe``` and assign created user as an owner.

Here's how it's done with ```sqlcmd```:
```
create login TicTacToe with password = "TicTacToe", check_policy = off
go

create database TicTacToe
go

exec sp_changedbowner 'TicTacToe'
go
```

2. Clone the project. Edit ```...\TicTacToe\TicTacToe.WebAPI\appsettings.json``` if necessary.
3. Build and run the project. Program will apply migrations automatically.

# API Definition
## Get a Player with specific ID
Request:
```http
GET /api/Player/{id}
```

Response:
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "string"
}
```

## Get all Players
Request:
```http
GET /api/Player/
```

Response:
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "string"
  }
]
```

## Create a Player
Request:
```http
POST /api/Player/
```

Acceptable parameters:
- ```?name={string}``` - name of a new Player

Response:
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "string"
}
```

## Get a Game with specific ID
Request:
```http
GET /api/Game/{id}
```

Response:
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "createdAt": "2023-03-17T15:18:09.474Z",
  "gamePlayers": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "string"
    }
  ],
  "status": "string",
  "state": "string"
}
```

## Get all Games
Request:
```http
GET /api/Game/
```

Response:
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "createdAt": "2023-03-17T15:18:09.474Z",
    "gamePlayers": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string"
      }
    ],
    "status": "string",
    "state": "string"
  }
]
```

## Create new Game
Request:
```http
POST /api/Game/
```

Request Body:
```json
{
  "player1Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "player2Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

Response:
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "createdAt": "2023-03-17T15:18:09.474Z",
  "gamePlayers": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "string"
    }
  ],
  "status": "string",
  "state": "string"
}
```

## Modify a Game
Request:
```http
PUT /api/Game/{id}
```

Acceptable parameters:
- ```?row={int32}``` - row = {0, 1, 2}
- ```?column={int32}``` - column = {0, 1, 2}

Response:
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "createdAt": "2023-03-17T15:18:09.474Z",
  "gamePlayers": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "string"
    }
  ],
  "status": "string",
  "state": "string"
}
```

## Clarification of ```state```
State of a game is returned as a string with length of 9. It represents a result of concatenation of all 3 rows of the playing grid.

For example, this grid:
```
. | O | .
. | X | .
X | . | .
```

is returned as ```.O..X.X..```

## Clarification of ```status```
Status of a game is returned as a string, which can have following values:
- ```Player1 Turn``` - a ```PUT``` request will put ```X``` on the grid.
- ```Player2 Turn``` - a ```PUT``` request will put ```O``` on the grid.
- ```Player1 won``` - game is over with Victory of Player1 and cannot be modified further.
- ```Player2 won``` - game is over with Victory of Player2 and cannot be modified further.
- ```Draw``` - game is over with Draw and cannot be modified further.

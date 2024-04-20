using System;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace Tennis
{
    public class TennisGame2 : ITennisGame
    {
        private int _p1point;
        private int _p2point;

        private string _p1res = "";
        private string _p2res = "";
        private string _player1Name;
        private string _player2Name;

        public TennisGame2(string player1Name, string player2Name)
        {
            _player1Name = player1Name;
            _player2Name = player2Name;
        }

        public string GetScore()
        {
            var score = String.Empty;

            if (_p1point == _p2point && _p1point < 3)
            {
                if (_p1point < 3)
                {
                    score = GetScoreString(_p1point);
                    score += "-All";
                }

                if (_p1point > 2)
                {
                    score = "Deuce";
                }
            }

            CheckingPositiveScore(_p1point, _p2point, ref _p1res, ref _p2res, ref score);

            CheckingPositiveScore(_p2point, _p1point, ref _p2res, ref _p1res, ref score);

            CheckingAverageScore(_p1point, _p2point, ref _p1res, ref _p2res, ref score);

            CheckingAverageScore(_p2point, _p1point, ref _p2res, ref _p1res, ref score);

            SetAdvantagePlayer(_p1point, _p2point, 1, ref score);

            SetAdvantagePlayer(_p2point, _p1point, 2, ref score);

            SetWinner(_p1point, _p2point, 1, ref score);

            SetWinner(_p2point, _p1point, 2, ref score);

            return score;
        }

        private void CheckingPositiveScore(int firstPoints, int secondPoints, ref string firstPRes, ref string secondPRes,
            ref string score)
        {
            if (firstPoints > 0 && secondPoints == 0)
            {
                firstPRes = GetScoreString(firstPoints);
                secondPRes = "Love";
                score = firstPRes + "-" + secondPRes;
            }
        }

        private void CheckingAverageScore(int firstPoints, int secondPoints, ref string firstPRes, ref string secondPRes,
            ref string score)
        {
            if (firstPoints > secondPoints && firstPoints < 4)
            {
                firstPRes = GetScoreString(firstPoints);
                secondPRes = GetScoreString(secondPoints);
                score = firstPRes + "-" + secondPRes;
            }
        }

        private void SetAdvantagePlayer(int firstPoints, int secondPoints, int playerNumber, ref string score)
        {
            if (firstPoints > secondPoints && secondPoints >= 3)
            {
                score = "Advantage player" + playerNumber;
            }
        }

        private void SetWinner(int firstPoints, int secondPoints, int playerNumber, ref string score)
        {
            if (firstPoints >= 4 && secondPoints >= 0 && (firstPoints - secondPoints) >= 2)
            {
                score = "Win for player" + playerNumber;
            }
        }

        private string GetScoreString(int score)
        {
            return score switch
            {
                0 => "Love",
                1 => "Fifteen",
                2 => "Thirty",
                3 => "Forty",
                _ => throw new InvalidOperationException("Invalid score")
            };
        }

        public void SetP1Score(int number)
        {
            for (int i = 0; i < number; i++)
            {
                P1Score();
            }
        }

        public void SetP2Score(int number)
        {
            for (var i = 0; i < number; i++)
            {
                P2Score();
            }
        }

        private void P1Score()
        {
            _p1point++;
        }

        private void P2Score()
        {
            _p2point++;
        }

        public void WonPoint(string player)
        {
            if (player == "player1")
                P1Score();
            else
                P2Score();
        }

    }
}


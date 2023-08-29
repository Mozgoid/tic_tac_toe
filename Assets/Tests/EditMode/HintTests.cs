using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class HintTests
    {
        [Test]
        public void HintWin()
        {
            var board = new Board();
            board.Set(Board.Symbol.X, 1 ,1);
            board.Set(Board.Symbol.O, 1 ,0);
            board.Set(Board.Symbol.X, 0 ,0);
            board.Set(Board.Symbol.O, 0 ,2);
            Assert.AreEqual(Hint.SmartHint(board, Board.Symbol.X), new Vector2Int(2, 2));
        }

        [Test]
        public void HintPreventLose()
        {
            var board = new Board();
            board.Set(Board.Symbol.X, 1 ,1);
            board.Set(Board.Symbol.O, 1 ,0);
            board.Set(Board.Symbol.X, 0 ,0);
            Assert.AreEqual(Hint.SmartHint(board, Board.Symbol.O), new Vector2Int(2, 2));
        }

        [Test]
        public void HintThrowsWhenNoMove()
        {
            var board = new Board();
            board.SetBoard(new Board.Symbol[,] {
                { Board.Symbol.X, Board.Symbol.O, Board.Symbol.X },
                { Board.Symbol.X, Board.Symbol.O, Board.Symbol.O },
                { Board.Symbol.O, Board.Symbol.X, Board.Symbol.X },
            });
            Assert.Throws<System.ArgumentException>(() => Hint.SmartHint(board, Board.Symbol.X));
        }
    }
}

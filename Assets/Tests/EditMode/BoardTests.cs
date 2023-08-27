using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BoardTests
    {
        [Test]
        public void NewBoardIsEmpty()
        {
            var board = new Board();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; ++j)
                    Assert.AreEqual(Board.Symbol.None, board.Get(i, j));
            }
        }

        [Test]
        public void SetAndGet()
        {
            var board = new Board();
            board.Set(Board.Symbol.X, 0, 0);
            Assert.AreEqual(Board.Symbol.X, board.Get(0, 0));
        }

        [Test]
        public void SetWrongPosition()
        {
            var board = new Board();
            Assert.Throws<System.ArgumentException>(() => board.Set(Board.Symbol.X, -1, 0));
            Assert.Throws<System.ArgumentException>(() => board.Set(Board.Symbol.X, 0, -1));
            Assert.Throws<System.ArgumentException>(() => board.Set(Board.Symbol.X, 3, 0));
            Assert.Throws<System.ArgumentException>(() => board.Set(Board.Symbol.X, 0, 3));
        }

        [Test]
        public void SetTwice()
        {
            var board = new Board();
            board.Set(Board.Symbol.X, 0, 0);
            Assert.Throws<System.ArgumentException>(() => board.Set(Board.Symbol.X, 0, 0));
        }

        [Test]
        public void GetWrongPosition()
        {
            var board = new Board();
            Assert.Throws<System.ArgumentException>(() => board.Get(-1, 0));
            Assert.Throws<System.ArgumentException>(() => board.Get(0, -1));
            Assert.Throws<System.ArgumentException>(() => board.Get(3, 0));
            Assert.Throws<System.ArgumentException>(() => board.Get(0, 3));
        }

        [Test]
        public void SetAndGet2()
        {
            var board = new Board();
            board.Set(Board.Symbol.X, 1, 1);
            Assert.AreEqual(Board.Symbol.X, board.Get(1, 1));
        }

        [Test]
        public void SetAndGet3()
        {
            var board = new Board();
            board.Set(Board.Symbol.O, 2, 2);
            Assert.AreEqual(Board.Symbol.O, board.Get(2, 2));
        }

        [Test]
        public void Reset()
        {
            var board = new Board();
            board.Set(Board.Symbol.X, 0, 0);
            board.Set(Board.Symbol.O, 1, 1);
            board.Set(Board.Symbol.X, 2, 2);
            board.Reset();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; ++j)
                    Assert.AreEqual(Board.Symbol.None, board.Get(i, j));
            }
        }

        [Test]
        public void WinVertical()
        {
            var board = new Board();
            Assert.AreEqual(null, board.WhoWins());
            board.Set(Board.Symbol.X, 0, 0);
            board.Set(Board.Symbol.X, 0, 1);
            Assert.AreEqual(null, board.WhoWins());
            board.Set(Board.Symbol.X, 0, 2);
            Assert.AreEqual(Board.Symbol.X, board.WhoWins());
        }

        [Test]
        public void WinDiagonal()
        {
            var board = new Board();
            Assert.AreEqual(null, board.WhoWins());
            board.Set(Board.Symbol.O, 0, 0);
            board.Set(Board.Symbol.O, 1, 1);
            Assert.AreEqual(null, board.WhoWins());
            board.Set(Board.Symbol.O, 2, 2);
            Assert.AreEqual(Board.Symbol.O, board.WhoWins());
        }

        [Test]
        public void WinOtherDiagonal()
        {
            var board = new Board();
            Assert.AreEqual(null, board.WhoWins());
            board.Set(Board.Symbol.O, 0, 2);
            board.Set(Board.Symbol.O, 1, 1);
            Assert.AreEqual(null, board.WhoWins());
            board.Set(Board.Symbol.O, 2, 0);
            Assert.AreEqual(Board.Symbol.O, board.WhoWins());
        }

        [Test]
        public void WinNonYet()
        {
            var board = new Board();
            Assert.AreEqual(null, board.WhoWins());
            board.Set(Board.Symbol.X, 0, 0);
            board.Set(Board.Symbol.O, 1, 1);
            Assert.AreEqual(null, board.WhoWins());
            board.Set(Board.Symbol.X, 2, 2);
            Assert.AreEqual(null, board.WhoWins());
        }

        [Test]
        public void Draw()
        {
            var board = new Board();
            board.Set(Board.Symbol.X, 0, 0);
            board.Set(Board.Symbol.O, 0, 1);
            board.Set(Board.Symbol.X, 0, 2);
            board.Set(Board.Symbol.X, 1, 0);
            board.Set(Board.Symbol.O, 1, 1);
            board.Set(Board.Symbol.X, 1, 2);
            board.Set(Board.Symbol.O, 2, 0);
            board.Set(Board.Symbol.X, 2, 1);
            board.Set(Board.Symbol.O, 2, 2);
            Assert.IsTrue(board.IsFull());
            Assert.AreEqual(Board.Symbol.None, board.WhoWins());
        }


        [Test]
        public void WinHorizontal()
        {
            var board = new Board();
            Assert.AreEqual(null, board.WhoWins());
            board.Set(Board.Symbol.X, 0, 1);
            board.Set(Board.Symbol.X, 1, 1);
            Assert.AreEqual(null, board.WhoWins());
            board.Set(Board.Symbol.X, 2, 1);
            Assert.AreEqual(Board.Symbol.X, board.WhoWins());
        }

        [Test]
        public void IsFull()
        {
            var board = new Board();
            Assert.IsFalse(board.IsFull());
            board.Set(Board.Symbol.X, 0, 0);
            Assert.IsFalse(board.IsFull());
            board.Set(Board.Symbol.X, 0, 1);
            Assert.IsFalse(board.IsFull());
            board.Set(Board.Symbol.X, 0, 2);
            Assert.IsFalse(board.IsFull());
            board.Set(Board.Symbol.X, 1, 0);
            Assert.IsFalse(board.IsFull());
            board.Set(Board.Symbol.X, 1, 1);
            Assert.IsFalse(board.IsFull());
            board.Set(Board.Symbol.X, 1, 2);
            Assert.IsFalse(board.IsFull());
            board.Set(Board.Symbol.X, 2, 0);
            Assert.IsFalse(board.IsFull());
            board.Set(Board.Symbol.X, 2, 1);
            Assert.IsFalse(board.IsFull());
            board.Set(Board.Symbol.X, 2, 2);
            Assert.IsTrue(board.IsFull());
        }

        [Test]
        public void SetBoard()
        {
            var board = new Board();
            var newBoard = new Board.Symbol[3, 3]
            {
                { Board.Symbol.X, Board.Symbol.X, Board.Symbol.X },
                { Board.Symbol.O, Board.Symbol.O, Board.Symbol.O },
                { Board.Symbol.X, Board.Symbol.X, Board.Symbol.X },
            };
            board.SetBoard(newBoard);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; ++j)
                    Assert.AreEqual(newBoard[i, j], board.Get(i, j));
            }
        }
    }
}

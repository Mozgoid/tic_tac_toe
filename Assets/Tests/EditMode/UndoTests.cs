using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class UndoTests
    {
        [Test]
        public void CantUndoOneMove()
        {
            var board = new Board();
            var history = new History(board);
            board.Set(Board.Symbol.X, 1 ,1);
            Assert.IsFalse(history.CanUndo);
            history.Undo();
            Assert.AreEqual(board.Get(1,1), Board.Symbol.X);
        }

        [Test]
        public void UndoOnce()
        {
            var board = new Board();
            var history = new History(board);
            board.Set(Board.Symbol.X, 2 ,2);
            board.Set(Board.Symbol.O, 0 ,1);
            board.Set(Board.Symbol.X, 1 ,1);
            Assert.IsTrue(history.CanUndo);
            history.Undo();
            Assert.AreEqual(board.Get(1,1), Board.Symbol.None);
            Assert.AreEqual(board.Get(0,1), Board.Symbol.None);
            Assert.AreEqual(board.Get(2,2), Board.Symbol.X);
        }

        
        [Test]
        public void UndoTwice()
        {
            var board = new Board();
            var history = new History(board);
            board.Set(Board.Symbol.X, 1 ,1);
            board.Set(Board.Symbol.O, 0 ,1);
            board.Set(Board.Symbol.X, 2 ,2);
            board.Set(Board.Symbol.O, 0 ,0);
            board.Set(Board.Symbol.X, 1 ,2);
            Assert.IsTrue(history.CanUndo);
            history.Undo();
            Assert.AreEqual(board.Get(1,2), Board.Symbol.None);
            Assert.AreEqual(board.Get(0,0), Board.Symbol.None);
            Assert.AreEqual(board.Get(2,2), Board.Symbol.X);
            Assert.AreEqual(board.Get(0,1), Board.Symbol.O);
            history.Undo();
            Assert.AreEqual(board.Get(2,2), Board.Symbol.None);
            Assert.AreEqual(board.Get(0,1), Board.Symbol.None);
        }

    }
}

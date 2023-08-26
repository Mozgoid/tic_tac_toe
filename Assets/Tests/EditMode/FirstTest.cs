using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class FirstTest
    {
        [Test]
        public void FirstTestSimplePasses()
        {
            Assert.AreEqual("Hello World", NewBehaviourScript.Test());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestBlockDeactivation
    {
        BlockBehaviour _block;
        [SetUp]
        public void Setup()
        {
            GameObject blockPrefab = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Blocks/Block"));
            Assert.IsNotNull(blockPrefab);

            _block = blockPrefab.gameObject.GetComponent<BlockBehaviour>();
            Assert.IsNotNull(_block);

            // Make sure block is not deactivated
            Assert.IsFalse(_block.IsDeactivated);
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_block);
        }

        [UnityTest]
        public IEnumerator FireFlowerBlockDeactivated()
        {
            _block.spawnType = BlockBehaviour.SpawnObjectType.FireFlower;

            CreateBlockCollision();

            // Check block finished the bouncing process.
            Assert.IsTrue(_block.IsBounceEnd);

            // Check block sprite changed to deactivate sprite.
            Assert.Equals(_block.gameObject.GetComponent<SpriteRenderer>().sprite, _block.deactivateSprite);

            // Check block state.
            Assert.IsTrue(_block.IsDeactivated);

            yield return null;
        }

        void CreateBlockCollision()
        {

        }
    }
}

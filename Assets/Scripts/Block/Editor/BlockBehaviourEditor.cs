using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlockBehaviour))]
[CanEditMultipleObjects]
public class BlockBehaviourEditor : Editor
{
    BlockBehaviour block;
    void OnEnable()
    {
        block = (BlockBehaviour)target;
    }
    public override void OnInspectorGUI()
    {
        // PowerUp
        block.spawnType = (BlockBehaviour.SpawnObjectType)EditorGUILayout.EnumPopup("Spawn Type", block.spawnType);

        switch (block.spawnType)
        {
            case BlockBehaviour.SpawnObjectType.SuperMushroom:
                block.SuperMushroom = (GameObject)EditorGUILayout.ObjectField("SuperMushroom Prefab", block.SuperMushroom, typeof(GameObject), false);
                break;
            case BlockBehaviour.SpawnObjectType.FireFlower:
                block.FireFlower = (GameObject)EditorGUILayout.ObjectField("FireFlower Prefab", block.FireFlower, typeof(GameObject), false);
                break;
            case BlockBehaviour.SpawnObjectType.Starman:
                block.Starman = (GameObject)EditorGUILayout.ObjectField("Starman Prefab", block.Starman, typeof(GameObject), false);
                break;
            case BlockBehaviour.SpawnObjectType.OneUpMushroom:
                block.OneUpMushroom = (GameObject)EditorGUILayout.ObjectField("OneUpMushroom Prefab", block.OneUpMushroom, typeof(GameObject), false);
                break;
            case BlockBehaviour.SpawnObjectType.Coin:
                block.Coin = (GameObject)EditorGUILayout.ObjectField("Coin Prefab", block.Coin, typeof(GameObject), false);
                block.numberOfCoins = EditorGUILayout.IntField("Number of Coins", block.numberOfCoins);
                block.coinAppearTime = EditorGUILayout.FloatField("Appear time", block.coinAppearTime);
                break;
            case BlockBehaviour.SpawnObjectType.None:
                EditorGUILayout.ObjectField("Brick Prefab", block.brick, typeof(GameObject), false);
                block.brickShatterSound = (AudioEvent)EditorGUILayout.ObjectField("Shatter Sound", block.brickShatterSound, typeof(AudioEvent), false);
                break;
        }
        block.deactivateSprite = (Sprite)EditorGUILayout.ObjectField("Deactivate Sprite", block.deactivateSprite, typeof(Sprite), false);

        if(GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}

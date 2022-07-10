using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateAnimations
{
    //[MenuItem("GameObject/Create Animations")]
    public static void GenerateAnim()
    {
        foreach(var gameObject in Selection.gameObjects)
        {
            Sprite baseSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            if(baseSprite != null)
            {
                //Initialize animations
                AnimationClip idle = new()
                {
                    name = "idle"
                };
                AnimationClip move = new()
                {
                    name = "move"
                };
                AnimationClip attack = new()
                {
                    name = "attack"
                };
                AnimationClip die = new()
                {
                    name = "die"
                };

                //Set up animation frames
                

                //create animation override
                AnimatorOverrideController animatorOverrideController = new((RuntimeAnimatorController)AssetDatabase.LoadAssetAtPath("Assets/Graphics/Animations/UnitAminator.controller", typeof(RuntimeAnimatorController)));

                List<KeyValuePair<AnimationClip, AnimationClip>> overrides = new(4)
                {
                    new(animatorOverrideController.runtimeAnimatorController.animationClips[0], idle),
                    new(animatorOverrideController.runtimeAnimatorController.animationClips[1], move),
                    new(animatorOverrideController.runtimeAnimatorController.animationClips[2], attack),
                    new(animatorOverrideController.runtimeAnimatorController.animationClips[3], die)
                };
                animatorOverrideController.ApplyOverrides(overrides);
                
                gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorOverrideController;
                foreach(var pair in overrides)
                {
                    Debug.Log(pair);
                }
                
                //save assets
                AssetDatabase.CreateAsset(idle, "Graphics/Animations/" + gameObject.name + "/idle.anim");
                AssetDatabase.CreateAsset(move, "Graphics/Animations/" + gameObject.name + "/move.anim");
                AssetDatabase.CreateAsset(attack, "Graphics/Animations/" + gameObject.name + "/attack.anim");
                AssetDatabase.CreateAsset(die, "Graphics/Animations/" + gameObject.name + "/die.anim");
                AssetDatabase.CreateAsset(animatorOverrideController, "Graphics/Animations/" + gameObject.name + "/controller.overrideController");
                AssetDatabase.SaveAssets();
                
            }

        }
    }
}

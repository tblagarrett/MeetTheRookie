using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Darklight.Game.SpriteAnimation
{

    /// <summary>
    /// Plays a frame animation on a sprite renderer
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class FrameAnimationPlayer : MonoBehaviour
    {
        public int frameRate = 4; // Global frame rate for all animations
        private float _timePerFrame => 1 / (float)frameRate; // Time each frame should be displayed
        private float _timer = 0f; // Timer to track when to switch to the next frame

        private SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();
        public SpriteSheet spriteSheet = null;
        public int currentFrame = 0;

        private void Start()
        {
            InitializeSprite();
        }

        private void Update()
        {
            UpdateAnimation();
        }

        private void InitializeSprite()
        {
            if (spriteSheet != null && spriteSheet.Length > 0)
            {
                _spriteRenderer.sprite = spriteSheet.GetSpriteAtFrame(currentFrame);
            }
        }

        public void UpdateAnimation()
        {
            if (spriteSheet == null) return;
            if (currentFrame + 1 == spriteSheet.Length && !spriteSheet.loop) return;

            _timer += Time.deltaTime; // Update Timer

            // Check if it's time to update to the next frame
            if (_timer >= _timePerFrame)
            {
                currentFrame = (currentFrame + 1) % spriteSheet.Length;
                _spriteRenderer.sprite = spriteSheet.GetSpriteAtFrame(currentFrame);

                // Reset the timer, accounting for any "overflow" time past the expected frame duration
                _timer -= _timePerFrame;
            }
        }

        // Timer to track when to switch to the next frame
        public void LoadSpriteSheet(SpriteSheet spriteSheet)
        {
            this.spriteSheet = spriteSheet;
            this._spriteRenderer.sprite = spriteSheet.GetSpriteAtFrame(0);
            currentFrame = 0;
            _timer = 0f;
        }

        public void Clear()
        {
            spriteSheet = null;
            _spriteRenderer.sprite = null;
        }

        public void FlipTransform(Vector2 moveInput)
        {
            _spriteRenderer.flipX = moveInput.x > 0; // Flip the sprite based on the input direction

            /*
            //how many ways can you flip a sprite?
            if (moveInput.x < 0) { _spriteRenderer.flipX = true; }
            else if (moveInput.x > 0) { _spriteRenderer.flipX = false; }
            */

            //_spriteRenderer.transform.localRotation = Quaternion.Euler(new Vector3(0, 180 * _flipMultiplier, 0));
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(FrameAnimationPlayer))]
    public class FrameAnimationPlayerEditor : Editor
    {
        private SerializedObject _serializedObject;
        private FrameAnimationPlayer _script;
        private void OnEnable()
        {
            _serializedObject = new SerializedObject(target);
            _script = (FrameAnimationPlayer)target;

            EditorApplication.update += OnUpdate;
        }

        private void OnDisable()
        {
            EditorApplication.update -= OnUpdate;
        }

        private void OnUpdate()
        {
            // Only use this in the editor
            if (Application.isPlaying) return;

            // do things
            _script.UpdateAnimation();

            // force loop update so that inputs aren't required
            EditorApplication.QueuePlayerLoopUpdate();
        }


        public override void OnInspectorGUI()
        {
            _serializedObject.Update();

            // Ensure there's a Spritesheet and it has frames
            if (_script == null) return;

            SerializedProperty frameRateProp = _serializedObject.FindProperty("frameRate");
            EditorGUILayout.PropertyField(frameRateProp);
            _script.frameRate = frameRateProp.intValue;

            Sprite currentSprite = _script.spriteSheet.GetSpriteAtFrame(_script.currentFrame);
            if (currentSprite == null) return;

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();

            Texture2D texture = AssetPreview.GetAssetPreview(currentSprite);
            GUILayout.Label(texture);

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField($"Global Frame Rate: {_script.frameRate.ToString()}");
            EditorGUILayout.LabelField($"Current Frame: {_script.currentFrame}");
            EditorGUILayout.LabelField($"Sprite: {currentSprite.name}");
            EditorGUILayout.LabelField($"Loop: {_script.spriteSheet.loop}");
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            _serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}

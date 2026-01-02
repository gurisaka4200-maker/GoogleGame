# GoogleGame - Mobile Sword Prototype

Unity version: 2021.3 LTS recommended (works on 2022.3 LTS too)

What this contains:
- Core C# scripts for a minimal mobile 3D sword-fighting extraction prototype.
  - VirtualJoystick.cs, PlayerController.cs, CameraFollow.cs, SwordHitbox.cs, EnemySimple.cs, GameManager.cs
- Instructions below to set up a minimal scene using primitives (no external assets).

Quick setup (in Unity):
1. Create a new Unity project (3D) using Unity 2021.3 LTS.
2. Create folders: Assets/Scripts, Assets/Scenes, Assets/Prefabs.
3. Copy the provided C# files into Assets/Scripts.

Scene setup (MainScene):
- Player:
  - Create a Capsule as the player object.
  - Add a CharacterController component.
  - Add an Animator (optional for demo).
  - Add PlayerController and assign CharacterController, VirtualJoystick (set later), Camera transform, Animator, and a child GameObject for sword hit origin.
  - Create child GameObject "SwordOrigin" in front of the capsule; add a BoxCollider (isTrigger) and attach SwordHitbox (disable collider at start).
- Camera:
  - Main Camera -> add CameraFollow, set target = player, tune offset.
- UI:
  - Create a Canvas (Screen Space - Overlay).
  - Add a joystick: an Image for background (left-bottom) and an Image child for handle. Attach VirtualJoystick to an object and wire references.
  - Add a Text (statusText) for extraction messages.
- Enemy:
  - Create a basic enemy (cube), add NavMeshAgent and EnemySimple script; set target = player.
  - Bake NavMesh (Window > AI > Navigation) after you have ground/cave geometry.
- Extraction:
  - Create an empty GameObject as extractionZone and a visible sphere; add GameManager and assign player, extractionZone, statusText.

Android build settings:
- PlayerSettings > Other Settings:
  - Scripting Backend: IL2CPP (recommended)
  - Target Architectures: armeabi-v7a and arm64-v8a (arm64 required for Play Store)
  - Minimum API Level: API 24 (Android 7.0+) or as desired
- Set Package Name: e.g., com.yourcompany.googleGame
- Connect device and test frequently.

Repo notes:
- Branch: feature/mobile-swordgame contains the prototype scripts and README.
- These are lightweight placeholder scripts and instructions. Import them into a Unity project and follow the scene setup steps.

If you want I can also:
- Provide a ready Unity scene and minimal prefabs (I can prepare them and push after the repo has an initial commit).
- Produce a downloadable unitypackage if you prefer to import into your project.

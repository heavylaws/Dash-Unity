# Dash-Unity

## Description
Dash-Unity is a 3-lane endless runner / rhythm dash game. The player runs forward automatically, switches lanes, jumps, and dashes through obstacles while the world reacts to music.

## Current Status
Prototype / early MVP. Core game state flow (Phase 2A), UI Feedback (Phase 2B), Enhanced Obstacles/Lanes (Phase 2C), and Player-Feel Polish (Phase 3) completed.

## Project Structure
Game-owned assets are organized under `Assets/_Dash`:
- `Assets/_Dash/Scripts/`: Game logic (Core, Player, World, Audio, Camera).
- `Assets/_Dash/Prefabs/`: Game objects (Obstacles, Track, Player, Managers, UI).
- `Assets/_Dash/Materials/`: Custom materials.
- `Assets/_Dash/Audio/`: Music and sound effects.
- `Assets/_Dash/Shaders/`: Custom shaders.
- `Assets/_Dash/Documentation/`: Internal project documentation.

The following assets intentionally remain in their original locations for engine or third-party compatibility:
- `Assets/Scenes/SampleScene.unity` (Main Playable Scene)
- `Assets/Settings/`
- `Assets/InputSystem_Actions.inputactions`
- `Assets/TextMesh Pro/`
- `Assets/AI Toolkit/`
- `Assets/TutorialInfo/`

## Unity Version
6000.4.7f1

## Game Genre
3-lane endless runner / rhythm dash game

## Game Vision
The player runs forward automatically, switches lanes, jumps, and dashes through obstacles while the world reacts to music.

## Current Implemented Features
- GameManager-driven GameState (Ready, Playing, Paused, GameOver)
- Start flow from Ready state (Space/Enter)
- Pause/Resume system (Escape/P)
- 3-lane movement
- Jump
- Dash
- Obstacle collision
- Endless track generation (gated by game state)
- Score system
- Ready UI prompt (Press Space or Enter to Start)
- Pause UI overlay (Paused — Press Esc or P to Resume)
- Verified GameOver UI (Red GAME OVER, Press R to Restart)
- Camera follow
- Background music
- Music-reactive system foundation
- Enhanced obstacle/lane logic (laneWidth field, centralized selection)
- Two-lane obstacle patterns with one-open-lane safety
- Lane streak safety (single-lane and pattern-open-lane)
- Polished player feel (responsive lane switching, snappy jump, balanced dash)
- Stabilized camera FOV (state-aware growth)
- Clean project structure (`Assets/_Dash`)

## Current Controls
- **Space / Enter**: start game (from Ready state)
- **Escape / P**: pause or resume game
- **A / D or Left / Right arrows**: lane movement
- **Space**: jump (when Playing)
- **Left Shift**: dash (when Playing)
- **R**: restart after game over

## Main Playable Scene
`Assets/Scenes/SampleScene.unity`

## Setup Instructions
1. Clone repo.
2. Open with Unity 6000.4.7f1.
3. Open `SampleScene`.
4. Press Play.

## Development Rules
- Keep main branch playable.
- Commit .meta files.
- Do not commit Library, Temp, Build, Logs, or UserSettings.
- Use small commits.
- Do not add new packages without approval.

## Roadmap Summary
- Phase 1: stabilize project
- Phase 2: complete core game loop
- Phase 3: player feel polish
- Phase 4: track patterns and object pooling
- Phase 5: audio and music reactivity
- Phase 6: UI/UX polish
- Phase 7: visual identity
- Phase 8: build and test
- Phase 9: final polish

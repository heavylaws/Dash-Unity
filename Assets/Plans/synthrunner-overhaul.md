# Project Overview
- Game Title: Synth-Runner (Placeholder)
- High-Level Concept: An endless runner in a neon synthwave environment where the player dodges obstacles to the beat of the music.
- Players: Single player
- Inspiration / Reference Games: Audiosurf, Sayonara Wild Hearts, Synthwave aesthetics.
- Tone / Art Direction: 80s Retro-Futurism / Neon / Synthwave.
- Target Platform: PC (StandaloneWindows64)
- Screen Orientation / Resolution: Landscape
- Render Pipeline: URP (PC_RPAsset)

# Game Mechanics
## Core Gameplay Loop
The player moves forward automatically at increasing speeds. They must switch between three lanes to avoid obstacles. The music provides the rhythm, and the environment reacts to the beat. Surviving longer increases the score.

## Controls and Input Methods
- **A / Left Arrow**: Move to the left lane.
- **D / Right Arrow**: Move to the right lane.
- **Space**: Jump (New mechanic to add depth).

# UI
- **HUD**: Top-right score display showing distance/time survived.
- **Game Over**: A glitchy screen overlay with "GAME OVER" text and a "Press R to Restart" prompt (replaces immediate reload).

# Key Assets & Context
- **Scripts**: 
    - `PlayerController.cs`: Refactor for snappier movement and tilt.
    - `CameraFollow.cs`: Add dynamic FOV and tilting.
    - `TrackGenerator.cs`: Update to support new visual styles for tiles.
    - `GameManager.cs`: Track score and handle death state.
- **Materials**: 
    - `Mat_GridFloor`: Neon grid shader.
    - `Mat_NeonObstacle`: Emissive material for cubes.
    - `Mat_Player`: High-glow material for the player vehicle.

# Implementation Steps
1. **Refactor Player Movement**
   - In `PlayerController.cs`, replace the lerp-based lane switching with a `SmoothDamp` or a fixed-duration transition for snappiness.
   - Add a `targetRotation` that tilts the player in the direction of movement.
   - Add a simple `Jump` logic (Vertical movement curve).

2. **Enhance Camera Juice**
   - Update `CameraFollow.cs` to include `fieldOfView` modification based on player speed.
   - Add a slight roll to the camera when the player switches lanes.

3. **Visual Aesthetics (Shaders & Materials)**
   - Create a shader (or use a standard URP shader with Emission) for the floor grid.
   - Configure the `Global Volume` with higher Bloom, Chromatic Aberration, and a subtle Vignette.
   - Add "Neon Mountains" to the background (simple meshes with wireframe materials).

4. **Game Loop & Feedback**
   - Update `GameManager.cs` to calculate a score based on distance.
   - Add a Particle System for the player's engine trail.
   - Modify the `OnTriggerEnter` in `PlayerController` to trigger a "Death Effect" (time scale reduction, camera shake) instead of an instant reload.

5. **UI Implementation**
   - Create a simple uGUI Canvas with a Score text.
   - Implement a basic Game Over screen.

# Verification & Testing
- **Movement Test**: Ensure switching lanes feels "crisp" and not "sluggish".
- **Visual Check**: Verify the Bloom isn't blowing out the screen while still giving a strong neon glow.
- **Death Test**: Check that colliding with an obstacle feels impactful (shake + slow-mo) before restarting.

# Project Phases

## Overall Vision
Dash-Unity is a high-energy, music-reactive endless runner where the environment pulses and shifts to the beat.

## Development Rules
- Keep main branch playable.
- Documentation must be updated each phase.
- No new packages without approval.
- Follow existing code patterns.

## Current Completed Phases
- Phase 0: Baseline Audit
- Phase 1A: Scene Restoration and Basic Game Loop Repair
- Phase 1B: Project Documentation and AI Development Log
- Phase 1C: Safe Project Structure Planning
- Phase 1D: Assets and Script Migration to _Dash
- Phase 1E: Documentation Update and Project Checkpoint
- Phase 2A: Core Game State Flow
- Phase 2B: UI Feedback
- Phase 2C: Enhanced Obstacles and Lanes
- Phase 3: Player Feel Polish

## Current Active Phase
- Phase 4: Track Patterns and Object Pooling (In Progress)

## Upcoming Phases
- Phase 4: Track Patterns and Object Pooling
- Phase 5: Audio and Music-Reactive Polish
- Phase 6: UI/UX Polish
- Phase 7: Visual Identity
- Phase 8: Build, Test, and Release
- Phase 9: Final Polish

## Phase Definitions and Done Criteria

### Phase 0: Baseline Audit
- **Goal**: Understand the current state of the project.
- **Done**: Audit report created.

### Phase 1A: Scene Restoration and Basic Game Loop Repair
- **Goal**: Make the game playable again.
- **Done**: Scene works, player can move, die, and restart.

### Phase 1B: Project Documentation and AI Development Log
- **Goal**: Set up management docs.
- **Done**: README.md, AI_DEVELOPMENT_LOG.md, and PROJECT_PHASES.md exist.

### Phase 1C: Safe Project Structure Planning
- **Goal**: Plan folder structure and namespace conventions.
- **Done**: Structure plan approved.

### Phase 1D: Assets and Script Migration to _Dash
- **Goal**: Organize game-owned assets into a safe, dedicated folder.
- **Done**: Scripts, prefabs, materials, audio, shaders, and docs moved to `Assets/_Dash`. References verified.

### Phase 1E: Documentation Update and Project Checkpoint
- **Goal**: Reflect project changes in documentation.
- **Done**: README.md, AI_DEVELOPMENT_LOG.md, and PROJECT_PHASES.md updated.

### Phase 2A: Core Game State Flow
- **Goal**: Create a proper game state flow for Ready, Playing, Paused, GameOver, and Restart behavior.
- **Done**: State machine in GameManager implements Ready, Playing, Paused, and GameOver. Controls gated by state. Main branch remains playable.

### Phase 2B: UI Feedback
- **Goal**: Implement UI feedback for Ready, Paused, and GameOver states.
- **Done**: Ready UI prompt and Pause UI overlay implemented in SampleScene. GameOver UI verified and preserved. UI panels correctly toggle based on game state. Main branch remains playable.

### Phase 2C: Enhanced Obstacles and Lanes
- **Goal**: Add variety to obstacles and lane challenges.
- **Done**: TrackGenerator uses a serialized laneWidth field. Lane selection is centralized. Single obstacles avoid excessive same-lane streaks. Two-lane obstacle patterns spawn occasionally with one-open-lane safety. Pattern open lanes avoid repeated streaks. Existing prefab reused. *Note: No object pooling or mobile controls.*

### Phase 3: Player Feel Polish
- **Goal**: Improve movement, jump, and dash feedback.
- **Done**: Lane switching is smooth and responsive with visual tilt. Jump feel is snappy with high gravity. Dash is balanced and readable. Camera FOV reacts to gameplay state and remains bounded. All player movement and world generation correctly gated by game state.

### Phase 4: Track Patterns and Object Pooling
- **Goal**: Create reusable track segments and optimize performance.
- **Done**: Track lifecycle cleanup completed. Script defaults aligned with scene values. Obstacles are now parented to their source track tiles. Redundant `activeObstacles` tracking removed. Hierarchy and lifecycle prepared for future pooling. *Note: Object pooling and pattern expansion are future work.*
- **Next Step**: Phase 4J planning only (pooling feasibility or pattern expansion).

### Phase 5: Audio and Music-Reactive Polish
- **Goal**: Integrate music and reactive visuals.
- **Done**: World reacts to BPM and frequency bands.

### Phase 6: UI/UX Polish
- **Goal**: Professional UI menus and HUD.
- **Done**: Main menu, Settings, and polished HUD.

### Phase 7: Visual Identity
- **Goal**: Apply consistent art style and lighting.
- **Done**: Materials, shaders, and lighting finalized.

### Phase 8: Build, Test, and Release
- **Goal**: Final testing and build production.
- **Done**: Clean build for target platform.

### Phase 9: Final Polish
- **Goal**: Final project refinement.
- **Done**: Project ready for release.

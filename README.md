# SimpleSpaceInvaders
This is my take on a Space Invaders. I spent half of Saturday and half of Sunday to develop this.
I borrowed the sprites from https://github.com/danz1ka19/Space-Invaders-Unity-Clone.

# How to Play
A, D or Left, Right to move.
Space to shoot.

# Goal
## Data Diven
Waves and levels are defined by .asset files. It allows waves and levels to bereuseable and editable by level designers.
## Minimal Memory Footprint
Mininum number bullets and enemy GameObjects are instantiated on the scene.

# WaveEditor (new!)
Game Designers can use the WaveEditor script to easily place Enemies into columns. This feature takes full advantage of the Unity editor, which is already a very well established WYSIWYG editor.

How to use:
1. Create an empty GameObject in a empty scene, attach a WaveEditor component to the empty GameObject.
2. Create an empty GameObject as children of the WaveEditor object. These are 'columns'.
3. Drag the prefabs of the enemies into the columns. Place  the enemy instances into creative and fun formations (only use prefabs!).
4. Click Save in the WaveEditor inspector to save the columns and enemies into a WaveData asset.
5. Click Load in the WaveEditor inspector to load and edit a WaveData that was previously created.

# BulletManager
BulletManager instantiates the approporiate number of bullets that may be active on the scene. This approach is better than instantiating and destroying bullets during runtime, because it does not suffer from memoy fragmentation.

# LevelManager
LevelManager instantiates the least enemy GameObjects required on the level. LevelManager iterates through the waves of the level to determine how many of each enenmy type needs to be instantiated and saved to the pool. On start of each wave, approporiate GameObject instances are pulled from the pool and activated.

# BehaviourHandler and Behaviours
BehaviourHandler is a simple FSM to handle Boss behaviour. BehaviourHandler handles transition between Behaviours. BehaviourHandler also includes a AI parameters dictionary and a cooldown manager.

# To Do
- ~~Implement WaveEditor scene~~
  - ~~Game designers should be able to freely add and edit columns and enemies visually (WYSIWYG)~~
  - ~~Allow enemies to be placed freely in the column (not just straight vertical columns)!~~
  - ~~Change LevelManager implementation to process the new WaveData format~~
  - Implement sanity check for Saving/Loading WaveData
- Port BehaviourHandler into a behaviour tree
  - Port WaveController into a behaviour tree
- Make BehaviourHandler data driven
- Add sounds/effects
- Multiple scenes and transitions
- Implement circular queue for GameObject pools

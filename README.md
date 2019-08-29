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

# BulletManager
BulletManager instantiates the approporiate number of bullets that may be active on the scene. This approach is better than instantiating and destroying bullets during runtime, because it does not suffer from memoy fragmentation.

# LevelManager
LevelManager instantiates the least enemy GameObjects required on the level. LevelManager iterates through the waves of the level to determine how many of each enenmy type needs to be instantiated and saved to the pool. On start of each wave, approporiate GameObject instances are pulled from the pool and activated.

# BehaviourHandler and Behaviours
BehaviourHandler is a simple FSM to handle Boss behaviour. BehaviourHandler handles transition between Behaviours. BehaviourHandler also includes a AI parameters dictionary and a cooldown manager.

# To Do
- Implement WaveEditor scene
  - Game designers should be able to freely add and edit columns and enemies visually (WYSIWYG)
  - Allow enemies to be placed freely in the column (not just straight vertical columns)!
  - Change LevelManager implementation to process the new WaveData format
- Port BehaviourHandler into a behaviour tree
  - Port WaveController into a behaviour tree
- Make BehaviourHandler data driven
- Add sounds/effects
- Multiple scenes and transitions
- Implement circular queue for GameObject pools

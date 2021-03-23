Use Scenes/Goye Workspace/GoyeWorkspace Scene as your test ground

Alf control set:
	left ctrl: role/dash
	spacebar: jump/jump down from stairs
	wasd/arrow keys: movement
	mouse3/x: parry
	mouse1/z: attack
	
Node:
/Scenes is a parent node holding all sub scenes
/Scenes/SampleScene for instance, is a sub scene
/Scenes/SampleScene/Background(Foreground/Main/etc.) used as nodes holding different image layers
/Scenes/SampleScene/Colliders used for colliders
	=> Colliders/World Wall is used as defining world wall, using Layer "Ground"
	=> Colliders/*place* used as you please, however, all platform collider should use Layer "Platform",
		plus a platform effector at the same transform, with oroperty surface Arc set to 60, tick "used by effector"
		of all box collider 2D
	
p.s. there is a composite collider is used for tile map type colliders, at the moment please use it instead of 
the collider types afore mentioned.

Creating Tile map:
First, create a tile map pallete some where inside your workspace folder. tips: use right click menu.
Second, create a node in your current scene node, and add a tilemap component to it.
Third, adjust your tilemap pallete/tilemap component properties, such as how many pixels per unit(try 32/64/16)
Finally, you are ready to go.
Bonus: try adding tilemap collider 2D to your tilemap, while using composite collider 2D, all should be found in
	Menu/Component/Tilemap

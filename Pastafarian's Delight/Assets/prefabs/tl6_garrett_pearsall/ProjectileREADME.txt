Purpose:
The projectile prefab is needed for use in the ranged attack. It is called and is used to clone gameObjects for each projectile used.

What functions:
This prefab is mainly used in the weapon class where we need to refrence a gameObject that will perform as a projectile. THere is a script attached to the prefab that defines how the projectile will function when it is spawned. In 
the script I have inculded multiple functions listed below

Start() this is the initaliztion of the projectile where it clones the projectile and sets a certian amount of time before the object gets destroyed
Update() this is used for detecting if it collides with something in the trajectory, I used raycasting to detect collision with any solid object in the game(walls and enemies)
DestroyProjectile() this is the method called to destroy the projectile

How to use:
If you use the weapon class you will have to assign a gameObject for a projectile refrence, you drag and drop this into the box

Summary:
This prefab allows you to use and spawn projectiles that will fire in a ceritan direction if needed with the weapon class
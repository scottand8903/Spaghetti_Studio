# Dart Projectile Prefab

## Overview
The Dart Projectile prefab is a Unity component that allows you to add a dynamic projectile mechanism to your 2D games. It handles movement, collision detection, and player interaction seamlessly.

---

## Features
- **Customizable Speed and Lifetime**  
  Adjust the projectile's speed and how long it exists in the scene.
  
- **Collision Handling**  
  Detects collisions with the player and solid objects, applying health updates as needed.
  
- **Directional Movement**  
  Easily set the projectile's direction programmatically for precise control.

- **Health Integration**  
  Reduces player health upon collision using a `HealthSystem` integration.

---

## Components
1. **Dart Projectile Script**
   - Manages projectile behavior including movement and destruction.
   - Integrates collision logic with the player and environment.

2. **Rigidbody 2D**
   - Applies realistic physics to the projectile.

3. **Box Collider 2D**
   - Detects collisions with players and solid objects.
   - Ensure proper sizing for accurate hit detection.

---

## Setup Instructions
1. Drag the Dart Projectile prefab into your Unity scene.
2. Configure the following parameters in the **Inspector**:
   - Speed: Set the desired speed for the projectile.
   - Lifetime: Specify how long the projectile should exist in the scene.
3. Ensure the **Box Collider 2D** dimensions match the size of your sprite for accurate collision detection.
4. Programmatically set the direction of the projectile using the `SetDirection()` method.
5. Test the prefab in **Play Mode** to verify its behavior.

---

## Usage
### Script Example
```csharp
DartProjectile dart = projectileObject.GetComponent<DartProjectile>();
dart.SetDirection(Vector2.right); // Set the direction to right
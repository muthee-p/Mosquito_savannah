Increase fps(frames per second)
- Qulity settings are set to High quality, instead of ultra high
- camera culling on static objects
- some textures and meshes are compressed
- used a unity asset that reduces LOD(levelof detail in plants)
- used Baked lights
- use LookAt Constraint component for makers to facetheplayer instead of a script
- moved the sky calculations to a coroutine instead of the update()
- in place of Canvas where nothing changes eg. the station markers, i used sprites with the words and images on them
- Particle systems have a trigger script that plays them when the player is near and stops when the player is away
- Avoided the use of mesh colliders and opted for built in mesh like cubes n spheres coliders


modified Character movement
- the character swimming is actully the in air animation,which is triggered when the player is off the ground.
  The script raises the player a little when they collide with a plane inside the ponds

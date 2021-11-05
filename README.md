# Simple Turn-Based Fighter

### Developer's Notes
* I aimed to write everything as clean as possible and apply patterns of OOP appropriately.
* I considered SoC and SOLID principles while I'm designing the architecture of the code.
* I didn't include a pooling system into the project with the intent of minimizing complexity and the number of problems that may arise in limited development duration.
* All prefabs are designed to be as reusable as possible for the same type of entities.
* All data containers are designed to be easily updatable with minimal compilation time and as parametric as possible.
* Script execution starts from the Start method of the Initializer script.

### About the game
* All heroes and bosses have attack power and health attributes.
* The player picks 3 heroes from her/his hero pool and fights with a random boss in the next scene.
* In the combat scene the player picks which hero to attack. The boss attacks after an attack made by the hero of the player.
* All of the player's heroes or the boss, whichever dies first, loses the game.
* Heroes of the player who survive in a combat gain experience points. When a hero gains enough experience points it levels up and its attack power and health attributes get improved depending on its level.

### Gameplay Sample

https://user-images.githubusercontent.com/12052479/140514482-33c9d724-5343-431e-abd1-cebbfe4d9f29.mp4

Team Graphix Kids presents: Space Maze, a project by Tristan Sallin, Jerome White, Leann Mayberry, Philip Ogunshola, and Heba Mawad

Member information 
Tristan Sallin: tsallin3@gatech.edu     tsallin3
Jerome White: jwhite335@gatech.edu     jwhite335
Leann Mayberry: lmayberry3@gatech.edu     lmayberry3
Philip Ogunshola: pogunshola3@gatech.edu     pogunshola3
Heba Mawad: hmawad3@gatech.edu     hmawad3

--How to Play--
Your goal is to escape the spaceship. Hit all the red buttons to open the doors in each room, find the boss room, and defeat the boss to reveal 
the ladder to the next floor. There are 5 floors of increasing danger. I promise it's beatable, but you will die a lot.

Installation: Use a windows computer. Click inside the "Builds" folder and activate "Space Maze.exe"

--Rubric Requirements--
3D Game Feel Game: The game is in 3 dimensions.
The goal is defined by escaping the spaceship after 5 floors. You can fail by reaching 0 hp.
The player's success is shown during gameplay with the "level" and "health bar" UI
It is shown explicitly by the win screen (I'd be surprised if you won) and the 
menu that says "game over".
There is a start menu with the functions to start game, exit to desktop, or play the 
game's music.
On a win, you have a button to return to the main menu. On a loss, you can restart or 
return to the main menu.
Camera is third person.

Precurors to Fun Gameplay: The main goal is shown in text when starting a new game, and controls 
are displayed in the first room of the game. Subgoals are in the form of score and timer 
that the player can see.
To make interesting choices, players can fullclear levels to get powerups and score, 
or rush the boss to win quickly and avoid attrition. Within a room, players can sometimes 
go for optional pickups or ignore them.
The consequences of the player's choices are often between living and dying. If they explore 
too much they might die in a bad room, if they don't explore enough the final boss is obnoxious 
and will probably kill them.
The player can engage with buttons to progress, platforms to jump on, hazards to avoid (mines, 
spikes), and enemies to kill (turrets, missiles, crawlers, swordbots, spreadbots, bosses). 
They can also activate a hacked robot by walking into a terminal, giving their movement control over 
two entities. 
There are some "portal roullette" rooms, but they're not devastating when the wrong choice 
is picked (and players can learn the layout over multiple climbs). I don't think 
there are really hollow or obvious decisions, although the pcg can generate a line if it's 
feeling boring.
I don't think there are really fun killers, the same room can't be generated 
multiple times on the same floor to avoid repetition, although the game can 
be frustrating due to the difficulty curve.
The player has limited resources in terms of health and health/shield powerups each 
run, and it is always possible to win with those resources (no forced damage)
This is a roguelike, there is extreme punishment for failure (but runs take like 15 minutes so its 
not as bad as some). Success is rewarded generally with having more health for the later 
parts of the run (and eventually winning the game).
The training is a multistage process. I'll talk about difficulty progression as well here. 
There is a tutorial room that shows the player the controls and the button pressing 
system for progression, and each floor generates a set from 25 rooms. The rooms have harder variations 
as the floor number increases, so players get an idea of how to clear the rooms on lower floors and 
can show their proficiency on the higher floors. In addition, enemies get a damage, health, and 
speed multiplier as floor level increases.
If you can cheese this game I will be very impressed.

3d Character with real time control: 
The player character was imported as the Adobe Mixamo "Astra" Character, and controls 
snappily in 8 directions and can jump with the spacebar. It uses root motion.
Moving the character is the only thing the player does, so it is tied into the gameplay.
The inputs and physics of the character were set by the team, shown in the scripts 
that begin with "Astro"
Character has running and jump animations.
Movement is continuous
WASD SPACE. I can think of several games with this control scheme.
The fluidity is decent, a compromise was made to give instant turning for better gameplay
Player character only slides when an external force pushes it
My input delay is a lot better than Smash Ultimate's
Camera is fixed within a room as to provide the player with total information over the 
challenges of the room (Binding of isaac design). The camera does move continuously 
when the player changes rooms.
The camera is at a height that does not enter objects.
All hitboxes have a hit sound to notify the player of damage (self or enemies). 
Buttons give a noise when pressed, doors swoosh when opened, projectiles boom when fired, 
and powerups ding when touched.
Main physics coupling is just the jumping, there wasn't much need elsewhere.

NPC Steering/AI: All of the AI is written from scratch. 
Different agents have different state machines with chase/attack/wander states, others 
use timers to control their behavior.
NAVMeshes are not used because all geometery is generated at runtime and 
it's arguable if the game would even be better with obstacle avoidance.
The steering is physics based with forces and all agents have a max turn velocity.
No humanoid AI agents.
AI chases players, backs off if it collides with an obstacle, and the sword robot 
will swing if the player is close to it. Most bullet shooters 
shoot at the player with some random noise added to direction.
The only sudden movement in the agents is when the sword bot hits an obstacle 
and instantly turns around, that snaps the sword behind it. Other movements are all smooth.
The AI isn't that expressive, although mines blare when the player is close, sword robots can 
be evaluated from their movement, and the boss turret's current attack is fairly clear. 
Other evaluation of the AI is from seeing their "shot intervals" and looking at their orientation.
Everything but the missiles, mines, and spikes scale in difficulty with the level 
(AI wise, not just damage). Most have shorter intervals between volleys and move faster, 
and the bosses get new tricks later on.
There's no perceptual model, but the rooms are small enough that it doesn't need one to 
seem fair. Again, Isaac design.
The AI is meant to be exploited into interacting with the environment for you, by pressing buttons
for you and killing each other. They are purposefully designed to be "stupid" in this regard.

Polish: Start menu again can start game, quit to desktop, view credits, and play music.
Pause menu can restart, change volume, quit to main menu, or quit to desktop.
Nothing is out of place for a game once you open the executable.
There is no debug lines shown. Code does have debug logs in it but it should be 
hidden for the build.
Escape - quit game - it's closed. 
All cheat codes have been disabled.
GUI elements use the default unity stuff, we lacked artists.
There is a loading screen, but loads are so fast that it never gets seen. 
level transitions are fast, but they aren't super jarring. Time pauses while the camera 
pans between rooms to give short breaks in the gameplay.
Polish effects include spinning backgrounds of space, different color schemes each floor, 
random wall textures, explosion effects, spinning collectibles, many sound effects, 
music change for bosses, and color changes to signify environmental changes.
The art-style is blocks because no artist. But it is trying to keep a consistent space theme
The lighting is simple, but the color pallettes theme each floor, and the music is jammin'
I'll talk about glitches in it's own section.
I'm pretty sure you can't glitch out of bounds any more, but no promises.
Since it's a spaceship in space, you can see outside the playable bounds into the void
I do not believe there are softlocks. It is possible to screw up and be forced to jump 
on a mine button if a puzzle is messed up.
My laptop froze once when running the CS 3600 pacman game and it runs this game fine.

As for the fun component, just surrender to the addiction. Just kidding, I hope you 
have fun getting progressively further in the game. 

There's no special instructions for checking the requirements, I believe by playing the game 
normally everything can be seen, except for the win screen - that requires skill.
There is a picture that shows it in this zip file.

--Known Bugs--
When the player jumps and hits a corner of a block, they sometimes shoot high into the air
The player can midair jump if they're running actively into a corner sometimes
In the editor version, the player can clip out of bounds if the game lags (I feel like 
this might be able to happen in the build version if the game lags, but I'm not sure)
The crawling robots can sometimes get stuck slightly in a wall, but they eventually get out
The pushable boxes are kinda screwy and sometimes emit a large force when pushed against a wall
There's an invisible wall sometimes in the room where you move a hacked bot along a track in the air

--External Resources Used--
Adobe Mixamo "Astra" Character


--Who did what--
Tristan - Procedural generation, Music, AI Agents, room design, Score system, 
several prefabs, textures
Jerome - collectible and terrain prefabs, portals, room design, menus
Leann - working extensively with the playable character, room design
Phillip - Room design, prefabs, sound effects
Heba - *cricket churping sounds here*

Scenes to open in unity:
GameMenuScene, Master Scene, Win (Same as in the build)

E-mail Tristan if there's problems/questions/high scores

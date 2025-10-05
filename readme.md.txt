Better Horse Handling v1.0.0
Author: caberfra (github.com/caberfra)

This mod increases the base handling of the horse for Story of Seasons: Grand Bazaar.
I found the horse sluggish and not fun to use, this fixes that in a vanilla friendly way.
Your horse's stats will still increase proportionately to your friendship level <3.

This config is fully customizable, see below for the main settings you can choose.
I've included my notes for each stat that a horse has in the game, feel free to tinker and see what feels right for you.

Settings:
- Debug: Enables detailed logging for diagnostics.
- AlwaysMaxStats: Forces max stats regardless of friendship.
- UseExponentialInterpolation: Enables exponential scaling.
- InterpolationExponent: Controls curve steepness (higher = more aggressive).

========================================================================

Explanation of Horse stats
    
    Definitions
    
        Speed: Horse's current speed
    
        Galloping: Horse top speed after the gallop button is pressed (RT on gamepad)
        
        Horse animations:
            Level 1: Walk / Trot
            Level 2: Canter				triggered right after gallop button is pressed
            Level 3: Gallop				triggered once Speed >= RunMotionBorderSpeed
            
        Horse turning animations: 
            Level 1: Normal				normal turn animation while horse speed < TurnBorderSpeed
            Level 2: Sharp				sharper turn triggered by TurnBorderSpeed
                                            makes the horse turning overall look janky if TurnSpeed is really high
                                            overrides TurnSpeed
    
    HorseSpeedSettings.Array Object Properties
    
        "MinFriendLevel": 5,			
        "MaxFriendLevel": 5,				
        
            Unimportant note on the two variables above, they dictate a range of friendship level(s) for which the horse stats below will be applied.
            The game will iterate through HorseSpeedSettings.Array and choose the first Object whose friendship level range applies to your horse's current friendship level.
                e.g. If the first Object in HorseSpeedSettings.Array has 
                    "MinFriendLevel": 0,
                    "MaxFriendLevel": 10,
                Then the game will ALWAYS apply that Object's values regardless of current friendship level.
        
        "InitSpeed": 4.0,					Horse top speed without galloping
        "MaxSpeed": 10.2,					Horse top speed
                                                vanilla = 10.2
                                                
        "RunMotionBorderSpeed": 8.0,		Speed at which the horse animation goes from level 2 to level 3
                                                Should roughly be 0.66 of MaxSpeed
                                                Level 3 animation speed scales up to approximately 1.5 * RunMotionBorderSpeed
        
        "RunMotionBorder": 1.275,			Horse stamina will start draining once Speed == RunMotionBorder * 8 
                                                Stamina will not drain (except from jumping) if RunMotionBorder * 8 > MaxSpeed 
                                                At RunMotionBorder == 0, stamina will be constantly drained, even when stationary
                                                
        "TurnBorderSpeed": 12.0,				Speed at which the horse turning animation goes from level 1 to level 2;	
                                                recommended to set TurnBorderSpeed > MaxSpeed to disable level 2 horse turning animation
                                                
        "TurnSpeed": 500,					Horse turn speed
                                                recommended = 500
        "DecelerationTurnSpeed": 10.0,
        "DecelerationSpeed": 10.0,
        "AccelerationWalk": 20.0,			Rate of acceleration from stop to InitSpeed
        "AccelerationRun": 20.0,			Rate of acceleration from InitSpeed to MaxSpeed
        "MinMotionSpeed": 1.0,				??
        "MaxMotionSpeed": 1.0,				??
        "JumpHight": 5.5,					Set JumpHight = MaxJumpHight + 6 to reach max jump height without 'floating'
        "MaxJumpHight": 1.3,				Need 3.5 to jump over the yellow windmill river
        "JumpForwardPower": 1.75			??
	
========================================================================
	
Below is a table of preset values I found viable in terms of gameplay.

    Modded:     default for this mod; increased acceleration and handling; same max speed and stamina as vanilla
    Stamina:    infinite stamina
    Modded+:    a little bit faster
    Jump:       jumping is high enough to clear the invisible wall along the river near the yellow windmill
    Speed:      high speed for fun; not recommended if you care about the horse derbies
    Ultimate:   highest jump and speed for those that don't care about balance
	
    Property					0 Hearts	10 Hearts	Modded		Stamina		Modded+		Jump		Speed		Ultimate
                                                                                
    "MinFriendLevel": 			0,			10,			0,			0,			0,			0,			0,			0,		
    "MaxFriendLevel": 			0,			10,         10,         10,     	10,         10,         10,         10,   
    "InitSpeed": 				3.0,		3.0,        4.0,        4.0,    	4.0,        4.0,        8.0,        8.0,  
    "MaxSpeed": 				7.0,		10.2,       10.2,       10.2,   	12.0,       10.2,       18.0,       18.0, 
    "RunMotionBorderSpeed": 	7.0,		7.0,        8.0,        8.0,    	8.0,        8.0,        10.0,       10.0,  
    "RunMotionBorder": 			0.6,		0.6,        1.20,       1.5, 		2.0, 	    1.275,      1.2,        3.0,
    "TurnBorderSpeed": 			7.9,		7.9,        12.0,       12.0,   	12.0,       12.0,       25.0,       25.0, 
    "TurnSpeed": 				120.0,		120.0,      500,        500,    	500,        500,        600,        600,  
    "DecelerationTurnSpeed":	1.0,		1.0,        10.0,       10.0,   	10.0,       10.0,       10.0,       10.0,   
    "DecelerationSpeed": 		0.2,		0.2,        10.0,       10.0,   	10.0,       10.0,       10.0,       10.0,   
    "AccelerationWalk": 		5.5,		5.5,        20.0,       20.0,   	20.0,       20.0,       20.0,       20.0, 
    "AccelerationRun": 			5.0,		5.0,        20.0,       20.0,   	20.0,       20.0,       20.0,       20.0, 
    "MinMotionSpeed": 			1.0,		1.0,        1.0,        1.0,    	1.0,        1.0,        1.0,        1.0,  
    "MaxMotionSpeed": 			1.0,		1.0,        1.0,        1.0,    	1.0,        1.0,        1.0,        1.0,  
    "JumpHight": 				5.5,		5.5,        5.5,        5.5,    	5.5,        10.0,       5.5,       	15.0,  
    "MaxJumpHight": 			1.3,		1.3,        1.3,        1.3,    	1.3,        4.0,        1.3,        9.0,  
    "JumpForwardPower": 		2.0			0.5         1.75        1.75    	1.75        1.75        1.75        1.75  
    
========================================================================

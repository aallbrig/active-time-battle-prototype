## Active Time Battle Prototype Practice

This repo contains a prototype of active time battle (ATB), implemented largely from finite state machines and the command design pattern. I have limited time, so we'll see how far I can go.

### TODO

    [ ] Finite state machine for ATB
        [ ] Create base/abstract/interface to describe generic ATB state
            [ ] Enter
            [ ] Tick
            [ ] Leave
        [ ] Start menu state
            [ ] On enter
                [ ] Play start menu enter animation
                [ ] Title and two UI buttons are presented to user
            [ ] On tick
                [ ] Start Fight UI button
                    [ ] Create new battle statistics
                    [ ] Transition to Begin Battle state
                [ ] Quit Game UI button
                    [ ] Unload game from context
                    [ ] (optional) hide from webGL based players
            [ ] On leave
                [ ] Play start menu exit animation
        [ ] Begin Battle state
            [ ] On enter
                [ ] (optional) Play "battle start" camera animation
                [ ] Generate player enemies from list of possible enemies (ATB Fighter)
                [ ] Announce battle
                    [ ] "x enemies encountered!" where x is the number of enemies generated
            [ ] On tick
                [ ] Wait for player interaction to iterate through battle begin announcements (spacebar?)
                [ ] After iterating through every announcement, transition to battle state
        [ ] Battle state
            [ ] On enter
                [ ] (optional) Focus camera on battle view
                [ ] Setup player ATB input state machine
            [ ] On tick
                [ ] player ATB input state machine.Tick()
                [ ] If all player ATB fighters are dead, transition to Battle lose state
                [ ] If all player enemies are dead, transition to Battle victory state
        [ ] Battle victory state
            [ ] On enter
                [ ] Present battle statistics and two UI buttons
            [ ] On tick
                [ ] Continue battle UI button
                    [ ] Transition to Begin Battle state
                [ ] Quit battle UI button
                    [ ] Transition to Start menu state
        [ ] Battle lose state
            [ ] On enter
                [ ] Present battle statistics and one UI buttons
            [ ] On tick
                [ ] Restart UI button
                    [ ] Transition to start menu state
    [ ] (optional? Technically provided by animation controller?) Finite state machine for RTS toons
        [ ] Create base/abstract/interface to describe generic RTS toon state
            [ ] Enter (play animation)
        [ ] Idle state
        [ ] Combat Idle state
        [ ] Charge state
        [ ] Melee Attack state
        [ ] Ranged Attack state
        [ ] Dead state
        [ ] (optional) Spell casting state
        [ ] (optional?) Walking state
        [ ] (optional?) Running state
    [ ] Finite state machine for Player ATB Input (expects an ATB Fighter to know what actions to show)
        [ ] Create base/abstract/interface to describe generic player input state
            [ ] Enter (show player UI)
            [ ] Leave ((optionally) hide player UI)
        [ ] Choose action state
            [ ] Present player with list of possible ATB fighter actions
                [ ] On action click, transition to Action
        [ ] Choose target state
        [ ] Player waiting state
    [ ] Controllers
        [ ] ATB controller (provides FSM context for ATB)
        [ ] (optional?) RTS Toon controller (provides FSM context to RTS toon) (nav mesh agent controller)
        [ ] Nav mesh agent controller
        [ ] Animation controllers
            [ ] (optional?) interface for animation controller classes
                [ ] string currentAnimationState
                [ ] void TransitionToAnimation(string animationName)
                [ ] bool AnimationClipIsPlaying()
            [ ] RTS toon animation controller
                [ ] Unity animation controller
                [ ] Animation Controller class
            [ ] Main menu animation controller
                [ ] Unity animation controller
                [ ] Animation Controller class
            [ ] ATB player menu animation controller
                [ ] Unity animation controller
                [ ] Animation Controller class
            [ ] Battle round end animation controller (for both victory and lose state)
                [ ] Unity animation controller
                [ ] Animation Controller class
            [ ] Camera animation controller
                [ ] Unity animation controller
                [ ] Animation Controller class
        [ ] Player controller (provide user interface for player input)
            [ ] Player is able to choose from ATB fighter actions from UI
        [ ] Enemy AI controller
            [ ] Enemy AI is able to choose from ATB fighter actions
    [ ] ATB Fighter (typed object for ATB Fighter types)
        [ ] Max health
        [ ] Current health
        [ ] Contains reference to a IATBFighterType fighter type (passed in from constructor)
        [ ] Contains list of possible actions ATB fighter can take (fighterType.ActionList())
        [ ] Create interface for what an ATB fighter can do
            [ ] DealDamage -> fighterType.DealDamage()
            [ ] TakeDamage -> fighterType.DealDamage()
            [ ] Die -> fighterType.Die()
    [ ] ATB Fighter Types
        [ ] Human Warrior
            [ ] Action list
                [ ] Melee Attack (target)
        [ ] Orc Warrior
            [ ] Action list
                [ ] Melee Attack (target)

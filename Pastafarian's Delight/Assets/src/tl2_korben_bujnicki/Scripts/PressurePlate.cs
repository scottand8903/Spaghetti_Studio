using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public DartTurret connectedTurret; // Reference to the turret to fire

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            if (connectedTurret != null)
            {
                connectedTurret.FireDart();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = false; // Allow re-triggering if desired
        }
    }
}
/*
[Intro: Freddie Mercury]
Mmm num ba de
Dum bum ba be
Doo buh dum ba beh beh

[Verse 1: David Bowie & Freddie Mercury]
Pressure
Pushing down on me
Pressing down on you
No man ask for
Under pressure
That burns a building down
Splits a family in two
Puts people on streets

[Refrain: Freddie Mercury]
Um ba ba be
Um ba ba be
De day da
Ee day da
That's okay

[Chorus: David Bowie & Freddie Mercury]
It's the terror of knowing what this world is about
Watching some good friends screaming, "Let me out"
Pray tomorrow gets me higher
Pressure on people, people on streets
See upcoming rock shows
Get tickets for your favorite artists
You might also like
Say Don�t Go (Taylor�s Version) [From The Vault]
Taylor Swift
all-american bitch
Olivia Rodrigo
Daylight
Drake
[Post-Chorus: Freddie Mercury]
Day day de mm hm
Da da da ba ba
Okay

[Verse 2: Freddie Mercury]
Chipping around, kick my brains 'round the floor
These are the days, it never rains, but it pours

[Interlude: Freddie Mercury & David Bowie]
Ee do ba be
Ee da ba ba ba
Um bo bo
Be lap
People on streets
Ee da de da de
People on streets
Ee da de da de da de da

[Chorus: David Bowie & Freddie Mercury]
It's the terror of knowing what this world is about
Watching some good friends screaming, "Let me out"
Pray tomorrow gets me higher, higher, high
Pressure on people, people on streets

[Bridge: David Bowie & Freddie Mercury]
Turned away from it all like a blind man
Sat on a fence, but it don't work
Keep coming up with love, but it's so slashed and torn
Why, why, why?
Love, love, love, love, love
Insanity laughs under pressure we're breaking
[Verse 3: Freddie Mercury]
Can't we give ourselves one more chance?
Why can't we give love that one more chance?
Why can't we give love, give love, give love, give love
Give love, give love, give love, give love, give love?

[Outro: David Bowie]
'Cause love's such an old-fashioned word
And love dares you to care for
The people on the (People on streets) edge of the night
And love (People on streets) dares you to change our way of
Caring about ourselves
This is our last dance
This is our last dance
This is ourselves
Under pressure
Under pressure
Pressure
*/
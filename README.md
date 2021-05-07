# Pathmatics.Interview
Attempt to find all duplicates in a list

This was definitely a challenge. 
If I were to continue working on this, I would definitely work to make the match criteria a little more sophisticated. 
As it stands, it's getting a lot of junk (anyhting with "LLC" and "INC" will match anything else, "ANGEL" matches "EvANGELical" etc.

I do think this is about the right path to be going down though. 
This program got through the list in a handful of minutes and caught a lot of good duplicates like Glock Ges.m.b.H and GLOCK, Inc.
Definitely needs something more sophisticated than "Contains" and "Where" 

If given more time, I'd refine the "Contains" statements into a more complete algorithm that weights each word in sequence and only lables it a duplicate if a certain (possibly configurable) threshold is met. 
Ideally, I would have made DuplicateFinder an implementation of IEnumerable on its own to avoid the clunky ThingsLike(List<string>, string) methods. 
I'd prefer to have something like DuplicateFinder.ThingsLike(string);

All in all, I think this is relatively readable and maintainable and would be happy to bring this to a pair programming session.

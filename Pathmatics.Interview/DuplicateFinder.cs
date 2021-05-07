using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Pathmatics.Interview
{
    class DuplicateFinder
    {
        public List<string> Items { get; set; }

        public Dictionary<string, List<string>> GetDuplicates()
        {
            List<string> workingList = new(Items);
            Dictionary<string, List<string>> duplicates = new();

            //You'll find that the naming doesn't get much better than "item" and "items"
            //I'm sure we can come up with something better together
            foreach (string item in Items)
            {
                //Get All exact alphabetical duplicates first
                //Please forgive the use of "x" creativity in naming was lacking against the clock
                if (workingList.Where(x => Sanitize(x) == Sanitize(item)).Count() > 1)
                    AddThingsLike(workingList, duplicates, item);
                //Then get anything like that item
                else if (workingList.Any(x => IsLike(x, item)))
                    AddThingsLike(workingList, duplicates, item);
            }
            
            return duplicates;
        }

        private void AddThingsLike(List<string> workingList, Dictionary<string, List<string>> duplicates, string item)
        {
            duplicates.Add(item, new List<string>());
            //"Thing" is admittedly generic, I'm sure there's an obviously better name
            foreach (string thing in ThingsLike(workingList, item))
            {
                //Below is for monitoring. Makes you feel like the program is working.
                //Console.WriteLine(string.Concat(item, ": ", thing));
                //adding independantly so we can...
                duplicates[item].Add(thing);
                //remove from the list and hopefully make things a bit faster and avoid duplication
                workingList.Remove(thing);
            }
        }

        private static List<string> ThingsLike(List<string> workingList, string item)
        {
            return workingList.Where(x=> IsLike(x, item)).ToList();
        }
        
        //Just take alphabetical characters
        private static readonly Regex AlphaFilters = new(@"[^A-Z]", RegexOptions.Compiled);
        private static string Sanitize(string x) => AlphaFilters.Replace((x ?? string.Empty).ToUpper(), string.Empty);

        private static bool IsLike(string x, string item)
        {
            //Get each distinct word
            string[] xArray = x.Split(" ");
            string scrubbedItem = Sanitize(item);
            //Avoid A, An, The, Of, in, etc.
            //In a perfect world I'd find a dictionary of common conjunctions, articles, etc. to avoid
            foreach (string xString in xArray.Where(x=>x.Length>3))
                //Definitely too broad, need to make criteria more specific, weighted scores etc.
                if (scrubbedItem.Contains(xString.ToUpper()))
                    return true;
                else
                {
                    //Get each distinct word
                    string[] itemArray = item.Split(" ");
                    string scrubbedX = Sanitize(x);
                    //Avoid A, An, The, Of, in, etc.
                    //In a perfect world I'd find a dictionary of common conjunctions, articles, etc. to avoid
                    foreach(string itemString in itemArray.Where(x=>x.Length>3))
                        //Definitely too broad, need to make criteria more specific, weighted scores etc.
                        if (scrubbedX.Contains(itemString.ToUpper()))
                            return true;
                }

            return false;
        }
    }
}

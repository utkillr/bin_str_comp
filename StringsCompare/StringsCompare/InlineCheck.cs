using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/*uncomment 1st to get cut version*/

namespace StringsCompare
{
    //uncomment for slow uncut version, but comment next
    
    class InlineCheck
    {
        int neededRepeatCount;
        bool toMessage;

        List<string> strings;   //Contains all the strings
        List<int> HashByID;     //Contains hashes of strings
        List<int> iterations;   //Contains lengthes of iterations
        List<int> enterings;    //Contains entering indexes of iterations
        List<List<string>> savedIter; //Contains saved strings
        Dictionary<int, List<int>> listOfIDs;   //Pair: hash - IDs

        public InlineCheck(int nrc, bool tm)
        {
            strings = new List<string>();
            listOfIDs = new Dictionary<int, List<int>>();
            HashByID = new List<int>();
            iterations = new List<int>();
            enterings = new List<int>();
            savedIter = new List<List<string>>();
            neededRepeatCount = nrc;
            toMessage = tm;
        }

        public void addLine(string byteStr)
        {
            //Adding new element
            int hash = byteStr.GetHashCode();
            HashByID.Add(hash);
            if (!listOfIDs.ContainsKey(hash)) listOfIDs.Add(hash, new List<int>());
            listOfIDs[hash].Add(HashByID.Count - 1);
            strings.Add(byteStr);

            //Checking all existing iterations
            for (int i = 0; i < iterations.Count; i++)
            {
                if (HashByID[enterings[i] + iterations[i]] == hash
                    && strings[enterings[i] + iterations[i]] == byteStr)     //If new element is next, 
                {
                    iterations[i]++;                                    //Then increase
                    savedIter[i].Add(byteStr);
                    if (iterations[i] == neededRepeatCount)             //If got needed length, remove
                    {
                        Console.WriteLine("Beep, repeated " + (neededRepeatCount) + " times from " + (enterings[i]+1) + " and " + (HashByID.Count - iterations[i] + 1));
                        if (toMessage)
                        {
                            for (int k = 0; k < neededRepeatCount; k++)
                            {
                                Console.WriteLine(savedIter[i][k]);
                            }
                        }
                        iterations.RemoveAt(i);
                        enterings.RemoveAt(i);
                        savedIter.RemoveAt(i);
                        i--;
                    }
                }
                else
                {                                                   //If new is not next
                    iterations.RemoveAt(i);                         //then remove
                    enterings.RemoveAt(i);
                    savedIter.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < listOfIDs[hash].Count - 1; i++)
            {
                int iter = listOfIDs[hash][i];
                if (strings[iter] == byteStr)
                {
                    iterations.Add(1);
                    enterings.Add(listOfIDs[hash][i]);
                    List<string> s = new List<string>();
                    s.Add(byteStr);
                    savedIter.Add(s);
                }
            }
        }
    }
    
    //Uncomment for faster cut version, but comment prev
    /*
    class InlineCheck
    {
        int neededRepeatCount;
        bool toMessage;

        List<string> strings;   //Contains all the strings
        List<int> HashByID;     //Contains hashes of strings
        List<int> iterations;   //Contains lengthes of iterations
        List<int> enterings;    //Contains entering indexes of iterations
        Dictionary<int, List<int>> listOfIDs;   //Pair: hash - IDs

        public InlineCheck(int nrc, bool tm)
        {
            strings = new List<string>();
            listOfIDs = new Dictionary<int, List<int>>();
            HashByID = new List<int>();
            iterations = new List<int>();
            enterings = new List<int>();
            neededRepeatCount = nrc;
            toMessage = tm;
        }

        public void addLine(string byteStr)
        {
            //Adding new element
            int hash = byteStr.GetHashCode();
            HashByID.Add(hash);
            if (!listOfIDs.ContainsKey(hash)) listOfIDs.Add(hash, new List<int>());
            listOfIDs[hash].Add(HashByID.Count - 1);
            strings.Add(byteStr);

            //Checking all existing iterations
            for (int i = 0; i < iterations.Count; i++)
            {
                if (HashByID[enterings[i] + iterations[i]] == hash &&
                    strings[enterings[i] + iterations[i]] == byteStr)     //If new element is next, 
                {
                    if (HashByID.Count > 368)
                    iterations[i]++;                                    //Then increase
                    if (iterations[i] == neededRepeatCount)             //If got needed length, remove
                    {
                        Console.WriteLine("Beep, repeated " + (neededRepeatCount) + " times from " + (enterings[i] + 1) + " and " + (HashByID.Count - iterations[i] + 1));
                        iterations.RemoveAt(i);
                        enterings.RemoveAt(i);
                        i--;
                    }
                }
                else
                {                                                   //If new is not next
                    iterations.RemoveAt(i);                         //then remove
                    enterings.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < listOfIDs[hash].Count - 1; i++)
            {
                int iter = listOfIDs[hash][i];
                if (strings[iter] == byteStr)
                {
                    iterations.Add(1);
                    enterings.Add(listOfIDs[hash][i]);
                }
            }
        }
    }
    */
}
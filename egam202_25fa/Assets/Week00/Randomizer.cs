using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    public bool isPlaytest = true;
    public List<Student> students;

    [System.Serializable]
    public class Student
    {
        public string name;
        public bool isPresent;
    }

    void Start()
    {
        Rerun();
    }

    public void Rerun()
    {
        // Copy the list
        List<string> names = new List<string>();
        foreach (Student s in students)
        {
            if (s.isPresent)
            {
                names.Add(s.name);
            }
        }

        // Randomize the list
        Shuffle(names);

        // Print to the inspector
        string output = "";
        for (int i = 0; i < names.Count; i++)
        {
            // Presenter
            string presenter = $"{i + 1}. {names[i]}\n";
            output += presenter;

            // Playtester?
            if (isPlaytest)
            {
                int index = (i + 1) % names.Count;
                string playtester = $"   -> Played by: {names[index]}\n";
                output += playtester;
            }

            // Newline if we're not the last name
            if (i + 1 < names.Count)
            {
                output += "\n";
            }            
        }
        Debug.Log(output);
    }

    public void Shuffle<T>(List<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace CodeAnalyzer
{
    class GenerateAutomatonText
    {
        public static void Start(string path, string outPath)
        {
            List<Tuple<int, char, int>> stateTransition = new List<Tuple<int, char, int>>();

            List<string> kws = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string s = reader.ReadLine().Trim();
                    if (s != "")
                    {
                        kws.Add(s);
                    }
                }
            }

            List<int> F = new List<int>();
            int q = 1;
            kws.ForEach((kw) =>
            {
                for (int i = 0; i < kw.Length; i++)
                {
                    int index = stateTransition.FindIndex(st => st.Item3 == q);
                    if (index >= 0)
                    {
                        stateTransition.Add(new Tuple<int, char, int>(q, kw[i], ++q));
                    }
                    else
                    {
                        stateTransition.Add(new Tuple<int, char, int>(1, kw[i], ++q));
                    }
                }
                F.Add(q);
                q++;
            });

            using (StreamWriter writer = new StreamWriter(outPath))
            {
                writer.WriteLine("#Q");
                string qs = "";
                for(int i = 1; i < q; i++)
                {
                    qs += "Q" + i + " ";
                }
                qs = qs.Trim();
                writer.WriteLine(qs);
                writer.WriteLine("#end\n");

                writer.WriteLine("#\\Delta");
                stateTransition.ForEach((st) =>
                {
                    string startState = "Q" + st.Item1;
                    string finalState = "Q" + st.Item3;
                    writer.WriteLine("{" + startState + "}{\\" + st.Item2 + "}{" + finalState + "}");
                });
                writer.WriteLine("#end\n");

                writer.WriteLine("#S");
                writer.WriteLine("Q1");
                writer.WriteLine("#end\n");

                writer.WriteLine("#F");
                string f = "";
                F.ForEach((item) =>
                {
                    f += "Q" + item + " ";
                });
                f = f.Trim();
                writer.WriteLine(f);
                writer.WriteLine("#end");
            }
        }
    }
}

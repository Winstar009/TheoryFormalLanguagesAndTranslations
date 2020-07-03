using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeAnalyzer
{
    class FiniteStateAutomaton
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        private List<string> Sigma;
        private List<string> State;
        private List<string> StartState;
        private List<string> FinalState;

        private struct StateTransition
        {
            public List<string> InStates, OutStates;
            public string allowSymbols, disallowSymbols;
        }
        private List<StateTransition> StateTransitions;

        public FiniteStateAutomaton(string path)
        {
            Sigma = new List<string>();
            State = new List<string>();
            StartState = new List<string>();
            FinalState = new List<string>();
            StateTransitions = new List<StateTransition>();
            ReadFiniteStateAutomaton(path);
        }

        private void ReadFiniteStateAutomaton(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string s = reader.ReadLine().Trim();

                        switch (s)
                        {
                            case "#Name":
                                Name = reader.ReadLine().Trim();
                                break;
                            case "#Priority":
                                Priority = Int32.Parse(reader.ReadLine().Trim());
                                break;
                            case "#Q":
                                s = reader.ReadLine().Trim();
                                while (s != "#end")
                                {
                                    if (s != "")
                                    {
                                        State.AddRange(s.Split(' ').ToList());
                                    }
                                    s = reader.ReadLine().Trim();
                                }
                                break;
                            case "#\\Sigma":
                                s = reader.ReadLine().Trim();
                                while (s != "#end")
                                {
                                    if (s != "")
                                    {
                                        Sigma.AddRange(s.Split(' ').ToList());
                                    }
                                    s = reader.ReadLine().Trim();
                                }
                                break;
                            case "#\\Delta":
                                s = reader.ReadLine().Trim();
                                while (s != "#end")
                                {
                                    if (s != "")
                                    {
                                        StateTransition stateTransition = new StateTransition
                                        {
                                            InStates = new List<string>(),
                                            OutStates = new List<string>(),
                                            allowSymbols = "",
                                            disallowSymbols = ""
                                        };

                                        int sPos = s.IndexOf('{', 0);
                                        int ePos = s.IndexOf('}', 1);
                                        string inStates = s.Substring(sPos + 1, ePos - sPos - 1);
                                        stateTransition.InStates.AddRange(inStates.Split(' '));

                                        bool negative = false;
                                        string con = "";
                                        for (int i = ePos + 1; i < s.Length; i++)
                                        {
                                            switch (s[i])
                                            {
                                                case '{':
                                                    con = "";
                                                    break;
                                                case '!':
                                                    negative = true;
                                                    break;
                                                case '\\':
                                                    i++;
                                                    if (negative)
                                                    {
                                                        stateTransition.disallowSymbols += s[i];
                                                        negative = false;
                                                    }
                                                    else
                                                    {
                                                        stateTransition.allowSymbols += s[i];
                                                    }
                                                    break;
                                                case '}':
                                                case '|':
                                                    switch (con)
                                                    {
                                                        case "[A-Z]":
                                                            if (negative)
                                                            {
                                                                stateTransition.disallowSymbols += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                                                            }
                                                            else
                                                            {
                                                                stateTransition.allowSymbols += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                                                            }
                                                            break;
                                                        case "[a-z]":
                                                            if (negative)
                                                            {
                                                                stateTransition.disallowSymbols += "abcdefghijklmnopqrstuvwxyz";
                                                            }
                                                            else
                                                            {
                                                                stateTransition.allowSymbols += "abcdefghijklmnopqrstuvwxyz";
                                                            }
                                                            break;
                                                        case "[0-9]":
                                                            if (negative)
                                                            {
                                                                stateTransition.disallowSymbols += "0123456789";
                                                            }
                                                            else
                                                            {
                                                                stateTransition.allowSymbols += "0123456789";
                                                            }
                                                            break;
                                                        default:
                                                            if (negative)
                                                            {
                                                                stateTransition.disallowSymbols += con;
                                                            }
                                                            else
                                                            {
                                                                stateTransition.allowSymbols += con;
                                                            }
                                                            break;
                                                    }
                                                    con = "";
                                                    break;
                                                default:
                                                    con += s[i];
                                                    break;
                                            }
                                            if (s[i] == '}')
                                            {
                                                ePos = i;
                                                break;
                                            }
                                        }

                                        sPos = s.IndexOf('{', ePos + 1);
                                        ePos = s.IndexOf('}', ePos + 1);
                                        string outStates = s.Substring(sPos + 1, ePos - sPos - 1);
                                        stateTransition.OutStates.AddRange(outStates.Split(' '));

                                        StateTransitions.Add(stateTransition);
                                    }
                                    s = reader.ReadLine().Trim();
                                }
                                break;
                            case "#S":
                                s = reader.ReadLine().Trim();
                                while (s != "#end")
                                {
                                    if (s != "")
                                    {
                                        StartState.AddRange(s.Split(' ').ToList());
                                    }
                                    s = reader.ReadLine().Trim();
                                }
                                break;
                            case "#F":
                                s = reader.ReadLine().Trim();
                                while (s != "#end")
                                {
                                    if (s != "")
                                    {
                                        FinalState.AddRange(s.Split(' ').ToList());
                                    }
                                    s = reader.ReadLine().Trim();
                                }
                                break;
                        }
                    }
                }
            }
        }

        private List<string> calc(string state, char c)
        {
            List<string> newState = new List<string>();

            StateTransitions.FindAll(st => st.InStates.Exists(s => s == state)).ForEach(item =>
            {
                if (item.allowSymbols.IndexOf(c) >= 0 && item.disallowSymbols.IndexOf(c) < 0)
                {
                    newState.AddRange(item.OutStates);
                }
            });

            return newState;
        }

        public Tuple<bool, int> maxString(string str, int k)
        {
            bool result = false;
            int m = 0;
            List<string> CurState = StartState;

            for (int i = k; i < str.Length; i++)
            {
                List<string> CurStateTmp = new List<string>();
                foreach (string state in CurState)
                {
                    CurStateTmp.AddRange(calc(state, str[i]));
                }
                CurState = CurStateTmp;
                if (CurState.Count == 0)
                {
                    break;
                }
                if (CurState.Intersect(FinalState).Any())
                {
                    result = true;
                    m = i - k + 1;
                }
            }

            return new Tuple<bool, int>(result, m);
        }
    }
}

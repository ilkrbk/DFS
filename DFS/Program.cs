using System;
using System.Collections.Generic;
using System.IO;

namespace laba3._4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Начальная вершина:");
            int start = Convert.ToInt32(Console.ReadLine());
            (int, int) sizeM = (0, 0);
            List<(int, int)> edgeList = ReadFile(ref sizeM);
            int[,] matrixA = AdjMatrix(sizeM, edgeList);
            List<(string, string, string)> depthList = SearchInDepth(sizeM, matrixA, start);
            ShowList(depthList, "Вершина", "DFS", "Стек");
        }
        static List<(int, int)> ReadFile(ref (int, int) sizeM)
        {
            string path = "test.txt";
            List<(int, int)> edgeList = new List<(int, int)>();
            StreamReader file = new StreamReader(path);
            string[] size = file.ReadLine()?.Split(' ');
            if (size != null)
            {
                sizeM = (Convert.ToInt32(size[0]), Convert.ToInt32(size[1]));
                for (int i = 0; i < sizeM.Item2; ++i)
                {
                    size = file.ReadLine()?.Split(' ');
                    if (size != null) edgeList.Add((Convert.ToInt32(size[0]), Convert.ToInt32(size[1])));
                }
            }

            return edgeList;
        }
        static int[,] AdjMatrix((int, int) sizeMatrix, List<(int, int)> edgeList)
        {
            int[,] matrixA = new int[sizeMatrix.Item1, sizeMatrix.Item1];
            for (int i = 0; i < sizeMatrix.Item1; i++)
            {
                for (int j = 0; j < sizeMatrix.Item1; j++)
                {
                    matrixA[i, j] = 0;
                }
            }

            for (int k = 0; k < sizeMatrix.Item2; k++)
            {
                matrixA[((edgeList[k].Item1) - 1), ((edgeList[k].Item2) - 1)] = 1;
                matrixA[((edgeList[k].Item2) - 1), ((edgeList[k].Item1) - 1)] = 1;
            }

            return matrixA;
        }
        static List<(string, string, string)> SearchInDepth((int, int) sizeM, int[,] matrix, int start)
        {
            List<(string, string, string)> DepthList = new List<(string, string, string)>();
            List<int> turn = new List<int>();
            List<int> visited = new List<int>();
            string turnStr = "";
            int count = 1;
            turn.Add(start);
            visited.Add(start);
            while (turn.Count != 0)
                for (int i = 0; i < sizeM.Item1; i++)
                    if (i + 1 == start)
                        for (int j = 0; j < sizeM.Item1; j++)
                        {
                            if (matrix[i, j] == 1 && SearchInList(visited, (j + 1)))
                            {
                                if (count == 1)
                                {
                                    turnStr = ConvertListToString(turn);
                                    DepthList.Add((Convert.ToString(count), Convert.ToString(start), turnStr));
                                }
                                start = j + 1;
                                turn.Add(start);
                                visited.Add(start);
                                turnStr = ConvertListToString(turn);
                                ++count;
                                DepthList.Add((Convert.ToString(count), Convert.ToString(start), turnStr));
                                break;
                            }
                            if (j == sizeM.Item1 - 1)
                            {
                                turn.Remove(turn[turn.Count - 1]);
                                turnStr = ConvertListToString(turn);
                                if (turn.Count == 0)
                                {
                                    DepthList.Add(("-", "-", "-0-"));
                                    break;
                                }
                                DepthList.Add(("-", "-", turnStr));
                                start = turn[turn.Count - 1];
                                break;
                            }
                        }
            return DepthList;
        }
        static bool SearchInList(List<int> list, int n)
        {
            foreach (var item in list)
            {
                if (item == n)
                {
                    return false;
                }
            }
            return true;
        }
        static string ConvertListToString(List<int> list)
        {
            string str = "";
            foreach (var item in list)
                str += Convert.ToString(item);
            return str;
        }
        static void ShowList(List<(string, string, string)> list, string a, string b, string c)
        {
            int count = 0;
            foreach (var item in list)
            {
                if (count == 0)
                {
                    Console.WriteLine($"{a}\t| {b}\t| {c}");
                }
                count++;
                Console.WriteLine($"{item.Item2}\t| {item.Item1}\t| {item.Item3}");
            }
        }
    }
}
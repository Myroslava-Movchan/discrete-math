using System;

namespace Warshall
{
    internal class WarshallAlgorithm
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter the matrix size (n x n): ");
            var n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the density of the graph (between 0 and 1): ");
            var density = Convert.ToDouble(Console.ReadLine());

            var graph = GenerateRandomGraph(n, density);

            Console.WriteLine("Generated adjacency matrix:");
            PrintMatrix(graph);

            var reach = ComputeReachability(graph);

            Console.WriteLine("Transitive closure matrix:");
            PrintMatrix(reach);
        }

        private static int[,] GenerateRandomGraph(int n, double density)
        {
            var rand = new Random();
            var graph = new int[n, n];

            var maxEdges = n * (n - 1) / 2;
            var actualEdges = (int)Math.Round(maxEdges * density);

            for (var i = 0; i < n; i++)
            {
                for (var j = i; j < n; j++) // Зміна j від i, щоб забезпечити симетричність
                {
                    if (i == j)
                    {
                        graph[i, j] = 0; // Немає петель
                    }
                    else
                    {
                        if (rand.NextDouble() < density)
                        {
                            graph[i, j] = 1;
                            graph[j, i] = 1; // Забезпечення симетричності
                        }
                    }
                }
            }

            return graph;
        }

        private static int[,] ComputeReachability(int[,] graph)
        {
            var verticesCount = graph.GetLength(0);
            var reach = new int[verticesCount, verticesCount];

            // Ініціалізація матриці досяжності
            for (var i = 0; i < verticesCount; i++)
            {
                for (var j = 0; j < verticesCount; j++)
                {
                    reach[i, j] = graph[i, j];
                }
            }

            // Побудова матриці досяжності
            for (var k = 0; k < verticesCount; k++)
            {
                for (var i = 0; i < verticesCount; i++)
                {
                    for (var j = 0; j < verticesCount; j++)
                    {
                        reach[i, j] = (reach[i, j] != 0) || ((reach[i, k] != 0) && (reach[k, j] != 0)) ? 1 : 0;
                    }
                }
            }

            return reach;
        }

        private static void PrintMatrix(int[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
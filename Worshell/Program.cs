namespace Worshell;

internal abstract class WarshallAlgorithm
{
    private static void Main(string[] args)
    {
        int[,] graph = {
            {0, 1, 0, 0, 0},
            {0, 0, 0, 1, 0},
            {0, 0, 0, 0, 1},
            {0, 0, 0, 0, 0},
            {1, 0, 1, 0, 0}
        };

        var reach = ComputeReachability(graph);

        Console.WriteLine("Матриця досяжності:");
        PrintMatrix(reach);
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
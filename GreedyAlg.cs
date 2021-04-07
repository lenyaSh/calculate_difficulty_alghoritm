using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_задание {
    class GreedyAlg {
		private readonly int _countOfVertices;
		private List<int>[] _adjacencies;
		public GreedyAlg(int countOfVertices, bool[,]matrix)
		{
			_countOfVertices = countOfVertices;
			_adjacencies = new List<int>[_countOfVertices];
			for (var i = 0; i < _countOfVertices; i++)
			{
				_adjacencies[i] = new List<int>();
			}
			ConvertToPar(matrix);
		}

		private void ConvertToPar(bool[,] matrix)
        {
			for (int i = 0; i < _countOfVertices; i++) 
            {
				for (int j = 1; j < _countOfVertices; j++) 
                {
					if (matrix[i, j]) AddEdge(i, j);
                }
            }
        }

		private void AddEdge(int v, int w)
		{
			_adjacencies[w].Add(v);
		}

		public void GreedyColoring()
		{
			var result = new int[_countOfVertices];
			result[0] = 0;

			for (var i = 1; i < _countOfVertices; i++)
				result[i] = -1;

			var available = new bool[_countOfVertices];
			for (var i = 0; i < _countOfVertices; i++)
				available[i] = false;

			for (var i = 1; i < _countOfVertices; i++)
			{
				foreach (var adjacency in _adjacencies[i].Where(adjacency => result[adjacency] != -1))
				{
					available[result[adjacency]] = true;
				}

				int cr;
				for (cr = 0; cr < _countOfVertices; cr++)
					if (available[cr] == false)
						break;

				result[i] = cr; 

				foreach (var adjacency in _adjacencies[i].Where(adjacency => result[adjacency] != -1))
				{
					available[result[adjacency]] = false;
				}
			}

			Console.WriteLine("Нужно ли выводить список вершина - цвет? y/n");
			string answer = Console.ReadLine();
			if (answer == "y")
				for (var i = 0; i < _countOfVertices; i++)
					Console.WriteLine($"Vertex  {i+1}  --->  Color {result[i]+1}");
            
			Console.WriteLine($"\nЖадный алгоритм: {result.Max()+1}\n");
		}
	}
}

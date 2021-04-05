using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_задание {
    class TotallyOverkill {
        public bool[,] Matrix { get; set; }
        
        public Dictionary<int, int> CalculateCountColors() {
            if (Matrix == null)
                return null;

            bool[] numbers = new bool[Matrix.GetLength(0)];
            
            Dictionary<int, int> colorsAndVertex = new Dictionary<int, int> {
                { 0, 1 }
            };

            for (int i = 1; i < Matrix.GetLength(0); i++) {
                for(int j = 0; j < Matrix.GetLength(1); j++) {

                    if (Matrix[i,j] && i != j && colorsAndVertex.ContainsKey(j)) {
                        numbers[colorsAndVertex[j] - 1] = true; 
                    } 

                }
                for(int j = 0; j < Matrix.GetLength(0); j++) {
                    if (!numbers[j]) {
                        colorsAndVertex.Add(i, j + 1);
                        break;
                    }
                }
                numbers = new bool[numbers.Length];
            }


            print(colorsAndVertex);

            return colorsAndVertex;
        }


        void print(Dictionary<int,int> c) {
            int max = 0;
            foreach (KeyValuePair<int,int> elem in c){
                if (elem.Value > max) max = elem.Value;
                Console.WriteLine($"{elem.Key + 1} - {elem.Value}");
            }
            Console.WriteLine(max);
        }
    }
}

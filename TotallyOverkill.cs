using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_задание {
    class TotallyOverkill {

        private int _dim;
        private bool[,] Matrix;

        /// <summary>
        /// Главный метод, инициализирующий все необходимые поля и начинающий работу по расскраске
        /// </summary>
        /// <param name="adjacencyMatrix">Исходная матрица смежности</param>
        public void GraphColoring(bool[,] adjacencyMatrix) {
            _dim = adjacencyMatrix.GetLength(0);
            Matrix = adjacencyMatrix;

            var localMax = int.MaxValue;
            var coloredNodesResult = new Dictionary<int, int>(0);
            for (int i = 0; i < _dim; i++) {
                var result = RecursiveColorGraph(new Dictionary<int, int>(), i);
                var max = result.Values.Max();
                if (localMax > max) {
                    localMax = max;
                    coloredNodesResult = result;
                }
            }

            var commonFormat = GetResult(_dim, ref coloredNodesResult);
            var colorsAmount = commonFormat.Max();

            Console.WriteLine("Нужно ли выводить список вершина - цвет? (y/n)");
            if(Console.ReadLine() == "y") {
                for(int i = 0; i < commonFormat.Length; i++) {
                    Console.Write($"Вершина {i + 1} ---> Цвет {commonFormat[i] + 1}\n");
                }
            }

            Console.WriteLine($"Количество цветов по алгоритму полного перебора = {colorsAmount + 1}");
        }

        /// <summary>
        /// Рекурсивный обход всех вершин
        /// </summary>
        /// <param name="currentColoredNodes">номер вершины, номер цвета</param>
        /// <param name="currentNode">текущая вершина</param>
        private Dictionary<int, int> RecursiveColorGraph(Dictionary<int, int> currentColoredNodes, int currentNode) {
            PaintCurrentNode(currentColoredNodes, currentNode);

            var minNodeColorsList = new Dictionary<int, int>(0);
            for (int i = 0; i < _dim; i++) {
                //если вершина уже раскрашена, пропускаем ее
                if (!currentColoredNodes.ContainsKey(i)) {
                    var tempNewDict = new Dictionary<int, int>(currentColoredNodes);
                    var nodeColorsList = RecursiveColorGraph(tempNewDict, i);
                    //если хроматическое число текущего минимума больше , чем у списка из рекурсивного возврата,то обновим набор вершин с минимальным хромо числом
                    var isNodeColorsListHasLessChromaticNumber =
                        CheckIsChromaticNumberIsLessOnComparingList(ref minNodeColorsList, ref nodeColorsList);
                    if (isNodeColorsListHasLessChromaticNumber) {
                        minNodeColorsList = nodeColorsList;
                    }
                }
            }

            if (currentColoredNodes.Count == _dim) {
                return currentColoredNodes;
            }

            return minNodeColorsList;
        }

        /// <summary>
        /// Раскрашивает вершину в свободный цвет
        /// </summary>
        private void PaintCurrentNode(Dictionary<int, int> currentColoredNodes, int currentNode) {
            //цикл по всем цветам
            for (int color = 0; color < _dim; color++) {
                var isColorFound = true;
                //цикл по всем раскрашенным вершинам
                foreach (var nodeNumber in currentColoredNodes.Keys)
                    //если вершина смежна с нашей
                    if (Matrix[currentNode, nodeNumber] == true)
                        //если она уже использует наш цвет, то его нельзя использовать
                        if (currentColoredNodes[nodeNumber] == color) {
                            isColorFound = false;
                            break;
                        }

                if (!isColorFound) continue;

                currentColoredNodes.Add(currentNode, color);
                break;
            }
        }

        /// <summary>
        /// Проверяет меньше ли хроматическое число у сравниваемого элемента относительно минимального
        /// </summary>
        /// <param name="minNodeColors"></param>
        /// <param name="comparingNodeColors"></param>
        private bool CheckIsChromaticNumberIsLessOnComparingList(ref Dictionary<int, int> minNodeColors, ref Dictionary<int, int> comparingNodeColors) {
            var minNodeColorsAmount = GetChromaticNumber(minNodeColors);
            var comparingNodeColorsAmount = GetChromaticNumber(comparingNodeColors);

            return comparingNodeColorsAmount < minNodeColorsAmount;
        }

        /// <summary>
        /// Получает хроматическое число из списка
        /// </summary>
        public int GetChromaticNumber(Dictionary<int, int> numberColors) {
            if (numberColors.Count == 0) return int.MaxValue;

            return numberColors.Values.Max();
        }


        /// <summary>
        /// Конвертирует структуру в массив, где индекс - номер узла, а значение - цвет 
        /// </summary>
        /// <param name="dim">размерность матрицы</param>
        /// <param name="sortedNodesByRank">список вершин отсортированных по степени</param>
        private int[] GetResult(int dim, ref Dictionary<int, int> sortedNodesByRank) {
            var nodeIndexColor = new int[dim];
            for (int i = 0; i < dim; i++) {
                nodeIndexColor[i] = sortedNodesByRank[i];
            }

            return nodeIndexColor;
        }
    }
}
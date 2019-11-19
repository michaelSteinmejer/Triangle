using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangleOddEvenSum
{
    class TriangleSum
    {
        // Path der indeholder højeste sum
        public Path maxPath { get; set; }

        // trekant af integers
        public List<List<int>> triangle { get; set; }

        // holder styr på største path for hver node
        public List<List<int>> cache { get; set; }

        public TriangleSum(string[] file)
        {
            triangle = new List<List<int>>();
            cache = new List<List<int>>();

            if (!read(file)) return;
            maxPath = new Path();
            calculateMaxPath(new Path(triangle[0][0]));
        }

        private void addCacheEntry(int level, int index, int sum)
        {
            cache[level][index] = sum;
        }

        private void calculateMaxPath(Path curPath)
        {
            if (curPath.level >= triangle.Count) return; // finished
            if (!isCached(curPath))
            {
                //tilføjer nuværende node til cache
                addCacheEntry(curPath.level, curPath.getParentIndex(), curPath.sum);
                
                //Lav 2 childs og tilføj deres nodes
                Path leftChild = createChild(curPath, curPath.getParentIndex());
                Path rightChild = createChild(curPath, curPath.getParentIndex() + 1);

                if (curPath.sum > maxPath.sum) maxPath = curPath;

                // et forsøg på at lave odd og even funktionalitet,
                // men ville fjerne en child node,
                // hvilket gør at jeg ikke ville kunne bruge den i en anden kombination.
                /*bool isEven = curPath.getParentIndex() % 2 == 0;
                if (isEven && leftChild.getParentIndex() % 2 == 0)
                {
                    calculateMaxPath(leftChild);
                }
                if (isEven && leftChild.getParentIndex() % 2 != 0)
                {
                    calculateMaxPath(rightChild);
                }*/

                calculateMaxPath(leftChild); 
                calculateMaxPath(rightChild); 
                
            }
        }

        public Path createChild(Path curPath, int index)
        {
            // kloner parent path
            ICloneable clone = (ICloneable)curPath;
            Path child = (Path)clone.Clone();

            child.level++; // rykker et niveau ned i trekanten
            // tilføjer child node til path
            if (child.level < triangle.Count)
            {
                child.path.Add(index);
                // updaterer sum
                child.sum += triangle[child.level][index];
            }

            return child;
        }

        public bool isCached(Path path)
        {
            List<int> curPath = path.path;

            foreach (var var in curPath)
                {


                    int runningSum = 0;
                    for (int i = 0; i < curPath.Count; i++)
                    {
                        //nuværende største sum af alle paths der besøger denne node
                        int cachedSum = cache[i][curPath[i]];
                        runningSum += triangle[i][curPath[i]];

                        if (cachedSum > runningSum) return true;
                    }

                    
                }
              

            return false;
        }
        // udskriver til program.cs
        public String printMaxPath()
        {
            StringBuilder retStr = new StringBuilder();
            List<int> pathToPrint = maxPath.path;
            int i;

            for (i = 0; i < (pathToPrint.Count - 1); i++)
            {
                retStr.Append(triangle[i][pathToPrint[i]] + " + ");
            }
            retStr.Append(triangle[i][pathToPrint[i]] + " = " + maxPath.sum);

            return retStr.ToString();
        }

        // indlæser text array og behandler det
        private bool read(string[] file)
        {
            try
            {
                string[] lines = file;
                int expectedElements = 1;
                for (int i = 0; i < lines.Length; i++)
                {
                    int elementCount = 0;
                    string line = lines[i];
                    Array numbers = line.Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string numStr in numbers)
                    { 
                        int number;
                        if (int.TryParse(numStr, out number))
                        {
                            

                            if (i < triangle.Count)
                            {
                                
                                triangle[i].Add(number);
                                cache[i].Add(int.MinValue);
                            }
                            else
                            { 
                                while (i >= triangle.Count)
                                {
                                    triangle.Add(new List<int>());
                                    cache.Add(new List<int>());
                                }
                                triangle[i].Add(number);
                                cache[i].Add(int.MinValue);
                            }
                            elementCount++;
                        }
                    }
                    expectedElements++;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
    }
    }

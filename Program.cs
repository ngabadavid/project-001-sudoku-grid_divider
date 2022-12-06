using System;
using System.Collections.Generic;
using System.Linq;

namespace GridDivider
{
    class Program
    {
        static void Main(string[] args)
        {
            int gridLength =9;
            Print(DivideGrid(gridLength));
        }
 
        public static int[,] DivideGrid(int gridLength)
        {
            int[,] grid = new int[gridLength, gridLength];

            // Divide the grid in equal sized square regions.
            Region[] regions = new Region[gridLength];
            for (int i =0; i< (int)Math.Sqrt(gridLength); i++)
            {
                for (int j = 0; j < (int)Math.Sqrt(gridLength); j++)
                {
                    regions[i * (int)Math.Sqrt(gridLength) + j] = new Region();
                    for (int k = 0; k< (int)Math.Sqrt(gridLength); k++)
                    {
                        for (int l = 0; l < (int)Math.Sqrt(gridLength); l++)
                        {
                            grid[i * (int)Math.Sqrt(gridLength) + k, j * (int)Math.Sqrt(gridLength) + l] = i * (int)Math.Sqrt(gridLength) + j + 1;
                            regions[i * (int)Math.Sqrt(gridLength) + j].InnerCellsCoordinates.Add(new Coordinate(i * (int)Math.Sqrt(gridLength) + k, j * (int)Math.Sqrt(gridLength) + l));
                            regions[i * (int)Math.Sqrt(gridLength) + j].NumberOfCell++;
                        }
                    }
                   
                    if (i * (int)Math.Sqrt(gridLength) - 1 >= 0)
                    {
                        for(int k=0; k < (int)Math.Sqrt(gridLength); k++)
                        {
                            regions[i* (int)Math.Sqrt(gridLength) + j].SurroundingCellsCoordinates.Add(new Coordinate(i * (int)Math.Sqrt(gridLength) - 1, (int)Math.Sqrt(gridLength) * j + k));
                        }
                        
                    }
                    if (i * (int)Math.Sqrt(gridLength) + (int)Math.Sqrt(gridLength) < gridLength)
                    {
                        for (int k = 0; k < (int)Math.Sqrt(gridLength); k++)
                        {
                            regions[i * (int)Math.Sqrt(gridLength) + j].SurroundingCellsCoordinates.Add(new Coordinate(i * (int)Math.Sqrt(gridLength) + (int)Math.Sqrt(gridLength), (int)Math.Sqrt(gridLength) * j + k));
                        }
                    }
                    if (j * (int)Math.Sqrt(gridLength) - 1 >= 0)
                    {
                        for (int k = 0; k < (int)Math.Sqrt(gridLength); k++)
                        {
                            regions[i * (int)Math.Sqrt(gridLength) + j].SurroundingCellsCoordinates.Add(new Coordinate((int)Math.Sqrt(gridLength) * i + k,j * (int)Math.Sqrt(gridLength) - 1));
                        }
                    }
                    if (j * (int)Math.Sqrt(gridLength) + (int)Math.Sqrt(gridLength) < gridLength)
                    {
                        for (int k = 0; k < (int)Math.Sqrt(gridLength); k++)
                        {
                            regions[i * (int)Math.Sqrt(gridLength) + j].SurroundingCellsCoordinates.Add(new Coordinate((int)Math.Sqrt(gridLength) * i + k, j * (int)Math.Sqrt(gridLength) + (int)Math.Sqrt(gridLength)));
                        }
                    }
                }
            }

            // Randomize the edge and form of each initial equal sized square region
            /*
             - Randomly choose a cell in the list of surrounding cell;
             - check if that cell does not devide another region into discountinuous region
             - remove the cell from the list of surrounding cell
             - add the cell in the list of inner cell
             - get the 4 cell perpendiculary surroundig that cell that are not inside the inner list yet
             - add those cell to the list of surrounding cells.
             - do all does step reciprocably to the region that have been amputee of its cell.
            */
            Randomize(regions,grid);
            return grid;
        }

        public static void Randomize(Region[] regions, int[,] grid)
        {
            int gridLength = (int)Math.Sqrt(grid.Length);
            for (int swap = 0; swap < gridLength*gridLength * 2; swap++)
            {
                Random random = new Random(DateTime.UtcNow.Millisecond);

                // Randomly choose a region
                int regionAIndexInRegions = random.Next(0, gridLength);
                int regionBIndexInRegions = regionAIndexInRegions;
                Region regionA = regions[regionAIndexInRegions];
                Region regionB = regionA;

                // Randomly select a cell to invade
                bool invasionA = false;
                List<int> invalidIndexOfCellInvadeByRegionAInSurroundingCellsCoordinates = new List<int>();
                while (invasionA == false && invalidIndexOfCellInvadeByRegionAInSurroundingCellsCoordinates.Count < regionA.SurroundingCellsCoordinates.Count)
                {
                    int indexOfCellInvadeByRegionAInSurroundingCellsCoordinates = random.Next(0, regionA.SurroundingCellsCoordinates.Count);
                    if (!invalidIndexOfCellInvadeByRegionAInSurroundingCellsCoordinates.Contains(indexOfCellInvadeByRegionAInSurroundingCellsCoordinates))
                    {
                        Coordinate cellInvadeByRegionA = regionA.SurroundingCellsCoordinates[indexOfCellInvadeByRegionAInSurroundingCellsCoordinates];
                        if (CanBeInvaded(cellInvadeByRegionA, regions, grid))
                        {
                            regionA.SurroundingCellsCoordinates.RemoveAt(indexOfCellInvadeByRegionAInSurroundingCellsCoordinates);
                            regionA.InnerCellsCoordinates.Add(cellInvadeByRegionA);
                            regionBIndexInRegions = grid[cellInvadeByRegionA.X, cellInvadeByRegionA.Y] - 1;
                            regionB = regions[regionBIndexInRegions];
                            grid[cellInvadeByRegionA.X, cellInvadeByRegionA.Y] = regionAIndexInRegions + 1;

                            foreach (Coordinate coordinate in getCellSurrounding(cellInvadeByRegionA, grid))
                            {
                                if (!regionA.SurroundingCellsCoordinates.Contains(coordinate) && !regionA.InnerCellsCoordinates.Contains(coordinate))
                                {
                                    regionA.SurroundingCellsCoordinates.Add(coordinate);
                                }
                            }

                            regionB.InnerCellsCoordinates.RemoveAll(cell => cell.Equals(cellInvadeByRegionA));
                            regionB.SurroundingCellsCoordinates.Add(cellInvadeByRegionA);
                            foreach (Coordinate coordinate in getCellSurrounding(cellInvadeByRegionA, grid))
                            {
                                if (!IsConnectedToRegion(coordinate, regionB, grid))
                                {
                                    regionB.SurroundingCellsCoordinates.RemoveAll(cell => cell.Equals(coordinate));
                                }
                            }
                            invasionA = true;
                        }
                        else
                        {
                            invalidIndexOfCellInvadeByRegionAInSurroundingCellsCoordinates.Add(indexOfCellInvadeByRegionAInSurroundingCellsCoordinates);
                        }
                    }
                }

                if (invasionA)
                {
                    bool invasionB = false;
                    List<int> candidateToInvasionByRegionB = new List<int>();
                    for (int i = 0; i < regionB.SurroundingCellsCoordinates.Count; i++)
                    {
                        if (regionA.InnerCellsCoordinates.Contains(regionB.SurroundingCellsCoordinates[i]))
                        {
                            candidateToInvasionByRegionB.Add(i);
                        }
                    }
                    List<int> invalidIndexOfCellInvadeByRegionBInCandidateToInvasionByRegionB = new List<int>();

                    while (invasionB == false && invalidIndexOfCellInvadeByRegionBInCandidateToInvasionByRegionB.Count < candidateToInvasionByRegionB.Count)
                    {
                        int indexOfCellInvadeByRegionBInnCandidateToInvasionByRegionB = random.Next(0, candidateToInvasionByRegionB.Count);
                        if (!invalidIndexOfCellInvadeByRegionBInCandidateToInvasionByRegionB.Contains(indexOfCellInvadeByRegionBInnCandidateToInvasionByRegionB))
                        {
                            int a = candidateToInvasionByRegionB[indexOfCellInvadeByRegionBInnCandidateToInvasionByRegionB];
                            Coordinate cellInvadeByRegionB = regionB.SurroundingCellsCoordinates[a];
                            if (CanBeInvaded(cellInvadeByRegionB, regions, grid))
                            {
                                regionB.SurroundingCellsCoordinates.RemoveAll(cell => cell.Equals(cellInvadeByRegionB));
                                regionB.InnerCellsCoordinates.Add(cellInvadeByRegionB);
                                grid[cellInvadeByRegionB.X, cellInvadeByRegionB.Y] = regionBIndexInRegions + 1;
                                foreach (Coordinate coordinate in getCellSurrounding(cellInvadeByRegionB, grid))
                                {
                                    if (!regionB.SurroundingCellsCoordinates.Contains(coordinate) && !regionB.InnerCellsCoordinates.Contains(coordinate))
                                    {
                                        regionB.SurroundingCellsCoordinates.Add(coordinate);
                                    }
                                }
                                regionA.InnerCellsCoordinates.RemoveAll(cell => cell.Equals(cellInvadeByRegionB));
                                regionA.SurroundingCellsCoordinates.Add(cellInvadeByRegionB);
                                foreach (Coordinate coordinate in getCellSurrounding(cellInvadeByRegionB, grid))
                                {
                                    if (!IsConnectedToRegion(coordinate, regionA, grid))
                                    {
                                        regionA.SurroundingCellsCoordinates.RemoveAll(cell => cell.Equals(coordinate));
                                    }
                                }
                                invasionB = true;
                            }
                            else
                            {
                                invalidIndexOfCellInvadeByRegionBInCandidateToInvasionByRegionB.Add(indexOfCellInvadeByRegionBInnCandidateToInvasionByRegionB);
                            }
                        }
                    }
                }
            }
        }

        public static List<Coordinate> getCellSurrounding(Coordinate coordinate, int[,] grid)
        {
            List<Coordinate> cList = new List<Coordinate>();
            int gridLength = (int)Math.Sqrt(grid.Length);
            int x = coordinate.X;
            int y = coordinate.Y;
            // Checked Left
            if (x - 1 >= 0)
            {
               cList.Add(new Coordinate(x - 1, y));
              
            }
            // Check Right
            if (x + 1 < gridLength)
            {
                cList.Add(new Coordinate(x + 1, y));
            }
            // Checked Up
            if (y - 1 >= 0)
            {
                cList.Add(new Coordinate(x, y -1));
            }
            // Check Bottom
            if (y + 1 < gridLength)
            {
                cList.Add(new Coordinate(x, y +1));
            }
            return cList;
        }

        public static bool IsConnectedToRegion(Coordinate coordinate, Region region, int [,] grid)
        {
            if (region.InnerCellsCoordinates.Contains(coordinate))
            {
                return true;
            }
            else
            {
                foreach (Coordinate co in getCellSurrounding(coordinate, grid))
                {
                    if (region.InnerCellsCoordinates.Contains(co))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool CanBeInvaded(Coordinate coordinate, Region[] regions, int[,] grid)
        {
            int regionBIndexInRegions = grid[coordinate.X, coordinate.Y] - 1;
            Region region = regions[regionBIndexInRegions];
            /*
             - check if the cell you want to invade has is surrounding cells connected to each other in perpandicular way. (evaluate also diagonale surrounding cells)
             */
            List<Coordinate> cList = new List<Coordinate>();
            int gridLength = (int)Math.Sqrt(grid.Length);
            int x = coordinate.X;
            int y = coordinate.Y;
            // Checked Left
            if (x - 1 >= 0)
            {
                Coordinate c = new Coordinate(x - 1, y);
                if (region.InnerCellsCoordinates.Contains(c))
                {
                    cList.Add(c);
                }
            }
            // Checked Left-Up
            if (x - 1 >= 0 && y - 1 >= 0)
            {
                Coordinate c = new Coordinate(x - 1, y - 1);
                if (region.InnerCellsCoordinates.Contains(c))
                {
                    cList.Add(c);
                }
            }
            // Checked Up
            if (y - 1 >= 0)
            {
                Coordinate c = new Coordinate(x, y - 1);
                if (region.InnerCellsCoordinates.Contains(c))
                {
                    cList.Add(c);
                }
            }
            // Check Up-Right
            if (x + 1 < gridLength && y - 1 >= 0)
            {
                Coordinate c = new Coordinate(x + 1, y - 1);
                if (region.InnerCellsCoordinates.Contains(c))
                {
                    cList.Add(c);
                }
            }
            // Check Right
            if (x + 1 < gridLength)
            {
                Coordinate c = new Coordinate(x + 1, y);
                if (region.InnerCellsCoordinates.Contains(c))
                {
                    cList.Add(c);
                }
            }
            // Check Right-Bottom
            if (x + 1 < gridLength && y + 1 < gridLength)
            {
                Coordinate c = new Coordinate(x + 1, y + 1);
                if (region.InnerCellsCoordinates.Contains(c))
                {
                    cList.Add(c);
                }
            }
            // Check Bottom
            if (y + 1 < gridLength)
            {
                Coordinate c = new Coordinate(x, y + 1);
                if (region.InnerCellsCoordinates.Contains(c))
                {
                    cList.Add(c);
                }
            }
            // Check Bottom-Left
            if (x - 1 >= 0 && y + 1 < gridLength)
            {
                Coordinate c = new Coordinate(x - 1, y + 1);
                if (region.InnerCellsCoordinates.Contains(c))
                {
                    cList.Add(c);
                }
            }

            // this case should not happen since a cell should always be connected to the body of its region
            // therefore, it should always be at least one surrounding cell of the same region than the cell we are trying to invade
            if(cList.Count == 0)
            {
                return false;
            }

            return BuildListOfContinuousCellsStartingByCellAtPositionZero(cList[0], cList, grid, new List<Coordinate>()).Count == cList.Count;
        }

        public static List<Coordinate> BuildListOfContinuousCellsStartingByCellAtPositionZero(Coordinate coordinate, List<Coordinate> coordinates, int[,] grid, List<Coordinate> coordinates2)
        {
            coordinates2.Add(coordinate);
            foreach (Coordinate c in getCellSurrounding(coordinate, grid))
            {
                if (coordinates.Contains(c) && !coordinates2.Contains(c))
                {
                    coordinates2 = coordinates2.Union(BuildListOfContinuousCellsStartingByCellAtPositionZero(c,coordinates, grid,coordinates2)).ToList();
                }
            }
            return coordinates2;
        }

        public static void Print(int[,] grid)
        {
            int gridLength = (int)Math.Sqrt(grid.Length);
            Console.WriteLine("Grid: =====================================");
            for (int row = 0; row < gridLength; row++)
            {
                for (int column = 0; column < gridLength; column++)
                {
                    Console.Write("|");
                    Console.Write(grid[row, column]);
                    Console.Write("|");
                }
                Console.WriteLine("");
            }
        }
    }
}
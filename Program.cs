﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GridDivider
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] grid = { { 1, 1, 1, 2, 2, 2, 3, 3, 3 },
                            { 1, 1, 1, 2, 2, 2, 3, 3, 3 },
                            { 1, 1, 1, 2, 2, 2, 3, 3, 3 },
                            { 4, 4, 4, 5, 5, 5, 6, 6, 6 },
                            { 4, 4, 4, 5, 5, 5, 6, 6, 6 },
                            { 4, 4, 4, 5, 5, 5, 6, 6, 6 },
                            { 7, 7, 7, 8, 8, 8, 9, 9, 9 },
                            { 7, 7, 7, 8, 8, 8, 9, 9, 9 },
                            { 7, 7, 7, 8, 8, 8, 9, 9, 9 } };



            /*int[,] grid = { { 1, 1, 2, 2 },
                            { 1, 1, 2, 2 },
                            { 3, 3, 4, 4 },
                            { 3, 3, 4, 4 } };*/
            Print(grid);
            DivideGrid(grid);
            Print(grid);



            /*int[,] grid = { { 1, 2, 3, 4 },
                            { 1, 2, 3, 4 },
                            { 1, 2, 3, 4 },
                            { 1, 2, 3, 4 } };
            Region[] regions = new Region[4];
            for(int i = 0; i < 4; i++)
            {
                regions[i] = new Region();
                regions[i].NumberOfCell = 4;

                regions[i].InnerCellsCoordinates.Add(new Coordinate(0, i));
                regions[i].InnerCellsCoordinates.Add(new Coordinate(1, i));
                regions[i].InnerCellsCoordinates.Add(new Coordinate(2, i));
                regions[i].InnerCellsCoordinates.Add(new Coordinate(3, i));

                if(i-1 >= 0)
                {
                    regions[i].SurroundingCellsCoordinates.Add(new Coordinate(0, i-1));
                    regions[i].SurroundingCellsCoordinates.Add(new Coordinate(1, i-1));
                    regions[i].SurroundingCellsCoordinates.Add(new Coordinate(2, i-1));
                    regions[i].SurroundingCellsCoordinates.Add(new Coordinate(3, i-1));
                }

                if (i + 1 < 4)
                {
                    regions[i].SurroundingCellsCoordinates.Add(new Coordinate(0, i + 1));
                    regions[i].SurroundingCellsCoordinates.Add(new Coordinate(1, i + 1));
                    regions[i].SurroundingCellsCoordinates.Add(new Coordinate(2, i + 1));
                    regions[i].SurroundingCellsCoordinates.Add(new Coordinate(3, i + 1));
                }
            }

            if(regions[0].InnerCellsCoordinates.Contains(new Coordinate(0, 0)))
            {
                Console.WriteLine("contains works");
            }*/

            /*for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("==================== : Surround : " + 0);
                foreach (Coordinate coordinate in regions[i].SurroundingCellsCoordinates)
                {
                    coordinate.Print();
                }*/

                /*Console.WriteLine("==================== : Inner : " + 0);
                foreach (Coordinate coordinate in regions[0].InnerCellsCoordinates)
                {
                    coordinate.Print();
                }
            }*/
            /*regions[0].InnerCellsCoordinates.RemoveAll(cell => cell.Equals(new Coordinate(0,0)));
            Console.WriteLine("==================== : Inner : " + 0);
            foreach (Coordinate coordinate in regions[0].InnerCellsCoordinates)
            {
                coordinate.Print();
            }
            Print(grid);*/
            // Console.WriteLine(CanBeInvaded(new Coordinate(0, 0), regions, grid));
            //Console.WriteLine(CanBeInvaded(new Coordinate(1, 0), regions, grid));
        }

        public static int[,] DivideGrid(int[,] grid)
        {
            // Divide the grid in equal sized square regions.
            int gridLength = (int)Math.Sqrt(grid.Length);
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

            // Testing
           /* for (int i = 0; i < gridLength; i++)
            {
                Console.WriteLine("==================== : Surround : " + i);
                foreach(Coordinate coordinate in regions[i].SurroundingCellsCoordinates) 
                {
                    coordinate.Print();
                }

                Console.WriteLine("==================== : Inner : " + i);
                foreach (Coordinate coordinate in regions[i].InnerCellsCoordinates)
                {
                    coordinate.Print();
                }
            }*/

            return grid;
        }

        public static void Randomize(Region[] regions, int[,] grid)
        {
            for(int swap = 0; swap < 50; swap++)
            {
                Console.WriteLine("=========== Randomization : " + swap);
                Random random = new Random(DateTime.UtcNow.Millisecond);
                int gridLength = (int)Math.Sqrt(grid.Length);

                // Randomly choose a region
                int regionAIndexInRegions = random.Next(0, gridLength);
                int regionBIndexInRegions = regionAIndexInRegions;
                Region regionA = regions[regionAIndexInRegions];
                Region regionB = regionA;
                // Randomly select a cell to invade
                bool invasionA = false;
                List<int> invalidIndexOfCellInvadeByRegionAInSurroundingCellsCoordinates = new List<int>();
                Console.WriteLine("=========== InvasionA : ");
                Console.Write("0");
                while (invasionA == false && invalidIndexOfCellInvadeByRegionAInSurroundingCellsCoordinates.Count < regionA.SurroundingCellsCoordinates.Count)
                {
                    Console.Write("1");
                    int indexOfCellInvadeByRegionAInSurroundingCellsCoordinates = random.Next(0, regionA.SurroundingCellsCoordinates.Count);
                    if (!invalidIndexOfCellInvadeByRegionAInSurroundingCellsCoordinates.Contains(indexOfCellInvadeByRegionAInSurroundingCellsCoordinates))
                    {
                        Console.Write("2");
                        Coordinate cellInvadeByRegionA = regionA.SurroundingCellsCoordinates[indexOfCellInvadeByRegionAInSurroundingCellsCoordinates];
                        Console.WriteLine("Cell invade by region A : ");
                        cellInvadeByRegionA.Print();
                        if (CanBeInvaded(cellInvadeByRegionA, regions, grid))
                        {
                            Console.Write("3");
                            regionA.SurroundingCellsCoordinates.RemoveAt(indexOfCellInvadeByRegionAInSurroundingCellsCoordinates);
                            regionA.InnerCellsCoordinates.Add(cellInvadeByRegionA);
                            regionBIndexInRegions = grid[cellInvadeByRegionA.X, cellInvadeByRegionA.Y] - 1;
                            regionB = regions[regionBIndexInRegions];
                            grid[cellInvadeByRegionA.X, cellInvadeByRegionA.Y] = regionAIndexInRegions + 1;

                            foreach (Coordinate coordinate in getCellSurrounding(cellInvadeByRegionA, grid))
                            {
                                Console.Write("4");
                                if (!regionA.SurroundingCellsCoordinates.Contains(coordinate) && !regionA.InnerCellsCoordinates.Contains(coordinate))
                                {
                                    Console.Write("5");
                                    regionA.SurroundingCellsCoordinates.Add(coordinate);
                                }
                            }

                            /*for (int i = 0; i < 4; i++)
                            {*/
                            Console.WriteLine("==================== : Surround : " + 0);
                            foreach (Coordinate coordinate in regions[0].SurroundingCellsCoordinates)
                            {
                                coordinate.Print();
                            }

                            Console.WriteLine("==================== : Inner : " + 0);
                            foreach (Coordinate coordinate in regions[0].InnerCellsCoordinates)
                            {
                                coordinate.Print();
                            }
                            /* }*/

                            regionB.InnerCellsCoordinates.RemoveAll(cell => cell.Equals(cellInvadeByRegionA));
                            regionB.SurroundingCellsCoordinates.Add(cellInvadeByRegionA);
                            foreach (Coordinate coordinate in getCellSurrounding(cellInvadeByRegionA, grid))
                            {
                                Console.Write("6");
                                if (!IsConnectedToRegion(coordinate, regionB, grid))
                                {
                                    Console.Write("7");
                                    regionB.SurroundingCellsCoordinates.RemoveAll(cell => cell.Equals(coordinate));
                                }
                            }
                            for (int i = 0; i < 4; i++)
                            {
                                Console.WriteLine("==================== : Surround : " + 1);
                                foreach (Coordinate coordinate in regions[1].SurroundingCellsCoordinates)
                                {
                                    coordinate.Print();
                                }

                                Console.WriteLine("==================== : Inner : " + 1);
                                foreach (Coordinate coordinate in regions[1].InnerCellsCoordinates)
                                {
                                    coordinate.Print();
                                }
                            }

                            invasionA = true;
                        }
                        else
                        {
                            Console.Write("8");
                            invalidIndexOfCellInvadeByRegionAInSurroundingCellsCoordinates.Add(indexOfCellInvadeByRegionAInSurroundingCellsCoordinates);
                        }
                    }
                }

                if (invasionA)
                {
                    Console.Write("9");
                    bool invasionB = false;
                    List<int> candidateToInvasionByRegionB = new List<int>();
                    for (int i = 0; i < regionB.SurroundingCellsCoordinates.Count; i++)
                    {
                        Console.Write("10");
                        if (regionA.InnerCellsCoordinates.Contains(regionB.SurroundingCellsCoordinates[i]))
                        {
                            Console.Write("11");
                            candidateToInvasionByRegionB.Add(i);
                        }
                    }
                    List<int> invalidIndexOfCellInvadeByRegionBInCandidateToInvasionByRegionB = new List<int>();
                    Console.WriteLine("=========== InvasionB : ");
                    while (invasionB == false && invalidIndexOfCellInvadeByRegionBInCandidateToInvasionByRegionB.Count < candidateToInvasionByRegionB.Count)
                    {
                        Console.Write("12");
                        int indexOfCellInvadeByRegionBInnCandidateToInvasionByRegionB = random.Next(0, candidateToInvasionByRegionB.Count);
                        if (!invalidIndexOfCellInvadeByRegionBInCandidateToInvasionByRegionB.Contains(indexOfCellInvadeByRegionBInnCandidateToInvasionByRegionB))
                        {
                            Console.Write("13");
                            int a = candidateToInvasionByRegionB[indexOfCellInvadeByRegionBInnCandidateToInvasionByRegionB];
                            Coordinate cellInvadeByRegionB = regionB.SurroundingCellsCoordinates[a];
                            Console.WriteLine("Cell invade by region B : ");
                            cellInvadeByRegionB.Print();
                            if (CanBeInvaded(cellInvadeByRegionB, regions, grid))
                            {
                                Console.Write("14");
                                regionB.SurroundingCellsCoordinates.RemoveAll(cell => cell.Equals(cellInvadeByRegionB));
                                regionB.InnerCellsCoordinates.Add(cellInvadeByRegionB);
                                grid[cellInvadeByRegionB.X, cellInvadeByRegionB.Y] = regionBIndexInRegions + 1;
                                foreach (Coordinate coordinate in getCellSurrounding(cellInvadeByRegionB, grid))
                                {
                                    Console.Write("15");
                                    if (!regionB.SurroundingCellsCoordinates.Contains(coordinate) && !regionB.InnerCellsCoordinates.Contains(coordinate))
                                    {
                                        Console.Write("16");
                                        regionB.SurroundingCellsCoordinates.Add(coordinate);
                                    }
                                }
                                regionA.InnerCellsCoordinates.RemoveAll(cell => cell.Equals(cellInvadeByRegionB));
                                regionA.SurroundingCellsCoordinates.Add(cellInvadeByRegionB);
                                foreach (Coordinate coordinate in getCellSurrounding(cellInvadeByRegionB, grid))
                                {
                                    Console.Write("17");
                                    if (!IsConnectedToRegion(coordinate, regionA, grid))
                                    {
                                        Console.Write("18");
                                        regionA.SurroundingCellsCoordinates.RemoveAll(cell => cell.Equals(coordinate));
                                    }
                                }
                                invasionB = true;
                            }
                            else
                            {
                                Console.Write("19");
                                invalidIndexOfCellInvadeByRegionBInCandidateToInvasionByRegionB.Add(indexOfCellInvadeByRegionBInnCandidateToInvasionByRegionB);
                            }
                        }
                    }
                }

               
                Print(grid);
               
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
            Console.WriteLine("Region : "+regionBIndexInRegions);
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
            /*Console.WriteLine("cList count : "+cList.Count);
            foreach(Coordinate co in cList)
            {
               co.Print();
            }*/

            // this case should not happen since a cell should always be connected to the body of its region
            // therefore, it should always be at least one surrounding cell of the same region than the cell we are trying to invade
            if(cList.Count == 0)
            {
                return false;
            }

            return BuildListOfContinuousCellsStartingByCellAtPositionZero(cList[0], cList, grid, new List<Coordinate>()).Count == cList.Count;
            /*
            bool canBeInvaded = false;
            Region region2 = region;
            region2.InnerCellsCoordinates.RemoveAll(x => x == coordinate);
            List<Coordinate> coordinates = new List<Coordinate>();
            coordinates.Add(region.InnerCellsCoordinates[0]);
            if(RecusiveInnerCellsCoordinatesListBuilding(coordinates, region2, grid).Count == region.InnerCellsCoordinates.Count)
            {
                canBeInvaded = true;
            }
            return canBeInvaded;*/
        }

        public static List<Coordinate> BuildListOfContinuousCellsStartingByCellAtPositionZero(Coordinate coordinate, List<Coordinate> coordinates, int[,] grid, List<Coordinate> coordinates2)
        {
            coordinates2.Add(coordinate);
            foreach (Coordinate c in getCellSurrounding(coordinate, grid))
            {
                if (coordinates.Contains(c) && !coordinates2.Contains(c))
                {
                    //coordinates2.Add(c);
                    coordinates2 = coordinates2.Union(BuildListOfContinuousCellsStartingByCellAtPositionZero(c,coordinates, grid,coordinates2)).ToList();
                }
            }

           /* Console.WriteLine("coordinates 2 count : " + coordinates2.Count);
            foreach (Coordinate co in coordinates2)
            {
                co.Print();
            }*/
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


/*public static void Randomize(Region[] regions, int[,] grid)
        {
            for(int swap = 0; swap < 1; swap++)
            {
                Random random = new Random(DateTime.UtcNow.Millisecond);
                int gridLength = (int)Math.Sqrt(grid.Length);
                int regionAIndexInRegions = random.Next(0, gridLength);
                Region regionA = regions[regionAIndexInRegions];
                int indexOfCellInvadeByRegionAInSurroundingCellsCoordinates = random.Next(0, regionA.SurroundingCellsCoordinates.Count);
                Coordinate cellInvadeByRegionA = regionA.SurroundingCellsCoordinates[indexOfCellInvadeByRegionAInSurroundingCellsCoordinates];


                //CanBeInvaded(Coordinate coordinate, Region region, int[,] grid)


                regionA.SurroundingCellsCoordinates.RemoveAt(indexOfCellInvadeByRegionAInSurroundingCellsCoordinates);
                regionA.InnerCellsCoordinates.Add(cellInvadeByRegionA);
                int regionBIndexInRegions = grid[cellInvadeByRegionA.X, cellInvadeByRegionA.Y] - 1;
                Region regionB = regions[regionBIndexInRegions];
                grid[cellInvadeByRegionA.X, cellInvadeByRegionA.Y] = regionAIndexInRegions + 1;
                
                foreach (Coordinate coordinate in getCellSurrounding(cellInvadeByRegionA, grid))
                {
                    if (!regionA.SurroundingCellsCoordinates.Contains(coordinate) && !regionA.InnerCellsCoordinates.Contains(coordinate))
                    {
                        regionA.SurroundingCellsCoordinates.Add(coordinate);
                    }
                }
                regionB.InnerCellsCoordinates.RemoveAll(cell => cell == cellInvadeByRegionA);
                regionB.SurroundingCellsCoordinates.Add(cellInvadeByRegionA);
                foreach (Coordinate coordinate in getCellSurrounding(cellInvadeByRegionA, grid))
                {
                    if (!IsConnectedToRegion(coordinate, regionB, grid))
                    {
                        regionB.SurroundingCellsCoordinates.RemoveAll(cell => cell == coordinate);
                    }
                }




                Coordinate cellInvadeByRegionB = cellInvadeByRegionA;
                for (int i = 0; i< regionB.SurroundingCellsCoordinates.Count; i++)
                {
                    if (regionA.InnerCellsCoordinates.Contains(regionB.SurroundingCellsCoordinates[i])){
                        cellInvadeByRegionB = regionB.SurroundingCellsCoordinates[i];
                        break;
                    }
                }
                regionB.SurroundingCellsCoordinates.RemoveAll(cell=>cell == cellInvadeByRegionB);
                regionB.InnerCellsCoordinates.Add(cellInvadeByRegionB);
                grid[cellInvadeByRegionB.X, cellInvadeByRegionB.Y] = regionBIndexInRegions + 1;
                foreach (Coordinate coordinate in getCellSurrounding(cellInvadeByRegionB, grid))
                {
                    if (!regionB.SurroundingCellsCoordinates.Contains(coordinate) && !regionB.InnerCellsCoordinates.Contains(coordinate))
                    {
                        regionB.SurroundingCellsCoordinates.Add(coordinate);
                    }
                }
                regionA.InnerCellsCoordinates.RemoveAll(cell => cell == cellInvadeByRegionB);
                regionA.SurroundingCellsCoordinates.Add(cellInvadeByRegionB);
                foreach (Coordinate coordinate in getCellSurrounding(cellInvadeByRegionB, grid))
                {
                    if (!IsConnectedToRegion(coordinate, regionB, grid))
                    {
                        regionB.SurroundingCellsCoordinates.RemoveAll(cell => cell == coordinate);
                    }
                }
            }
        }*/

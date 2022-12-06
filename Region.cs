using System;
using System.Collections.Generic;
using System.Text;

namespace GridDivider
{
    class Region
    {
        public int NumberOfCell { get; set; }
        public List<Coordinate> InnerCellsCoordinates { get; set; }
        public List<Coordinate> SurroundingCellsCoordinates { get; set; }

        public Region()
        {
            this.NumberOfCell = 0;
            this.InnerCellsCoordinates = new List<Coordinate>();
            this.SurroundingCellsCoordinates = new List<Coordinate>();
        }


        /*public List<Coordinate> AddCellCoordinate(Coordinate coordinate, List<Coordinate> coordinates)
        {

        }

        public List<Coordinate> RemoveCellCoordinate(Coordinate coordinate, List<Coordinate> coordinates)
        {

        }*/
    }
}

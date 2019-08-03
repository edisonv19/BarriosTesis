﻿using ReadBarrios.Models;
using System.Collections.Generic;

namespace ReadBarrios.Area
{
    public class Polygon
    {
        public List<Coordinate> coordinates { get; set; }

        public bool contains(Coordinate location)
        {
            bool contains = false;
            for (int i = 0, j = this.coordinates.Count - 1; i < this.coordinates.Count; j = i++)
            {
                if (((this.coordinates[i].latitude > location.latitude) != (this.coordinates[j].latitude > location.latitude)) && (location.longitude < (this.coordinates[j].longitude - this.coordinates[i].longitude) * (location.latitude - this.coordinates[i].latitude) / (this.coordinates[j].latitude - this.coordinates[i].latitude) + this.coordinates[i].longitude))
                    contains = !contains;
            }
            return contains;
        }
    }
}

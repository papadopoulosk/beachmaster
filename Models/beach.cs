using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace beachmaster.Models
{
    public class beach
    {
        public int beachId { get; set; } //primary key
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string latitude { get; set; }  //co-ords
        [Required]
        public string longitude { get; set; } //co-ords
        public int numReviews { get; set; } //number of reviews that the beach has received
        public float rate { get; set; } //rating of the beach
        public int votes { get; set; } // number of votes so as to approve the beach by admin
        public bool approved { get; set; } //if this is true, the beach is approved by the admin, otherwise it is still pending
        public string imagePath { get; set; } //Url to store link to beach.. Not yet to upload image..
        //public double Brate { get; set; }

        public int totalRates { get; set; }

        public beach()
        {
            this.numReviews = 0;
        }

        public int submitRate(int rate)
        {
            int tempTotalRates = this.totalRates;
            float tempRate = this.rate;
            this.rate = ((tempTotalRates * tempRate) + rate) / (tempTotalRates + 1);
            this.totalRates = tempTotalRates + 1;
            return 0;
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //:::                                                                         :::
        //:::  This routine calculates the distance between two points (given the     :::
        //:::  latitude/longitude of those points). It is being used to calculate     :::
        //:::  the distance between two locations using GeoDataSource(TM) products    :::
        //:::                                                                         :::
        //:::  Definitions:                                                           :::
        //:::    South latitudes are negative, east longitudes are positive           :::
        //:::                                                                         :::
        //:::  Passed to function:                                                    :::
        //:::    lat1, lon1 = Latitude and Longitude of point 1 (in decimal degrees)  :::
        //:::    lat2, lon2 = Latitude and Longitude of point 2 (in decimal degrees)  :::
        //:::    unit = the unit you desire for results                               :::
        //:::           where: 'M' is statute miles                                   :::
        //:::                  'K' is kilometers (default)                            :::
        //:::                  'N' is nautical miles                                  :::
        //:::                                                                         :::
        //:::  Worldwide cities and other features databases with latitude longitude  :::
        //:::  are available at http://www.geodatasource.com                          :::
        //:::                                                                         :::
        //:::  For enquiries, please contact sales@geodatasource.com                  :::
        //:::                                                                         :::
        //:::  Official Web site: http://www.geodatasource.com                        :::
        //:::                                                                         :::
        //:::           GeoDataSource.com (C) All Rights Reserved 2014                :::
        //:::                                                                         :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //private double distance(double lat1, double lon1, double lat2, double lon2, char unit)
        private double distanceFromOtherPoint(double lat2, double lon2)
        {
            double lat1 = Convert.ToDouble(this.latitude);
            double lon1 = Convert.ToDouble(this.longitude);
            char unit = 'K';
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
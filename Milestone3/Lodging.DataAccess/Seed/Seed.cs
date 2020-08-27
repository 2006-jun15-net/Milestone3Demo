using System.Linq;
using System.Collections.Generic;
using System;
using Lodging.Models;

namespace Lodging.DataAccess.Seed
{
    public static class Seed
    {
        public static void SeedDatabase(LodgingContext context)
        {
            if (context.Lodging.Count() > 0) return;
            if (context.Rentals.Count() > 0) return;
            if (context.Reviews.Count() > 0) return;

            var lodging1 = new LodgingModel() {
                Id = 1,
                Location = new LocationModel()
                {
                    Id = 1,
                    Address = new AddressModel()
                    {
                        Id = 1,
                        LocationId = 1,
                        Street = "110 Smith Road",
                        City = "Tuscon",
                        StateProvince = "AZ",
                        PostalCode = "85701",
                        Country = "USA",
                    },
                    Latitude = "32 N",
                    Longitude = "-110 E",
                    Locale = "English",

                },

                Rentals = new List<RentalModel>()
                {
                    new RentalModel()
                    {
                        Id =1,
                        Name= "Shady Valley",
                        LodgingId =1,
                        Occupancy= 4,
                        Price = 1000,
                        Status= "available",
                        Type = "Lodging",

                    },
                    new RentalModel()
                    {
                        Id =2,
                        Name= "Sacred Waterfalls camping",
                        LodgingId =2,
                        Occupancy= 3,
                        Price = 200,
                        Status= "available",
                        Type = "camping",

                    },
                    new RentalModel()
                    {
                        Id =2,
                        Name= "Honey Moon reservation",
                        LodgingId =3,
                        Occupancy= 2,
                        Price = 400,
                        Status= "occupied",
                        Type = "Lodging",

                    },



                },

                Reviews = new List<ReviewModel>()
                {
                    new ReviewModel()
                    {
                        Id= 1,
                        AccountId = 1,
                        Comment = "Took the wife and kids here, loved it!",
                        LodgingId = 1, 
                        Rating = 5,
                        DateCreated = DateTime.Now,
                        
                    },
                    new ReviewModel()
                    {
                        Id= 2,
                        AccountId = 1,
                        Comment = "Beautiful area, however the campsite was in bad condition when we arrived",
                        LodgingId = 2,
                        Rating = 2,
                        DateCreated = DateTime.Now,

                    },
                    new ReviewModel()
                    {
                        Id= 1,
                        AccountId = 2,
                        Comment = "Had a wonderful Honeymoon here!",
                        LodgingId = 3,
                        Rating = 5,
                        DateCreated = DateTime.Now,

                    },

                }



            };
            var lodging2 = new LodgingModel()
            {
                Id = 2,
                Location = new LocationModel()
                {
                    Id = 4,
                    Address = new AddressModel()
                    {
                        Id = 4,
                        LocationId = 4,
                        Street = "357 N Hoopes Ave",
                        City = "Auburn",
                        StateProvince = "NY",
                        PostalCode = "13021",
                        Country = "USA",
                    },
                    Latitude = "42 N",
                    Longitude = "-76 E",
                    Locale = "English",

                },

                Rentals = new List<RentalModel>()
                {
                    new RentalModel()
                    {
                        Id =4,
                        Name= "Sacred cabin",
                        LodgingId =4,
                        Occupancy= 1,
                        Price = 1100,
                        Status= "booked", 
                        Type = "Lodging",

                    },
                    new RentalModel()
                    {
                        Id =5,
                        Name= "dual suite",
                        LodgingId =4,
                        Occupancy= 2,
                        Price = 600,
                        Status= "available",
                        Type = "Lodging",

                    },
                    new RentalModel()
                    {
                        Id =6,
                        Name= "family accomodation",
                        LodgingId =4,
                        Occupancy= 5,
                        Price = 900,
                        Status= "occupied",
                        Type = "Lodging",

                    },



                },

                Reviews = new List<ReviewModel>()
                {
                    new ReviewModel()
                    {
                        Id= 1,
                        AccountId = 1,
                        Comment = "Astounding! Would highly reccomend to fellow campers.",
                        LodgingId = 4,
                        Rating = 5,
                        DateCreated = DateTime.Now,

                    },
                    new ReviewModel()
                    {
                        Id= 2,
                        AccountId = 1,
                        Comment = "staff was rude",
                        LodgingId = 4,
                        Rating = 1,
                        DateCreated = DateTime.Now,

                    },
                    new ReviewModel()
                    {
                        Id= 3,
                        AccountId = 2,
                        Comment = "Average. Good for large families. ",
                        LodgingId = 4,
                        Rating = 3,
                        DateCreated = DateTime.Now,

                    },

                }

            };
            var lodging3 = new LodgingModel()
            {
                Id = 5,
                Location = new LocationModel()
                {
                    Id = 3,
                    Address = new AddressModel()
                    {
                        Id = 3,
                        LocationId = 3,
                        Street = "124 Berkmar Ave",
                        City = "Charlottesville",
                        StateProvince = "VA",
                        PostalCode = "22901",
                        Country = "USA",
                    },
                    Latitude = "38 N",
                    Longitude = "-78 E",
                    Locale = "English",

                },

                Rentals = new List<RentalModel>()
                {
                    new RentalModel()
                    {
                        Id =1,
                        Name= "Presidental suite",
                        LodgingId =5,
                        Occupancy= 15,
                        Price = 30000,
                        Status= "booked",
                        Type = "Lodging",

                    },
                    new RentalModel()
                    {
                        Id =2,
                        Name= "outback campground",
                        LodgingId =5,
                        Occupancy= 3,
                        Price = 100,
                        Status= "available",
                        Type = "camping",

                    },
                    new RentalModel()
                    {
                        Id =3,
                        Name= "Honey Moon reservation",
                        LodgingId =5,
                        Occupancy= 2,
                        Price = 2000,
                        Status= "occupied",
                        Type = "Lodging",

                    },



                },

                Reviews = new List<ReviewModel>()
                {
                    new ReviewModel()
                    {
                        Id= 1,
                        AccountId = 1,
                        Comment = "Stayed here with the marketing team for buisness conference. Wonderful accomodations",
                        LodgingId = 5,
                        Rating = 5,
                        DateCreated = DateTime.Now,

                    },
                    new ReviewModel()
                    {
                        Id= 2,
                        AccountId = 18,
                        Comment = "my friends and I stayed here, pretty sure its haunted",
                        LodgingId = 5,
                        Rating = 2,
                        DateCreated = DateTime.Now,

                    },
                    new ReviewModel()
                    {
                        Id= 1,
                        AccountId = 7,
                        Comment = "Had a wonderful Honeymoon here!",
                        LodgingId = 5,
                        Rating = 5,
                        DateCreated = DateTime.Now,

                    },

                }

            };

            context.Add(lodging1);
            context.Add(lodging2);
            context.Add(lodging3);
            context.SaveChanges();
        }
    }
}

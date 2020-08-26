using System;
using System.Collections.Generic;
using Lodging.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Linq;
using AutoFixture;
namespace Lodging.IntegrationTests
{
    public static class StaticTestingData
    {
        //Arbitrary number used to limit num
        //of loops during url creation
        private const int IdIterations = 5;

        // Another arbitrary number for limiting
        // length of string generation
        private const int RandomStringLength = 5;

        // Root to be prepended to each url request
        public static string root = "api/v0.0/";

        //Dep for fake data
        private static Fixture _fixture = new Fixture();
        //Routes for the app
        public static List<string> Routes = new List<string>()
    {
      "review",
      "lodging",
      "rental"
    };
        /*
         * <summary>
         * returns List<string baseurl+route>
         *         ex: "api/v0.0/" + "review"
         *              baseurl        route
         *</summary> 
         */
        public static List<object[]> BaseUrls()
        {
            var r = new List<object[]>();
            for (int i = 0; i < Routes.Count; i++)
            {
                r.Add(new object[] { root + Routes[i] });
            }
            return r;
        }
        /*
         * <summary>
         * returns List<string baseurl+(xNum of gibberish strings)>
         *         ex: "api/v0.0/" + nonsense
         *              baseurl        route
         *</summary> 
         */
        public static List<object[]> Get404Requests()
        {
            var r = new List<object[]>();
            for (int i = 0; i < IdIterations; i++)
            {
                r.Add(new object[] { root + Utils.GenerateString(RandomStringLength) });
            }
            return r;
        }
        /*
         * <summary>
         * returns List< ex: ["api/v0.0/" + /route/ + id, "api/v0.0/" + route]>
         * Gets a valid object from url and tries to post it as is to the server
         * </summary>
         */
        public static List<object[]> Get409Requests()
        {
            var r = new List<object[]>();

            foreach (var s in Routes)
            {
                for (int i = 1; i < IdIterations; i++)
                {
                    r.Add(new object[] { root + s + '/' + i.ToString(), root + s });
                }
            }
            return r;
        }
        /*
        * <summary>
        * Returns a list of urls to each route with an id x IdIterations times
        *\/base/route/id
        * </summary>
        */
        public static List<object[]> GetRequests()
        {
            var _ = new List<object[]>();
            _.AddRange(BaseUrls());
            var range = _.Count;
            for (int z = 0; z < range; z++)
            {
                for (int i = 1; i < IdIterations; i++)
                {
                    _.Add(new object[] { _[z][0] + "/" + i });
                }
            }
            return _;
        }
        /*
        * <summary>
        * Returns a list of urls to each route with an id x IdIterations times
        *\/base/route/id
        * </summary>
        */
        public static List<object[]> DeleteRequests()
        {
            var _ = new List<object[]>();
            foreach (var s in BaseUrls())
            {
                for (int i = 1; i < IdIterations; i++)
                {
                    _.Add(new object[] { s[0] + "/" + i });
                }
            }
            return _;
        }
        /*
        * <summary>
        * Returns a list of valid post url and damaged json string as payload
        * </summary>
        */
        public static List<object[]> Post422Requests()
        {
            var _ = new List<object[]>();
            _.AddRange(PostRequests());
            foreach (var z in _)
            {
                z[1] = z[1].ToString() + Utils.GenerateString(RandomStringLength);
            }
            return _;
        }
        /*
        * <summary>
        * Returns a list of [valid post urls, post content]
        * </summary>
        */
        public static List<object[]> PostRequests()
        {
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            return new List<object[]>
      {
        new object[] { "/api/v0.0/review", JObject.FromObject(_fixture.Create<ReviewModel>()) },
        new object[] { "/api/v0.0/rental", JObject.FromObject(_fixture.Create<RentalModel>()) },
        new object[] { "/api/v0.0/lodging", JObject.FromObject(new LodgingModel()
        {
          Id = 1,
          Name = "Quiet Forest Lodge",
          Description = "A quiet lodge with a nearby forest. Great for outdoor activities.",
          Location = new LocationModel()
          {
            Address = new AddressModel()
            {
              City = "Buena Vista",
              Country = "United States",
              PostalCode = "81211",
              StateProvince = "CO",
              Street = "30500 Co Rd 383",
            },
            Latitude = 0,
            Longitude = 0,
          },
          Amenities = new List<AmenityModel>()
          {
            new AmenityModel() { Amenity = "Wifi" },
            new AmenityModel() { Amenity = "Coffee" },
            new AmenityModel() { Amenity = "Pool" },
          },
          Images = new List<ImageModel>()
          {
            new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/seed/lodge-12.jpg" },
            new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/seed/cabin-01.jpg" },
            new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/seed/cabin-02.jpg" },
            new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/seed/cabin-03.jpg" },
            new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/seed/cabin-04.jpg" },
            new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/seed/cabin-05.jpg" },
            new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/seed/cabin-06.jpg" },
          },
          Rentals = new List<RentalModel>()
          {
            new RentalModel() {
              RentalUnit = new RentalUnitModel()
              {
                Name = "Rental Unit 1",
                Description = "Unit for rent",
                Occupancy = 3,
                RentalUnitType = "Rental unit",
                Bedrooms = new List<BedroomModel>()
                {
                  new BedroomModel() {
                    BedType = new BedTypeModel() { BedType = "King" },
                    BedCount = 2,
                    RoomNumber = "100",
                    Amenities = new List<AmenityModel>() {
                      new AmenityModel() { Amenity = "Coffee" },
                      new AmenityModel() { Amenity = "Wifi" },
                      new AmenityModel() { Amenity = "Pool" },
                    },
                    Images = new List<ImageModel>() {
                      new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/square/cabin-05-square.jpg" },
                    }
                  },
                  new BedroomModel() {
                    BedType = new BedTypeModel() { BedType = "King" },
                    BedCount = 2,
                    RoomNumber = "101",
                    Amenities = new List<AmenityModel>() {
                      new AmenityModel() { Amenity = "Coffee" },
                      new AmenityModel() { Amenity = "Wifi" },
                      new AmenityModel() { Amenity = "Pool" },
                    },
                    Images = new List<ImageModel>() {
                      new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/square/cabin-06-square.jpg" },
                    }
                  }
                },
                Bathrooms = new List<BathroomModel>() {
                  new BathroomModel() { Fixture = 1 },
                  new BathroomModel() { Fixture = 2 },
                },
              },
            },
            new RentalModel() {
              RentalUnit = new RentalUnitModel() {
                Name = "Rental Unit 2",
                Description = "Unit for rent",
                Occupancy = 3,
                RentalUnitType = "Rental unit",
                Bedrooms = new List<BedroomModel>() {
                  new BedroomModel() {
                    BedType = new BedTypeModel() { BedType = "Queen" },
                    BedCount = 2,
                    RoomNumber = "200",
                    Amenities = new List<AmenityModel>() {
                      new AmenityModel() { Amenity = "Coffee" },
                      new AmenityModel() { Amenity = "Wifi" },
                      new AmenityModel() { Amenity = "Pool" },
                    },
                    Images = new List<ImageModel>() {
                      new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/square/cabin-01-square.jpg" },
                    }
                  },
                  new BedroomModel() {
                    BedType = new BedTypeModel() { BedType = "King" },
                    BedCount = 2,
                    RoomNumber = "201",
                    Amenities = new List<AmenityModel>() {
                      new AmenityModel() { Amenity = "Coffee" },
                      new AmenityModel() { Amenity = "Wifi" },
                      new AmenityModel() { Amenity = "Pool" },
                    },
                    Images = new List<ImageModel>() {
                      new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/square/cabin-03-square.jpg" },
                    }
                  }
                },
                Bathrooms = new List<BathroomModel>() {
                  new BathroomModel() { Fixture = 1 },
                  new BathroomModel() { Fixture = 2 },
                },
              },
            },
            new RentalModel() {
              RentalUnit = new RentalUnitModel() {
                Name = "Rental Unit 3",
                Description = "Unit for rent",
                Occupancy = 3,
                RentalUnitType = "Rental unit",
                Bedrooms = new List<BedroomModel>() {
                  new BedroomModel() {
                    BedType = new BedTypeModel() { BedType = "King" },
                    BedCount = 1,
                    RoomNumber = "201",
                    Amenities = new List<AmenityModel>() {
                      new AmenityModel() { Amenity = "Coffee" },
                      new AmenityModel() { Amenity = "Wifi" },
                      new AmenityModel() { Amenity = "Pool" },
                    },
                    Images = new List<ImageModel>() {
                      new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/square/cabin-02-square.jpg" },
                    }
                  },
                  new BedroomModel() {
                    BedType = new BedTypeModel() { BedType = "Queen" },
                    BedCount = 2,
                    RoomNumber = "202",
                    Amenities = new List<AmenityModel>() {
                      new AmenityModel() { Amenity = "Coffee" },
                      new AmenityModel() { Amenity = "Pool" },
                    },
                    Images = new List<ImageModel>() {
                      new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/square/cabin-04-square.jpg" },
                    }
                  }
                },
                Bathrooms = new List<BathroomModel>() {
                  new BathroomModel() { Fixture = 1 },
                  new BathroomModel() { Fixture = 2 },
                },
              },
            }
          },
          Reviews = new List<ReviewModel>() {
            new ReviewModel() {
              AccountId = 1,
              Comment = "This lodge is fantastic!",
              DateCreated = DateTime.Now,
              Rating = 9,
            },
            new ReviewModel() {
              AccountId = 2,
              Comment = "I had fun.",
              DateCreated = DateTime.Now,
              Rating = 7,
            },
            new ReviewModel() {
              AccountId = 3,
              Comment = "The nearby forest is great.",
              DateCreated = DateTime.Now,
              Rating = 8.5,
            },
            new ReviewModel() {
              AccountId = 4,
              Comment = "I only came here for coffee.",
              DateCreated = DateTime.Now,
              Rating = 5,
            }
          }
        }) }
      };
        }
    }
}

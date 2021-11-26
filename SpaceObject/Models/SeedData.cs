using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpaceObjectApi.Entities;
using System;
using System.Linq;

namespace SpaceObjectApi.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SpaceObjectContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SpaceObjectContext>>()))
            {
                if (context.SpaceObjects.Any())
                {
                    return;
                }

                context.SpaceObjects.AddRange(
                    new Asteroid
                    {
                        Id = 1,

                        Name = "Икар",

                        Description = "Небольшой околоземный астероид из группы аполлонов, " +
                                  "который характеризуется крайне вытянутой орбитой. Он был открыт" +
                                  "немецким астрономом Вальтером Бааде в Паломарской обсерватории США и назван в честь Икара," +
                                  "персонажа древнегреческой мифологии, известного своей необычной смертью",

                        Weight = "990000000"
                    },

                    new BlackHole
                    {
                        Id = 2,

                        Name = "Holmberg 15A",

                        Description = "Сверхгигантская эллиптическая галактика типа cD в скоплении галактик " +
                                  "Abell 85 в созвездии Кита на расстоянии около 700 млн световых лет от Солнца",

                        Diameter = "1500000"
                    },

                    new Planet
                    {
                        Id = 3,

                        Name = "Марс",

                        Description = "Четвёртая по удалённости от Солнца и седьмая по размеру планета" +
                                 "Солнечной системы; масса планеты составляет 10,7 % массы Земли." +
                                 "Названа в честь Марса — древнеримского бога войны, соответствующего древнегреческому Аресу.",

                        DistanceFromEarth = "55760000"
                    },

                    new Star
                    {
                        Id = 4,

                        Name = "Сириус",

                        Description = "Звезда созвездия Большого Пса. Звезда главной последовательности" +
                                 "спектрального класса A1. Ярчайшая звезда ночного неба," +
                                 "её светимость в 25 раз превышает светимость Солнца, при этом не является " +
                                 "рекордной в мире звёзд.",

                        NumberOfYears = "230000000"
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}

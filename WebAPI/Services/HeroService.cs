using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public static class HeroService
    {
        static List<Hero> Heroes { get; }
        static int nextId = 2;
        static HeroService()
        {
            Heroes = new List<Hero>
        {
                new Hero {
                    id = 1,
                    name = "Superman",
                    ability = 0,
                    startingDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    primaryColor = "blue",
                    secondaryColor = "red",
                    guideId = 1,
                    startingPower = 100,
                    currentPower = 100,
                    dailyTrainCount = 0,
                }
            };
        }

        public static List<Hero> GetAll() => Heroes;

        public static Hero Get(int id) => Heroes.FirstOrDefault(p => p.id == id);

        public static void Add(Hero hero)
        {
            hero.id = nextId++;
            Heroes.Add(hero);
        }

        public static void Train(Hero hero) {
            var rand = new Random();
            double percent = rand.NextDouble() / 10 + 1;
            hero.startingPower = hero.currentPower;
            hero.currentPower = (float)(Math.Round((hero.currentPower * percent) * 100f) / 100f);
            hero.dailyTrainCount++;
            Update(hero);
        }

        public static void Delete(int id)
        {
            var hero = Get(id);
            if (hero is null)
                return;

            Heroes.Remove(hero);
        }

        public static void Update(Hero hero)
        {
            var index = Heroes.FindIndex(p => p.id == hero.id);
            if (index == -1)
                return;

            Heroes[index] = hero;
        }

        public static void ResetDailyCounts() {
            foreach (var hero in Heroes) {
                hero.dailyTrainCount = 0;
            }
        }

        
    }
}
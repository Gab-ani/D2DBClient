using D2DBClient.Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2DBClient
{
    public static class Cybersport
    {

        private static Dictionary<string, Team> _teams;
        private static Dictionary<string, Player> _players;
        private static Dictionary<int, Hero> _heroes;

        static Cybersport()
        {
            _heroes = FetchAllHeroes();
            _players = FetchAllPlayers();
            _teams = FetchAllTeams();
            System.Diagnostics.Debug.WriteLine(_teams["Bublic Squad"].Five.Name);
        }

        public static Player GetPlayer(string nickname)
        {
            return _players[nickname];
        }

        public static Team GetTeam(string name)
        {
            return _teams[name];
        }

        public static Hero GetHero(int id)
        {
            return _heroes[id];
        }

        private static Dictionary<int, Hero> FetchAllHeroes()
        {
            var heroes = new Dictionary<int, Hero>();
            using (StreamReader reader = new StreamReader($"Resources/Heroes.txt"))
            {
                try
                {
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    string heroesSerialized = reader.ReadToEnd();

                    var heroNodes = JArray.Parse(heroesSerialized).ToList();
                    foreach (var heroNode in heroNodes)
                    {
                        Hero hero = ReadHeroNode(heroNode);
                        heroes[hero.Id] = hero;
                    }
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("had trouble deserializing heroes");
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }
            }
            return heroes;
        }

        private static Dictionary<string, Player> FetchAllPlayers()
        {
            var players = new Dictionary<string, Player>();
            using (StreamReader reader = new StreamReader($"Resources/Players.txt"))
            {
                try
                {
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    string heroesSerialized = reader.ReadToEnd();

                    var playerNodes = JArray.Parse(heroesSerialized).ToList();
                    foreach (var playerNode in playerNodes)
                    {
                        Player player = ReadPlayerNode(playerNode);
                        players[player.Nickname] = player;
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("had trouble deserializing heroes");
                }
            }
            return players;
        }

        private static Dictionary<string, Team> FetchAllTeams()
        {
            var teams = new Dictionary<string, Team>();

            using (StreamReader reader = new StreamReader($"Resources/Teams.txt"))
            {
                try
                {
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    string teamsSerialized = reader.ReadToEnd();

                    var teamNodes = JArray.Parse(teamsSerialized).ToList();
                    foreach (var teamNode in teamNodes)
                    {
                        Team team = ReadTeamNode(teamNode);
                        teams[team.Name] = team;
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("had trouble deserializing teams");
                }
            }

            return teams;
        }

        private static Team ReadTeamNode(JToken teamNode)
        {
            string name = teamNode["Name"].ToObject<string>();

            Player carry = GetPlayer(teamNode["Carry"].ToObject<string>());
            Player mid = GetPlayer(teamNode["Mid"].ToObject<string>());
            Player offlane = GetPlayer(teamNode["Offlane"].ToObject<string>());
            Player four = GetPlayer(teamNode["Four"].ToObject<string>());
            Player five = GetPlayer(teamNode["Five"].ToObject<string>());

            Bitmap icon = new Bitmap($"Resources/TeamIcons/" + name + ".png");

            Team team = new Team
            {
                Name = name,
                Carry = carry,
                Mid = mid,
                Offlane = offlane,
                Four = four,
                Five = five,
                Icon = icon
            };
            return team;
        }

        private static Hero ReadHeroNode(JToken heroNode)
        {
            Hero hero = new Hero();

            hero.Name = heroNode["Name"].ToObject<string>();
            hero.Id = heroNode["Id"].ToObject<int>();

            string iconName = hero.Name.Replace("'", "").Replace(" ", "_").Replace("-", "").ToLower();
            hero.Icon = new Bitmap($"Resources/HeroIcons/" + iconName + ".png");

            return hero;
        }

        private static Player ReadPlayerNode(JToken playerNode)
        {
            Player player = new Player();

            player.Nickname = playerNode["Nickname"].ToObject<string>();
            player.Name = playerNode["Name"].ToObject<string>();
            player.HeroPool = new List<Hero>();

            var heropoolNodes = playerNode["HeroPool"].ToList();
            foreach (var idNode in heropoolNodes)
            {
                player.HeroPool.Add(GetHero(idNode.ToObject<int>()));
            }



            return player;
        }
    }
}
